using UnityEngine;
using System.Collections;
using UnityEngine.UI;
using System.Collections.Generic;

public class WellMathClass : MonoBehaviour
{
	bool isSuccesfulOrFalse = false;
	
	public GameObject ReportPanel;
	public Text ReportPanelText;
	
	public ReportToSite SCORM;
	
	public GNVP_Graph Graphic;
	public GameObject fire;
	
	public RectTransform Gas2D;
	
	public RectTransform R_nkt_2D;
	public RectTransform R_zatr_2D;

	float Dempher = 0.0001f;
	
	//Глубина скважины,м
	public float H_well = 1000f;
	
	//Эквивалентная диаметр НКТ,м
	public float D_nkt = 0.073f;
	
	//Эквивалентная диаметр,м
	public float D_well = 0.1f;
	
	//Давление на забое, Па
	public float P_down = 2000000f; //1MPa
	
	//плотности жидкостей на момент начала
	public float standart_Ro_water = 1000f; //кг/м3

	//Давление гидроразрыва
	public float P_false_1 = 40000000f;
	
	//Давление разрыва башмака
	public float P_false_2 = 30000000f;
	
	//кинематическая вязкость воды, м2/сек
	public float v_koeff = 0.00000116f; 
	//10 сек  до полного давления на закрытом дросселе
	public float DempherTime = 10f;


	public Text NaFakel_text;
	float M_NaFakel = 0;
	
	public Text P_to_plast_text;
	float P_to_plast = 0;
	
	public Text P_za_text;
	public Text P_down_text;
	public Text P_plast_text;
	public Text P_nkt_text;
	public Text P_zatrub_text;
	public GameObject FlagFromWell;
	public GameObject FlagToWell;
	
	
	public ManometrArrow Manometr_nkt;
	public ManometrArrow Manometr_zatrub;
	
	float minusMass=0;
	
	
	bool init = false;
	
	bool Density_Pressure_mode=true; //true - show density, false - show pressure
	
	
	class element
	{
		//масса жидкости
		public float m_liquid;
		//плотность жидкости
		public float Ro_liquid;
		//газ P1V1=P2V2
		public float V;
		public float P;
		//идентификатор жидкостей  0-скважинная 1-с ЦА
		public int id=0;
	}
	
	List <element> Elaments = new List<element>();
	
	
	public void Initialization()
	{
		//инициализация
		//S = 3.1415 * d^2 / 4
		float S_nkt = 3.1415f * D_nkt*D_nkt /4f;
		float S_zatrub = 3.1415f * D_well*D_well /4f;
		float S_result = S_zatrub - S_nkt;
			
		//Заполняем НКТ
		//Определяем объем НКТ
		{
			float V_nkt = S_nkt * H_well;
			//зная объем НКТ, считаем сколько жидкости необходимо добавить в первый элемент
			//m = V*Ro
			float m = standart_Ro_water * V_nkt;
			//добавляем элемент в скважину
			element temp = new element();
			temp.m_liquid = m;
			temp.Ro_liquid = standart_Ro_water;
			Elaments.Add (temp);
		}
		//добавляем газовую пачку (2й элемент)
		{
			//добавляем элемент в скважину
			element temp = new element();
			temp.m_liquid = 0;
			temp.Ro_liquid = 0;
			temp.V=1f;
			
			//P_down
			temp.P = P_down;
			
			Elaments.Add (temp);
		}
		//Заполняем кольцевое затрубное пространство (на 1 м3 меньше, т.к. 1 м3 газа)
		{
			float V_result = S_result * H_well - 1f; 
			//зная объем, считаем сколько жидкости необходимо добавить в 3 элемент
			//m = V*Ro
			float m = standart_Ro_water * V_result;
			//добавляем элемент в скважину
			element temp = new element();
			temp.m_liquid = m;
			temp.Ro_liquid = standart_Ro_water;
			Elaments.Add (temp);
		}
		init=true;
	}
	
	
    void Start()
	{
		//Initialization();
	}
	
	
	
	
	public void ToggleDensity_Pressure(bool Density_is_true)
	{
		Density_Pressure_mode = Density_is_true;
	}
	
