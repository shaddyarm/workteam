using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using static Scenario_step_text;

public class Scenario_Lab1 : MonoBehaviour
{
    public GameObject pruzina_big;
    public GameObject pruzina_small;
    public GameObject massa;
    public GameObject weights_0;
    public GameObject weights_1;
    public GameObject weights_2;
    public GameObject weights_3;
    public GameObject palka;
    public GameObject begunok;
    public GameObject ustanovka;
    public GameObject time;
    public string big_name;
    public string small_name;
    public string big_name_last;
    public string small_name_last;
    public GameObject metka_na_lineuke;
    public int kolvo;
    public int zapomni;
    public int ust_kol;
    public bool setactive;
    public Scenario_step_waiter timer;

    // Start is called before the first frame update
    void Start()
    {
        massa = GameObject.Find("massa");
        weights_0 = GameObject.Find("weights_0");
        weights_1 = GameObject.Find("weights_1");
        weights_2 = GameObject.Find("weights_2");
        weights_3 = GameObject.Find("weights_3");
        metka_na_lineuke = GameObject.Find("metka_na_lineuke");
        palka = GameObject.Find("palka");
        begunok = GameObject.Find("begunok");
        setactive = true;
        zapomni = 0;
        ust_kol = 1;
    }
    
    public void SetPruzinaBig(GameObject val)
    {
        pruzina_big = val;
        big_name = val.name;
        kolvo = 1;
        zapomni++;

        if (big_name.Equals("pruzina06_1"))
        {
            pruzina_small = GameObject.Find("pruzina03_1");
            small_name = pruzina_small.name;
        }
        else if (big_name.Equals("pruzina05_1"))
        {
            pruzina_small = GameObject.Find("pruzina02_2");
            small_name = pruzina_small.name;
        }
        else
        {
            pruzina_small = GameObject.Find("pruzina01");
            small_name = pruzina_small.name;
        }
        
        if (zapomni == 2)
        {
            big_name_last = big_name;
            small_name_last = small_name;
        }
    }

    public void SetPruzinaSmall(GameObject val)
    {
        pruzina_small = val;
        small_name = val.name;
        kolvo = 1;
    }

    public void SetTransformBig()
    {
        pruzina_big.transform.localRotation = Quaternion.Euler(0, 0, 0);

        if (big_name.Equals("pruzina06_1"))
        {
            if (kolvo == 1)
            {
                pruzina_big.transform.localPosition = new Vector3(1.4037f, 0.137799f, 1.1019f);
                pruzina_big.transform.localScale = new Vector3(1f, 1f, 1f);
                massa.transform.localPosition = new Vector3(-0.4443f, 0.1115f, 0.2205f);
                weights_0.transform.localPosition = new Vector3(-0.4443f, 0.1115f, 0.0705f);
                weights_1.transform.localPosition = new Vector3(-0.4443f, 0.1115f, 0.0755f);
                metka_na_lineuke.transform.localPosition = new Vector3(-0.4241998f, -0.113f, 0.2041f);
                kolvo++;
            } 
            else if (kolvo == 2)
            {
                pruzina_big.transform.localScale = new Vector3(1f, 1f, 1.02f);
                massa.transform.localPosition = new Vector3(-0.4443f, 0.1115f, 0.2127f);
                weights_0.transform.localPosition = new Vector3(-0.4443f, 0.1115f, 0.0627f);
                weights_1.transform.localPosition = new Vector3(-0.4443f, 0.1115f, 0.0677f);
                weights_2.transform.localPosition = new Vector3(-0.4443f, 0.1115f, 0.0761f);
                metka_na_lineuke.transform.localPosition = new Vector3(-0.4241998f, -0.113f, 0.1963f);
                kolvo++;
            }
            else if (kolvo == 3)
            {
                pruzina_big.transform.localScale = new Vector3(1f, 1f, 1.08f);
                massa.transform.localPosition = new Vector3(-0.4443f, 0.114f, 0.204f);
                weights_0.transform.localPosition = new Vector3(-0.4443f, 0.1115f, 0.054f);
                weights_1.transform.localPosition = new Vector3(-0.4443f, 0.1115f, 0.059f);
                weights_2.transform.localPosition = new Vector3(-0.4443f, 0.1115f, 0.0674f);
                weights_3.transform.localPosition = new Vector3(-0.4443f, 0.1115f, 0.0851f);
                metka_na_lineuke.transform.localPosition = new Vector3(-0.4241998f, -0.113f, 0.1876f);
            }
        }
        else if (big_name.Equals("pruzina05_1"))
        {
            if (kolvo == 1)
            {
                pruzina_big.transform.localPosition = new Vector3(1.284f, 0.137799f, 1.1019f);
                pruzina_big.transform.localScale = new Vector3(1f, 1f, 1.08f);
                massa.transform.localPosition = new Vector3(-0.4443f, 0.1115f, 0.1379f);
                weights_0.transform.localPosition = new Vector3(-0.4443f, 0.1115f, -0.0121f);
                weights_1.transform.localPosition = new Vector3(-0.4443f, 0.1115f, -0.0071f);
                metka_na_lineuke.transform.localPosition = new Vector3(-0.4241998f, -0.113f, 0.1215f);
                kolvo++;
            }
            else if (kolvo == 2)
            {
                pruzina_big.transform.localScale = new Vector3(1f, 1f, 1.09f);
                massa.transform.localPosition = new Vector3(-0.4443f, 0.1115f, 0.1322f);
                weights_0.transform.localPosition = new Vector3(-0.4443f, 0.1115f, -0.0178f);
                weights_1.transform.localPosition = new Vector3(-0.4443f, 0.1115f, -0.0128f);
                weights_2.transform.localPosition = new Vector3(-0.4443f, 0.1115f, -0.0044f);
                metka_na_lineuke.transform.localPosition = new Vector3(-0.4241998f, -0.113f, 0.1158f);
                kolvo++;
            }
            else if (kolvo == 3)
            {
                pruzina_big.transform.localScale = new Vector3(1f, 1f, 1.15f);
                massa.transform.localPosition = new Vector3(-0.4443f, 0.1115f, 0.119f);
                weights_0.transform.localPosition = new Vector3(-0.4443f, 0.1115f, -0.031f);
                weights_1.transform.localPosition = new Vector3(-0.4443f, 0.1115f, -0.026f);
                weights_2.transform.localPosition = new Vector3(-0.4443f, 0.1115f, -0.0176f);
                weights_3.transform.localPosition = new Vector3(-0.4443f, 0.1115f, 0.0001f);
                metka_na_lineuke.transform.localPosition = new Vector3(-0.4241998f, -0.113f, 0.1026f);
            }
        }
        else
        {
            if (kolvo == 1)
            {
                pruzina_big.transform.localPosition = new Vector3(1.167f, 0.137799f, 1.1019f);
                pruzina_big.transform.localScale = new Vector3(1f, 1f, 1.04f);
                massa.transform.localPosition = new Vector3(-0.4443f, 0.1115f, 0.0564f);
                weights_0.transform.localPosition = new Vector3(-0.4443f, 0.1115f, -0.0936f);
                weights_1.transform.localPosition = new Vector3(-0.4443f, 0.1115f, -0.0886f);
                metka_na_lineuke.transform.localPosition = new Vector3(-0.4241998f, -0.113f, 0.04f);
                kolvo++;
            }
            else if (kolvo == 2)
            {
                pruzina_big.transform.localScale = new Vector3(1f, 1f, 1.06f);
                massa.transform.localPosition = new Vector3(-0.4443f, 0.1115f, 0.0501f);
                weights_0.transform.localPosition = new Vector3(-0.4443f, 0.1115f, -0.0999f);
                weights_1.transform.localPosition = new Vector3(-0.4443f, 0.1115f, -0.0949f);
                weights_2.transform.localPosition = new Vector3(-0.4443f, 0.1115f, -0.0865f);
                metka_na_lineuke.transform.localPosition = new Vector3(-0.4241998f, -0.113f, 0.0337f);
                kolvo++;
            }
            else if (kolvo == 3)
            {
                pruzina_big.transform.localScale = new Vector3(1f, 1f, 1.11f);
                massa.transform.localPosition = new Vector3(-0.4443f, 0.1115f, 0.031f);
                weights_0.transform.localPosition = new Vector3(-0.4443f, 0.1115f, -0.119f);
                weights_1.transform.localPosition = new Vector3(-0.4443f, 0.1115f, -0.114f);
                weights_2.transform.localPosition = new Vector3(-0.4443f, 0.1115f, -0.1056f);
                weights_3.transform.localPosition = new Vector3(-0.4443f, 0.1115f, -0.0879f);
                metka_na_lineuke.transform.localPosition = new Vector3(-0.4241998f, -0.113f, 0.0146f);
            }
        }
    }

