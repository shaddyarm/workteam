using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class ResatelFirstHelp4 : MonoBehaviour 
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
	
	public Animator A;
	public Animator B;
	float time;
	bool active;
	public Button b1,b2,b3,b4;
	
	
	public GameObject Sphere1;
	
	

	// Use this for initialization
	void Start () 
	{
		incorrect=0;
		correct=0;
		
		
		QuestionCanvas.SetActive(false);
		A.speed=0;
		B.speed=0;
		A.ForceStateNormalizedTime(0);
		B.ForceStateNormalizedTime(0);
		
		step=0;
		active=false;
		time=0;
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
	
	// Update is called once per frame
	void Update () 
	{
	
		if (step==109)
		{
			time += Time.deltaTime;
			float frame = time/2f;
			if (frame<1f)
			{	
				A.ForceStateNormalizedTime(frame);
			}
			if (frame>=1f)
			{
				A.ForceStateNormalizedTime(0);
				active=false;
				step++;
			}
		}
		
		if (step==114)
		{
			time += Time.deltaTime;
			float frame = time/5f;
			float zzz = frame;
			for (int i=0;i<10;i++)
			{
				if (zzz>1f) zzz = zzz-1f;
			}
			
			if (frame<10f)
			{	
				B.ForceStateNormalizedTime(zzz);
			}
			if (frame>=10f)
			{
				B.ForceStateNormalizedTime(0);
				active=false;
				step++;
			}
		}
		
		
		
		
	}
	
	
	public void CORRECT_ANSWER()
	{
		QuestionCanvas.SetActive(false);
		step++;
		active=false;
		
		correct++;
		
		
		if (correct>=8)
		{
			step=400;
			ResultCanvas.SetActive(true);
			string result = "Правильных ответов: " + correct.ToString() + ". Ошибок " + incorrect.ToString() + ".";
			Result.text=result;
		}
			
			
	}
	public void INCORRECT_ANSWER()
	{
		incorrect++;
	}
	
	
	
	
	public void PRESS(string name)
	{
		if (active==true) return;
		
		if ((name=="0")&&(step==0))
		{
			step=100;
			active=true;
			time=0;
			
			QuestionCanvas.SetActive(true);
			Question.text = "Пострадавший упал на нащих глазах. Что необходимо сделать?";
			Answer1.text = "Задать пострадавшему вопрос. Понять его состояние";
			Answer2.text = "Вызвать скорую помощь и уйти";
			Answer3.text = "Перенести человека в помещение";
			Answer4.text = "Привести человека в сознание пощечиной";
			RandonButtons();
			return;
		}
		
		if ((name=="0")&&(step==101))
		{
			step=102;
			active=true;
			time=0;
			
			QuestionCanvas.SetActive(true);
			Question.text = "Пострадавний не отвечает. Не двигается. Бледнеет. Что делать?";
			Answer1.text = "Посмотреть повреждения, дыхание, пульс на сонной артерии";
			Answer2.text = "Посмотреть внешние повреждения, пульс на руке";
			Answer3.text = "Посмотреть внешние повреждения";
			Answer4.text = "Посмотреть внешние повреждения, пульс на ноге";
			RandonButtons();
			return;
		}
		
		if ((name=="0")&&(step==103))
		{
			step=104;
			active=true;
			time=0;
			
			QuestionCanvas.SetActive(true);
			Question.text = "Видимых повреждений нет, дыхания нет, пульса нет. Что делать?";
			Answer1.text = "Выполнить диагностику обструкции дыхательных путей ";
			Answer2.text = "Перевернуть на живот";
			Answer3.text = "Перевернуть на бок";
			Answer4.text = "Попытаться поставить на ноги";
			RandonButtons();
			return;
		}
		
		if ((name=="0")&&(step==105))
		{
			step=106;
			active=true;
			time=0;
			
			QuestionCanvas.SetActive(true);
			Question.text = "Дыхательные пути свободны. Что делать?";
			Answer1.text = "Приступить к сердечно-лёгочной реанимации";
			Answer2.text = "Проверить давление";
			Answer3.text = "Проверить температуру";
			Answer4.text = "Подуть на лицо";
			RandonButtons();
			return;
		}
		
		if ((name=="0")&&(step==107))
		{
			step=108;
			active=true;
			time=0;
			
			QuestionCanvas.SetActive(true);
			Question.text = "Что делать?";
			Answer1.text = "Выполнить прекардиальный удар.  Указательный палец и средний палец необходимо положить на мечевидный отросток. Затем ребром сжатой в кулак ладони ударить по грудине выше пальцев, при этом локоть наносящей удар руки должен быть направлен вдоль туловища пострадавшего.";
			Answer2.text = "Выполнить прекардиальный удар.  Ударить по животу кулаком.";
			Answer3.text = "Выполнить прекардиальный удар.  Ударить по грудной кледке кулаком изо всей силы.";
			Answer4.text = "Выполнить прекардиальный удар.  Ладонь необходимо положить на мечевидный отросток. Затем ребром сжатой в кулак ладони ударить по грудине выше пальцев, при этом локоть наносящей удар руки должен быть направлен поперек туловища пострадавшего.";
			RandonButtons();
			return;
		}
		
	
		if ((name=="0")&&(step==110))
		{
			step=111;
			active=true;
			time=0;
			
			QuestionCanvas.SetActive(true);
			Question.text = "Что делать?";
			Answer1.text = "Расположение на коленях сбоку от человека с размещением выпрямленных в локтевых суставах рук на середине грудины с расположением кистей рук по типу ладонь на ладони или замок.";
			Answer2.text = "Расположение стоя над человеком";
			Answer3.text = "Расположение на коленях сбоку от человека с размещением выпрямленных в локтевых суставах рук на середине грудины с расположением кистей рук в кулак";
			Answer4.text = "Расположение на коленях сбоку от человека с размещением выпрямленных в локтевых суставах рук на верхней части грудины с расположением кистей рук в кулак";
			RandonButtons();
			return;
		}
		
		
		if ((name=="0")&&(step==112))
		{
			step=113;
			active=true;
			time=0;
			
			QuestionCanvas.SetActive(true);
			Question.text = "Что делать?";
			Answer1.text = "Проведение компрессий грудной клетки строго перпендикулярно грудине с глубиной 5-6 см и обеспечением полного расправления грудной клетки после каждой компрессии.";
			Answer2.text = "Проведение компрессий грудной клетки строго перпендикулярно грудине с глубиной 1-2 см и обеспечением полного расправления грудной клетки после каждой компрессии.";
			Answer3.text = "Проведение компрессий грудной клетки строго перпендикулярно грудине с глубиной 7-8 см и обеспечением полного расправления грудной клетки после каждой компрессии.";
			Answer4.text = "Проведение компрессий грудной клетки строго перпендикулярно грудине с глубиной 3-9 см и обеспечением полного расправления грудной клетки после каждой компрессии.";
			RandonButtons();
			return;
		}
		
		
		if ((name=="0")&&(step==115))
		{
			step=116;
			active=true;
			time=0;
			
			QuestionCanvas.SetActive(true);
			Question.text = "Появилось дыхание, пульс. Что делать?";
			Answer1.text = "Вызвать скорую помощь.";
			Answer2.text = "Поставить человека на ноги.";
			Answer3.text = "Положить человека на бок.";
			Answer4.text = "Положить человека на живот.";
			RandonButtons();
			return;
		}
		
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
