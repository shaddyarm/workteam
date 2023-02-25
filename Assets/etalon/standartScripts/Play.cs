using UnityEngine;
using UnityEngine.UI;
using UnityEngine.EventSystems;// Required when using Event data.
using System.Collections;
using System.Collections.Generic;
using System.IO;
using System;

//https://www.mvcode.com/lessons/first-person-camera-and-controller-jamie

public class Play : MonoBehaviour
{
	public Text debug=null;



	//-1 - no move && no rotate
	//-2 - no move
	//0-standart
	//1-orbit
	public int mode = 0;

	public bool inCabin = false;


	#region "Variables"
	public Rigidbody Rigid;
	public GameObject Rigid2;
	public Camera MyCamera;
    public float MouseSensitivity;
	public float TouchSensitivity = 1f;
	public float MoveSpeed;
    public float JumpForce;
	
	
	public AudioSource foodstepSound;
	public bool walkSound;
    
    #endregion
   
    private Vector3 rotateValue;
    private bool guimode;



	//
	float UpDown = 0;
	float LeftRight = 0;
	float ForwardBack = 0;
	float Strafe = 0;
	bool zoom = false;


	public void SetMouseSensitivity(float value)
    {
		MouseSensitivity = value;
	}

	public void Set_Up_Down (float value)
    {
		UpDown = value;
    }
	public void Set_Left_Right(float value)
	{
		LeftRight = value;
	}
	public void Set_Forward_Back(float value)
	{
		ForwardBack = value;
	}
	public void Set_Strafe(float value)
	{
		Strafe = value;
	}

	public void Set_Zoom(bool value)
	{
		zoom = value;
	}


	public void ChangeCameraMode(int _mode)
	{
		mode=_mode;
	}
	public void ChangeFocusObject(Transform ob)
	{
		focus = 	ob;
	}

	public void SetUserInPoint(Transform point)
	{
		//player.transform.parent = KABINA.transform;
		this.gameObject.transform.localPosition = point.localPosition;
//		this.gameObject.transform.localRotation = point.localRotation;
		//player_camera.transform.localRotation = Quaternion.Euler(9.771001f , 0, 0);
	}
	public void SetUserInPointAndRotate(Transform point)
	{
		//player.transform.parent = KABINA.transform;
		this.gameObject.transform.localPosition = point.localPosition;
		this.gameObject.transform.localRotation = point.localRotation;
		//player_camera.transform.localRotation = Quaternion.Euler(9.771001f , 0, 0);
	}


	public void GuiON()
    {
        guimode = true;
    }

    public void GuiOFF()
    {
        guimode = false;
    }

    // Use this for initialization
    void Start()
    {
        guimode = false;
		string filename = System.IO.Path.Combine(Application.streamingAssetsPath, "config.txt");
		//читаем конфиг
		if (System.IO.File.Exists(filename) == true)
		{

			FileInfo theSourceFile = new FileInfo(filename);
			StreamReader  reader = theSourceFile.OpenText();
			string number = reader.ReadLine(); //сенс поворота через тач
			if (number != null)
            {
				float convertedNumber = -1f;
				bool result = float.TryParse(number, System.Globalization.NumberStyles.Any, System.Globalization.CultureInfo.InvariantCulture, out convertedNumber);
				if (result==true)
                {
					TouchSensitivity = convertedNumber;
					Debug.Log("read config. TouchSensitivity=" + TouchSensitivity);
				}
			}
			number = reader.ReadLine(); //сенс поворота через мышь
			if (number != null)
			{
				float convertedNumber = -1f;
				bool result = float.TryParse(number, System.Globalization.NumberStyles.Any, System.Globalization.CultureInfo.InvariantCulture, out convertedNumber);
				if (result == true)
				{
					MouseSensitivity = convertedNumber;
					Debug.Log("read config. MouseSensitivity=" + MouseSensitivity);
				}
			}


		}

	}
    
	public float sensitivity = 10f;
	 float minYAngle = 80f;
	 float maxYAngle = -80f;
	private Vector2 currentRotation;
	
