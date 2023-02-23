using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class ZoomImage : MonoBehaviour 
{
	public RectTransform ob;
	public Slider slider;
	
	public void Zoom()
	{
		float k = slider.value;
		ob.localScale = new Vector3(k,k,1f);
	}
	
	public void Reset()
	{
		slider.value=1f;
	}
	
	
	
	
}
