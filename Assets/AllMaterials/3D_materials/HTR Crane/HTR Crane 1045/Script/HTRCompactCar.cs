using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
public class HTRCompactCar : MonoBehaviour {

	public Rigidbody rigCrane;
	public Transform centerOfMassObj;
	public KeyCode trunSignalKey;
	public KeyCode headlightsKey;
	private bool headlightsKey_Bool = true;
	private bool trunSignalKey_Bool = true;
	public Material matLight;
	[HideInInspector]
	public bool powerCarBool = true;
	[Header("WheelCollider")]
	public WheelCollider wColL1;
	public WheelCollider wColL2;
	public WheelCollider wColL3;
	public WheelCollider wColR1;
	public WheelCollider wColR2;
	public WheelCollider wColR3;
	[Header("WheelTransform")]
	public Transform wTransL1;
	public Transform wTransL2;
	public Transform wTransL3;
	public Transform wTransR1;
	public Transform wTransR2;
	public Transform wTransR3;
	[Header("Motor")]
	public KeyCode sportKey;
	public float motor = 0f;
	public float maxMotor = 0f;
	public float spotrMotor = 0f;
	private float m_Vertical = 0f;
	private bool spotrMotor_Bool = true;
	private bool revers_Bool = false;
	private int checkRevers_int = 0;
	[Header("Steer")]
	private float m_Horizontal = 0f;
	public float steerFor = 0f;
	public float steerBack = 0f;
	[Header("Brake")]
	public KeyCode braceKeye;
	public float brace = 0f;
	public float maxBrace = 0f;
	private bool bracKey_Bool = true;
	[Header("Braking if motort Revers")]
	public float T = 0;
	[Header("Braking if motort = Forward")]
	public float M = 0;
	[Header("Ui")]
	public Image brakeIm;
	public Image trunSignalIm;
	public Text speedCraneText;
	public Image lightIm;
	[Header("Light")]
	public Light brakeLightL;
	public Light brakeLightR;
	public Light reversLightL;
	public Light reversLightR;
	public Light headlightsFL;
	public Light headlightsFR;
	public Light[] trunSignal;
	[Header("Sound")]
	public AudioSource headlightsSound;
	public AudioSource trunSignalSound;
	public AudioSource motorSound;
	private float maxEngineRPM = 6000.0f;
	private float minEngineRPM = 1000.0f;
	private float engineRPM = 0f;
	private int currentGear = 0;
	private float gearShiftRate = 12.0f;
	public float pitchSoundCrane = 0.9f;

