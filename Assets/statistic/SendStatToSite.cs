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


public class SendStatToSite : MonoBehaviour 
{
	private IEnumerator coroutine;
	
	public string org;
	public string name;

	// Use this for initialization
	void Start () 
	{	 
		if (Application.isEditor==true) return;
		StartCheck();
	}
	
	public void StartCheck()
	{
		//запускаем процедуру проверки
		coroutine = Check();
		StartCoroutine(coroutine);
	}
	
	private IEnumerator Check() 
	{
		
		yield return new WaitForSeconds(10f);
		
		float CurrentFPS = 1f/Time.deltaTime;
		float fps = UpdateCumulativeMovingAverageFPS(CurrentFPS);
		Debug.Log ("fps=" + fps.ToString());
		
		
		string video = SystemInfo.graphicsDeviceName;
		string cpu = SystemInfo.processorType;
		
		//https://lcontent.ru/statistic.php?type=statistic&name=Насосы&org=ТИУ&ip=127.0.0.1
		string host = "https://lcontent.ru/statistic.php?type=statistic&name="+name+"&org=" + org +"&cpu=" + cpu +"&video=" + video + " (fps=" + fps.ToString() +")" + "&ip=" + GetLocalIPAddress();
		
		
		
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
	
	public string GetLocalIPAddress()
	{
		#if UNITY_WEBGL
             return "webgl";     
        #endif

		#if !UNITY_WEBGL
			var host = Dns.GetHostEntry(Dns.GetHostName());
			foreach (var ip in host.AddressList)
			{
				if (ip.AddressFamily == AddressFamily.InterNetwork)
				{
					return ip.ToString();
				}
			}
			return "not detect";     
        #endif
		 
		
	}
	
	//FPS
	
	float qty = 1f;
	float currentAvgFPS = 30f;
	
	
	void Update()
	{
		float CurrentFPS = 1f/Time.deltaTime;
		UpdateCumulativeMovingAverageFPS(CurrentFPS);
	}
	
	float UpdateCumulativeMovingAverageFPS(float newFPS)
	{
		qty+=0.1f;
		currentAvgFPS += (newFPS - currentAvgFPS)/qty;
		return currentAvgFPS;
	}

	
}