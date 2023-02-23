/***************************************************************************
Scenario_value.cs  - редактор/пролигрыватель сценария 
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

[System.Serializable]
public class StringEvent : UnityEvent<string> { }

public class Scenario_value : MonoBehaviour 
{
	[Header("Значение")]
	//значение, которое анализируется в сценарии
	public float my_value;
	//комментарий к ошибке, например "Вращение штурвала.... "Задвижки ЗКД14""
	public string comment;
	//флаг, если true то при изменении значения, не происходит вызов ошибки, если false - происходит
	public bool ChangeAllow;
	public StringEvent ErrorEvent;
	public UnityEvent ChangeEvent;

	void Start () 
	{
		//при запуске редактора или шага, все переменные сцены типа Scenario_value должны быть переподписаны на обработчик конкретного сценария
		//editor.Answer1Button.onClick.AddListener(delegate { CorrectAnswer(); });
		//editor.Answer2Button.onClick.AddListener(delegate { IncorrectAnswer1(); });
	}

	//change
	public void SetValue(float newvalue)
	{
		my_value = newvalue;
		if (ChangeAllow==true)
		{
			ChangeEvent.Invoke();
		}
		else
		{
			//ChangeEvent.Invoke();
			ErrorEvent.Invoke(comment);
		}
	}

	

	
}

