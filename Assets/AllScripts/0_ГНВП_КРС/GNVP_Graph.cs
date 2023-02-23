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

public class GNVP_Graph : MonoBehaviour 
{

	public GameObject mainObj;
	
	public List<List<float>> FFT;

    private int lengthOfLineRenderer = 125;
	
	private List <GameObject> channels;
	private List <Color> colors;
	private float timer;
	int currentT;
	float timeInterval=1f;
	
	public void SetTimeInterval(float timeinterval)
	{
		timeInterval=timeinterval;
	}
	
	public void AddPoint(float Pзатруб, float Pнкт, float Pца, float Pmin, float Pmax, float DrosselOpen, float Pпласта, float Pfact)
	{
		//if (gameObject.activeSelf==false) return;
		
		timer += Time.deltaTime;
		if (timer>timeInterval)
		{
			timer=0;
			currentT++;
			if (currentT>=lengthOfLineRenderer)
			{
				currentT=0;
			}
			
			FFT[0][currentT] = -3.85f + Pзатруб * 10.14f / Pmax;  
			FFT[1][currentT] = -3.85f + Pнкт * 10.14f / Pmax;
			FFT[2][currentT] = -3.85f + Pца * 10.14f / Pmax;
			FFT[3][currentT] = -3.85f + Pmin * 10.14f / Pmax;
			FFT[4][currentT] = -3.85f + Pmax * 10.14f / Pmax;
			FFT[5][currentT] = -3.85f + DrosselOpen * 10.14f / Pmax;
			FFT[6][currentT] = -3.85f + Pпласта * 10.14f / Pmax;
			FFT[7][currentT] = -3.85f + Pfact * 10.14f / Pmax;
			
			
			
			for (int i = 0; i < lengthOfLineRenderer; i++)
			{
				FFT[3][i] = FFT[3][currentT];
				FFT[4][i] = FFT[4][currentT];
				FFT[6][i] = FFT[6][currentT];
			}
			
			
		}
	}
	
	
    private void Update() 
	{
		//return;
		
			for (int z=0;z<8;z++)
			{
				LineRenderer lineRenderer = channels[z].GetComponent<LineRenderer>();
				for (int i = 0; i < lengthOfLineRenderer; i++)
				{
					float iii = FFT[z][i];
					lineRenderer.SetPosition(i, new Vector3(i * 0.086f, iii, 0.0f));
				}
			}
		
    }

    

    void Start()
    {
		
		
		timer = 0;
		currentT=0;
		
		FFT = new List<List<float>>();
	
		for (int y=0; y <8; y++)
		{
			List<float> temp = new List<float>();
			for (int x=0; x <lengthOfLineRenderer; x++)
			{
				temp.Add(-3.85f);
			}
			FFT.Add (temp);
		}
		
		
		
		channels = new List <GameObject>();
		colors = new List <Color>();
		
		colors.Add (Color.black);
		colors.Add (Color.gray);
		colors.Add (Color.yellow);
		colors.Add (Color.blue);
		colors.Add (Color.cyan);
		colors.Add (Color.green);
		colors.Add (Color.magenta);
		colors.Add (Color.red);
			
		for (int i=0;i<8 ; i++)
		{
			GameObject go = new GameObject(i.ToString("N0"));
			go.transform.parent = gameObject.transform;
			go.transform.localPosition = new Vector3(15f, 343f, 0f);
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
		
		
		mainObj.SetActive(false);
        
    }


   

}
