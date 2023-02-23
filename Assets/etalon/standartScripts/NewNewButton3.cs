using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.Events;
using UnityEngine.EventSystems;



public class NewNewButton3 : MonoBehaviour 
{
	public Camera myCam;

	public GameObject MY;
	
	public Xenu.Game.UnityOutlineManager manager;
	
	public UnityEvent myEventEnter;
	public UnityEvent myEventExit;
	public UnityEvent myEventDown;
	public UnityEvent myEventUp;
	
	public float MaxDistance=0;
	 
	 
	bool onSelected;
	bool previusButtonState;

	public bool Enabled = true;
	
	private Button ui_button = null;

	float LastTime = 0;


	
	private LayerMask _layerMask = 1 >> 8;
	

	void GetMouseInfo()
	{
		Ray ray = myCam.ScreenPointToRay(Input.mousePosition);
		RaycastHit hit;
		LayerMask layerMask = ~_layerMask;


		if (Physics.Raycast(ray, out hit, Mathf.Infinity, layerMask))
		{ 
			if (hit.collider.transform.name == name)
			{
				//Debug.Log("Mouse is over " + name + ".");
				if (onSelected==false)
                {
					previusButtonState = Input.GetMouseButton(0);
					OnMouseEnter();
				}
			}
			else
            {
				if (onSelected == true)
				{
					OnMouseExit();
				}
			}



		}

	}


	void Update()
    {
		LastTime += Time.deltaTime;

		if (myCam != null)
		{
			GetMouseInfo();
		}

		if (onSelected == true)
		{
			if ((Input.GetMouseButton(0)==true) && (previusButtonState==false))
            {
				

				previusButtonState = true;
				OnMouseDown();
			}

			if ((Input.GetMouseButton(0) == false) && (previusButtonState == true))
            {
				previusButtonState = false;
				OnMouseUp();
			}

		}


	}






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
		previusButtonState = false;


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

		if (LastTime < 1f) return;
		LastTime = 0;

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

	
	