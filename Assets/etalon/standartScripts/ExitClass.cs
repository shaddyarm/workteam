using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

#if UNITY_WEBGL
using System.Runtime.InteropServices;
#endif

public class ExitClass : MonoBehaviour 
{
#if UNITY_WEBGL
	[DllImport("__Internal")]
	private static extern void SimulatorExit();
#endif

	public void EXIT()
	{
		UnityEngine.Debug.Log("goodbuy");
		//Выход
		//if (!Application.isEditor) System.Diagnostics.Process.GetCurrentProcess().Kill();
		//Application.Quit();

#if (UNITY_EDITOR)
			UnityEditor.EditorApplication.isPlaying = false;
#elif (UNITY_STANDALONE)
			Application.Quit();
#elif (UNITY_WEBGL)
			//Application.OpenURL("about:blank");
			//Application.Quit();
			//Функция сообщает в JS что тренажер загрузился и JS может вызывать методы
			SimulatorExit();
#endif
	}
}
	

