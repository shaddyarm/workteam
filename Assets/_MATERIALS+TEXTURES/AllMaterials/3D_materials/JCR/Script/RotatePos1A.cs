using UnityEngine;
using System.Collections;

public class RotatePos1A : MonoBehaviour  {
	
	public KeyCode KeyFOR;
	public KeyCode KeyAB;
	public KeyCode KeyBAK;
	[Range(0f , 100f)]
	public float speed;
	//[Range(-180f , 180f)]
	public float minValue;
	//[Range(-180f , 180f)]
	public float maxValue;
	private Vector3 myRotation;
	public Transform target;

	public enum RotAxis  {
		XAxis,
		YAxis,
		ZAxis
	}

	public RotAxis myRotAxis;

	void Start() {
		myRotation = target.localEulerAngles;
	}


	void Update() {
		if(minValue > maxValue)  {
			float t = maxValue;
			maxValue = minValue;
			minValue = t;
		}

		if(Input.GetKey(KeyAB)&&Input.GetKey(KeyFOR)) {
			switch(myRotAxis)  {
			case RotAxis.XAxis:
				myRotation.x = Mathf.Clamp(myRotation.x + speed * Time.deltaTime, minValue, maxValue);
				break;
			case RotAxis.YAxis:
				myRotation.y = Mathf.Clamp(myRotation.y + speed * Time.deltaTime, minValue, maxValue);
				break;
			case RotAxis.ZAxis:
				myRotation.z = Mathf.Clamp(myRotation.z + speed * Time.deltaTime, minValue, maxValue);
				break;
			}
			target.transform.localRotation = Quaternion.Euler(myRotation);
		}

		if(Input.GetKey(KeyAB)&&Input.GetKey(KeyBAK)){
			switch(myRotAxis)  {
			case RotAxis.XAxis:
				myRotation.x = Mathf.Clamp(myRotation.x - speed * Time.deltaTime, minValue, maxValue);
				break;
			case RotAxis.YAxis:
				myRotation.y = Mathf.Clamp(myRotation.y - speed * Time.deltaTime, minValue, maxValue);
				break;
			case RotAxis.ZAxis:
				myRotation.z = Mathf.Clamp(myRotation.z - speed * Time.deltaTime, minValue, maxValue);
				break;
			}
			target.transform.localRotation = Quaternion.Euler(myRotation);
		}
	}

}