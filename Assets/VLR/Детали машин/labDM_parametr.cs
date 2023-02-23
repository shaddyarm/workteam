using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class labDM_parametr : MonoBehaviour
{
    public Text valueText;
    public int min;
    public int max;
    public int value;

    public void Plus()
    {
        value++;
        if (value > max) value = max;
        valueText.text = value.ToString("N0");
    }

    public void Minus()
    {
        value--;
        if (value < min) value = min;
        valueText.text = value.ToString("N0");
    }

}
