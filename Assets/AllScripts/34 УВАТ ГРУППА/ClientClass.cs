
using UnityEngine;
using UnityEngine.UI;
using TMPro;
using System.Collections;
using System.Collections.Generic;
using System.Net;
using System.Net.Sockets;
using System.Threading;
using System;
using System.IO;

public class ClientClass : MonoBehaviour 
{
	private TcpClient client = null;
	private NetworkStream stream = null;
	private string IP;
	private int mynumber;
	
	int _A,_B,_C;
	
	void Start()
	{
		_A=0;
		_B=0;
		_C=0;
		
		//запускаем с задержкой в 2 секунды
		//StartCoroutine(Waiter());
		
		StartClient();
		
		//запускаем PINGPONG с задержкой в 5 секунды
		StartCoroutine(PINGPONG());
	}
	
	public void SentData(int A, int B, int C)
	{
		_A = A;
		_B = B;
		_C = C;
	
		try
		{
			byte[] byteArray1 = BitConverter.GetBytes(mynumber);
			byte[] byteArray2 = BitConverter.GetBytes(A);
			byte[] byteArray3 = BitConverter.GetBytes(B);
			byte[] byteArray4 = BitConverter.GetBytes(C);
			
			byte[] result = {byteArray1[0], byteArray1[1], byteArray1[2], byteArray1[3], byteArray2[0], byteArray2[1], byteArray2[2], byteArray2[3], byteArray3[0], byteArray3[1], byteArray3[2], byteArray3[3], byteArray4[0], byteArray4[1], byteArray4[2], byteArray4[3]};



			if (stream.CanWrite)
			{
			  stream.Write(result, 0, result.Length);
			}
		}
		catch (Exception ex)
		{
			Debug.Log("exception  : " + ex);
			return;
		}
	}
	
	//PINGPONG
	IEnumerator PINGPONG()
	{
		while (true)
		{
			yield return new WaitForSeconds(10);
			
			
			SentData(_A,_B,_C);
				
			
			//for
		}
	}
	//PINGPONG
	
	
	
	//запускает подключение к серверу через 2 секунды для того чтобы сервер успел стартануть
	/*
	IEnumerator Waiter()
	{
		yield return new WaitForSeconds(1);
		//запустить клитента
		StartClient();
	}
	*/
	
	void StartClient()
	{
		try
		{
			//
			string filename = Path.Combine(Application.streamingAssetsPath, "server.txt");
			if (File.Exists(filename)==true)
			{
				IP = File.ReadAllText(filename);
				Debug.Log ("Загружаю файл server=" + filename + ", IP=" + IP);	
			}
			else
			{
				IP="127.0.0.1";
				Debug.Log ("Файла server нет=" + filename + ", IP=" + IP);	
			}
			
			string filename2 = Path.Combine(Application.streamingAssetsPath, "mynumber.txt");
			if (File.Exists(filename2)==true)
			{
				string _mynumber = File.ReadAllText(filename2);
				mynumber = int.Parse(_mynumber);
				Debug.Log ("Загружаю файл mynumber=" + filename + ", _mynumber=" + mynumber);	
			}
			else
			{
				Debug.Log ("Файла mynumber нет=" + filename);	
			}
			
			
			//
			client = new TcpClient(IP,60000);
			stream = client.GetStream();
			//stream.ReadTimeout = 60000;
			Debug.Log ("Клиент. Я подключился к серверу.");
			
			SentData(0,0,0);
			
			
			
			
		}
		catch (Exception ex)
		{
			Debug.Log("exception  : " + ex);
			return;
		}
	}
	
	
}
