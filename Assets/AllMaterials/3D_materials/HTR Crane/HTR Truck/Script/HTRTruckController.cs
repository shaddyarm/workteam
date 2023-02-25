using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
public class HTRTruckController : MonoBehaviour {

	public Rigidbody trucRig;
	public Transform centerOfMass;
	[Header("WheelController")]
	public WheelCollider wheelColFL;
	public WheelCollider wheelColFR;
	public WheelCollider wheelColBL1;
	public WheelCollider wheelColBL2;
	public WheelCollider wheelColBR1;
	public WheelCollider wheelColBR2;
	[Header("WheelTransfotm")]
	public Transform wheelFL;
	public Transform wheelFR;
	public Transform wheelBL1;
	public Transform wheelBL2;
	public Transform wheelBR1;
	public Transform wheelBR2;
	[Header("OptionTruck")]
	public float motor = 0f;
	public float maxMotor = 0f;
	public float spotrMotor = 0f;
	public float steer = 0f;
	public float brace = 0f;
	public float maxBrace = 0f;
	public float m_Vertical = 0f;
	public float m_Horizontal = 0f;
	private bool spotrMotor_Bool = true;
	private bool revers_Bool = false;
	private int checkRevers_int = 0;
	[Header("Wheel Up Dowen")]
	public KeyCode wheelUpKey;
	public KeyCode wheelDownKey;
	private bool wheelUpTruck_Bool = true;
	public float speedWheelUp = 0f;
	public float maxminWheelUp = 0f;
	public float minWheelUp = 0f;
	public float maxWheelUp = 0f;
	public AudioSource wheelUpSound;
	public AudioClip clip1;
	public AudioClip clip2;
	[Header("KeyCode")]
	public KeyCode sportMotor;
	public KeyCode braceKeye;
	public KeyCode headlightsKey;
	public KeyCode trunSignalKey;
	private bool trunSignal_Bool = true;
	private bool headlightsKey_Bool = true;
	private bool bracKeye_Bool = true;
	[Header("Braking if motort = -0")]
	public float T = 0;
	[Header("Braking if motort = 0")]
	public float M = 0;
	[Header("Light")]
	public Light reversBL;
	public Light reversBR;
	public Light braceBL;
	public Light braceBR;
	public Light[] headlights;
	public Light[] trunSignal;
	public Material matLight;
	[Header("Sound")]
	public AudioSource headlightsSound;
	public AudioSource trunSignalSound;
	public AudioSource motorSound;
	private float maxEngineRPM = 6000.0f;
	private float minEngineRPM = 1000.0f;
	private float engineRPM = 0f;
	private int currentGear = 0;
	private float gearShiftRate = 12.0f;
	[Header("UI")]
	public Image turnSignalIm;
	public Image lightIm;
	public Image brakeIm;
	public Image connectedImGreen;
	public Text speedTruck;
	[Header("Raycast Trailer Enter Truck")]
	public Transform rayTruck;
	public HTRTrailer[] trailerScript;


