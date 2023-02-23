using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class SuperRazbor : MonoBehaviour 
{
	
	
	
	public List<GameObject> a1;
	
	List<bool> b1;
	
	
	//public float position;
	
	
	public void Z(int num)
	{
		b1[num] = !b1[num];
		
		if (b1[num]==false) 
		{
			//a1[num].transform.localPosition = new Vector3(-position * 1f, a1[num].transform.localPosition.y , a1[num].transform.localPosition.z);
			a1[num].SetActive(false);
		}
		else
		{
			a1[num].SetActive(true);
			//a1[num].transform.localPosition = new Vector3(0,a1[num].transform.localPosition.y, a1[num].transform.localPosition.z);
		}
		
		
		
		
	}
	
		
	// Use this for initialization
	void Start () 
	{
		//position=3.5f;
		
		b1 = new List<bool>();
		for (int i=0;i<18;i++)
		{
			b1.Add(true);
		}
	
	}
	
	// Update is called once per frame
	void Update () 
	{
		
	}
	
	
	
}