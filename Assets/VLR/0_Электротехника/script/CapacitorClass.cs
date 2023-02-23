using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class CapacitorClass : MonoBehaviour
{
     public ValveClass1 Vvalue;

    public TextMeshProUGUI text1;

    public float maxR = 1f;


    public float R = 0;

    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    
    public void Change2D_value(float value)
    {
        //Debug.Log(value);
        R = value * maxR;
        Vvalue.SetShift(value * Vvalue.maxA);

        text1.text = R.ToString("N");
    }
}
