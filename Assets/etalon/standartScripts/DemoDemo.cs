using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DemoDemo : MonoBehaviour {

	// Use this for initialization
	void Start () 
	{
		Debug.Log("demodemo");
		   StartCoroutine("Quit");
	}
	
	IEnumerator Quit () 
	{
		
		yield return new WaitForSeconds(600);
		Debug.Log("exit");
		Application.Quit ();
	}
}
