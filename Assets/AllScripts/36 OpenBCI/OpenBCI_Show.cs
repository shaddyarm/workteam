using System;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;



public class OpenBCI_Show : MonoBehaviour  
{
	public List <TMP_Text> texts;
	public List <Slider> sliders;
	private List <float> maximums;
	private List <float> values;
	
	public OpenBCI_UDP OpenBCI;
	
	
	void Start()
	{
		maximums = new List<float>();
		maximums.Add(0);
		maximums.Add(0);
		maximums.Add(0);
		maximums.Add(0);
		maximums.Add(0);
		
		values = new List<float>();
		values.Add(0);
		values.Add(0);
		values.Add(0);
		values.Add(0);
		values.Add(0);
	}

	void Update () 
	{         
		if (OpenBCI.Get()==true)
		{
			values[0] = OpenBCI.Delta;
			values[1] = OpenBCI.Theta;
			values[2] = OpenBCI.Alpha;
			values[3] = OpenBCI.Beta;
			values[4] = OpenBCI.Gamma;
			
			for (int i=0;i<=4;i++)
			{
				if (values[i] > maximums[i])
				{
					sliders[i].maxValue  = values[i];
					maximums[i] = values[i];
				} 
				sliders[i].value = values[i];
				texts[i].text = values[i].ToString("N6");
			}
		}
	}
	
	
}
 