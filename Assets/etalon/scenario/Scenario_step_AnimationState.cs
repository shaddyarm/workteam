/***************************************************************************
Scenario_step_Animation.cs  - редактор/пролигрыватель сценария 
-------------------
begin                : 27 мая 2020
copyright            : (C) 2020 by Гаммер Максим Дмитриевич (maximum2000)
email                : MaxGammer@gmail.com
site				 : lcontent.ru 
org					 : ИП Гаммер Максим Дмитриевич
***************************************************************************/
//https://github.com/cfoulston/Unity-Reorderable-List

using UnityEngine;
using UnityEngine.UI;
using System.Collections;
using System.Collections.Generic;


//анимация на время и пауза

public class Scenario_step_AnimationState : MonoBehaviour 
{
	public Animator anim;
	public float CurrentTime;
	private float TargetTime;
	public float dT=1f;
	
	private bool initialized = false;
	
	void Start()
	{
		anim.speed=0;
		anim.ForceStateNormalizedTime(CurrentTime);
	}
	
	public void SetTargetTime(float t)
	{
		TargetTime=t;
		initialized=true;
	}
	
	void  Update ()
	{
		if (initialized==false) return;
		
		if (CurrentTime<= TargetTime)
		{
			CurrentTime+= Time.deltaTime / dT;
			
			if (CurrentTime >= TargetTime)
			{
				CurrentTime=TargetTime;
				anim.ForceStateNormalizedTime(CurrentTime);
				initialized=false;
				return;
			}
			else
			{
				anim.ForceStateNormalizedTime(CurrentTime);
			}
		}
		else if (CurrentTime> TargetTime)
		{
			CurrentTime-= Time.deltaTime / dT;
			if (CurrentTime <= TargetTime)
			{
				CurrentTime=TargetTime;
				anim.ForceStateNormalizedTime(CurrentTime);
				initialized=false;
				return;
			}
			else
			{
				anim.ForceStateNormalizedTime(CurrentTime);
			}
		}
		
	}
	
	
	
}

