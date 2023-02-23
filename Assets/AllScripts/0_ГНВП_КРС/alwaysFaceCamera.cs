using System.Collections;
 using System.Collections.Generic;
 using UnityEngine;
 
 public class alwaysFaceCamera : MonoBehaviour {
 
 
     private Transform camera;
 
 
     // Use this for initialization
     void Start () {
 
         camera = Camera.main.transform;
 
     }
 
     // Update is called once per frame
     void Update()
     {
        // Rotate the camera every frame so it keeps looking at the target
        //transform.LookAt(camera);
		 //transform.LookAt(camera, Vector3.left);
		 
		Vector3 targetPostition = new Vector3( camera.position.x, 
                                        this.transform.position.y, 
                                        camera.position.z ) ;
		this.transform.LookAt( targetPostition ) ;
     }
 
 }