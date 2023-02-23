/***************************************************************************
Scenario_step_script.cs  - редактор/пролигрыватель сценария 
-------------------
begin                : 27 мая 2020
copyright            : (C) 2020 by Гаммер Максим Дмитриевич (maximum2000)
email                : MaxGammer@gmail.com
site				 : lcontent.ru 
org					 : ИП Гаммер Максим Дмитриевич
***************************************************************************/

//https://github.com/cfoulston/Unity-Reorderable-List

using UnityEngine;
using UnityEngine.UI;
using System.Collections;
using System.Collections.Generic;
using UnityEngine.Events;

//звук

public class Scenario_step_script : MonoBehaviour 
{
	//ссылка на сценарий, там все элементы и текст и меню и все остальное
	//там же метод Next, когда сценарий закончился...
	private ScenarioEditor editor = null;
	
	//
	public UnityEvent m_MyEvent;
	
	//настройка, привязываем обработчики
	public void Setup(ScenarioEditor _editor)
	{
		editor = _editor;
		m_MyEvent.Invoke();
		//
		OK();
	}
	
	
	//когда нажали на ОК...
	public void OK()
	{
		//this.gameObject.SetActive(false);
		//посылаем команду на следующий шаг
		editor.StepFinish();
	}
	
}

