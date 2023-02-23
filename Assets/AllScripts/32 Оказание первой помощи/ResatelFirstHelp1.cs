using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class ResatelFirstHelp1 : MonoBehaviour 
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
	
	
	public GameObject Sphere1;
	public GameObject Patch1;
	
	public GameObject Sphere2;
	public GameObject Patch2;
	
	public GameObject Sphere3;
	public GameObject Patch3;
	public GameObject blood3;
	

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
	
		if (step==103)
		{
			time += Time.deltaTime;
			float frame = time/10f;
			if (frame<=121f/121f)
			{	
				A.ForceStateNormalizedTime(frame);
			}
			if (frame>121f/121f)
			{
				active=false;
			}
		}
		
		
		if (step==105)
		{
			//показать повязку, спрятать шар, перейти на 0
			Sphere1.SetActive(false);
			Patch1.SetActive(true);
			step=0;
		}
	
		if (step==205)
		{
			//показать повязку, спрятать шар, перейти на 0
			Sphere2.SetActive(false);
			Patch2.SetActive(true);
			step=0;
		}
		
		
		
		
		
		
		if (step==307)
		{
			Patch3.SetActive(true);
			blood3.SetActive(false);
		}
		
		if (step>=320)
		{
			//показать повязку, спрятать шар, перейти на 0
			Sphere3.SetActive(false);
			step=0;
		}
		
		
		
	}
	
	
	public void CORRECT_ANSWER()
	{
		QuestionCanvas.SetActive(false);
		step++;
		active=false;
		
		correct++;
		
		
		if (correct>=16)
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
		
		if ((name=="0_1")&&(step==0))
		{
			step=100;
			active=true;
			time=0;
			
			QuestionCanvas.SetActive(true);
			Question.text = "Пострадавший в сознании. Крови мало. Что необходимо сделать?";
			Answer1.text = "Опасность инфекции, промываем и накладываем повязку.";
			Answer2.text = "Ничего не делаем, опасности нет.";
			Answer3.text = "Опасность инфекции, промываем и накладываем жгут.";
			Answer4.text = "Обрабатываем растворителем.";
			RandonButtons();
		}
		
		if ((name=="0_1")&&(step==101))
		{
			step=102;
			active=true;
			time=0;
			
			QuestionCanvas.SetActive(true);
			Question.text = "для промывки годится....";
			Answer1.text = "любая бесцветная жидкость, которую можно пить";
			Answer2.text = "только спирт";
			Answer3.text = "тольго бутиоированная вода";
			Answer4.text = "только негазированная вода";
			RandonButtons();
		}
		
		if ((name=="0_1")&&(step==103))
		{
			step=104;
			active=true;
			time=0;
			
			QuestionCanvas.SetActive(true);
			Question.text = "для повязки годится....";
			Answer1.text = "любая чистая (относительно) ткань";
			Answer2.text = "тольго бинт";
			Answer3.text = "тольго жгут";
			Answer4.text = "только хлопчатобумажная ткань";
			RandonButtons();
		}
		
	
		
		
		////////////////////
			
		
		
		
		if ((name=="0_2")&&(step==0))
		{
			step=200;
			active=true;
			time=0;
			
			QuestionCanvas.SetActive(true);
			Question.text = "Пострадавший в сознании.  Крови много. Что необходимо сделать?";
			Answer1.text = "Опасность кровопотери, наложить давящую повязку.";
			Answer2.text = "Ничего не делаем, опасности нет.";
			Answer3.text = "Опасность инфекции, промываем и накладываем жгут.";
			Answer4.text = "Опасность инфекции, промываем и накладываем повязк.";
			RandonButtons();
		}
		
		
		if ((name=="0_2")&&(step==201))
		{
			step=202;
			active=true;
			time=0;
			
			QuestionCanvas.SetActive(true);
			Question.text = "для повязки годится....";
			Answer1.text = "любая чистая (относительно) ткань";
			Answer2.text = "тольго бинт";
			Answer3.text = "тольго жгут";
			Answer4.text = "только хлопчатобумажная ткань";
			RandonButtons();
		}
		
		
		if ((name=="0_2")&&(step==203))
		{
			step=204;
			active=true;
			time=0;
			
			QuestionCanvas.SetActive(true);
			Question.text = "если продолжает сочиться кровь....";
			Answer1.text = "то накладываем еще повязку и сильнее прижимаем";
			Answer2.text = "снимаем уже пропитавшуюся повязку";
			Answer3.text = "то накладываем еще повязку и слабее прижимаем";
			Answer4.text = "то накладываем еще повязку и не прижимаем";
			RandonButtons();
		}

		
		if ((name=="0_3")&&(step==0))
		{
			step=300;
			active=true;
			time=0;
			
			QuestionCanvas.SetActive(true);
			Question.text = "Пострадавший в сознании. Фонтан крови. Что необходимо сделать?";
			Answer1.text = "Очень быстрая кровопотеря. Необходимо зажать артерию, наложить жгут.";
			Answer2.text = "Опасность кровопотери, наложить давящую повязку.";
			Answer3.text = "Опасность инфекции, промываем и накладываем повязку.";
			Answer4.text = "Вызвать медиков.";
			RandonButtons();
		}
		
		if ((name=="0_3")&&(step==301))
		{
			step=302;
			active=true;
			time=0;
			
			QuestionCanvas.SetActive(true);
			Question.text = "Жгут накладывается ";
			Answer1.text = "накладывается выше раны";
			Answer2.text = "накладывается ниже раны";
			Answer3.text = "на рану";
			Answer4.text = "не важно";
			RandonButtons();
		}
		
		if ((name=="0_3")&&(step==303))
		{
			step=304;
			active=true;
			time=0;
			
			QuestionCanvas.SetActive(true);
			Question.text = "Жгут накладывается ";
			Answer1.text = "на одежду (если одежды нет — подкладываем)";
			Answer2.text = "без одежды";
			Answer3.text = "желательно без одежды";
			Answer4.text = "не важно";
			RandonButtons();
		}
		
		if ((name=="0_3")&&(step==305))
		{
			step=306;
			active=true;
			time=0;
			
			QuestionCanvas.SetActive(true);
			Question.text = "1 тур жгута — закрепляем и .... ";
			Answer1.text = "потом растягиваем и накладываем 3—4 тура";
			Answer2.text = "потом накладываем 3—4 тура";
			Answer3.text = "потом растягиваем и накладываем 5—6 тура";
			Answer4.text = "потом накладываем 5—6 тура";
			RandonButtons();
		}
		
		if ((name=="0_3")&&(step==307))
		{
			step=308;
			active=true;
			time=0;
			
			QuestionCanvas.SetActive(true);
			Question.text = "жгут накладывать быстро, .... ";
			Answer1.text = "снимать медленно, постепенно";
			Answer2.text = "снимать быстро, постепенно";
			Answer3.text = "не важно";
			Answer4.text = "со средней скоростью";
			RandonButtons();
		}
		
		if ((name=="0_3")&&(step==309))
		{
			step=310;
			active=true;
			time=0;
			
			QuestionCanvas.SetActive(true);
			Question.text = "пишем дату и время наложения жгута... ";
			Answer1.text = "на лбу (чем угодно)";
			Answer2.text = "на руке (чем угодно)";
			Answer3.text = "на бедре (ручкой)";
			Answer4.text = "на груди (ручкой)";
			RandonButtons();
		}
		
		if ((name=="0_3")&&(step==311))
		{
			step=312;
			active=true;
			time=0;
			
			QuestionCanvas.SetActive(true);
			Question.text = "максимальное время наложения жгута... ";
			Answer1.text = "зимой — 1 час, летом — 2 часа";
			Answer2.text = "зимой — 2 часа, летом — 3 часа";
			Answer3.text = "зимой — 3 часа, летом — 2 часа";
			Answer4.text = "зимой — 4 часа, летом — 1 часа";
			RandonButtons();
		}
		
		if ((name=="0_3")&&(step==313))
		{
			step=314;
			active=true;
			time=0;
			
			QuestionCanvas.SetActive(true);
			Question.text = "потом ослабить на ... ";
			Answer1.text = "5—10 минут и наложить жгут чуть выше";
			Answer2.text = "15—20 минут и наложить жгут чуть выше";
			Answer3.text = "1—2 минуты и наложить жгут чуть выше";
			Answer4.text = "5—15 минут и наложить жгут чуть выше";
			RandonButtons();
		}
		
		if ((name=="0_3")&&(step==315))
		{
			step=316;
			active=true;
			time=0;
			
			QuestionCanvas.SetActive(true);
			Question.text = "проверить, что жгут наложен правильно — ... ";
			Answer1.text = "отсутствует пульс на конечности";
			Answer2.text = "есть пульс на конечности";
			Answer3.text = "отсутствует пульс на шее";
			Answer4.text = "невозможно проверить";
			RandonButtons();
		}
		 
		if ((name=="0_3")&&(step==317))
		{
			step=319;
			active=true;
			time=0;
			
			QuestionCanvas.SetActive(true);
			Question.text = "доставить пострадавшего к врачу ...";
			Answer1.text = "как можно быстрее";
			Answer2.text = "без разницы";
			Answer3.text = "можно не доставлять";
			Answer4.text = "в течении 5 часов";
			RandonButtons();
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
