using UnityEngine;
using System.Collections;

public class MovePosA : MonoBehaviour {

	public Transform target;
	[Range(0.0f , 10f)]
	public float speed = 0.0f;
	public KeyCode KeyFOR;
	public KeyCode KeyAB;
	public KeyCode KeyBAK;
	public Vector3 forwardPos;
	public Vector3 rearPos;


	void Start () {

	}
		

	void Update () {
	

		if (Input.GetKey(KeyAB) && Input.GetKey(KeyFOR))

 {



			target.transform.localPosition = Vector3.MoveTowards(target.transform.localPosition, forwardPos, speed  * Time.deltaTime);

		}
		if (Input.GetKey(KeyAB) && Input.GetKey(KeyBAK))
		{
			target.transform.localPosition = Vector3.MoveTowards (target.transform.localPosition, rearPos, speed * Time.deltaTime);
		}
	}
}