	void Start(){
		trucRig.centerOfMass = centerOfMass.localPosition;
		matLight.DisableKeyword ("_EMISSION");
	}
	void Update(){
		m_Vertical = Input.GetAxis ("Vertical");
		m_Horizontal = Input.GetAxis ("Horizontal");
		UpdateWheelPoses ();
		Motor ();
		Steer ();
		Brace ();
		Light ();
		SoundEngine ();
		//WheelUpTruck
		if (Input.GetKey (wheelUpKey) && maxminWheelUp > minWheelUp) {
			WheelUpTruck ();
			wheelUpTruck_Bool = true;
		} else if (Input.GetKey (wheelDownKey) && maxminWheelUp < maxWheelUp) {
			WheelUpTruck ();
			wheelUpTruck_Bool = false;
		} else if (Input.GetKeyUp (wheelUpKey) || Input.GetKeyUp (wheelDownKey)) {
			wheelUpSound.Stop ();
			wheelUpSound.PlayOneShot (clip2, 1);
		}
		if (Input.GetKeyDown (wheelUpKey) || Input.GetKeyDown (wheelDownKey)) {
			wheelUpSound.clip = clip1;
			wheelUpSound.Play ();
		}
		if (maxminWheelUp == minWheelUp || maxminWheelUp == maxWheelUp) {
			wheelUpSound.Stop ();
		}
		maxminWheelUp = Mathf.Clamp (maxminWheelUp, minWheelUp, maxWheelUp);
		//Check Speed
		float speedT = trucRig.velocity.magnitude;
		int speedInt = (int)(speedT * 3.6f);
		speedTruck.text = "" + speedInt.ToString ();
		//Raycast If the beam hit the trailer1
		Debug.DrawRay (rayTruck.position, Vector3.up * 1f, Color.green);
		int layer = 1 << 10;
		Ray rayT = new Ray (rayTruck.position, Vector3.up);
		RaycastHit hit;
		if (Physics.Raycast (rayT, out hit, 1f, layer)) {
			hit.collider.transform.parent.GetComponent<HTRTrailer> ().enabled = true;
			for (int i = 0; i < trailerScript.Length; i++) {
				if (trailerScript [i].connected_Bool == true) {
					if (hit.collider != null) {
						connectedImGreen.enabled = true;
					}
				}
				if (trailerScript [i].GetComponent<ConfigurableJoint>() != null && trailerScript [i].GetComponent<ConfigurableJoint> ().connectedBody != null) {
					connectedImGreen.enabled = false;
				}
			}
		} else {
			for (int i = 0; i < trailerScript.Length; i++) {
				if (trailerScript [i].connected_Bool == true) {
					trailerScript [i].enabled = false;
				}
				}
			connectedImGreen.enabled = false;
		}
	}
		IEnumerator TrunSignalIE(){
		while (true) {
			yield return new WaitForSeconds (0.3f);
			for (int i = 0; i < trunSignal.Length; i++) {
				trunSignal [i].enabled = true;
			}
			turnSignalIm.GetComponent<Image> ().color = new Color32 (72, 255, 76, 245);
			yield return new WaitForSeconds (0.3f);
			for (int i = 0; i < trunSignal.Length; i++) {
				trunSignal [i].enabled = false;
			}
			turnSignalIm.GetComponent<Image> ().color = new Color32 (255, 255, 255, 245);
		}
	}
	private void Motor(){
		if (spotrMotor_Bool == true) {
			wheelColBL1.motorTorque = m_Vertical * motor;
			wheelColBL2.motorTorque = m_Vertical * motor;
			wheelColBR1.motorTorque = m_Vertical * motor;
			wheelColBR2.motorTorque = m_Vertical * motor;
			trucRig.velocity = Vector3.ClampMagnitude (trucRig.velocity,maxMotor);
		} else if (spotrMotor_Bool == false) {
			wheelColBL1.motorTorque = m_Vertical * spotrMotor;
			wheelColBL2.motorTorque = m_Vertical * spotrMotor;
			wheelColBR1.motorTorque = m_Vertical * spotrMotor;
			wheelColBR2.motorTorque = m_Vertical * spotrMotor;
			trucRig.velocity = Vector3.ClampMagnitude (trucRig.velocity,maxMotor);
		}
		if (Input.GetKeyDown (sportMotor)) {
			spotrMotor_Bool = false;
		} else if (Input.GetKeyUp (sportMotor)) {
			spotrMotor_Bool = true;
		}
	}
	private void UpdateWheel(WheelCollider wCol,Transform wTran){
		Vector3 _pos = wTran.position;
		Quaternion _quat = wTran.rotation;
		wCol.GetWorldPose (out _pos, out _quat);
		wTran.transform.position = _pos;
		wTran.transform.rotation = _quat;
	}
	public void UpdateWheelPoses(){
		UpdateWheel (wheelColFL,wheelFL);
		UpdateWheel (wheelColFR,wheelFR);
		UpdateWheel (wheelColBL1,wheelBL1);
		UpdateWheel (wheelColBL2,wheelBL2);
		UpdateWheel (wheelColBR1,wheelBR1);
		UpdateWheel (wheelColBR2,wheelBR2);
	}
	public void Steer(){
		wheelColFL.steerAngle = m_Horizontal * steer;
		wheelColFR.steerAngle = m_Horizontal * steer;

	}
	public void Brace(){
		if (m_Vertical < 0 && !revers_Bool) {
			wheelColFL.brakeTorque = (brace) * (Mathf.Abs (m_Vertical));
			wheelColFR.brakeTorque = (brace) * (Mathf.Abs (m_Vertical));
			wheelColBL1.brakeTorque = (brace) * (Mathf.Abs (m_Vertical) / 1.8f);
			wheelColBL2.brakeTorque = (brace) * (Mathf.Abs (m_Vertical) / 1.8f);
			wheelColBR1.brakeTorque = (brace) * (Mathf.Abs (m_Vertical) / 1.8f);
			wheelColBR2.brakeTorque = (brace) * (Mathf.Abs (m_Vertical) / 1.8f);
		} else {
			if (bracKeye_Bool == true) {
				wheelColFL.brakeTorque = 0;
				wheelColFR.brakeTorque = 0;
				wheelColBL1.brakeTorque = 0;
				wheelColBL2.brakeTorque = 0;
				wheelColBR1.brakeTorque = 0;
				wheelColBR2.brakeTorque = 0;
			}
		}
		if (braceBL.enabled == false) {
			if (m_Vertical == 0 && wheelColFL.rpm < 0.5f) {
				wheelColBL1.brakeTorque = T;
				wheelColBL2.brakeTorque = T;
				wheelColBR1.brakeTorque = T;
				wheelColBR2.brakeTorque = T;
			} else if (m_Vertical == 0 && wheelColFL.rpm > 6f) {
				wheelColBL1.brakeTorque = M;
				wheelColBL2.brakeTorque = M;
				wheelColBR1.brakeTorque = M;
				wheelColBR2.brakeTorque = M;
			} else if (m_Vertical <= 0 && wheelColFL.rpm < 5f) {
				revers_Bool = true;
			}
		}
			//Check Revers
		if (m_Vertical <= 0 && wheelColBR2.rpm < 5f) {
			revers_Bool = true;
		} else
			revers_Bool = false;
		//Key Brace
		if(Input.GetKeyDown(braceKeye)){
			wheelColFL.brakeTorque = maxBrace;
			wheelColFR.brakeTorque = maxBrace;
			wheelColBL1.brakeTorque = maxBrace;
			wheelColBL2.brakeTorque = maxBrace;
			wheelColBR1.brakeTorque = maxBrace;
			wheelColBR2.brakeTorque = maxBrace;
			braceBL.enabled = true;
			braceBR.enabled = true;
			brakeIm.GetComponent<Image> ().color = new Color32 (255, 64, 64, 245);
			bracKeye_Bool = false;
		}else if(Input.GetKeyUp(braceKeye)){
			wheelColBL1.brakeTorque = 0;
			wheelColBL2.brakeTorque = 0;
			wheelColBR1.brakeTorque = 0;
			wheelColBR2.brakeTorque = 0;
			braceBL.enabled = false;
			braceBR.enabled = false;
			brakeIm.GetComponent<Image> ().color = new Color32 (255, 255, 255, 245);
			bracKeye_Bool = true;
		}
	}
	public void Light(){
		checkRevers_int = (int)(wheelColFL.rpm);
		if (m_Vertical < 0 && checkRevers_int < 0) {
			reversBL.enabled = true;
			reversBR.enabled = true;
		} else if (m_Vertical == 0 && checkRevers_int < 0) {
			reversBL.enabled = true;
			reversBR.enabled = true;
		} else if (checkRevers_int > 0 || checkRevers_int == 0) {
			reversBL.enabled = false;
			reversBR.enabled = false;
		}
		// Stop Light
		if (m_Vertical < 0 && checkRevers_int > 0) {
			braceBL.enabled = true;
			braceBR.enabled = true;
		} else if (m_Vertical < 0 && checkRevers_int < 0) {
			braceBL.enabled = false;
			braceBR.enabled = false;
		}
		// On Headlights
		if (Input.GetKeyDown (headlightsKey) && headlightsKey_Bool == true) {
			for (int i = 0; i < headlights.Length; i++) {
				headlights [i].enabled = true;
			}
			lightIm.GetComponent<Image> ().color = new Color32 (72,255,76,245);
			matLight.EnableKeyword ("_EMISSION");
			headlightsSound.Play ();
			headlightsKey_Bool = false;
		} else if (Input.GetKeyDown (headlightsKey) && headlightsKey_Bool == false) {
			for (int i = 0; i < headlights.Length; i++) {
				headlights [i].enabled = false;
			}
			lightIm.GetComponent<Image> ().color = new Color32 (255,255,255,245);
			matLight.DisableKeyword ("_EMISSION");
			headlightsSound.Play ();
			headlightsKey_Bool = true;
		}
		//TrunSignal
		if (Input.GetKeyDown (trunSignalKey) && trunSignal_Bool == true) {
			StartCoroutine ("TrunSignalIE");
			trunSignalSound.Play ();
			trunSignal_Bool = false;
		} else if (Input.GetKeyDown (trunSignalKey) && trunSignal_Bool == false) {
			StopCoroutine ("TrunSignalIE");
			trunSignalSound.Stop ();
			turnSignalIm.GetComponent<Image> ().color = new Color32 (255, 255, 255, 245);
			for (int i = 0; i < trunSignal.Length; i++) {
				trunSignal [i].enabled = false;
			}
			trunSignal_Bool = true;
		}
	}
	public void WheelUpTruck(){
		if (wheelUpTruck_Bool == true) {
				wheelColFL.center -= Vector3.up * speedWheelUp * Time.deltaTime;
			maxminWheelUp = wheelColFL.center.y;
		    //
			wheelColFR.center -= Vector3.up * speedWheelUp * Time.deltaTime;
			maxminWheelUp = wheelColFR.center.y;
			//
			wheelColBL1.center -= Vector3.up * speedWheelUp * Time.deltaTime;
			maxminWheelUp = wheelColBL1.center.y;
			//
			wheelColBL2.center -= Vector3.up * speedWheelUp * Time.deltaTime;
			maxminWheelUp = wheelColBL2.center.y;
			//
				wheelColBR1.center -= Vector3.up * speedWheelUp * Time.deltaTime;
			maxminWheelUp = wheelColBR1.center.y;
			//
			wheelColBR2.center -= Vector3.up * speedWheelUp * Time.deltaTime;
			maxminWheelUp = wheelColBR2.center.y;
		} else if (wheelUpTruck_Bool == false) {
			wheelColFL.center += Vector3.up * speedWheelUp * Time.deltaTime;
			maxminWheelUp = wheelColFL.center.y;
			//
			wheelColFR.center += Vector3.up * speedWheelUp * Time.deltaTime;
			maxminWheelUp = wheelColFR.center.y;
			//
			wheelColBL1.center += Vector3.up * speedWheelUp * Time.deltaTime;
			maxminWheelUp = wheelColBL1.center.y;
			//
			wheelColBL2.center += Vector3.up * speedWheelUp * Time.deltaTime;
			maxminWheelUp = wheelColBL2.center.y;
			//
			wheelColBR1.center += Vector3.up * speedWheelUp * Time.deltaTime;
			maxminWheelUp = wheelColBR1.center.y;
			//
			wheelColBR2.center += Vector3.up * speedWheelUp * Time.deltaTime;
			maxminWheelUp = wheelColBR2.center.y;
		}
	}
	public void SoundEngine(){
		engineRPM = Mathf.Clamp ((((Mathf.Abs ((wheelColFL.rpm + wheelColBR1.rpm)) * gearShiftRate) + minEngineRPM)) / (float)(currentGear + 1), minEngineRPM, maxEngineRPM);
		motorSound.pitch = Mathf.Lerp (motorSound.pitch, Mathf.Lerp (1f, 2f, (engineRPM - minEngineRPM / 1.82f) / (maxEngineRPM + minEngineRPM)), Time.deltaTime * 5f);
	}
}
