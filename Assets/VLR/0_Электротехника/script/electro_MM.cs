using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System.IO;
using System.Diagnostics;
using TMPro;

// https://www.partsim.com/simulator/#78470

public class electro_MM : MonoBehaviour
{
    private IEnumerator coroutine;

    public PowerSupplyClass PowerSupply;
    public ReostatClass Reostat1;
    public SoprotivlenieClass R_2;
    public TransformatorClass Transformator;
    public SwitchClass switcher;

    public TextMeshPro V1_text;
    public TextMeshPro A1_text;
    public TextMeshPro V2_text;
    public TextMeshPro A2_text;


    // Start is called before the first frame update
    public void Start1()
    {
        UnityEngine.Debug.Log("1 start");
        coroutine = Call_ngspice();
        StartCoroutine(coroutine);
    }

    private IEnumerator Call_ngspice()
    {
        bool busy = false;
        while (true)
        {
            yield return new WaitForSeconds(1f);

            if (busy == true) yield return new WaitForSeconds(1f);
            busy = true;

            //-------------Читаем текущие значения из 3D-----------------------------------

            float PowerSupplyV = 0;
            //заменяем на наши значения
            if (PowerSupply.On == true)
            {
                PowerSupplyV = PowerSupply.V;
            }

            //

            float R1 = Reostat1.R;
            float R2 = R_2.R;

            float L1 = Transformator.L1;
            float L2 = Transformator.L2;


            if (switcher.Position==1f)
            {
            }
            else if (switcher.Position == 2f)
            {
                R2 = 999999f;
            }
            else if (switcher.Position == 3f)
            {
                //КЗ
                R2 = 0.1f;
            }



            //-----------------------------------------------------------------------------
            //открываем эталон
            var file1 = new StreamReader(Application.streamingAssetsPath + "/Spice64/bin/Lab1/Lab1_etalon.sp");
            string fileContents = file1.ReadToEnd();
            file1.Close();

           
            string newContents = fileContents.Replace("###V###", PowerSupplyV.ToString("N6").Replace(",", "."));
            newContents = newContents.Replace("###R1###", R1.ToString("N8").Replace(",", "."));
            newContents = newContents.Replace("###R2###", R2.ToString("N8").Replace(",", "."));
            newContents = newContents.Replace("###L1###", L1.ToString("N8").Replace(",", "."));
            newContents = newContents.Replace("###L2###", L2.ToString("N8").Replace(",", "."));


            //пишем в файл для выполнения
            var sr = File.CreateText(Application.streamingAssetsPath + "/Spice64/bin/Lab1/Lab1_calc.sp");
            sr.Write(newContents);
            sr.Close();

            //-----------------------------------------------------------------------------

            //ngspice_con -b -o output.txt ./voltage_divider.sp

            string exefile = Path.Combine(Application.streamingAssetsPath, "Spice64/bin/ngspice_con.exe");
            string workingDirectory = Path.Combine(Application.streamingAssetsPath, "Spice64/bin");

            ProcessStartInfo startInfo = new ProcessStartInfo();
            startInfo.RedirectStandardOutput = true;
            startInfo.UseShellExecute = false;
            startInfo.RedirectStandardError = true;
            startInfo.CreateNoWindow = true;
            startInfo.FileName = exefile;
            startInfo.WorkingDirectory = workingDirectory;
            //startInfo.Arguments = @"-b -o output.txt ./voltage_divider.sp";
            startInfo.Arguments = @"-b ./Lab1/Lab1_calc.sp";

            // start process
            Process proc = new Process();
            proc.StartInfo = startInfo;
            proc.Start();
            //proc.WaitForExit();
            proc.EnableRaisingEvents = true;
            proc.Exited += delegate
            {
                //UnityEngine.Debug.Log(".");
                // read process output
                string cmdError = proc.StandardError.ReadToEnd();
                string cmdOutput = proc.StandardOutput.ReadToEnd();
                //UnityEngine.Debug.Log(cmdError);
                //UnityEngine.Debug.Log(cmdOutput);
                busy = false;
            };

            //-------------читаем файлы и вытаскиваем показания-----------------------------------

            float V1 = GetFloatFromFile("/Lab1/V1.out");
            V1_text.text = V1.ToString("N4");
            //UnityEngine.Debug.Log(V1);
            float V2 = GetFloatFromFile("/Lab1/V2.out");
            V2_text.text = V2.ToString("N4");
            float A1 = -1f *  GetFloatFromFile("/Lab1/A1.out");
            A1_text.text = A1.ToString("N4");
            float A2 = GetFloatFromFile("/Lab1/A2.out");
            A2_text.text = A2.ToString("N4");

            if (switcher.Position == 1f)
            {
            }
            else if (switcher.Position == 2f)
            {
                V2_text.text = "0";
                A2_text.text = "0";
            }
            else if (switcher.Position == 3f)
            {
                
            }


        }
    }


    private float GetFloatFromFile(string filename)
    {
        string FullFilename = Application.streamingAssetsPath + "/Spice64/bin" + filename;
        //открываем файл
        var file1 = new StreamReader(FullFilename);
        string fileContents = file1.ReadToEnd();
        file1.Close();
        //находим последнюю строку
        string[] Array1 = fileContents.Split("\n"[0]);
        string line1 = Array1[Array1.Length - 3];
        //берем первый столбец
        string[] Array2 = line1.Split(',');
        string line2 = Array2[0];
        //заменяем .,
        line2 = line2.Replace(".", ",");
        return float.Parse(line2);
    }



}
