using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
//[ExecuteInEditMode]
public class HTRCompactCrane : MonoBehaviour {
	
	[Header("Start Crane")]
	public KeyCode startCrane;
	private bool startCraneBool = true;
	[HideInInspector]
	public bool onCraneCom_Bool = false;
	private bool startCrane_S1 = true;
	public Transform boomStand;
	[Header("Rotation Crane")]
	public Transform rotCrane;
	public KeyCode leftRotKey;
	public KeyCode rightRotKey;
	public KeyCode aBRot;
	public float speedRot = 0;
	private float rotationImage = 0;
	[Header("Forward Arrow")]
	public KeyCode forArrowKey;
	public KeyCode backArrowKey;
	public KeyCode aBKeyArrowFor;
	public float speedArrowFor = 0;
	[HideInInspector]
	public int disArrowInt = 0;
	private float disArrowFloat = 0;
	public Transform checkDis1;
	public Transform checkDis2;
	public Transform rotCable1;
	public Transform rotCable2;
	[HideInInspector]
	public bool rotCable2_Bool = false;
	public float speedRotCable = 0;
	/// The arrow1.
	public Transform arrow1;
	public Vector3 minArrow1;
	public Vector3 maxArrow1;
	private Vector3 endPos1;
	/// The arrow2.
	public Transform arrow2;
	public Vector3 minArrow2;
	public Vector3 maxArrow2;
	private Vector3 endPos2;
	/// The arrow3.
	public Transform arrow3;
	public Vector3 minArrow3;
	public Vector3 maxArrow3;
	private Vector3 endPos3;
	/// The arrow4.
	public Transform arrow4;
	public Vector3 minArrow4;
	public Vector3 maxArrow4;
	[Header("Arrow Up")]
	public KeyCode upArrow;
	public KeyCode downArrow;
	public KeyCode AB_UpEndDown;
	public Transform arrowUp;
	public float minValuearrowUp = 0;
	public float maxValuearrowUp = 0;
	public float speedArrowUp = 0f;
	[HideInInspector]
	public float arrowUp_Float = 0f;
	public Transform pisC1;
	public Transform pisC2;
	//Display
	[HideInInspector]
	public float displayArrow = 0f;
	public float speedArrowDislpay = 0f;
	[Header("Cabin")]
	//ArrowCabinRot
	public KeyCode upArrowCabin;
	public KeyCode downArrowCabin;
	public KeyCode AB_UpEndDownCabin;
	public Transform arrowCabin;
	public float minValuearrowCabin = 0;
	public float maxValuearrowCabin = 0;
	public float speedArrowCabin = 0f;
	private float arrow_FloatCabin = 0f;
	//ArrowCabinForward
	public KeyCode arrowCabinForKey;
	public KeyCode arrowCabinBackKey;
	public KeyCode AB_CabinFor;
	public Transform arrowCabinFor;
	public Vector3 AF_min;
	public Vector3 AF_max;
	public float speedArrowCabinFor = 0;
	//Cabin
	public Transform cabin;
	private bool arrowRot_S1 = true;
	private bool arrowFor_S1 = true;
	//Piston ArrowCabin
	public Transform pisA1;
	public Transform pisA2;
	//Piston Cabin
	public Transform pisB1;
	public Transform pisB2;
	[Header("Light Crane")]
	public Light[] lightCrane;
	public float speedLight = 0;
	[Header("UI")]
	public Image[] panelCrane;
	public Image panelRot;
	public Image aR;
	public Image aF;
	public Image aU;
	public Image hD;
	public Image cargoA;
	public Image cargoB;
	public Text displayRot;
	public Text distanceArrow;
	public Image powerCrane;
	public Image arrowUpIm;
	[Header("Panel Info Crane")]
	public Image panelInfo;
	public Text panelInfoText;
	public float minPanelInfo = 0f;
	public float maxPanelInfo = 0f;
	private Vector2 p_min;
	private Vector2 p_max;
	public float speedOpenPanel = 0f;
	private bool panelInfo_Bool = true;
	//Detali Support
	private bool detSupport_S1 = true;
	[Header("HTR Compact Support Script")]
	public HTRCompactSupport scriptC;
	[Header("GameObject Hook ")]
	public GameObject hookC1;
	private bool hook_S1 = true;

