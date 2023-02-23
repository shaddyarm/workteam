
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SpecialValve1 : MonoBehaviour 
{

	public ValveClass0 Shturval;
	public GameObject r1;
	public GameObject r2;
	
	
	// Update is called once per frame
	void Update () 
	{
		float angle = Shturval.currentShift / (Shturval.maxA - Shturval.minA);
		
		r1.transform.localRotation = Quaternion.Euler(0 , angle * 180f, 0);
		r2.transform.localRotation = Quaternion.Euler(0 , angle * 180f, 0);
	}
	
	
	
	
	

	
	
}

