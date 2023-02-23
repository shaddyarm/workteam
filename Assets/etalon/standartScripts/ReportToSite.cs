using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.Networking;
using System.Net.NetworkInformation;
using System.IO;
using UnityEngine.SceneManagement;

using System.Net;
using System.Net.Sockets;


public class ReportToSite : MonoBehaviour 
{
	private IEnumerator coroutine;
	
	public string GUID;
	
	public string score="99";
	public string min="0";
	public string max="100";
	public string status="completed";
	public string report="report detailed на русском";

	// Use this for initialization
	void Start () 
	{
		//привет % 20всем % 20 /% 20ДА

		//string t2 = UnityWebRequest.EscapeURL("Привет всем / ДА!");
		//string t2 = WWW.EscapeURL("Привет всем / ДА!");
		//string t1 = "https://lcontent.ru/setreport.php?type=setreport&name=e1&score=90&min=0&max=100&status=completed&report=" + t2;

		//Debug.Log("t1=" + t1);
		//Debug.Log("t2=" + t2);


		if (Application.isEditor == true)
		{
			System.DateTime theTime = System.DateTime.Now;
			string datetime = theTime.ToString("yyyy-MM-dd-HH:mm:ss");
			GUID = "Editor" + datetime;
		}
		else
		{
			string[] args = System.Environment.GetCommandLineArgs();
			if (args.Length > 1)
			{
				GUID = args[1];
			}
			else
			{
				GUID = "";
			}
		}
	}
	
	public void StartCheck()
	{
		if (GUID=="") return;
		
		//запускаем процедуру отправки
		coroutine = Check();
		StartCoroutine(coroutine);
	}
	
	private IEnumerator Check() 
	{
		yield return new WaitForSeconds(0.1f);

		//https://lcontent.ru/setreport.php?type=setreport&name=e28dfb13-be05-4eb4-a572-0721ff1c61bb&score=90&min=0&max=100&status=completed&report=dfjsdkhf\n1kjsdhkjfkjsdhkjfhskd\njhfk%20skdjhfkjsdhkjfskdjfkjsdkjf%20skdjhfkjsdhkjfskdjfkjsdf%20skdjhfkjsdkjfkjsdfkj%20sdkjfhksdhfkjsdhkjfhksjdhfkjh%20sdkjfhksdhkfjhsdkjfhkjsdhf%20sdkjfhksdhfkjhsdkjfhkjsdhkjf
		string host = "https://lcontent.ru//setreport.php?type=setreport&name="+GUID+"&score=" + score +"&min=" + min +"&max=" + max + "&status=" + status + "&report=" + WWW.EscapeURL(report);

		// string escName = UnityWebRequest.EscapeURL("Fish & Chips");

		var Query = new WWW(host);
		yield return Query;

		if (Query.error != null) 
		{
			Debug.Log("Check query failed " + Query.error );	
			//Нет связи с интернотом/сервером, просто выходим и все
		} 
		else 
		{
			Debug.Log("Check query: " + Query.text);
		}
		Query.Dispose();
		
		this.gameObject.SetActive(false);
	}
	
	
	
	

	
}