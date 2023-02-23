using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class ResatelNGPO13 : MonoBehaviour 
{
	public Text РЕЖИМ;
	public Text КОМАНДА;
	public Text ПОЛОЖЕНИЕ_ТЕКСТ;
	public Slider ПОЛОЖЕНИЕ_СЛАЙДЕР;
	
	
	public GameObject АСУТП;
	public Text P;
	public Slider _P;
	
	
	public AudioSource MOTOR;
	
	//0-0
	//1-local
	//2-remove
	int mode;
	
	public GameObject Switch;

	
	public GameObject ШТОК;
	
	float Position;
	float Position_ASUTP;
	float dy;
	
	// Use this for initialization
	void Start () 
	{
		mode=0;
		Position=0;
		Position_ASUTP=0;
		dy=0;
	}
	
	// Update is called once per frame
	void Update () 
	{
		ПОЛОЖЕНИЕ_СЛАЙДЕР.value =  Position *(1f/0.125f);
		ПОЛОЖЕНИЕ_ТЕКСТ.text = ПОЛОЖЕНИЕ_СЛАЙДЕР.value.ToString("N3");
		
		if (dy==0) КОМАНДА.text = "---";
		if (dy<0) КОМАНДА.text = "ЗАКРЫТИЕ";
		if (dy>0) КОМАНДА.text = "ОТКРЫТИЕ";
		
		if (mode==1)
		{
			dy=0;
			if ((Position-Position_ASUTP)<0.001f) dy = 0.1f;
			if ((Position-Position_ASUTP)>-0.001f) dy = -0.1f;
			
			if (Mathf.Abs (Position-Position_ASUTP)<0.0025f) dy=0;
			
			if ((MOTOR.isPlaying == true)&&(dy==0)) MOTOR.Pause();
			if ((MOTOR.isPlaying == false)&&(dy!=0)) MOTOR.Play();
			
			
		}
		
		
		ШТОК.transform.localPosition = new Vector3(0, Position, 0);
		
		Position+=dy*Time.deltaTime*0.1f;
		
		if (mode==2)
		{
			if(dy!=0) _P.value = Position *(1f/0.125f);
		}	
		
		if (Position<0) 
		{
			Position=0;
			dy=0;
			MOTOR.Pause();
		}
		if (Position>0.125f) 
		{
			Position=0.125f;
			dy=0;
			MOTOR.Pause();
		}
		
		float switch_angle=0;
		if (mode==1)
		{
			switch_angle = 45f;
		}
		if (mode==2)
		{
			switch_angle = -45f;
		}
		Switch.transform.localRotation = Quaternion.Euler(switch_angle, 0, 0);
		
	}
		
	public void CHANGE()
	{
		P.text = _P.value.ToString("N3");
		Position_ASUTP = _P.value *0.125f;
	}
	
	public void PRESS(string name)
	{
		if (name=="mode")
		{
			dy=0;
			MOTOR.Pause();
			mode++;
			if (mode>=3) mode=0;
			
			if (mode==1)
			{
				АСУТП.SetActive(true);
			}
			else
			{
				АСУТП.SetActive(false);
			}
			
			if (mode==0) РЕЖИМ.text = "Выключен";
			if (mode==1) РЕЖИМ.text = "Дистанционный";
			if (mode==2) РЕЖИМ.text = "Локальный";
		}
		
		
		
		
		
		if ((name=="OPEN")&&(mode==2))
		{
			dy=0.1f;
			MOTOR.Play();
		}
		if ((name=="CLOSE")&&(mode==2))
		{
			dy=-0.1f;
			MOTOR.Play();
		}
		if ((name=="STOP")&&(mode==2))
		{
			dy=0;
			MOTOR.Pause();
		}
		
		
	}
}
