using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class AnimationClass : MonoBehaviour 
{
	
	public Animator СТРОПАЛЬЩИК;
	public Animator КРАН;
	
	bool active;	
	float time;
	
	float timestart;
	float timeend;
	
	public void PlayAnimationN(int N)
	{
		if (N==1) PlayAnimation(0, 240f);
		if (N==2) PlayAnimation(240f, 480f);
		if (N==3) PlayAnimation(480f, 720f);
		if (N==4) PlayAnimation(720f, 990f);
		if (N==5) PlayAnimation(990f, 1200f);
		if (N==6) PlayAnimation(1200f, 1400f);
		if (N==7) PlayAnimation(1400f, 1590f);
		
		
		
	}
	
	void PlayAnimation(float timeStart, float timeEnd)
	{
		active=true;
		time = 0;
		timestart=timeStart;
		timeend=timeEnd;
	}
	

	// Use this for initialization
	void Start () 
	{
		active=false;
		СТРОПАЛЬЩИК.speed = 0.0f;
		КРАН.speed = 0.0f;
	}
	

	// Update is called once per frame
	void Update () 
	{
		if (active==false) return;
		
		time += Time.deltaTime;
		
		float frame = timestart/1590f + time/40f;
		
		if (frame<=timeend/1590f )
		{	
			СТРОПАЛЬЩИК.ForceStateNormalizedTime(frame);
			КРАН.ForceStateNormalizedTime(frame);
		}
		else
		{
			active=false;
		}
		
	}
		
	//////
	
	
	
	
}

	
	