using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.Events;
using UnityEngine.EventSystems;



public class NewNewButton2 : MonoBehaviour 
{
	public GameObject MY;
	
	public Xenu.Game.UnityOutlineManager manager;
	
	public UnityEvent myEventEnter;
	public UnityEvent myEventExit;
	public UnityEvent myEventDown;
	public UnityEvent myEventUp;
	
	public float MaxDistance=0;
	 
	 
	bool onSelected;
	
	public bool Enabled = true;
	
	private Button ui_button = null;
	
	public void SetEnabled(bool value)
	{
		Enabled=value;
		//if (Enabled==false) manager.ClearSelection2();
	}
	
	void UpdateState()
	{
		if (onSelected==true)
		{
			//manager.ClearSelection();
			manager.AddSelect2(MY);
			
		}
		else
		{
			manager.ClearSelection2();
			//manager.RemoveSelect(MY);
		}
	}
	
	
	// Use this for initialization
	void Start () 
	{
		onSelected=false;
		
		ui_button = gameObject.GetComponent<Button>();
      
	}
	
	void OnMouseEnter()
    {
		if (EventSystem.current.IsPointerOverGameObject()==true) return;
		if (Enabled==false) return;
		
		if (MaxDistance!=0)
		{
			float distance = Vector3.Distance (manager.outlinePostEffect.gameObject.transform.position, MY.transform.position);
			if (distance>MaxDistance) return;
		}
		
        onSelected = true;
		UpdateState();
		myEventEnter.Invoke();
		
    }

    void OnMouseExit()
    {
		if (EventSystem.current.IsPointerOverGameObject()==true) return;
		if (Enabled==false) return;
		
        onSelected = false;
		UpdateState();
		
		if (MaxDistance!=0)
		{
			float distance = Vector3.Distance (manager.outlinePostEffect.gameObject.transform.position, MY.transform.position);
			if (distance>MaxDistance) return;
		}
		
		myEventExit.Invoke();

    }

    void OnMouseDown()
    {
		if (EventSystem.current.IsPointerOverGameObject()==true) return;
		if (Enabled==false) return;
		
		if (MaxDistance!=0)
		{
			float distance = Vector3.Distance (manager.outlinePostEffect.gameObject.transform.position, MY.transform.position);
			if (distance>MaxDistance) return;
		}
		
		myEventDown.Invoke();
		
		if (ui_button!=null)
		{
			ui_button.onClick.Invoke();
		}
	}
	
	void OnMouseUp()
	{
		if (EventSystem.current.IsPointerOverGameObject()==true) return;
		if (Enabled==false) return;
		
		if (MaxDistance!=0)
		{
			float distance = Vector3.Distance (manager.outlinePostEffect.gameObject.transform.position, MY.transform.position);
			if (distance>MaxDistance) return;
		}
		
		myEventUp.Invoke();
	}
	
}

	
	