    public void SetTransformSmall()
    {
        pruzina_small.transform.localRotation = Quaternion.Euler(0, 0, 0);

        if (small_name.Equals("pruzina03_1"))
        {
            if (kolvo == 1)
            {
                pruzina_small.transform.localPosition = new Vector3(0.9952f, 0.1378f, 1.1232f);
                pruzina_small.transform.localScale = new Vector3(1f, 1f, 0.94f);
                massa.transform.localPosition = new Vector3(-0.4443f, 0.1115f, 0.2203f);
                weights_0.transform.localPosition = new Vector3(-0.4443f, 0.1115f, 0.0703f);
                weights_1.transform.localPosition = new Vector3(-0.4443f, 0.1115f, 0.0753f);
                metka_na_lineuke.transform.localPosition = new Vector3(-0.4241998f, -0.113f, 0.2039f);
                kolvo++;
            }
            else if (kolvo == 2)
            {
                pruzina_small.transform.localScale = new Vector3(1f, 1f, 0.97f);
                massa.transform.localPosition = new Vector3(-0.4443f, 0.1115f, 0.2194f);
                weights_0.transform.localPosition = new Vector3(-0.4443f, 0.1115f, 0.0694f);
                weights_1.transform.localPosition = new Vector3(-0.4443f, 0.1115f, 0.0744f);
                weights_2.transform.localPosition = new Vector3(-0.4443f, 0.1115f, 0.0834f);
                metka_na_lineuke.transform.localPosition = new Vector3(-0.4241998f, -0.113f, 0.2034f);
                kolvo++;
            }
            else if(kolvo == 3)
            {
                pruzina_small.transform.localScale = new Vector3(1f, 1f, 0.98f);
                massa.transform.localPosition = new Vector3(-0.4443f, 0.1115f, 0.2162f);
                weights_0.transform.localPosition = new Vector3(-0.4443f, 0.1115f, 0.0662f);
                weights_1.transform.localPosition = new Vector3(-0.4443f, 0.1115f, 0.0712f);
                weights_2.transform.localPosition = new Vector3(-0.4443f, 0.1115f, 0.0802f);
                weights_3.transform.localPosition = new Vector3(-0.4443f, 0.1115f, 0.0972f);
                metka_na_lineuke.transform.localPosition = new Vector3(-0.4241998f, -0.113f, 0.2002f);
            }
        }
        else if (small_name.Equals("pruzina02_2"))
        {
            if (kolvo == 1)
            {
                pruzina_small.transform.localPosition = new Vector3(0.8749f, 0.1378f, 1.1232f);
                pruzina_small.transform.localScale = new Vector3(1f, 1f, 1.03f);
                massa.transform.localPosition = new Vector3(-0.4443f, 0.1115f, 0.1475f);
                weights_0.transform.localPosition = new Vector3(-0.4443f, 0.1115f, -0.0025f);
                weights_1.transform.localPosition = new Vector3(-0.4443f, 0.1115f, 0.0025f);
                metka_na_lineuke.transform.localPosition = new Vector3(-0.4241998f, -0.113f, 0.1315f);
                kolvo++;
            }
            else if (kolvo == 2)
            {
                pruzina_small.transform.localScale = new Vector3(1f, 1f, 1.04f);
                massa.transform.localPosition = new Vector3(-0.4443f, 0.1115f, 0.1427f);
                weights_0.transform.localPosition = new Vector3(-0.4443f, 0.1115f, -0.0073f);
                weights_1.transform.localPosition = new Vector3(-0.4443f, 0.1115f, -0.0023f);
                weights_2.transform.localPosition = new Vector3(-0.4443f, 0.1115f, 0.0067f);
                metka_na_lineuke.transform.localPosition = new Vector3(-0.4241998f, -0.113f, 0.1267f);
                kolvo++;
            }
            else if (kolvo == 3)
            {
                pruzina_small.transform.localScale = new Vector3(1f, 1f, 1.05f);
                massa.transform.localPosition = new Vector3(-0.4443f, 0.1115f, 0.1397f);
                weights_0.transform.localPosition = new Vector3(-0.4443f, 0.1115f, -0.0103f);
                weights_1.transform.localPosition = new Vector3(-0.4443f, 0.1115f, -0.0053f);
                weights_2.transform.localPosition = new Vector3(-0.4443f, 0.1115f, 0.0037f);
                weights_3.transform.localPosition = new Vector3(-0.4443f, 0.1115f, 0.0207f);
                metka_na_lineuke.transform.localPosition = new Vector3(-0.4241998f, -0.113f, 0.1237f);
            }
        }
        else
        {
            if (kolvo == 1)
            {
                pruzina_small.transform.localPosition = new Vector3(-0.4405f, 0.1088f, 0.3852f);
                pruzina_small.transform.localScale = new Vector3(1f, 1f, 1.01f);
                massa.transform.localPosition = new Vector3(-0.4443f, 0.1115f, 0.0679f);
                weights_0.transform.localPosition = new Vector3(-0.4443f, 0.1115f, -0.0821f);
                weights_1.transform.localPosition = new Vector3(-0.4443f, 0.1115f, -0.0771f);
                metka_na_lineuke.transform.localPosition = new Vector3(-0.4241998f, -0.113f, 0.0519f);
                kolvo++;
            }
            else if (kolvo == 2)
            {
                pruzina_small.transform.localScale = new Vector3(1f, 1f, 1.02f);
                massa.transform.localPosition = new Vector3(-0.4443f, 0.1115f, 0.0644f);
                weights_0.transform.localPosition = new Vector3(-0.4443f, 0.1115f, -0.0856f);
                weights_1.transform.localPosition = new Vector3(-0.4443f, 0.1115f, -0.0806f);
                weights_2.transform.localPosition = new Vector3(-0.4443f, 0.1115f, -0.0716f);
                metka_na_lineuke.transform.localPosition = new Vector3(-0.4241998f, -0.113f, 0.0484f);
                kolvo++;
            }
            else if (kolvo == 3)
            {
                pruzina_small.transform.localScale = new Vector3(1f, 1f, 1.04f);
                massa.transform.localPosition = new Vector3(-0.4443f, 0.1115f, 0.0603f);
                weights_0.transform.localPosition = new Vector3(-0.4443f, 0.1115f, -0.0897f);
                weights_1.transform.localPosition = new Vector3(-0.4443f, 0.1115f, -0.0847f);
                weights_2.transform.localPosition = new Vector3(-0.4443f, 0.1115f, -0.0757f);
                weights_3.transform.localPosition = new Vector3(-0.4443f, 0.1115f, -0.0587f);
                metka_na_lineuke.transform.localPosition = new Vector3(-0.4241998f, -0.113f, 0.0443f);
            }
        }
    }

