using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.Events;


public class ElementNewUvatClass : MonoBehaviour 
{
	public string Question;
	public string Answer1;
	public string Answer2;
	public string Answer3;
	public string Answer4;
	
		public GameObject Z;
		
	public GameObject my;
	public UnityEvent myEvent0;
	
	public UnityEvent myEventCorrect;
	
	public AudioSource audioSource;
	
	public float ПлюсДействиям;
	public float ПлюсУсловиям;
	
	
	public GameObject PLAYER;
	

	Renderer[] rs;
	float time;
	
	
	void Start () 
	{
		rs = my.GetComponentsInChildren<Renderer>();
		time=0;
		Z = transform.GetChild (0).gameObject;
	}
	
	
	
	// Update is called once per frame
	void Update () 
	{
		time +=  Time.deltaTime*4f;
		if (time>180f) time=0;;
		//SetColorAlpha (Mathf.Sin(time));
		
		my.transform.localRotation = Quaternion.Euler(0,  time*10f , 0);
		
		
		//////
		float dist = Vector3.Distance (PLAYER.transform.position, my.transform.position);
		if (dist>10f)
		{
			Z.SetActive(false);
		}
		else
		{
			Z.SetActive(true);
		}
		
	}
	
	void SetColorRed ()
	{
			Color col;
			col.a = 1f;
			col.r = 1f;
			col.g = 0;
			col.b = 0;
			rs[0].materials[0].color = col;

	}
	
	void SetColorGreen ()
	{
			Color col;
			col.a = 1f;
			col.r = 0;
			col.g = 1f;
			col.b = 0;
			rs[0].materials[0].color = col;

	}
	
	public void CorrectAnswer()
	{
		if (audioSource != null ) audioSource.Play();
		
		myEventCorrect.Invoke();
	}
	
	public void InCorrectAnswer()
	{
		//myEventCorrect.Invoke();
	}
	
	
	//нажатие
	void OnMouseDown()
    {
		myEvent0.Invoke();
		SetColorRed();
    }
	
	void OnMouseEnter()
    {
       SetColorGreen ();
    }

    
    void OnMouseExit()
    {
       SetColorRed();
    }
	
}
