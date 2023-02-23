using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.Events;

public class PushClass : MonoBehaviour 
{
	public UnityEvent myEvent0;


	
	
	void Start () 
	{
		
	}
	
	// Update is called once per frame
	void Update () 
	{
		
	}
	
	
	
	//нажатие
	void OnMouseDown()
    {
		myEvent0.Invoke();
    }
	
}
