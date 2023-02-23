using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LookAtScript : MonoBehaviour
{
	public Transform target;
	public float xx,yy,zz;
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        transform.LookAt(target);
		
		//transform.rotation *= Quaternion.FromToRotation(Vector3.left, Vector3.forward);
		transform.rotation = transform.rotation*Quaternion.Euler(xx,yy,zz);        
    }
}