    public void SetTransformParalelno()
    {
        pruzina_big.transform.localRotation = Quaternion.Euler(0, 0, 0);
        pruzina_small.transform.localRotation = Quaternion.Euler(0, 0, 0);
        begunok.transform.localRotation = Quaternion.Euler(0, 0, 0);

        if (big_name.Equals("pruzina06_1"))
        {
                if (kolvo == 1)
                {
                    pruzina_big.transform.localPosition = new Vector3(2.3972f, 0.1026f, 1.0802f);
                    pruzina_big.transform.localScale = new Vector3(1f, 1f, 0.95f);
                    pruzina_small.transform.localPosition = new Vector3(2.4431f, 0.1037f, 1.1048f);
                    pruzina_small.transform.localScale = new Vector3(1f, 1f, 0.95f);
                    palka.transform.localPosition = new Vector3(0.371f, 0.08f, 0.204f);
                    begunok.transform.localPosition = new Vector3(0.7456f, 0.076f, 0.201f);
                    massa.transform.localPosition = new Vector3(0.744f, 0.074f, 0.146f);
                    weights_0.transform.localPosition = new Vector3(0.745f, 0.082f, -0.004f);
                    weights_1.transform.localPosition = new Vector3(0.745f, 0.082f, 0.001f);
                    metka_na_lineuke.transform.localPosition = new Vector3(0.7796f, -0.113f, 0.1955f);
                    kolvo++;
                } else if (kolvo == 2)
                {
                    pruzina_big.transform.localScale = new Vector3(1f, 1f, 0.97f);
                    pruzina_small.transform.localScale = new Vector3(1f, 1f, 0.97f);
                    palka.transform.localPosition = new Vector3(0.371f, 0.08f, 0.2022f);
                    begunok.transform.localPosition = new Vector3(0.7456f, 0.076f, 0.1992f);
                    massa.transform.localPosition = new Vector3(0.744f, 0.074f, 0.1442f);
                    weights_0.transform.localPosition = new Vector3(0.745f, 0.082f, -0.0058f);
                    weights_1.transform.localPosition = new Vector3(0.745f, 0.082f, -0.0008f);
                    weights_2.transform.localPosition = new Vector3(0.745f, 0.082f, 0.0082f);
                    metka_na_lineuke.transform.localPosition = new Vector3(0.7796f, -0.113f, 0.1937f);
                    kolvo++;
            } else if (kolvo == 3)
                {
                    pruzina_big.transform.localScale = new Vector3(1f, 1f, 0.98f);
                    pruzina_small.transform.localScale = new Vector3(1f, 1f, 0.98f);
                    palka.transform.localPosition = new Vector3(0.371f, 0.08f, 0.2013f);
                    begunok.transform.localPosition = new Vector3(0.7456f, 0.076f, 0.1983f);
                    massa.transform.localPosition = new Vector3(0.744f, 0.074f, 0.1433f);
                    weights_0.transform.localPosition = new Vector3(0.745f, 0.082f, -0.0067f);
                    weights_1.transform.localPosition = new Vector3(0.745f, 0.082f, -0.0017f);
                    weights_2.transform.localPosition = new Vector3(0.745f, 0.082f, 0.0073f);
                    weights_3.transform.localPosition = new Vector3(0.745f, 0.082f, 0.0243f);
                    metka_na_lineuke.transform.localPosition = new Vector3(0.7796f, -0.113f, 0.1928f);
            }
        } else if (big_name.Equals("pruzina05_1"))
        {
                if (kolvo == 1)
                {
                    pruzina_big.transform.localPosition = new Vector3(2.2722f, 0.1058f, 1.0801f);
                    pruzina_big.transform.localScale = new Vector3(1f, 1f, 1.05f);
                    pruzina_small.transform.localPosition = new Vector3(2.323f, 0.1055f, 1.1024f);
                    pruzina_small.transform.localScale = new Vector3(1f, 1f, 1.05f);
                    palka.transform.localPosition = new Vector3(0.371f, 0.08f, 0.1269f);
                    begunok.transform.localPosition = new Vector3(0.7456f, 0.076f, 0.1239f);
                    massa.transform.localPosition = new Vector3(0.744f, 0.074f, 0.0689f);
                    weights_0.transform.localPosition = new Vector3(0.745f, 0.082f, -0.0811f);
                    weights_1.transform.localPosition = new Vector3(0.745f, 0.082f, -0.0761f);
                    metka_na_lineuke.transform.localPosition = new Vector3(0.7796f, -0.113f, 0.1179f);
                    kolvo++;
                }
                else if (kolvo == 2)
                {
                    pruzina_big.transform.localScale = new Vector3(1f, 1f, 1.06f);
                    pruzina_small.transform.localScale = new Vector3(1f, 1f, 1.06f);
                    palka.transform.localPosition = new Vector3(0.371f, 0.08f, 0.1251f);
                    begunok.transform.localPosition = new Vector3(0.7456f, 0.076f, 0.1221f);
                    massa.transform.localPosition = new Vector3(0.744f, 0.074f, 0.0671f);
                    weights_0.transform.localPosition = new Vector3(0.745f, 0.082f, -0.0829f);
                    weights_1.transform.localPosition = new Vector3(0.745f, 0.082f, -0.0779f);
                    weights_2.transform.localPosition = new Vector3(0.745f, 0.082f, -0.0689f);
                    metka_na_lineuke.transform.localPosition = new Vector3(0.7796f, -0.113f, 0.1161f);
                    kolvo++;
                }
                else if(kolvo == 3)
                {
                    pruzina_big.transform.localScale = new Vector3(1f, 1f, 1.07f);
                    pruzina_small.transform.localScale = new Vector3(1f, 1f, 1.07f);
                    palka.transform.localPosition = new Vector3(0.371f, 0.08f, 0.121f);
                    begunok.transform.localPosition = new Vector3(0.7456f, 0.076f, 0.118f);
                    massa.transform.localPosition = new Vector3(0.744f, 0.074f, 0.063f);
                    weights_0.transform.localPosition = new Vector3(0.745f, 0.082f, -0.087f);
                    weights_1.transform.localPosition = new Vector3(0.745f, 0.082f, -0.082f);
                    weights_2.transform.localPosition = new Vector3(0.745f, 0.082f, -0.073f);
                    weights_3.transform.localPosition = new Vector3(0.745f, 0.082f, -0.056f);
                    metka_na_lineuke.transform.localPosition = new Vector3(0.7796f, -0.113f, 0.112f);
                }
        } else
        {
                if (kolvo == 1)
                {
                    pruzina_big.transform.localPosition = new Vector3(2.1531f, 0.1037f, 1.0777f);
                    pruzina_big.transform.localScale = new Vector3(1f, 1f, 1.01f);
                    pruzina_small.transform.localPosition = new Vector3(1.002f, 0.083f, 0.3631f);
                    pruzina_small.transform.localScale = new Vector3(1f, 1f, 1.01f);
                    palka.transform.localPosition = new Vector3(0.371f, 0.08f, 0.0496f);
                    begunok.transform.localPosition = new Vector3(0.7456f, 0.076f, 0.0466f);
                    massa.transform.localPosition = new Vector3(0.744f, 0.074f, -0.0084f);
                    weights_0.transform.localPosition = new Vector3(0.745f, 0.082f, -0.1584f);
                    weights_1.transform.localPosition = new Vector3(0.745f, 0.082f, -0.1534f);
                    metka_na_lineuke.transform.localPosition = new Vector3(0.7796f, -0.113f, 0.0406f);
                    kolvo++;
                }
                else if (kolvo == 2)
                {
                    pruzina_big.transform.localScale = new Vector3(1f, 1f, 1.02f);
                    pruzina_small.transform.localScale = new Vector3(1f, 1f, 1.02f);
                    palka.transform.localPosition = new Vector3(0.371f, 0.08f, 0.0474f);
                    begunok.transform.localPosition = new Vector3(0.7456f, 0.076f, 0.0444f);
                    massa.transform.localPosition = new Vector3(0.744f, 0.074f, -0.0106f);
                    weights_0.transform.localPosition = new Vector3(0.745f, 0.082f, -0.1606f);
                    weights_1.transform.localPosition = new Vector3(0.745f, 0.082f, -0.1556f);
                    weights_2.transform.localPosition = new Vector3(0.745f, 0.082f, -0.1466f);
                    metka_na_lineuke.transform.localPosition = new Vector3(0.7796f, -0.113f, 0.0384f);
                    kolvo++;
                }
                else if (kolvo == 3)
                {
                    pruzina_big.transform.localScale = new Vector3(1f, 1f, 1.04f);
                    pruzina_small.transform.localScale = new Vector3(1f, 1f, 1.04f);
                    palka.transform.localPosition = new Vector3(0.371f, 0.08f, 0.0443f);
                    begunok.transform.localPosition = new Vector3(0.7456f, 0.076f, 0.0413f);
                    massa.transform.localPosition = new Vector3(0.744f, 0.074f, -0.0137f);
                    weights_0.transform.localPosition = new Vector3(0.745f, 0.082f, -0.1637f);
                    weights_1.transform.localPosition = new Vector3(0.745f, 0.082f, -0.1587f);
                    weights_2.transform.localPosition = new Vector3(0.745f, 0.082f, -0.1497f);
                    weights_3.transform.localPosition = new Vector3(0.745f, 0.082f, -0.1327f);
                    metka_na_lineuke.transform.localPosition = new Vector3(0.7796f, -0.113f, 0.0353f);
                }
        }
    }

