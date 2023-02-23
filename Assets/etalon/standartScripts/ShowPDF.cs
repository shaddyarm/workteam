using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ShowPDF : MonoBehaviour {

	//public GameObject Methodichka;
	
	public string filename;
		
	public void Show()
	{
		//if (Methodichka.activeSelf==true)
		{
			//Application.OpenURL( Application.streamingAssetsPath + "\\pdf\\" + filename);
			Application.OpenURL(filename ); //"http://training.ranik.org/kit/ngpo1/metod.pdf"
			
		}

		
	}
	
	// Use this for initialization
	void Start () {
		
	}
	
	// Update is called once per frame
	void Update () {
		
	}
}
