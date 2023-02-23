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

public class Scenario_wait_value : MonoBehaviour 
{
	//ссылка на сценарий, там все элементы и текст и меню и все остальное
	//там же метод Next, когда сценарий закончился...
	private ScenarioEditor editor = null;
	
	[Header("диапазон [A..B]")]
	public float A;
	public float B;

	[Header("что написать в отчет")]
	public string ToReportString="";

	//
	public Scenario_value _value;
	
	
	private float  _time;
	private bool trigger;
	private bool initialized=false;
	
	//настройка, привязываем обработчики
	public void Setup(ScenarioEditor _editor)
	{
		editor = _editor;
		initialized=true;
		
		_time = Time.time;
		trigger=false;
		
		//выбираем все объекты типа 
		var found_Scenario_values = FindObjectsOfType<Scenario_value>();
		foreach (Scenario_value myValue in found_Scenario_values)
		{
			myValue.ChangeAllow=false;
			
			myValue.ErrorEvent.RemoveListener(InCorrectChange);
			myValue.ErrorEvent.RemoveAllListeners();
			myValue.ErrorEvent.AddListener(InCorrectChange);
			
			myValue.ChangeEvent.RemoveListener(delegate { CorrectChange(); });
			myValue.ChangeEvent.RemoveAllListeners();
		}
		
		_value.ChangeAllow=true;
		_value.ChangeEvent.AddListener(delegate { CorrectChange(); });
	}
	
	//для Scenario_wait_group_values
	public void Setup2()
	{
		_time = Time.time;
		trigger=false;
	}
	
	void Update()
	{
		if (initialized==false) return;
		
		if ((_value.my_value >=A)&&(_value.my_value <=B))
		{
			this.gameObject.SetActive(false);
			//посылаем команду на следующий шаг
			if ((editor!=null)&&(trigger==false))
			{
				//отправляем в отчет
				if (ToReportString != "")
				{
					editor.AddToReport(true);
					System.DateTime theTime = System.DateTime.Now;
					string datetime = theTime.ToString("yyyy-MM-dd\\ HH:mm:ss\\ ");
					//editor.ДетальныйОтчетОДействиях.Add(datetime + ToReportString);

					ReportStorageStepClass temp = new ReportStorageStepClass();
					temp.guid_id = System.Guid.NewGuid().ToString();
					temp.definition_description = ToReportString;
					temp.datatime_real = datetime;
					temp.datatime_simulation = datetime;
					temp.type = "Scenario_wait_value";
					temp.completed = 1f;
					temp.passed = 1f;
					temp.categoty = "";

					editor.ReportStorage.ReportStorageStepsList.Add(temp);


				}
				//отправляем в отчет

				editor.StepFinish();
				
				_value.ChangeAllow=false;
				_time = Time.time;
				
				trigger=true;
			}
		}
	}
	
	public void CorrectChange()
	{
		if ((Time.time - _time) < 1f) return;
		_time = Time.time;
		
		if (editor!=null) 
		{
			editor.Напоминалка.SetActive(false);
			editor.Напоминалка_текст.text = "";
		}
	}
	
	public void InCorrectChange (string name)
	{
		if ((Time.time - _time) < 1f) return;
		_time = Time.time;
		
		if (editor!=null) 
		{
			editor.AddToReport(false);
			System.DateTime theTime = System.DateTime.Now;
			string datetime = theTime.ToString("yyyy-MM-dd\\ HH:mm:ss\\ ");
			//editor.ДетальныйОтчетОДействиях.Add(datetime + "Неверные действия с объектом :" + name + ". ");

			EffectStorageStepClass temp = new EffectStorageStepClass();
			temp.guid_id = System.Guid.NewGuid().ToString();
			temp.definition_description = "Неверные действия с объектом :" + name + ". ";
			temp.datatime_real = datetime;
			temp.datatime_simulation = datetime;
			//причина коротко, не определена/ошибка восприятия/диагностики/принятия решения/выполнение действий
			temp.cause = "";
			//причина причина полный, персонал стремился к выполнению неверной цели, персонал не увидел первых признаков ГНВП, персонал слишком долго принимал решение
			temp.cause_full = "";
			//потери общее, Выход из строя насоса Д1
			temp.losses = "";
			//потери $
			temp.losses_money = 0;
			//потери жизни и здоровья, сломана нога, 1 погиб, 1 находится в коме
			temp.losses_life_health = 0;
			//потери экология, розлив нефти в количестве 10 тонн
			temp.losses_ecology = 0;
			editor.ReportStorage.ReportStorageEffextsList.Add(temp);


			editor.Напоминалка.SetActive(true);
			editor.Напоминалка_текст.text = "Неверные действия с объектом :" + name + ". ";
		}

	}
	
	
	
	
	
}


