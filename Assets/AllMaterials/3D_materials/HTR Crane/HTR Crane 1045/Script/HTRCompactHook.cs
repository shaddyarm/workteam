using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
public class HTRCompactHook : MonoBehaviour {

	public Rigidbody rigArrow;
	[Header("ON OFF Hook")]
	public KeyCode onHook;
	[HideInInspector]
	public bool onHook_Bool = true;
	[HideInInspector]
	public bool blockRay_Bool = false;
	[Header("Up Down Hook")]
	public KeyCode upHookKey;
	public KeyCode downHookKey;
	public KeyCode abHookKey;
	private float limitHookG = 0.0f;
	public float speedHook = 0f;
	private bool hookOnCollision = true;
	private float anchorHook = 2.39f;
	public Transform startPointHook;
	public Transform pointLineHook;
	[Header("Rotation Cargo")]
	public KeyCode rotLeft;
	public KeyCode rotRight;
	public KeyCode abKey;
	private float motorRot = 0.0f;
	public float speedRot = 0f;
	private float targetVelosityPlatform = 30f;
	private bool rotPlatformBool = false;
	[Header("Rotation Cable")]
	public Transform rotCable2;
	public float speedRotCable = 0f;
	[Header("Decali")]
	public GameObject decalPoint;
	//Cargo Connected
	public Image connectedIm;
	public MenuSceneSym g1;
	[Header("Support Platform")]
	public RaycastHit hitSupportPlatform;
	[HideInInspector]
	public string nameSupportPaltform;
	[HideInInspector]
	public bool nameSupportPaltform_Bool = true;

