using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[RequireComponent(typeof(HingeJoint))]
public class SpoolController2 : MonoBehaviour {

    HingeJoint joint;
    public float speed = 80;

	// Use this for initialization
	void Start () {
        joint = GetComponent<HingeJoint>();
	}
	
	// Update is called once per frame
	void Update () {
        JointMotor motor = joint.motor;

        if (Input.GetKey(KeyCode.F3)){
            motor.targetVelocity = -speed;
        }else if (Input.GetKey(KeyCode.F4)){
            motor.targetVelocity = speed;
        }else{
            motor.targetVelocity = 0;
        }
        joint.motor = motor;
	}
}
