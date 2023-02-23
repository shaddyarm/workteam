using System.Collections;
using System.Collections.Generic;
using UnityEngine;
//[ExecuteInEditMode]
public class HTRCompactCable : MonoBehaviour {

	[Header("LineRenderer")]
	public GameObject lineA1;
	public GameObject lineA2;
	public GameObject lineB1;
	public GameObject lineB2;
	public Material lineMat;
	[Header("Point Cable/Line Hook")]
	//Arrow
	public Transform lineArrow1;
	public Transform lineArrow2;
	public Transform lineArrow3;
	public Transform lineArrow4;
	//Hook
	public GameObject lineHook1;
	public GameObject lineHook2;
	public GameObject lineHook3;
	public GameObject lineHook4;
	//LookAt Line
	public Transform lookAtLineArrow;
	public Transform lookAtLineHook;

	void Start(){
		AddLineRen_A ();
	}
	void Update(){
		LineRen_A ();
		LineRen_B ();
		LineRenHook ();
	}
	void LateUpdate(){
		Vector3 lineArrow = lookAtLineHook.position - lookAtLineArrow.position;
		Quaternion rotLineArrow = Quaternion.LookRotation (lineArrow, Vector3.left);
		lookAtLineArrow.localRotation = rotLineArrow;
	}
	public void LineRen_A(){
		lineA1.GetComponent<LineRenderer> ().startWidth = 0.037f;
		lineA1.GetComponent<LineRenderer> ().endWidth = 0.037f;
		Vector3[] lineArrow = new Vector3[2];
		lineArrow [0] = new Vector3 (lineA1.transform.position.x, lineA1.transform.position.y, lineA1.transform.position.z);
		lineArrow [1] = new Vector3 (lineA2.transform.position.x, lineA2.transform.position.y, lineA2.transform.position.z);
		lineA1.GetComponent<LineRenderer> ().positionCount = lineArrow.Length;
		lineA1.GetComponent<LineRenderer> ().SetPositions (lineArrow);
		lineA1.GetComponent<LineRenderer> ().material = lineMat;
	}
	public void LineRen_B(){
		lineB1.GetComponent<LineRenderer> ().startWidth = 0.017f;
		lineB1.GetComponent<LineRenderer> ().endWidth = 0.017f;
		Vector3[] lineArrowS = new Vector3[2];
		lineArrowS [0] = new Vector3 (lineB1.transform.position.x, lineB1.transform.position.y, lineB1.transform.position.z);
		lineArrowS [1] = new Vector3 (lineB2.transform.position.x, lineB2.transform.position.y, lineB2.transform.position.z);
		lineB1.GetComponent<LineRenderer> ().positionCount = lineArrowS.Length;
		lineB1.GetComponent<LineRenderer> ().SetPositions (lineArrowS);
		lineB1.GetComponent<LineRenderer> ().material = lineMat;
	}
	public void LineRenHook(){
		//1
		lineHook1.GetComponent<LineRenderer> ().startWidth = 0.037f;
		lineHook1.GetComponent<LineRenderer> ().endWidth = 0.037f;
		Vector3[] line1 = new Vector3[2];
		line1 [0] = new Vector3 (lineHook1.transform.position.x, lineHook1.transform.position.y, lineHook1.transform.position.z);
		line1 [1] = new Vector3 (lineArrow1.transform.position.x, lineArrow1.transform.position.y, lineArrow1.transform.position.z);
		lineHook1.GetComponent<LineRenderer> ().positionCount = line1.Length;
		lineHook1.GetComponent<LineRenderer> ().SetPositions (line1);
		lineHook1.GetComponent<LineRenderer> ().material = lineMat;
		//2
		lineHook2.GetComponent<LineRenderer> ().startWidth = 0.037f;
		lineHook2.GetComponent<LineRenderer> ().endWidth = 0.037f;
		Vector3[] line2 = new Vector3[2];
		line2 [0] = new Vector3 (lineHook2.transform.position.x, lineHook2.transform.position.y, lineHook2.transform.position.z);
		line2 [1] = new Vector3 (lineArrow2.transform.position.x, lineArrow2.transform.position.y, lineArrow2.transform.position.z);
		lineHook2.GetComponent<LineRenderer> ().positionCount = line2.Length;
		lineHook2.GetComponent<LineRenderer> ().SetPositions (line2);
		lineHook2.GetComponent<LineRenderer> ().material = lineMat;
		//3
		lineHook3.GetComponent<LineRenderer> ().startWidth = 0.037f;
		lineHook3.GetComponent<LineRenderer> ().endWidth = 0.037f;
		Vector3[] line3 = new Vector3[2];
		line3 [0] = new Vector3 (lineHook3.transform.position.x, lineHook3.transform.position.y, lineHook3.transform.position.z);
		line3 [1] = new Vector3 (lineArrow3.transform.position.x, lineArrow3.transform.position.y, lineArrow3.transform.position.z);
		lineHook3.GetComponent<LineRenderer> ().positionCount = line3.Length;
		lineHook3.GetComponent<LineRenderer> ().SetPositions (line3);
		lineHook3.GetComponent<LineRenderer> ().material = lineMat;
		//4
		lineHook4.GetComponent<LineRenderer> ().startWidth = 0.037f;
		lineHook4.GetComponent<LineRenderer> ().endWidth = 0.037f;
		Vector3[] line4 = new Vector3[2];
		line4 [0] = new Vector3 (lineHook4.transform.position.x, lineHook4.transform.position.y, lineHook4.transform.position.z);
		line4 [1] = new Vector3 (lineArrow4.transform.position.x, lineArrow4.transform.position.y, lineArrow4.transform.position.z);
		lineHook4.GetComponent<LineRenderer> ().positionCount = line4.Length;
		lineHook4.GetComponent<LineRenderer> ().SetPositions (line4);
		lineHook4.GetComponent<LineRenderer> ().material = lineMat;
	}
	public void AddLineRen_A(){
		lineA1.AddComponent<LineRenderer> ();
		lineB1.AddComponent<LineRenderer> ();
		lineHook1.AddComponent<LineRenderer> ();
		lineHook2.AddComponent<LineRenderer> ();
		lineHook3.AddComponent<LineRenderer> ();
		lineHook4.AddComponent<LineRenderer> ();
	}
}
