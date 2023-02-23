
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

[System.Serializable]
public class MyFloatEvent : UnityEvent<float>
{
}

public class ValveClassAuma : MonoBehaviour 
{
	public bool block_open=false;
	public bool block_close=false;

	public GameObject Shturval;
	
	public float X;
	public float Y;
	public float Z;
	
	public MyFloatEvent myEvent;
	

	bool onSelected;
	Renderer[] rs;
	
	float centerMousePosition;
	float currentShift;
	
	// Use this for initialization
	void Start () 
	{
		currentShift=0;
		rs = Shturval.GetComponentsInChildren<Renderer>();
		onSelected=false;
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
		centerMousePosition= Input.mousePosition.x;
	}
	
	void OnMouseDrag()
    {
		float dx = centerMousePosition - Input.mousePosition.x;
		if ((dx<0)&&(block_close==true)) return;
		if ((dx>0)&&(block_open==true)) return;
		
		if ((block_open==true)&&(block_close==true)) return;
		
		currentShift += dx;
		Shturval.transform.localRotation = Quaternion.Euler(X * currentShift , Y * currentShift, Z * currentShift);
		centerMousePosition= Input.mousePosition.x;
		
		myEvent.Invoke(dx);
    }

}

