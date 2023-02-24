using System.Collections;
using System.Collections.Generic;
using System;
using UnityEngine;

public class Helper : MonoBehaviour
{
    public GameObject podskaska;
    public Transform Player;
    public Transform Object;
    public float show_distanse;
    // Start is called before the first frame update
    void Start()
    {

    }

    // Update is called once per frame
    void Update()
    {
        var distanse = Vector3.Distance(Player.transform.position, Object.transform.position);

        if (distanse >= show_distanse)
        {
            podskaska.SetActive(false);
        }
        else
        {
            podskaska.SetActive(true);
        }
    }
}
