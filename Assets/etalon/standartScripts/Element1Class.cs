using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class Element1Class : MonoBehaviour 
{
	public IgnoreStepText NextStepButton;
	public string ID;
	public string TEXT;
	public GameObject my;
	public AudioSource myAudioSource;
	public Observer1Class myObserver;
	Renderer[] rs;
	float time;
	
	public GameObject textPanel;
	public Text text;
	
	bool GoGo;
	
	public void hide()
	{
		my.SetActive(false);
	}
	
	public void show()
	{
		my.SetActive(true);
	}
	
	public void Reset()
	{
		if (myAudioSource != null)
		{
			myAudioSource.Stop();
		}
		GoGo=false;
		time=0;
		
		
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
			if (time>180f) time=0;;
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
					myObserver.nextOK=true;
				}
			}
			else
			{
				my.SetActive(false);
				myObserver.nextOK=true;
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
	
	public void ShowQuestion()
	{
		textPanel.SetActive(true);
		text.text = "Укажите - " + TEXT;
	}
	
	public void Go()
	{
		myObserver.nextOK=false;
		
		GoGo=true;
		textPanel.SetActive(true);
		text.text = TEXT;
		
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
		if (myObserver.nextOK==false) return;
		if (GoGo) return;
		
		//если режим обучения, то показал текст, звук (если есть) и исчез
		if (myObserver.ЭКЗАМЕН==false)
		{
			myObserver.Press(ID);
			Go();
			
		}
		
		//если режим экзамена, то передал myObserver что нажали ID, потом он рулит
		if (myObserver.ЭКЗАМЕН==true)
		{
			myObserver.Press(ID);
		}
		
    }
	
}
