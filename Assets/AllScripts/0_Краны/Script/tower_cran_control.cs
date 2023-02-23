using System.Collections;
using System.Collections.Generic;
using UnityEngine;


public class tower_cran_control : MonoBehaviour 
{


    //
	public float speed_forward_back=1f;
	public float forward_back_value=0f;
	public GameObject CranBase;
	public GameObject CranUp;
	public GameObject CranDown;
	
	
	
	public float speed_left_right=1f;
	public float left_right_value=0f;
	public GameObject CranRotatePlatform;
    
	public Transform target_1;
	public Transform target_2;
	
	
	public Play player;

	
	public List<CranStrop> СТРОПЫ;
	public List<CranGruz> ГРУЗЫ;
	
	public float speed_Caretka=1f;
	public float Caretka_forward_back_value=-18.63f;
	public GameObject CranCaretka;
	
	public float UpDown_Speed=1f;
	public float UpDown_value=-4.514f;
	public GameObject Hook;
	
	public GameObject StropOff;
	public GameObject StropOn;
	
	IEnumerator CheckForControllers() 
	{
		Debug.Log("...");
		bool connected = false;
		while (true) 
		{
			var controllers = Input.GetJoystickNames();
			if (!connected && controllers.Length > 0) 
			{
				connected = true;
				Debug.Log("Connected");
			} else if (connected && controllers.Length == 0) 
			{
				connected = false;
				Debug.Log("Disconnected");
			}
			yield return new WaitForSeconds(1f);
		}
	}
	
	IEnumerator HideStrops() 
	{
		yield return new WaitForSeconds(1);
		for (int i=0;i<СТРОПЫ.Count;i++)
		{
			СТРОПЫ[i].Стропа.SetActive(false);
		}
	}


	void Awake()
    {
		Time.fixedDeltaTime = 0.004f;
		Time.maximumDeltaTime = 0.3333f;
		
		StartCoroutine(CheckForControllers());
		StartCoroutine(HideStrops());
    }
	
	
	// Use this for initialization
	void Start () 
	{
		
	}
	
	// Update is called once per frame
	void Update () 
	{
		//грузы
		foreach (CranGruz gruz in ГРУЗЫ)
		{
			//проверяем расстояние от крюка
			float distance2 = Vector3.Distance (gruz.КоординатыГруза.transform.position, Hook.transform.position);
			if (distance2<=gruz.РасстояниеАктивации)
			{
				//запоминаем этот груз для стропальщиков
				//current_gruz_for_stropmens = gruz;
				gruz.ПоказатьСтрелки();
				if (gruz.Застрапован==true) 
				{
					StropOff.SetActive(true);
					StropOn.SetActive(false);
				}
				else
				{
					StropOff.SetActive(false);
					StropOn.SetActive(true);
				}
				break;
			}
			else
			{
				gruz.СпрятатьСтрелки();
				StropOff.SetActive(false);
				StropOn.SetActive(false);
			}
		}
		
		//Вперед назад
		{
			float Cran_forward_back_delta = Time.deltaTime * speed_forward_back;
			float Cran_forward_back_joy = 0;
			if (Input.GetKey(KeyCode.F1))
			{
				Cran_forward_back_joy = 1f;
				forward_back_value += Cran_forward_back_delta;
			}
			if (Input.GetKey(KeyCode.F2))
			{
				Cran_forward_back_joy = -1f;
				forward_back_value -= Cran_forward_back_delta;
			}
			if (forward_back_value<-2.5f) forward_back_value=-2.5f;
			if (forward_back_value>2.5f) forward_back_value=2.5f;
		}
		CranBase.transform.localPosition = new Vector3(forward_back_value,0,0);
		
		
		//Влево вправо
		{
			float left_right_value_delta = Time.deltaTime * speed_left_right;
			float Cran_left_right_joy = 0;
			if (Input.GetKey(KeyCode.F3))
			{
				Cran_left_right_joy = 1f;
				left_right_value += left_right_value_delta;
			}
			if (Input.GetKey(KeyCode.F4))
			{
				Cran_left_right_joy = -1f;
				left_right_value -= left_right_value_delta;
			}
		}
		CranRotatePlatform.transform.localRotation = Quaternion.Euler(0,left_right_value , 0);
		
		
		//Каретка Вперед назад
		{
			float Caretka_forward_back_delta = Time.deltaTime * speed_Caretka;
			float Caretka_forward_back_joy = 0;
			if (Input.GetKey(KeyCode.F5))
			{
				Caretka_forward_back_joy = 1f;
				Caretka_forward_back_value += Caretka_forward_back_delta;
			}
			if (Input.GetKey(KeyCode.F6))
			{
				Caretka_forward_back_joy = -1f;
				Caretka_forward_back_value -= Caretka_forward_back_delta;
			}
			if (Caretka_forward_back_value<-59.3f) Caretka_forward_back_value=-59.3f;
			if (Caretka_forward_back_value>-12.3f) Caretka_forward_back_value=-12.3f;
		}
		CranCaretka.transform.localPosition = new Vector3(Caretka_forward_back_value,74.56f,-1.67f);
		
		//Груз вверх вниз
		{
			float Up_down_delta = Time.deltaTime * UpDown_Speed;
			float UpDown_joy = 0;
			if (Input.GetKey(KeyCode.F7))
			{
				UpDown_joy = 1f;
				UpDown_value += Up_down_delta;
			}
			if (Input.GetKey(KeyCode.F8))
			{
				UpDown_joy = -1f;
				UpDown_value -= Up_down_delta;
			}
			if (UpDown_value<-74.32f) UpDown_value=-74.32f;
			if (UpDown_value>-4.514f) UpDown_value=-4.514f;
		}
		Hook.transform.localPosition = new Vector3(-0.01f,UpDown_value,-0.008f);
		

	

	
	
	
	}
	