	private Vector3 force;
		 
	void Update()
	{
		if (mode!=0) 
		{
			if (foodstepSound.isPlaying==true) foodstepSound.Pause();
			return;
		}
		
		/*
		if (Input.GetKeyDown(KeyCode.Alpha1)==true)
		{
			if (mode==1)
			{
				mode=0;
				MyCamera.transform.localPosition = new Vector3(0, 0.85f, 0);
				MyCamera.transform.transform.localRotation = Quaternion.Euler(0, 0, 0);
			}
			else if (mode==0)
			{
				mode=1;
			}
		}
		*/
		
		
		
		
	   this.force = (this.CalculateDirection() + this.CalculateDirection2() + this.CalculateDirection3() + this.CalculateDirection4()) * MoveSpeed * 3000.0f * 1.0f;
	   
	   if (walkSound==false) return;
	   
	   if ((this.force.magnitude <=50)&&(foodstepSound.isPlaying==true))
	   {
		   foodstepSound.Pause();
	   }
	   if ((this.force.magnitude >50)&&(foodstepSound.isPlaying==false) && (this.transform.localPosition.y<=1.3f))
	   {
		   foodstepSound.Play();
	   }
	   
	   
	}

	public bool touch4 = false;
	private bool testFirstFloor=false;




	private Vector3 CalculateDirection4()
    {
		if (Input.GetMouseButtonUp(0) == true)
		{
			testFirstFloor = false;
		}

		if (EventSystem.current.IsPointerOverGameObject() == true)
		{
			testFirstFloor = false;
			return new Vector3(0, 0, 0) ;
		}

		


		if (touch4 == false) return new Vector3(0, 0, 0); 
		RaycastHit hit;
		Physics.Raycast(MyCamera.ScreenPointToRay(Input.mousePosition), out hit);

		bool ok__ = true;
		if (Input.touchCount >= 1)
		{
			if (Input.GetTouch(0).phase == TouchPhase.Moved)
            {
				ok__ = false;
			}
		}

		if (((Input.GetMouseButton(0) == true)|| (Input.GetMouseButtonDown(0) == true)) && (ok__==true))
		{
			if (hit.collider.gameObject.tag == "GROUND_MOVE")
			{
				if ((Input.GetMouseButtonDown(0) == true) && (testFirstFloor == false))
				{
					testFirstFloor = true;
					return new Vector3(0, 0, 0);
				}

				if (testFirstFloor == true)
				{
					Vector3 newposition = hit.point;
					newposition.y = this.gameObject.transform.localPosition.y;
					//РЕЗКО
					//this.gameObject.transform.localPosition = newposition;
					//ПЛАВНО
					//this.gameObject.transform.localPosition = Vector3.MoveTowards(this.gameObject.transform.localPosition, newposition, Time.deltaTime * 15f);

					var targetDir = newposition - this.gameObject.transform.localPosition;
					var forward = MyCamera.transform.right;
					var angle = Vector3.Angle(targetDir, forward)-90f;
					//Debug.Log(angle);
					return new  Vector3(-angle/40f, 0, 1.5f) ;
				}
			}
			else
            {
				//Debug.Log("*" + hit.collider.gameObject.name);
				testFirstFloor = false;
			}
		}
		return new Vector3(0, 0, 0); 
	}
	 
