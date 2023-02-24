using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Cargo : MonoBehaviour {

	public HTRCompactHook scriptM2;
	public MenuSceneSym scriptM3;
	public KeyCode connectedKey;
	[HideInInspector]
	public bool connectedS_Bool = true;
	private Rigidbody m_rigidbody;
	private float m_distanceHook;
	[HideInInspector]
	public bool blockKey = true;
	private bool OnTrailer = false;
	[HideInInspector]
	public bool onColTrailer_Bool = false;
	[Header("Line Renderer")]
	[HideInInspector]
	public Transform m_PointLineHook;
	public Material matLine;
	public float m_StartWidth;
	public float m_EndWidth;
	public Transform pointLine1;
	public Transform pointLine2;
	public Transform pointLine3;
	public Transform pointLine4;

	void Update(){
		if (Input.GetKeyDown (connectedKey) && blockKey == true) {
		if (scriptM2.nameSupportPaltform == this.gameObject.name && scriptM3.canvasCompact.enabled == true) {
				m_PointLineHook = scriptM2.pointLineHook;
				m_rigidbody = scriptM2.gameObject.GetComponent<Rigidbody> ();
				m_distanceHook = scriptM2.hitSupportPlatform.distance;
				ConnectedS ();
				scriptM2.nameSupportPaltform_Bool = false;
				blockKey = false;
			}
		} else if (Input.GetKeyDown (connectedKey) && blockKey == false) {
			if (scriptM2.nameSupportPaltform == gameObject.name && connectedS_Bool == false && scriptM3.canvasCompact.enabled == true) {
				ConnectedS ();
				scriptM2.nameSupportPaltform_Bool = true;
				blockKey = true;
			}
		}
		if (connectedS_Bool == false) {
			LineRen ();
		}
		Ray ray = new Ray (transform.position, -Vector3.up);
		int layerTrailer = (1 << 11);
		RaycastHit hit;
		if (Physics.Raycast (ray, out hit, 4, layerTrailer) && connectedS_Bool == true) {
			OnTrailer = true;
		} else
			OnTrailer = false;
		if (OnTrailer == true && this.gameObject.GetComponent<LineRenderer> () == null && onColTrailer_Bool == true && connectedS_Bool == true) {
			transform.SetParent (hit.transform);
			Destroy (gameObject.GetComponent<ConstantForce> ());
			Destroy (gameObject.GetComponent<Rigidbody> ());
		}
	}
	void FixedUpdate(){
		if (connectedS_Bool == false) {
			this.gameObject.GetComponent<HingeJoint> ().connectedAnchor = m_PointLineHook.localPosition;
		}
	}
	void OnCollisionEnter(Collision col){
		if (col.collider.tag == "OnCargoTrailer" || col.collider.tag == "Platform") {
			onColTrailer_Bool = true;
		}
	}
	void OnCollisionExit(Collision col){
		if (col.collider.tag == "OnCargoTrailer" || col.collider.tag == "Platform") {
			onColTrailer_Bool = false;
		}
	}
	public void ConnectedS(){
		if (connectedS_Bool == true) {
			this.transform.parent = null;
			m_PointLineHook.gameObject.AddComponent<LineRenderer> ();
			if (this.gameObject.GetComponent<Rigidbody> () == null) {
				Rigidbody rig = gameObject.AddComponent<Rigidbody> ();
				rig.mass = 50;
				rig.drag = 2f;
			}
			HingeJoint hin = gameObject.AddComponent<HingeJoint> ();
			hin.connectedBody = m_rigidbody;
			hin.axis = new Vector3 (0, 1, 0);
			hin.anchor = new Vector3 (0, m_distanceHook, 0);
			hin.autoConfigureConnectedAnchor = false;
			foreach (Transform m_layer in GetComponentInChildren<Transform>(true)) {
				m_layer.gameObject.layer = 8;
			}
			gameObject.layer = 8;
			if (scriptM2.connectedIm.enabled == true) {
				scriptM2.connectedIm.enabled = false;
			}
			if (this.gameObject.GetComponent<ConstantForce> () != null) {
				Destroy (this.gameObject.GetComponent<ConstantForce> ());
			}
			connectedS_Bool = false;
		} else if (connectedS_Bool == false && OnTrailer == false) {
			Destroy (gameObject.GetComponent<HingeJoint> ());
			Destroy (m_PointLineHook.GetComponent<LineRenderer> ());
			ConstantForce cons = gameObject.AddComponent<ConstantForce> ();
			cons.force = new Vector3 (0, -0.02f, 0);
			gameObject.layer = 12;
			foreach (Transform m_layer in GetComponentInChildren<Transform>(true)) {
				m_layer.gameObject.layer = 0;
			}
			connectedS_Bool = true;
		}
	}
	public void LineRen(){
		m_PointLineHook.GetComponent<LineRenderer> ().startWidth = m_StartWidth;
		m_PointLineHook.GetComponent<LineRenderer> ().endWidth = m_EndWidth;
		Vector3[] line = new Vector3[8];
		line [0] = new Vector3 (m_PointLineHook.position.x, m_PointLineHook.position.y, m_PointLineHook.position.z);
		line [1] = new Vector3 (pointLine1.position.x, pointLine1.position.y, pointLine1.position.z);
		line [2] = new Vector3 (m_PointLineHook.position.x, m_PointLineHook.position.y, m_PointLineHook.position.z);
		line [3] = new Vector3 (pointLine2.position.x, pointLine2.position.y, pointLine2.position.z);
		line [4] = new Vector3 (m_PointLineHook.position.x, m_PointLineHook.position.y, m_PointLineHook.position.z);
		line [5] = new Vector3 (pointLine3.position.x, pointLine3.position.y, pointLine3.position.z);
		line [6] = new Vector3 (m_PointLineHook.position.x, m_PointLineHook.position.y, m_PointLineHook.position.z);
		line [7] = new Vector3 (pointLine4.position.x, pointLine4.position.y, pointLine4.position.z);
		m_PointLineHook.GetComponent<LineRenderer> ().positionCount = line.Length;
		m_PointLineHook.GetComponent<LineRenderer> ().SetPositions (line);
		m_PointLineHook.GetComponent<LineRenderer> ().material = matLine;
	}
}