	void Update(){
		//On Off Hook
		if (Input.GetKeyDown (onHook) && gameObject.GetComponentInParent<HTRCompactCrane> ().onCraneCom_Bool == true) {
			OnHook ();
		}
		if (onHook_Bool == false) {
			UpDownHook ();
			//Start position Hook anchor 2.39f to 1.33f
			if (anchorHook > 1.33f && gameObject.GetComponentInParent<HTRCompactCrane> ().arrowUp_Float != 0) {
				anchorHook -= Time.deltaTime * 0.6f;
			}
			if(blockRay_Bool == true){
			Debug.DrawRay (pointLineHook.position, -Vector3.up * 1000, Color.red);
			Ray ray = new Ray (pointLineHook.position, -Vector3.up);
			int layerIgnor = ~(1 << 8);
			int layerCargo = (1 << 12);
			RaycastHit hit;
			if (Physics.Raycast (ray, out hit, 1000, layerIgnor)) {
				decalPoint.transform.position = hit.point + hit.normal * 0.01f;
				decalPoint.transform.rotation = Quaternion.LookRotation (-hit.normal);
			}
				//Cargo
				if (Physics.Raycast (ray, out hitSupportPlatform, 3.6f, layerCargo)) {
					if (nameSupportPaltform_Bool == true) {
						nameSupportPaltform = hitSupportPlatform.collider.gameObject.name;
					}
					if (pointLineHook.GetComponent<LineRenderer> () == null) {
						connectedIm.enabled = true;
					}
					} else {
					if (nameSupportPaltform_Bool == true) {
						nameSupportPaltform = null;
					}
					connectedIm.enabled = false;
				}
	}
		//Transform rotation Cargo
		if (Input.GetKey (rotLeft) && Input.GetKey (abKey) && nameSupportPaltform != null) {
			rotPlatformBool = true;
			motorRot = +targetVelosityPlatform;
		} else if (Input.GetKey (rotRight) && Input.GetKey (abKey) && nameSupportPaltform != null) {
			rotPlatformBool = true;
			motorRot = -targetVelosityPlatform;
		} else if (Input.GetKeyUp (rotLeft) || Input.GetKeyUp (rotRight) || Input.GetKeyUp (abKey) && nameSupportPaltform != null) {
			rotPlatformBool = false;
		}
	}
	}
		void FixedUpdate(){
		if (onHook_Bool == false) {
			ConfigurableJoint hookJ = gameObject.GetComponent<ConfigurableJoint> ();
			SoftJointLimit hookLimit = new SoftJointLimit ();
			hookLimit.limit = limitHookG;
			hookJ.linearLimit = hookLimit;
		}
		if (onHook_Bool == false) {
			gameObject.GetComponent<ConfigurableJoint> ().anchor = new Vector3 (0, anchorHook, 0);
		}
		if (rotPlatformBool == true && nameSupportPaltform_Bool == false && g1.canvasCompact.enabled == true) {
			HingeJoint hin = GameObject.Find (nameSupportPaltform).GetComponent<HingeJoint> ();
			JointMotor joi = new JointMotor ();
			joi.targetVelocity = motorRot;
			joi.force = speedRot;
			hin.motor = joi;
			hin.useMotor = true;
		} else if (rotPlatformBool == false && nameSupportPaltform_Bool == false) {
			GameObject.Find (nameSupportPaltform).GetComponent<HingeJoint> ().useMotor = false;
		}
	}
	//If the hook collides with a collision, then stop down
	void OnCollisionEnter(Collision colEnter){
		hookOnCollision = false;
	}
	void OnCollisionExit(Collision colExit){
		hookOnCollision = true;
	}
	public void OnHook(){
		if (gameObject.GetComponentInParent<HTRCompactCrane> ().onCraneCom_Bool == true) {
			if (onHook_Bool == true) {
				Rigidbody rigHook =	gameObject.AddComponent<Rigidbody> ();
				ConstantForce forceH = gameObject.AddComponent<ConstantForce> ();
				forceH.force = new Vector3 (0, -0.01f, 0);
				ConfigurableJoint joinHook = gameObject.AddComponent<ConfigurableJoint> ();
				rigHook.mass = 20;
				rigHook.drag = 1.1f;
				joinHook.xMotion = ConfigurableJointMotion.Locked;
				joinHook.yMotion = ConfigurableJointMotion.Limited;
				joinHook.zMotion = ConfigurableJointMotion.Locked;
				joinHook.angularYMotion = ConfigurableJointMotion.Locked;
				joinHook.connectedBody = rigArrow;
				joinHook.autoConfigureConnectedAnchor = false;
				joinHook.anchor = new Vector3 (0, 2.39f, 0);
				joinHook.connectedAnchor = new Vector3 (0, -0.5f, 0.06f);
				anchorHook = 2.39f;
				decalPoint.SetActive (true);
				onHook_Bool = false;
			} else if (onHook_Bool == false && gameObject.GetComponentInParent<HTRCompactCrane>().displayRot.text == "CENTER" && gameObject.GetComponentInParent<HTRCompactCrane>().displayArrow == 0 && gameObject.GetComponentInParent<HTRCompactCrane>().disArrowInt == 0) {
				Destroy (gameObject.GetComponent<ConstantForce> ());
				Destroy (gameObject.GetComponent<ConfigurableJoint> ());
				Destroy (gameObject.GetComponent<Rigidbody> ());
				transform.position = startPointHook.position;
				transform.localRotation = startPointHook.transform.localRotation;
				decalPoint.SetActive (false);
				onHook_Bool = true;
			}
		}
	}
		public void UpDownHook(){
		if (gameObject.GetComponentInParent<HTRCompactCrane> ().onCraneCom_Bool == true && g1.canvasCompact.enabled == true) {
			if (Input.GetKey (downHookKey) && Input.GetKey (abHookKey) && hookOnCollision == true) {
				limitHookG += Time.deltaTime * speedHook;
				rotCable2.Rotate (Vector3.right * speedRotCable * Time.deltaTime);
				rotCable2.GetComponentInParent<HTRCompactCrane> ().rotCable2_Bool = true;
				gameObject.GetComponentInParent<HTRCompactCrane> ().SoundPitchCrane ();
			} else if (Input.GetKey (upHookKey) && Input.GetKey (abHookKey) && limitHookG > 0.0f) {
				limitHookG -= Time.deltaTime * speedHook;
				rotCable2.Rotate (Vector3.left * speedRotCable * Time.deltaTime);
				rotCable2.GetComponentInParent<HTRCompactCrane> ().rotCable2_Bool = true;
				gameObject.GetComponentInParent<HTRCompactCrane> ().SoundPitchCrane ();
			}else 
				rotCable2.GetComponentInParent<HTRCompactCrane> ().rotCable2_Bool = false;
		}
	}
}