/***************************************************************************
Scenario_step_text.cs  - редактор/пролигрыватель сценария 
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

public class Scenario_step_text : MonoBehaviour 
{
	//ссылка на сценарий, там все элементы и текст и меню и все остальное
	//к кнопке мы привязываемся
	//там же метод Next, когда сценарий закончился...
	private ScenarioEditor editor = null;
	
	//тип 
	public TextStepEnum ПоказатьТекст;
	public enum TextStepEnum {
		Сверху,	
		Сверху_ОК
	}
	
	//убирать при переходе на следующий шаг сценария
	public bool HideOnNextStep = false;
	
	//Собственно текст
	public string Message = "";
	
	bool WidthSound=false;
	
	public AudioClip clip = null;
	Coroutine lastRoutine = null;
	
	[Header("ID перевода")]
	public string translateID = "";
	
	
	/////////////////////////////////////////////////
	public void LoadAndUpdateTextsFromFile(ref string jsonString)
	{
		if (translateID!="")
		{
			//ПричинаВыполнения
			//ПоследствиеВыполнения
			//ПричинаНЕвыполнения
			//ПоследствиеНЕвыполнения
			SimpleJSON.JSONNode data = SimpleJSON.JSON.Parse(jsonString);
			foreach(SimpleJSON.JSONNode record in data[translateID])
			{
				Message = record["Сообщение"].Value;
			}
		}
	}
	/////////////////////////////////////////////////
	
	
	//настройка, привязываем обработчики
	public void Setup(ScenarioEditor _editor)
	{
		editor = _editor;
		
		editor.TopMenu.SetActive(true);
		editor.TopMenuText.text = Message;
		editor.TopMenuButton.gameObject.SetActive(false);
		
		if (clip!=null) 
		{
			WidthSound=true;
		}
		else
		{
			WidthSound=false;
		}
		
		if (WidthSound==true)
		{
			
			editor.AudioSourceForMessage.clip = clip;
			editor.AudioSourceForMessage.Play();
		}
		
		if (ПоказатьТекст==TextStepEnum.Сверху)
		{
			//настраиваем таймер, по завершению которого вызовется ОК
			if (WidthSound==true)
			{
				float clipLength = clip.length;
				lastRoutine = StartCoroutine(WaitMethod(clipLength));
			}
		}
		else if (ПоказатьТекст == TextStepEnum.Сверху_ОК)
		{
			//показываем меню с клавишей ОК, привязываем обработчик нажатия на ОК к методу ОК
			editor.TopMenuButton.gameObject.SetActive(true);
			editor.TopMenuButton.onClick.AddListener(delegate { OK(); });
		}
		
		
		//если просто текст, то сразу переходим на следующий
		if ((WidthSound==false)&&(ПоказатьТекст==TextStepEnum.Сверху))
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
		//отвязываем обработчики
		editor.TopMenuButton.onClick.RemoveListener(delegate { OK(); });
		editor.TopMenuButton.onClick.RemoveAllListeners();
		
		
		//скрываем если нужно
		if (HideOnNextStep==true)
		{
			editor.TopMenu.SetActive(false);
		}
		
		if (WidthSound==true)
		{
			editor.AudioSourceForMessage.Pause();
			if (lastRoutine!=null)	StopCoroutine(lastRoutine);
		}
		
		//никакие аргументы не передаем в Editor, типа правильно/неправильно
		this.gameObject.SetActive(false);


		//
		System.DateTime theTime = System.DateTime.Now;
		string datetime = theTime.ToString("yyyy-MM-dd\\ HH:mm:ss\\ ");

		ReportStorageStepClass temp = new ReportStorageStepClass();
		temp.guid_id = System.Guid.NewGuid().ToString();
		temp.definition_description = "Показан текст " + Message;
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