    public void SetTransformPosledovatelno()
    {
        pruzina_big.transform.localRotation = Quaternion.Euler(0, 0, 0);
        pruzina_small.transform.localRotation = Quaternion.Euler(0, 0, 0);

        if (big_name.Equals("pruzina06_1"))
        {
            if (small_name.Equals("pruzina03_1"))
            {
                if (kolvo == 1)
                {
                    pruzina_big.transform.localPosition = new Vector3(1.4037f, 0.137799f, 1.1019f);
                    pruzina_big.transform.localScale = new Vector3(1f, 1f, 1f);
                    pruzina_small.transform.localPosition = new Vector3(0.9952f, 0.1378f, 0.9588f);
                    pruzina_small.transform.localScale = new Vector3(1f, 1f, 0.94f);
                    massa.transform.localPosition = new Vector3(-0.4443f, 0.1115f, 0.058f);
                    weights_0.transform.localPosition = new Vector3(-0.4443f, 0.1115f, -0.092f);
                    weights_1.transform.localPosition = new Vector3(-0.4443f, 0.1115f, -0.087f);
                    metka_na_lineuke.transform.localPosition = new Vector3(-0.4241998f, -0.113f, 0.0456f);
                    kolvo++;
                }
                else if (kolvo == 2)
                {
                    pruzina_big.transform.localScale = new Vector3(1f, 1f, 1.02f);
                    pruzina_small.transform.localPosition = new Vector3(0.9952f, 0.1378f, 0.9528f);
                    pruzina_small.transform.localScale = new Vector3(1f, 1f, 0.97f);
                    massa.transform.localPosition = new Vector3(-0.4443f, 0.1115f, 0.0514f);
                    weights_0.transform.localPosition = new Vector3(-0.4443f, 0.1115f, -0.0986f);
                    weights_1.transform.localPosition = new Vector3(-0.4443f, 0.1115f, -0.0936f);
                    weights_2.transform.localPosition = new Vector3(-0.4443f, 0.1115f, -0.0846f);
                    metka_na_lineuke.transform.localPosition = new Vector3(-0.4241998f, -0.113f, 0.039f);
                    kolvo++;
                }
                else if(kolvo == 3)
                {
                    pruzina_big.transform.localScale = new Vector3(1f, 1f, 1.08f);
                    pruzina_small.transform.localPosition = new Vector3(0.9952f, 0.1378f, 0.9404f);
                    pruzina_small.transform.localScale = new Vector3(1f, 1f, 0.98f);
                    massa.transform.localPosition = new Vector3(-0.4443f, 0.1115f, 0.0346f);
                    weights_0.transform.localPosition = new Vector3(-0.4443f, 0.1115f, -0.1154f);
                    weights_1.transform.localPosition = new Vector3(-0.4443f, 0.1115f, -0.1104f);
                    weights_2.transform.localPosition = new Vector3(-0.4443f, 0.1115f, -0.1014f);
                    weights_3.transform.localPosition = new Vector3(-0.4443f, 0.1115f, -0.0844f);
                    metka_na_lineuke.transform.localPosition = new Vector3(-0.4241998f, -0.113f, 0.0287f);
                    kolvo = 1;
                }
            }
            else if (small_name.Equals("pruzina02_2"))
            {
                if (kolvo == 1)
                {
                    pruzina_big.transform.localPosition = new Vector3(1.4037f, 0.137799f, 1.1019f);
                    pruzina_big.transform.localScale = new Vector3(1f, 1f, 1f);
                    pruzina_small.transform.localPosition = new Vector3(0.8749f, 0.1378f, 0.957f);
                    pruzina_small.transform.localScale = new Vector3(1f, 1f, 1.03f);
                    massa.transform.localPosition = new Vector3(-0.4443f, 0.1115f, -0.026f);
                    weights_0.transform.localPosition = new Vector3(-0.4443f, 0.1115f, -0.176f);
                    weights_1.transform.localPosition = new Vector3(-0.4443f, 0.1115f, -0.171f);
                    metka_na_lineuke.transform.localPosition = new Vector3(-0.4241998f, -0.113f, -0.032f);
                    kolvo++;
                }
                else if (kolvo == 2)
                {
                    pruzina_big.transform.localScale = new Vector3(1f, 1f, 1.02f);
                    pruzina_small.transform.localPosition = new Vector3(0.8749f, 0.1378f, 0.9518f);
                    pruzina_small.transform.localScale = new Vector3(1f, 1f, 1.04f);
                    massa.transform.localPosition = new Vector3(-0.4443f, 0.1115f, -0.03f);
                    weights_0.transform.localPosition = new Vector3(-0.4443f, 0.1115f, -0.18f);
                    weights_1.transform.localPosition = new Vector3(-0.4443f, 0.1115f, -0.175f);
                    weights_2.transform.localPosition = new Vector3(-0.4443f, 0.1115f, -0.166f);
                    metka_na_lineuke.transform.localPosition = new Vector3(-0.4241998f, -0.113f, -0.036f);
                    kolvo++;
                }
                else if (kolvo == 3)
                {
                    pruzina_big.transform.localScale = new Vector3(1f, 1f, 1.08f);
                    pruzina_small.transform.localPosition = new Vector3(0.8749f, 0.1378f, 0.9419f);
                    pruzina_small.transform.localScale = new Vector3(1f, 1f, 1.05f);
                    massa.transform.localPosition = new Vector3(-0.4443f, 0.1115f, -0.046f);
                    weights_0.transform.localPosition = new Vector3(-0.4443f, 0.1115f, -0.196f);
                    weights_1.transform.localPosition = new Vector3(-0.4443f, 0.1115f, -0.191f);
                    weights_2.transform.localPosition = new Vector3(-0.4443f, 0.1115f, -0.182f);
                    weights_3.transform.localPosition = new Vector3(-0.4443f, 0.1115f, -0.165f);
                    metka_na_lineuke.transform.localPosition = new Vector3(-0.4241998f, -0.113f, -0.052f);
                    kolvo = 1;
                }
            }
            else
            {
                if (kolvo == 1)
                {
                    pruzina_big.transform.localPosition = new Vector3(1.4037f, 0.137799f, 1.1019f);
                    pruzina_big.transform.localScale = new Vector3(1f, 1f, 1f);
                    pruzina_small.transform.localPosition = new Vector3(-0.4453f, 0.1088f, 0.217f);
                    pruzina_small.transform.localScale = new Vector3(1f, 1f, 1.01f);
                    massa.transform.localPosition = new Vector3(-0.4443f, 0.1115f, -0.1028f);
                    weights_0.transform.localPosition = new Vector3(-0.4443f, 0.1115f, -0.2528f);
                    weights_1.transform.localPosition = new Vector3(-0.4443f, 0.1115f, -0.2478f);
                    metka_na_lineuke.transform.localPosition = new Vector3(-0.4241998f, -0.113f, -0.1088f);
                    kolvo++;
                }
                else if (kolvo == 2)
                {
                    pruzina_big.transform.localScale = new Vector3(1f, 1f, 1.02f);
                    pruzina_small.transform.localPosition = new Vector3(-0.4453f, 0.1088f, 0.2088f);
                    pruzina_small.transform.localScale = new Vector3(1f, 1f, 1.02f);
                    massa.transform.localPosition = new Vector3(-0.4443f, 0.1115f, -0.1086f);
                    weights_0.transform.localPosition = new Vector3(-0.4443f, 0.1115f, -0.2586f);
                    weights_1.transform.localPosition = new Vector3(-0.4443f, 0.1115f, -0.2536f);
                    weights_2.transform.localPosition = new Vector3(-0.4443f, 0.1115f, -0.2446f);
                    metka_na_lineuke.transform.localPosition = new Vector3(-0.4241998f, -0.113f, -0.1146f);
                    kolvo++;
                }
                else if (kolvo == 3)
                {
                    pruzina_big.transform.localScale = new Vector3(1f, 1f, 1.08f);
                    pruzina_small.transform.localPosition = new Vector3(-0.4453f, 0.1088f, 0.202f);
                    pruzina_small.transform.localScale = new Vector3(1f, 1f, 1.04f);
                    massa.transform.localPosition = new Vector3(-0.4443f, 0.1115f, -0.124f);
                    weights_0.transform.localPosition = new Vector3(-0.4443f, 0.1115f, -0.274f);
                    weights_1.transform.localPosition = new Vector3(-0.4443f, 0.1115f, -0.269f);
                    weights_2.transform.localPosition = new Vector3(-0.4443f, 0.1115f, -0.26f);
                    weights_3.transform.localPosition = new Vector3(-0.4443f, 0.1115f, -0.243f);
                    metka_na_lineuke.transform.localPosition = new Vector3(-0.4241998f, -0.113f, -0.13f);
                    kolvo = 1;
                }
            }
        }
        else if (big_name.Equals("pruzina05_1"))
        {
            if (small_name.Equals("pruzina03_1"))
            {
                if (kolvo == 1)
                {
                    pruzina_big.transform.localPosition = new Vector3(1.284f, 0.137799f, 1.1019f);
                    pruzina_big.transform.localScale = new Vector3(1f, 1f, 1.08f);
                    pruzina_small.transform.localPosition = new Vector3(0.9952f, 0.1378f, 0.8789f);
                    pruzina_small.transform.localScale = new Vector3(1f, 1f, 0.94f);
                    massa.transform.localPosition = new Vector3(-0.4443f, 0.1115f, -0.022f);
                    weights_0.transform.localPosition = new Vector3(-0.4443f, 0.1115f, -0.172f);
                    weights_1.transform.localPosition = new Vector3(-0.4443f, 0.1115f, -0.167f);
                    metka_na_lineuke.transform.localPosition = new Vector3(-0.4241998f, -0.113f, -0.0329f);
                    kolvo++;
                }
                else if (kolvo == 2)
                {
                    pruzina_big.transform.localScale = new Vector3(1f, 1f, 1.09f);
                    pruzina_small.transform.localPosition = new Vector3(0.9952f, 0.1378f, 0.8733f);
                    pruzina_small.transform.localScale = new Vector3(1f, 1f, 0.97f);
                    massa.transform.localPosition = new Vector3(-0.4443f, 0.1115f, -0.0257f);
                    weights_0.transform.localPosition = new Vector3(-0.4443f, 0.1115f, -0.1757f);
                    weights_1.transform.localPosition = new Vector3(-0.4443f, 0.1115f, -0.1707f);
                    weights_2.transform.localPosition = new Vector3(-0.4443f, 0.1115f, -0.1617f);
                    metka_na_lineuke.transform.localPosition = new Vector3(-0.4241998f, -0.113f, -0.0366f);
                    kolvo++;
                }
                else if (kolvo == 3)
                {
                    pruzina_big.transform.localScale = new Vector3(1f, 1f, 1.15f);
                    pruzina_small.transform.localPosition = new Vector3(0.9952f, 0.1378f, 0.8572f);
                    pruzina_small.transform.localScale = new Vector3(1f, 1f, 0.98f);
                    massa.transform.localPosition = new Vector3(-0.4443f, 0.1115f, -0.0428f);
                    weights_0.transform.localPosition = new Vector3(-0.4443f, 0.1115f, -0.1928f);
                    weights_1.transform.localPosition = new Vector3(-0.4443f, 0.1115f, -0.1878f);
                    weights_2.transform.localPosition = new Vector3(-0.4443f, 0.1115f, -0.1788f);
                    weights_3.transform.localPosition = new Vector3(-0.4443f, 0.1115f, -0.1618f);
                    metka_na_lineuke.transform.localPosition = new Vector3(-0.4241998f, -0.113f, -0.0537f);
                    kolvo = 1;
                }
            }
            else if (small_name.Equals("pruzina02_2"))
            {
                if (kolvo == 1)
                {
                    pruzina_big.transform.localPosition = new Vector3(1.284f, 0.137799f, 1.1019f);
                    pruzina_big.transform.localScale = new Vector3(1f, 1f, 1.08f);
                    pruzina_small.transform.localPosition = new Vector3(0.8749f, 0.1378f, 0.876f);
                    pruzina_small.transform.localScale = new Vector3(1f, 1f, 1.03f);
                    massa.transform.localPosition = new Vector3(-0.4443f, 0.1115f, -0.0979f);
                    weights_0.transform.localPosition = new Vector3(-0.4443f, 0.1115f, -0.2479f);
                    weights_1.transform.localPosition = new Vector3(-0.4443f, 0.1115f, -0.2429f);
                    metka_na_lineuke.transform.localPosition = new Vector3(-0.4241998f, -0.113f, -0.1088f);
                    kolvo++;
                }
                else if (kolvo == 2)
                {
                    pruzina_big.transform.localScale = new Vector3(1f, 1f, 1.09f);
                    pruzina_small.transform.localPosition = new Vector3(0.8749f, 0.1378f, 0.873f);
                    pruzina_small.transform.localScale = new Vector3(1f, 1f, 1.04f);
                    massa.transform.localPosition = new Vector3(-0.4443f, 0.1115f, -0.1041f);
                    weights_0.transform.localPosition = new Vector3(-0.4443f, 0.1115f, -0.2541f);
                    weights_1.transform.localPosition = new Vector3(-0.4443f, 0.1115f, -0.2491f);
                    weights_2.transform.localPosition = new Vector3(-0.4443f, 0.1115f, -0.2401f);
                    metka_na_lineuke.transform.localPosition = new Vector3(-0.4241998f, -0.113f, -0.115f);
                    kolvo++;
                }
                else if (kolvo == 3)
                {
                    pruzina_big.transform.localScale = new Vector3(1f, 1f, 1.15f);
                    pruzina_small.transform.localPosition = new Vector3(0.8749f, 0.1378f, 0.8601f);
                    pruzina_small.transform.localScale = new Vector3(1f, 1f, 1.05f);
                    massa.transform.localPosition = new Vector3(-0.4443f, 0.1115f, -0.1216f);
                    weights_0.transform.localPosition = new Vector3(-0.4443f, 0.1115f, -0.2716f);
                    weights_1.transform.localPosition = new Vector3(-0.4443f, 0.1115f, -0.2666f);
                    weights_2.transform.localPosition = new Vector3(-0.4443f, 0.1115f, -0.2576f);
                    weights_3.transform.localPosition = new Vector3(-0.4443f, 0.1115f, -0.2406f);
                    metka_na_lineuke.transform.localPosition = new Vector3(-0.4241998f, -0.113f, -0.1325f);
                    kolvo = 1;
                }
            }
            else
            {
                if (kolvo == 1)
                {
                    pruzina_big.transform.localPosition = new Vector3(1.284f, 0.137799f, 1.1019f);
                    pruzina_big.transform.localScale = new Vector3(1f, 1f, 1.08f);
                    pruzina_small.transform.localPosition = new Vector3(-0.4405f, 0.1088f, 0.138f);
                    pruzina_small.transform.localScale = new Vector3(1f, 1f, 1.01f);
                    massa.transform.localPosition = new Vector3(-0.4443f, 0.1115f, -0.176f);
                    weights_0.transform.localPosition = new Vector3(-0.4443f, 0.1115f, -0.326f);
                    weights_1.transform.localPosition = new Vector3(-0.4443f, 0.1115f, -0.321f);
                    metka_na_lineuke.transform.localPosition = new Vector3(-0.4241998f, -0.113f, -0.1869f);
                    kolvo++;
                }
                else if (kolvo == 2)
                {
                    pruzina_big.transform.localScale = new Vector3(1f, 1f, 1.09f);
                    pruzina_small.transform.localPosition = new Vector3(-0.4405f, 0.1088f, 0.1343f);
                    pruzina_small.transform.localScale = new Vector3(1f, 1f, 1.02f);
                    massa.transform.localPosition = new Vector3(-0.4443f, 0.1115f, -0.1826f);
                    weights_0.transform.localPosition = new Vector3(-0.4443f, 0.1115f, -0.3326f);
                    weights_1.transform.localPosition = new Vector3(-0.4443f, 0.1115f, -0.3276f);
                    weights_2.transform.localPosition = new Vector3(-0.4443f, 0.1115f, -0.3186f);
                    metka_na_lineuke.transform.localPosition = new Vector3(-0.4241998f, -0.113f, -0.1935f);
                    kolvo++;
                }
                else if (kolvo == 3)
                {
                    pruzina_big.transform.localScale = new Vector3(1f, 1f, 1.15f);
                    pruzina_small.transform.localPosition = new Vector3(-0.4405f, 0.1088f, 0.1219f);
                    pruzina_small.transform.localScale = new Vector3(1f, 1f, 1.04f);
                    massa.transform.localPosition = new Vector3(-0.4443f, 0.1115f, -0.2024f);
                    weights_0.transform.localPosition = new Vector3(-0.4443f, 0.1115f, -0.3524f);
                    weights_1.transform.localPosition = new Vector3(-0.4443f, 0.1115f, -0.3474f);
                    weights_2.transform.localPosition = new Vector3(-0.4443f, 0.1115f, -0.3384f);
                    weights_3.transform.localPosition = new Vector3(-0.4443f, 0.1115f, -0.3214f);
                    metka_na_lineuke.transform.localPosition = new Vector3(-0.4241998f, -0.113f, -0.2133f);
                    kolvo = 1;
                }
            }
        }
        else
        {
            if (small_name.Equals("pruzina03_1"))
            {
                if (kolvo == 1)
                {
                    pruzina_big.transform.localPosition = new Vector3(1.167f, 0.137799f, 1.1019f);
                    pruzina_big.transform.localScale = new Vector3(1f, 1f, 1.04f);
                    pruzina_small.transform.localPosition = new Vector3(0.9952f, 0.138f, 0.7944f);
                    pruzina_small.transform.localScale = new Vector3(1f, 1f, 0.94f);
                    massa.transform.localPosition = new Vector3(-0.4443f, 0.1115f, -0.1016f);
                    weights_0.transform.localPosition = new Vector3(-0.4443f, 0.1115f, -0.2516f);
                    weights_1.transform.localPosition = new Vector3(-0.4443f, 0.1115f, -0.2466f);
                    metka_na_lineuke.transform.localPosition = new Vector3(-0.4241998f, -0.113f, -0.1125f);
                    kolvo++;
                }
                else if (kolvo == 2)
                {
                    pruzina_big.transform.localScale = new Vector3(1f, 1f, 1.06f);
                    pruzina_small.transform.localPosition = new Vector3(0.9952f, 0.138f, 0.7878f);
                    pruzina_small.transform.localScale = new Vector3(1f, 1f, 0.97f);
                    massa.transform.localPosition = new Vector3(-0.4443f, 0.1115f, -0.1121f);
                    weights_0.transform.localPosition = new Vector3(-0.4443f, 0.1115f, -0.2612f);
                    weights_1.transform.localPosition = new Vector3(-0.4443f, 0.1115f, -0.2574f);
                    weights_2.transform.localPosition = new Vector3(-0.4443f, 0.1115f, -0.2496f);
                    metka_na_lineuke.transform.localPosition = new Vector3(-0.4241998f, -0.113f, -0.123f);
                    kolvo++;
                }
                else if (kolvo == 3)
                {
                    pruzina_big.transform.localScale = new Vector3(1f, 1f, 1.11f);
                    pruzina_small.transform.localPosition = new Vector3(0.9952f, 0.138f, 0.7708f);
                    pruzina_small.transform.localScale = new Vector3(1f, 1f, 0.98f);
                    massa.transform.localPosition = new Vector3(-0.4443f, 0.1115f, -0.1319f);
                    weights_0.transform.localPosition = new Vector3(-0.4443f, 0.1115f, -0.2819f);
                    weights_1.transform.localPosition = new Vector3(-0.4443f, 0.1115f, -0.2769f);
                    weights_2.transform.localPosition = new Vector3(-0.4443f, 0.1115f, -0.2679f);
                    weights_3.transform.localPosition = new Vector3(-0.4443f, 0.1115f, -0.2509f);
                    metka_na_lineuke.transform.localPosition = new Vector3(-0.4241998f, -0.113f, -0.1428f);
                    kolvo = 1;
                }
            }
            else if (small_name.Equals("pruzina02_2"))
            {
                if (kolvo == 1)
                {
                    pruzina_big.transform.localPosition = new Vector3(1.167f, 0.137799f, 1.1019f);
                    pruzina_big.transform.localScale = new Vector3(1f, 1f, 1.04f);
                    pruzina_small.transform.localPosition = new Vector3(0.8749f, 0.1378f, 0.7976f);
                    pruzina_small.transform.localScale = new Vector3(1f, 1f, 1.03f);
                    massa.transform.localPosition = new Vector3(-0.4443f, 0.1115f, -0.178f);
                    weights_0.transform.localPosition = new Vector3(-0.4443f, 0.1115f, -0.328f);
                    weights_1.transform.localPosition = new Vector3(-0.4443f, 0.1115f, -0.323f);
                    metka_na_lineuke.transform.localPosition = new Vector3(-0.4241998f, -0.113f, -0.1889f);
                    kolvo++;
                }
                else if (kolvo == 2)
                {
                    pruzina_big.transform.localScale = new Vector3(1f, 1f, 1.06f);
                    pruzina_small.transform.localPosition = new Vector3(0.8749f, 0.1378f, 0.7898f);
                    pruzina_small.transform.localScale = new Vector3(1f, 1f, 1.04f);
                    massa.transform.localPosition = new Vector3(-0.4443f, 0.1115f, -0.1887f);
                    weights_0.transform.localPosition = new Vector3(-0.4443f, 0.1115f, -0.3369f);
                    weights_1.transform.localPosition = new Vector3(-0.4443f, 0.1115f, -0.3329f);
                    weights_2.transform.localPosition = new Vector3(-0.4443f, 0.1115f, -0.3239f);
                    metka_na_lineuke.transform.localPosition = new Vector3(-0.4241998f, -0.113f, -0.1996f);
                    kolvo++;
                }
                else if (kolvo == 3)
                {
                    pruzina_big.transform.localScale = new Vector3(1f, 1f, 1.11f);
                    pruzina_small.transform.localPosition = new Vector3(0.8749f, 0.1378f, 0.7741f);
                    pruzina_small.transform.localScale = new Vector3(1f, 1f, 1.05f);
                    massa.transform.localPosition = new Vector3(-0.4443f, 0.1115f, -0.2078f);
                    weights_0.transform.localPosition = new Vector3(-0.4443f, 0.1115f, -0.3578f);
                    weights_1.transform.localPosition = new Vector3(-0.4443f, 0.1115f, -0.3528f);
                    weights_2.transform.localPosition = new Vector3(-0.4443f, 0.1115f, -0.3438f);
                    weights_3.transform.localPosition = new Vector3(-0.4443f, 0.1115f, -0.3268f);
                    metka_na_lineuke.transform.localPosition = new Vector3(-0.4241998f, -0.113f, -0.2187f);
                    kolvo = 1;
                }
            }
            else
            {
                if (kolvo == 1)
                {
                    pruzina_big.transform.localPosition = new Vector3(1.167f, 0.137799f, 1.1019f);
                    pruzina_big.transform.localScale = new Vector3(1f, 1f, 1.04f);
                    pruzina_small.transform.localPosition = new Vector3(-0.4405f, 0.1088f, 0.0569f);
                    pruzina_small.transform.localScale = new Vector3(1f, 1f, 1.01f);
                    massa.transform.localPosition = new Vector3(-0.4443f, 0.1115f, -0.2556f);
                    weights_0.transform.localPosition = new Vector3(-0.4443f, 0.1115f, -0.4056f);
                    weights_1.transform.localPosition = new Vector3(-0.4443f, 0.1115f, -0.4006f);
                    metka_na_lineuke.transform.localPosition = new Vector3(-0.4241998f, -0.113f, -0.2665f);
                    kolvo++;
                }
                else if (kolvo == 2)
                {
                    pruzina_big.transform.localScale = new Vector3(1f, 1f, 1.06f);
                    pruzina_small.transform.localPosition = new Vector3(-0.4405f, 0.1088f, 0.0504f);
                    pruzina_small.transform.localScale = new Vector3(1f, 1f, 1.02f);
                    massa.transform.localPosition = new Vector3(-0.4443f, 0.1115f, -0.2658f);
                    weights_0.transform.localPosition = new Vector3(-0.4443f, 0.1115f, -0.4158f);
                    weights_1.transform.localPosition = new Vector3(-0.4443f, 0.1115f, -0.4108f);
                    weights_2.transform.localPosition = new Vector3(-0.4443f, 0.1115f, -0.4018f);
                    metka_na_lineuke.transform.localPosition = new Vector3(-0.4241998f, -0.113f, -0.2767f);
                    kolvo++;
                }
                else if (kolvo == 3)
                {
                    pruzina_big.transform.localScale = new Vector3(1f, 1f, 1.11f);
                    pruzina_small.transform.localPosition = new Vector3(-0.4405f, 0.1088f, 0.0334f);
                    pruzina_small.transform.localScale = new Vector3(1f, 1f, 1.04f);
                    massa.transform.localPosition = new Vector3(-0.4443f, 0.1115f, -0.2889f);
                    weights_0.transform.localPosition = new Vector3(-0.4443f, 0.1115f, -0.4389f);
                    weights_1.transform.localPosition = new Vector3(-0.4443f, 0.1115f, -0.4339f);
                    weights_2.transform.localPosition = new Vector3(-0.4443f, 0.1115f, -0.4249f);
                    weights_3.transform.localPosition = new Vector3(-0.4443f, 0.1115f, -0.4079f);
                    metka_na_lineuke.transform.localPosition = new Vector3(-0.4241998f, -0.113f, -0.2998f);
                    kolvo = 1;
                }
            }
        }
    }

