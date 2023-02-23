using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class deactivator : MonoBehaviour {

	// Use this for initialization
	void Start () 
	{
			PlayerPrefs.DeleteAll();
			#if (UNITY_EDITOR)
				UnityEditor.EditorApplication.isPlaying = false;
			#elif (UNITY_STANDALONE) 
				Application.Quit();
			#elif (UNITY_WEBGL)
				Application.OpenURL("about:blank");
			#endif
			Debug.Log("EXOT OK");
	}
	
	// Update is called once per frame
	void Update () {
		
	}
}