	void FixedUpdate()
	{
		TouchUpdate();
		//Touch4();

		if (mode==-1) return;
		
		
		if (mode == 0)
		{
			if (Rigid.useGravity == true)
			{
				Rigid.velocity = new Vector3(0f, -1f, 0f);
			}
			else
			{
				Rigid.velocity = new Vector3(0, 0, 0);
			}



			Rigid.AddRelativeForce(this.force, ForceMode.Force);
			Rigid.angularVelocity = Vector3.zero;


			

			if (Input.GetKey("space"))
			{
				Rigid.AddForce(transform.up * JumpForce);
			}
		}



		if ((Input.GetKey(KeyCode.Q) == true)||(zoom==true))
		{
			MyCamera.fieldOfView = 20f;
		}
		else
		{
			MyCamera.fieldOfView = 60f;
		}

		if (Input.GetMouseButton(1))
		{
			Cursor.lockState = CursorLockMode.Locked;
			
		}
		else
		{
				Cursor.lockState = CursorLockMode.None;
		}
		
		
		//float dt = Time.deltaTime * 50.0f;
		float dt = Time.deltaTime;
		if (dt<1f/10f) dt=1f/10f;


		if ((UpDown!=0)||(LeftRight!=0)||(ForwardBack!=0))
        {
			rotateValue = new Vector3(UpDown * MouseSensitivity * 20.0f * dt, 0, 0);
			MyCamera.transform.eulerAngles = MyCamera.transform.eulerAngles - rotateValue;

			{
				Rigid.MoveRotation(Rigid.rotation * Quaternion.Euler(new Vector3(0, LeftRight * MouseSensitivity * 20.0f * dt, 0)));
			}
			if (inCabin == true) Rigid2.transform.localRotation = Rigid.rotation * Quaternion.Euler(new Vector3(0, LeftRight * MouseSensitivity * 20.0f * dt, 0));
		}

		//touch
		if (Input.touchCount >= 1)
		{
			if (Input.GetTouch(0).phase == TouchPhase.Moved)
			{
				Vector2 touchDeltaPosition = Input.GetTouch(0).deltaPosition;
				float k = -0.0012f;
				rotateValue = new Vector3(touchDeltaPosition.y * MouseSensitivity * TouchSensitivity * 20.0f * k * dt, 0, 0);
				MyCamera.transform.eulerAngles = MyCamera.transform.eulerAngles - rotateValue;
				{
					Rigid.MoveRotation(Rigid.rotation * Quaternion.Euler(new Vector3(0, touchDeltaPosition.x * MouseSensitivity * TouchSensitivity * 20.0f * k * dt, 0)));
				}
				if (inCabin == true) Rigid2.transform.localRotation = Rigid.rotation * Quaternion.Euler(new Vector3(0, touchDeltaPosition.x * MouseSensitivity * TouchSensitivity * 20.0f * k * dt, 0));
			}
		}

		
        if (Input.GetMouseButton(1) == true)
        {
            if (guimode == true) return;
            rotateValue = new Vector3(Input.GetAxis("Mouse Y") * MouseSensitivity *20.0f *  dt , 0, 0);
			

			//if (MyCamera.transform.eulerAngles.x - rotateValue.x < minYAngle)
			//{
			//	rotateValue.x = 0;
			//}

			
			float kkk = 0;
			if (MyCamera.transform.eulerAngles.x > 180f)
            {
				kkk = 360f;
			}

			//Debug.Log("xxxx=" + (MyCamera.transform.eulerAngles.x - rotateValue.x - kkk));

			if (MyCamera.transform.eulerAngles.x - rotateValue.x - kkk > minYAngle)
			{
				rotateValue.x = 0;
			}
			else if (MyCamera.transform.eulerAngles.x - rotateValue.x - kkk < maxYAngle)
			{
				rotateValue.x = 0;
			}
			else
			{
				MyCamera.transform.eulerAngles = MyCamera.transform.eulerAngles - rotateValue;
			}
			

			{
				Rigid.MoveRotation(Rigid.rotation * Quaternion.Euler(new Vector3(0, Input.GetAxis("Mouse X") * MouseSensitivity * 20.0f * dt, 0)));
			}
			if (inCabin == true)  Rigid2.transform.localRotation = Rigid.rotation * Quaternion.Euler(new Vector3(0, Input.GetAxis("Mouse X") * MouseSensitivity * 20.0f * dt, 0));
		}
		
	}
	 
	private Vector3 CalculateDirection()
	{
		float Formvard2=0;
		if ((Input.GetMouseButton(2) == true) && (Input.GetMouseButton(1) == true)) Formvard2=1.0f  ;
		
	   return new Vector3(Input.GetAxisRaw("Horizontal"), 0f, Input.GetAxisRaw("Vertical") + Formvard2).normalized;
	}

