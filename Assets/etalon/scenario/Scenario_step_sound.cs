/***************************************************************************
Scenario_step_sound.cs  - редактор/пролигрыватель сценария 
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

public class Scenario_step_sound : MonoBehaviour 
{
	//ссылка на сценарий, там все элементы и текст и меню и все остальное
	//там же метод Next, когда сценарий закончился...
	private ScenarioEditor editor = null;
	
	//тип 
	public StepEnum Ждем_Окончания;
	public enum StepEnum {
		Да,	
		Нет
	}
	
	public AudioSource source = null;
	Coroutine lastRoutine = null;
	
	/////////////////////////////////////////////////
	
	
	//настройка, привязываем обработчики
	public void Setup(ScenarioEditor _editor)
	{
		editor = _editor;
		source.Play();
		
		if (Ждем_Окончания==StepEnum.Да)
		{
			//настраиваем таймер, по завершению которого вызовется ОК
			float clipLength = source.clip.length;
			lastRoutine = StartCoroutine(WaitMethod(clipLength));
		}
		else
		{
			OK();
		}
	}
	
	
	private IEnumerator WaitMethod(float clipLength)
    {
        yield return new WaitForSeconds(clipLength);
        OK();
    }
	

	//когда нажали на ОК...
	public void OK()
	{
		if (Ждем_Окончания==StepEnum.Да)
		{
			source.Pause();
			if (lastRoutine!=null)	StopCoroutine(lastRoutine);
		}
		//никакие аргументы не передаем в Editor, типа правильно/неправильно
		this.gameObject.SetActive(false);

		//
		System.DateTime theTime = System.DateTime.Now;
		string datetime = theTime.ToString("yyyy-MM-dd\\ HH:mm:ss\\ ");

		ReportStorageStepClass temp = new ReportStorageStepClass();
		temp.guid_id = System.Guid.NewGuid().ToString();
		temp.definition_description = "ВОспроизведен звук " + source.gameObject.name;
		temp.datatime_real = datetime;
		temp.datatime_simulation = datetime;
		temp.type = "Scenario_step_text";
		temp.completed = 1f;
		temp.passed = 1f;
		temp.categoty = "";
		editor.ReportStorage.ReportStorageStepsList.Add(temp);
		//

		//посылаем команду на следующий шаг
		editor.StepFinish();
	}
	
}

