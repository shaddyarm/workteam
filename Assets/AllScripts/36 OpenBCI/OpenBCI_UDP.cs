using System;
using System.Collections.Generic;
using System.Net.Sockets;
using System.Threading;
using System.Net;
using System.Text;

using UnityEngine;
using UnityEngine.UI;
using System.Globalization;






public class OpenBCI_UDP : MonoBehaviour  
{
	UdpClient udpClientBandPower; 
	Thread threadBandPower;
	
	UdpClient udpClientFFT; 
	Thread threadFFT;
	
	public float Delta;
	public float Theta;
	public float Alpha;
	public float Beta;
	public float Gamma;
	
	public List<List<float>> FFT;
	
	
	System.Object locker = new System.Object();
	
	

	
	private string getBetween(string strSource, string strStart, string strEnd)
	{
		int Start, End;
		if (strSource.Contains(strStart) && strSource.Contains(strEnd))
		{
			Start = strSource.IndexOf(strStart, 0) + strStart.Length;
			End = strSource.IndexOf(strEnd, Start);
			return strSource.Substring(Start, End - Start);
		}
		else
		{
			return "";
		}
	}
	
	public bool Get()
	{
		bool ok=false;
		lock(locker)
		{
			ok=true;
		}
		
		return ok;
		
	}
	

	private void ReceiveMessage()
	{                      
		while (true)
		{
			//IPAddress ip = IPAddress.Parse("127.0.0.1"); 
			IPEndPoint remoteIPEndPoint = new IPEndPoint(IPAddress.Any, 12345);
			byte[] content = udpClientBandPower.Receive(ref remoteIPEndPoint);
			
			
			Delta=0;
			Theta=0;
			Alpha=0;
			Beta=0;
			Gamma=0;

			if (content.Length > 0)
			{
					string message = Encoding.ASCII.GetString(content);
					//Debug.Log (message);
					if (message.Contains("bandPower")==false)
					{
						Debug.Log ("is not bandPower");
						return;
					}						
					
					//выкидиваем лишнее
					string one = getBetween (message, "[[" , "]]");
					//режем по каналам				
					string[] channelList = one.Split(new string[] { "],[" }, StringSplitOptions.None);
					//проходим каждый каналам
					int y=0;
					
					lock(locker)
					
					{
						
						foreach (string channel in channelList)
						{
							y++;
							string[] valuesOfChannelList = channel.Split(',');
							if (valuesOfChannelList.Length==5)
							{
								CultureInfo ci = (CultureInfo)CultureInfo.CurrentCulture.Clone();
								ci.NumberFormat.CurrencyDecimalSeparator = ".";
					
								Delta+=float.Parse(valuesOfChannelList[0],NumberStyles.Any,ci);
								Theta+=float.Parse(valuesOfChannelList[1],NumberStyles.Any,ci);
								Alpha+=float.Parse(valuesOfChannelList[2],NumberStyles.Any,ci);
								Beta+=float.Parse(valuesOfChannelList[3],NumberStyles.Any,ci);
								Gamma+=float.Parse(valuesOfChannelList[4],NumberStyles.Any,ci);
							}
						}
						
						if (y!=0)
						{
							Delta = Delta / y;
							Theta = Theta / y;
							Alpha = Alpha / y;
							Beta = Beta / y;
							Gamma = Gamma / y;
						}
					
					}
					
					//Debug.Log ("Delta=" + Delta + " Theta=" + Theta + " Alpha=" + Alpha + " Beta=" + Beta + " Gamma=" + Gamma);
					
					
			}
		}
	}
	
	private void ReceiveMessageFFT()
	{                      
		while (true)
		{
			//IPAddress ip = IPAddress.Parse("127.0.0.1"); 
			IPEndPoint remoteIPEndPoint = new IPEndPoint(IPAddress.Any, 12346);
			byte[] content = udpClientFFT.Receive(ref remoteIPEndPoint);
			
			if (content.Length > 0)
			{
				string message = Encoding.ASCII.GetString(content);
				//Debug.Log (message);
				if (message.Contains("fft")==false)
				{
					Debug.Log ("is not fft");
					return;
				}						
				
				//выкидиваем лишнее
				string one = getBetween (message, "[[" , "]]");
				//режем по каналам				
				string[] channelList = one.Split(new string[] { "],[" }, StringSplitOptions.None);
				//проходим каждый каналам
				int y=0;
				
				lock(locker)					
				{
					foreach (string channel in channelList)
					{
						string[] valuesOfChannelList = channel.Split(',');
						
						if (valuesOfChannelList.Length==125)
						{
							CultureInfo ci = (CultureInfo)CultureInfo.CurrentCulture.Clone();
							ci.NumberFormat.CurrencyDecimalSeparator = ".";
							
							for (int x=0;x<125;x++)
							{
								FFT[y][x] = float.Parse(valuesOfChannelList[x],NumberStyles.Any,ci);
							}
						}
						y++;
						if (y>=8) break;
					}
				}	
			}
		}
	}

	void Start () 
	{ 
		FFT = new List<List<float>>();
	
		for (int y=0; y <8; y++)
		{
			List<float> temp = new List<float>();
			for (int x=0; x <125; x++)
			{
				temp.Add(0);
			}
			FFT.Add (temp);
		}

	
		udpClientBandPower = new UdpClient(12345);
		threadBandPower = new Thread(new ThreadStart(ReceiveMessage));
		threadBandPower.IsBackground = true;
		threadBandPower.Start();
		
		
		udpClientFFT = new UdpClient(12346);
		threadFFT = new Thread(new ThreadStart(ReceiveMessageFFT));
		threadFFT.IsBackground = true;
		threadFFT.Start();
		
		
	}
	
	void OnDestroy()
    {
		threadBandPower.Abort();
		udpClientBandPower.Close();
        Debug.Log("OnDestroy1");
		threadFFT.Abort();
		udpClientFFT.Close();
        Debug.Log("OnDestroy2");
    }
}
 