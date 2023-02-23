using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Valve.VR;

public class My_VR_ActionScript : MonoBehaviour
{
	//public Play player;
	
	// a reference to the action
	public SteamVR_Action_Boolean Reference_to_action_up;
	// a reference to the hand
	public SteamVR_Input_Sources Reference_to_hand_up;
	
	// a reference to the action
	public SteamVR_Action_Boolean Reference_to_action_down;
	// a reference to the hand
	public SteamVR_Input_Sources Reference_to_hand_down;
	
	
	// a reference to the action
	public SteamVR_Action_Boolean Reference_to_action_step_forward;
	public SteamVR_Action_Boolean Reference_to_action_step_back;
	public SteamVR_Action_Boolean Reference_to_action_step_left;
	public SteamVR_Action_Boolean Reference_to_action_step_right;
	// a reference to the hand
	public SteamVR_Input_Sources Reference_to_hand_step;
	
	
	bool down=false;
	bool up=false;
	
	bool forward=false;
	bool back=false;
	bool left=false;
	bool right=false;
	

	void Start()
	{
		Reference_to_action_up.AddOnStateDownListener(Up_TriggerDown, Reference_to_hand_up);
		Reference_to_action_up.AddOnStateUpListener(Up_TriggerUp, Reference_to_hand_up);
		
		Reference_to_action_down.AddOnStateDownListener(Down_TriggerDown, Reference_to_hand_down);
		Reference_to_action_down.AddOnStateUpListener(Down_TriggerUp, Reference_to_hand_down);
		
		
		
		Reference_to_action_step_forward.AddOnStateDownListener(StepForward_TriggerDown, Reference_to_hand_step);
		Reference_to_action_step_forward.AddOnStateUpListener(StepForward_TriggerUp, Reference_to_hand_step);
		
		Reference_to_action_step_back.AddOnStateDownListener(StepBack_TriggerDown, Reference_to_hand_step);
		Reference_to_action_step_back.AddOnStateUpListener(StepBack_TriggerUp, Reference_to_hand_step);
		
		Reference_to_action_step_left.AddOnStateDownListener(StepLeft_TriggerDown, Reference_to_hand_step);
		Reference_to_action_step_left.AddOnStateUpListener(StepLeft_TriggerUp, Reference_to_hand_step);
		
		Reference_to_action_step_right.AddOnStateDownListener(StepRight_TriggerDown, Reference_to_hand_step);
		Reference_to_action_step_right.AddOnStateUpListener(StepRight_TriggerUp, Reference_to_hand_step);
		
	}

	public void Up_TriggerUp(SteamVR_Action_Boolean fromAction, SteamVR_Input_Sources fromSource)
	{
		up=false;
	}

	public void Up_TriggerDown(SteamVR_Action_Boolean fromAction, SteamVR_Input_Sources fromSource)
	{
		up=true;
		
	}
	
	public void Down_TriggerUp(SteamVR_Action_Boolean fromAction, SteamVR_Input_Sources fromSource)
	{
		down=false;
	}

	public void Down_TriggerDown(SteamVR_Action_Boolean fromAction, SteamVR_Input_Sources fromSource)
	{
		down=true;
		
	}
	
	//
	public void StepForward_TriggerDown(SteamVR_Action_Boolean fromAction, SteamVR_Input_Sources fromSource)
	{
		forward=true;
	}
	public void StepForward_TriggerUp(SteamVR_Action_Boolean fromAction, SteamVR_Input_Sources fromSource)
	{
		forward=false;
	}
	//
	public void StepBack_TriggerDown(SteamVR_Action_Boolean fromAction, SteamVR_Input_Sources fromSource)
	{
		back=true;
	}
	public void StepBack_TriggerUp(SteamVR_Action_Boolean fromAction, SteamVR_Input_Sources fromSource)
	{
		back=false;
	}
	//
	public void StepLeft_TriggerDown(SteamVR_Action_Boolean fromAction, SteamVR_Input_Sources fromSource)
	{
		left=true;
	}
	public void StepLeft_TriggerUp(SteamVR_Action_Boolean fromAction, SteamVR_Input_Sources fromSource)
	{
		left=false;
	}
	//
	public void StepRight_TriggerDown(SteamVR_Action_Boolean fromAction, SteamVR_Input_Sources fromSource)
	{
		right=true;
	}
	public void StepRight_TriggerUp(SteamVR_Action_Boolean fromAction, SteamVR_Input_Sources fromSource)
	{
		right=false;
	}
	/*
		
	*/
	
	/*
	GameObject PlayerCamera = GameObject.Find("VRCamera");  //get the VRcamera object
	Vector3 GlobalCameraPosition = PlayerCamera.transform.position;  //get the global position of VRcamera
	Vector3 GlobalPlayerPosition = this.transform.position;
	Vector3 GlobalOffsetCameraPlayer = new Vector3(GlobalCameraPosition.x - GlobalPlayerPosition.x, 0, GlobalCameraPosition.z - GlobalPlayerPosition.z);
	Vector3 newRigPosition = new Vector3(welcome_start_position_object.transform.position.x - GlobalOffsetCameraPlayer.x, this.transform.position.y, welcome_start_position_object.transform.position.z - GlobalOffsetCameraPlayer.z);
			this.transform.position = newRigPosition;
	*/
	

	void Update()
	{
		if (up) {}//player.MoveUp();
		if (down) {}//player.MoveDown();
		
		
//		Player player = Player.instance;
//		Vector3 playerFeetOffset = player.trackingOriginTransform.position - player.feetPositionGuess;
		
		
		
//		player.trackingOriginTransform.position -= playerFeetOffset;
		//player.transform.Rotate(Vector3.up, angle);
		//playerFeetOffset = Quaternion.Euler(0.0f, angle, 0.0f) * playerFeetOffset;
//		player.trackingOriginTransform.position += playerFeetOffset;
		
	}

}