	void Start(){
		//Panel Info
		p_min = new Vector3(135.786f,minPanelInfo,0);
		p_max = new Vector3(135.786f,maxPanelInfo,0);
	}
	void Update(){
		RotationCrane ();
		ArrowForward ();
		ArrowCabin ();
		ArrowUp ();
		PanelInfo ();
		ArrowCabinFor ();
		//On Off Crane
		if (Input.GetKeyDown (startCrane) && startCraneBool == true) {
			PowerCrane ();
		} else if (Input.GetKeyDown (startCrane) && startCraneBool == false && displayRot.text == "CENTER" && displayArrow == 0) {
			PowerCrane ();
		}
	}
	IEnumerator LightCrane(){
		while (true) {
			yield return new WaitForSeconds (speedLight);
			for (int i = 0; i < lightCrane.Length; i++) {
				lightCrane [i].enabled = true;
			}
			yield return new WaitForSeconds (speedLight);
			for (int i = 0; i < lightCrane.Length; i++) {
				lightCrane [i].enabled = false;
			}
		}
	}
	void LateUpdate(){
		//Piston ArrowCabin
		if (pisA1 != null && pisA2 != null) {
			pisA1.LookAt (pisA2.position, pisA1.up);
			pisA2.LookAt (pisA1.position, pisA2.up);
		}
		//Piston Cabin
		if (pisB1 != null && pisB2 != null) {
			pisB1.LookAt (pisB2.position, pisB1.up);
			pisB2.LookAt (pisB1.position, pisB2.up);
		}
		//Piston Arrow Up
		if (pisC1 != null && pisC2 != null) {
			pisC1.LookAt (pisC2.position, pisC1.up);
			pisC2.LookAt (pisC1.position, pisC2.up);
		}
	}
	public void RotationCrane(){
		if (Input.GetKey (leftRotKey) && Input.GetKey (aBRot) && onCraneCom_Bool == true && arrow_FloatCabin > 9.35f && hookC1.GetComponent<Rigidbody>() != null) {
			rotCrane.Rotate (Vector3.up * speedRot * Time.deltaTime);
			rotationImage -= Time.deltaTime * speedRot;
			aR.GetComponent<Image> ().color = new Color32 (0, 255, 2, 255);
			SoundPitchCrane ();
		} else if (Input.GetKey (rightRotKey) && Input.GetKey (aBRot) && onCraneCom_Bool == true && arrow_FloatCabin > 9.35f && hookC1.GetComponent<Rigidbody>() != null) {
			rotCrane.Rotate (Vector3.up * -speedRot * Time.deltaTime);
			rotationImage += Time.deltaTime * speedRot;
			aR.GetComponent<Image> ().color = new Color32 (0, 255, 2, 255);
			SoundPitchCrane ();
		} else if (Input.GetKeyUp (leftRotKey) || Input.GetKeyUp (rightRotKey)) {
			aR.GetComponent<Image> ().color = new Color32 (255, 185, 0, 255);
		}
		//Rotation Image
		panelRot.GetComponent<RectTransform> ().rotation = Quaternion.Euler (0, 0, rotationImage);
	    //Display Text
		if (panelRot.rectTransform.rotation.z >= 0.01f) {
			displayRot.text = "NO";
		} else if (panelRot.rectTransform.rotation.z <= -0.01f) {
			displayRot.text = "NO";
		}else 
			displayRot.text = "CENTER";
	}
	public void ArrowForward(){
		if (onCraneCom_Bool == true) {
			if (Input.GetKey (forArrowKey) && Input.GetKey (aBKeyArrowFor) && hookC1.GetComponent<Rigidbody>() != null) {
				arrow1.transform.localPosition = Vector3.MoveTowards (arrow1.transform.localPosition, minArrow1, speedArrowFor * Time.deltaTime);
				if (endPos1 == minArrow1) {
					arrow2.transform.localPosition = Vector3.MoveTowards (arrow2.transform.localPosition, minArrow2, speedArrowFor * Time.deltaTime);
				}
				if (endPos2 == minArrow2) {
					arrow3.transform.localPosition = Vector3.MoveTowards (arrow3.transform.localPosition, minArrow3, speedArrowFor * Time.deltaTime);
				}
				if (endPos3 == minArrow3) {	
					arrow4.transform.localPosition = Vector3.MoveTowards (arrow4.transform.localPosition, minArrow4, speedArrowFor * Time.deltaTime);
				}
				if (disArrowInt != 0 && rotCable2_Bool == false) {
					rotCable1.Rotate (Vector3.right * speedRotCable * Time.deltaTime);
					rotCable2.Rotate (Vector3.left * speedRotCable * Time.deltaTime);
				}
					aF.GetComponent<Image> ().color = new Color32 (0, 255, 2, 255);
				SoundPitchCrane ();
			} else if (Input.GetKey (backArrowKey) && Input.GetKey (aBKeyArrowFor) && hookC1.GetComponent<Rigidbody>() != null) {
				arrow1.transform.localPosition = Vector3.MoveTowards (arrow1.transform.localPosition, maxArrow1, speedArrowFor * Time.deltaTime);
				if (endPos1 == maxArrow1) {
					arrow2.transform.localPosition = Vector3.MoveTowards (arrow2.transform.localPosition, maxArrow2, speedArrowFor * Time.deltaTime);
				}
				if (endPos2 == maxArrow2) {
					arrow3.transform.localPosition = Vector3.MoveTowards (arrow3.transform.localPosition, maxArrow3, speedArrowFor * Time.deltaTime);
				}
				if (endPos3 == maxArrow3) {
					arrow4.transform.localPosition = Vector3.MoveTowards (arrow4.transform.localPosition, maxArrow4, speedArrowFor * Time.deltaTime);
				}
				if (disArrowInt != 113 && rotCable2_Bool == false) {
					rotCable1.Rotate (Vector3.left * speedRotCable * Time.deltaTime);
					rotCable2.Rotate (Vector3.right * speedRotCable * Time.deltaTime);
				}
					aF.GetComponent<Image> ().color = new Color32 (0, 255, 2, 255);
				SoundPitchCrane ();
			} else if (Input.GetKeyUp (forArrowKey) || Input.GetKeyUp (backArrowKey)) {
				aF.GetComponent<Image> ().color = new Color32 (255, 185, 0, 255);
			}
			//Check Position Arrow
			endPos1 = arrow1.transform.localPosition;
			endPos2 = arrow2.transform.localPosition;
			endPos3 = arrow3.transform.localPosition;
		//Distance Arrow
			disArrowFloat = Vector3.Distance(checkDis1.position,checkDis2.position);
			disArrowInt = (int)(disArrowFloat * 3.6f);
			distanceArrow.text = "" + disArrowInt.ToString ();
		}
	}
	public void ArrowUp(){
		if (onCraneCom_Bool == true) {
			if (Input.GetKey (upArrow) && Input.GetKey (AB_UpEndDown) && hookC1.GetComponent<Rigidbody>() != null) {
				arrowUp_Float += Time.deltaTime * speedArrowUp;
				arrowUp_Float = Mathf.Clamp (arrowUp_Float, minValuearrowUp, maxValuearrowUp);
				arrowUp.transform.localRotation = Quaternion.AngleAxis (arrowUp_Float, Vector3.left);
				aU.GetComponent<Image> ().color = new Color32 (0, 255, 2, 255);
				displayArrow += Time.deltaTime * speedArrowDislpay;
				SoundPitchCrane ();
			} else if (Input.GetKey (downArrow) && Input.GetKey (AB_UpEndDown)) {
				arrowUp_Float -= Time.deltaTime * speedArrowUp;
				arrowUp_Float = Mathf.Clamp (arrowUp_Float, minValuearrowUp, maxValuearrowUp);
				arrowUp.transform.localRotation = Quaternion.AngleAxis (arrowUp_Float, Vector3.left);
				aU.GetComponent<Image> ().color = new Color32 (0, 255, 2, 255);
				displayArrow -= Time.deltaTime * speedArrowDislpay;
				SoundPitchCrane ();
			} else if (Input.GetKeyUp (upArrow) || Input.GetKeyUp (downArrow)) {
				aU.GetComponent<Image> ().color = new Color32 (255, 185, 0, 255);
			}
		}
		//Display ArrowUp
		displayArrow = Mathf.Clamp(displayArrow,0,110f);
		arrowUpIm.GetComponent<RectTransform> ().rotation = Quaternion.Euler (0, 0, displayArrow);
	}
	// Off On Crane
	public void PowerCrane(){
		if (startCraneBool == true) {
			gameObject.GetComponent<HTRCompactCar>().powerCarBool = false;
			onCraneCom_Bool = true;
			powerCrane.GetComponent<Image> ().color = new Color32 (86,253,80,255);
			StartCoroutine ("LightCrane");
			gameObject.GetComponent<HTRCompactCar> ().pitchSoundCrane = 1.05f;
			for (int i = 0; i < panelCrane.Length; i++) {
				panelCrane [i].enabled = true;
			}
			displayRot.enabled = true;
			distanceArrow.enabled = true;
			panelInfoText.enabled = true;
			boomStand.localRotation = Quaternion.Euler (-68f, 0, 0);
			startCraneBool = false;
		} else if (startCraneBool == false) {
			gameObject.GetComponent<HTRCompactCar>().powerCarBool = true;
			onCraneCom_Bool = false;
			powerCrane.GetComponent<Image> ().color = new Color32 (255,255,255,255);
			StopCoroutine ("LightCrane");
			for (int i = 0; i < lightCrane.Length; i++) {
				lightCrane [i].enabled = false;
			}
			for (int i = 0; i < panelCrane.Length; i++) {
				panelCrane [i].enabled = false;
			}
			displayRot.enabled = false;
			distanceArrow.enabled = false;
			panelInfoText.enabled = false;
			gameObject.GetComponent<HTRCompactCar> ().pitchSoundCrane = 0.9f;
			boomStand.localRotation = Quaternion.Euler (0, 0, 0);
			startCraneBool = true;
		}
	}
	public void ArrowCabin(){
		if(onCraneCom_Bool == true){
			if (Input.GetKey (upArrowCabin) && Input.GetKey (AB_UpEndDownCabin)) {
				arrow_FloatCabin += Time.deltaTime * speedArrowCabin;
				arrow_FloatCabin = Mathf.Clamp (arrow_FloatCabin, minValuearrowCabin, maxValuearrowCabin);
				arrowCabin.transform.localRotation = Quaternion.AngleAxis (arrow_FloatCabin, Vector3.left);
				cabin.transform.localRotation = Quaternion.AngleAxis (arrow_FloatCabin, Vector3.right);
				SoundPitchCrane ();
			} else if (Input.GetKey (downArrowCabin) && Input.GetKey (AB_UpEndDownCabin)) {
				arrow_FloatCabin -= Time.deltaTime * speedArrowCabin;
				arrow_FloatCabin = Mathf.Clamp (arrow_FloatCabin, minValuearrowCabin, maxValuearrowCabin);
				arrowCabin.transform.localRotation = Quaternion.AngleAxis (arrow_FloatCabin, Vector3.left);
				cabin.transform.localRotation = Quaternion.AngleAxis (arrow_FloatCabin, Vector3.right);
				SoundPitchCrane();
			}
		}
		if (arrowCabinFor.localPosition.z < 2.62f) {
			minValuearrowCabin = 7;
		} else
			minValuearrowCabin = 0;
	}
	public void ArrowCabinFor(){
		if (Input.GetKey(arrowCabinForKey) && Input.GetKey(AB_CabinFor) && onCraneCom_Bool == true && arrow_FloatCabin > 6.33f) {
			arrowCabinFor.transform.localPosition = Vector3.MoveTowards (arrowCabinFor.transform.localPosition, AF_min, speedArrowCabinFor * Time.deltaTime);
			SoundPitchCrane ();
		} else if (Input.GetKey(arrowCabinBackKey) && Input.GetKey(AB_CabinFor) && onCraneCom_Bool == true && arrow_FloatCabin > 6.33f) {
			arrowCabinFor.transform.localPosition = Vector3.MoveTowards (arrowCabinFor.transform.localPosition, AF_max, speedArrowCabinFor * Time.deltaTime);
			SoundPitchCrane ();
		}
	}
	public void SoundPitchCrane(){
		gameObject.GetComponent<HTRCompactCar> ().motorSound.pitch = Mathf.Lerp (gameObject.GetComponent<HTRCompactCar> ().motorSound.pitch, 1.95f, Time.deltaTime * 0.8f);
	}
	public void PanelInfo ()
	{
		if (onCraneCom_Bool == true) {
			
			if (Input.GetKey (leftRotKey) && Input.GetKey (aBRot) && arrow_FloatCabin < 9.34f || Input.GetKey (rightRotKey) && Input.GetKey (aBRot) && arrow_FloatCabin < 9.34f) {
				panelInfoText.text = "LIFT THE CABIN BEFORE TURNING THE CRANE";	
				panelInfo_Bool = false;
				arrowFor_S1 = true;
				arrowRot_S1 = false;
				detSupport_S1 = true;
				startCrane_S1 = true;
				hook_S1 = true;
			} else if (arrowRot_S1 == false) {
				panelInfo_Bool = true;
			}	
			if (Input.GetKey (arrowCabinForKey) && Input.GetKey (AB_CabinFor) && arrow_FloatCabin < 6.33f || Input.GetKey (arrowCabinBackKey) && Input.GetKey (AB_CabinFor) && arrow_FloatCabin < 6.33f) {
				panelInfoText.text = "LIFT CABIN";	
				panelInfo_Bool = false;
				arrowFor_S1 = false;
				arrowRot_S1 = true;
				detSupport_S1 = true;
				startCrane_S1 = true;
				hook_S1 = true;
			} else if (arrowFor_S1 == false) {
				panelInfo_Bool = true;
			}
			if (Input.GetKey (scriptC.forKey) && Input.GetKey (scriptC.supportForward) && scriptC.block_FL == false || Input.GetKey (scriptC.forKey) && Input.GetKey (scriptC.supportForward) && scriptC.block_FR == false || Input.GetKey (scriptC.forKey) && Input.GetKey (scriptC.supportForward) && scriptC.block_BL == false || Input.GetKey (scriptC.forKey) && Input.GetKey (scriptC.supportForward) && scriptC.block_BR == false) {
				panelInfoText.text = "REMOVE THE STAND UNDER THE SUPPORT";
				panelInfo_Bool = false;
				arrowFor_S1 = true;
				arrowRot_S1 = true;
				detSupport_S1 = false;
				startCrane_S1 = true;
				hook_S1 = true;
			} else if (detSupport_S1 == false) {
				panelInfo_Bool = true;
			}
			if (Input.GetKey (startCrane) && displayRot.text == "NO CENTER" || Input.GetKey (startCrane) && displayArrow > 0 || Input.GetKey (startCrane) && disArrowInt != 0) {
				panelInfoText.text = "CRANE ARROW SHOULD BE IN ORIGINAL POSITION";
				panelInfo_Bool = false;
				arrowFor_S1 = true;
				arrowRot_S1 = true;
				detSupport_S1 = true;
				startCrane_S1 = false;
				hook_S1 = true;
			} else if (startCrane_S1 == false) {
				panelInfo_Bool = true;
			}
			if (Input.GetKey (upArrow) && Input.GetKey (AB_UpEndDown) && hookC1.GetComponent<Rigidbody> () == null || Input.GetKey (downArrow) && Input.GetKey (AB_UpEndDown) && hookC1.GetComponent<Rigidbody> () == null || Input.GetKey (forArrowKey) && Input.GetKey (aBKeyArrowFor) && hookC1.GetComponent<Rigidbody> () == null || Input.GetKey (backArrowKey) && Input.GetKey (aBKeyArrowFor) && hookC1.GetComponent<Rigidbody> () == null) {
				panelInfoText.text = "UNHOOK THE HOOK BEFORE OPERATING THE CRANE";
				panelInfo_Bool = false;
				arrowFor_S1 = true;
				arrowRot_S1 = true;
				detSupport_S1 = true;
				startCrane_S1 = true;
				hook_S1 = false;
			} else if (hook_S1 == false) {
				panelInfo_Bool = true;
			}
			if (arrow_FloatCabin > 9.34f) {
				if (Input.GetKey (leftRotKey) && Input.GetKey (aBRot) && hookC1.GetComponent<Rigidbody> () == null || Input.GetKey (rightRotKey) && Input.GetKey (aBRot) && hookC1.GetComponent<Rigidbody> () == null) {
					panelInfoText.text = "UNHOOK THE HOOK BEFORE OPERATING THE CRANE";
					panelInfoText.fontSize = 13;
					panelInfo_Bool = false;
					arrowFor_S1 = true;
					arrowRot_S1 = true;
					detSupport_S1 = true;
					startCrane_S1 = true;
					hook_S1 = false;
				} else if (hook_S1 == false) {
					panelInfo_Bool = true;
				}
			}
		}
			//Open Panel
		if (panelInfo_Bool == true) {
			panelInfo.GetComponent<RectTransform> ().anchoredPosition = Vector2.MoveTowards (panelInfo.rectTransform.anchoredPosition, p_min, speedOpenPanel * Time.deltaTime);
		} else if (panelInfo_Bool == false) {
			panelInfo.GetComponent<RectTransform> ().anchoredPosition = Vector2.MoveTowards (panelInfo.rectTransform.anchoredPosition, p_max, speedOpenPanel * Time.deltaTime);
		}
	//Destoy Text
		if (panelInfo.GetComponent<RectTransform> ().anchoredPosition.y == minPanelInfo) {
			panelInfoText.text = "";
		}
	}
}