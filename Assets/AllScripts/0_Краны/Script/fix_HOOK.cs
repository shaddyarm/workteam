using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class fix_HOOK : MonoBehaviour
{
	public Rigidbody m_Rigidbody;
	public GameObject HOOK;
    

    void Update()
    {
		/*
		if (Mathf.Abs(HOOK.transform.localRotation.z)>1f)
		{
			m_Rigidbody.centerOfMass = Vector3.zero;
			m_Rigidbody.freezeRotation = true;
		}
		*/
		
		m_Rigidbody.centerOfMass = Vector3.zero;
		m_Rigidbody.freezeRotation = true;
        
    }
}
