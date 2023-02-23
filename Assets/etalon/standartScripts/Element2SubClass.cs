using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.Events;


public class Element2SubClass : MonoBehaviour 
{
	public Element2Class parent;
	public IgnoreStepText NextStepButton;
	public GameObject my;
	public AudioSource myAudioSource;
	public GameObject textpanel;
	public string TEXT;
	public Text text;
	Renderer[] rs;
	float time;
	public bool GoGo;
	public UnityEvent m_MyEvent;
	
	public void Reset()
	{
		GoGo=false;
		time=0;
		if (myAudioSource != null)
		{
			myAudioSource.Stop();
		}
		hide();
	}
	
	public void hide()
	{
		my.SetActive(false);
	}
	
	public void show()
	{
		my.SetActive(true);
	}
	
	void Start () 
	{
		GoGo=false;
		rs = my.GetComponentsInChildren<Renderer>();
	}
	
	// Update is called once per frame
	void Update () 
	{
		if (GoGo==false)
		{
			time +=  Time.deltaTime*4f;
			if (time>180f) time=0;
			SetColorAlpha (Mathf.Sin(time));
		}
		else
		{
			SetColorAlpha (0);
			if (myAudioSource != null)
			{
				if (myAudioSource.isPlaying == false)
				{
					my.SetActive(false);
					parent.nextOK=true;
				}
			}
			else
			{
				my.SetActive(false);
				parent.nextOK=true;
			}
			
			
			
			
			
			
		}
	}
	
	void SetColorAlpha (float alpha)
	{
		if (alpha<0) alpha=-alpha;
		alpha=alpha/2f;
		foreach (Renderer r in rs)
        {
            for (var j = 0; j < r.materials.Length; j++)
            {
                Color col;
                col.a = alpha;
                col.r = r.materials[j].color.r;
                col.g = r.materials[j].color.g;
                col.b = r.materials[j].color.b;
                r.materials[j].color = col;
            }
        }
	}
		
	public void Go()
	{
		
		GoGo=true;
		if (myAudioSource != null)
		{
			NextStepButton.audio = myAudioSource;
			myAudioSource.Play();
			NextStepButton.show();
		}
		else
		{
			NextStepButton.hide();
		}
		
	}
	
	//нажатие
	void OnMouseDown()
    {
		if (parent.nextOK==false) return;
		if (GoGo) return;
		
		parent.nextOK=false;
		
		if (TEXT!="")
		{
			textpanel.SetActive(true);
		}
		text.text = TEXT;

		
		Go();
		
		m_MyEvent.Invoke();
    }
	
}
