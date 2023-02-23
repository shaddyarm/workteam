
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ValveClass0 : MonoBehaviour 
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
	Renderer[] rs;
	
	float centerMousePosition;
	
	string startText;
	
	public void SetShift(float value)
	{
		currentShift=value;
		Update();
		upd();
	}
	
	
	// Use this for initialization
	void Start () 
	{
		
		
		
		rs = Shturval.GetComponentsInChildren<Renderer>();
		onSelected=false;
		upd();

		if (I != null)	startText = I.info;
	}
	
	// Update is called once per frame
	void Update () 
	{
		//1. color
		Color col = new Color();
		
		if ( onSelected == true)
		{
			col.r = 0;
			col.g = 1;
			col.b = 0;
		}
		else
		{
			col.r = 1;
			col.g = 1;
			col.b = 1;
		}
			
		SetColor(col);
	}
	
	void SetColor (Color color)
	{
		foreach (Renderer r in rs)
        {
            for (var j = 0; j < r.materials.Length; j++)
            {
                Color col;
                col.a = r.materials[j].color.a;
                col.r = color.r;
                col.g = color.g;
                col.b = color.b;

                r.materials[j].color = col;
            }

        }
	}
	
	
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
			I.info = "Заблокировано";
			return;
		}
		
		currentShift += (centerMousePosition - Input.mousePosition.x);

		if (currentShift<minA) currentShift=minA;
		if (currentShift>maxA) currentShift=maxA;
	
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

