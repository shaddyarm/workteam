using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class winchFIX : MonoBehaviour
{
	public float X,Y,Z;
    // Update is called once per frame
    void Update()
    {
        transform.localPosition = new Vector3(X, Y, Z);
    }
}
