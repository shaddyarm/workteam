using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Valve.VR;

public class My_VR_ActionScript2 : MonoBehaviour
{
    // a reference to the hand
    public SteamVR_Input_Sources handType;

    // a reference to the action
    public SteamVR_Action_Boolean booleanAction;
    // a reference to the action
    //public SteamVR_Action_Single singleAction;

    //reference to the sphere
    public GameObject Sphere;

    void Start()
    {
        booleanAction.AddOnStateDownListener(TriggerDown, handType);
        booleanAction.AddOnStateUpListener(TriggerUp, handType);

        //singleAction.AddOnChangeListener(ActionChanged, handType);


    }
    public void TriggerUp(SteamVR_Action_Boolean fromAction, SteamVR_Input_Sources fromSource)
    {
        //Debug.Log("Trigger is up");
        Sphere.GetComponent<MeshRenderer>().enabled = false;
    }
    public void TriggerDown(SteamVR_Action_Boolean fromAction, SteamVR_Input_Sources fromSource)
    {
        //Debug.Log("Trigger is down");
        Sphere.GetComponent<MeshRenderer>().enabled = true;
    }

    //public void ActionChanged(SteamVR_Action_Single fromAction, SteamVR_Input_Sources fromSource)
    //{
    //    Debug.Log("Trigger value2:" + SteamVR_Actions._default.Squeeze.GetAxis(handType).ToString());
    //}

    void Update()
    {
        //Debug.Log("Trigger value:" + SteamVR_Actions._default.Squeeze.GetAxis(handType).ToString());
    }
}