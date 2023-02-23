using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.Events;

public class ElementNewClass : MonoBehaviour 
{
	public GameObject my;
	public UnityEvent myEvent0;

	Renderer[] rs;
	float time;
	
	
	void Start () 
	{
		rs = my.GetComponentsInChildren<Renderer>();
		time=0;
	}
	
	// Update is called once per frame
	void Update () 
	{
		time +=  Time.deltaTime*4f;
		if (time>180f) time=0;;
		SetColorAlpha (Mathf.Sin(time));
	}
	
	void SetColorAlpha (float alpha)
	{
		if (alpha<0) alpha=-alpha;
		alpha=alpha/2f;
		
			Color col;
			col.a = alpha;
			col.r = rs[0].materials[0].color.r;
			col.g = rs[0].materials[0].color.g;
			col.b = rs[0].materials[0].color.b;
			rs[0].materials[0].color = col;

	}
	
	
	//нажатие
	void OnMouseDown()
    {
		myEvent0.Invoke();
    }
	
}
