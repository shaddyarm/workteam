using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;
using UnityEngine.UI;

public class labDM_1 : MonoBehaviour
{

    public GameObject Indicator_On;
    public ValveClass1 SpeedValue;
    public ValveClass1 TormozValue;
    public TextMeshPro rmp_text;

    public Text rmp_motor_text;
    public Text P_motor_text;
    public Text moment_motor_text;
    public Text rmp_tormoz_text;
    public Text moment_tormoz_text;
    public Text kpd_text;

    public AudioSource Rotor_sound;

    public GameObject Rotor_motor;
    public GameObject Rotor_mufta_motor;
    public GameObject Rotor_Dynonometr_1;
    public GameObject Rotor_reductor_1;
    public GameObject Rotor_reductor_2;
    public GameObject Rotor_reductor_Mid;
    public GameObject Rotor_Dynonometr_2;
    public GameObject Rotor_mufta_tormoz;
    public GameObject Rotor_tormoz;

    float angle1 = 0;
    float angle2 = 0;
    float angleMid=0;

    float rpm = 500f; //[500..1500];
    
    private bool isOn=false;
    private bool isStart = false;


    float ttt = 0;

    public labDM_parametr param1;
    public labDM_parametr param2;
    public labDM_parametr param3;
    public labDM_parametr param4;
    public labDM_parametr param5;
    public labDM_parametr param6;
    public labDM_parametr param7;
    public labDM_parametr param8;
    public labDM_parametr param9;
    public labDM_parametr param10;

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


    //Старт-стоп
    public void Start_button()
    {
        if (isOn == false) return;
        if (isStart == false)
        {
            isStart = true;
            Rotor_sound.Play();
            Indicator_On.SetActive(true);
        }

       
    }
    public void Stop_button()
    {
        if (isOn == false) return;
        if (isStart == true)
        {
            isStart = false;
            Rotor_sound.Stop();
            Indicator_On.SetActive(false);
        }
    }


    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        float freq = (SpeedValue.currentShift / SpeedValue.maxA) * 49f + 1f;
        rpm = (freq) / 50f * 1500f; //1500 оборотов при частоте 50
        rmp_text.text = freq.ToString("N1");
        Rotor_sound.pitch = (freq+25f) / 50f;

        

