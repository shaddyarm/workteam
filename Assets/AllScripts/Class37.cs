using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;

public class Class37 : MonoBehaviour 
{
	float ШТОК_ПОЛОЖЕНИЕ = 0;
	public Slider ПоложениеШтока;
	public TextMeshProUGUI ПоложениеШтокаЗначение;
	public TextMeshPro ПоложениеШтокаЗначение2;
	
	public Slider P_slider;
	public TextMeshProUGUI P_Text;
	public Slider I_slider;
	public TextMeshProUGUI I_Text;
	public Slider D_slider;
	public TextMeshProUGUI D_Text;
	
	
	public GameObject ModeSwitch;
	string SwitchMode = "0" ; // "Remote" "Local"
	
	public TextMeshProUGUI Mode_Text;
	public TextMeshPro Mode_Text2;
	
	public ValveClassAuma ШТУРВАЛ;
	
	public GameObject ШТОК;
	
	public GameObject УРОВЕНЬ;
	
	bool OpenActive=false;
	bool CloseActive=false;
	
	float VWater;
	
	
	public TextMeshProUGUI РАСХОД_В_1;
	public TextMeshPro РАСХОД_В_2;
	
	public TextMeshProUGUI РАСХОД_ИЗ_1;
	public TextMeshPro РАСХОД_ИЗ_2;
	
	public TextMeshProUGUI Уровень1;
	public TextMeshProUGUI Уровень2;
	
	public TextMeshProUGUI P1;
	public TextMeshProUGUI P2;
	public TextMeshPro P1b;
	public TextMeshPro P2b;
	
	public GameObject ЭКМ_стрелка;
	public GameObject ЭКМ_стрелка_КонтактMAX;
	
	public ValveClass0 ЭКМ_стрелка_MAX;
	
	public TextMeshProUGUI MAXMAX;
	
	public PID pid;
	
	public Toggle PIDDDD;
	
	public ValveClass0 ЗадвижкаВход;
	

	// Use this for initialization
	void Start () 
	{
		VWater=0;
	}
	
	// Update is called once per frame
	void Update () 
	{
		float h = VWater / 0.50264f; //h=v/s
		УРОВЕНЬ.transform.localScale = new Vector3(0.06699996f 	,  h/1.5f , 0.06699996f);
		
		
		
		float Q_IN = 0.012566f * ЗадвижкаВход.currentShift/ 360f; // m3/sec
		float Q_OUT = ШТОК_ПОЛОЖЕНИЕ * 0.02f + ШТОК_ПОЛОЖЕНИЕ * h * 0.001f; // m3/sec
		
		if (VWater<Q_OUT* Time.deltaTime) Q_OUT = Q_IN;
		
		VWater+=Q_IN* Time.deltaTime;
		VWater-=Q_OUT* Time.deltaTime ; //m3/sec
		
		if (VWater>0.75f) VWater=0.75f;
		if (VWater<0) VWater=0;
		
		
		
		//Расходомеры
		float Q_IN2 = Q_IN*60f*60f;
		float Q_OUT2 = Q_OUT*60f*60f;
		
		РАСХОД_В_1.text = Q_IN2.ToString("0.000");
		РАСХОД_В_2.text = Q_IN2.ToString("0.000");
		
		РАСХОД_ИЗ_1.text = Q_OUT2.ToString("0.000");
		РАСХОД_ИЗ_2.text = Q_OUT2.ToString("0.000");
		
		Уровень1.text = h.ToString("0.000");
		Уровень2.text = h.ToString("0.000");
		
		//низ kPa
		float P_1 = 1000f * 9.81f * h / 1000f;
		//верх
		float P_2 = 1000f * 9.81f * (h-1.4f) / 1000f;
		if (P_2<0) P_2=0;
		
		P1.text = P_1.ToString("0.000");
		P2.text = P_2.ToString("0.000");
		P1b.text = P_1.ToString("0.000");
		P2b.text = P_2.ToString("0.000");
		
		
		//середина
		float P_3 = 1000f * 9.81f * (h-0.75f) / 1000f;
		if (P_3<0) P_3=0;
		float P_3_angle = P_3*63.75f/2f;
		
		float P_3_angle_max = ЭКМ_стрелка_MAX.currentShift;
		
		ЭКМ_стрелка.transform.localRotation = Quaternion.Euler(0 , 0, P_3_angle);
		
		if (P_3_angle_max > P_3_angle) 
		{
			ЭКМ_стрелка_КонтактMAX.transform.localRotation = Quaternion.Euler(0 , 0, P_3_angle);
			MAXMAX.text = "нет";
		}
		else
		{
			ЭКМ_стрелка_КонтактMAX.transform.localRotation = Quaternion.Euler(0 , 0, P_3_angle_max);
			MAXMAX.text = "да";
		}
		
		
		if (PIDDDD.isOn == true)
		{
			float НовоеЗначениеПозиции = pid.Update(1f, h, Time.deltaTime);
			Debug.Log(НовоеЗначениеПозиции);
		
			if (SwitchMode == "Remote") 
			{
				float zzz = 1f-НовоеЗначениеПозиции;
				if (zzz>1f) zzz=1f;
				if (zzz<0) zzz=0;
				ПоложениеШтока.value = zzz;
			}
		}
		
		
		
		
		
		
		
		float PValue = P_slider.value;
		P_Text.text = PValue.ToString("0.000");
		float IValue = I_slider.value;
		I_Text.text = IValue.ToString("0.000");
		float DValue = D_slider.value;
		D_Text.text = DValue.ToString("0.000");
		
		if (SwitchMode == "Local")
		{
			if (OpenActive)
			{
				ШТОК_ПОЛОЖЕНИЕ += Time.deltaTime*0.2f;
				if (ШТОК_ПОЛОЖЕНИЕ>1f)
				{
					ШТОК_ПОЛОЖЕНИЕ=1f;
					OpenActive=false;
				}
				upd();
			}
			if (CloseActive)
			{
				ШТОК_ПОЛОЖЕНИЕ -= Time.deltaTime*0.2f;
				if (ШТОК_ПОЛОЖЕНИЕ<0)
				{
					ШТОК_ПОЛОЖЕНИЕ=0;
					CloseActive=false;
				}
				upd();
			}
		}
		
		
		if (SwitchMode == "Remote") 
		{
			if (ПоложениеШтока.value!=ШТОК_ПОЛОЖЕНИЕ)
			{
				float delta = ПоложениеШтока.value-ШТОК_ПОЛОЖЕНИЕ;
				if (Mathf.Abs(delta)> Time.deltaTime * 0.2f)
				{
					if (delta>0) ШТОК_ПОЛОЖЕНИЕ += Time.deltaTime * 0.2f;
					if (delta<0) ШТОК_ПОЛОЖЕНИЕ -= Time.deltaTime * 0.2f;
					
					if (ШТОК_ПОЛОЖЕНИЕ<0) ШТОК_ПОЛОЖЕНИЕ=0;
					if (ШТОК_ПОЛОЖЕНИЕ>1f) ШТОК_ПОЛОЖЕНИЕ=1f;
					upd();
				}
				else
				{
					ШТОК_ПОЛОЖЕНИЕ = ПоложениеШтока.value;
					upd();
				}
			}
			
		}
		
	}
	
