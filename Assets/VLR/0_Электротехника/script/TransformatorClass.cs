using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class TransformatorClass : MonoBehaviour
{
    public TextMeshProUGUI textL1;
    public TextMeshProUGUI textL2;

    public float maxL1 = 100f;
    public float maxL2 = 100f;

    public float L1 = 1f;
    public float L2 = 1f;

    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    
    public void Change2D_L1_value(float value)
    {
        //Debug.Log(value);
        L1 = 1f + value * maxL1;
        textL1.text = L1.ToString("N");
    }

    public void Change2D_L2_value(float value)
    {
        //Debug.Log(value);
        L2 = 1f + value * maxL2;
        textL2.text = L2.ToString("N");
    }
}