	private Vector3 CalculateDirection3()
	{
		return new Vector3(Strafe, 0 , ForwardBack).normalized;
	}


	private Vector3 CalculateDirection2()
	{
		return new Vector3(0f, 0f, 0f).normalized;
		if (Input.touchCount >= 2)
        {
            // Store both touches.
            Touch touchZero = Input.GetTouch(0);
            Touch touchOne = Input.GetTouch(1);

            // Find the position in the previous frame of each touch.
            Vector2 touchZeroPrevPos = touchZero.position - touchZero.deltaPosition;
            Vector2 touchOnePrevPos = touchOne.position - touchOne.deltaPosition;

            // Find the magnitude of the vector (the distance) between the touches in each frame.
            float prevTouchDeltaMag = (touchZeroPrevPos - touchOnePrevPos).magnitude;
            float touchDeltaMag = (touchZero.position - touchOne.position).magnitude;

            // Find the difference in the distances between each frame.
            float deltaMagnitudeDiff = prevTouchDeltaMag - touchDeltaMag;
			
			return new Vector3(0f, 0f, deltaMagnitudeDiff).normalized;
		}
		return new Vector3(0f, 0f, 0f).normalized;
	}
	
	///////////////////////////////////////////////////////////////////////////////////////////
	
	[SerializeField]
	Transform focus = default;

	[SerializeField, Range(1f, 100f)]
	float distance = 5f;

	[SerializeField, Min(0f)]
	float focusRadius = 5f;

	[SerializeField, Range(0f, 1f)]
	float focusCentering = 0.5f;

	[SerializeField, Range(1f, 360f)]
	float rotationSpeed = 90f;

	[SerializeField, Range(-89f, 89f)]
	float minVerticalAngle = -45f, maxVerticalAngle = 45f;

	[SerializeField, Min(0f)]
	float alignDelay = 5f;

	[SerializeField, Range(0f, 90f)]
	float alignSmoothRange = 45f;

	[SerializeField]
	LayerMask obstructionMask = -1;

	Vector3 focusPoint, previousFocusPoint;

	[SerializeField]
	Vector2 orbitAngles = new Vector2(0f, 0f);//45
	
	public bool BLOCK=true;
	

	float lastManualRotationTime;

	Vector3 CameraHalfExtends 
	{
		get 
		{
			Vector3 halfExtends;
			halfExtends.y =
				MyCamera.nearClipPlane *
				Mathf.Tan(0.5f * Mathf.Deg2Rad * MyCamera.fieldOfView);
			halfExtends.x = halfExtends.y * MyCamera.aspect;
			halfExtends.z = 0f;
			return halfExtends;
		}
	}

	void OnValidate () 
	{
		if (maxVerticalAngle < minVerticalAngle) 
		{
			maxVerticalAngle = minVerticalAngle;
		}
	}

	void Awake () 
	{
		if (focus!=null)
		{
			focusPoint = focus.position;
		}
		//Cursor.visible = false;
		
		//MyCamera.transform.localRotation = Quaternion.Euler(orbitAngles);
	}

	void LateUpdate () 
	{
		if (mode!=1) return;
		
		UpdateFocusPoint();
		Quaternion lookRotation;
		if (ManualRotation() || AutomaticRotation()) {
			ConstrainAngles();
			lookRotation = Quaternion.Euler(orbitAngles);
		}
		else {
			//lookRotation = MyCamera.transform.localRotation;
			lookRotation = Quaternion.Euler(orbitAngles);
		}

		Vector3 lookDirection = lookRotation * Vector3.forward;
		Vector3 lookPosition = focusPoint - lookDirection * distance;

		Vector3 rectOffset = lookDirection * MyCamera.nearClipPlane;
		Vector3 rectPosition = lookPosition + rectOffset;
		Vector3 castFrom = focus.position;
		Vector3 castLine = rectPosition - castFrom;
		float castDistance = castLine.magnitude;
		Vector3 castDirection = castLine / castDistance;

		if (Physics.BoxCast(
			castFrom, CameraHalfExtends, castDirection, out RaycastHit hit,
			lookRotation, castDistance, obstructionMask
		)) 
		{
			if (BLOCK)
			{
				rectPosition = castFrom + castDirection * hit.distance;
				lookPosition = rectPosition - rectOffset;
			}
		}
		
		MyCamera.transform.SetPositionAndRotation(lookPosition, lookRotation);
		//MyCamera.transform.SetPositionAndRotation(MyCamera.transform.localPosition, MyCamera.transform.localRotation);
		
	}

