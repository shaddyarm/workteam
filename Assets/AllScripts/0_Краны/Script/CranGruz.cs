using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class CranGruz : MonoBehaviour 
{
	//какую стропу активировать для данного груза (номер)
	public int НомерСтропы=0;
	
	//Список точек крепления
	//По этим координатам станут крбки стропы + эти точки будут указаны в JOINе
	public List <GameObject> uzels;
	
	//на каком расстоянии активировать снятие/подвешивание
	public float РасстояниеАктивации=4f;
	
	public bool Застрапован=false;
	
	//координаты с которых бедет считаться расстояние до крюка
	public GameObject КоординатыГруза;
	//public GameObject MY;
	
	public GameObject СтрелкаОтвязать;
	public GameObject СтрелкаПривязать;
	
	public GameObject ПозицияСтропальщика1;
	public GameObject ПозицияСтропальщика2;
	
	public float torque;
	public Rigidbody rb;
	
	void Start () 
	{
		rb = GetComponent<Rigidbody>();
	}
	
	void FixedUpdate()
	{
		if (Застрапован==false) return;
		
		if (Input.GetKey(KeyCode.O))
		{
			rb.AddTorque(transform.up * torque * 1f);
		}
		if (Input.GetKey(KeyCode.P))
		{
			rb.AddTorque(-transform.up * torque * 1f);
		}
		
	}
	
	public void ПоказатьСтрелки()
	{
		if (Застрапован) 
		{
			СтрелкаОтвязать.SetActive(true);
			СтрелкаПривязать.SetActive(false);
			
		}
		else
		{
			СтрелкаПривязать.SetActive(true);
			СтрелкаОтвязать.SetActive(false);
		}
		
	}
	public void СпрятатьСтрелки()
	{
		СтрелкаОтвязать.SetActive(false);
		СтрелкаПривязать.SetActive(false);
	}
	
	
	
}

