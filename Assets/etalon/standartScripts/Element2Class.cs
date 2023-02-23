using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class Element2Class : MonoBehaviour 
{
	public string ID;
	public string QUESTION;
	public string ANSWER1; //correct
	public string ANSWER2; //correct
	public string ANSWER3; //correct
	public string ANSWER4; //correct
	
	public bool nextOK;
	
	public Observer2Class myobserver;
	
	
	public List <Element2SubClass> subElements;
	
	bool finish;
	
	
	public void Reset()
	{
		foreach (Element2SubClass child in subElements)
		{
			child.Reset();
		}
		finish=false;
		nextOK=true;
	}
	
	
	
	public void hide()
	{
		foreach (Element2SubClass child in subElements)
		{
			child.hide();
		}
	}
	
	public void show()
	{
		foreach (Element2SubClass child in subElements)
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
		foreach (Element2SubClass child in subElements)
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
			
		}
	}
	
}
