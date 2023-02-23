using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class SuperRazbor2 : MonoBehaviour 
{
	public GameObject a1;
	
	bool b1;
	
	
	public float dX;
	public float dY;
	public float dZ;
	
	Renderer[] rs;
	bool onSelected;
	
	void Z()
	{
		b1 = !b1;
		
		if (b1==false) 
		{
			a1.transform.localPosition = new Vector3(a1.transform.localPosition.x -dX * 1f, a1.transform.localPosition.y-dY , a1.transform.localPosition.z-dZ);
		}
		else
		{
			a1.transform.localPosition = new Vector3(a1.transform.localPosition.x + dX * 1f,a1.transform.localPosition.y+dY, a1.transform.localPosition.z+dZ);
		}
		
		
		
		
	}
	
		
	// Use this for initialization
	void Start () 
	{
		rs = transform.GetComponentsInChildren<Renderer>();
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
		Z();
	}
	
	void OnMouseDrag()
    {
    }
	
}