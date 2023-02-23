using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Scenario_Lab3 : MonoBehaviour
{
    public GameObject ustanovka;
    public GameObject pruzina;
    public GameObject time;
    public Scenario_step_waiter timer;
    public int kolvo;
    public bool setactive;

    // Start is called before the first frame update
    void Start()
    {
        kolvo = 1;
        setactive = true;
        ustanovka = GameObject.Find("Animation_lab3_pruzina01_one");
    }

    public void SetPruzina(GameObject val)
    {
        pruzina = val;
    }

    public void Experiment()
    {
        Scenario_step_AnimationState anima;
        pruzina.transform.localPosition = new Vector3(0f, 0f, -10f);

        if (kolvo == 1)
        {
            if (pruzina.name.Equals("pruzina01_lab3"))
            {
                if (setactive == true)
                {
                    ustanovka.transform.localPosition = new Vector3(0f, 0f, -10f);
                    ustanovka = GameObject.Find("Animation_lab3_pruzina01_one");
                    ustanovka.transform.localPosition = new Vector3(-2.244999f, 0.07499996f, -0.6430001f);
                    setactive = false;
                }
                else
                {
                    anima = (Scenario_step_AnimationState)ustanovka.GetComponent(typeof(Scenario_step_AnimationState));
                    anima.SetTargetTime(10);
                    setactive = true;

                    time = GameObject.Find("Waiter lab 3 1");
                    timer = (Scenario_step_waiter)time.GetComponent(typeof(Scenario_step_waiter));

                    timer.delay = 15.5f;
                    kolvo++;
                }
            }
            else
            {
                if (setactive == true)
                {
                    ustanovka.transform.localPosition = new Vector3(0f, 0f, -10f);
                    ustanovka = GameObject.Find("Animation_lab3_pruzina02_one");
                    ustanovka.transform.localPosition = new Vector3(-2.244999f, 0.07499996f, -0.6430001f);
                    setactive = false;
                }
                else
                {
                    anima = (Scenario_step_AnimationState)ustanovka.GetComponent(typeof(Scenario_step_AnimationState));
                    anima.SetTargetTime(10);
                    setactive = true;

                    time = GameObject.Find("Waiter lab 3 1");
                    timer = (Scenario_step_waiter)time.GetComponent(typeof(Scenario_step_waiter));

                    timer.delay = 15f;
                    kolvo++;
                }
            }
        } 
        else if (kolvo == 2)
        {
            if (pruzina.name.Equals("pruzina01_lab3"))
            {
                if (setactive == true)
                {
                    ustanovka.transform.localPosition = new Vector3(0f, 0f, -10f);
                    ustanovka = GameObject.Find("Animation_lab3_pruzina01_two");
                    ustanovka.transform.localPosition = new Vector3(-2.244999f, 0.07499996f, -0.6430001f);
                    setactive = false;
                }
                else
                {
                    anima = (Scenario_step_AnimationState)ustanovka.GetComponent(typeof(Scenario_step_AnimationState));
                    anima.SetTargetTime(10);
                    setactive = true;

                    time = GameObject.Find("Waiter lab 3 2");
                    timer = (Scenario_step_waiter)time.GetComponent(typeof(Scenario_step_waiter));

                    timer.delay = 11f;
                    kolvo++;
                }
            }
            else
            {
                if (setactive == true)
                {
                    ustanovka.transform.localPosition = new Vector3(0f, 0f, -10f);
                    ustanovka = GameObject.Find("Animation_lab3_pruzina02_two");
                    ustanovka.transform.localPosition = new Vector3(-2.244999f, 0.07499996f, -0.6430001f);
                    setactive = false;
                }
                else
                {
                    anima = (Scenario_step_AnimationState)ustanovka.GetComponent(typeof(Scenario_step_AnimationState));
                    anima.SetTargetTime(10);
                    setactive = true;

                    time = GameObject.Find("Waiter lab 3 2");
                    timer = (Scenario_step_waiter)time.GetComponent(typeof(Scenario_step_waiter));

                    timer.delay = 14f;
                    kolvo++;
                }
            }
        } 
        else if (kolvo == 3)
        {
            if (pruzina.name.Equals("pruzina01_lab3"))
            {
                if (setactive == true)
                {
                    ustanovka.transform.localPosition = new Vector3(0f, 0f, -10f);
                    ustanovka = GameObject.Find("Animation_lab3_pruzina01_three");
                    ustanovka.transform.localPosition = new Vector3(-2.244999f, 0.07499996f, -0.6430001f);
                    setactive = false;
                }
                else
                {
                    anima = (Scenario_step_AnimationState)ustanovka.GetComponent(typeof(Scenario_step_AnimationState));
                    anima.SetTargetTime(5f);
                    setactive = true;

                    time = GameObject.Find("Waiter lab 3 3");
                    timer = (Scenario_step_waiter)time.GetComponent(typeof(Scenario_step_waiter));

                    timer.delay = 20f;
                    kolvo++;
                }
            }
            else
            {
                if (setactive == true)
                {
                    ustanovka.transform.localPosition = new Vector3(0f, 0f, -10f);
                    ustanovka = GameObject.Find("Animation_lab3_pruzina02_three");
                    ustanovka.transform.localPosition = new Vector3(-2.244999f, 0.07499996f, -0.6430001f);
                    setactive = false;
                }
                else
                {
                    anima = (Scenario_step_AnimationState)ustanovka.GetComponent(typeof(Scenario_step_AnimationState));
                    anima.SetTargetTime(2.5f);
                    setactive = true;

                    time = GameObject.Find("Waiter lab 3 3");
                    timer = (Scenario_step_waiter)time.GetComponent(typeof(Scenario_step_waiter));

                    timer.delay = 35f;
                    kolvo++;
                }
            }
        }
        else
        {
            if (pruzina.name.Equals("pruzina01_lab3"))
            {
                if (setactive == true)
                {
                    ustanovka.transform.localPosition = new Vector3(0f, 0f, -10f);
                    ustanovka = GameObject.Find("Animation_lab3_pruzina01_four");
                    ustanovka.transform.localPosition = new Vector3(-2.244999f, 0.07499996f, -0.6430001f);
                    setactive = false;
                }
                else
                {
                    anima = (Scenario_step_AnimationState)ustanovka.GetComponent(typeof(Scenario_step_AnimationState));
                    anima.SetTargetTime(10);
                    setactive = true;

                    time = GameObject.Find("Waiter lab 3 4");
                    timer = (Scenario_step_waiter)time.GetComponent(typeof(Scenario_step_waiter));

                    timer.delay = 0f;
                    kolvo++;
                }
            }
            else
            {
                if (setactive == true)
                {
                    ustanovka.transform.localPosition = new Vector3(0f, 0f, -10f);
                    ustanovka = GameObject.Find("Animation_lab3_pruzina02_four");
                    ustanovka.transform.localPosition = new Vector3(-2.244999f, 0.07499996f, -0.6430001f);
                    setactive = false;
                }
                else
                {
                    anima = (Scenario_step_AnimationState)ustanovka.GetComponent(typeof(Scenario_step_AnimationState));
                    anima.SetTargetTime(10);
                    setactive = true;

                    time = GameObject.Find("Waiter lab 3 4");
                    timer = (Scenario_step_waiter)time.GetComponent(typeof(Scenario_step_waiter));

                    timer.delay = 0f;
                    kolvo++;
                }
            }
        }
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
