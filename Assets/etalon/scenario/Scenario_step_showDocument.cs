/***************************************************************************
Scenario_step_showDocument.cs  - редактор/пролигрыватель сценария 
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

public class Scenario_step_showDocument : MonoBehaviour 
{
	//ссылка на сценарий, там все элементы и текст и меню и все остальное
	//там же метод Next, когда сценарий закончился...
	private ScenarioEditor editor = null;
	
	//все страницы
	public List <Sprite> pages;
	
	public bool ЖдемЗакрытия = true;
	
	//настройка, привязываем обработчики
	public void Setup(ScenarioEditor _editor)
	{
		editor = _editor;
		//
		editor.DocumentViewer.SetActive(true);
		editor.DocumentViewerCloseButton.onClick.AddListener(delegate { OK(); });
		editor.DocClass.Setup(ref pages);
		
		if (ЖдемЗакрытия==true)
		{
			editor.DocumentViewerCloseButton.gameObject.SetActive(true);
		}
		else
		{
			editor.DocumentViewerCloseButton.gameObject.SetActive(false);
			OK();
		}
	}
	
	
	//когда нажали на ОК...
	public void OK()
	{
		//отвязываем обработчики
		editor.DocumentViewerCloseButton.onClick.RemoveListener(delegate { OK(); });
		editor.DocumentViewerCloseButton.onClick.RemoveAllListeners();
		//
		this.gameObject.SetActive(false);

		//
		System.DateTime theTime = System.DateTime.Now;
		string datetime = theTime.ToString("yyyy-MM-dd\\ HH:mm:ss\\ ");

		ReportStorageStepClass temp = new ReportStorageStepClass();
		temp.guid_id = System.Guid.NewGuid().ToString();
		temp.definition_description = "Пользователь увидел документы";
		temp.datatime_real = datetime;
		temp.datatime_simulation = datetime;
		temp.type = "Scenario_step_showDocument";
		temp.completed = 1f;
		temp.passed = 1f;
		temp.categoty = "";
		editor.ReportStorage.ReportStorageStepsList.Add(temp);
		//


		//посылаем команду на следующий шаг
		editor.StepFinish();
	}
	
}

