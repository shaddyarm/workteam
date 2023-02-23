using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.Networking;
using System.Net.NetworkInformation;
using System.IO;
using UnityEngine.SceneManagement;


public class CheckProcedure : MonoBehaviour 
{
	public GameObject EnterLoginDialog;
	public InputField LoginText;
	public GameObject NoInetText;

	private IEnumerator coroutine;

	
	string MYMAC;
	string login;
	public string productCode;


	// Use this for initialization
	void Start () 
	{	
		EnterLoginDialog.SetActive(false);
	
	
		NoInetText.SetActive(false);
		
		login="";
	
		//if (Application.isEditor) return;
		
		
		
		
		//получаем mac-адреса
		ShowNetworkInterfaces();
		
		
		//смотрим в реестр.... ищем сохраненные login
		login = PlayerPrefs.GetString("MAXGAMMER_CLIENT_LOGIN", "");
				
		if (login=="")
		{
			EnterLoginDialog.SetActive(true);
		}
		else
		{
			StartCheck();
		}
		
	}
	
	public void StartCheck()
	{
		NoInetText.SetActive(false);
		
		Debug.Log("StartCheck");	
		if (login=="") login = LoginText.text;
		//запускаем процедуру проверки
		coroutine = Check();
		StartCoroutine(coroutine);
	}
	
	public void Exit()
	{
		#if (UNITY_EDITOR)
			UnityEditor.EditorApplication.isPlaying = false;
		#elif (UNITY_STANDALONE) 
			Application.Quit();
		#elif (UNITY_WEBGL)
			Application.OpenURL("about:blank");
		#endif
	}

	//
	public void ShowNetworkInterfaces()
	{
	 string info="";
	 IPGlobalProperties computerProperties = IPGlobalProperties.GetIPGlobalProperties();
	 NetworkInterface[] nics = NetworkInterface.GetAllNetworkInterfaces();

	 foreach (NetworkInterface adapter in nics)
	 {
		 PhysicalAddress address = adapter.GetPhysicalAddress();
		 byte[] bytes = address.GetAddressBytes();
		 string mac = null;
		 for (int i = 0; i < bytes.Length; i++)
		 {
			 mac = string.Concat(mac +(string.Format("{0}", bytes[i].ToString("X2"))));
			 if (i != bytes.Length - 1)
			 {
				 mac = string.Concat(mac + "-");
			 }
		 }
		 info += mac + "\n";
		 if (mac!=null)
		 {
			if (mac.Substring(0,2) != "00") 
			{
				MYMAC = mac;
				Debug.Log(MYMAC);
				return;
			}
		 }
	 }

	 
	}
	 
	
	
	private IEnumerator Check() 
	{
		Debug.Log("Check");	
		//https://lcontent.ru/check.php?type=check&login=MAX&MAC=54-04-A6-C0-57-E4&product_id=14
		string host = "https://lcontent.ru/check.php?type=check&login="+login+"&MAC=" + MYMAC + "&product_id=" + productCode;
		
		var Query = new WWW(host);
		yield return Query;

		if (Query.error != null) 
		{
			Debug.Log("Check query failed " + Query.error );	
			//Нет связи с интернотом/сервером, просто выходим и все
			if (PlayerPrefs.GetString("MAXGAMMER_CLIENT_LOGIN", "")!="") 
			{
				EnterLoginDialog.SetActive(false);
			}
			else
			{
				EnterLoginDialog.SetActive(true);
				NoInetText.SetActive(true);
			}
		} 
		else 
		{
			Debug.Log("Check query: " + Query.text);
			Debug.Log(Query.text);	
			
			if (Query.text == " true")
			{
				//ok
				EnterLoginDialog.SetActive(false);
				PlayerPrefs.SetString("MAXGAMMER_CLIENT_LOGIN", login);
				yield break;
			}	
			else
			{
				Debug.Log("BLOCK!!!");
				PlayerPrefs.SetString("MAXGAMMER_CLIENT_LOGIN", "");
				EnterLoginDialog.SetActive(true);
				yield break;
			}			
		}
		Query.Dispose();
	}
	
	
	
}