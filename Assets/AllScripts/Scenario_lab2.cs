using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Scenario_lab2 : MonoBehaviour
{
    public GameObject pruzina;
    public GameObject massa;
    public GameObject weights_0;
    public GameObject weights_1;
    public GameObject weights_2;
    public GameObject weights_3;
    public GameObject ustanovka;
    public GameObject metka_na_lineuke;
    public GameObject time;
    public string pruzina_name;
    public int kolvo;
    public bool setactive;
    public Scenario_step_waiter timer;

    // Start is called before the first frame update
    void Start()
    {
        massa = GameObject.Find("massa_lab2");
        weights_0 = GameObject.Find("weights_0_lab2");
        weights_1 = GameObject.Find("weights_1_lab2");
        weights_2 = GameObject.Find("weights_2_lab2");
        weights_3 = GameObject.Find("weights_3_lab2");
        metka_na_lineuke = GameObject.Find("metka_na_lineuke_lab2");
        kolvo = 1;
        setactive = true;
    }

    public void SetPruzina(GameObject val)
    {
        pruzina = val;
        pruzina_name = val.name;

        //if (pruzina_name.Equals("pruzina06_lab2"))
        //{
        //    ustanovka = GameObject.Find("Animation_lab2_pruzina06_wl");
        //}
        //else if (pruzina_name.Equals("pruzina05_lab2"))
        //{
        //    ustanovka = GameObject.Find("Animation_lab2_pruzina05_wl");
        //}
        //else if (pruzina_name.Equals("pruzina04_lab2"))
        //{
        //    ustanovka = GameObject.Find("Animation_lab2_pruzina04_wl");
        //}
        //else if (pruzina_name.Equals("pruzina03_lab2"))
        //{
        //    ustanovka = GameObject.Find("Animation_lab2_pruzina03_wl");
        //}
        //else if (pruzina_name.Equals("pruzina02_lab2"))
        //{
        //    ustanovka = GameObject.Find("Animation_lab2_pruzina02_wl");
        //}
        //else
        //{
        //    ustanovka = GameObject.Find("Animation_lab2_pruzina01_wl");
        //}

        if (pruzina_name.Equals("pruzina04_lab2"))
        {
            ustanovka = GameObject.Find("Animation_lab2_pruzina04_wl");
        }
        else
        {
            ustanovka = GameObject.Find("Animation_lab2_pruzina01_wl");
        }
    }

    public void SetTransform()
    {
        pruzina.transform.localRotation = Quaternion.Euler(-180, 0, 0);

        if (pruzina_name.Equals("pruzina01_lab2"))
        {
            pruzina.transform.localRotation = Quaternion.Euler(-360, 0, 360);
        }

        //if (pruzina_name.Equals("pruzina06_lab2"))
        //{
        //    pruzina.transform.localPosition = new Vector3(-1.0372f, -0.1977f, -0.8598f);
        //    pruzina.transform.localScale = new Vector3(1f, 1f, 1.26f);
        //    massa.transform.localPosition = new Vector3(-2.1392f, -0.9171f, -0.511f);
        //    weights_0.transform.localPosition = new Vector3(-1.8234f, -0.9119f, -0.6327f);
        //    weights_1.transform.localPosition = new Vector3(-1.8234f, -0.9119f, -0.6297f);
        //    weights_2.transform.localPosition = new Vector3(-1.8234f, -0.9119f, -0.6239f);
        //    weights_3.transform.localPosition = new Vector3(-1.8234f, -0.9119f, -0.6099f);
        //    metka_na_lineuke.transform.localPosition = new Vector3(-1.795f, -1.0018f, -0.5172f);
        //}
        //else if (pruzina_name.Equals("pruzina05_lab2"))
        //{
        //    pruzina.transform.localPosition = new Vector3(-1.1852f, -0.1943f, -0.8583f);
        //    pruzina.transform.localScale = new Vector3(1f, 1f, 1.4f);
        //    massa.transform.localPosition = new Vector3(-2.1392f, -0.9217f, -0.5979f);
        //    weights_0.transform.localPosition = new Vector3(-1.8234f, -0.9167f, -0.7199f);
        //    weights_1.transform.localPosition = new Vector3(-1.8234f, -0.9167f, -0.7169f);
        //    weights_2.transform.localPosition = new Vector3(-1.8234f, -0.9167f, -0.7109f);
        //    weights_3.transform.localPosition = new Vector3(-1.8234f, -0.9167f, -0.6969f);
        //    metka_na_lineuke.transform.localPosition = new Vector3(-1.795f, -1.0018f, -0.6043f);
        //}
        //else if (pruzina_name.Equals("pruzina04_lab2"))
        //{
        //    pruzina.transform.localPosition = new Vector3(-1.3348f, -0.2f, -0.86f);
        //    pruzina.transform.localScale = new Vector3(1f, 1f, 1.37f);
        //    massa.transform.localPosition = new Vector3(-2.1392f, -0.917f, -0.6853f);
        //    weights_0.transform.localPosition = new Vector3(-1.8234f, -0.9119f, -0.8073f);
        //    weights_1.transform.localPosition = new Vector3(-1.8234f, -0.9119f, -0.8043f);
        //    weights_2.transform.localPosition = new Vector3(-1.8234f, -0.9119f, -0.7983f);
        //    weights_3.transform.localPosition = new Vector3(-1.8234f, -0.9119f, -0.7843f);
        //    metka_na_lineuke.transform.localPosition = new Vector3(-1.795f, -1.0018f, -0.6965f);
        //}
        //else if (pruzina_name.Equals("pruzina03_lab2"))
        //{
        //    pruzina.transform.localPosition = new Vector3(-0.58f, -0.2064f, -0.8831f);
        //    pruzina.transform.localScale = new Vector3(1f, 1f, 1.14f);
        //    massa.transform.localPosition = new Vector3(-2.1375f, -0.9228f, -0.495f);
        //    weights_0.transform.localPosition = new Vector3(-1.8217f, -0.9178f, -0.617f);
        //    weights_1.transform.localPosition = new Vector3(-1.8217f, -0.9178f, -0.614f);
        //    weights_2.transform.localPosition = new Vector3(-1.8217f, -0.9178f, -0.608f);
        //    weights_3.transform.localPosition = new Vector3(-1.8217f, -0.9178f, -0.594f);
        //    metka_na_lineuke.transform.localPosition = new Vector3(-1.795f, -1.0018f, -0.5017f);
        //}
        //else if (pruzina_name.Equals("pruzina02_lab2"))
        //{
        //    pruzina.transform.localPosition = new Vector3(-0.733f, -0.209f, -0.8852f);
        //    pruzina.transform.localScale = new Vector3(1f, 1f, 1.3f);
        //    massa.transform.localPosition = new Vector3(-2.1375f, -0.9176f, -0.5778f);
        //    weights_0.transform.localPosition = new Vector3(-1.8217f, -0.9126f, -0.6998f);
        //    weights_1.transform.localPosition = new Vector3(-1.8217f, -0.9126f, -0.6968f);
        //    weights_2.transform.localPosition = new Vector3(-1.8217f, -0.9126f, -0.6908f);
        //    weights_3.transform.localPosition = new Vector3(-1.8217f, -0.9126f, -0.6768f);
        //    metka_na_lineuke.transform.localPosition = new Vector3(-1.795f, -1.0018f, -0.5827f);
        //}
        //else
        //{
        //    pruzina.transform.localPosition = new Vector3(-1.824f, -0.9141f, -0.339f);
        //    pruzina.transform.localScale = new Vector3(0.8f, 0.8f, 1f);
        //    massa.transform.localPosition = new Vector3(-2.1375f, -0.9176f, -0.6556f);
        //    weights_0.transform.localPosition = new Vector3(-1.8217f, -0.9126f, -0.7776f);
        //    weights_1.transform.localPosition = new Vector3(-1.8217f, -0.9126f, -0.7746f);
        //    weights_2.transform.localPosition = new Vector3(-1.8217f, -0.9126f, -0.7686f);
        //    weights_3.transform.localPosition = new Vector3(-1.8217f, -0.9126f, -0.7546f);
        //    metka_na_lineuke.transform.localPosition = new Vector3(-1.795f, -1.0018f, -0.6664f);
        //}

        if (pruzina_name.Equals("pruzina04_lab2"))
        {
            pruzina.transform.localPosition = new Vector3(-1.3348f, -0.2f, -0.86f);
            pruzina.transform.localScale = new Vector3(1f, 1f, 1.25f);
            massa.transform.localPosition = new Vector3(-2.1392f, -0.917f, -0.6853f);
            weights_0.transform.localPosition = new Vector3(-1.8234f, -0.9119f, -0.8073f);
            weights_1.transform.localPosition = new Vector3(-1.8234f, -0.9119f, -0.8043f);
            metka_na_lineuke.transform.localPosition = new Vector3(-1.795f, -1.0018f, -0.6678f);
        }
        else
        {
            pruzina.transform.localPosition = new Vector3(-1.824f, -0.9141f, -0.339f);
            pruzina.transform.localScale = new Vector3(0.8f, 0.8f, 1f);
            massa.transform.localPosition = new Vector3(-2.1375f, -0.9176f, -0.6556f);
            weights_0.transform.localPosition = new Vector3(-1.8217f, -0.9126f, -0.7776f);
            weights_1.transform.localPosition = new Vector3(-1.8217f, -0.9126f, -0.7746f);
            metka_na_lineuke.transform.localPosition = new Vector3(-1.795f, -1.0018f, -0.6664f);
        }
    }

    public void KolebaniyaWithoutLiquid()
    {
        Scenario_step_AnimationState anima;

        pruzina.transform.localPosition = new Vector3(0.394f, 0.001399368f, -10f);
        massa.transform.localPosition = new Vector3(-1.294f, -0.804f, -10f);
        weights_0.transform.localPosition = new Vector3(-0.476f, -0.823f, -10f);
        weights_1.transform.localPosition = new Vector3(-0.593f, -0.823f, -10f);

        if (kolvo == 1)
        {
            ustanovka.transform.localPosition = new Vector3(0f, 0f, 0f);
            kolvo++;
        }
        else
        {
            anima = (Scenario_step_AnimationState)ustanovka.GetComponent(typeof(Scenario_step_AnimationState));
            anima.SetTargetTime(10);
            kolvo = 1;

            time = GameObject.Find("Waiter lab 2 1");
            timer = (Scenario_step_waiter)time.GetComponent(typeof(Scenario_step_waiter));

            if (pruzina_name.Equals("pruzina04_lab2"))
            {
                timer.delay = 18f;
            } 
            else
            {
                timer.delay = 10f;
            }
        }
    }

    public void KolebaniyaWithLiquid()
    {
        Scenario_step_AnimationState anima;

        pruzina.transform.localPosition = new Vector3(0.394f, 0.001399368f, -10f);
        massa.transform.localPosition = new Vector3(-1.294f, -0.804f, -10f);
        weights_0.transform.localPosition = new Vector3(-0.476f, -0.823f, -10f);
        weights_1.transform.localPosition = new Vector3(-0.593f, -0.823f, -10f);

        if (pruzina_name.Equals("pruzina04_lab2"))
        {
            if (kolvo == 1)
            {                
                ustanovka = GameObject.Find("Animation_lab2_pruzina04_benzin");
                if (setactive == true)
                {
                    ustanovka.transform.localPosition = new Vector3(0f, 0f, 0f);
                    setactive = false;
                }
                else
                {
                    anima = (Scenario_step_AnimationState)ustanovka.GetComponent(typeof(Scenario_step_AnimationState));
                    anima.SetTargetTime(1);
                    setactive = true;
                    kolvo++;

                    time = GameObject.Find("Waiter lab 2 2");
                    timer = (Scenario_step_waiter)time.GetComponent(typeof(Scenario_step_waiter));

                    timer.delay = 10f;
                }
            }
            else if (kolvo == 2)
            {
                if (setactive == true)
                {
                    ustanovka.transform.localPosition = new Vector3(0f, 0f, -10f);
                    ustanovka = GameObject.Find("Animation_lab2_pruzina04_benzin_2");
                    ustanovka.transform.localPosition = new Vector3(0f, 0f, 0f);
                    setactive = false;
                }
                else
                {
                    anima = (Scenario_step_AnimationState)ustanovka.GetComponent(typeof(Scenario_step_AnimationState));
                    anima.SetTargetTime(1);
                    setactive = true;
                    kolvo++;

                    time = GameObject.Find("Waiter lab 2 3");
                    timer = (Scenario_step_waiter)time.GetComponent(typeof(Scenario_step_waiter));

                    timer.delay = 21f;
                }
            }
            else if (kolvo == 3)
            {
                if (setactive == true)
                {
                    ustanovka.transform.localPosition = new Vector3(0f, 0f, -10f);
                    ustanovka = GameObject.Find("Animation_lab2_pruzina04_voda");
                    ustanovka.transform.localPosition = new Vector3(0f, 0f, 0f);
                    setactive = false;
                }
                else
                {
                    anima = (Scenario_step_AnimationState)ustanovka.GetComponent(typeof(Scenario_step_AnimationState));
                    anima.SetTargetTime(1);
                    setactive = true;
                    kolvo++;

                    time = GameObject.Find("Waiter lab 2 4");
                    timer = (Scenario_step_waiter)time.GetComponent(typeof(Scenario_step_waiter));

                    timer.delay = 9f;
                }
            }
            else if (kolvo == 4)
            {
                if (setactive == true)
                {
                    ustanovka.transform.localPosition = new Vector3(0f, 0f, -10f);
                    ustanovka = GameObject.Find("Animation_lab2_pruzina04_voda_2");
                    ustanovka.transform.localPosition = new Vector3(0f, 0f, 0f);
                    setactive = false;
                }
                else
                {
                    anima = (Scenario_step_AnimationState)ustanovka.GetComponent(typeof(Scenario_step_AnimationState));
                    anima.SetTargetTime(1);
                    setactive = true;
                    kolvo++;

                    time = GameObject.Find("Waiter lab 2 5");
                    timer = (Scenario_step_waiter)time.GetComponent(typeof(Scenario_step_waiter));

                    timer.delay = 12f;
                }
            }
            else if (kolvo == 5)
            {
                if (setactive == true)
                {
                    ustanovka.transform.localPosition = new Vector3(0f, 0f, -10f);
                    ustanovka = GameObject.Find("Animation_lab2_pruzina04_maslo");
                    ustanovka.transform.localPosition = new Vector3(0f, 0f, 0f);
                    setactive = false;
                }
                else
                {
                    anima = (Scenario_step_AnimationState)ustanovka.GetComponent(typeof(Scenario_step_AnimationState));
                    anima.SetTargetTime(1);
                    setactive = true;
                    kolvo++;

                    time = GameObject.Find("Waiter lab 2 6");
                    timer = (Scenario_step_waiter)time.GetComponent(typeof(Scenario_step_waiter));

                    timer.delay = 4f;
                }
            }
        }
        else
        {
            if (kolvo == 1)
            {
                ustanovka = GameObject.Find("Animation_lab2_pruzina01_benzin");
                if (setactive == true)
                {
                    ustanovka.transform.localPosition = new Vector3(0f, 0f, 0f);
                    setactive = false;
                }
                else
                {
                    anima = (Scenario_step_AnimationState)ustanovka.GetComponent(typeof(Scenario_step_AnimationState));
                    anima.SetTargetTime(1);
                    setactive = true;
                    kolvo++;

                    time = GameObject.Find("Waiter lab 2 2");
                    timer = (Scenario_step_waiter)time.GetComponent(typeof(Scenario_step_waiter));

                    timer.delay = 6f;
                }
            }
            else if (kolvo == 2)
            {
                if (setactive == true)
                {
                    ustanovka.transform.localPosition = new Vector3(0f, 0f, -10f);
                    ustanovka = GameObject.Find("Animation_lab2_pruzina01_benzin_2");
                    ustanovka.transform.localPosition = new Vector3(0f, 0f, 0f);
                    setactive = false;
                }
                else
                {
                    anima = (Scenario_step_AnimationState)ustanovka.GetComponent(typeof(Scenario_step_AnimationState));
                    anima.SetTargetTime(1);
                    setactive = true;
                    kolvo++;

                    time = GameObject.Find("Waiter lab 2 3");
                    timer = (Scenario_step_waiter)time.GetComponent(typeof(Scenario_step_waiter));

                    timer.delay = 19f;
                }
            }
            else if (kolvo == 3)
            {
                if (setactive == true)
                {
                    ustanovka.transform.localPosition = new Vector3(0f, 0f, -10f);
                    ustanovka = GameObject.Find("Animation_lab2_pruzina01_voda");
                    ustanovka.transform.localPosition = new Vector3(0f, 0f, 0f);
                    setactive = false;
                }
                else
                {
                    anima = (Scenario_step_AnimationState)ustanovka.GetComponent(typeof(Scenario_step_AnimationState));
                    anima.SetTargetTime(1);
                    setactive = true;
                    kolvo++;

                    time = GameObject.Find("Waiter lab 2 4");
                    timer = (Scenario_step_waiter)time.GetComponent(typeof(Scenario_step_waiter));

                    timer.delay = 5f;
                }
            }
            else if (kolvo == 4)
            {
                if (setactive == true)
                {
                    ustanovka.transform.localPosition = new Vector3(0f, 0f, -10f);
                    ustanovka = GameObject.Find("Animation_lab2_pruzina01_voda_2");
                    ustanovka.transform.localPosition = new Vector3(0f, 0f, 0f);
                    setactive = false;
                }
                else
                {
                    anima = (Scenario_step_AnimationState)ustanovka.GetComponent(typeof(Scenario_step_AnimationState));
                    anima.SetTargetTime(1);
                    setactive = true;
                    kolvo++;

                    time = GameObject.Find("Waiter lab 2 5");
                    timer = (Scenario_step_waiter)time.GetComponent(typeof(Scenario_step_waiter));

                    timer.delay = 12f;
                }
            }
            else if (kolvo == 5)
            {
                if (setactive == true)
                {
                    ustanovka.transform.localPosition = new Vector3(0f, 0f, -10f);
                    ustanovka = GameObject.Find("Animation_lab2_pruzina01_maslo");
                    ustanovka.transform.localPosition = new Vector3(0f, 0f, 0f);
                    setactive = false;
                }
                else
                {
                    anima = (Scenario_step_AnimationState)ustanovka.GetComponent(typeof(Scenario_step_AnimationState));
                    anima.SetTargetTime(1);
                    setactive = true;
                    kolvo++;

                    time = GameObject.Find("Waiter lab 2 6");
                    timer = (Scenario_step_waiter)time.GetComponent(typeof(Scenario_step_waiter));

                    timer.delay = 3f;
                }
            }
        }
    }

    public void ReturnAll()
    {
        pruzina.transform.localRotation = Quaternion.Euler(90, 0, 0);
        massa.transform.localPosition = new Vector3(-1.294f, -0.804f, -0.9308f);
        weights_0.transform.localPosition = new Vector3(-0.476f, -0.823f, -1.06f);
        weights_1.transform.localPosition = new Vector3(-0.593f, -0.823f, -1.06f);
        metka_na_lineuke.transform.localPosition = new Vector3(-0.4241998f, -0.113f, -10f);

        ustanovka.transform.localPosition = new Vector3(0f, 0f, -10f);


        //weights_2.transform.localPosition = new Vector3(-0.71f, -0.823f, -1.06f);
        //weights_3.transform.localPosition = new Vector3(-0.823f, -0.823f, -1.06f);

        //if (pruzina_name.Equals("pruzina06_lab2")) 
        //{
        //    pruzina.transform.localPosition = new Vector3(0.394f, 0.0003093481f, -0.0002765656f);
        //    pruzina.transform.localScale = new Vector3(1f, 1f, 0.8855619f);
        //}
        //else if (pruzina_name.Equals("pruzina05_lab2")) 
        //{
        //    pruzina.transform.localPosition = new Vector3(0.394f, 0.0003093481f, -0.0002765656f);
        //    pruzina.transform.localScale = new Vector3(1f, 1f, 1.020686f);
        //}
        //else if (pruzina_name.Equals("pruzina04_lab2")) 
        //{
        //    pruzina.transform.localPosition = new Vector3(0.394f, 0.001399368f, -0.0002877414f);
        //    pruzina.transform.localScale = new Vector3(1f, 1f, 1f);
        //}
        //else if (pruzina_name.Equals("pruzina03_lab2"))
        //{
        //    pruzina.transform.localPosition = new Vector3(0.394f, 0.0005239248f, -0.0003857911f);
        //    pruzina.transform.localScale = new Vector3(1f, 1f, 0.8855619f);
        //}
        //else if (pruzina_name.Equals("pruzina02_lab2"))
        //{
        //    pruzina.transform.localPosition = new Vector3(0.394f, 0.0005239248f, -0.0003857911f);
        //    pruzina.transform.localScale = new Vector3(1f, 1f, 1.020686f);
        //}
        //else
        //{
        //    pruzina.transform.localPosition = new Vector3(-0.8099992f, -1.333f, -1.047f);
        //    pruzina.transform.localScale = new Vector3(0.8f, 0.8f, 0.8f);
        //}

        if (pruzina_name.Equals("pruzina04_lab2"))
        {
            pruzina.transform.localPosition = new Vector3(0.394f, 0.001399368f, -0.0002877414f);
            pruzina.transform.localScale = new Vector3(1f, 1f, 1f);
        }
        else
        {
            pruzina.transform.localPosition = new Vector3(-0.8099992f, -1.333f, -1.047f);
            pruzina.transform.localScale = new Vector3(0.8f, 0.8f, 0.8f);
        }
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
