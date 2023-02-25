using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
public class HTRCompactSupport : MonoBehaviour {

	[Header("STAND UNDER THE SUPPORT")]
	public KeyCode stand;
	[Header("Image")]
	public Image lightFL;
	public Image lightFR;
	public Image lightBL;
	public Image lightBR;
	[Header("Button")]
	public Button FL;
	private bool FL_Bool = true;
	public Button FR;
	private bool FR_Bool = true;
	public Button BL;
	private bool BL_Bool = true;
	public Button BR;
	private bool BR_Bool = true;
	public KeyCode forKey;
	public KeyCode backKey;
	public KeyCode supportForward;
	public KeyCode supportLift;
	public float speedSupport = 0;
	private bool supportEndLift = true;
	[Header("Support FL")]
	public Transform supportFL;
	public float minSupportFL = 0f;
	public float maxSupportFL = 0f;
	private Vector3 FL_min1;
	private Vector3 FL_max1;
	[HideInInspector]
	public bool block_FL = true;
	//Lift Support
	public Transform liftFL;
	public float minLiftFL = 0f;
	public float maxLiftFL = 0f;
	private Vector3 FL_lift_min1;
	private Vector3 FL_lift_max1;
	//Detals
	public Transform supportDetFL;
	public Transform pointCraneFL;
	public Transform pointLiftFL;
	//Check Distance Support End Crane
	public Transform disSupport_FL;
	[HideInInspector]
	public float disFloat_FL = 0f;
	[Header("Support FR")]
	public Transform supportFR;
	public float minSupportFR = 0f;
	public float maxSupportFR = 0f;
	private Vector3 FR_min1;
	private Vector3 FR_max1;
	[HideInInspector]
	public bool block_FR = true;
	//Lift Support
	public Transform liftFR;
	public float minLiftFR = 0f;
	public float maxLiftFR = 0f;
	private Vector3 FR_lift_min1;
	private Vector3 FR_lift_max1;
	//Detals
	public Transform supportDetFR;
	public Transform pointCraneFR;
	public Transform pointLiftFR;
	//Check Distance Support End Crane
	public Transform disSupport_FR;
	[HideInInspector]
	public float disFloat_FR = 0f;
	[Header("Support BL")]
	public Transform supportBL;
	public float minSupportBL = 0f;
	public float maxSupportBL = 0f;
	private Vector3 BL_min1;
	private Vector3 BL_max1;
	[HideInInspector]
	public bool block_BL = true;
	//Lift Support
	public Transform liftBL;
	public float minLiftBL = 0f;
	public float maxLiftBL = 0f;
	private Vector3 BL_lift_min1;
	private Vector3 BL_lift_max1;
	[HideInInspector]
	public bool block_BR = true;
	//Detals
	public Transform supportDetBL;
	public Transform pointCraneBL;
	public Transform pointLiftBL;
	//Check Distance Support End Crane
	public Transform disSupport_BL;
	[HideInInspector]
	public float disFloat_BL = 0f;
	[Header("Support BR")]
	public Transform supportBR;
	public float minSupportBR = 0f;
	public float maxSupportBR = 0f;
	private Vector3 BR_min1;
	private Vector3 BR_max1;
	//Lift Support
	public Transform liftBR;
	public float minLiftBR = 0f;
	public float maxLiftBR = 0f;
	private Vector3 BR_lift_min1;
	private Vector3 BR_lift_max1;
	//Detals
	public Transform supportDetBR;
	public Transform pointCraneBR;
	public Transform pointLiftBR;
	//Check Distance Support End Crane
	public Transform disSupport_BR;
	[HideInInspector]
	public float disFloat_BR = 0f;
	private bool detalsSupport = true;

