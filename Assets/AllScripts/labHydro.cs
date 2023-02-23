using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;
using UnityEngine.UI;
using System;
using System.Security.Cryptography.X509Certificates;

public class labHydro : MonoBehaviour
{
    public GameObject Indicator_On;
    public ValveClass1 SpeedValue;
    public ValveClass1Hydro PodachaValue;
    // public ValveClass1 TormozValue;
    public TextMeshPro rmp_text;
    public TextMeshPro flowmeter1_text;
    public TextMeshPro flowmeter2_text;
    public TextMeshPro pressureP1_text;


    public Text rmp_motor_text;
    public Text podacha_text;
    public Text p1_text;
    public Text p2_text;
    //public Text moment_tormoz_text;
    public Text time_text;

    public AudioSource Rotor_sound;

    public Slider timeSlider;

    public GameObject Rotor_motor;
    //public GameObject Rotor_mufta_motor;
    //public GameObject Rotor_Dynonometr_1;
    //public GameObject Rotor_reductor_1;
    //public GameObject Rotor_reductor_2;
    //public GameObject Rotor_reductor_Mid;
    //public GameObject Rotor_Dynonometr_2;
    //public GameObject Rotor_mufta_tormoz;
    //public GameObject Rotor_tormoz;

    float angle1 = 0;
    float angle2 = 0;
    float angleMid = 0;

    float rpm = 500f; //[500..1500];
    float Q_podacha = 0f;

    private bool isOn = false;
    private bool isStart = false;
    private bool IsKlapanOpened = true;
    public bool rotorOn = false;
   

    float ttt = 0;

    float p1_maxValue = 0;
    float p2_maxValue = 0;


    //public labDM_parametr param1;
    //public labDM_parametr param2;
    //public labDM_parametr param3;
    //public labDM_parametr param4;
    //public labDM_parametr param5;
    //public labDM_parametr param6;
    //public labDM_parametr param7;
    //public labDM_parametr param8;
    //public labDM_parametr param9;
    //public labDM_parametr param10;

    //рубильник
    public void On()
    {
        isOn = true;
        rmp_text.gameObject.SetActive(true);
    }
    public void Off()
    {
        Rotor_sound.Stop();
        isOn = false;
        rmp_text.gameObject.SetActive(false);
        Indicator_On.SetActive(false);
        isStart = false;
    }

    public void RotorOn()
    {
        rotorOn = true;
    }

    public void RotorOff()
    {
        rotorOn = false;
    }

    //Старт-стоп
    public void Start_button()
    {
        if (isOn == false) return;
        if (isStart == false )
        {
            isStart = true;
            Rotor_sound.Play();
            Indicator_On.SetActive(true);
        }


    }
    public void Stop_button()
    {
        if (isOn == false) return;
        if (isStart == true )
        {
            isStart = false;
            Rotor_sound.Stop();
            Indicator_On.SetActive(false);
        }
    }

   

    public void klapanOn()
    {
        IsKlapanOpened = true;
        // Debug.Log("!!!KLAPANON!!!");
    }

    public void KlapanOff()
    {
        IsKlapanOpened = false;
        // Debug.Log("!!!KLAPANOff!!!");

    }

    public void ClearMaxValues()
    {
        p1_maxValue = 0;
        p2_maxValue = 0;
    }


    // Start is called before the first frame update
    void Start()
    {

    }


