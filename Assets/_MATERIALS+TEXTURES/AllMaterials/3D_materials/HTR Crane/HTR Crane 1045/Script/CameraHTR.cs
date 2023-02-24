using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CameraHTR : MonoBehaviour {

	public Transform hTRCraneCompact;
	public Transform HTRTruck;
	public Transform startPositionCam;
	private Transform targetCam;
	public float distance = 6f;
	private float x = 0f;
	private float y = 0f;
	float xSpeed= 250f;
	float  ySpeed= 120f;
	private float yMinLi= -30f;
	private float yMaxLi= 85f;
	[HideInInspector]
	public bool ifDownKey_Bool = true;
	public MenuSceneSym menuScript;
	public Texture2D cursorT;
	public CursorMode cursorM = CursorMode.Auto;
	public Vector2 hotSpot = Vector2.zero;


	void Start(){
		Cursor.lockState = CursorLockMode.Locked;
		Cursor.visible = false;
		targetCam = startPositionCam;
	}
	void  LateUpdate (){
		if (menuScript.canvasCompact.enabled == true) {
			targetCam = hTRCraneCompact;
		} else if (menuScript.canvasTruck.enabled == true) {
			targetCam = HTRTruck;
		}
		x += Input.GetAxis ("Mouse X") * xSpeed * 0.02f;
		y -= Input.GetAxis ("Mouse Y") * ySpeed * 0.02f;
		y = ClampAngle (y, yMinLi, yMaxLi);
		Quaternion rotation = Quaternion.Euler (y, x, 0);
		Vector3 position = rotation * new Vector3 (0f, 0f, -distance) + targetCam.position;

		transform.rotation = rotation;
		transform.position = position;

		if (Input.GetAxis ("Mouse ScrollWheel") > 0) {
			distance--;
		} else if (Input.GetAxis ("Mouse ScrollWheel") < 0) {
			distance++;
		}
		if (Input.GetKeyDown (menuScript.onMouse)) {
			xSpeed = 0.0f;
			ySpeed = 0.0f;
			Cursor.lockState = CursorLockMode.None;
			Cursor.visible = true;
			Cursor.SetCursor (cursorT, hotSpot, cursorM);	
			ifDownKey_Bool = false;
		} else if (Input.GetKeyUp (menuScript.onMouse)) {
			xSpeed = 250f;
			ySpeed = 120f;
			Cursor.lockState = CursorLockMode.Locked;
			Cursor.visible = false;
			Cursor.SetCursor (null, Vector2.zero, cursorM);
			ifDownKey_Bool = true;
		}
		if (Input.GetKeyDown (menuScript.onMenu)) {
			xSpeed = 0.0f;
			ySpeed = 0.0f;
			Cursor.lockState = CursorLockMode.None;
			Cursor.visible = true;
			Cursor.SetCursor (cursorT, hotSpot, cursorM);	
			ifDownKey_Bool = false;
		} else if (Input.GetKeyUp (menuScript.onMenu)) {
			xSpeed = 250f;
			ySpeed = 120f;
			Cursor.lockState = CursorLockMode.Locked;
			Cursor.visible = false;
			Cursor.SetCursor (null, Vector2.zero, cursorM);
			ifDownKey_Bool = true;
		}
	}
	static float ClampAngle ( float angle ,   float min ,   float max  ){

		if (angle < -360)
			angle += 360;
		if (angle > 360)
			angle -= 360;
		return Mathf.Clamp (angle, min, max);
	}
}
