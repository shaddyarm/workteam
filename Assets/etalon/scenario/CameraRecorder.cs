using UnityEngine;
 using System.Collections;
 
 public class CameraRecorder : MonoBehaviour 
 {
	 public Transform Camera;
     public Transform target;
     
     // Update is called once per frame
     void Update () 
	 {
         Camera.LookAt(target);
     }
 }