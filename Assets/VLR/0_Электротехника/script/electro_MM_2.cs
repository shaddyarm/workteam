using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System.IO;
using System.Diagnostics;
using TMPro;

//https://www.partsim.com/simulator#384301

public class electro_MM_2 : MonoBehaviour
{
    private IEnumerator coroutine;

    public PowerSupplyClass PowerSupply;
    public ReostatClass Reostat1;
    public SoprotivlenieClass R_2;
    public SoprotivlenieClass R_3;
    
    public SwitchClass switcher1;
    public SwitchClass switcher2;
    public SwitchClass switcher3;
    public SwitchClass switcher4;

    public TextMeshPro A1_text;
    public TextMeshPro A2_text;
    public TextMeshPro A3_text;
    public TextMeshPro A4_text;


    // Start is called before the first frame update
    public void Start1()
    {
        UnityEngine.Debug.Log("2 start");
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

            float PowerSupplyV1 = 0;
            float PowerSupplyV2 = 0;
            float PowerSupplyV3 = 0;
            //заменяем на наши значения
            if (PowerSupply.On == true)
            {
                PowerSupplyV1 = PowerSupply.V;
                PowerSupplyV2 = PowerSupply.V;
                PowerSupplyV3 = PowerSupply.V;
            }


            float R1 = Reostat1.R;
            float R2 = R_2.R;
            float R3 = R_3.R;
            float R4 = 500f;

            //выключен
            if (switcher1.Position!=2f)
            {
                PowerSupplyV1 = 0;
                R1 = 10000001f;
            }
            //
            if (switcher2.Position != 2f)
            {
                PowerSupplyV2 = 0;
                R2 = 10000001f;
            }
            //
            if (switcher3.Position != 2f)
            {
                PowerSupplyV3 = 0;
                R3 = 10000001f;
            }
            //
            if (switcher4.Position != 2f)
            {
                R4 = 10000001f;
            }
            //-------------

            //-----------------------------------------------------------------------------
            //открываем эталон
            var file1 = new StreamReader(Application.streamingAssetsPath + "/Spice64/bin/Lab2/star_etalon.sp");
            string fileContents = file1.ReadToEnd();
            file1.Close();

           
            string newContents = fileContents.Replace("###V1###", PowerSupplyV1.ToString("N6").Replace(",", "."));
            newContents = newContents.Replace("###V2###", PowerSupplyV2.ToString("N6").Replace(",", "."));
            newContents = newContents.Replace("###V3###", PowerSupplyV3.ToString("N6").Replace(",", "."));

            string strR1 = R1.ToString("N8").Replace(",", ".");
            string strR2 = R2.ToString("N8").Replace(",", ".");
            string strR3 = R3.ToString("N8").Replace(",", ".");
            string strR4 = R4.ToString("N8").Replace(",", ".");

            if (R1 > 100000f) strR1 = "10000000M";
            if (R2 > 100000f) strR2 = "10000000M";
            if (R3 > 100000f) strR3 = "10000000M";
            if (R4 > 100000f) strR4 = "10000000M";

            newContents = newContents.Replace("###R1###", strR1);
            newContents = newContents.Replace("###R2###", strR2);
            newContents = newContents.Replace("###R3###", strR3);
            newContents = newContents.Replace("###R4###", strR4);


            //пишем в файл для выполнения
            var sr = File.CreateText(Application.streamingAssetsPath + "/Spice64/bin/Lab2/star_calc.sp");
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
            startInfo.Arguments = @"-b -o ./Lab2/output.txt ./Lab2/star_calc.sp";

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

            GetAllFromFile("/Lab2/output.txt");
            
        }
    }


    private void GetAllFromFile(string filename)
    {
        string FullFilename = Application.streamingAssetsPath + "/Spice64/bin" + filename;
        //открываем файл
        var file1 = new StreamReader(FullFilename);
        string fileContents = file1.ReadToEnd();
        file1.Close();
        //находим последнюю строку
        string[] Array1 = fileContents.Split("\n"[0]);

        int found = 0;
        for (int a=0;a< Array1.Length;a++)
        {
            if (Array1[a].Contains("a_1                 =") == true)
            {
                found = a;
                break;
            }
        }

       

        if (found == 0) return;

        float _A1 = 0;
        //a1
        {
            string strA1 = Array1[found+0];

            int Start, End;
            Start = strA1.IndexOf("=  ", 0);
            End = strA1.IndexOf(" at=  ", Start);
            string result = strA1.Substring(Start+3, End - Start-3);
            result = result.Replace(".", ",");
            _A1 = float.Parse(result);
        }

        float _A2 = 0;
        //a1
        {
            string strA1 = Array1[found + 1];

            int Start, End;
            Start = strA1.IndexOf("=  ", 0);
            End = strA1.IndexOf(" at=  ", Start);
            string result = strA1.Substring(Start + 3, End - Start - 3);
            result = result.Replace(".", ",");
            _A2 = float.Parse(result);
        }

        float _A3 = 0;
        //a1
        {
            string strA1 = Array1[found + 2];

            int Start, End;
            Start = strA1.IndexOf("=  ", 0);
            End = strA1.IndexOf(" at=  ", Start);
            string result = strA1.Substring(Start + 3, End - Start - 3);
            result = result.Replace(".", ",");
            _A3 = float.Parse(result);
        }

        float _A4 = 0;
        //a1
        {
            string strA1 = Array1[found + 3];

            int Start, End;
            Start = strA1.IndexOf("=  ", 0);
            End = strA1.IndexOf(" at=  ", Start);
            string result = strA1.Substring(Start + 3, End - Start - 3);
            result = result.Replace(".", ",");
            _A4 = float.Parse(result);
        }




        A1_text.text = _A1.ToString("N3");
        A2_text.text = _A2.ToString("N3");
        A3_text.text = _A3.ToString("N3");
        A4_text.text = _A4.ToString("N3");

    }



}
