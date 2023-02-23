using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;
using UnityEngine.UI;
using TMPro;

public class Timer : MonoBehaviour
{
    private int sec = 0;
    private int min = 0;
    public TextMeshPro timerText;
    private IEnumerator coroutine;

    private IEnumerator WaitAndPrint(float waitTime)
    {
        while (true)
        {
            yield return new WaitForSeconds(waitTime);
            if (sec == 59)
            {
                min++;
                sec = -1;
            }
            sec++;
            timerText.text = min.ToString("D2") + " : " + sec.ToString("D2");
        }
    }
    public void StartTimer()
    {
        min = 0;
        sec = 0;
        coroutine = WaitAndPrint(1.0f);
        StartCoroutine(coroutine);
    }

    public void StopTimer()
    {
        StopCoroutine(coroutine);
    }
}
