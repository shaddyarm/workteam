/***************************************************************************
simply_showDocument.cs  - редактор/пролигрыватель сценария 
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

public class simply_showDocument : MonoBehaviour 
{
	public DocumentShowClass viewer = null;
	
	//все страницы
	public List <Sprite> pages;
	
	//настройка, привязываем обработчики
	public void Setup()
	{
		//
		viewer.gameObject.SetActive(true);
		viewer.Setup(ref pages);
	}
	
}

