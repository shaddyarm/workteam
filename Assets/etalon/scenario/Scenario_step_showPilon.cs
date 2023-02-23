/***************************************************************************
Scenario_step_showPilon.cs  - редактор/пролигрыватель сценария 
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

public class Scenario_step_showPilon : MonoBehaviour 
{
	//ссылка на сценарий, там все элементы и текст и меню и все остальное
	//к кнопке мы привязываемся
	//там же метод Next, когда сценарий закончился...
	private ScenarioEditor editor = null;
	
	public GameObject target;
	public float distance=7;
	/////////////////////////////////////////////////
	
	
	//настройка, привязываем обработчики
	public void Setup(ScenarioEditor _editor)
	{
		editor = _editor;
		//
		editor.Pilon.gameObject.SetActive(true);
		editor.Pilon.distance = distance;
		//
		
		editor.Напоминалка.SetActive(true);
		editor.Напоминалка_текст.text = "Необходимо подойти в указанную стрелкой позицию.";
	
		
		editor.Pilon.transform.localPosition = target.transform.localPosition;
		
		//привязываем обработчик нажатия на ОК к методу ОК
		editor.Pilon.m_MyEvent.AddListener(delegate { OK(); });
	}
	
	
	
	

	//когда нажали на ОК...
	public void OK()
	{
		editor.Напоминалка.SetActive(false);
		//отвязываем обработчики
		editor.Pilon.m_MyEvent.RemoveListener(delegate { OK(); });
		editor.Pilon.m_MyEvent.RemoveAllListeners();
	
		editor.Pilon.gameObject.SetActive(false);
		
		this.gameObject.SetActive(false);

		//
		System.DateTime theTime = System.DateTime.Now;
		string datetime = theTime.ToString("yyyy-MM-dd\\ HH:mm:ss\\ ");

		ReportStorageStepClass temp = new ReportStorageStepClass();
		temp.guid_id = System.Guid.NewGuid().ToString();
		temp.definition_description = "Вользователь перешел в нужное место " + target.name;
		temp.datatime_real = datetime;
		temp.datatime_simulation = datetime;
		temp.type = "Scenario_step_showPilon";
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
		private void scenario_Pilon(bool pilonInCoordinates, float pilon_x, float pilon_y, float pilon_z, string path)
		{
			if (pilonInCoordinates==true)
			{
				Pilon.transform.localPosition = new Vector3(pilon_x, pilon_y, pilon_z);
			}
			else
			{
				GameObject current = GameObject.Find(path);
				if (current==null)
				{
					Debug.Log ("Такого элемента нет.");
					return;
				}
				Pilon.transform.localPosition = current.transform.localPosition;
			}
			//пилон не должен удаляться со сцены!
			Pilon.SetActive(true);
		}
		*/

