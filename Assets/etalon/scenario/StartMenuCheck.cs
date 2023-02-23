/***************************************************************************
Scenario_step_script.cs  - редактор/пролигрыватель сценария 
-------------------
begin                : 27 мая 2020
copyright            : (C) 2020 by Гаммер Максим Дмитриевич (maximum2000)
email                : MaxGammer@gmail.com
site				 : lcontent.ru 
org					 : ИП Гаммер Максим Дмитриевич
***************************************************************************/

using UnityEngine;
using UnityEngine.UI;
using System.Collections;
using System.Collections.Generic;
using UnityEngine.Events;

public class StartMenuCheck : MonoBehaviour 
{
	public InputField FIO;
	public InputField TYPE;
	public InputField ORG;
	
	public Button ButtonEdu;
	public Button ButtonExam;
	
	public Play player;
	
	void Start()
	{
		//hold player
		player.mode=-1;

		ButtonEdu.interactable = true;
		ButtonExam.interactable = true;
	}
	
	public void Check()
	{
		/*
		if ((FIO.text!="")&&(ORG.text!="")&&(TYPE.text !=""))
		{
			ButtonEdu.interactable = true;
			ButtonExam.interactable = true;
		}
		else
		{
			ButtonEdu.interactable =false;
			ButtonExam.interactable =false;
		}
		*/
	}
	
	public void UnHoldPlayer()
	{
		player.mode=0;
	}
}

