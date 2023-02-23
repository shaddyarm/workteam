using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class SwitchClass : MonoBehaviour
{
    public ValveClass1 Vvalue;

    public TextMeshProUGUI text1;

    public float Position = 1;

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
        Position = value;


        Vvalue.SetShift((value-1f) * 90f);

        text1.text = Position.ToString("N0");
    }
}
