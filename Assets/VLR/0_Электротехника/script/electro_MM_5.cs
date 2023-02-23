using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System.IO;
using System.Diagnostics;
using TMPro;

// https://www.partsim.com/simulator/#78470

public class electro_MM_5 : MonoBehaviour
{
    private IEnumerator coroutine;

    //---------------A---------------
    public PowerSupplyClass PowerSupply;
    public ReostatClass Reostat1;
    public SoprotivlenieClass R_2;
   
    public TextMeshPro A1_text;
    public TextMeshPro V1_text;
    public TextMeshPro V2_text;

    //---------------B---------------

    public PowerSupplyClass PowerSupply_2;
    public ReostatClass Reostat_2_1;
    public SoprotivlenieClass R_2_1;
    public SoprotivlenieClass R_2_2;

    public TextMeshPro A2_1_text;
    public TextMeshPro A2_2_text;
    public TextMeshPro A2_3_text;
    public TextMeshPro V2_1_text;

    //---------------C---------------
    public PowerSupplyClass PowerSupply_3;
    public ReostatClass Reostat_3_1;
    public SoprotivlenieClass R_3_1;
    public SoprotivlenieClass R_3_2;

    public TextMeshPro A3_1_text;
    public TextMeshPro A3_2_text;
    public TextMeshPro A3_3_text;
    public TextMeshPro V3_1_text;



    // Start is called before the first frame update
    public void Start1()
    {
        UnityEngine.Debug.Log("5 start");
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


            //-------------------------------A----------------------------------------
            float PowerSupplyV = 0;
            //заменяем на наши значения
            if (PowerSupply.On == true)
            {
                PowerSupplyV = PowerSupply.V;
            }
            float R1 = Reostat1.R;
            float R2 = R_2.R;

            //-------------------------------B----------------------------------------
            float PowerSupplyV_2 = 0;
            //заменяем на наши значения
            if (PowerSupply_2.On == true)
            {
                PowerSupplyV_2 = PowerSupply_2.V;
            }
            float R2__1 = Reostat_2_1.R;
            float R2__2 = R_2_1.R;
            float R2__3 = R_2_1.R;
            //-------------------------------C----------------------------------------
            float PowerSupplyV_3 = 0;
            //заменяем на наши значения
            if (PowerSupply_3.On == true)
            {
                PowerSupplyV_3 = PowerSupply_3.V;
            }
            float R3__1 = Reostat_3_1.R;
            float R3__2 = R_3_1.R;
            float R3__3 = R_3_1.R;



            //-----------------------------------------------------------------------------
            //открываем эталон
            var file1 = new StreamReader(Application.streamingAssetsPath + "/Spice64/bin/Lab5/5_etalon.sp");
            string fileContents = file1.ReadToEnd();
            file1.Close();

           
            string newContents = fileContents.Replace("###V1###", PowerSupplyV.ToString("N6").Replace(",", "."));
            newContents = newContents.Replace("###R1###", R1.ToString("N8").Replace(",", "."));
            newContents = newContents.Replace("###R2###", R2.ToString("N8").Replace(",", "."));

            newContents = newContents.Replace("###V2###", PowerSupplyV_2.ToString("N8").Replace(",", "."));
            newContents = newContents.Replace("###R21###", R2__1.ToString("N8").Replace(",", "."));
            newContents = newContents.Replace("###R22###", R2__3.ToString("N8").Replace(",", "."));
            newContents = newContents.Replace("###R23###", R2__2.ToString("N8").Replace(",", "."));

            newContents = newContents.Replace("###V3###", PowerSupplyV_3.ToString("N8").Replace(",", "."));
            newContents = newContents.Replace("###R31###", R3__1.ToString("N8").Replace(",", "."));
            newContents = newContents.Replace("###R32###", R1.ToString("N8").Replace(",", "."));
            newContents = newContents.Replace("###R33###", R2.ToString("N8").Replace(",", "."));

            //пишем в файл для выполнения
            var sr = File.CreateText(Application.streamingAssetsPath + "/Spice64/bin/Lab5/Lab5_calc.sp");
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
            startInfo.Arguments = @"-b ./Lab5/Lab5_calc.sp";

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

            float V1 = GetFloatFromFile("/Lab5/V1_1.out");
            V1_text.text = V1.ToString("N4");
            float V2 = GetFloatFromFile("/Lab5/V1_2.out");
            V2_text.text = V2.ToString("N4");
            float A1 = GetFloatFromFile("/Lab5/A1_1.out");
            A1_text.text = A1.ToString("N4");
            ///////////////////////////////

            float V2_1 = GetFloatFromFile("/Lab5/V2_1.out");
            V2_1_text.text = V2_1.ToString("N4");
            float A2_1 = GetFloatFromFile("/Lab5/A2_1.out");
            A2_1_text.text = A2_1.ToString("N4");
            float A2_2 = GetFloatFromFile("/Lab5/A2_2.out");
            A2_2_text.text = A2_2.ToString("N4");
            float A2_3 = GetFloatFromFile("/Lab5/A2_3.out");
            A2_3_text.text = A2_3.ToString("N4");
            ///////////////////////////////

            float V3_1 = GetFloatFromFile("/Lab5/V3_1.out");
            V3_1_text.text = V3_1.ToString("N4");
            float A3_1 = GetFloatFromFile("/Lab5/A3_1.out");
            A3_1_text.text = A3_1.ToString("N4");
            float A3_2 = GetFloatFromFile("/Lab5/A3_2.out");
            A3_2_text.text = A2_3.ToString("N4");
            float A3_3 = GetFloatFromFile("/Lab5/A3_3.out");
            A3_3_text.text = A3_3.ToString("N4");
            /////////////////////////////




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
