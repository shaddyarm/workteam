using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class CranStrop : MonoBehaviour 
{
	public GameObject Стропа;
	//Список точек крепления
	//По этим координатам станут крбки стропы + эти точки будут указаны в JOINе
	public List <GameObject> Крюки;
	public List <GameObject> КрюкиСтартовоеПоложение;
	public List <GameObject> ParentКрюки;
	public List <Rigidbody> НулевыеМассы;
	//public List <Filo.Cable> Тросы;
	
	
	void Start () 
	{
	}
}

