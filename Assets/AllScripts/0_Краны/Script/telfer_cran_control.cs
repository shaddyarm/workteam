using System.Collections;
using System.Collections.Generic;
using UnityEngine;


public class telfer_cran_control : MonoBehaviour 
{


    
	
	public List<CranStrop> СТРОПЫ;
	public List<CranGruz> ГРУЗЫ;
	
	
	
	IEnumerator CheckForControllers() 
	{
		Debug.Log("...");
		bool connected = false;
		while (true) 
		{
			var controllers = Input.GetJoystickNames();
			if (!connected && controllers.Length > 0) 
			{
				connected = true;
				Debug.Log("Connected");
			} else if (connected && controllers.Length == 0) 
			{
				connected = false;
				Debug.Log("Disconnected");
			}
			yield return new WaitForSeconds(1f);
		}
	}
	
	IEnumerator HideStrops() 
	{
		yield return new WaitForSeconds(1);
		for (int i=0;i<СТРОПЫ.Count;i++)
		{
			СТРОПЫ[i].Стропа.SetActive(false);
		}
	}


	void Awake()
    {
		Time.fixedDeltaTime = 0.004f;
		Time.maximumDeltaTime = 0.3333f;
		
		StartCoroutine(CheckForControllers());
		StartCoroutine(HideStrops());
    }
	
	
	// Use this for initialization
	void Start () 
	{
		
	}
	
	// Update is called once per frame
	void Update () 
	{
		
	}
	
	
	
	
	
	
}
