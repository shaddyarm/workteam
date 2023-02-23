using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class Stantion610 : MonoBehaviour 
{
	public float z = 1.75f;

	public float minimum = 0;
	public float maximum = 20f;
	
	public float dZ = 0.3f;
	
	void Start () 
	{
	
	}
	
	public void UP()
	{
		z+=dZ;
		if (z>maximum) z = maximum;
		transform.localPosition = new Vector3(transform.localPosition.x, z, transform.localPosition.z);
	}
	
	public void DOWN()
	{
		z-=dZ;
		if (z<minimum) z = minimum;
		transform.localPosition = new Vector3(transform.localPosition.x, z, transform.localPosition.z);
	}
	
	// Update is called once per frame
	void Update () 
	{
		
		
	}
}
