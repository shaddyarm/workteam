using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
public class MenuSceneSym : MonoBehaviour {

	public KeyCode onMenu;
	public KeyCode onMouse;
	public Canvas menuCanvas;
	private bool blockOnClic = true;
	[Header("HTR Crane Compact 1045")]
	public HTRCompactCar scriptC1;
	public HTRCompactCrane scriptC2;
	public HTRCompactSupport scriptC4;
	public HTRCompactHook scriptC5;
	public Canvas canvasCompact;
	private bool ifStart_Bool_Compact = true;
	[Header("HTR Truck")]
	public HTRTruckController scriptT1;
	public Canvas canvasTruck;
	private bool ifStart_Bool_Truck = true;
	[Header("Sound Engine")]
	public AudioClip startEngine;
	public AudioClip stopEngine;
	public AudioClip engine;
	[Header("Info Controller")]
	public KeyCode infoKey;
	public Image infoCraneCompact;
	public Image infoTruck;
	public Image window;
	public Image craneCompact;
	public Image truck;

	void Update(){
		if (Input.GetKeyDown (onMenu)) {
			menuCanvas.enabled = true;
		} else if (Input.GetKeyUp (onMenu)) {
			menuCanvas.enabled = false;
		}
		if (Input.GetKeyDown (infoKey)) {
			menuCanvas.enabled = true;
			infoCraneCompact.enabled = true;
			infoTruck.enabled = true;
			window.enabled = false;
			craneCompact.enabled = false;
			truck.enabled = false;
		} else if (Input.GetKeyUp (infoKey)) {
			menuCanvas.enabled = false;
			infoCraneCompact.enabled = false;
			infoTruck.enabled = false;
			window.enabled = true;
			craneCompact.enabled = true;
			truck.enabled = true;
		}
	}
	//Block OnClic Button
	IEnumerator BlockOnClic(){
		yield return new WaitForSeconds (2.3f);
		blockOnClic = true;
	}
	IEnumerator StartCrane_Compact(){
		scriptC1.enabled = true;
		scriptC2.enabled = true;
		scriptC4.enabled = true;
		scriptC5.blockRay_Bool = true;
		canvasCompact.enabled = true;
			yield return new WaitForSeconds (0.1f);
			scriptC1.motorSound.PlayOneShot (startEngine, 1);
			yield return new WaitForSeconds (0.64f);
			scriptC1.motorSound.clip = engine;
			scriptC1.motorSound.Play ();
	}
	IEnumerator Start_Truck(){
		scriptT1.enabled = true;
		scriptT1.trucRig.constraints = RigidbodyConstraints.None;
		canvasTruck.enabled = true;
			yield return new WaitForSeconds (0.1f);
			scriptT1.motorSound.PlayOneShot (startEngine, 1);
			yield return new WaitForSeconds (0.64f);
			scriptT1.motorSound.clip = engine;
			scriptT1.motorSound.Play ();
}
	IEnumerator StopCrane_Compac(){
			scriptC1.motorSound.Stop ();
			scriptC1.motorSound.PlayOneShot (stopEngine, 1);
			yield return new WaitForSeconds (1.508f);
		scriptC1.enabled = false;
		scriptC2.enabled = false;
		scriptC4.enabled = false;
		scriptC5.blockRay_Bool = false;
		canvasCompact.enabled = false;
		}
	IEnumerator Stop_Truck(){
			scriptT1.motorSound.Stop ();
			scriptT1.motorSound.PlayOneShot (stopEngine, 1);
			yield return new WaitForSeconds (1.508f);
		scriptT1.enabled = false;
		scriptT1.trucRig.constraints = RigidbodyConstraints.FreezeAll;
		canvasTruck.enabled = false;
	}
	IEnumerator Crane_Compact(){
		if (canvasTruck.enabled == true) {
			StartCoroutine ("Stop_Truck");
		}
		yield return new WaitForSeconds (1.508f);
		StartCoroutine ("StartCrane_Compact");
	}
	IEnumerator Truck(){
		if (canvasCompact.enabled == true) {
			StartCoroutine ("StopCrane_Compac");
		}
		yield return new WaitForSeconds (1.508f);
		StartCoroutine ("Start_Truck");
	}
	public void OnClick_Crane_Compact(){
		if (blockOnClic == true) {
			if (ifStart_Bool_Compact == true) {
				StartCoroutine ("StartCrane_Compact");
				ifStart_Bool_Truck = false;
				ifStart_Bool_Compact = false;
			} else if (ifStart_Bool_Compact == false && canvasCompact.enabled == false) {
				StartCoroutine ("Crane_Compact");
			}
			blockOnClic = false;
			StartCoroutine ("BlockOnClic");
		}
	}
		public void OnClick_Truck(){
		if(blockOnClic == true){
		if (ifStart_Bool_Truck == true) {
			StartCoroutine ("Start_Truck");
			ifStart_Bool_Truck = false;
			ifStart_Bool_Compact = false;
		} else if (ifStart_Bool_Truck == false && canvasTruck.enabled == false) {
			StartCoroutine ("Truck");
		}
			blockOnClic = false;
			StartCoroutine ("BlockOnClic");
		}
	}
}