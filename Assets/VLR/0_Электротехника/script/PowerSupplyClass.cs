using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class PowerSupplyClass : MonoBehaviour
{
    public ValveClass1 OnOff;
    public ValveClass1 Vvalue;

    public TextMeshProUGUI text1;

    public float maxV = 120;


    public bool On = false;
    public float V = 0;

    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    

    public void Change2D_OnOff(bool value)
    {
        if (value==true)
        {
            OnOff.SetShift(0);
            On = true;
        }
        else
        {
            OnOff.SetShift(1f);
            On = false;
        }
    }

    public void Change2D_value(float value)
    {
        //Debug.Log(value);
        V = value * maxV;
        Vvalue.SetShift(value * Vvalue.maxA);

        text1.text = V.ToString("N");
    }
}
