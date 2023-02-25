using UnityEngine;
using System.Collections;

[ExecuteInEditMode]
public class KinematicT : MonoBehaviour {
 

	public Transform Stan_1;
	public Transform Stan_2;	
	
	private Vector3 _stan_1_pos;
	private Vector3 _stan_2_pos;


	void LateUpdate () {
		
		if (Stan_1!=null && Stan_2!=null){
			
			Stan_1.LookAt(Stan_2.position,Stan_1.up);
			Stan_2.LookAt(Stan_1.position,Stan_2.up);

		}
	}

}