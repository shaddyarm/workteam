/***************************************************************************
Scenario_step_key.cs  - редактор/пролигрыватель сценария 
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

public class Scenario_step_key : MonoBehaviour 
{
	//ссылка на сценарий, там все элементы и текст и меню и все остальное
	//там же метод Next, когда сценарий закончился...
	private ScenarioEditor editor = null;
	bool initialized = false;
	
	//настройка, привязываем обработчики
	public void Setup(ScenarioEditor _editor)
	{
		editor = _editor;
		initialized=true;
	}
	
	void Update()
    {
		if (initialized==false) return;
        if (Input.GetKeyDown("space"))
        {
            //никакие аргументы не передаем в Editor, типа правильно/неправильно
			this.gameObject.SetActive(false);
			//посылаем команду на следующий шаг
			editor.StepFinish();
			initialized=false;
        }
    }

}

