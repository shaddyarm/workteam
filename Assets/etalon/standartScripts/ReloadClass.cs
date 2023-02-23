using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class ReloadClass : MonoBehaviour 
{
	public void Reload()
	{
		Application.LoadLevel(Application.loadedLevel);
	}
}
	

