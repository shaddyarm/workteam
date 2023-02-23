using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;
using TMPro;

public class lab6_big_script : MonoBehaviour
{
    public GameObject tumbler, text_pribor, obrazec;
    public double rotation_obrazec, rotation, rotation_x;
    public decimal display_rotation;

    // Start is called before the first frame update
    void Start()
    {
        text_pribor = GameObject.Find("Text_on_chastotomer");
    }

    public void SetTumbler(bool x)
    {
        if (x)
        {
            tumbler = GameObject.Find("tumbler1");
           
        }
        else
        {
            tumbler = GameObject.Find("ch_per_avt");
        }
    }

    public void setobrazec(GameObject obj) 
    {
        obrazec = obj;
    }

    // Update is called once per frame
    void Update()
    {
        if (Input.GetKey(KeyCode.Mouse0) && tumbler.name == "tumbler1")
        {
            rotation += Input.GetAxis("Mouse X");
            tumbler.transform.localRotation = Quaternion.Euler(0f, 0f, Input.GetAxis("Mouse X"));

            if (obrazec.name == "gruz03_visit" && rotation > 30 && rotation < 60)
            {
                rotation_obrazec += Input.GetAxis("Mouse X");
            }
            else if (obrazec.name == "gruz02_visit" && rotation > 45 && rotation < 75)
            {
                rotation_obrazec += Input.GetAxis("Mouse X") * 1.5;
            }
            else if (obrazec.name == "gruz01_visit" && rotation > 60)
            {
                rotation_obrazec += Input.GetAxis("Mouse X") * 2;
            }
            
            if (obrazec.name == "gruz03_visit" || obrazec.name == "gruz02_visit" || obrazec.name == "gruz01_visit")
            {

            }


        }

        rotation_x += rotation / 30;

        obrazec.transform.localRotation = Quaternion.Euler((float)rotation_x, 90f, (float)rotation_obrazec);

        if (rotation < 0)
        {
            rotation = 0;
        } else if (rotation > 90)
        {
            rotation = 90;
        }
        else if (rotation > 0)
        {

        }    

        display_rotation = Math.Round((decimal)rotation);

        text_pribor.GetComponent<TextMeshPro>().text = display_rotation.ToString();
    }
}
