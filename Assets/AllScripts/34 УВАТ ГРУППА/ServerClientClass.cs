/***************************************************************************
ServerClient.cs  - Обертка TcpClient 
-------------------
begin                : 5 октября 2019
copyright            : (C) 2019 by Гаммер Максим Дмитриевич (maximum2000)
email                : MaxGammer@gmail.com
site				 : lcontent.ru 
org					 : ИП Гаммер Максим Дмитриевич
***************************************************************************/

using System;
using System.Collections;
using System.Collections.Generic;
using System.Net;
using System.Net.Sockets;
using System.Text;
using System.Threading;
using UnityEngine;


class ServerClientClass 
{
	public string UserName="";
	public bool isReady=false;
	
	Mutex mutexObj = new Mutex();
	
	bool fail=false;
	
	private TcpClient clientSocket = null;
	private Thread ReadThread;
	private NetworkStream stream = null;
	
	//тут храним все прочитанные байты
	private List<byte> ReadedBytes = new List<byte>();
	
			
	//конструктор
	public ServerClientClass(TcpClient _clientSocket)
	{
		fail=false;
		clientSocket = _clientSocket;
		stream = clientSocket.GetStream();
		stream.ReadTimeout = 60000;
		clientSocket.ReceiveTimeout = 60000;
		
		ReadThread = new Thread(new ThreadStart(ReadProcessWorker));
        ReadThread.IsBackground = true;
        ReadThread.Start();
	}
	
	public bool SocketIsConnected()
	{
		if (fail==true)
		{
			return false;
		}
		if (ReadThread.IsAlive == false ) return false;
		if (clientSocket.Connected == false ) return false;
		
		return true;
	}
	
	~ServerClientClass()
    {
		Debug.Log ("desctructor superclient");
        mutexObj.Dispose();
		clientSocket.Close();
		fail=true;
		ReadThread.Abort();
    }
	
	//возвращаем все что прочитали
	public List<byte> GetReadedBytes()
	{
		if ((fail==true)||(ReadedBytes.Count<6))
		{
			return new List<byte> ();
		}
		
		mutexObj.WaitOne();
		List<byte> ReadedBytesDeepCopy = new List<byte>();
		foreach (byte b in ReadedBytes)
		{
			ReadedBytesDeepCopy.Add(b);
		}
		ReadedBytes.Clear();
		mutexObj.ReleaseMutex();
		return ReadedBytesDeepCopy;
	}
	
	
	//отдельный поток, всегда читает что там пришло
	private void ReadProcessWorker()
    {
		Byte[] bytes = new Byte[4096];
		int length;

		while ((clientSocket.Connected==true)&&(fail==false))
		{
			// Read incomming stream into byte arrary.  
			try
            {		
				//using (clientSocket)
				//using (stream)
				{
					while ((length = stream.Read(bytes, 0, bytes.Length)) != 0)
					{
						mutexObj.WaitOne();
						for (int i=0;i<=length; i++)
						{
							ReadedBytes.Add(bytes[i]);
						}
						mutexObj.ReleaseMutex();
					}
				}
			}
			catch (Exception ex)
			{
				mutexObj.Dispose();
				clientSocket.Close();
				Debug.Log("exception  : " + ex);
				fail=true;
				ReadThread.Abort(); 
                return;
			}
		
			//
		}
		
		fail=true;
		ReadThread.Abort(); 
        //  
    }
	//
	

}