    // Update is called once per frame
    void Update()
    {
        //float freq = (SpeedValue.currentShift / SpeedValue.maxA) * 49f + 1f;
        //rpm = (freq) / 50f * 3000f; //3000 оборотов при частоте 50
        //rmp_text.text = freq.ToString("N1");
        //Rotor_sound.pitch = (freq + 25f) / 50f;

        float freq = (SpeedValue.currentShift / SpeedValue.maxA) * 49f + 1f;
        if (freq > 20f)
        {
            rpm = (freq) / 50f * 3000f; //3000 оборотов при частоте 50
            rmp_text.text = freq.ToString("N1");
            Rotor_sound.pitch = (freq + 25f) / 50f;
        }
        else
        {
            freq = 20f;
            rpm = (freq) / 50f * 3000f; //3000 оборотов при частоте 50
            rmp_text.text = (freq).ToString("N1");
            Rotor_sound.pitch = (freq + 25f) / 50f;
        }
        float freq2 = (PodachaValue.currentShift / PodachaValue.maxA) * 14f + 1f;
        Q_podacha = (freq2) / 3600f; //3000 оборотов при частоте 50
                                     // Debug.Log("freq2=" + freq2.ToString() + "  Qpodacha= " + Q_podacha.ToString());



        //Rotor_sound.pitch = (freq + 25f) / 50f;

        if (isStart == true)
        {




            float KPD;
            //? об/мин * 1000
            float w = rpm / 1000f;
            rmp_motor_text.text = (w * 1000f).ToString("N0");

            //2*длина трубопровода/Си
            float faza_T = (2f * 10f) / Convert.ToSingle((Math.Sqrt(2000000000f / 1000f) * Math.Sqrt(1f + 0.01f * 10f / 4f * 1f)));
            //формула с википедии толщина стенок трубы 4 диаметр трубы 10
            float Si = Convert.ToSingle(Math.Sqrt(2000000000f / 1000f)) * Convert.ToSingle(Math.Sqrt(1f + 0.01f * 10f / 4f * 1f));
            // Debug.Log("!!!faza!!! =="+faza_T.ToString());
            float WaterSpeed = 0.531f;



            time_text.text = "1/" + timeSlider.value.ToString("N1");

            podacha_text.text = Q_podacha.ToString();

            float q1_value = (15f / 3600f) * ((w * 1000f) / 3000f);

            //float p1_value = ((-28219f * Q_podacha + 117.6f) *((w*1000/3000)* (w * 1000 / 3000)));
            //float p1_value = ((-28219f * q1_value + 117.6f) * ((w * 1000 / 3000) * (w * 1000 / 3000)));
            //
            // float p1_value = ((12f*9.8f*1000f) * ((w * 1000f / 3000f) * (w * 1000f / 3000f)));

            float p1_value = ((117.6f) * ((w * 1000f / 3000f) * (w * 1000f / 3000f)));


            // float p1_value = (/*(-28219f * Q_podacha + */117.6f) / ((w * 1000f / 3000f) * (w * 1000f / 3000f));

            //обозначить максимумы

           // p1_text.text = (p1_value / 1000f).ToString() + "/" + (p1_maxValue / 1000000f).ToString("N3");

            if (IsKlapanOpened == true)
            {
                if (freq2 == 1f)
                //zadv closed and klapan opened
                {
                    if (p1_maxValue< p1_value)
                    {
                        p1_maxValue = p1_value;
                    }

                    podacha_text.text = "0";
                    p1_text.text = (p1_value / 1000f).ToString("N3") + "/" + (p1_maxValue / 1000f).ToString("N3");
                    p2_text.text = "0";

                }
                else
                {
                    //klapan opened zadv opened
                    podacha_text.text = (Q_podacha*3600).ToString("N3");

                    if (Q_podacha > 0.00416f)
                    {
                        p1_text.text = "0"+"/"+(p1_maxValue/1000f).ToString("N3");
                        p2_text.text = "0" + "/" + (p2_maxValue/1000f).ToString("N3");
                        podacha_text.text = "15";
                    }
                    else
                    {
                        if(p1_maxValue< ((-28219f * Q_podacha + 117.6f) * ((w * 1000 / 3000) * (w * 1000 / 3000))))
                        {
                            p1_maxValue = ((-28219f * Q_podacha + 117.6f) * ((w * 1000 / 3000) * (w * 1000 / 3000)));
                        }

                        if (p2_maxValue < ((-28219f * Q_podacha + 117.6f) * ((w * 1000 / 3000) * (w * 1000 / 3000))))
                        {
                            p2_maxValue = ((-28219f * Q_podacha + 117.6f) * ((w * 1000 / 3000) * (w * 1000 / 3000)));
                        }

                        podacha_text.text = (Q_podacha * 3600).ToString("N3");

                        //p1_text.text = (p1_value / 1000f).ToString("N3") + "/" + (p1_maxValue / 1000f).ToString("N3");
                        p1_text.text = (((-28219f * Q_podacha + 117.6f) * ((w * 1000 / 3000) * (w * 1000 / 3000))) / 1000f).ToString("N3")+"/"+(p1_maxValue / 1000f).ToString("N3");
                        p2_text.text = (((-28219f * Q_podacha + 117.6f) * ((w * 1000 / 3000) * (w * 1000 / 3000))) / 1000f).ToString("N3") + "/" + (p2_maxValue / 1000f).ToString("N3");

                    }

                }
            }
            else
            {
                //zadv opneed klapan closed
                if (freq2 != 1f)
                {

                    podacha_text.text = "0";


                    if (1f / timeSlider.value < faza_T)
                    {
                        float hydro_p = /*1000f **/ Si * WaterSpeed + 4 * p1_value;

                        if (p1_maxValue < p1_value + hydro_p)
                        {
                            p1_maxValue = p1_value + hydro_p;
                        }

                        if (p2_maxValue < hydro_p)
                        {
                            p2_maxValue = hydro_p;
                        }

                        p1_text.text = (p1_value / 1000f).ToString("N3") + "/" + (p1_maxValue / 1000f).ToString("N3");
                        p2_text.text = "0/" + (p2_maxValue/1000f).ToString("N3");
                    }
                    else
                    {
                        float hydro_p = 2*p1_value + 2f * /*1000f **/ WaterSpeed * 10f / (1f / Convert.ToSingle(timeSlider.value));

                        if (p1_maxValue < p1_value + hydro_p)
                        {
                            p1_maxValue = p1_value + hydro_p;
                        }

                        if (p2_maxValue < hydro_p)
                        {
                            p2_maxValue = hydro_p;
                        }

                        p1_text.text = (p1_value / 1000f).ToString("N3") + "/" + (p1_maxValue / 1000f).ToString("N3");
                        p2_text.text = "0/" + (p2_maxValue/1000f).ToString("N3");

                    }
                }
                else//zadv closed and klapan closed
                {
                    podacha_text.text = "0";
                    p2_text.text = "0";

                    if (1f / timeSlider.value < faza_T)
                    {
                        float hydro_p = /*1000f **/ Si * WaterSpeed + 4 * p1_value;
                        if (p1_maxValue < p1_value + hydro_p)
                        {
                            p1_maxValue = p1_value + hydro_p;
                        }
                        p1_text.text = (p1_value / 1000f).ToString("N3") + "/" + (p1_maxValue / 1000f).ToString("N3");

                    }
                    else
                    {
                        float hydro_p = 2*p1_value + 2f * /*1000f **/ WaterSpeed * 10f / (1f / Convert.ToSingle(timeSlider.value));
                        if (p1_maxValue < p1_value + hydro_p)
                        {
                            p1_maxValue = p1_value + hydro_p;
                        }
                        p1_text.text = (p1_value / 1000f).ToString("N3") + "/" + (p1_maxValue / 1000f).ToString("N3");
                    }
                }


            }


            if (p2_text.text.Equals("0"))
            {
                p2_text.text = "0/" + (p2_maxValue / 1000f).ToString("N3");
            }

           


            flowmeter1_text.text = podacha_text.text;
            flowmeter2_text.text = podacha_text.text;
            pressureP1_text.text = (p1_value).ToString("N1");



            if (rotorOn)
            {
                angle1 += rpm / 60f * Time.deltaTime * 360f;
                if (angle1 > 36000f) angle1 -= 36000f;
                if (angle1 < 36000f) angle1 += 36000f;

                Rotor_motor.transform.localRotation = Quaternion.Euler(-angle1, 0, 0);
            }

        }
        else
        {
            rmp_motor_text.text = "0";
            podacha_text.text = "0";
            p1_text.text = "0";
            p2_text.text = "0";
            time_text.text = "0";
        }


    }
}