    public void SetPruzinaLast()
    {
        Scenario_step_AnimationState anima;

        if (big_name_last.Equals("pruzina06_1"))
        {
            if (kolvo == 1)
            {
                ustanovka = GameObject.Find("Animation_06_5m");
                anima = (Scenario_step_AnimationState)ustanovka.GetComponent(typeof(Scenario_step_AnimationState));
                anima.SetTargetTime(10);

                kolvo++;

                time = GameObject.Find("Waiter lab 1 1");
                timer = (Scenario_step_waiter)time.GetComponent(typeof(Scenario_step_waiter));

                timer.delay = 13.5f;
            }
            else if (kolvo == 2)
            {
                ustanovka = GameObject.Find("Animation_06_M");
                anima = (Scenario_step_AnimationState)ustanovka.GetComponent(typeof(Scenario_step_AnimationState));
                anima.SetTargetTime(10);

                kolvo++;

                time = GameObject.Find("Waiter lab 1 2");
                timer = (Scenario_step_waiter)time.GetComponent(typeof(Scenario_step_waiter));

                timer.delay = 23.5f;
            }
            else if (kolvo == 3)
            {
                ustanovka = GameObject.Find("Animation_03_3m");
                anima = (Scenario_step_AnimationState)ustanovka.GetComponent(typeof(Scenario_step_AnimationState));
                anima.SetTargetTime(10);

                kolvo++;

                time = GameObject.Find("Waiter lab 1 3");
                timer = (Scenario_step_waiter)time.GetComponent(typeof(Scenario_step_waiter));

                timer.delay = 5.5f;
            }
            else if (kolvo == 4)
            {
                ustanovka = GameObject.Find("Animation_06_03_5m");
                anima = (Scenario_step_AnimationState)ustanovka.GetComponent(typeof(Scenario_step_AnimationState));
                anima.SetTargetTime(10);

                kolvo++;

                time = GameObject.Find("Waiter lab 1 4");
                timer = (Scenario_step_waiter)time.GetComponent(typeof(Scenario_step_waiter));

                timer.delay = 6.5f;
            }
            else
            {
                ustanovka = GameObject.Find("Animation_06_03_M");
                anima = (Scenario_step_AnimationState)ustanovka.GetComponent(typeof(Scenario_step_AnimationState));
                anima.SetTargetTime(10);

                time = GameObject.Find("Waiter lab 1 5");
                timer = (Scenario_step_waiter)time.GetComponent(typeof(Scenario_step_waiter));

                timer.delay = 11f;
            }
        } else if (big_name_last.Equals("pruzina05_1"))
        {
            if (kolvo == 1)
            {
                ustanovka = GameObject.Find("Animation_05_5m");
                anima = (Scenario_step_AnimationState)ustanovka.GetComponent(typeof(Scenario_step_AnimationState));
                anima.SetTargetTime(10);

                kolvo++;

                time = GameObject.Find("Waiter lab 1 1");
                timer = (Scenario_step_waiter)time.GetComponent(typeof(Scenario_step_waiter));

                timer.delay = 15.5f;
            }
            else if (kolvo == 2)
            {
                ustanovka = GameObject.Find("Animation_05_M");
                anima = (Scenario_step_AnimationState)ustanovka.GetComponent(typeof(Scenario_step_AnimationState));
                anima.SetTargetTime(10);

                kolvo++;

                time = GameObject.Find("Waiter lab 1 2");
                timer = (Scenario_step_waiter)time.GetComponent(typeof(Scenario_step_waiter));

                timer.delay = 27f;
            }
            else if (kolvo == 3)
            {
                ustanovka = GameObject.Find("Animation_02_3m");
                anima = (Scenario_step_AnimationState)ustanovka.GetComponent(typeof(Scenario_step_AnimationState));
                anima.SetTargetTime(10);

                kolvo++;

                time = GameObject.Find("Waiter lab 1 3");
                timer = (Scenario_step_waiter)time.GetComponent(typeof(Scenario_step_waiter));

                timer.delay = 6f;
            }
            else if (kolvo == 4)
            {
                ustanovka = GameObject.Find("Animation_05_02_5m");
                anima = (Scenario_step_AnimationState)ustanovka.GetComponent(typeof(Scenario_step_AnimationState));
                anima.SetTargetTime(10);

                kolvo++;

                time = GameObject.Find("Waiter lab 1 4");
                timer = (Scenario_step_waiter)time.GetComponent(typeof(Scenario_step_waiter));

                timer.delay = 7f;
            }
            else
            {
                ustanovka = GameObject.Find("Animation_05_02_M");
                anima = (Scenario_step_AnimationState)ustanovka.GetComponent(typeof(Scenario_step_AnimationState));
                anima.SetTargetTime(10);


                time = GameObject.Find("Waiter lab 1 5");
                timer = (Scenario_step_waiter)time.GetComponent(typeof(Scenario_step_waiter));

                timer.delay = 12f;
            }
        }
        else
        {
            if (kolvo == 1)
            {
                ustanovka = GameObject.Find("Animation_04_5m");
                anima = (Scenario_step_AnimationState)ustanovka.GetComponent(typeof(Scenario_step_AnimationState));
                anima.SetTargetTime(10);

                kolvo++;

                time = GameObject.Find("Waiter lab 1 1");
                timer = (Scenario_step_waiter)time.GetComponent(typeof(Scenario_step_waiter));

                timer.delay = 18f;
            }
            else if (kolvo == 2)
            {
                ustanovka = GameObject.Find("Animation_04_M");
                anima = (Scenario_step_AnimationState)ustanovka.GetComponent(typeof(Scenario_step_AnimationState));
                anima.SetTargetTime(10);

                kolvo++;

                time = GameObject.Find("Waiter lab 1 2");
                timer = (Scenario_step_waiter)time.GetComponent(typeof(Scenario_step_waiter));

                timer.delay = 33f;
            }
            else if (kolvo == 3)
            {
                ustanovka = GameObject.Find("Animation_01_3m");
                anima = (Scenario_step_AnimationState)ustanovka.GetComponent(typeof(Scenario_step_AnimationState));
                anima.SetTargetTime(10);

                kolvo++;

                time = GameObject.Find("Waiter lab 1 3");
                timer = (Scenario_step_waiter)time.GetComponent(typeof(Scenario_step_waiter));

                timer.delay = 7f;
            }
            else if (kolvo == 4)
            {
                ustanovka = GameObject.Find("Animation_04_01_5m");
                anima = (Scenario_step_AnimationState)ustanovka.GetComponent(typeof(Scenario_step_AnimationState));
                anima.SetTargetTime(10);

                kolvo++;

                time = GameObject.Find("Waiter lab 1 4");
                timer = (Scenario_step_waiter)time.GetComponent(typeof(Scenario_step_waiter));

                timer.delay = 9f;
            }
            else
            {
                ustanovka = GameObject.Find("Animation_04_01_M");
                anima = (Scenario_step_AnimationState)ustanovka.GetComponent(typeof(Scenario_step_AnimationState));
                anima.SetTargetTime(10);


                time = GameObject.Find("Waiter lab 1 5");
                timer = (Scenario_step_waiter)time.GetComponent(typeof(Scenario_step_waiter));

                timer.delay = 15f;
            }
        }
    }

