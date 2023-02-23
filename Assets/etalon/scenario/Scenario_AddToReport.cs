/***************************************************************************
Scenario_AddToReport.cs  - редактор/пролигрыватель сценария 
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

public class Scenario_AddToReport : MonoBehaviour 
{
	//ссылка на сценарий, там все элементы и текст и меню и все остальное
	//там же метод Next, когда сценарий закончился...
	private ScenarioEditor editor = null;

	[Header("что написать в отчет")]
	public string ToReportString="";

	[Header("сообщить в GUI если задано")]
	public GameObject РезультатВыполнения = null;
	public Text РезультатВыполнения_текст = null;

	//настройка, привязываем обработчики
	public void Setup(ScenarioEditor _editor)
	{
		editor = _editor;

		if (editor != null)
		{
			System.DateTime theTime = System.DateTime.Now;
			string datetime = theTime.ToString("yyyy-MM-dd\\ HH:mm:ss\\ ");
			

			ReportStorageStepClass temp = new ReportStorageStepClass();
			temp.guid_id = System.Guid.NewGuid().ToString();
			temp.definition_description = ToReportString;
			temp.datatime_real = datetime;
			temp.datatime_simulation = datetime;
			temp.type = "Scenario_AddToReport";
			temp.completed = 1f;
			temp.passed = 1f;
			temp.categoty = "";

			editor.ReportStorage.ReportStorageStepsList.Add(temp);

		}

		if ((РезультатВыполнения != null)&&(РезультатВыполнения_текст !=null))
		{
			if (ToReportString != "")
			{
				РезультатВыполнения.SetActive(true);
				РезультатВыполнения_текст.text = ToReportString;
			}
		}

		OK();
	}
	
	
		
		

	

	
	public void OK()
	{
		//никакие аргументы не передаем в Editor, типа правильно/неправильно
		this.gameObject.SetActive(false);

		//посылаем команду на следующий шаг
		editor.StepFinish();
	}





}


