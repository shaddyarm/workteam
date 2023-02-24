using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
public class HTRTrailer : MonoBehaviour {

	public MenuSceneSym mScript;
	[Header("WheelCillider")]
	public WheelCollider wColL1;
	public WheelCollider wColL2;
	public WheelCollider wColL3;
	public WheelCollider wColR1;
	public WheelCollider wColR2;
	public WheelCollider wColR3;
	[Header("TransformCollider")]
	public Transform wTransformL1;
	public Transform wTransformL2;
	public Transform wTransformL3;
	public Transform wTransformR1;
	public Transform wTransformR2;
	public Transform wTransformR3;
	public Rigidbody rigTrailer;
	public Transform centerOfMass;
	public float motorTrailer = 0f;
	[Header("Light")]
	public Light reversL;
	public Light reversR;
	public Light braceL;
	public Light braceR;
	public Light trunSignalL;
	public Light trunSignalR;
	[Header("Supoort mechanism")]
	public Transform supoortTransform;
	public float supoortFor;
	public float supoortBack;
	private Vector3 m_For;
	private Vector3 m_Back;
	public float speedSupoort;
	private bool supoort_Bool = false;
	[Header("Connected Trailer")]
	public KeyCode connected;
	[HideInInspector]
	public bool connected_Bool = true;
	public GameObject truck;
	public AudioSource soundTrailer;
	public Transform pointDet2Trailer;
	public Transform pointDet4Trailer;
	public Transform pointDet2Wheel;
	public Transform pointDet4Wheel;
	public GameObject det2;
	public GameObject det4;
	public Image connectedImWhite;


	void Start(){
		GetComponent<Rigidbody>().centerOfMass = new Vector3 ((centerOfMass.transform.localPosition.x * transform.localScale.x),(centerOfMass.transform.localPosition.y * transform.localScale.y),(centerOfMass.transform.localPosition.z * transform.localScale.z));
		m_For = new Vector3 (supoortTransform.localPosition.x,supoortFor,supoortTransform.localPosition.z);
		m_Back = new Vector3 (supoortTransform.localPosition.x,supoortBack,supoortTransform.localPosition.z);
	}
	void Update(){
		Motor ();
		//Connected Trailer to Truck
		if (Input.GetKeyDown (connected) && connected_Bool == true && mScript.canvasTruck.enabled == true) {
			soundTrailer.Play ();
			ConfigurableJoint join = this.gameObject.AddComponent<ConfigurableJoint> ();
			join.xMotion = ConfigurableJointMotion.Locked;
			join.yMotion = ConfigurableJointMotion.Locked;
			join.zMotion = ConfigurableJointMotion.Locked;
			join.angularXMotion = ConfigurableJointMotion.Limited;
			join.angularYMotion = ConfigurableJointMotion.Limited;
			join.angularZMotion = ConfigurableJointMotion.Limited;
			join.connectedBody = truck.GetComponent<Rigidbody>();
			SoftJointLimit sof = new SoftJointLimit ();
			sof.limit = 100;
			join.angularYLimit = sof;
			truck.GetComponent<HTRTruckController> ().connectedImGreen.enabled = false;
			connectedImWhite.enabled = true;
			supoort_Bool = true;
			DET ();
			connected_Bool = false;
		} else if (Input.GetKeyDown (connected) && connected_Bool == false && mScript.canvasTruck.enabled == true) {
			DET ();
			Destroy (gameObject.GetComponent<ConfigurableJoint> ());
			truck.GetComponent<HTRTruckController> ().connectedImGreen.enabled = true;
			StartCoroutine ("DisconectedTruck");
			connectedImWhite.enabled = false;
		}
	}
	void LateUpdate(){
		//Check that the trailer has no movement
		float speedT = rigTrailer.velocity.magnitude;
		int speedInt = (int)(speedT * 3.6f);
		if (speedInt != 0) {
			UpdateWheelPoses ();
		}
		//Light TrunSignal
		if (truck.GetComponent<HTRTruckController> ().trunSignal [0].enabled == true) {
			trunSignalL.enabled = true;
			trunSignalR.enabled = true;
		} else {
			trunSignalL.enabled = false;
			trunSignalR.enabled = false;
		}
		//Light Brace
		if (truck.GetComponent<HTRTruckController> ().braceBL.enabled == true) {
			braceL.enabled = true;
			braceR.enabled = true;
		} else {
			braceL.enabled = false;
			braceR.enabled = false;
		}
		//Light Revers
		if (truck.GetComponent<HTRTruckController> ().reversBR.enabled == true) {
			reversL.enabled = true;
			reversR.enabled = true;
		} else {
			reversL.enabled = false;
			reversR.enabled = false;
		}
		//Supoort mechanism
		if (supoort_Bool == false) {
			supoortTransform.transform.localPosition = Vector3.MoveTowards (supoortTransform.transform.localPosition, m_For, speedSupoort * Time.deltaTime);
		} else if (supoort_Bool == true) {
			supoortTransform.transform.localPosition = Vector3.MoveTowards (supoortTransform.transform.localPosition, m_Back, speedSupoort * Time.deltaTime);
		}
		if (mScript.canvasTruck.enabled == false && this.gameObject.GetComponent<ConfigurableJoint> () != null) {
			rigTrailer.isKinematic = true;
		} else if (mScript.canvasTruck.enabled == true && this.gameObject.GetComponent<ConfigurableJoint> () != null) {
			rigTrailer.isKinematic = false;
		}
			
	}
	IEnumerator DisconectedTruck(){
		supoort_Bool = false;
		yield return new WaitForSeconds (0.7f);
		connected_Bool = true;
		soundTrailer.Play ();
	}
	public void UpdateWheelPoses(){
			UpdateWheel (wColL1, wTransformL1);
			UpdateWheel (wColL2, wTransformL2);
			UpdateWheel (wColL3, wTransformL3);
			UpdateWheel (wColR1, wTransformR1);
			UpdateWheel (wColR2, wTransformR2);
			UpdateWheel (wColR3, wTransformR3);
	}
	private void UpdateWheel(WheelCollider wCol,Transform wTran){
			Vector3 _pos = wTran.position;
			Quaternion _quat = wTran.rotation;
			wCol.GetWorldPose (out _pos, out _quat);
			wTran.transform.position = _pos;
			wTran.transform.rotation = _quat;
	}
	public void Motor(){
		wColL1.motorTorque = 0.02f;
		wColL2.motorTorque = 0.02f;
		wColL3.motorTorque = 0.02f;
		wColR1.motorTorque = 0.02f;
		wColR2.motorTorque = 0.02f;
		wColR3.motorTorque = 0.02f;
		}
	public void DET(){
		if (connected_Bool == true) {
			//DET2
			det2.transform.position = pointDet2Trailer.position;
			det2.transform.rotation = pointDet2Trailer.rotation;
			//DET4
			det4.transform.position = pointDet4Trailer.position;
			det4.transform.rotation = pointDet4Trailer.rotation;
		} else if (connected_Bool == false) {
			//DET2
			det2.transform.position = pointDet2Wheel.position;
			det2.transform.rotation = pointDet2Wheel.rotation;
			//DET4
			det4.transform.position = pointDet4Wheel.position;
			det4.transform.rotation = pointDet4Wheel.rotation;
		}
	}
}


