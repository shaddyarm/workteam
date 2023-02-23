using UnityEngine;
using UnityEngine.UI;
using UnityEngine.EventSystems;// Required when using Event data.

//https://www.mvcode.com/lessons/first-person-camera-and-controller-jamie

public class Play1 : MonoBehaviour {
 
    #region "Variables"
    public GameObject Rigid;
	public GameObject MyCamera;
    public float MouseSensitivity;
    public float MoveSpeed;
    public float JumpForce;
	
	
	//public AudioSource foodstepSound;
	//public bool walkSound;
    
    #endregion
   
    private Vector3 rotateValueY;
	private Vector3 rotateValueX;
   
	Vector3 firstPoint;
	
	float XX,YY;
  

    // Use this for initialization
    void Start()
    {
      XX=180f;
	  YY=0;
    }
    
	
	
	
	
	public float sensitivity = 10f;
    public float maxYAngle = 40f;
    private Vector2 currentRotation;
	
	private Vector3 force;
		 
	void Update()
	{
		
	}
	 
	void FixedUpdate()
	{
		
	    if ((Input.touchCount >= 2)) 
		{
			Touch touchOne = Input.GetTouch(1);
			if ( touchOne.deltaPosition.y < 0f) Rigid.transform.Translate(Vector3.forward * Time.deltaTime);
			if ( touchOne.deltaPosition.y > 0f) Rigid.transform.Translate(Vector3.back * Time.deltaTime);
		}
		
		
		
		if (Input.touchCount > 0)
        {
            if (Input.GetTouch(0).phase == TouchPhase.Began)
            {
                firstPoint = Input.GetTouch(0).position;
            }
			if (Input.GetTouch(0).phase == TouchPhase.Moved)
            {
                Vector3 secondPoint = Input.GetTouch(0).position;
                float x = secondPoint.x - firstPoint.x;
                float y = secondPoint.y - firstPoint.y;
                firstPoint = secondPoint;
				
				XX-=x*0.02f;
				YY+=y*0.01f;
				if (YY < -maxYAngle) YY=-40;
				if (YY > maxYAngle) YY=40;
            }
		}
	   

		rotateValueY = new Vector3(YY , XX, 0);
		MyCamera.transform.eulerAngles =  rotateValueY;
		
		rotateValueX = new Vector3(0 , XX, 0);
		Rigid.transform.eulerAngles =  rotateValueX;
		
		
	}
	 

	
	


	
}