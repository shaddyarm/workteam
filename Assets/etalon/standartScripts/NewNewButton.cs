using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.Events;

public class NewNewButton : MonoBehaviour {

	public GameObject MY;
	
	public UnityEvent myEvent;
	 
	 
	bool onSelected;
	Renderer[] rs;
	
	// Use this for initialization
	void Start () 
	{
		rs = MY.GetComponentsInChildren<Renderer>();
		onSelected=false;
	}
	
	// Update is called once per frame
	void Update2 () 
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
		Update2();
    }

    void OnMouseExit()
    {
        onSelected = false;
		Update2();
    }

    void OnMouseDown()
    {
		onSelected=false;
		Update2();
		myEvent.Invoke();
		
	}
}

	
	