	void Update()
    {
		if (init==false) return;
		
		//расчеты
		MM_step();
		//отрисовка
        Repaint();
		
		//бугущие линии
		UpdateLines();	
    }
	
	
	void MM_step()
	{
		
	}
	
	
	void Repaint()
	{
		float dT = Time.deltaTime/ 1f;
		
		float S_nkt = 3.1415f * D_nkt*D_nkt /4f;
		float S_zatrub = 3.1415f * D_well*D_well /4f;
		float S_result = S_zatrub - S_nkt;
		
		
		bool GasDetected=false;
		int indexGasElement=0;
		float QGH_nkt=0;
		float QGH_zatrub=0;
		
		
		float P1=0; //давление на манометре затрубном (устье)
		float P2=0; //давление на забой
		float P3=0; //давление на манометре НКТ (устье)
		
		float __L=0;
		float LenghtOfGas=0;
		float current_L3=0;
		//считаем QGH в НКТ и ЗАТРУБЕ
		{
			//флаг, что НКТ "прошли"
			__L=0;
			float __V=0;
			bool __nkt_end=false;
			for (int i=0; i < Elaments.Count; i++)
			{
				if ((Elaments[i].V!=0))
				{
					indexGasElement=i;
					GasDetected=true;
					LenghtOfGas = __L;
					__L+=Elaments[i].V/S_result; 
					__V+=Elaments[i].V;
					continue;
				}
				//ищем
				if (__nkt_end==false)
				{
					//зная объем НКТ, считаем 
					//m = V*Ro
					float currentV = Elaments[i].m_liquid / Elaments[i].Ro_liquid;
					float current_L = currentV/S_nkt;
					if ((__L + current_L) < H_well)
					{
						//Все идет по сечению НКТ
						__L+=current_L;
						__V+=currentV;
						QGH_nkt += Elaments[i].Ro_liquid * current_L * 9.81f;
					}
					else
					{
						__nkt_end=true;
						//часть пересчитываем уже как в затрубном кольце
						float _V_nkt = S_nkt * H_well;
						
						float prereborV = __V+currentV-_V_nkt;
						float normaV = currentV - prereborV;
						
						QGH_nkt+= Elaments[i].Ro_liquid * normaV /S_nkt  * 9.81f;
						
						//
						float addL = prereborV / S_result;
						QGH_zatrub+= Elaments[i].Ro_liquid * addL * 9.81f;
						
						__L=H_well + addL; 
						__V+=currentV;
					}
				}
				else
				{
					
					float currentV = Elaments[i].m_liquid / Elaments[i].Ro_liquid;
					//часть пересчитываем уже как в затрубном кольце
					float current_L = currentV/S_result;
					__L+=current_L;
					__V+=currentV;
					
					if (GasDetected==false)
					{
						QGH_zatrub+= Elaments[i].Ro_liquid * current_L * 9.81f;
					}
					
					if (Elaments[0].id !=0)
					{
						current_L3 = current_L;
					}
				}

				
			}
			//Прошли, показываем
		}
		
		
		
		//если есть газовая пачка, то считаем давление от нее вверх до устья и вниз до забоя
		//если газовой пачки нет, то считаем QGH нкт и затруба и принимаем забойное как наибольшее из 2х
		float Pgaz=0; //давление газовой шапки
		//float QGH1 =0;
		if (GasDetected==true)
		{
			//если есть жидкость над газовой пачкой то 
			// P1 = Pг.п. - QGH жидкости сверху
			//если  нет жидкости над газовой пачкой то 
			// P1 = Pг.п.
			if (Elaments.Count-1 > indexGasElement)
			{
				float ro = Elaments[indexGasElement+1].Ro_liquid;
				float g = 9.81f;
				float h = (Elaments[indexGasElement+1].m_liquid / Elaments[indexGasElement+1].Ro_liquid) / S_result;
				//
				float QGH1 = ro * g * h;
				Pgaz = Elaments[indexGasElement].P;
				P1 = Pgaz - QGH1;
				
				//Debug.Log ("Pgaz=" + Pgaz/1000000f + " QGH1=" + QGH1/1000000f + " =" + P1/1000000f);
			}
			else
			{
				Pgaz = Elaments[indexGasElement].P;
				P1 = Pgaz;
			}
			
			//считаем давление P2 (забой) = QGH_zatrub + P1
			P2 = QGH_zatrub + Pgaz;
					
			//считаем давление P3 (Нкт устье) = P2 - QGH_nkt
			P3 = P2 - QGH_nkt;
		}
		
		
		
		if (GasDetected==false)
		{
			//p2 = max (QGH_zatrub, QGH_nkt)
			//соотв 
			//P2 = Mathf.Max(QGH_zatrub, QGH_nkt);
			//P1 = P2 - QGH_zatrub;
			//P3 = P2 - QGH_nkt;
			
			P2=Mathf.Max(QGH_zatrub,P_down);
			P2=Mathf.Min(QGH_nkt,QGH_zatrub);
			
			P1=0;
			P3=0;
		}
		
		//Debug.Log ("QGH_nkt = " + QGH_nkt/1000000f + " QGH_zatrub = " + QGH_zatrub/1000000f+ " P1 = " + P1/1000000f );
		
		
		
		//Debug.Log ("P1 = " + P1/1000000f + " P2 = " + P2/1000000f + " P3 = " + P3/1000000f );
		
		//все, с шидростатикой определились, теперь добавляем гидродинамику
		float P1b = P1;
		float P2b = P2;
		float P3b = P3;
		
		if ((AZ_pumpOn==true)&&(AZ_pump_Q>0)&&(DrosselF_open>0))
		{
			//считаем шидравлические потери отдельно в НКТ, в затрубе и отдельно на дросселе
			//, тогда
			//P1' = P1 + Pдроссель
			//P2' = P2 + Pдроссель + Pвзатрубе
			//P3' = P3 + Pдроссель + Pвзатрубе + Pвнкт
			float Q_standart = AZ_pump_Q * 0.001f ; //литры/с в (м3/с)
			
			
			
			//скорости
			float V_drossel = Q_standart / (S_result*DrosselF_open);
			float V_nkt = Q_standart / (S_nkt);
			float V_zatrub = Q_standart / (S_result);
			//коэффициент для дросселя
			float Edrossel = (1f-DrosselF_open)*20f;
			//потери на дросселе
			float Hdross = Edrossel * (V_drossel*V_drossel) / (2f * 9.81f) ;
			
			
			Dempher+=dT/DempherTime;
			if (Dempher>1f) Dempher=1f;
			//Debug.Log ("Dempher = " + Dempher);
			
			float p_dross = Hdross * 9.81f * 1000f;
			if (p_dross>40000000f) p_dross=40000000f;
			
			p_dross*=Dempher;
			
			//потери на нкт и затрубе
			float Re=(V_nkt*D_nkt)/v_koeff;
			//ν=1,16*10-6=0,00000116. Взято из таблицы. Для воды при температуре 16°С.
			float Lambda = 64f/Re; //круглое сечение
			float LambdaTube = 96f/Re; //кольцевое сечение
			float Hnkt =   Lambda * H_well / D_nkt * (V_nkt*V_nkt) / (2f * 9.81f);
			float Hzatr =  LambdaTube * H_well / (D_well-D_nkt) * (V_nkt*V_nkt) / (2f * 9.81f);
			
			//Debug.Log(CalcPoteri(Q_standart,(D_well-D_nkt),H_well)/1000000f);
			
			
			P1b+= p_dross;
			P2b+= p_dross + Hzatr* 9.81f * 1000f;
			P3b+= p_dross + Hzatr* 9.81f * 1000f + Hnkt* 9.81f * 1000f;
			
			
			
			
			
			//Debug.Log ("P1' = " + P1b/1000000f + " P2' = " + P2b/1000000f + " P3' = " + P3b/1000000f );
		}
		
		
		//нагнетание
		float addMass=0;
		minusMass=0;
		if ((AZ_pump_Q>0)&&(AZ_pumpOn==true))
		{
			if (Elaments[0].id ==0)
			{
				//добавляем новый элемент
				element temp = new element();
				temp.id=1;
				temp.m_liquid = 0;
				if (NumAZinUse==1)
				{
					temp.Ro_liquid = Az_RO_1;
				}
				else
				{
					temp.Ro_liquid = Az_RO_2;
				}
				temp.V = 0;
				temp.P = 0;
				Elaments.Insert(0, temp);
				return;
			}
			else
			{
				/*
				if (Dempher>1f)
				{
					Dempher -=  dT;
				}
				else
				{
					Dempher=1f;
				}
				*/
				
				float realQ = AZ_pump_Q;
				if (P3b>40000000f) 
				{
					realQ=0;
				}
				
				if (NumAZinUse==1)
				{
					addMass= realQ * 0.001f *Az_RO_1* dT ; //0.001 - литры в м3, / Dempher
				}
				else
				{
					addMass= realQ * 0.001f *Az_RO_2* dT ; //0.001 - литры в м3, / Dempher
				}
				
				/*
				if (GasDetected)
				{
					float Ro=0;
					if (NumAZinUse==1)
					{
						Ro = Az_RO_1;
						
					}
					else
					{
						Ro = Az_RO_2;
					}
					float V1 = Elaments[indexGasElement].V;
					Elaments[indexGasElement].V -= addMass/Ro;
					float V2 = Elaments[indexGasElement].V;
					float newP = Elaments[indexGasElement].P * (V1) / V2;
					Elaments[indexGasElement].P  = newP;
				}
				*/
				Elaments[0].m_liquid += addMass;
				minusMass=addMass;
			}
		}
		
		//сжатие "несжимаемой" жидкости
		{
			/*
			float prereborP = 0;
			if (__L > 2f * H_well)
			{
				float prerebor = __L-2f * H_well;
				prereborP = prerebor*10000000f;
			}
			P1b+= prereborP;
			P2b+= prereborP;
			P3b+= prereborP;
			*/
		}
		
		
		//процесс всплытия газа)
		if (GasDetected)
		{
			//если над газом (после газа) чтото есть, перебрасываем вниз
			if (Elaments.Count-1 > indexGasElement)
			{
				float needMass = 10f * dT; //10
				if (Elaments[indexGasElement+1].m_liquid>needMass)
				{
					Elaments[indexGasElement+1].m_liquid-=needMass;
					Elaments[indexGasElement-1].m_liquid+=needMass;
				}
				else
				{
					Elaments[indexGasElement-1].m_liquid+=Elaments[indexGasElement+1].m_liquid;
					Elaments[indexGasElement+1].m_liquid=0;
					Elaments.RemoveAt(indexGasElement+1);
				}
			}
		}
		
		//Дроссель
		if (DrosselF_open>0)
		{
			//
		
			
			NaFakel_text.text = M_NaFakel.ToString("00.00");
			
			float perepad = P2b - P_down;
			float DrosselToFakelM = 0;
			
			if (perepad>0)
			{
				//забираю при таком давлении... х кг воды или газа
				//DrosselToFakelM = DrosselF_open*perepad/1000000f * dT * 1000f; 
				DrosselToFakelM = DrosselF_open*40f * dT + minusMass; 
			}
					
			//если забираю воду значит увеличиваю объем гащовой шапки если она есть
			//если забираю газ, то соответственно сбрасываю давление, могу убить газовую шапку
			if (Elaments[Elaments.Count-1].V!=0)
			{
				
				
				DrosselToFakelM = DrosselF_open * dT *1000000f; // / 20
				//газовая шапка
				if (Elaments[Elaments.Count-1].V > 0.4f)
				{
					//предыдущий объем
					float Vold = Elaments[Elaments.Count-1].V;
					//float P1 = Elaments[Elaments.Count-1].P;
					
					//новый объем газовой пачки равен Vскважины - Vжидкости
					float LL = H_well*2f - LenghtOfGas;
					float newV = LL * S_result;
					if (newV<0) newV=0;
					
					Debug.Log ("LenghtOfGas = " + LenghtOfGas + " newV = " + newV );
					
					Elaments[Elaments.Count-1].V = newV;
					//новое давление ...
					float newP = Elaments[Elaments.Count-1].P * Vold / newV;
					if (newP<0) newP=0;
					
					Elaments[Elaments.Count-1].P = newP;
					Elaments[Elaments.Count-1].P-=DrosselToFakelM;
					if (Elaments[Elaments.Count-1].P<=0) 
					{
						Elaments[Elaments.Count-1].P=0;
					}
					else
					{
						M_NaFakel += DrosselToFakelM;
						fire.SetActive(true);
					}
					
					
					
					//float V2 = Elaments[Elaments.Count-1].V;
					//Elaments[Elaments.Count-1].P * (V1*(1f-0.0001f*DrosselF_open)) / V2;
					//Elaments[Elaments.Count-1].P  = newP;
				}
				else
				{
					M_NaFakel += Elaments[Elaments.Count-1].V;
					Elaments[Elaments.Count-1].V=0;
					Elaments.RemoveAt(Elaments.Count-1);
					Debug.Log("Gas goodbuy!!!");
					fire.SetActive(false);
					//гудбай газ!!!
					ReportPanel.SetActive(true);
					ReportPanelText.text = "Газовая пачка удалена.";
					return;
				}
			}
			else
			{
				if (__L>=H_well*2f*0.9999f) //95
				{
					//вода
					if (Elaments[Elaments.Count-1].m_liquid > DrosselToFakelM)
					{
						Elaments[Elaments.Count-1].m_liquid-=DrosselToFakelM;
						M_NaFakel += DrosselToFakelM;
						
						if (GasDetected)
						{
							float V1 = Elaments[indexGasElement].V;
							Elaments[indexGasElement].V += DrosselToFakelM / Elaments[Elaments.Count-1].Ro_liquid;
							float V2 = Elaments[indexGasElement].V;
							float newP = Elaments[indexGasElement].P * (V1*(1f-0.001f*DrosselF_open)) / V2;
							Elaments[indexGasElement].P  = newP;
							
							
							//Debug.Log ("P=" + Elaments[indexGasElement].P + " V=" + Elaments[indexGasElement].V);
						}
					}
					else
					{
						if (GasDetected)
						{
							float V1 = Elaments[indexGasElement].V;
							Elaments[indexGasElement].V += Elaments[Elaments.Count-1].m_liquid / Elaments[Elaments.Count-1].Ro_liquid;
							float V2 = Elaments[indexGasElement].V;
							float newP = Elaments[indexGasElement].P * (V1*(1f-0.001f*DrosselF_open)) / V2;
							Elaments[indexGasElement].P  = newP;
						}
						
						M_NaFakel += Elaments[Elaments.Count-1].m_liquid;
						Elaments[Elaments.Count-1].m_liquid=0;
						Elaments.RemoveAt(Elaments.Count-1);
						Debug.Log("Water goodbuy!!!");
						//гудбай вода
						return;
					}
				}
				
			}
			
			
		}
		
		//вывод
		{
			P_down_text.text = "P (факт) = " + (P2b/1000000f).ToString("00.00") + " МПа";
			P_plast_text.text = "P (пласт) = " + (P_down/1000000f).ToString("00.00") + " МПа";
			
			P_nkt_text.text = "P (нкт) = " + (P3b/1000000f).ToString("00.00") + " МПа";
			P_zatrub_text.text = "P (затруб) = " + (P1b/1000000f).ToString("00.00") + " МПа";
			
			P_za_text.text = (P3b/1000000f).ToString("00.00");
			
			Manometr_nkt.SetValue(P3b);
			Manometr_zatrub.SetValue(P1b);
			
			//вывод на график
			//float Pзатруб, float Pнкт, float Pца, float Pmin, float Pmax, float DrosselOpen, float Pпласта, float Pfact
			Graphic.AddPoint(P1b,P3b,P3b,P_down,P_false_2,DrosselF_open*P_false_2,P_down,P2b);
			//Debug.Log("P_false_2=" + P_false_2);
			
			
			
			if (GasDetected)
			{
				float GazVolumePosition = (LenghtOfGas-H_well) *(984.7f-25.98434f)/H_well + 25.98434f;
				Gas2D.anchoredPosition  = new Vector3(0, GazVolumePosition, 0);
				Gas2D.gameObject.SetActive(true);
				//длинна пачки газа
				float l = Elaments[indexGasElement].V / S_result  /8f;
				Gas2D.sizeDelta = new Vector2(38.73584f, (l) *(1006.3f-18.55739f)/H_well + 18.55739f); 
			}
			else
			{
				Gas2D.gameObject.SetActive(false);
			}
			
			if (P2b<(P_down*0.89f))
			{
				FlagFromWell.SetActive(false);
				FlagToWell.SetActive(true);
			}
			else
			{
				FlagFromWell.SetActive(true);
				FlagToWell.SetActive(false);
			}
				
				
		}
		
		//итоги
		{
			//порвали пласт
			if ((isSuccesfulOrFalse==false)&&(P2b>P_false_2))
			{
				isSuccesfulOrFalse=true;
				ReportPanel.SetActive(true);
				ReportPanelText.text = "Превышено давление гидроразрыва. Задание провалено.";
				SCORM.score = "0";
				SCORM.status = "failed";
				SCORM.report = "Превышено давление гидроразрыва. Задание провалено.";
				SCORM.StartCheck();
			}
			
			//Задание выполнено
			
			if ((isSuccesfulOrFalse==false)&&(GasDetected==false)&&(current_L3>=H_well*2f*0.85f))
			{
				isSuccesfulOrFalse=true;
				ReportPanel.SetActive(true);
				ReportPanelText.text = "Задание выполнено.";
				SCORM.score = "100";
				SCORM.status = "completed";
				SCORM.report = "Задание выполнено.";
				SCORM.StartCheck();
			}
			
			//Задание провалено
			
			if ((isSuccesfulOrFalse==false)&&(P2b<P_down*0.89f))
			{
				isSuccesfulOrFalse=true;
				ReportPanel.SetActive(true);
				ReportPanelText.text = "Выход второй пачки. Задание провалено.";
				SCORM.score = "0";
				SCORM.status = "failed";
				SCORM.report = "Задание провалено.";
				SCORM.StartCheck();
			}
			
		}
		
		
		

				//рисуем раствор глушения
				//public RectTransform R_nkt_2D;
				//public RectTransform R_zatr_2D;
				
				if (Elaments[0].id !=0)
				{
					float currentV = Elaments[0].m_liquid / Elaments[0].Ro_liquid;
					float current_L = currentV/S_nkt;
					
					current_L3=current_L;
					
					if (current_L < H_well)
					{
						//нкт
						R_nkt_2D.gameObject.SetActive(true);
						R_nkt_2D.sizeDelta = new Vector2(12.01886f, -1000f + (current_L) *(1000f-25.98392f)/H_well + 25.98392f); 
					}
					else
					{
						R_zatr_2D.gameObject.SetActive(true);
						R_nkt_2D.sizeDelta = new Vector2(12.01886f, -25.98392f); 
						
						//остаток в затрубном
						float ostatokV = (current_L - H_well) * S_nkt;
						float current_L2 = ostatokV / S_result;
						R_zatr_2D.sizeDelta = new Vector2(34.87249f, (current_L2) *(980.9763f-26.02084f)/H_well + 26.02084f); 
						
					}
				}
				else
				{
					R_nkt_2D.gameObject.SetActive(false);
					R_zatr_2D.gameObject.SetActive(false);
				}
		
	}
	
	
	//ЦА
	public void SetAzPumpQ(float value)
	{
		AZ_pump_Q = value;
		AZ_AZ_pump_Q_text.text = value.ToString();
	}
	public void SetAzPumpOnOff(bool value)
	{
		AZ_pumpOn = value;
		if (AZ_pumpOn==true)
		{
			AZ_pump_ON_OFF_text.text = "ВКЛЮЧЕН";
		}
		else
		{
			AZ_pump_ON_OFF_text.text = "ВЫКЛЮЧЕН";
		}
	}
	//Текущая подача (л/с):
	float AZ_pump_Q = 0;
	//Текущее давление по манометру на ЦА (МПа):
	float AZ_pump_P = 0;
	//
	bool AZ_pumpOn=false;
	//
	public Text AZ_AZ_pump_Q_text;
	public Text AZ_pump_P_text;
	public Text AZ_pump_ON_OFF_text;
	//ЦА
	
	
	//АЦ
	//какой АЦ подключен к ЦА
	int NumAZinUse = 1;
	//плотности в АЦ №1 и №2
	float Az_RO_1=1000f;
	float Az_RO_2=1000f;
	
