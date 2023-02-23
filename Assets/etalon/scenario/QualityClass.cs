using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;



public class QualityClass : MonoBehaviour 
{
		public Toggle toggle;
	
		void Start () 
		{
			if (PlayerPrefs.HasKey("SavedQualityInteger"))
			{
				int q = PlayerPrefs.GetInt("SavedQualityInteger");
				QualitySettings.SetQualityLevel(q, true);
				
				if (q==0)
				{
					toggle.isOn=false;
				}
				else
				{
					toggle.isOn=true;
				}
			}
		
			
		}

		public void Change(bool value)
		{
			int q=0;
			if (value==true)
			{
				q=5;
				Debug.Log("Quality settings set to 'Fantastic'");
			}
			else
			{
				q=0;
				Debug.Log("Quality settings set to 'Fastest'");
			}
			
			QualitySettings.SetQualityLevel(q, true);
			PlayerPrefs.SetInt("SavedQualityInteger", q);
			PlayerPrefs.Save();
		}

	
}