using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;

public class infoClass : MonoBehaviour 
{
	bool Enabled=true;
	
	public void SetEnabled(bool value)
	{
		Enabled = value;
		if (Enabled==false)
		{
			HUD.SetActive(false);
		}
	}
	
	public GameObject HUD;
	public string info;
	public TextMeshProUGUI text;
	
	public string translateID = "";

	// Use this for initialization
	void Start () {
		
	}
	
	// Update is called once per frame
	void Update () {
		
	}
	
	void OnDisable()
    {
		if (HUD!=null) HUD.SetActive(false);
    }
	
	void OnMouseEnter()
    {
		if (Enabled==false) return;
		
		
		HUD.SetActive(true);
		text.gameObject.SetActive(true);
		
		text.text = info;
		
    }
	
	void OnMouseOver()
	{
		if (Enabled==false) return;
		
		if (HUD.active)
		{
			float x = Input.mousePosition.x;
			float y = Input.mousePosition.y+40f;
			HUD.transform.position = new Vector2(x,y);
			text.text = info;
		}
	}

    void OnMouseExit()
    {
		if (Enabled==false) return;
		
       HUD.SetActive(false);
    }
	
	void OnMouseDown()
    {
		if (Enabled==false) return;
		
		HUD.SetActive(false);
	}
	
}
