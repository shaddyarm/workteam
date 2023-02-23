using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class ReostatClass : MonoBehaviour
{
     public ValveClass1 Vvalue;

    public TextMeshProUGUI text1;

    public float maxR = 10f;


    public float R = 1f;

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
        R = 1f + value * maxR;
        Vvalue.SetShift(value * Vvalue.maxA);

        text1.text = R.ToString("N");
    }
}
