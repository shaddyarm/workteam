using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.Events;

public class ShowHideScript : MonoBehaviour {

	public GameObject MY;
	
	public bool showhide;
	
	
	
	
	
    public void Change()
    {
		showhide = !showhide;
		MY.SetActive(showhide);
	}
}

	
	