	void UpdateFocusPoint () {
		previousFocusPoint = focusPoint;
		Vector3 targetPoint = focus.position;
		if (focusRadius > 0f) {
			float distance = Vector3.Distance(targetPoint, focusPoint);
			if (distance > focusRadius) {
				focusPoint = Vector3.Lerp(
					targetPoint, focusPoint, focusRadius / distance
				);
			}
			if (distance > 0.01f && focusCentering > 0f) {
				focusPoint = Vector3.Lerp(
					targetPoint, focusPoint,
					Mathf.Pow(1f - focusCentering, Time.unscaledDeltaTime)
				);
			}
		}
		else {
			focusPoint = targetPoint;
		}
	}

	bool ManualRotation () 
	{
		float zoom = Input.GetAxis("Zoom Camera") - Input.mouseScrollDelta.y *4f;
		distance += rotationSpeed * Time.unscaledDeltaTime * zoom /10f;
		
		Vector2 input = new Vector2(
			Input.GetAxis("Vertical Camera"),
			Input.GetAxis("Horizontal Camera")
		);
		const float e = 0.001f;
		if (input.x < -e || input.x > e || input.y < -e || input.y > e) {
			orbitAngles += rotationSpeed * Time.unscaledDeltaTime * input;
			lastManualRotationTime = Time.unscaledTime;
			return true;
		}
		return false;
	}

	bool AutomaticRotation () 
	{		
		if (Time.unscaledTime - lastManualRotationTime < alignDelay) {
			return false;
		}

		Vector2 movement = new Vector2(
			focusPoint.x - previousFocusPoint.x,
			focusPoint.z - previousFocusPoint.z
		);
		float movementDeltaSqr = movement.sqrMagnitude;
		if (movementDeltaSqr < 0.0001f) {
			return false;
		}

		float headingAngle = GetAngle(movement / Mathf.Sqrt(movementDeltaSqr));
		float deltaAbs = Mathf.Abs(Mathf.DeltaAngle(orbitAngles.y, headingAngle));
		float rotationChange =
			rotationSpeed * Mathf.Min(Time.unscaledDeltaTime, movementDeltaSqr);
		if (deltaAbs < alignSmoothRange) {
			rotationChange *= deltaAbs / alignSmoothRange;
		}
		else if (180f - deltaAbs < alignSmoothRange) {
			rotationChange *= (180f - deltaAbs) / alignSmoothRange;
		}
		orbitAngles.y =
			Mathf.MoveTowardsAngle(orbitAngles.y, headingAngle, rotationChange);
		return true;
	}

	void ConstrainAngles () 
	{
		orbitAngles.x =
			Mathf.Clamp(orbitAngles.x, minVerticalAngle, maxVerticalAngle);

		if (orbitAngles.y < 0f) {
			orbitAngles.y += 360f;
		}
		else if (orbitAngles.y >= 360f) {
			orbitAngles.y -= 360f;
		}
	}

	static float GetAngle (Vector2 direction) 
	{
		float angle = Mathf.Acos(direction.y) * Mathf.Rad2Deg;
		return direction.x < 0f ? 360f - angle : angle;
	}


	

