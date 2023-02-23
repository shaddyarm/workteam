using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.Events;
using UnityEngine.EventSystems;
using Valve.VR;
using Valve.VR.InteractionSystem;

[RequireComponent( typeof( Valve.VR.InteractionSystem.Interactable ) )]
public class NewNewButton2VR : MonoBehaviour 
{
	
	// a reference to the action
	public SteamVR_Action_Boolean Reference_to_action_Press;
	// a reference to the hand
	public SteamVR_Input_Sources Reference_to_hand;
	
	private Interactable interactable;
	
	
	
	public GameObject MY;
	
	public UnityEvent myEventDown;

	public bool Enabled = true;
	bool near=false;
	
	private Button ui_button = null;
	private NewNewButton2 button2 = null;

	Hand temp_hand;
	
	
	//понты
	public SteamVR_Skeleton_Poser skeletonPoser;

	//
	public bool noclick = false;




	public void SetEnabled(bool value)
	{
		Enabled=value;
	}
	

	void Awake()
	{
		interactable = this.GetComponent<Interactable>();
	}
	
	// Called every Update() while a Hand is hovering over this object
	private void HandHoverUpdate( Hand hand )
	{
	}
	
	// Called when a Hand starts hovering over this object
	private void OnHandHoverBegin( Hand hand )
	{
		if (skeletonPoser!=null)
		{
			hand.skeleton.BlendToPoser(skeletonPoser);
			temp_hand = hand;
		}
		
		near=true;

		if (noclick==true) click_1();
	}

	// Called when a Hand stops hovering over this object
	private void OnHandHoverEnd( Hand hand )
	{
		if (skeletonPoser!=null)
		{
			hand.skeleton.BlendToSkeleton();
			temp_hand = null;
		}
		near=false;

	}
	
	// Use this for initialization
	void Start () 
	{
		ui_button = gameObject.GetComponent<Button>();
		button2 = gameObject.GetComponent<NewNewButton2>();
		Reference_to_action_Press.AddOnStateDownListener(Up_TriggerDown, Reference_to_hand);
		Reference_to_action_Press.AddOnStateUpListener(Up_TriggerUp, Reference_to_hand);
	}
	
	public void Up_TriggerUp(SteamVR_Action_Boolean fromAction, SteamVR_Input_Sources fromSource)
	{
		
	}

	public void Up_TriggerDown(SteamVR_Action_Boolean fromAction, SteamVR_Input_Sources fromSource)
	{
		click_1();
	}


	void click_1()
    {
		if (Enabled == false) return;
		if (near == false) return;

		if (temp_hand != null) OnHandHoverEnd(temp_hand);

		myEventDown.Invoke();
		if (ui_button != null)
		{
			ui_button.onClick.Invoke();
		}

		if (button2 != null)
        {
			button2.myEventDown.Invoke();
		}
	}
	
}

	
	