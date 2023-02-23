using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using System.IO;
using System;

public class Observer3Class : MonoBehaviour 
{
	
	
	public ToMainMenuClass breaker;
	
	public string НАЗВАНИЕ;
	
	float StartTime;
	
	public string mode;
	public List <Element3Class> elements;
	
	public bool ЭКЗАМЕН;
	public Toggle экзамен;
	
	public GameObject textPanel;
	public Text text;
	
	int Индекс;
	
	int balls;
	
	
	float timeElapsed;
	
	public void Reset()
	{
		Start () ;
	}
	
	public void SetMode(string _mode)
	{
		timeElapsed=0;
		
		mode=_mode;
		balls=0;
		
		if (mode=="ДЕЙСТВИЯ")
		{
			textPanel.SetActive(true);
			text.text = "";
			ЭКЗАМЕН = экзамен.isOn;
			
			if (ЭКЗАМЕН==true)
			{
				breaker.isEkzamen=true;
				экзамен.interactable  =false;
				экзамен.isOn =false;
			}
							
			Индекс=0;
			 CorrectAnswer();
		}
		
	}
	
	void Update () 
	{
		if (mode=="") return;
		timeElapsed+=Time.deltaTime;
	}
	
	public void CorrectAnswer()
	{
		if (mode=="") return;
		
		//показываем точки, если они есть
		//и ждем когда пользователь все понажимает
		elements[Индекс].show();
		//если нажимать нечего, не ждем
		if (elements[Индекс].subElements.Count ==0)
		{
			Press(elements[Индекс].ID);
		}
		
	}
	
		
	void Start () 
	{
		textPanel.SetActive(false);
		Индекс=0;
		mode="";
		foreach (Element3Class child in elements)
		{
			child.hide();
			child.Reset();
		}
		
	}
	
	
	//нажали на элемент с ID, нужно решить что делать с ним 
	public void Press (string ID)
	{
		balls++;
		text.text = "Баллы: " + balls.ToString("N0") + " из " + elements.Count.ToString("N0");
		
		if (Индекс==(elements.Count-1))
		{
			text.text = "Задание завершено. Баллы: " + balls.ToString("N0") + " из " + elements.Count.ToString("N0");
			if (ЭКЗАМЕН) 
			{
				breaker.isEkzamen=false;
				WriteReport();
			}
		}
		else
		{
			Индекс++;
			
		}
	}
	
	void WriteReport()
	{
		try
		{
			ToScormScript.StartLMS();
			ToScormScript.SendData ("cmi.score.min", "0");
			ToScormScript.SendData ("cmi.score.max", elements.Count.ToString("N0"));
			ToScormScript.SendData ("cmi.score.raw", balls.ToString("N0"));
			ToScormScript.SendData ("cmi.progress_measure", "0");
			ToScormScript.SendData ("cmi.score.min", "1");
			ToScormScript.SendData ("cmi.success_status", "passed");
			ToScormScript.SendData ("cmi.completion_status", "completed");
			ToScormScript.SendData ("cmi.interactions.0.id", "1");
			ToScormScript.SendData ("cmi.interactions.0.description", НАЗВАНИЕ);
			ToScormScript.SendData ("cmi.session_time", timeElapsed.ToString("N0"));
			ToScormScript.FinishLMS();
		}
		catch (Exception ex)
		{
			Debug.Log ("ToScormScript false-" + ex);
		}
		
		
		//
	}
		
}