	public void SetAz1(bool value)
	{
		if (value==true)
		{
			NumAZinUse = 1;
		}
		else
		{
			NumAZinUse = 2;
		}
	}
	
	public void SetAzRo1str(string value)
	{
		Az_RO_1 = (float)int.Parse(value);
		//Debug.Log ("SetAzRo1str=" + Az_RO_1);
	}
	public void SetAzRo2str(string value)
	{
		Az_RO_2 = (float)int.Parse(value);
	}
	
	public InputField AZ_RO_1_text;
	public InputField AZ_RO_2_text;

	//АЦ
	
	//Манифольд Факел
	public Text Valve1text;
	float DrosselF_open=0f;
	public void Valve1_angle(float value)
	{
		Valve1text.text = ((int)(value/3600f*100f)).ToString() + "%";
		DrosselF_open=value / 3600f;
	}
	//Манифольд Факел
	
	
	//анимация бегущих линий 
	public GameObject LinesG;
	public GameObject LinesF;
	void UpdateLines()
	{
		//1.Если дроссель не закрыт и есть подача насоса - показываем
		if ((AZ_pump_Q>0)&&(AZ_pumpOn==true))
		{
			LinesG.SetActive(true);
		}
		else
		{
			LinesG.SetActive(false);
		}
		
		//1.Если дроссель не закрыт  - показываем
		if (DrosselF_open>0)
		{
			LinesF.SetActive(true);
		}
		else
		{
			LinesF.SetActive(false);
		}
	}
	//анимация бегущих линий 
	

