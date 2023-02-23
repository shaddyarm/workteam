using UnityEngine;
using System.Collections;

public class RotatePos1C : MonoBehaviour {
	public KeyCode KeyFOR;
	public KeyCode KeyAB;
	public KeyCode KeyBAK;
	public KeyCode KeyFOR1;
	public KeyCode KeyAB1;
	public KeyCode KeyBAK1;


	public Transform arm;		
	public Transform plate;		
	private Vector3 armRot;
	private Vector3 plateRot;
	   

	public float armSpeed;
	public float plateSpeed;
	public float minArmAngle;
	public float maxArmAngle;
	public float minPlateAngle;
	public float maxPlateAngle;




	void Start()  {
		armRot = arm.localEulerAngles;
		plateRot = plate.localEulerAngles;

	}

	void Update()  {
		if (minArmAngle > maxArmAngle) {
			float t = maxArmAngle;
			maxArmAngle = minArmAngle;
			minArmAngle = t;
		}
		if (minPlateAngle > maxPlateAngle) {
			float t = maxPlateAngle;
			maxPlateAngle = minPlateAngle;
			minPlateAngle = t;
		}




		if (Input.GetKey (KeyAB1) && Input.GetKey (KeyFOR1) && armRot.z + armSpeed * Time.deltaTime >= minArmAngle && armRot.z + armSpeed * Time.deltaTime <= maxArmAngle) {
			armRot.z = armRot.z + armSpeed * Time.deltaTime;
			plateRot.z = plateRot.z - armSpeed * Time.deltaTime;

		} else if (Input.GetKey (KeyAB1) && Input.GetKey (KeyBAK1) && armRot.z - armSpeed * Time.deltaTime >= minArmAngle && armRot.z - armSpeed * Time.deltaTime <= maxArmAngle) {
			armRot.z = armRot.z - armSpeed * Time.deltaTime;
			plateRot.z = plateRot.z + armSpeed * Time.deltaTime;

		}
	
	


		if(Input.GetKey(KeyAB) && Input.GetKey(KeyFOR) && plateRot.z + plateSpeed * Time.deltaTime >= minPlateAngle && plateRot.z + plateSpeed * Time.deltaTime <= maxPlateAngle)  {
			plateRot.z = plateRot.z + plateSpeed * Time.deltaTime;

		}
	else if(Input.GetKey(KeyAB) && Input.GetKey(KeyBAK) && plateRot.z - plateSpeed * Time.deltaTime >= minPlateAngle && plateRot.z - plateSpeed * Time.deltaTime <= maxPlateAngle)  {
			plateRot.z = plateRot.z - plateSpeed * Time.deltaTime;

		}

		arm.localEulerAngles = armRot;
		plate.localEulerAngles = plateRot;

	}

}