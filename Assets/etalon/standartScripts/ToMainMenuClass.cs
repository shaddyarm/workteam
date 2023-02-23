using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.Events;

public class ToMainMenuClass : MonoBehaviour 
{
	public UnityEvent MyEventToMainMenu;
	public bool isEkzamen;
	public GameObject MenuAlert;
	
	void Start()
	{
		isEkzamen = false;
		MenuAlert.SetActive(false);
	}
	
	public void ToMAINmenuExit()
	{
		//
		
		
		
		if (isEkzamen==true)
		{
			MenuAlert.SetActive(true);
		}
		else
		{
			MyEventToMainMenu.Invoke ();
			Application.LoadLevel(Application.loadedLevel);
		}
	}
	
	public void ForceMAINmenuExit()
	{
		isEkzamen = false;
		MyEventToMainMenu.Invoke ();
		Application.LoadLevel(Application.loadedLevel);
	}

	
}