    public void SetUstanovka()
    {
        massa = GameObject.Find("massa");
        weights_0 = GameObject.Find("weights_0");
        weights_1 = GameObject.Find("weights_1");
        weights_2 = GameObject.Find("weights_2");
        weights_3 = GameObject.Find("weights_3");
        palka = GameObject.Find("palka");
        begunok = GameObject.Find("begunok");

        if (big_name_last.Equals("pruzina06_1"))
        {
            pruzina_big = GameObject.Find("pruzina06_1");
            pruzina_small = GameObject.Find("pruzina03_1");

            if (ust_kol == 1)
            {
                pruzina_big.transform.localPosition = new Vector3(0f, 0f, -100f);
                massa.transform.localPosition = new Vector3(-1.552f, -0.366f, -100f);
                weights_0.transform.localPosition = new Vector3(-1.332f, -0.184f, -100f);
                weights_1.transform.localPosition = new Vector3(-1.449f, -0.184f, -100f);

                ustanovka = GameObject.Find("Animation_06_5m");
                if (setactive == true)
                {
                    ustanovka.transform.localPosition = new Vector3(0f, 0f, 0f);
                    setactive = false;
                } else
                {
                    ustanovka.transform.localPosition = new Vector3(0f, 0f, -100f);
                    setactive = true;
                    ust_kol++;
                }
            }
            else if (ust_kol == 2)
            {
                weights_2.transform.localPosition = new Vector3(-1.565f, -0.184f, -100f);
                weights_3.transform.localPosition = new Vector3(-1.679f, -0.184f, -100f);
                ustanovka = GameObject.Find("Animation_06_M");
                if (setactive == true)
                {
                    ustanovka.transform.localPosition = new Vector3(0f, 0f, 0f);
                    setactive = false;
                }
                else
                {
                    ustanovka.transform.localPosition = new Vector3(0f, 0f, -100f);
                    setactive = true;
                    ust_kol++;
                }
            }
            else if (ust_kol == 3)
            {
                pruzina_small.transform.localPosition = new Vector3(0f, 0f, -100f);
                massa.transform.localPosition = new Vector3(-1.552f, -0.366f, -100f);
                ustanovka = GameObject.Find("Animation_03_3m");
                if (setactive == true)
                {
                    ustanovka.transform.localPosition = new Vector3(0.9733635f, 0.1440007f, 0.6995002f);
                    setactive = false;
                }
                else
                {
                    ustanovka.transform.localPosition = new Vector3(0f, 0f, -100f);
                    setactive = true;
                    ust_kol++;
                }
            }
            else if (ust_kol == 4)
            {
                pruzina_big.transform.localPosition = new Vector3(0f, 0f, -100f);
                pruzina_small.transform.localPosition = new Vector3(0f, 0f, -100f);
                massa.transform.localPosition = new Vector3(-1.552f, -0.366f, -100f);
                weights_0.transform.localPosition = new Vector3(-1.332f, -0.184f, -100f);
                weights_1.transform.localPosition = new Vector3(-1.449f, -0.184f, -100f);
                palka.transform.localPosition = new Vector3(-2.127f, -0.576f, -100f);
                begunok.transform.localPosition = new Vector3(-1.82f, -0.233f, -100f);
                ustanovka = GameObject.Find("Animation_06_03_5m");
                if (setactive == true)
                {
                    ustanovka.transform.localPosition = new Vector3(0.9733635f, 0.1440007f, 0.6995002f);
                    setactive = false;
                }
                else
                {
                    ustanovka.transform.localPosition = new Vector3(0f, 0f, -100f);
                    setactive = true;
                    ust_kol++;
                }
            }
            else
            {
                weights_2.transform.localPosition = new Vector3(-1.332f, -0.184f, -100f);
                weights_3.transform.localPosition = new Vector3(-1.449f, -0.184f, -100f);
                ustanovka = GameObject.Find("Animation_06_03_M");
                if (setactive == true)
                {
                    ustanovka.transform.localPosition = new Vector3(0.9733635f, 0.1440007f, 0.6995002f);
                    setactive = false;
                }
                else
                {
                    ustanovka.transform.localPosition = new Vector3(0f, 0f, -100f);
                    setactive = true;
                }
            }
        }
        else if (big_name_last.Equals("pruzina05_1"))
        {
            pruzina_big = GameObject.Find("pruzina05_1");
            pruzina_small = GameObject.Find("pruzina02_2");

            if (ust_kol == 1)
            {
                pruzina_big.transform.localPosition = new Vector3(0f, 0f, -100f);
                massa.transform.localPosition = new Vector3(-1.552f, -0.366f, -100f);
                weights_0.transform.localPosition = new Vector3(-1.332f, -0.184f, -100f);
                weights_1.transform.localPosition = new Vector3(-1.449f, -0.184f, -100f);

                ustanovka = GameObject.Find("Animation_05_5m");
                if (setactive == true)
                {
                    ustanovka.transform.localPosition = new Vector3(0f, 0f, 0f);
                    setactive = false;
                }
                else
                {
                    ustanovka.transform.localPosition = new Vector3(0f, 0f, -100f);
                    setactive = true;
                    ust_kol++;
                }
            }
            else if (ust_kol == 2)
            {
                weights_2.transform.localPosition = new Vector3(-1.565f, -0.184f, -100f);
                weights_3.transform.localPosition = new Vector3(-1.679f, -0.184f, -100f);

                ustanovka = GameObject.Find("Animation_05_M");
                if (setactive == true)
                {
                    ustanovka.transform.localPosition = new Vector3(0f, 0f, 0f);
                    setactive = false;
                }
                else
                {
                    ustanovka.transform.localPosition = new Vector3(0f, 0f, -100f);
                    setactive = true;
                    ust_kol++;
                }
            }
            else if (ust_kol == 3)
            {
                pruzina_small.transform.localPosition = new Vector3(0f, 0f, -100f);
                massa.transform.localPosition = new Vector3(-1.552f, -0.366f, -100f);

                ustanovka = GameObject.Find("Animation_02_3m");
                if (setactive == true)
                {
                    ustanovka.transform.localPosition = new Vector3(0.9733635f, 0.1440007f, 0.6995002f);
                    setactive = false;
                }
                else
                {
                    ustanovka.transform.localPosition = new Vector3(0f, 0f, -100f);
                    setactive = true;
                    ust_kol++;
                }
            }
            else if (ust_kol == 4)
            {
                pruzina_big.transform.localPosition = new Vector3(0f, 0f, -100f);
                pruzina_small.transform.localPosition = new Vector3(0f, 0f, -100f);
                massa.transform.localPosition = new Vector3(-1.552f, -0.366f, -100f);
                weights_0.transform.localPosition = new Vector3(-1.332f, -0.184f, -100f);
                weights_1.transform.localPosition = new Vector3(-1.449f, -0.184f, -100f);
                palka.transform.localPosition = new Vector3(-2.127f, -0.576f, -100f);
                begunok.transform.localPosition = new Vector3(-1.82f, -0.233f, -100f);

                ustanovka = GameObject.Find("Animation_05_02_5m");
                if (setactive == true)
                {
                    ustanovka.transform.localPosition = new Vector3(0.9733635f, 0.1440007f, 0.6995002f);
                    setactive = false;
                }
                else
                {
                    ustanovka.transform.localPosition = new Vector3(0f, 0f, -100f);
                    setactive = true;
                    ust_kol++;
                }
            }
            else
            {
                weights_2.transform.localPosition = new Vector3(-1.332f, -0.184f, -100f);
                weights_3.transform.localPosition = new Vector3(-1.449f, -0.184f, -100f);

                ustanovka = GameObject.Find("Animation_05_02_M");
                if (setactive == true)
                {
                    ustanovka.transform.localPosition = new Vector3(0.9733635f, 0.1440007f, 0.6995002f);
                    setactive = false;
                }
                else
                {
                    ustanovka.transform.localPosition = new Vector3(0f, 0f, -100f);
                    setactive = true;
                }
            }
        }
        else
        {
            pruzina_big = GameObject.Find("pruzina04_1");
            pruzina_small = GameObject.Find("pruzina01");
            ustanovka = GameObject.Find("Animation_04_5m");
            if (ust_kol == 1)
            {
                pruzina_big.transform.localPosition = new Vector3(0f, 0f, -100f);
                massa.transform.localPosition = new Vector3(-1.552f, -0.366f, -100f);
                weights_0.transform.localPosition = new Vector3(-1.332f, -0.184f, -100f);
                weights_1.transform.localPosition = new Vector3(-1.449f, -0.184f, -100f);

                if (setactive == true)
                {
                    ustanovka.transform.localPosition = new Vector3(0f, 0f, 0f);
                    setactive = false;
                }
                else
                {
                    ustanovka.transform.localPosition = new Vector3(0f, 0f, -100f);
                    setactive = true;
                    ust_kol++;
                }
            }
            else if (ust_kol == 2)
            {
                weights_2.transform.localPosition = new Vector3(-1.565f, -0.184f, -100f);
                weights_3.transform.localPosition = new Vector3(-1.679f, -0.184f, -100f);

                ustanovka = GameObject.Find("Animation_04_M");
                if (setactive == true)
                {
                    ustanovka.transform.localPosition = new Vector3(0f, 0f, 0f);
                    setactive = false;
                }
                else
                {
                    ustanovka.transform.localPosition = new Vector3(0f, 0f, -100f);
                    setactive = true;
                    ust_kol++;
                }
            }
            else if (ust_kol == 3)
            {
                pruzina_small.transform.localPosition = new Vector3(0f, 0f, -100f);
                massa.transform.localPosition = new Vector3(-1.552f, -0.366f, -100f);

                ustanovka = GameObject.Find("Animation_01_3m");
                if (setactive == true)
                {
                    ustanovka.transform.localPosition = new Vector3(0.9733635f, 0.1440007f, 0.6995002f);
                    setactive = false;
                }
                else
                {
                    ustanovka.transform.localPosition = new Vector3(0f, 0f, -100f);
                    setactive = true;
                    ust_kol++;
                }
            }
            else if (ust_kol == 4)
            {
                pruzina_big.transform.localPosition = new Vector3(0f, 0f, -100f);
                pruzina_small.transform.localPosition = new Vector3(0f, 0f, -100f);
                massa.transform.localPosition = new Vector3(-1.552f, -0.366f, -100f);
                weights_0.transform.localPosition = new Vector3(-1.332f, -0.184f, -100f);
                weights_1.transform.localPosition = new Vector3(-1.449f, -0.184f, -100f);
                palka.transform.localPosition = new Vector3(-2.127f, -0.576f, -100f);
                begunok.transform.localPosition = new Vector3(-1.82f, -0.233f, -100f);

                ustanovka = GameObject.Find("Animation_04_01_5m");
                if (setactive == true)
                {
                    ustanovka.transform.localPosition = new Vector3(0.9733635f, 0.1440007f, 0.6995002f);
                    setactive = false;
                }
                else
                {
                    ustanovka.transform.localPosition = new Vector3(0f, 0f, -100f);
                    setactive = true;
                    ust_kol++;
                }
            }
            else
            {
                weights_2.transform.localPosition = new Vector3(-1.332f, -0.184f, -100f);
                weights_3.transform.localPosition = new Vector3(-1.449f, -0.184f, -100f);

                ustanovka = GameObject.Find("Animation_04_01_M");
                if (setactive == true)
                {
                    ustanovka.transform.localPosition = new Vector3(0.9733635f, 0.1440007f, 0.6995002f);
                    setactive = false;
                }
                else
                {
                    ustanovka.transform.localPosition = new Vector3(0f, 0f, -100f);
                    setactive = true;
                }
            }
        }
    }

