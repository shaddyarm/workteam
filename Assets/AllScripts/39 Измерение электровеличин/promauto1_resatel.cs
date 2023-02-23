using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class promauto1_resatel : MonoBehaviour 
{
	/*
	public Material sin1;
	public Material sin2;
	public TextMesh fr1;
	public TextMesh fr2;
	public TextMesh amp1;
	public TextMesh amp2;
	public TextMesh Vt;

	public ValveClass FR;
	public ValveClass AMP;
	public ValveClass V;
	
	float old_fr;
	float old_v;
	float old_amp;
	
	float _time;
	bool ok;
	
	public TextMesh V1;
	public TextMesh F1;
	public TextMesh V2;
	public TextMesh F2;
	
	
	// Use this for initialization
	void Start () 
	{
		_time=1f;
		ok=false;
		
		old_v = 1f + V.currentShift / 27.0f;
		old_fr = 1f + FR.currentShift * 333.0f;
		old_amp = 1f + AMP.currentShift / 300.0f;
		
	}
	
	// Update is called once per frame
	void Update () 
	{
		float _v = 1f + V.currentShift / 27.0f;
		Vt.text = _v.ToString("N0") + " V";
		
		float _fr = 1f + FR.currentShift * 333.0f;
		fr1.text = _fr.ToString("N0") + " Hz";
		fr2.text = fr1.text;
		
		float _amp = 1f + AMP.currentShift / 300.0f;
		amp1.text = _amp.ToString("N1") + " V";
		amp2.text = amp1.text;
		
		
		if ((old_fr!=_fr)||(old_v!=_v)||(old_amp!=_amp))
		{
			ok=true;
			old_v = 1f + V.currentShift / 27.0f;
			old_fr = 1f + FR.currentShift * 333.0f;
			old_amp = 1f + AMP.currentShift / 300.0f;
			
		}
		
		if (ok==false)
		{
			float __v1 = _v ;
			V1.text = __v1.ToString("N1") + " V";
			F1.text = fr1.text;
			
			float __v2 = _v+ _amp;
			V2.text = __v2.ToString("N1") + " V";
			F2.text = fr1.text;
		}
		
		if (ok==true)
		{
			_time +=Time.deltaTime*10f;
			if (_time>12)
			{
				_time=1;
				ok=false;
			}
		}
		
		sin1.SetTextureScale("_MainTex", new Vector2(0.015f* _time, 0.02f * 1f ));
		
	}
	*/
	
}

	
	