using System.Collections;
using System.Collections.Generic;
using System;
using UnityEngine;

public class Helper : MonoBehaviour
{
    public GameObject podskaska_whatis_off;
    public Transform Player;
    public Transform Object;
    public GameObject Editor;
    public float show_distanse;
    public float PlayerView;
    void Start()
    {
     
    }



void Update()
    {
        podskaska_whatis_off.transform.LookAt(Player);
        podskaska_whatis_off.transform.Rotate(0, 180, 0);
        var distanse = Vector3.Distance(Player.transform.position, Object.transform.position);
        if (!Editor.GetComponent<ScenarioEditor>().exam_mode)
        {
            if(distanse >= show_distanse)
            {
                podskaska_whatis_off.SetActive(false);
            }
            else
            {
                podskaska_whatis_off.SetActive(true);
            }
        }  
    }
}
