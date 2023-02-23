using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CranScript : MonoBehaviour
{
	public GameObject hook;
	public float xx,yy,zz;
	// Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
		
		hook.transform.rotation = Quaternion.Euler(xx,hook.transform.rotation.y,hook.transform.rotation.z);
		//transform.rotation = transform.rotation*Quaternion.Euler(xx,yy,zz);        
    }
}
