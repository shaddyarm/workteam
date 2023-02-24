using System.Collections;
using System.Collections.Generic;
using System;
using UnityEngine;

public class Helper : MonoBehaviour
{
    public GameObject podskaska_whatis_off;

    public Transform Player;
    public Transform Object;

    public float show_distanse;
    // Start is called before the first frame update
    void Start()
    {
       // PlayerforTargeting = GameObject.Find("Player");
    }



// Update is called once per frame
void Update()
    {
        var distanse = Vector3.Distance(Player.transform.position, Object.transform.position);

        if (distanse >= show_distanse)
        {
            podskaska_whatis_off.SetActive(false);
        }
        else
        {
            podskaska_whatis_off.SetActive(true);
        }


    }
}
