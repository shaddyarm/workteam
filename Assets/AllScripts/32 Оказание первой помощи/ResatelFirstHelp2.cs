using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class ResatelFirstHelp2 : MonoBehaviour 
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
	
	public Animator A;
	float time;
	bool active;
	
	
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
	
	// Use this for initialization
	void Start () 
	{
		incorrect=0;
		correct=0;
		
		
		QuestionCanvas.SetActive(false);
		A.speed=0;
		
		step=0;
		active=false;
		time=0;
	}
	
	// Update is called once per frame
	void Update () 
	{
		if (step==115)
		{
			A.speed=1f;
		}
			
		if (step>=123)
		{
			
			step=200;
			ResultCanvas.SetActive(true);
			string result = "Правильных ответов: " + correct.ToString() + ". Ошибок " + incorrect.ToString() + ".";
			Result.text=result;
		}
	}
	
	
	public void CORRECT_ANSWER()
	{
		QuestionCanvas.SetActive(false);
		step++;
		active=false;
		
		correct++;
		
		
		
			
			
	}
	public void INCORRECT_ANSWER()
	{
		incorrect++;
	}
	
	
	
	
	public void PRESS(string name)
	{
		if (active==true) return;
		
		if ((name=="0_1")&&(step==0))
		{
			step=100;
			active=true;
			time=0;
			
			QuestionCanvas.SetActive(true);
			Question.text = "когда у пострадавшего одновременно отсутствуют и дыхание и пульс .....";
			Answer1.text = "проводится срочная сердечно-легочная реанимация.";
			Answer2.text = "Ничего не делаем, опасности нет.";
			Answer3.text = "проводится искусственная вентиляция легких";
			Answer4.text = "необходимо поднять пострадавшего";
			RandonButtons();
		}
		
		if ((name=="0_1")&&(step==101))
		{
			step=102;
			active=true;
			time=0;
			
			QuestionCanvas.SetActive(true);
			Question.text = "восстановление работы сердца может быть достигнуто .....";
			Answer1.text = "проведением прекардиального удара";
			Answer2.text = "укола антибиотика";
			Answer3.text = "искусственной вентиляцией легких";
			Answer4.text = "поднятием пострадавшего";
			RandonButtons();
		}
		
		if ((name=="0_1")&&(step==103))
		{
			step=104;
			active=true;
			time=0;
			
			QuestionCanvas.SetActive(true);
			Question.text = "Для этого .....";
			Answer1.text = "ладонь одной руки размещают на нижней трети груди и наносят по ней короткий и резкий удар кулаком другой руки.";
			Answer2.text = "ладонь одной руки размещают на нижней четверти груди и наносят по ней короткий и резкий удар кулаком другой руки.";
			Answer3.text = "ладонь одной руки размещают на верхней трети груди и наносят по ней короткий и резкий удар кулаком другой руки.";
			Answer4.text = "ладонь одной руки размещают на нижней трети груди и наносят по ней короткий и слабый удар кулаком другой руки.";
			RandonButtons();
		}
			
		if ((name=="0_1")&&(step==105))
		{
			step=106;
			active=true;
			time=0;
			
			QuestionCanvas.SetActive(true);
			Question.text = "Затем повторно проверяют наличие пульса на сонной артерии и при его отсутствии  .....";
			Answer1.text = "приступают к проведению непрямого массажа сердца и искусственной вентиляции легких";
			Answer2.text = "приступают к проведению прямого массажа сердца и искусственной вентиляции легких";
			Answer3.text = "приступают к проведению прямого массажа сердца";
			Answer4.text = "приступают к проведению непрямого массажа сердца";
			RandonButtons();
		}	
			
			
		if ((name=="0_1")&&(step==107))
		{
			step=108;
			active=true;
			time=0;
			
			QuestionCanvas.SetActive(true);
			Question.text = "Для этого пострадавшего укладывают .....";
			Answer1.text = "на жесткую поверхность спиной вниз";
			Answer2.text = "на жесткую поверхность спиной вверх";
			Answer3.text = "на мягкую поверхность спиной вниз";
			Answer4.text = "на мягкую поверхность спиной вверх";
			RandonButtons();
		}		
		 
		if ((name=="0_1")&&(step==109))
		{
			step=110;
			active=true;
			time=0;
			
			QuestionCanvas.SetActive(true);
			Question.text = "оказывающий помощь помещает свой сложенные крестом ладони .....";
			Answer1.text = "на нижнюю часть грудины пострадавшего и энергичными толчками надавливает на грудную стенку";
			Answer2.text = "на верхнюю часть грудины пострадавшего и энергичными толчками надавливает на грудную стенку";
			Answer3.text = "на нижнюю часть грудины пострадавшего и слабыми толчками надавливает на грудную стенку";
			Answer4.text = "на верхнюю часть грудины пострадавшего и слабыми толчками надавливает на грудную стенку";
			RandonButtons();
		}		
			 
		if ((name=="0_1")&&(step==111))
			{
				step=112;
				active=true;
				time=0;
				
				QuestionCanvas.SetActive(true);
				Question.text = "оказывающий толкает .....";
				Answer1.text = "используя при этом не только руки, но и массу собственного тела";
				Answer2.text = "используя при этом только руки";
				Answer3.text = "используя при этом не только руки, но и ноги";
				Answer4.text = "используя при этом не только пальцы";
				RandonButtons();
			}	
			
		if ((name=="0_1")&&(step==113))
			{
				step=114;
				active=true;
				time=0;
				
				QuestionCanvas.SetActive(true);
				Question.text = "У взрослого человека такую операцию необходимо проводить с частотой.....";
				Answer1.text = "60 надавливаний в минуту";
				Answer2.text = "120 надавливаний в минуту";
				Answer3.text = "30 надавливаний в минуту";
				Answer4.text = "12 надавливаний в минуту";
				RandonButtons();
			}		
			
		if ((name=="0_1")&&(step==115))
			{
				step=116;
				active=true;
				time=0;
				
				QuestionCanvas.SetActive(true);
				Question.text = "Правильность проводимого массажа определяется .....";
				Answer1.text = "появлением пульса на сонной артерии в такт с нажатием на грудную клетку";
				Answer2.text = "появлением пульса на руке";
				Answer3.text = "появлением пульса на шее";
				Answer4.text = "появлением пульса на груди";
				RandonButtons();
			}	


		if ((name=="0_1")&&(step==117))
			{
				step=118;
				active=true;
				time=0;
				
				QuestionCanvas.SetActive(true);
				Question.text = "Через каждые 15 надавливаний оказывающий помощь .....";
				Answer1.text = "дважды подряд вдувает в легкие пострадавшего воздух и вновь проводит массаж сердца";
				Answer2.text = "трижды подряд вдувает в легкие пострадавшего воздух и вновь проводит массаж сердца";
				Answer3.text = "4 раза подряд вдувает в легкие пострадавшего воздух и вновь проводит массаж сердца";
				Answer4.text = "6 раз подряд вдувает в легкие пострадавшего воздух и вновь проводит массаж сердца";
				RandonButtons();
			}


		if ((name=="0_1")&&(step==119))
			{
				step=120;
				active=true;
				time=0;
				
				QuestionCanvas.SetActive(true);
				Question.text = "Частота вдуваний должна составлять  .....";
				Answer1.text = "12-18 раз в минуту, то есть на каждый цикл нужно тратить 4-5 сек";
				Answer2.text = "14-22 раз в минуту, то есть на каждый цикл нужно тратить 3-4 сек";
				Answer3.text = "10-11 раз в минуту, то есть на каждый цикл нужно тратить 4-5 сек";
				Answer4.text = "2-8 раз в минуту, то есть на каждый цикл нужно тратить 5-6 сек";
				RandonButtons();
			}				
				 
		if ((name=="0_1")&&(step==121))
			{
				step=122;
				active=true;
				time=0;
				
				QuestionCanvas.SetActive(true);
				Question.text = "При восстановлении дыхания и сердечной деятельности пострадавшего, находящегося в бессознательном состоянии,  .....";
				Answer1.text = "обязательно укладывают на бок, чтобы исключить его удушение собственным запавшим языком или рвотными массами.";
				Answer2.text = "нужно напоить чаем";
				Answer3.text = "необходимо положить на живот";
				Answer4.text = "необходимо поставить на ноги";
				RandonButtons();
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
