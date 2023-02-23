/***************************************************************************
ScenarioManager_LoadFromFile.cs  - редактор/пролигрыватель сценария 
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
using UnityEngine.Events;
using UnityEngine.SceneManagement;

public class Translate_LoadFromFile : MonoBehaviour 
{
	public string filename="";	
	public bool UseFile=false;	
	
	private string JSONstring = "";
	
	
	void MakeList()
	{
		//Сценарии
		{
			ScenarioEditor[] scenarions = Resources.FindObjectsOfTypeAll<ScenarioEditor>();	
			foreach (ScenarioEditor scenario in scenarions)
			{
				scenario.LoadAndUpdateTextsFromFile(ref JSONstring);
			}
		}
		//Тексты сценария
		{
			Scenario_step_text[] texts = Resources.FindObjectsOfTypeAll<Scenario_step_text>();	
			foreach (Scenario_step_text text in texts)
			{
				text.LoadAndUpdateTextsFromFile(ref JSONstring);
			}
		}
		//Вопросы сценария
		{
			Scenario_step_question[] questions = Resources.FindObjectsOfTypeAll<Scenario_step_question>();	
			foreach (Scenario_step_question question in questions)
			{
				question.LoadAndUpdateTextsFromFile(ref JSONstring);
			}
		}
	}
	
    void Start()
    {
        if (UseFile==false) return;
		
		
		string filePath = System.IO.Path.Combine(Application.streamingAssetsPath, filename);
		
		if (System.IO.File.Exists(filePath)==false) return;
		
		JSONstring = System.IO.File.ReadAllText(filePath);
		if (JSONstring=="") return;
		
		//1. Проходим все что нам сказали перевести
		MakeList ();
    }


}