    public void ReturnAllBig()
    {
        massa = GameObject.Find("massa");
        weights_0 = GameObject.Find("weights_0");
        weights_1 = GameObject.Find("weights_1");
        weights_2 = GameObject.Find("weights_2");
        weights_3 = GameObject.Find("weights_3");
        palka = GameObject.Find("palka");
        begunok = GameObject.Find("begunok");

        pruzina_big.transform.localRotation = Quaternion.Euler(90, 0, 0);
        massa.transform.localRotation = Quaternion.Euler(0, 0, 0);
        massa.transform.localPosition = new Vector3(-1.552f, -0.366f, -0.608f);
        palka.transform.localPosition = new Vector3(-2.127f, -0.576f, -0.765f);
        begunok.transform.localPosition = new Vector3(-1.82f, -0.233f, -0.7575f);
        weights_0.transform.localPosition = new Vector3(-1.332f, -0.184f, -0.764f);
        weights_1.transform.localPosition = new Vector3(-1.449f, -0.184f, -0.764f);
        weights_2.transform.localPosition = new Vector3(-1.565f, -0.184f, -0.764f);
        weights_3.transform.localPosition = new Vector3(-1.679f, -0.184f, -0.764f);
        metka_na_lineuke.transform.localPosition = new Vector3(-0.4241998f, -0.113f, -10f);

        if (big_name.Equals("pruzina06_1"))
        {
            pruzina_big.transform.localPosition = new Vector3(0f, 0f, 0f);
            pruzina_big.transform.localScale = new Vector3(1f, 1f, 0.8855621f);
        } else if (big_name.Equals("pruzina05_1"))
        {
            pruzina_big.transform.localPosition = new Vector3(0f, 0f, 0f);
            pruzina_big.transform.localScale = new Vector3(1f, 1f, 1.020686f);
        }
        else
        {
            pruzina_big.transform.localPosition = new Vector3(0f, 0.001092714f, 0f);
            pruzina_big.transform.localScale = new Vector3(1f, 1f, 1f);
        }
    }

