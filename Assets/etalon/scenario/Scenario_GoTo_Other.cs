/***************************************************************************
Scenario_GoTo_Other.cs  - редактор/пролигрыватель сценария 
-------------------
begin                : 27 мая 2020
copyright            : (C) 2020 by Гаммер Максим Дмитриевич (maximum2000)
email                : MaxGammer@gmail.com
site				 : lcontent.ru 
org					 : ИП Гаммер Максим Дмитриевич
***************************************************************************/

using UnityEngine;
using UnityEngine.UI;
using System.Collections;
using System.Collections.Generic;


//звук

public class Scenario_GoTo_Other : MonoBehaviour 
{
	//ссылка на сценарий, там все элементы и текст и меню и все остальное
	//там же метод Next, когда сценарий закончился...
	private ScenarioEditor editor = null;
	
	public ScenarioEditor SubScenario = null;
	
	
	//тип 
	public StepEnum После_завершения;
	public enum StepEnum {
		Возврат_к_основному,	
		Ничего
	}
	
	Coroutine lastRoutine = null;
	
	/////////////////////////////////////////////////
	
	
	//настройка, привязываем обработчики
	public void Setup(ScenarioEditor _editor)
	{
		editor = _editor;
		SubScenario.ManualStart(0);
		
		if (После_завершения==StepEnum.Возврат_к_основному)
		{
			//настраиваем проверку раз в секунду, по завершению которого вызовется ОК
			lastRoutine = StartCoroutine(WaitMethod());
		}
		else
		{
			//все, мы забыли про основной сценарий.....
			editor.StepEnd();
		}
	}
	
	
	private IEnumerator WaitMethod()
    {
		//ждем пока сценарий закончится
        while (SubScenario.isFinished==false)
		{
			yield return new WaitForSeconds(1.0f);
		}
		//закочился
        OK();
    }
	

	//когда нажали на ОК...
	public void OK()
	{
		if (После_завершения==StepEnum.Возврат_к_основному)
		{
			if (lastRoutine!=null)	StopCoroutine(lastRoutine);
			
			editor.ВсегоДействий += SubScenario.ВсегоДействий;
			editor.ВсегоОшибок += SubScenario.ВсегоОшибок;
		}
		//никакие аргументы не передаем в Editor, типа правильно/неправильно
		this.gameObject.SetActive(false);
		//посылаем команду на следующий шаг
		editor.StepFinish();
	}
	
}