	void Start(){
		//SupportForward FL
		FL_min1 = new Vector3 (minSupportFL, supportFL.transform.localPosition.y, supportFL.transform.localPosition.z);
		FL_max1 = new Vector3 (maxSupportFL, supportFL.transform.localPosition.y, supportFL.transform.localPosition.z);
		//SupportForward FR
		FR_min1 = new Vector3 (minSupportFR, supportFR.transform.localPosition.y, supportFR.transform.localPosition.z);
		FR_max1 = new Vector3 (maxSupportFR, supportFR.transform.localPosition.y, supportFR.transform.localPosition.z);
		//SupportForward BL
		BL_min1 = new Vector3 (minSupportBL, supportBL.transform.localPosition.y, supportBL.transform.localPosition.z);
		BL_max1 = new Vector3 (maxSupportBL, supportBL.transform.localPosition.y, supportBL.transform.localPosition.z);
		//SupportForward BR
		BR_min1 = new Vector3 (minSupportBR, supportBR.transform.localPosition.y, supportBR.transform.localPosition.z);
		BR_max1 = new Vector3 (maxSupportBR, supportBR.transform.localPosition.y, supportBR.transform.localPosition.z);
	    //SupportLift FL
		FL_lift_min1 = new Vector3(liftFL.transform.localPosition.x,minLiftFL,liftFL.transform.localPosition.z);
		FL_lift_max1 = new Vector3(liftFL.transform.localPosition.x,maxLiftFL,liftFL.transform.localPosition.z);
		//SupportLift FR
		FR_lift_min1 = new Vector3(liftFR.transform.localPosition.x,minLiftFR,liftFR.transform.localPosition.z);
		FR_lift_max1 = new Vector3(liftFR.transform.localPosition.x,maxLiftFR,liftFR.transform.localPosition.z);
		//SupportLift BL
		BL_lift_min1 = new Vector3(liftBL.transform.localPosition.x,minLiftBL,liftBL.transform.localPosition.z);
		BL_lift_max1 = new Vector3(liftBL.transform.localPosition.x,maxLiftBL,liftBL.transform.localPosition.z);
		//SupportLift BR
		BR_lift_min1 = new Vector3(liftBR.transform.localPosition.x,minLiftBR,liftBR.transform.localPosition.z);
		BR_lift_max1 = new Vector3(liftBR.transform.localPosition.x,maxLiftBR,liftBR.transform.localPosition.z);
	}
	void Update(){
			//Support Forward
		if (Input.GetKey (forKey) && Input.GetKey (supportForward) && GetComponent<HTRCompactCrane>().onCraneCom_Bool == true) {
				supportEndLift = true;
			SupportMoveForward ();
		} else if (Input.GetKey (backKey) && Input.GetKey (supportForward)  && GetComponent<HTRCompactCrane>().onCraneCom_Bool == true) {
				supportEndLift = false;
			SupportMoveForward ();
			}
			//Support Lift
		if (Input.GetKey (forKey) && Input.GetKey (supportLift) && GetComponent<HTRCompactCrane>().onCraneCom_Bool == true) {
				supportEndLift = true;
			SupportMoveLift ();
		} else if (Input.GetKey (backKey) && Input.GetKey (supportLift) && GetComponent<HTRCompactCrane>().onCraneCom_Bool == true) {
				supportEndLift = false;
			SupportMoveLift ();
			}
	//Check Distance
		if (GetComponent<HTRCompactCrane> ().onCraneCom_Bool == true) {
			disFloat_FL = Vector3.Distance (disSupport_FL.position, supportFL.position);
			disFloat_FR = Vector3.Distance (disSupport_FR.position, supportFR.position);
			disFloat_BL = Vector3.Distance (disSupport_BL.position, supportBL.position);
			disFloat_BR = Vector3.Distance (disSupport_BR.position, supportBR.position);
		}
		if (supportDetFL.transform.parent == pointLiftFL && disFloat_FL < 0.4f) {
			block_FL = false;
		}else 
			block_FL = true;
		if (supportDetFR.transform.parent == pointLiftFR && disFloat_FR < 0.4f) {
			block_FR = false;
		}else 
			block_FR = true;
		if (supportDetBL.transform.parent == pointLiftBL && disFloat_BL < 0.4f) {
			block_BL = false;
		}else
			block_BL = true;
		if (supportDetBR.transform.parent == pointLiftBR && disFloat_BR < 0.4f) {
			block_BR = false;
		}else 
			block_BR = true;
		if (Input.GetKeyDown (stand) && GetComponent<HTRCompactCrane>().onCraneCom_Bool == true) {
			DetalsSupport ();
		}
	}
	public void SupportMoveForward(){
		if (supportEndLift == true) {
			if (FL_Bool == false && block_FL == true) {
				supportFL.transform.localPosition = Vector3.MoveTowards (supportFL.transform.localPosition, FL_min1, speedSupport * Time.deltaTime);
			}
			if (FR_Bool == false && block_FR == true) {
				supportFR.transform.localPosition = Vector3.MoveTowards (supportFR.transform.localPosition, FR_min1, speedSupport * Time.deltaTime);
			}
			if (BL_Bool == false && block_BL == true) {
				supportBL.transform.localPosition = Vector3.MoveTowards (supportBL.transform.localPosition, BL_min1, speedSupport * Time.deltaTime);
			}
			if (BR_Bool == false && block_BR == true) {
				supportBR.transform.localPosition = Vector3.MoveTowards (supportBR.transform.localPosition, BR_min1, speedSupport * Time.deltaTime);
			}
		} else if (supportEndLift == false) {
			if (FL_Bool == false) {
				supportFL.transform.localPosition = Vector3.MoveTowards (supportFL.transform.localPosition, FL_max1, speedSupport * Time.deltaTime);
			}
			if (FR_Bool == false) {
				supportFR.transform.localPosition = Vector3.MoveTowards (supportFR.transform.localPosition, FR_max1, speedSupport * Time.deltaTime);
			}
			if (BL_Bool == false) {
				supportBL.transform.localPosition = Vector3.MoveTowards (supportBL.transform.localPosition, BL_max1, speedSupport * Time.deltaTime);
			}
			if (BR_Bool == false) {
				supportBR.transform.localPosition = Vector3.MoveTowards (supportBR.transform.localPosition, BR_max1, speedSupport * Time.deltaTime);
			}
		}
		gameObject.GetComponent<HTRCompactCrane> ().SoundPitchCrane ();
	}
	public void SupportMoveLift(){
		if (supportEndLift == true) {
			if (FL_Bool == false) {
				liftFL.transform.localPosition = Vector3.MoveTowards (liftFL.transform.localPosition, FL_lift_min1, speedSupport * Time.deltaTime);
			}
			if (FR_Bool == false) {
				liftFR.transform.localPosition = Vector3.MoveTowards (liftFR.transform.localPosition, FR_lift_min1, speedSupport * Time.deltaTime);
			}
			if (BL_Bool == false) {
				liftBL.transform.localPosition = Vector3.MoveTowards (liftBL.transform.localPosition, BL_lift_min1, speedSupport * Time.deltaTime);
			}
			if (BR_Bool == false) {
				liftBR.transform.localPosition = Vector3.MoveTowards (liftBR.transform.localPosition, BR_lift_min1, speedSupport * Time.deltaTime);
			}
		} else if (supportEndLift == false) {
			if (FL_Bool == false) {
				liftFL.transform.localPosition = Vector3.MoveTowards (liftFL.transform.localPosition, FL_lift_max1, speedSupport * Time.deltaTime);
			}
			if (FR_Bool == false) {
				liftFR.transform.localPosition = Vector3.MoveTowards (liftFR.transform.localPosition, FR_lift_max1, speedSupport * Time.deltaTime);
			}
			if (BL_Bool == false) {
				liftBL.transform.localPosition = Vector3.MoveTowards (liftBL.transform.localPosition, BL_lift_max1, speedSupport * Time.deltaTime);
			}
			if (BR_Bool == false) {
				liftBR.transform.localPosition = Vector3.MoveTowards (liftBR.transform.localPosition, BR_lift_max1, speedSupport * Time.deltaTime);
			}
		}
		gameObject.GetComponent<HTRCompactCrane> ().SoundPitchCrane ();
	}
	public void ButtonFL(){
		if (FL_Bool == true) {
			lightFL.GetComponent<Image> ().color = new Color32 (0, 255, 2, 255);
			FL_Bool = false;
		} else if (FL_Bool == false) {
			lightFL.GetComponent<Image> ().color = new Color32 (255, 185, 0, 255);
			FL_Bool = true;
			detalsSupport = true;
		}
	}
	public void ButtonFR(){
		if (FR_Bool == true) {
			lightFR.GetComponent<Image> ().color = new Color32 (0, 255, 2, 255);
			FR_Bool = false;
		} else if (FR_Bool == false) {
			lightFR.GetComponent<Image> ().color = new Color32 (255, 185, 0, 255);
			FR_Bool = true;
			detalsSupport = true;
		}
	}
	public void ButtonBL(){
		if (BL_Bool == true) {
			lightBL.GetComponent<Image> ().color = new Color32 (0, 255, 2, 255);
			BL_Bool = false;
		} else if (BL_Bool == false) {
			lightBL.GetComponent<Image> ().color = new Color32 (255, 185, 0, 255);
			BL_Bool = true;
			detalsSupport = true;
		}
	}
	public void ButtonBR(){
		if (BR_Bool == true) {
			lightBR.GetComponent<Image> ().color = new Color32 (0, 255, 2, 255);
			BR_Bool = false;
		} else if (BR_Bool == false) {
			lightBR.GetComponent<Image> ().color = new Color32 (255, 185, 0, 255);
			BR_Bool = true;
			detalsSupport = true;
		}
	}
	public void DetalsSupport(){
		if (detalsSupport == true) {
			if (disFloat_FL > 0.8f && FL_Bool == false) {
				supportDetFL.transform.position = pointLiftFL.transform.position;
				supportDetFL.transform.rotation = pointLiftFL.transform.rotation;
				supportDetFL.transform.parent = pointLiftFL;
			}
			if (disFloat_FR > 0.8f && FR_Bool == false) {
				supportDetFR.transform.position = pointLiftFR.transform.position;
				supportDetFR.transform.rotation = pointLiftFR.transform.rotation;
				supportDetFR.transform.parent = pointLiftFR;
			}
			if (disFloat_BL > 0.8f && BL_Bool == false) {
				supportDetBL.transform.position = pointLiftBL.transform.position;
				supportDetBL.transform.rotation = pointLiftBL.transform.rotation;
				supportDetBL.transform.parent = pointLiftBL;
			}
			if (disFloat_BR > 0.8f && BR_Bool == false) {
				supportDetBR.transform.position = pointLiftBR.transform.position;
				supportDetBR.transform.rotation = pointLiftBR.transform.rotation;
				supportDetBR.transform.parent = pointLiftBR;
			}
			detalsSupport = false;
		} else if (detalsSupport == false) {
			if (FL_Bool == false) {
				supportDetFL.transform.position = pointCraneFL.transform.position;
				supportDetFL.transform.rotation = pointCraneFL.transform.rotation;
				supportDetFL.transform.parent = pointCraneFL;
			}
			if (FR_Bool == false) {
				supportDetFR.transform.position = pointCraneFR.transform.position;
				supportDetFR.transform.rotation = pointCraneFR.transform.rotation;
				supportDetFR.transform.parent = pointCraneFR;
			}
			if (BL_Bool == false) {
				supportDetBL.transform.position = pointCraneBL.transform.position;
				supportDetBL.transform.rotation = pointCraneBL.transform.rotation;
				supportDetBL.transform.parent = pointCraneBL;
			}
			if (BR_Bool == false) {
				supportDetBR.transform.position = pointCraneBR.transform.position;
				supportDetBR.transform.rotation = pointCraneBR.transform.rotation;
				supportDetBR.transform.parent = pointCraneBR;
			}
			detalsSupport = true;
		}
	}
}
