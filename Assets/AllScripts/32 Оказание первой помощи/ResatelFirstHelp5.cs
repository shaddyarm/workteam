using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class ResatelFirstHelp5 : MonoBehaviour 
{
	int step;
	
	int incorrect;
	int correct;
	
	
	public GameObject ResultCanvas;
	public Text Result;
	
	public GameObject QuestionCanvas;
	public Text Question;
	public Text Answer1;
	public Text Answer2;
	public Text Answer3;
	public Text Answer4;
	
	public Button b1,b2,b3,b4;
	

	float time;
	bool active;
	
	
	public GameObject Sphere1;
	public GameObject Sphere2;
	public GameObject Sphere3;
	public GameObject Sphere4;
	public GameObject Sphere5;
	public GameObject Sphere6;
	
	public GameObject Human1;
	public GameObject Human2;
	public GameObject Human3;
	
	
	

	// Use this for initialization
	void Start () 
	{
		incorrect=0;
		correct=0;
		
		
		QuestionCanvas.SetActive(false);
		
		
		step=0;
		active=false;
		time=0;
	}
	
	// Update is called once per frame
	void Update () 
	{
	
		
		
		
		
		
	}
	
	
	public void CORRECT_ANSWER()
	{
		if (step==106)
		{
			Human1.SetActive(false);
		}
		if (step==108)
		{
			Human2.SetActive(false);
			Human3.SetActive(false);
		}
		
		
		QuestionCanvas.SetActive(false);
		step++;
		active=false;
		
		correct++;
		
		
		if (correct>=10)
		{
			step=400;
			ResultCanvas.SetActive(true);
			string result = "Правильных ответов: " + correct.ToString() + ". Ошибок " + incorrect.ToString() + ".";
			Result.text=result;
		}
		
		if (step==106)
		{
			Human1.SetActive(false);
		}
			
			
	}
	public void INCORRECT_ANSWER()
	{
		incorrect++;
	}
	
	
	
	
	public void PRESS(string name)
	{
		if (active==true) return;
		
		if ((name=="1")&&(step==0))
		{
			step=100;
			active=true;
			time=0;
			
			QuestionCanvas.SetActive(true);
			Question.text = "Вы слышите крик пожар и начинаете чувствовать запах дыма. Что необходимо сделать?";
			Answer1.text = "Определите для себя, выходить или не выходить наружу. ";
			Answer2.text = "Встретьте пожарных и проведите их к месту пожара";
			Answer3.text = "Сообщите о пожаре в пожарную охрану по телефонам «112»";
			Answer4.text = "постоянно смачивайте дверь, пол";
			RandonButtons();
			
			Sphere1.SetActive(false);
			
			return;
		}
		
		if ((name=="2")&&(step==101))
		{
			step=102;
			active=true;
			time=0;
			
			QuestionCanvas.SetActive(true);
			Question.text = "Что делать?";
			Answer1.text = "Изучить схему эвакуации из помещения";
			Answer2.text = "Звонить 112";
			Answer3.text = "Оповестить всех громким криком пожар";
			Answer4.text = "ВЫпить как можно больше воды";
			RandonButtons();
			
			Sphere2.SetActive(false);
			return;
		}
		
		if ((name=="3")&&(step==103))
		{
			step=104;
			active=true;
			time=0;
			
			QuestionCanvas.SetActive(true);
			Question.text = "Что делать?";
			Answer1.text = "Нажать на кнопку ПОЖАР";
			Answer2.text = "Нажимать пожар не обязательно";
			Answer3.text = "Нажать и удерживать кнопку ПОЖАР";
			Answer4.text = "Выйти из помещения";
			RandonButtons();
			
			Sphere3.SetActive(false);
			return;
		}
		
		
		if ((name=="4")&&(step==105))
		{
			step=106;
			active=true;
			time=0;
			
			QuestionCanvas.SetActive(true);
			Question.text = "Вы встретили сотрудника. Человек не значет что делать, он в панике. Что делать?";
			Answer1.text = "Не поддавайтесь панике, успокоить человека, сказать идти за вами";
			Answer2.text = "Оставить человека на месте";
			Answer3.text = "Сообщить о человеке в 112, указав точное расположение и отличительные приметы";
			Answer4.text = "Спросить ФИО и должность. Приказать провести эвакуацию.";
			RandonButtons();
			
			Sphere4.SetActive(false);
			return;
		}
		
		
		if ((name=="5")&&(step==107))
		{
			step=108;
			active=true;
			time=0;
			
			QuestionCanvas.SetActive(true);
			Question.text = "Вы нашли постадавшего и человека, который пытается оказать помощь. Что делать?";
			Answer1.text = "Совместными усилиями поднять пострадавшего и нести его с эвакуационному выходу.";
			Answer2.text = "Оставить человека на месте";
			Answer3.text = "Сообщить о человеке в 112, указав точное расположение и отличительные приметы";
			Answer4.text = "Привести пострадавшего в сознание, убедиться что он может самостоятельно покинуть помещение";
			RandonButtons();
			
			Sphere5.SetActive(false);
			return;
		}
		
		if ((name=="6")&&(step==109))
		{
			step=110;
			active=true;
			time=0;
			
			QuestionCanvas.SetActive(true);
			Question.text = "Вы стоите перед дверью. Что делать?";
			Answer1.text = "Убедитесь, что за дверью нет пожара, приложив свою руку к двери или к металлической ручке.";
			Answer2.text = "Немедленно открыть дверь";
			Answer3.text = "Медленно открыть дверь и посмотреть что там";
			Answer4.text = "Позвонить в 112 и спросить, можно-ли выходить?";
			RandonButtons();
			
			
			return;
		}
		
		if ((name=="6")&&(step==111))
		{
			step=112;
			active=true;
			time=0;
			
			QuestionCanvas.SetActive(true);
			Question.text = "После выхода в безопасное место - что делать?";
			Answer1.text = "По пути за собой плотно закрывайте дверь";
			Answer2.text = "Вы забыли телефон, нужно вернуться";
			Answer3.text = "Наблюдать за распостранение пожара и сообщать информацию МЧС";
			Answer4.text = "Позвонить в 112 и рассказать о том, что Вы вышли";
			RandonButtons();
			
			
			return;
		}
		
		if ((name=="6")&&(step==113))
		{
			step=114;
			active=true;
			time=0;
			
			QuestionCanvas.SetActive(true);
			Question.text = "После выхода в безопасное место - что делать?";
			Answer1.text = "Сообщите о пожаре в пожарную охрану по телефонам «112», «01» (с сотового тел. 01*, 112)";
			Answer2.text = "Сообщите о пожаре в пожарную охрану по телефонам «113», «02» (с сотового тел. 02*, 113)";
			Answer3.text = "Сообщите о пожаре в пожарную охрану по телефонам «114», «03» (с сотового тел. 03*, 114)";
			Answer4.text = "Сообщите о пожаре в пожарную охрану по телефонам «115», «04» (с сотового тел. 04*, 115)";
			RandonButtons();
			
			
			return;
		}
		
		if ((name=="6")&&(step==115))
		{
			step=116;
			active=true;
			time=0;
			
			QuestionCanvas.SetActive(true);
			Question.text = "После выхода в безопасное место - что делать с пострадавшим с ожогами?";
			Answer1.text = "Охладить место ожога. 1 и 2 степень - охлаждать проточной водой 10 - 15 мин 3 и 4 - чистая влажная повязка, потом охладить с повязкой в стоячей воде";
			Answer2.text = "Охладить место ожога. 1 и 2 степень - охлаждать проточной водой 15 - 35 мин 3 и 4 - чистая влажная повязка, потом охладить с повязкой в стоячей воде";
			Answer3.text = "Охладить место ожога. 1 и 2 степень - охлаждать спиртом 10 - 15 мин 3 и 4 - чистая влажная повязка, потом охладить с повязкой в спирте";
			Answer4.text = "Охладить место ожога. Дать горячий чай";
			RandonButtons();
			
			
			return;
		}
		
		if ((name=="6")&&(step==117))
		{
			step=118;
			active=true;
			time=0;
			
			QuestionCanvas.SetActive(true);
			Question.text = "После выхода в безопасное место - что делать с пострадавшим с ожогами?";
			Answer1.text = "Вызвать скорую помощь, закрыть влажной повязкой, обеспечить покой и противошоковые меры";
			Answer2.text = "Закрыть влажной повязкой, обеспечить покой и противошоковые меры";
			Answer3.text = "Вызвать скорую помощь, закрыть влажной повязкой, обеспечить покой";
			Answer4.text = "Вызвать скорую помощь";
			RandonButtons();
			
			
			return;
		}
		
		
		
	}
	
	private void RandonButtons()
	{
		float[] YArray = new float[4];
		YArray [0] = 61f;
		YArray [1] = -1f;
		YArray [2] = -62f;
		YArray [3] = -124f;
		
		
		for (int XX=0; XX<=3; XX++)
		for (int YY=0; YY<=3; YY++)
		{
			if (Random.Range(0.0f,1.0f)>0.5f)
			{
				float temp = YArray[XX];
				YArray[XX]= YArray[YY];
				YArray[YY] = temp;
			}	
		}
		
		var r1 = b1.transform as RectTransform;
		var r2 = b2.transform as RectTransform;
		var r3 = b3.transform as RectTransform;
		var r4 = b4.transform as RectTransform;
		
		r1.localPosition = new Vector3(0, YArray[0], 0);
		r2.localPosition = new Vector3(0, YArray[1], 0);
		r3.localPosition = new Vector3(0, YArray[2], 0);
		r4.localPosition = new Vector3(0, YArray[3], 0);
		
		
		/*
		if (ЭКЗАМЕН==false)
		{
			b1.GetComponentInChildren<Text>().fontStyle = FontStyle.Bold;
		}
		else
		{
			b1.GetComponentInChildren<Text>().fontStyle = FontStyle.Normal;
		}
		*/
	}
	
	public void exit()
	{
		#if (UNITY_EDITOR)
			UnityEditor.EditorApplication.isPlaying = false;
		#elif (UNITY_STANDALONE) 
			Application.Quit();
		#elif (UNITY_WEBGL)
			Application.OpenURL("about:blank");
		#endif
	}
	
}