	void TouchUpdate()
	{
		

		if (debug == null) return;
		int num = Input.touchCount;
		string debugText = "Input.touchCount=" + num.ToString() + System.Environment.NewLine;
		if (Input.touchCount >= 1)
        {
			if (Input.GetTouch(0).phase == TouchPhase.Began)
            {
				debugText += "Input.GetTouch(0).phase =" + "TouchPhase.Began" + System.Environment.NewLine;
			}
			if (Input.GetTouch(0).phase == TouchPhase.Moved)
			{
				debugText += "Input.GetTouch(0).phase =" + "TouchPhase.Moved" + System.Environment.NewLine;
			}
			if (Input.GetTouch(0).phase == TouchPhase.Stationary)
			{
				debugText += "Input.GetTouch(0).phase =" + "TouchPhase.Stationary" + System.Environment.NewLine;
			}
			if (Input.GetTouch(0).phase == TouchPhase.Ended)
			{
				debugText += "Input.GetTouch(0).phase =" + "TouchPhase.Ended" + System.Environment.NewLine;
			}
			if (Input.GetTouch(0).phase == TouchPhase.Canceled)
			{
				debugText += "Input.GetTouch(0).phase =" + "TouchPhase.Canceled" + System.Environment.NewLine;
			}
			// Get movement of the finger since last frame
			Vector2 touchDeltaPosition = Input.GetTouch(0).deltaPosition;
			debugText += " Input.GetTouch(0).deltaPosition =" + touchDeltaPosition.ToString() + System.Environment.NewLine;
			Touch touch0 = Input.GetTouch(0);
			debugText += " Input.GetTouch(0).position =" + touch0.position + System.Environment.NewLine;

		}

		debugText +=  System.Environment.NewLine;

		if (Input.touchCount >= 2)
		{
			if (Input.GetTouch(1).phase == TouchPhase.Began)
			{
				debugText += "Input.GetTouch(1).phase =" + "TouchPhase.Began" + System.Environment.NewLine;
			}
			if (Input.GetTouch(1).phase == TouchPhase.Moved)
			{
				debugText += "Input.GetTouch(1).phase =" + "TouchPhase.Moved" + System.Environment.NewLine;
			}
			if (Input.GetTouch(1).phase == TouchPhase.Stationary)
			{
				debugText += "Input.GetTouch(1).phase =" + "TouchPhase.Stationary" + System.Environment.NewLine;
			}
			if (Input.GetTouch(1).phase == TouchPhase.Ended)
			{
				debugText += "Input.GetTouch(1).phase =" + "TouchPhase.Ended" + System.Environment.NewLine;
			}
			if (Input.GetTouch(1).phase == TouchPhase.Canceled)
			{
				debugText += "Input.GetTouch(1).phase =" + "TouchPhase.Canceled" + System.Environment.NewLine;
			}
			// Get movement of the finger since last frame
			Vector2 touchDeltaPosition = Input.GetTouch(1).deltaPosition;
			debugText += " Input.GetTouch(1).deltaPosition =" + touchDeltaPosition.ToString() + System.Environment.NewLine;
			Touch touch1 = Input.GetTouch(1);
			debugText += " Input.GetTouch(1).position =" + touch1.position + System.Environment.NewLine;
		}


		/*//////////////////touch//////////////////////////////////
		Touch touchZero = Input.GetTouch(0);
		Touch touchOne = Input.GetTouch(1);

		// Find the position in the previous frame of each touch.
		Vector2 touchZeroPrevPos = touchZero.position - touchZero.deltaPosition;
		Vector2 touchOnePrevPos = touchOne.position - touchOne.deltaPosition;

		// Find the magnitude of the vector (the distance) between the touches in each frame.
		float prevTouchDeltaMag = (touchZeroPrevPos - touchOnePrevPos).magnitude;
		float touchDeltaMag = (touchZero.position - touchOne.position).magnitude;

		// Find the difference in the distances between each frame.
		float deltaMagnitudeDiff = prevTouchDeltaMag - touchDeltaMag;

		
		/*//////////////////////////////////////////////////

		debug.text = debugText;
	}
	


}