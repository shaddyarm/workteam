using UnityEngine;
using System.Runtime.InteropServices;

public class ToScormScript : MonoBehaviour 
{
	[DllImport("__Internal")]
    public static extern void StartLMS();
	
	[DllImport("__Internal")]
    public static extern void SendData(string key, string value);

	[DllImport("__Internal")]
    public static extern void FinishLMS();
}