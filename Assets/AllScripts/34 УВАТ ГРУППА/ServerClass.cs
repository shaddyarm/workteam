
using UnityEngine;
using UnityEngine.UI;
using TMPro;
using System.Collections;
using System.Collections.Generic;
using System.Net;
using System.Net.Sockets;
using System.Threading;
using System;

public class ServerClass : MonoBehaviour 
{
	public List<ClientPanel> ClientPanels;
	private List<ServerClientClass> ConnectedClients = new List<ServerClientClass>();
	
	private TcpListener tcpListener;
	/// Background thread for TcpServer workload.  
    private Thread tcpListenerThread;

	//скрываем всех, показываем при подключении
	void Start ()
	{
		foreach (ClientPanel one in ClientPanels)
		{
			one.gameObject.SetActive(false);
		}
		
		
		tcpListenerThread = new Thread(new ThreadStart(ListenForIncommingRequests));
        tcpListenerThread.IsBackground = true;
        tcpListenerThread.Start();
		
		Debug.Log ("Запустил сервер");
		
		
		//запускаем PINGPONG с задержкой в 5 секунды
		StartCoroutine(PINGPONG());
	}
	
	//PINGPONG
	IEnumerator PINGPONG()
	{
		while (true)
		{
			yield return new WaitForSeconds(10);
			
			foreach (ServerClientClass client in ConnectedClients)
			{
				
				
			}
			//for
		}
	}
	//PINGPONG
	
	

	
	private void ListenForIncommingRequests()
    {
		//Диапазон 49152—65535 содержит динамически выделяемые или частные порты, которые не регистрируются IANA. 
		//Эти порты используются временными (короткоживущими) соединениями «клиент — сервер» или в определённых частных случаях.
        tcpListener = new TcpListener(IPAddress.Any, 60000);
        tcpListener.Start();
        ThreadPool.QueueUserWorkItem(this.ListenerWorker, null);
    }
	
    private void ListenerWorker(object token)
    {
        while (tcpListener != null)
        {
            TcpClient connectedTcpClient = tcpListener.AcceptTcpClient();
			Debug.Log("Сервер. Подключился новый клиент.");
			ServerClientClass newclient = new ServerClientClass(connectedTcpClient);
            ConnectedClients.Add(newclient);
        }
    }
	
	void Update()
    {
		//проверяем дисконнект клиентов
		for (int i = ConnectedClients.Count -1; i>=0; i--)
		{
			if (ConnectedClients[i].SocketIsConnected()==false)
			{
				Debug.Log ("Сервер. Клиент отключился.");
				ConnectedClients.RemoveAt(i);
			}
		}
		
		//прочитать сообщения от всех клиентов
		ReadAllChanges();
    }
	
	//прочитать сообщения от всех клиентов
	private void ReadAllChanges()
	{
		//задача - пройти весь накопленный буфер и обработать все цельные сообщения, сообщений которые содержат только часть (в конце) не должно быть в природе
		//очистить буфер от прочитанных сообщений - автоматом
		foreach (ServerClientClass client in ConnectedClients)
		{
			//алгоритм такой, начинаем с 0 индекса и идем вперед в надежде найти заголовок NEURO
			List<byte> buffer = client.GetReadedBytes();
			
			if (buffer.Count >= 4*4) 
			{
				byte[] bytes1 = { buffer[0], buffer[1], buffer[2], buffer[3] };
				int ПорядковыйНомер = BitConverter.ToInt32(bytes1, 0);
				byte[] bytes2 = { buffer[4], buffer[5], buffer[6], buffer[7] };
				int ДейсвийНайдено = BitConverter.ToInt32(bytes2, 0);
				byte[] bytes3 = { buffer[8], buffer[9], buffer[10], buffer[11] };
				int УсловийНайдено = BitConverter.ToInt32(bytes3, 0);
				byte[] bytes4 = { buffer[12], buffer[13], buffer[14], buffer[15] };
				int ОшибокДопущено = BitConverter.ToInt32(bytes4, 0);
				
				ClientPanels[ПорядковыйНомер-1].gameObject.SetActive(true);
				ClientPanels[ПорядковыйНомер-1].SetProgress(ДейсвийНайдено, УсловийНайдено, ОшибокДопущено);
			}
		}
	}
	
	
	
	
	
}
