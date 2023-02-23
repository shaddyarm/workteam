using UnityEngine;
using UnityEngine.UI;
using System.Collections;
using System.Collections.Generic;
using UnityEngine.Events;

//звук

public class SelectAndStartScenario : MonoBehaviour 
{
	//ссылка на сценарий, там все элементы и текст и меню и все остальное
	//там же метод Next, когда сценарий закончился...
	public List<ScenarioEditor> editors;
	public GameObject StartMenu;

	
	public Toggle ExamMode;
	public Dropdown ScenariesList;
	
	void Start()
	{
		StartMenu.SetActive(true);
		foreach (ScenarioEditor element in editors)
		{
			ScenariesList.options.Add(new Dropdown.OptionData(element.ScenarioName));
		}
		
		ScenariesList.value=0;
	}
	
	public void GoGo()
	{
		StartMenu.SetActive(false);
		int currentNum = ScenariesList.value;
		
		editors[currentNum].exam_mode = ExamMode.isOn;
		editors[currentNum].ManualStart(0);
		

	}
	
}