	public void GoUp()
	{
		player.transform.parent = CranUp.transform;
		player.gameObject.transform.localPosition = target_1.localPosition;
		player.gameObject.transform.localRotation = target_1.localRotation;
		//player_camera.transform.localRotation = Quaternion.Euler(9.771001f , 0, 0);
	}
	
	public void GoDown()
	{
		player.transform.parent = CranDown.transform;
		player.gameObject.transform.localPosition = target_2.localPosition;
		player.gameObject.transform.localRotation = target_2.localRotation;
		//player_camera.transform.localRotation = Quaternion.Euler(9.771001f , 0, 0);
	}
	
		
	public void ПРИВЯЗАТЬ_ГРУЗ()
	{
		
		
		int o = GetIndexNearGruz();
		if (o==-1) return;
		if (ГРУЗЫ[o].Застрапован==true) return;
		
		
		
		
		ГРУЗЫ[o].Застрапован=true;
		Debug.Log ("ПРИВЯЗАТЬ...");
		
		//StropaNull.SetActive(false);
		//StropaRoga.SetActive(false);
		
		int stropa_num = ГРУЗЫ[o].НомерСтропы;
		for (int i=0;i<СТРОПЫ.Count;i++)
		{
			СТРОПЫ[i].Стропа.SetActive(false);
		}
		СТРОПЫ[stropa_num].Стропа.SetActive(true);
		
		for (int i=0;i<ГРУЗЫ[o].uzels.Count;i++)
		{
			//ставим крюк у стропы на координаты точки зацепа
			//в настройках JOIN связываем....
			СТРОПЫ[stropa_num].Крюки[i].transform.parent = ГРУЗЫ[o].uzels[i].transform.parent;
			СТРОПЫ[stropa_num].Крюки[i].transform.localPosition = ГРУЗЫ[o].uzels[i].transform.localPosition;
			
			//Rigidbody A = СТРОПЫ[stropa_num].Крюки[i].GetComponent<Rigidbody>();
			//A.MovePosition(ГРУЗЫ[o].uzels[i].transform.localPosition);
			
			Rigidbody J = ГРУЗЫ[o].uzels[i].GetComponent<Rigidbody>();
			CharacterJoint X = СТРОПЫ[stropa_num].Крюки[i].GetComponent<CharacterJoint>();
			X.connectedBody = J;
			СТРОПЫ[stropa_num].НулевыеМассы[i].isKinematic = true;
			
		}
		
		
		
	}
	public void ОТВЯЗАТЬ_ГРУЗ()
	{
		int o = GetIndexNearGruz();
		if (o==-1) return;
		
		if (ГРУЗЫ[o].Застрапован==false) return;
		ГРУЗЫ[o].Застрапован=false;
		Debug.Log ("ОТВЯЗАТЬ...");
		
		//current_gruz_for_stropmens=null;
		//StropMenAnim1.Play("IDLE", -1, 0);
		//StropMen1.isStopped = true;
		//StropMenAnim2.Play("IDLE", -1, 0);
		//StropMen2.isStopped = true;
		
		//StropForRelax=o;
		//TimeRelax=3f;
		
		int stropa_num = ГРУЗЫ[o].НомерСтропы;
		
		
		for (int i=0;i<ГРУЗЫ[o].uzels.Count;i++)
		{
			СТРОПЫ[stropa_num].Крюки[i].transform.parent = СТРОПЫ[stropa_num].ParentКрюки[i].transform;
			
			СТРОПЫ[stropa_num].Крюки[i].transform.localPosition = СТРОПЫ[stropa_num].КрюкиСтартовоеПоложение[i].transform.localPosition;
			//СТРОПЫ[stropa_num].НулевыеМассы[i].transform.localPosition = СТРОПЫ[stropa_num].КрюкиСтартовоеПоложение[i].transform.localPosition;
			
			Rigidbody A = СТРОПЫ[stropa_num].Крюки[i].GetComponent<Rigidbody>();
			
			A.velocity = Vector3.zero;
			A.angularVelocity = Vector3.zero;
			
			CharacterJoint X = СТРОПЫ[stropa_num].Крюки[i].GetComponent<CharacterJoint>();
			X.connectedBody = СТРОПЫ[stropa_num].НулевыеМассы[i];
			СТРОПЫ[stropa_num].НулевыеМассы[i].isKinematic = false;
			
			/*
			for (int q=0;q<СТРОПЫ[stropa_num].Тросы.Count;q++)
			{
				//СТРОПЫ[stropa_num].Тросы[q].Clear();
				СТРОПЫ[stropa_num].Тросы[q].Setup();
			}
			*/
			
			//СТРОПЫ[stropa_num].Крюки[i].transform.localPosition = СТРОПЫ[stropa_num].КрюкиСтартовоеПоложение[i].transform.localPosition;
			A.velocity = Vector3.zero;
			A.angularVelocity = Vector3.zero;
			
		}
		
		
	}
	
	
	private int GetIndexNearGruz ()
	{
		for (int i = 0; i < ГРУЗЫ.Count ; i++)
		{
			//проверяем расстояние от крюка
			float distance = Vector3.Distance (ГРУЗЫ[i].КоординатыГруза.transform.position, Hook.transform.position);
			if (distance<=ГРУЗЫ[i].РасстояниеАктивации)
			{
				return i;
			}
		}
		return -1;
	}
	
}
