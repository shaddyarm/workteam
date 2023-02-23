/***************************************************************************
Scenario_step_waiter.cs  - редактор/пролигрыватель сценария 
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


//звук

public class Scenario_step_waiter : MonoBehaviour 
{
	//ссылка на сценарий, там все элементы и текст и меню и все остальное
	//там же метод Next, когда сценарий закончился...
	private ScenarioEditor editor = null;
	
	public float delay=1000;
	Coroutine lastRoutine = null;
	
	/////////////////////////////////////////////////
	
	
	//настройка, привязываем обработчики
	public void Setup(ScenarioEditor _editor)
	{
		editor = _editor;
		lastRoutine = StartCoroutine(WaitMethod(delay));
	}
	
	
	private IEnumerator WaitMethod(float delay_)
    {
        yield return new WaitForSeconds(delay_);
        OK();
    }
	

	//когда нажали на ОК...
	public void OK()
	{
		if (lastRoutine!=null)	StopCoroutine(lastRoutine);
		//никакие аргументы не передаем в Editor, типа правильно/неправильно
		this.gameObject.SetActive(false);
		//посылаем команду на следующий шаг
		editor.StepFinish();
	}
	
}

