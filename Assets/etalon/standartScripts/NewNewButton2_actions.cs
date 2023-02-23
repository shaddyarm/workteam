using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.Events;

public class NewNewButton2_actions : MonoBehaviour 
{
	public List<UnityEvent> myEventsList;
	//myEventEnter.Invoke();
	
	public bool repeat=false;
	public int currentStep=0;
	
	public void NextStep()
	{
		if (myEventsList.Count <= currentStep)
		{
			if (repeat==true) currentStep=0;
		}
		
		if (myEventsList.Count > currentStep)
		{
			myEventsList[currentStep].Invoke();
		}
		
		currentStep++;
	}
	
	
	
}

	
	