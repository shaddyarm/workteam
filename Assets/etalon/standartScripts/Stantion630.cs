using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class Stantion630 : MonoBehaviour 
{
	public GameObject Head;
	public float step = 0.1f;


	public int min = -12;
	public int max = 10;
	int current = 0;

	void Start () 
	{
	}
	
	public void UP()
	{
		if (current > max) return;
		current++;
		Head.transform.position +=  new Vector3(0, step, 0);
	}
	
	public void DOWN()
	{
		if (current < min) return;
		current--;
		Head.transform.position += new Vector3(0, -step, 0);
	}
	
}
