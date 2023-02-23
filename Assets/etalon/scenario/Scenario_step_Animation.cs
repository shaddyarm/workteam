/***************************************************************************
Scenario_step_Animation.cs  - редактор/пролигрыватель сценария 
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

public class Scenario_step_Animation : MonoBehaviour 
{
	//ссылка на сценарий, там все элементы и текст и меню и все остальное
	//там же метод Next, когда сценарий закончился...
	private ScenarioEditor editor = null;
	
	public Animator anim;
	public string animation_name;
	
	//тип 
	public StepEnum Ждем_Окончания;
	public enum StepEnum {
		Да,	
		Нет
	}
	
	Coroutine lastRoutine = null;
	
	//настройка, привязываем обработчики
	public void Setup(ScenarioEditor _editor)
	{
		editor = _editor;
		anim.Play(animation_name, -1, 0);
		
		if (Ждем_Окончания==StepEnum.Да)
		{
			//настраиваем таймер, по завершению которого вызовется ОК
			float time=0;
			RuntimeAnimatorController ac = anim.runtimeAnimatorController;    //Get Animator controller
			for(int i = 0; i<ac.animationClips.Length; i++)                 //For all animations
			{
				if(ac.animationClips[i].name == animation_name)        
				{
					time = ac.animationClips[i].length ;
					break;
				}
			}
			lastRoutine = StartCoroutine(WaitMethod(time));
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
			if (lastRoutine!=null)	StopCoroutine(lastRoutine);
		}
		//никакие аргументы не передаем в Editor, типа правильно/неправильно
		this.gameObject.SetActive(false);
		//посылаем команду на следующий шаг
		editor.StepFinish();
	}
	
}

