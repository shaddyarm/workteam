/// Credit mgear, SimonDarksideJ
/// Sourced from - https://forum.unity3d.com/threads/radial-slider-circle-slider.326392/#post-3143582
/// Updated to include lerping features and programmatic access to angle/value

using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;


public class ManometrArrow : MonoBehaviour
{
	public RectTransform ImageArror;
	
	public float Max_angle=270f;
	public float Max_Value=60000000f;
	
	public Text text;

	void Update()
	{
		//SetValue (10000000f);
	}

	void Start()
	{
		
	}
	
	public void SetValue (float value)
	{
		float ugol = Max_angle*value / Max_Value;
		ImageArror.rotation = Quaternion.Euler (0, 0, -ugol);
		
		text.text = (value/1000000f).ToString("00.00");
	}
}
