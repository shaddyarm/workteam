using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class SoprotivlenieClass : MonoBehaviour
{
    public TextMeshProUGUI textR1;

    public float maxR1 = 1000f;


    public float R = 1f;

    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    
    public void Change2D_R1_value(float value)
    {
        //Debug.Log(value);
        R = 1f + value * maxR1;
        textR1.text = R.ToString("N");
    }
}
