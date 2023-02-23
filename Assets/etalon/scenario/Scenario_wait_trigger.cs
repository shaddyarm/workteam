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

public class Scenario_wait_trigger : MonoBehaviour 
{
	//ссылка на сценарий, там все элементы и текст и меню и все остальное
	//там же метод Next, когда сценарий закончился...
	private ScenarioEditor editor = null;
	
	//
	public Scenario_Trigger trigger;
	
	//настройка, привязываем обработчики
	public void Setup(ScenarioEditor _editor)
	{
		editor = _editor;
	}
	
	void Update()
	{
		if (editor==null) return;
		
		if (trigger.on == true)
		{
			this.gameObject.SetActive(false);

			//
			System.DateTime theTime = System.DateTime.Now;
			string datetime = theTime.ToString("yyyy-MM-dd\\ HH:mm:ss\\ ");

			ReportStorageStepClass temp = new ReportStorageStepClass();
			temp.guid_id = System.Guid.NewGuid().ToString();
			temp.definition_description = "Событие "+ trigger.gameObject.name;
			temp.datatime_real = datetime;
			temp.datatime_simulation = datetime;
			temp.type = "Scenario_wait_trigger";
			temp.completed = 1f;
			temp.passed = 1f;
			temp.categoty = "";
			editor.ReportStorage.ReportStorageStepsList.Add(temp);
			//


			//посылаем команду на следующий шаг
			editor.StepFinish();
			//
			editor=null;
		}
	}
}

