using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using System.IO;

public class IgnoreStepText : MonoBehaviour 
{
	public AudioSource audio;
	public GameObject MY;
	
	public void Next()
	{
		if (audio != null)
		{
			audio.Pause();
			audio=null;
			
		}
	}
	
	public void hide()
	{
		MY.SetActive(false);
	}
	
	public void show()
	{
		MY.SetActive(true);
	}
		
}
