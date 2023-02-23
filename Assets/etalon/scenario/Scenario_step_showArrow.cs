/***************************************************************************
Scenario_step_showArrow.cs  - редактор/пролигрыватель сценария 
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


//текст сверху, текст с кнопкой и т.д.

public class Scenario_step_showArrow : MonoBehaviour 
{
	//ссылка на сценарий, там все элементы и текст и меню и все остальное
	//к кнопке мы привязываемся
	//там же метод Next, когда сценарий закончился...
	private ScenarioEditor editor = null;
	
	//тип 
	public StepEnum Поведение;
	public enum StepEnum 
	{
		Показать_и_ждать_нажатия,	
		Показать,
		Скрыть
	}
	
	public GameObject target;
	
	/////////////////////////////////////////////////
	
	
	//настройка, привязываем обработчики
	public void Setup(ScenarioEditor _editor)
	{
		editor = _editor;
		
		
		
		editor.Arrow.gameObject.SetActive(true);
		
		editor.Arrow.transform.localPosition = target.transform.localPosition;
		
		if (Поведение==StepEnum.Показать_и_ждать_нажатия)
		{
			//привязываем обработчик нажатия на ОК к методу ОК
			editor.Arrow.m_MyEvent.AddListener(delegate { OK(); });
			editor.Arrow.need_push = true;
			
			editor.Напоминалка.SetActive(true);
			editor.Напоминалка_текст.text = "Необходимо нажать на стрелку";
		}
		else 
		{
			editor.Arrow.need_push = false;
		}
		
		
		//если просто скрыть, то сразу переходим на следующий
		if ((Поведение==StepEnum.Скрыть))
		{
			OK();
		}
		
		//если просто показать, то сразу переходим на следующий
		if ((Поведение==StepEnum.Показать))
		{
			OK();
		}
	}
	
	
	
	

	//когда нажали на ОК...
	public void OK()
	{
		if (Поведение==StepEnum.Показать_и_ждать_нажатия)
		{
			editor.Напоминалка.SetActive(false);
			//отвязываем обработчики
			editor.Arrow.m_MyEvent.RemoveListener(delegate { OK(); });
			editor.Arrow.m_MyEvent.RemoveAllListeners();
		}
		
		//скрываем если нужно
		if (Поведение==StepEnum.Показать_и_ждать_нажатия)
		{
			editor.Arrow.gameObject.SetActive(false);
		}
		if (Поведение==StepEnum.Скрыть)
		{
			editor.Arrow.gameObject.SetActive(false);
		}
		
		this.gameObject.SetActive(false);


		//
		System.DateTime theTime = System.DateTime.Now;
		string datetime = theTime.ToString("yyyy-MM-dd\\ HH:mm:ss\\ ");

		ReportStorageStepClass temp = new ReportStorageStepClass();
		temp.guid_id = System.Guid.NewGuid().ToString();
		temp.definition_description = "Пользователь указал необходимую позицию " + target.name;
		temp.datatime_real = datetime;
		temp.datatime_simulation = datetime;
		temp.type = "Scenario_step_showArrow";
		temp.completed = 1f;
		temp.passed = 1f;
		temp.categoty = "";
		editor.ReportStorage.ReportStorageStepsList.Add(temp);
		//

		//посылаем команду на следующий шаг
		editor.StepFinish();
	}
	
}

/*
		private void scenario_ShowArrow(bool need_push, string object_from_path, string object_to_path)
		{
			Arrow.GetComponent <ArrowClass>().need_push = need_push;
			
			GameObject current = GameObject.Find(object_from_path);
			if (current==null)
			{
				Debug.Log ("Такого элемента нет.");
				return;
			}
			Arrow.transform.localPosition = current.transform.localPosition;
			
			if (object_to_path != "")
			{
				GameObject currentTo = GameObject.Find(object_to_path);
				if (currentTo==null)
				{
					Debug.Log ("Такого элемента нет.");
					return;
				}
				Arrow.transform.LookAt(currentTo.transform);
			}
			else
			{
				Arrow.transform.LookAt(Vector3.zero);
			}
			Arrow.SetActive(true);
		}
		*/

