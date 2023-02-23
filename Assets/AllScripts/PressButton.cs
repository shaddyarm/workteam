using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class PressButton : MonoBehaviour 
{
	float startX;
	float startY;
	float startZ;
	
	public GameObject BUTT;
	public float DX;
	public float DY;
	public float DZ;
	
	float time;
		
	// Use this for initialization
	void Start () 
	{
		startX = BUTT.transform.localPosition.x;
		startY = BUTT.transform.localPosition.y;
		startZ = BUTT.transform.localPosition.z;
	}
	
	// Update is called once per frame
	void Update () 
	{
		if (time==0) return;
		if (time>1f) 
		{
			time=0;
			return;
		}
		
		time+=Time.deltaTime*2f;
		
		BUTT.transform.localPosition = new Vector3(startX + DX * (1f-time), startY + DY * (1f-time), startZ + DZ * (1f-time));
	}
		
	
	public void PRESS()
	{
		time=0.001f;
	}
}
