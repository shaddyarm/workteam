/* 
    ------------------- Code Monkey -------------------

    Thank you for downloading this package
    I hope you find it useful in your projects
    If you have any questions let me know
    Cheers!

               unitycodemonkey.com
    --------------------------------------------------
 */

using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class Window_Graph : MonoBehaviour 
{
	public OpenBCI_UDP OpenBCI;
	

    private int lengthOfLineRenderer = 125;
	
	private List <GameObject> channels;
	private List <Color> colors;
	
	
    private void Update() 
	{
		//return;
		
		if (OpenBCI.Get()==true)
		{
			for (int z=0;z<8;z++)
			{
				LineRenderer lineRenderer = channels[z].GetComponent<LineRenderer>();
				for (int i = 0; i < lengthOfLineRenderer; i++)
				{
					lineRenderer.SetPosition(i, new Vector3(i * 0.086f, OpenBCI.FFT[z][i] / 3f - 4.04f, 0.0f));
				}
			}
		}
    }

    

    void Start()
    {
		channels = new List <GameObject>();
		colors = new List <Color>();
		
		colors.Add (Color.black);
		colors.Add (Color.red);
		colors.Add (Color.yellow);
		colors.Add (Color.blue);
		colors.Add (Color.cyan);
		colors.Add (Color.green);
		colors.Add (Color.magenta);
		colors.Add (Color.gray);
			
		for (int i=0;i<8 ; i++)
		{
			GameObject go = new GameObject(i.ToString("N0"));
			go.transform.parent = gameObject.transform;
			go.transform.localPosition = new Vector3(15f, 343f, 10f);
			go.transform.localRotation = Quaternion.Euler(0, 0, 0);
			go.transform.localScale = new Vector3(87.91202f, 87.91202f, 87.91202f);
			//go.GetComponent<Renderer>().receiveShadows = false;
			//go.GetComponent<Renderer>().shadowCastingMode = UnityEngine.Rendering.ShadowCastingMode.Off;
			
			LineRenderer lineRenderer = go.AddComponent<LineRenderer>();
			lineRenderer.numCornerVertices = 3;
			lineRenderer.useWorldSpace = false;
			lineRenderer.material = new Material(Shader.Find("Sprites/Default"));
			lineRenderer.widthMultiplier = 0.005f;
			lineRenderer.positionCount = lengthOfLineRenderer;

			// A simple 2 color gradient with a fixed alpha of 1.0f.
			float alpha = 1.0f;
			Gradient gradient = new Gradient();
			gradient.SetKeys(
				new GradientColorKey[] { new GradientColorKey(colors[i], 0.0f), new GradientColorKey(colors[i], 1.0f) },
				new GradientAlphaKey[] { new GradientAlphaKey(alpha, 1.0f), new GradientAlphaKey(alpha, 1.0f) }
			);
			lineRenderer.colorGradient = gradient;
			
			channels.Add (go);
		}
		
		
		
        
    }


   

}
