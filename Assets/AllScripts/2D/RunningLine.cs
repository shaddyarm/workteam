using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RunningLine : MonoBehaviour
{
	public Material material;
	
	public float speed=1f;
	
    // Start is called before the first frame update
    void Start()
    {
       
    }

    // Update is called once per frame
    void Update()
    {
		float offset = Time.time * 2.0f *speed;
        material.SetTextureOffset("_MainTex", new Vector2(offset, 0));
    }
	
	void OnDestroy()
	{
		material.SetTextureOffset("_MainTex", new Vector2(0, 0));
	}
}