    public void ReturnAllSmall()
    {
        pruzina_small.transform.localRotation = Quaternion.Euler(90, 0, 0);
        massa.transform.localPosition = new Vector3(-1.552f, -0.366f, -0.608f);
        weights_0.transform.localPosition = new Vector3(-1.332f, -0.184f, -0.764f);
        weights_1.transform.localPosition = new Vector3(-1.449f, -0.184f, -0.764f);
        weights_2.transform.localPosition = new Vector3(-1.565f, -0.184f, -0.764f);
        weights_3.transform.localPosition = new Vector3(-1.679f, -0.184f, -0.764f);
        metka_na_lineuke.transform.localPosition = new Vector3(-0.4241998f, -0.113f, -10f);

        if (small_name.Equals("pruzina03_1"))
        {
            pruzina_small.transform.localPosition = new Vector3(0f, 0f, 0f);
            pruzina_small.transform.localScale = new Vector3(1f, 1f, 0.8855619f);
        }
        else if (small_name.Equals("pruzina02_2"))
        {
            pruzina_small.transform.localPosition = new Vector3(0f, 0f, 0f);
            pruzina_small.transform.localScale = new Vector3(1f, 1f, 1.020686f);
        }
        else
        {
            pruzina_small.transform.localPosition = new Vector3(-1.201f, -0.02399981f, -0.7390002f);
            pruzina_small.transform.localScale = new Vector3(1f, 1f, 1f);
        }
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
