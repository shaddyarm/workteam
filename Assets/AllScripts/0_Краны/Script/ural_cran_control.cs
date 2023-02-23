using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;


public class ural_cran_control : MonoBehaviour 
{

	public NavMeshAgent StropMen1;
	public Animator StropMenAnim1;
	
	public NavMeshAgent StropMen2;
	public Animator StropMenAnim2;
	
	private CranGruz current_gruz_for_stropmens = null;
	
    public float speedWinch = 100f;
	public float speed_Strela_lenght = 10f;
	public float speed_Strela_up_down = 10f;
	public float speed_Strela_left_right = 10f;
	
	public GameObject CranBase;
	public GameObject Strela1;
	public GameObject Strela2;
	public GameObject Strela3;
	
	public GameObject Winch;
	public Material TrosWinch;
	
	
	public float Winch_value=0;
	
	public float Strela_lenght_value=0;
	public float Strela_updown_value=0;
	public float Strela_left_right_value=0;
	
	
	public Material Tros;
	public GameObject RolikUp;
	public GameObject RolikDown;
	
	public GameObject Eggs_rotate;
	public GameObject Eggs;
	public GameObject RolikEgg;
	
	
	public GameObject HOOK2;
	public GameObject HOOK_CenterRotate;
	
	//float TimeRelax=0;
	//int StropForRelax=0;
	
	
	public GameObject R5;
	public GameObject R1;
	public GameObject R2;
	public GameObject R3;
	public GameObject R4;
	
	//Аутриггеры
	float Auttriggers_nogi=0;
	float Auttriggers_move_nogi =0; //-1 ..0 .. 1
	public GameObject Auttriggers_noga_3;
	public GameObject Auttriggers_noga_4;
	
	public GameObject Auttriggers_noga_1;
	public GameObject Auttriggers_noga_2;
	
	//3й
	float Auttrigger_cylinder3=0;
	float Auttrigger_move_cylinder3 =0;//-1 ..0 .. 1
	public GameObject Auttriggers_cylinder_3;
	//1й
	float Auttrigger_cylinder1=0;
	float Auttrigger_move_cylinder1 =0;//-1 ..0 .. 1
	public GameObject Auttriggers_cylinder_1;
	
	//4й
	float Auttrigger_cylinder4=0;
	float Auttrigger_move_cylinder4 =0;//-1 ..0 .. 1
	public GameObject Auttriggers_cylinder_4;
	//2й
	float Auttrigger_cylinder2=0;
	float Auttrigger_move_cylinder2 =0;//-1 ..0 .. 1
	public GameObject Auttriggers_cylinder_2;
	
	
	public GameObject URAL;
	public GameObject KABINA;
	public Transform target_KABINA;
	public Transform target_2;
	public Transform target_3;
	public Transform target_4;
	public Transform target_5;
	
	public Play player;
	public GameObject player_camera;
	public GameObject ArrowIn;
	public GameObject ArrowOut;
	public GameObject ArrowDown;
	public GameObject ArrowUp;
	
	public Filo.Cable cable;
	private Filo.Cable.Link link;
	private bool link_hide=false;
	
	public GameObject JoysticLeft;
	public GameObject JoysticRight;
	
	
	public GameObject ArrowStropToRoga;
	public GameObject ArrowStropFromRoga;
	
	
	public GameObject StropaNull;
	public GameObject StropaRoga;
	
	public List<CranStrop> СТРОПЫ;
	public List<CranGruz> ГРУЗЫ;
	
	
	public AudioSource SoundWitch;
	public AudioSource SoundCylinder;
	public AudioSource SoundCabinRotate;
	public AudioSource SoundStrelaLenght;
	public AudioSource SoundAuttriggers;
	public AudioSource SoundBeep;
	
	public GameObject StropOff;
	public GameObject StropOn;
	
	public Scenario_Trigger trigger1;
	
	
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
		
		link = cable.links[1];
		link_hide=false;
		