        if (isStart == true)
        {

            //-------------------------------------------dm1---------------------------------
            float CWNumEdit1 = (float)param1.value;   //число зубьев колеса 2й ступени
            float CWNumEdit2 = (float)param2.value;   //число зубьев шестерни 2й ступени
            float CWNumEdit3 = (float)param3.value;   //число зубьев шестерни 1й ступени
            float CWNumEdit4 = (float)param4.value;   //число зубьев колеса 1й ступени	
            float CWNumEdit5 = (float)param5.value;    //степень точности
            float CWNumEdit6 = (float)param6.value;   //межосевое расстояние
            float CWNumEdit7 = (float)param7.value;   //вязкость
            float CWNumEdit8 = (float)param8.value;   //ширина 1-го
            float CWNumEdit9 = (float)param9.value;   //ширина 2-го
            float CWNumEdit10 = (float)param10.value;   //comp.params[10] = { min = 1,max = 3,value = 1, text ={ "Шариковые","Цилиндрические","Конические"} }


            float KPD;
            //? об/мин * 1000
            float w = rpm /1000f;
            float reductor_k = CWNumEdit3 / CWNumEdit4 * CWNumEdit2 / CWNumEdit1;
            float reductor_k2 = CWNumEdit3 / CWNumEdit4;
            float w_t = w * reductor_k;
            float rpm_2 = w_t * 1000f;
            float rpm_mid = w * reductor_k2 * 1000f;

            float Modul = (2f * CWNumEdit6) / (CWNumEdit3 + CWNumEdit4);
            float Vokr = Modul * CWNumEdit3 * w * 1000f * 0.0000537f;

            float Ftr = 1.25f * (0.075f - 0.027f * Mathf.Log(1f + Vokr * 0.7f));
            float KPDzub = 1f - 2.3f * Ftr * (1f / CWNumEdit3 + 1f / CWNumEdit4);
            float Modul_ = (2f * CWNumEdit6) / (CWNumEdit2 + CWNumEdit1);
            float Vokr_ = Modul_ * CWNumEdit2 * w * 1000f * 0.0000547f * (CWNumEdit3 / CWNumEdit4);
            float Ftr_ = 1.25f * (0.075f - 0.027f * Mathf.Log(1f + Vokr_ * 0.7f));
            float KPDzub_ = 1f - 2.3f * Ftr * (1f / CWNumEdit2 + 1f / CWNumEdit1);
            float K_zub = 1f - (1f - KPDzub * KPDzub_) * (1f + (CWNumEdit5 - 7.2f) / 3f);
            float f_ = 0.002f * CWNumEdit10;
            float f__ = f_ * (1f + (CWNumEdit5 - 6f) / 3f);
            float KPD_podsh = (1f - 1.7f * f__);
            float i = w / w_t;
            float Pi_30 = Mathf.PI / 30f; //!
            float Pgidr = (0.04f * Modul * CWNumEdit3 * CWNumEdit8) * (w * Pi_30 * 1000f) * Mathf.Sqrt((2f * Vokr * CWNumEdit7 * 0.000001f) / (CWNumEdit3 + CWNumEdit4));
            float Pgidr_ = (0.04f * Modul_ * CWNumEdit2 * CWNumEdit9) * (w * Pi_30 * 1000f * CWNumEdit3 / CWNumEdit4) * Mathf.Sqrt((2f * Vokr_ * CWNumEdit7 * 0.000001f) / (CWNumEdit2 + CWNumEdit1));

            //!!!!!!!!!!!!!!!! ТОРМОЗ!!!!!
            float valueOfTormoz = (TormozValue.currentShift / TormozValue.maxA) * 40f; //40 Н*м
            float P2 = valueOfTormoz * Mathf.PI / 30000f * w_t * 1000f; //ruchka:get() * Mathf.PI / 30000 * w_t * 1000
            float P1 = (Pgidr + Pgidr_) / 1000f + P2 / (K_zub * KPD_podsh * KPD_podsh * KPD_podsh);

            if (P1 == 0)
            {
                KPD = 0;
            }
            else
            {
                KPD = P2 / P1;
            }
            float poteri = 1f - KPD;
            float poteri_ = poteri * (0.98f + Random.Range(0, 1f) * 0.04f);
            float poteri_3 = (P1 - P2) * 1000f * (0.99f + Random.Range(0, 1f) * 0.02f) + (2f - Random.Range(0, 1f) * 4f);
            KPD = 1f - poteri_;
            float gKPD = KPD * 100f;

            if (gKPD < 0) gKPD = 0;

            //--print("KPD = "..KPD)
            //!!!!!!!!!!!!!!!! ТОРМОЗ!!!!!
            float s_p = valueOfTormoz * w_t * Mathf.PI / 30f * KPD ; //ruchka:get() * w_t * math.pi / 30 * KPD  
            //strelka_p: set(s_p)
            float s_J;
            if (w == 0)
            {
                s_J = 0;
            }
            else
            {
                s_J = 100f * s_p / w * Pi_30;
            }
            //strelka_J:set(s_J)

            //обновляем 2 раза в секунду....
            ttt += Time.deltaTime;
            if (ttt > 0.5f)
            {
                rmp_motor_text.text = (w * 1000f).ToString("N0");
                P_motor_text.text = s_p.ToString("N3");
                moment_motor_text.text = s_J.ToString("N3");
                rmp_tormoz_text.text = (w_t * 1000f).ToString("N0");
                moment_tormoz_text.text = valueOfTormoz.ToString("N3");
                kpd_text.text = gKPD.ToString("N3");
                ttt = 0;
            }


            //-------------------------------------------dm1---------------------------------
            //

            angle1 += rpm / 60f * Time.deltaTime *360f; 
            if (angle1 > 36000f) angle1 -= 36000f;
            if (angle1 < 36000f) angle1 += 36000f;

            angle2 += rpm_2 / 60f * Time.deltaTime * 360f;
            if (angle2 > 36000f) angle2 -= 36000f;
            if (angle2 < 36000f) angle2 += 36000f;

            angleMid += rpm_mid / 60f * Time.deltaTime * 360f;
            if (angleMid > 36000f) angleMid -= 36000f;
            if (angleMid < 36000f) angleMid += 36000f;

            
            Rotor_motor.transform.transform.localRotation = Quaternion.Euler(0, 0, -angle1);
            Rotor_mufta_motor.transform.localRotation = Quaternion.Euler(angle1, 0, 0);
            Rotor_Dynonometr_1.transform.localRotation = Quaternion.Euler(0, -angle1, 0);
            Rotor_reductor_1.transform.localRotation = Quaternion.Euler(0, 0, -angle1);
            Rotor_reductor_2.transform.localRotation = Quaternion.Euler(0, 0, -angle2);
            Rotor_reductor_Mid.transform.localRotation = Quaternion.Euler(-angleMid, 0, 0 );
            Rotor_Dynonometr_2.transform.localRotation = Quaternion.Euler(0, angle2, 0);
            Rotor_mufta_tormoz.transform.localRotation = Quaternion.Euler(angle2, 0, 0);
            Rotor_tormoz.transform.localRotation = Quaternion.Euler(0, 0, angle2);
        }
        else
        {
            rmp_motor_text.text = "0";
            P_motor_text.text = "0"; 
            moment_motor_text.text = "0";
            rmp_tormoz_text.text = "0";
            moment_tormoz_text.text = "0";
            kpd_text.text =  "0";
        }

        
    }
}



