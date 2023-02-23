using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MoleculeMini2 : MonoBehaviour 
{

	public bool YBLOCK=false;
	
	bool onSelected;
	public float currentShift;
	public float currentShift2;
	Renderer[] rs;
	
	float centerMousePosition;
	float centerMousePosition2;
	
	
	
	// Use this for initialization
	void Start () 
	{
		rs = transform.GetComponentsInChildren<Renderer>();
		onSelected=false;
		currentShift=0;
		currentShift2=0;
		
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
		return;
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
		centerMousePosition2= Input.mousePosition.y;
	}
	
	void OnMouseDrag()
    {
		currentShift  += (centerMousePosition - Input.mousePosition.x);
		if (YBLOCK==false)
		currentShift2 += (centerMousePosition2 - Input.mousePosition.y);
		
		
		

		upd();

		
		centerMousePosition  = Input.mousePosition.x;
		centerMousePosition2 = Input.mousePosition.y;
    }
	
	void upd()
	{

			//Shturval.transform.eulerAngles = new Vector3( currentShift2, Shturval.transform.eulerAngles.y, Shturval.transform.eulerAngles.z);
		transform.localRotation = Quaternion.Euler(0, currentShift, currentShift2);
	}
	
	
	

	
	
}