		StartCoroutine(CheckForControllers());
		StartCoroutine(HideStrops());
    }
	
	
	// Use this for initialization
	void Start () 
	{
		TrosWinch.SetTextureOffset("_MainTex", new Vector2(0, 0));
		Tros.SetTextureScale("_MainTex", new Vector2(1f, 1f));
		ArrowIn.SetActive(false);
		ArrowOut.SetActive(false);
		ArrowDown.SetActive(false);
		ArrowStropToRoga.SetActive(false);
		
		
		
		
		StropMen1.autoBraking = true;
		StropMen1.isStopped = true;
		StropMen1.stoppingDistance = 1f;
		StropMenAnim1.Play("IDLE", -1, 0);
		
		StropMen2.autoBraking = true;
		StropMen2.isStopped = true;
		StropMen2.stoppingDistance = 1f;
		StropMenAnim2.Play("IDLE", -1, 0);
	}
	
	// Update is called once per frame
	void Update () 
	{
		//смотрим, если расстояние до текущего груза стало больше 100, то идем туда
		if (current_gruz_for_stropmens!=null)
		{
			{
				//расстояние до стропальщика1
				float dist1 = Vector3.Distance (current_gruz_for_stropmens.ПозицияСтропальщика1.transform.position, StropMen1.gameObject.transform.position);
				if (dist1>2f)
				{
					Vector3 myVector1 = new Vector3(current_gruz_for_stropmens.ПозицияСтропальщика1.transform.position.x, 0, current_gruz_for_stropmens.ПозицияСтропальщика1.transform.position.z);
					StropMen1.destination = myVector1;
					if (StropMen1.isStopped == true)
					{
						StropMenAnim1.Play("WALK", -1, 0);
						NavMeshPath navMeshPath = new NavMeshPath();
						StropMen1.CalculatePath(current_gruz_for_stropmens.ПозицияСтропальщика1.transform.position, navMeshPath);
						StropMen1.destination = current_gruz_for_stropmens.ПозицияСтропальщика1.transform.position;
						StropMen1.isStopped = false;
					}
					
					if (StropMen1.path.status == NavMeshPathStatus.PathInvalid || StropMen1.path.status == NavMeshPathStatus.PathPartial) 
					{
						Debug.Log("WRONG WAY");
						StropMenAnim1.Play("IDLE", -1, 0);
						StropMen1.isStopped = true;
					}
					
				}
				else
				{
					//если мы близко, стои, курим
					StropMenAnim1.Play("IDLE", -1, 0);
					StropMen1.isStopped = true;
				}
			}
			{
				//расстояние до стропальщика2
				float dist2 = Vector3.Distance (current_gruz_for_stropmens.ПозицияСтропальщика2.transform.position, StropMen2.gameObject.transform.position);
				if (dist2>2f)
				{
					Vector3 myVector2 = new Vector3(current_gruz_for_stropmens.ПозицияСтропальщика2.transform.position.x, 0, current_gruz_for_stropmens.ПозицияСтропальщика2.transform.position.z);
					StropMen2.destination = myVector2;
					if (StropMen2.isStopped == true)
					{
						StropMenAnim2.Play("WALK", -1, 0);
						NavMeshPath navMeshPath = new NavMeshPath();
						StropMen2.CalculatePath(current_gruz_for_stropmens.ПозицияСтропальщика2.transform.position, navMeshPath);
						StropMen2.destination = current_gruz_for_stropmens.ПозицияСтропальщика2.transform.position;
						StropMen2.isStopped = false;
					}
					
					if (StropMen2.path.status == NavMeshPathStatus.PathInvalid || StropMen2.path.status == NavMeshPathStatus.PathPartial) 
					{
						Debug.Log("WRONG WAY");
						StropMenAnim2.Play("IDLE", -1, 0);
						StropMen2.isStopped = true;
					}
					
				}
				else
				{
					//если мы близко, стои, курим
					StropMenAnim2.Play("IDLE", -1, 0);
					StropMen2.isStopped = true;
				}
			}
		}

				
		/*
		if (TimeRelax>0)
		{
			TimeRelax-=Time.deltaTime;
			for (int i=0;i<СТРОПЫ[StropForRelax].Крюки.Count;i++)
			{
				СТРОПЫ[StropForRelax].Крюки[i].transform.localPosition = СТРОПЫ[StropForRelax].КрюкиСтартовоеПоложение[i].transform.localPosition;
				Rigidbody A = СТРОПЫ[StropForRelax].Крюки[i].GetComponent<Rigidbody>();
				
				A.velocity = Vector3.zero;
				A.angularVelocity = Vector3.zero;
			}
		}
		*/
		
		//Debug.Log (Input.GetAxis("Left_beep"));
		
		if (Input.GetKey(KeyCode.Escape)||(Input.GetAxis("Left_beep")!=0))
		{
			if (SoundBeep.isPlaying==false)	SoundBeep.Play();
		}
	
		
		
		
		//грузы
		foreach (CranGruz gruz in ГРУЗЫ)
		{
			//проверяем расстояние от крюка
			float distance2 = Vector3.Distance (gruz.КоординатыГруза.transform.position, RolikEgg.transform.position);
			if (distance2<=gruz.РасстояниеАктивации)
			{
				//запоминаем этот груз для стропальщиков
				current_gruz_for_stropmens = gruz;
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
		
		
		
		bool EggCheck=true;
		
		float distanceEgg = Vector3.Distance (Eggs.transform.position, RolikEgg.transform.position);
		if(distanceEgg<0.3f) EggCheck=false;
		
    

		//Поднять/опустить
		bool soundForWitch = false;
		float Winch_value_delta = Time.deltaTime * speedWinch;
		float Winch_value_joy = 0;
        if (Input.GetKey(KeyCode.F1))
		{
			Winch_value_joy = 1f;
			//if (EggCheck==true) 
			{
				Winch_value += Winch_value_delta;
				soundForWitch=true;
			}	
        }
		if (Input.GetKey(KeyCode.F2))
		{
			Winch_value_joy = -1f;
			if (EggCheck==true) 
			{
				Winch_value -= Winch_value_delta;
				soundForWitch=true;
			}
        }
		
		
		if ((soundForWitch==true)&&(SoundWitch.isPlaying==false))
		{
			//public AudioSource SoundCabinRotate;
			//public AudioSource SoundStrelaLenght;
			//public AudioSource Auttriggers;
			SoundWitch.Play();
		}
		if ((soundForWitch==false)&&(SoundWitch.isPlaying==true))
		{
			SoundWitch.Pause();
		}
		
		
		//if (Winch_value<0) Winch_value=0;
		
		
		
		Filo.Cable.Link WINCH = cable.links[0];
		WINCH.storedCable= 50 - Winch_value/100f;
		cable.links[0] = WINCH;
		cable.Setup();
		
		TrosWinch.SetTextureOffset("_MainTex", new Vector2(-Winch_value/100f,0));
		
		
		//Поднять-опустить стрелу
		bool SoundForCylinder=false;
		float Strela_updown_value_delta = Time.deltaTime * speed_Strela_up_down;
		float Strela_updown_value_joy = 0;
		if (Input.GetKey(KeyCode.F5))
		{
			Strela_updown_value_joy=1f;
			Strela_updown_value += Strela_updown_value_delta;
			SoundForCylinder=true;
        }
		if (Input.GetKey(KeyCode.F6))
		{
			Strela_updown_value_joy=-1f;
			Strela_updown_value -= Strela_updown_value_delta;
			SoundForCylinder=true;
        }
		if (Strela_updown_value<0) Strela_updown_value=0;
		if (Strela_updown_value>78f) Strela_updown_value=78f;
		
		if ((link_hide==false)&&(Strela_updown_value>23.09682f))
		{
			link_hide=true;
			cable.links.RemoveAt(1);
			cable.Setup();
		}
		
		if ((link_hide==true)&&(Strela_updown_value<23.09682f))
		{
			link_hide=false;
			cable.links.Insert(1, link);	
			cable.Setup();
		}
		//
		if ((SoundForCylinder==true)&&(SoundCylinder.isPlaying==false))
		{
			SoundCylinder.Play();
		}
		if ((SoundForCylinder==false)&&(SoundCylinder.isPlaying==true))
		{
			SoundCylinder.Pause();
		}
		//
		JoysticRight.transform.localRotation = Quaternion.Euler( Winch_value_joy * 20f, 0, Strela_updown_value_joy*10f);
		
		
		
		
		
		//Выдвинуть/задвинуть стрелу
		bool soundForLenght = false;
		float Strela_lenght_value_delta = Time.deltaTime * speed_Strela_lenght / 1000f;
		float Strela_lenght_value_joy = 0;
		
		//joy
		Strela_lenght_value_joy = Input.GetAxis("Left_ForwardBack");
		if (Strela_lenght_value_joy!=0)
		{
			if ((EggCheck==true)||(Strela_lenght_value_joy<0)) 
			{
			Strela_lenght_value += Strela_lenght_value_delta * Strela_lenght_value_joy ;
			soundForLenght=true;
			}
		}
		//joy
		
		if (Input.GetKey(KeyCode.F3))
		{
			Strela_lenght_value_joy = 1f;
			if (EggCheck==true) 
			{
				Strela_lenght_value += Strela_lenght_value_delta;
				soundForLenght=true;
			}
        }
		if (Input.GetKey(KeyCode.F4))
		{
			Strela_lenght_value_joy = -1f;
			Strela_lenght_value -= Strela_lenght_value_delta;
			soundForLenght=true;
        }
		if (Strela_lenght_value<0) Strela_lenght_value=0;
		if (Strela_lenght_value>1f) Strela_lenght_value=1f;
		
		if ((soundForLenght==true)&&(SoundStrelaLenght.isPlaying==false))
		{
			//public AudioSource SoundCabinRotate;
			//public AudioSource Auttriggers;
			SoundStrelaLenght.Play();
		}
		if ((soundForLenght==false)&&(SoundStrelaLenght.isPlaying==true))
		{
			SoundStrelaLenght.Pause();
		}
		
		
		
		//Влево-вправо
		bool soundForRotate=false;
		float Strela_left_right_delta = Time.deltaTime * speed_Strela_left_right;
		float Strela_left_right_joy = 0;
		
		//joy
		Strela_left_right_joy = Input.GetAxis("Left_LeftRight");
		if (Strela_left_right_joy!=0)
		{
			Strela_left_right_value -= Strela_left_right_delta * Strela_left_right_joy ;
			soundForRotate=true;
		}
		//joy
	
		
		if (Input.GetKey(KeyCode.F8))
		{
			Strela_left_right_joy = -1f;
			Strela_left_right_value += Strela_left_right_delta ;
			soundForRotate=true;
        }
		if (Input.GetKey(KeyCode.F7))
		{
			Strela_left_right_joy = 1f;
			Strela_left_right_value -= Strela_left_right_delta;
			soundForRotate=true;
        }
		
		if ((soundForRotate==true)&&(SoundCabinRotate.isPlaying==false))
		{
			//public AudioSource Auttriggers;
			SoundCabinRotate.Play();
		}
		if ((soundForRotate==false)&&(SoundCabinRotate.isPlaying==true))
		{
			SoundCabinRotate.Pause();
		}
		
		
		//if (Strela_left_right_value<35) Strela_left_right_value=35;
		//if (Strela_left_right_value>325f) Strela_left_right_value=325f;
		//
		JoysticLeft.transform.localRotation = Quaternion.Euler( Strela_lenght_value_joy * 20f, 0, Strela_left_right_joy*10f);
		
		Winch.transform.localRotation = Quaternion.Euler(0, -90f, Winch_value);
		Strela2.transform.localPosition = new Vector3(0, 0, -Strela_lenght_value*20f * 0.7326387f);
        Strela3.transform.localPosition = new Vector3(0, 0, -Strela_lenght_value*20f * 0.7326387f);
		Strela1.transform.localRotation = Quaternion.Euler(-180f  - Strela_updown_value , 0, 0);
		CranBase.transform.localRotation = Quaternion.Euler(0,Strela_left_right_value , 0);
		//канат
		float distance = Vector3.Distance (RolikUp.transform.position, RolikDown.transform.position);
		Tros.SetTextureScale("_MainTex", new Vector2(1f, 0.01f+ distance/2f));
		
		
		
		
		
		
		//Аутриггеры ноги
		Auttriggers_nogi +=Auttriggers_move_nogi * Time.deltaTime / 10f;
		if (Auttriggers_nogi<0) Auttriggers_nogi=0;
		if (Auttriggers_nogi>1f) Auttriggers_nogi=1f;
		Auttriggers_noga_3.transform.localPosition = new Vector3(5.372f - Auttriggers_nogi*5.372f, 0, 0);
		Auttriggers_noga_3.transform.localRotation = Quaternion.Euler(0,0 , -4f * Auttriggers_nogi);
		Auttriggers_noga_1.transform.localPosition = new Vector3(5.372f - Auttriggers_nogi*5.372f, 0, 0);
		Auttriggers_noga_1.transform.localRotation = Quaternion.Euler(0,0 , -4f * Auttriggers_nogi);
		
		Auttriggers_noga_4.transform.localPosition = new Vector3(- Auttriggers_nogi*5.372f, 0, 0);
		Auttriggers_noga_4.transform.localRotation = Quaternion.Euler(0,0 , -4f * Auttriggers_nogi);
		Auttriggers_noga_2.transform.localPosition = new Vector3(- Auttriggers_nogi*5.372f, 0, 0);
		Auttriggers_noga_2.transform.localRotation = Quaternion.Euler(0,0 , -4f * Auttriggers_nogi);
		
		float k=1.1f;
		//3й
		Auttrigger_cylinder3 +=Auttrigger_move_cylinder3 * Time.deltaTime / 10f * k;
		if (Auttrigger_cylinder3<0) Auttrigger_cylinder3=0;
		if (Auttrigger_cylinder3>1f * k) Auttrigger_cylinder3=1f * k;
		Auttriggers_cylinder_3.transform.localPosition = new Vector3(0, 0, -1.036f * Auttrigger_cylinder3);
		//1й
		Auttrigger_cylinder1 +=Auttrigger_move_cylinder1 * Time.deltaTime / 10f * k;
		if (Auttrigger_cylinder1<0) Auttrigger_cylinder1=0;
		if (Auttrigger_cylinder1>1f* k) Auttrigger_cylinder1=1f* k;
		Auttriggers_cylinder_1.transform.localPosition = new Vector3(0, 0, -1.036f * Auttrigger_cylinder1);
		
		//4й
		Auttrigger_cylinder4 +=Auttrigger_move_cylinder4 * Time.deltaTime / 10f * k;
		if (Auttrigger_cylinder4<0) Auttrigger_cylinder4=0;
		if (Auttrigger_cylinder4>1f* k) Auttrigger_cylinder4=1f* k;
		Auttriggers_cylinder_4.transform.localPosition = new Vector3(0, 0, -1.036f * Auttrigger_cylinder4);
		//2й
		Auttrigger_cylinder2 +=Auttrigger_move_cylinder2 * Time.deltaTime / 10f * k;
		if (Auttrigger_cylinder2<0) Auttrigger_cylinder2=0;
		if (Auttrigger_cylinder2>1f* k) Auttrigger_cylinder2=1f* k;
		Auttriggers_cylinder_2.transform.localPosition = new Vector3(0, 0, -1.036f * Auttrigger_cylinder2);
		
		float mimin = Mathf.Min(Auttrigger_cylinder1, Auttrigger_cylinder2);
		mimin = Mathf.Min(mimin, Auttrigger_cylinder3);
		mimin = Mathf.Min(mimin, Auttrigger_cylinder4);
		
		if (mimin>0.9f)
		{
			URAL.transform.localPosition = new Vector3(URAL.transform.localPosition.x, 3.85f + 0.1f*(mimin-0.9f) , URAL.transform.localPosition.z);
		}
		else
		{
			URAL.transform.localPosition = new Vector3(URAL.transform.localPosition.x, 3.85f, URAL.transform.localPosition.z);
		}
		
		if ((mimin>=0.99f)&&(Auttriggers_move_nogi>=0.99f))
		{
			trigger1.On();
		}
		
		
		
		//Крюк
		HOOK_CenterRotate.transform.localRotation = Quaternion.Euler(Strela_updown_value,0 , 0);
		HOOK2.transform.localPosition = new Vector3(HOOK2.transform.localPosition.x ,3f + Winch_value/80f - Strela_lenght_value*10f, HOOK2.transform.localPosition.z);
		
		Eggs_rotate.transform.localRotation = Quaternion.Euler(-1.959f,Strela_updown_value-2.5f , 0.019f);
		
		
	}
	
	
	
	
	
	
	
	//ноги аутриггеров
	public void Autrigger5up()
	{
		R5.transform.localRotation = Quaternion.Euler(12f,0 , 0);
		Auttriggers_move_nogi = -1f;
		SoundAuttriggers.Play();
	}
	public void Autrigger5idle()
	{
		R5.transform.localRotation = Quaternion.Euler(0,0 , 0);
		Auttriggers_move_nogi=0;
		SoundAuttriggers.Pause();
	}
	public void Autrigger5down()
	{
		R5.transform.localRotation = Quaternion.Euler(-12f,0 , 0);
		Auttriggers_move_nogi=1f;
		SoundAuttriggers.Play();
	}
	
	
	
	public void Autrigger1up()
	{
		R1.transform.localRotation = Quaternion.Euler(12f,0 , 0);
		Auttrigger_move_cylinder1 =-1f;
		SoundAuttriggers.Play();
	}
	public void Autrigger1idle()
	{
		R1.transform.localRotation = Quaternion.Euler(0,0 , 0);
		Auttrigger_move_cylinder1 =0;
		SoundAuttriggers.Pause();
	}
	public void Autrigger1down()
	{
		R1.transform.localRotation = Quaternion.Euler(-12f,0 , 0);
		Auttrigger_move_cylinder1 =1f;
		SoundAuttriggers.Play();
	}
	
	public void Autrigger2up()
	{
		R2.transform.localRotation = Quaternion.Euler(12f,0 , 0);
		Auttrigger_move_cylinder2 =-1f;
		SoundAuttriggers.Play();
	}
	public void Autrigger2idle()
	{
		R2.transform.localRotation = Quaternion.Euler(0,0 , 0);
		Auttrigger_move_cylinder2 =0;
		SoundAuttriggers.Pause();
	}
	public void Autrigger2down()
	{
		R2.transform.localRotation = Quaternion.Euler(-12f,0 , 0);
		Auttrigger_move_cylinder2 =1f;
		SoundAuttriggers.Play();
	}
	
	public void Autrigger3up()
	{
		R3.transform.localRotation = Quaternion.Euler(12f,0 , 0);
		Auttrigger_move_cylinder3 =-1f;
		SoundAuttriggers.Play();
	}
	public void Autrigger3idle()
	{
		R3.transform.localRotation = Quaternion.Euler(0,0 , 0);
		Auttrigger_move_cylinder3 =0f;
		SoundAuttriggers.Pause();
	}
	public void Autrigger3down()
	{
		R3.transform.localRotation = Quaternion.Euler(-12f,0 , 0);
		Auttrigger_move_cylinder3 =1f;
		SoundAuttriggers.Play();
	}
	
	public void Autrigger4up()
	{
		R4.transform.localRotation = Quaternion.Euler(12f,0 , 0);
		Auttrigger_move_cylinder4 =-1f;
		SoundAuttriggers.Play();
	}
	public void Autrigger4idle()
	{
		R4.transform.localRotation = Quaternion.Euler(0,0 , 0);
		Auttrigger_move_cylinder4 =0;
		SoundAuttriggers.Pause();
	}
	public void Autrigger4down()
	{
		R4.transform.localRotation = Quaternion.Euler(-12f,0 , 0);
		Auttrigger_move_cylinder4 =1f;
		SoundAuttriggers.Play();
	}
	
	public void GoInCabin()
	{
		//player;
		ArrowIn.SetActive(false);
		ArrowOut.SetActive(true);
		ArrowDown.SetActive(false);
		
		player.transform.parent = KABINA.transform;
		player.gameObject.transform.localPosition = target_KABINA.localPosition;
		player.gameObject.transform.localRotation = target_KABINA.localRotation;
		//player_camera.transform.localRotation = Quaternion.Euler(9.771001f , 0, 0);
	}
	
	public void GoOutOffCabin()
	{
		ArrowIn.SetActive(true);
		ArrowOut.SetActive(false);
		ArrowDown.SetActive(true);
		
		player.transform.parent = URAL.transform;
		player.gameObject.transform.localPosition = target_2.localPosition;
		player.gameObject.transform.localRotation = target_2.localRotation;
	}
	
	public void GoDown()
	{
		ArrowDown.SetActive(false);
		ArrowUp.SetActive(true);
		ArrowIn.SetActive(false);
		
		player.transform.parent = URAL.transform;
		player.gameObject.transform.localPosition = target_3.localPosition;
		player.gameObject.transform.localRotation = target_3.localRotation;
	}
	public void GoUp()
	{
		ArrowDown.SetActive(true);
		ArrowUp.SetActive(false);
		ArrowIn.SetActive(true);
		
		player.transform.parent = URAL.transform;
		player.gameObject.transform.localPosition = target_2.localPosition;
		player.gameObject.transform.localRotation = target_2.localRotation;
	}
	
	public void GoInUralCabin()
	{
		player.transform.parent = URAL.transform;
		player.gameObject.transform.localPosition = target_5.localPosition;
		player.gameObject.transform.localRotation = target_5.localRotation;	
	}
	public void GoOutUralCabin()
	{
		player.transform.parent = URAL.transform;
		player.gameObject.transform.localPosition = target_4.localPosition;
		player.gameObject.transform.localRotation = target_4.localRotation;
	}
	
	public void StropToRoga()
	{
		ArrowStropFromRoga.SetActive(false);
		ArrowStropToRoga.SetActive(true);
		
		StropaNull.SetActive(true);
		StropaRoga.SetActive(false);
	}
	
	public void StropFromRoga()
	{
		float distance = Vector3.Distance (ArrowStropToRoga.transform.position, RolikEgg.transform.position);
		Debug.Log(distance);
		if(distance>1.1f) return;
		
		
		ArrowStropToRoga.SetActive(false);
		ArrowStropFromRoga.SetActive(true);
	
		
		StropaNull.SetActive(false);
		StropaRoga.SetActive(true);
	}
	
	public void ПРИВЯЗАТЬ_ГРУЗ()
	{
		
		
		int o = GetIndexNearGruz();
		if (o==-1) return;
		if (ГРУЗЫ[o].Застрапован==true) return;
		
		
		
		
		ГРУЗЫ[o].Застрапован=true;
		Debug.Log ("ПРИВЯЗАТЬ...");
		
		StropaNull.SetActive(false);
		StropaRoga.SetActive(false);
		
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
		
		current_gruz_for_stropmens=null;
		StropMenAnim1.Play("IDLE", -1, 0);
		StropMen1.isStopped = true;
		StropMenAnim2.Play("IDLE", -1, 0);
		StropMen2.isStopped = true;
		
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
			float distance = Vector3.Distance (ГРУЗЫ[i].КоординатыГруза.transform.position, RolikEgg.transform.position);
			if (distance<=ГРУЗЫ[i].РасстояниеАктивации)
			{
				return i;
			}
		}
		return -1;
	}
	
		
	
	
}
