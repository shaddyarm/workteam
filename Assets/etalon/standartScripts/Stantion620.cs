using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class Stantion620 : MonoBehaviour 
{
	bool Up = true;
	
	public GameObject Head;
	
	public float minimum = 0;
	public float maximum = 20f;
	
	void Start () 
	{

	}
	
	public void UP()
	{
		if (Up==true) return;
		Up=true;
		Head.transform.localPosition = new Vector3(0, maximum,0);
	}
	
	public void DOWN()
	{
		if (Up==false) return;
		Up=false;
		Head.transform.localPosition = new Vector3(0, minimum,0);
	}
	
}