	float CalcPoteri (float Q, float D, float L, float dH=0)
		{
			float g = 9.80665f;
			float Qwater = 1000.0f;
			//double u =0.03f;
			
	
			if (Q==0) return 0;
			
			if (D==0) D=0.00000001f;
					
			float tw = 20.0f; //t воды 
			float E = 0.00025f;
			
			
			//1 Определить среднюю скорость движения воды в трубопроводе
			//
			float V= 4.0f * Q / (3.1415f * D*D);
			//найти по формуле Пуазейля кинематическую вязкость воды:
			float v = 0.0178f /(1.0f+0.6337f * tw + 0.000221f *tw*tw);
			//По известным значениям  и  определить число Рейнольдса
			float Re = V*D/v;
			
			////Δэ=0,25мм=0,00025м. Взято из таблицы, для новой чугунной трубы.
			//float E = 0.00025f;
			
			//безразмерный коэффициент гидравлического сопротивления трению
			float y=0;
			if (Re < 2320)
			{
				//Ламинарный ф. Пуазейля
				y = 64.0f / Re;
			}
			else if (( 2320 <= Re) && ( Re <= 100000))
			{
				//Зона гладкостенного сопротивления ф. Блазиуса
				y = 0.3164f / (Mathf.Sqrt(Mathf.Sqrt(Re)));
			}
			else if (( 4000 <= Re) && ( Re <= 3000000))
			{
				//Зона гладкостенного сопротивления ф. Конакова
				float z = 1.8f * Mathf.Log10 (Re) -1.5f;
				y = 1.0f / (z*z);
			}
			else if (( 20 * D / E <= Re) && ( Re <= 500 * D / E))
			{
				//Зона доквадратичного сопротивления ф. Альтшуля
				float z = E / D + 64 / Re;
				y = 0.11f * Mathf.Sqrt (Mathf.Sqrt(z));
			}
			else if ( Re >= 500 * D / E)
			{
				//Зона квадратичного сопротивления	 ф. Шифринсона
				y = 0.11f * Mathf.Sqrt( Mathf.Sqrt ((E/D)));
			}
			
			Debug.Log ("y = " + y);
			Debug.Log ("V = " + V);
			
			//y=0.03f;
			//Далее завершаем формулой:
			float h = y *   (L* (V*V)) / (D*2*g);
			float P = Qwater*g*h + Qwater*g*dH ;
				
			return P;	

			
		}
	
}