	void Start(){
		rigCrane.centerOfMass = centerOfMassObj.localPosition;
		matLight.DisableKeyword ("_EMISSION");
	}
	void Update(){
		if (powerCarBool == true) {
			m_Horizontal = Input.GetAxis ("Horizontal");
			m_Vertical = Input.GetAxis ("Vertical");
			Motor ();
			Steer ();
			Brace ();
		}
			Light ();
		SoundEngine ();
		//Check Speed
		float speedT = rigCrane.velocity.magnitude;
		int speedInt = (int)(speedT * 3.6f);
		speedCraneText.text = "" + speedInt.ToString ();
	}
	IEnumerator TrunSignalIE(){
		while (true) {
			yield return new WaitForSeconds (0.3f);
			for (int i = 0; i < trunSignal.Length; i++) {
				trunSignal [i].enabled = true;
			}
			trunSignalIm.GetComponent<Image> ().color = new Color32 (72, 255, 76, 245);
			yield return new WaitForSeconds (0.3f);
			for (int i = 0; i < trunSignal.Length; i++) {
				trunSignal [i].enabled = false;
			}
			trunSignalIm.GetComponent<Image> ().color = new Color32 (255, 255, 255, 245);
		}
	}
	private void Motor(){
		if (spotrMotor_Bool == true) {
			wColL1.motorTorque = m_Vertical * motor;
			wColL2.motorTorque = m_Vertical * motor;
			wColL3.motorTorque = m_Vertical * motor;
			wColR1.motorTorque = m_Vertical * motor;
			wColR2.motorTorque = m_Vertical * motor;
			wColR3.motorTorque = m_Vertical * motor;
			rigCrane.velocity = Vector3.ClampMagnitude (rigCrane.velocity, maxMotor);
		} else if (spotrMotor_Bool == false) {
			wColL1.motorTorque = m_Vertical * spotrMotor;
			wColL2.motorTorque = m_Vertical * spotrMotor;
			wColL3.motorTorque = m_Vertical * spotrMotor;
			wColR1.motorTorque = m_Vertical * spotrMotor;
			wColR2.motorTorque = m_Vertical * spotrMotor;
			wColR3.motorTorque = m_Vertical * spotrMotor;
			rigCrane.velocity = Vector3.ClampMagnitude (rigCrane.velocity, maxMotor);
		}
		UpdateWheelPoses ();
		if (Input.GetKeyDown (sportKey)) {
			spotrMotor_Bool = false;
		}else if(Input.GetKeyUp(sportKey)){
			spotrMotor_Bool = true;
		}
	}
	private void UpdateWheel(WheelCollider wcol,Transform wTran){
		Vector3 _pos = wTran.position;
		Quaternion _quat = wTran.rotation;
		wcol.GetWorldPose (out _pos, out _quat);
		wTran.transform.position = _pos;
		wTran.transform.rotation = _quat;
	}
	public void UpdateWheelPoses(){
		UpdateWheel (wColL1, wTransL1);
		UpdateWheel (wColL2, wTransL2);
		UpdateWheel (wColL3, wTransL3);
		UpdateWheel (wColR1, wTransR1);
		UpdateWheel (wColR2, wTransR2);
		UpdateWheel (wColR3, wTransR3);
	}
	public void Steer(){
		wColL1.steerAngle = m_Horizontal * steerFor;
		wColL2.steerAngle = m_Horizontal * steerFor;
		wColR1.steerAngle = m_Horizontal * steerFor;
		wColR2.steerAngle = m_Horizontal * steerFor;
		wColL3.steerAngle = m_Horizontal * -steerBack;
		wColR3.steerAngle = m_Horizontal * -steerBack;
	}
	public void Brace(){
		if (m_Vertical < 0 && !revers_Bool) {
			wColL1.brakeTorque = (brace) * (Mathf.Abs (m_Vertical));
			wColL2.brakeTorque = (brace) * (Mathf.Abs (m_Vertical));
			wColR1.brakeTorque = (brace) * (Mathf.Abs (m_Vertical));
			wColR2.brakeTorque = (brace) * (Mathf.Abs (m_Vertical));
			wColL3.brakeTorque = (brace) * (Mathf.Abs (m_Vertical) / 1.8f);
			wColR3.brakeTorque = (brace) * (Mathf.Abs (m_Vertical) / 1.8f);
		} else {
			if (bracKey_Bool == true) {
				wColL1.brakeTorque = 0;
				wColL2.brakeTorque = 0;
				wColL3.brakeTorque = 0;
				wColR1.brakeTorque = 0;
				wColR2.brakeTorque = 0;
				wColR3.brakeTorque = 0;
			}
		}
		if (brakeLightL.enabled == false) {
			if (m_Vertical == 0 && wColL1.rpm < 0.5f) {
				wColL1.brakeTorque = T;
				wColL2.brakeTorque = T;
				wColL3.brakeTorque = T;
				wColR1.brakeTorque = T;
				wColR2.brakeTorque = T;
				wColR3.brakeTorque = T;
			} else if (m_Vertical == 0 && wColL1.rpm > 6f) {
				wColL1.brakeTorque = M;
				wColL2.brakeTorque = M;
				wColL3.brakeTorque = M;
				wColR1.brakeTorque = M;
				wColR2.brakeTorque = M;
				wColR3.brakeTorque = M;
			} else if (m_Vertical <= 0 && wColL1.rpm < 5f) {
				revers_Bool = true;
			}
		}
		//Check Revers
		if (m_Vertical <= 0 && wColL1.rpm < 5f) {
			revers_Bool = true;
		} else
			revers_Bool = false;
		//Key Brace
		if(Input.GetKeyDown(braceKeye)){
			wColL1.brakeTorque = maxBrace;
			wColL2.brakeTorque = maxBrace;
			wColL3.brakeTorque = maxBrace;
			wColR1.brakeTorque = maxBrace;
			wColR2.brakeTorque = maxBrace;
			wColR3.brakeTorque = maxBrace;
			brakeLightL.enabled = true;
			brakeLightR.enabled = true;
			brakeIm.GetComponent<Image> ().color = new Color32 (255, 64, 64, 245);
			bracKey_Bool = false;
		}else if(Input.GetKeyUp(braceKeye)){
			wColL1.brakeTorque = 0;
			wColL2.brakeTorque = 0;
			wColL3.brakeTorque = 0;
			wColR1.brakeTorque = 0;
			wColR2.brakeTorque = 0;
			wColR3.brakeTorque = 0;
			brakeLightL.enabled = false;
			brakeLightR.enabled = false;
			brakeIm.GetComponent<Image> ().color = new Color32 (255, 255, 255, 245);
			bracKey_Bool = true;
		}
	}
	public void Light(){
		checkRevers_int = (int)(wColL1.rpm);
		if (m_Vertical < 0 && checkRevers_int < 0) {
			reversLightL.enabled = true;
			reversLightR.enabled = true;
		} else if (m_Vertical == 0 && checkRevers_int < 0) {
			reversLightL.enabled = true;
			reversLightR.enabled = true;
		} else if (checkRevers_int > 0 || checkRevers_int == 0) {
			reversLightL.enabled = false;
			reversLightR.enabled = false;
		}
		// Stop Light
		if (m_Vertical < 0 && checkRevers_int > 0) {
			brakeLightL.enabled = true;
			brakeLightR.enabled = true;
		} else if (m_Vertical < 0 && checkRevers_int < 0) {
			brakeLightL.enabled = false;
			brakeLightR.enabled = false;
		}
		//TrunSignal
		if (Input.GetKeyDown (trunSignalKey) && trunSignalKey_Bool == true) {
			StartCoroutine ("TrunSignalIE");
			trunSignalSound.Play ();
			trunSignalKey_Bool = false;
		} else if (Input.GetKeyDown (trunSignalKey) && trunSignalKey_Bool == false) {
			StopCoroutine ("TrunSignalIE");
			trunSignalSound.Stop ();
			trunSignalIm.GetComponent<Image> ().color = new Color32 (255, 255, 255, 245);
			for (int i = 0; i < trunSignal.Length; i++) {
				trunSignal [i].enabled = false;
			}
			trunSignalKey_Bool = true;
		}
		// On Headlights
		if (Input.GetKeyDown (headlightsKey) && headlightsKey_Bool == true) {
			headlightsFL.enabled = true;
			headlightsFR.enabled = true;
			lightIm.GetComponent<Image> ().color = new Color32 (72,255,76,245);
			matLight.EnableKeyword ("_EMISSION");
			headlightsSound.Play ();
			headlightsKey_Bool = false;
		} else if (Input.GetKeyDown (headlightsKey) && headlightsKey_Bool == false) {
			headlightsFL.enabled = false;
			headlightsFR.enabled = false;
			lightIm.GetComponent<Image> ().color = new Color32 (255,255,255,245);
			matLight.DisableKeyword ("_EMISSION");
			headlightsSound.Play ();
			headlightsKey_Bool = true;
		}
	}
		public void SoundEngine(){
		engineRPM = Mathf.Clamp ((((Mathf.Abs ((wColL1.rpm + wColL1.rpm)) * gearShiftRate) + minEngineRPM)) / (float)(currentGear + 1), minEngineRPM, maxEngineRPM);
		motorSound.pitch = Mathf.Lerp (motorSound.pitch, Mathf.Lerp (pitchSoundCrane, 2f, (engineRPM - minEngineRPM / 1.82f) / (maxEngineRPM + minEngineRPM)), Time.deltaTime * 5f);
	}
}
