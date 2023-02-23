using UnityEngine;
using UnityEngine.UI;
using UnityEngine.EventSystems;// Required when using Event data.

//https://www.mvcode.com/lessons/first-person-camera-and-controller-jamie

public class PlayPanorama : MonoBehaviour {
 
    float horizontal=270f;
	float vertical=0;
	float turnSpeedMouse=0.1f;
	public Transform container;
	public Transform container2;
	

	
	float oldx,oldy;
	bool stop=false;
	
	void Start()
	{
		oldx = Input.mousePosition.x;
			oldy = Input.mousePosition.y;
			stop=false;
	}
	
	void LateUpdate () 
	{
		
		if (Input.GetMouseButtonUp(0))
		{
			stop=true;
			oldx = Input.mousePosition.x;
			oldy = Input.mousePosition.y;
		}
		
		if (Input.GetMouseButtonDown(0))
		{
			if (stop==true) 
			{
				stop=false;
				oldx = Input.mousePosition.x;
				oldy = Input.mousePosition.y;
			}
		}
		
		
		
		//Using mouse
		if (Input.GetMouseButton(0))
		{
			float dx = (Input.mousePosition.x  - oldx);
			float dy = (Input.mousePosition.y  - oldy);
			if (Mathf.Abs (dx)<0.1f) dx = 0;
			if (Mathf.Abs (dy)<0.1f) dy = 0;

			horizontal += dx*Time.deltaTime*turnSpeedMouse;
			vertical += dy*Time.deltaTime*turnSpeedMouse;
			
			if (vertical>60f) vertical=60f;
			if (vertical<-60f) vertical=-60f;
		}
		
			
		container.localRotation = Quaternion.Euler(vertical*(-1), 0, 0); 

		container2.localRotation = Quaternion.Euler(0, horizontal, 0);
		
	}


	
}