	public void ModeSwitchPress()
	{
		if (SwitchMode=="0") 
		{
			SwitchMode = "Remote";
		}
		else if (SwitchMode == "Remote")
		{
			SwitchMode = "Local";
		}
		else if (SwitchMode == "Local")
		{
			SwitchMode = "0";
		}
		
		float angle = 0;
		if (SwitchMode=="0") 
		{
			angle=0;
			Mode_Text.text = "Режим привода: 0";
			Mode_Text2.text = "Mode: 0";
			ШТУРВАЛ.block_close=false;
			ШТУРВАЛ.block_open=false;
			
			ПоложениеШтока.interactable = false;
		}
		else if (SwitchMode == "Remote")
		{
			angle=45f;
			Mode_Text.text = "Режим привода: Remote";
			Mode_Text2.text = "Mode: Remote";
			ШТУРВАЛ.block_close=true;
			ШТУРВАЛ.block_open=true;
			
			ПоложениеШтока.interactable = true;
		}
		else if (SwitchMode == "Local")
		{
			angle=-45f;
			Mode_Text.text = "Режим привода: Local";
			Mode_Text2.text = "Mode: Local";
			ШТУРВАЛ.block_close=true;
			ШТУРВАЛ.block_open=true;
			
			ПоложениеШтока.interactable = false;
		}
		
		ModeSwitch.transform.localRotation = Quaternion.Euler(0 ,  angle , 0);
		
	}
	
	public void ChangePidKoeff()
	{
		pid.pFactor = P_slider.value;
		pid.iFactor = I_slider.value;
		pid.dFactor = D_slider.value;
	}
	
	public void DX(float dx)
	{
		ШТОК_ПОЛОЖЕНИЕ += dx / 720f;
		
		if (ШТОК_ПОЛОЖЕНИЕ<=0)
		{
			ШТУРВАЛ.block_close=true;
			ШТОК_ПОЛОЖЕНИЕ=0;
		}
		else
		{
			ШТУРВАЛ.block_close=false;
		}
		if (ШТОК_ПОЛОЖЕНИЕ>=1)
		{
			ШТУРВАЛ.block_open=true;
			ШТОК_ПОЛОЖЕНИЕ=1f;
		}
		else
		{
			ШТУРВАЛ.block_open=false;
		}
		
		upd();
		
	}
	
	public void PressOpenButton()
	{
		OpenActive=true;
		CloseActive=false;
	}
	public void PressCloseButton()
	{
		OpenActive=false;
		CloseActive=true;
	}
	public void PressStopButton()
	{
		OpenActive=false;
		CloseActive=false;
	}
	
	
	
	void upd()
	{
		if (SwitchMode != "Remote")
		{
			ПоложениеШтока.value = ШТОК_ПОЛОЖЕНИЕ;
		}
		
		ПоложениеШтокаЗначение.text = ШТОК_ПОЛОЖЕНИЕ.ToString("0.000");
		ПоложениеШтокаЗначение2.text = ШТОК_ПОЛОЖЕНИЕ.ToString("0.000");
		
		ШТОК.transform.localPosition = new Vector3( 0 , 0,ШТОК_ПОЛОЖЕНИЕ * 1.51f);
		
		
		ПоложениеШтокаЗначение.text = ШТОК_ПОЛОЖЕНИЕ.ToString("0.000");
		
		
		
		
	}
}
