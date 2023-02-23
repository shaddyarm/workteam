

using UnityEngine;
using UnityEngine.UI;
using System.Collections;
using System.Collections.Generic;



public class AutoHide : MonoBehaviour 
{
	void Awake()
	{
		this.gameObject.SetActive(false);
	}
}