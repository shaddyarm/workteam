using System.Collections;
using System.Collections.Generic;
using System;
using UnityEngine;

public class Helper : MonoBehaviour
{
    public GameObject podskaska_whatis_off;

    public Transform Player;
    public Transform Object;
    private float PlayerView;
    public float show_distanse;
    public GameObject Editor;
    // Start is called before the first frame update
    void Start()
    {


    }



// Update is called once per frame
void Update()
    {


        var distanse = Vector3.Distance(Player.transform.position, Object.transform.position);

        podskaska_whatis_off.transform.LookAt(Player);
        podskaska_whatis_off.transform.Rotate(0, 180, 0);

        if (!Editor.GetComponent<ScenarioEditor>().exam_mode)
        {
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
}
