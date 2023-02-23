
using UnityEngine;
using System;
using System.Collections;

//[ExecuteInEditMode]
[RequireComponent(typeof(Animator))] 

[ExecuteAlways]
public class Scenario_IK : MonoBehaviour {
    
    protected Animator animator;
    
    public bool ikActive = false;
	
    public Transform rightHandObj = null;
	public Transform leftHandObj = null;
	public Transform rightFootObj = null;
	public Transform leftFootObj = null;
    public Transform lookObj = null;
	

	
	
	public void SetActive(bool value)
	{
		ikActive=value;
	}
	
	public void ResetAll()
	{
		rightHandObj = null;
		leftHandObj = null;
		rightFootObj = null;
		leftFootObj = null;

		lookObj = null;
	}
	
		
	public void SetLeftHandObj(Transform value)
	{
		leftHandObj=value;
	}
	
	public void SetRightHandObj(Transform value)
	{
		rightHandObj=value;
	}
	
	public void SetRightFootObj(Transform value)
	{
		rightFootObj=value;
	}
	
	public void SetLeftFootObj(Transform value)
	{
		leftFootObj=value;
	}
	
	public void SetLookObj(Transform value)
	{
		lookObj=value;
	}
	
	public void SetLayerWeight_1(int index)
	{
		animator.SetLayerWeight(index,1f);
	}
	
	public void SetLayerWeight_0(int index)
	{
		animator.SetLayerWeight(index,0f);
	}
	
	public void SetPosition(Transform value)
	{
		animator.transform.position = value.position;
		animator.transform.rotation = value.rotation;
	}
	
	

    void Start () 
    {
        animator = GetComponent<Animator>();
		//animator.SetLayerWeight(1,1f);
    }
    
    //a callback for calculating IK
    void OnAnimatorIK()
    {
        if(animator) {
            
            //if the IK is active, set the position and rotation directly to the goal. 
            if(ikActive) 
			{

                // Set the look target position, if one has been assigned
                if(lookObj != null) {
                    animator.SetLookAtWeight(1);
                    animator.SetLookAtPosition(lookObj.position);
                }    

                // Set the right hand target position and rotation, if one has been assigned
                if(rightHandObj != null) {
                    animator.SetIKPositionWeight(AvatarIKGoal.RightHand,1);
                    animator.SetIKRotationWeight(AvatarIKGoal.RightHand,1);  
                    animator.SetIKPosition(AvatarIKGoal.RightHand,rightHandObj.position);
                    animator.SetIKRotation(AvatarIKGoal.RightHand,rightHandObj.rotation);
                }        
				
				// Set the left hand target position and rotation, if one has been assigned
                if(leftHandObj != null) {
                    animator.SetIKPositionWeight(AvatarIKGoal.LeftHand,1);
                    animator.SetIKRotationWeight(AvatarIKGoal.LeftHand,1);  
                    animator.SetIKPosition(AvatarIKGoal.LeftHand,leftHandObj.position);
                    animator.SetIKRotation(AvatarIKGoal.LeftHand,leftHandObj.rotation);
                } 
				
				// Set the left hand target position and rotation, if one has been assigned
                if(leftFootObj != null) {
                    animator.SetIKPositionWeight(AvatarIKGoal.LeftFoot,1);
                    animator.SetIKRotationWeight(AvatarIKGoal.LeftFoot,1);  
                    animator.SetIKPosition(AvatarIKGoal.LeftFoot,leftFootObj.position);
                    animator.SetIKRotation(AvatarIKGoal.LeftFoot,leftFootObj.rotation);
                } 
				
				// Set the left hand target position and rotation, if one has been assigned
                if(rightFootObj != null) {
                    animator.SetIKPositionWeight(AvatarIKGoal.RightFoot,1);
                    animator.SetIKRotationWeight(AvatarIKGoal.RightFoot,1);  
                    animator.SetIKPosition(AvatarIKGoal.RightFoot,rightFootObj.position);
                    animator.SetIKRotation(AvatarIKGoal.RightFoot,rightFootObj.rotation);
                } 
                
            }
            //if the IK is not active, set the position and rotation of the hand and head back to the original position
            else {          
                animator.SetIKPositionWeight(AvatarIKGoal.LeftHand,0);
                animator.SetIKRotationWeight(AvatarIKGoal.LeftHand,0);
				
				animator.SetIKPositionWeight(AvatarIKGoal.RightHand,0);
                animator.SetIKRotationWeight(AvatarIKGoal.RightHand,0);
				
				animator.SetIKPositionWeight(AvatarIKGoal.LeftFoot,0);
                animator.SetIKRotationWeight(AvatarIKGoal.LeftFoot,0);
				
				animator.SetIKPositionWeight(AvatarIKGoal.RightFoot,0);
                animator.SetIKRotationWeight(AvatarIKGoal.RightFoot,0);
				

				
                animator.SetLookAtWeight(0);
            }
        }
    }    
}



