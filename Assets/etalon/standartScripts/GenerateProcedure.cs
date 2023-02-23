using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.Networking;
using System.Net.NetworkInformation;
using System.IO;
using UnityEngine.SceneManagement;


public class GenerateProcedure : MonoBehaviour 
{
	
	
	
	
	public InputField t0;
	public InputField password2;

	public InputField password;

	int summ; 
	
	public void EXIT()
	{
		Application.Quit();
	}
	
	
	
		

	
	
	string toSERIAL(string CHECK)
	{
		string MACADRESS = CHECK;
		string temp1 ="";
		int PASS=0;
		for (int i=0 ; i<12*2;i++)
		{
			int value =0;
			if (MACADRESS[i]=='1') value=1;
			if (MACADRESS[i]=='2') value=2;
			if (MACADRESS[i]=='3') value=3;
			if (MACADRESS[i]=='4') value=4;
			if (MACADRESS[i]=='5') value=5;
			if (MACADRESS[i]=='6') value=6;
			if (MACADRESS[i]=='7') value=7;
			if (MACADRESS[i]=='8') value=8;
			if (MACADRESS[i]=='9') value=9;
			
			PASS += (value)*(i+value)*(i+value);
		}
		
		PASS*=PASS;
		return PASS.ToString();
	}

	
	public void CHECK()
	{
		Toggle[] allObjects = UnityEngine.Object.FindObjectsOfType<Toggle>() ;
		
		string full = t0.text;
		string[] x = full.Split('-');	
		int summ=0;
		foreach (string s in x)
        {   
            summ += int.Parse(s, System.Globalization.NumberStyles.HexNumber);
        }
		//password.text = summ.ToString();
		
		string result = "";

        foreach (Toggle box in allObjects)
		{
			if (box.isOn == true)
			{
				string id = box.GetComponentInChildren<Text> ().text;
				int pass = summ + int.Parse(id, System.Globalization.NumberStyles.HexNumber);
				result += id + "-" + pass.ToString() + ";";
			}
		}
		
		password.text = result.ToString();
		
		
		//
		string ComplexID = "000000";
		string MACADRESS = full.Replace("-", ""); 
		string FORCHECK = "NIIEOR "+MACADRESS+ComplexID;
		string SERIAL = toSERIAL (FORCHECK);
		password2.text = SERIAL;
		
		
	}
	

}