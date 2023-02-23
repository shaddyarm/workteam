/***************************************************************************
ArrowClass.cs  - Стрелка
-------------------
begin                : 10 октября 2019
copyright            : (C) 2019 by Гаммер Максим Дмитриевич (maximum2000)
email                : MaxGammer@gmail.com
site				 : lcontent.ru 
org					 : ИП Гаммер Максим Дмитриевич
***************************************************************************/

using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.Events;
using UnityEngine.EventSystems;


public class ArrowClass : MonoBehaviour 
{
	public GameObject my;
	public bool need_push=false;
	
	public UnityEvent m_MyEvent=null;
	
	Renderer[] rs;
	float time;
	
	
	void Start () 
	{
		rs = my.GetComponentsInChildren<Renderer>();
		time=0;
		
		if (m_MyEvent == null) m_MyEvent = new UnityEvent();
	}
	
		
	// Update is called once per frame
	void Update () 
	{
		time +=  Time.deltaTime*4f;
		if (time>180f) time=0;;
		//SetColorAlpha (Mathf.Sin(time));
		my.transform.localRotation = Quaternion.Euler(0,  time*10f , 0);
	}
	
	void SetColorRed ()
	{
		Color col;
		col.a = 1f;
		col.r = 1f;
		col.g = 0;
		col.b = 0;
		rs[0].materials[0].color = col;

	}
	
	void SetColorGreen ()
	{
		Color col;
		col.a = 1f;
		col.r = 0;
		col.g = 1f;
		col.b = 0;
		rs[0].materials[0].color = col;
	}
	
	//нажатие мышкой....., VR, Joy
	void OnMouseDown()
    {
		SetColorGreen ();
		Press();
    }
	
	void Press()
	{
		if (need_push==false) return;
		
		m_MyEvent.Invoke();
	}
	
	void OnMouseEnter()
    {
       SetColorRed();
    }

    void OnMouseExit()
    {
	   SetColorGreen ();
    }
	
}
