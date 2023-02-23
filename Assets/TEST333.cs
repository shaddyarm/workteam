using UnityEngine;



public partial class TEST333 : MonoBehaviour
{
	void Start()
	{
		CalcPoteri();
		
	}
	
	void CalcPoteri ()
	{
		
		float D = 0.1f;
		float L=1f;
		float dH=0;
		
		float Qwater = 1000.0f;
		float g = 9.80665f;
		float tw = 20.0f; //t воды 
		float E = 0.00025f;
		
		for (float Q=0.000001f; Q<1f; Q+=0.01f)
		{
		
		
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
			
			Debug.Log ("1=" + y);
		}
		else if (( 2320 <= Re) && ( Re <= 100000))
		{
			//Зона гладкостенного сопротивления ф. Блазиуса
			y = 0.3164f / (Mathf.Sqrt(Mathf.Sqrt(Re)));
			Debug.Log ("2=" + y);
		}
		
		else if (( 4000 <= Re) && ( Re <= 3000000))
		{
			//Зона гладкостенного сопротивления ф. Конакова
			float z = 1.8f * Mathf.Log10 (Re) -1.5f;
			y = 1.0f / (z*z);
			Debug.Log ("3=" + y);
		}
		else if (( 20 * D / E <= Re) && ( Re <= 500 * D / E))
		{
			//Зона доквадратичного сопротивления ф. Альтшуля
			float z = E / D + 64 / Re;
			y = 0.11f * Mathf.Sqrt (Mathf.Sqrt(z));
			Debug.Log ("4=" + y);
		}
		else if ( Re >= 500 * D / E)
		{
			//Зона квадратичного сопротивления	 ф. Шифринсона
			y = 0.11f * Mathf.Sqrt( Mathf.Sqrt ((E/D)));
			Debug.Log ("5=" + y);
		}
		
		float h = y *   (L* (V*V)) / (D*2*g);
		float P = Qwater*g*h + Qwater*g*dH ;
		Debug.Log ("P=" + P/1000000f + "/" + V*V);
		Debug.Log ("Re=" + Re);
			
		}
		
	}
	}
	