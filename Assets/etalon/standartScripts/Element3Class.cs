using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class Element3Class : MonoBehaviour 
{
	public string ID;
	
	
	public bool nextOK;
	
	public Observer3Class myobserver;
	
	
	public List <Element3SubClass> subElements;
	
	bool finish;
	
	
	public void Reset()
	{
		foreach (Element3SubClass child in subElements)
		{
			child.Reset();
		}
		finish=false;
		nextOK=true;
	}
	
	
	
	public void hide()
	{
		foreach (Element3SubClass child in subElements)
		{
			child.hide();
		}
	}
	
	public void show()
	{
		foreach (Element3SubClass child in subElements)
		{
			child.show();
		}

	}
	
	void Start () 
	{
		finish=false;
		nextOK=true;
	}
	
	
	void Update () 
	{
		if (finish) return;
		
		bool ok=true;
		foreach (Element3SubClass child in subElements)
		{
			if (child.GoGo==false)
			{
				ok=false;
				break;
			}
			if (child.myAudioSource != null)
			{
				if (child.myAudioSource.isPlaying==true)
				{
					ok=false;
					break;
				}
			}
		}
		if (ok)
		{
			//нажаты все элементы
			finish=true;
			myobserver.Press(ID);
			myobserver.CorrectAnswer();
			
		}
	}
	
}
