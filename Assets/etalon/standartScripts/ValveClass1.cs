
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
//using UnityEngine.Events;


//[System.Serializable]
//public class MyValveEvent : UnityEvent<float> {}


public class ValveClass1 : MonoBehaviour 
{
	public bool block=false;
	
	public GameObject Shturval;
	public GameObject Shtok;
	public infoClass I;
			
	public int minA;
	public int maxA;
	
	public float X;
	public float Y;
	public float Z;
	
	public float K;
	
	
	bool onSelected;
	public float currentShift;
	
	
	float centerMousePosition;
	
	string startText;
	
	
	
	public Scenario_value _value;


	public void SetBlock(bool value)
    {
		block = value;

	}


	public void SetShift(float value)
	{
		currentShift=value;
		upd();
	}
	
	
	// Use this for initialization
	void Start () 
	{
		
		
		
		
		onSelected=false;
		upd();

		if (I != null)	startText = I.info;
	}
	
	// Update is called once per frame
	
	
	
	
	
	void OnMouseEnter()
    {
        onSelected = true;
    }

    void OnMouseExit()
    {
        onSelected = false;
    }

    void OnMouseDown()
    {
		if (block) return;
		centerMousePosition= Input.mousePosition.x;
		
	}
	
	void OnMouseDrag()
    {
		if (block) 
		{
			if(I != null)
			{
				I.info = "Заблокировано";
			}
			return;
		}
		
		if (_value != null)
		if (_value.ChangeAllow==false) 
		{
			_value.SetValue(currentShift);
			return;
		}
		
		
		currentShift += (centerMousePosition - Input.mousePosition.x);

		if (currentShift<minA) currentShift=minA;
		if (currentShift>maxA) currentShift=maxA;
		
		//!!!!!!!!!!!!
		if (_value != null)
		_value.SetValue(currentShift);
		//!!!!!!!!!!!!
	
		upd();
			
		centerMousePosition= Input.mousePosition.x;
		
		float zzzzz = currentShift / (maxA-minA) * 100f;
		if (zzzzz<0) zzzzz = zzzzz * -1f;
		
		if (I != null)
		{
			I.info = startText + " \n" + "Открытие: " + zzzzz.ToString("N0") + " %";
		}
    }

	void upd()
	{
		Shturval.transform.localRotation = Quaternion.Euler(X * currentShift, Y * currentShift, Z * currentShift);

		if (Shtok != null)
		{ 
			Shtok.transform.localPosition = new Vector3(K * X * currentShift, K * Y * currentShift, K * Z * currentShift);
		}
	}
	
	
	

	
	
}

