using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class ResatelFirstHelp3 : MonoBehaviour 
{
	int step;
	
	int incorrect;
	int correct;
	
	public GameObject QuestionCanvas;
	public Text Question;
	public Text Answer1;
	public Text Answer2;
	public Text Answer3;
	public Text Answer4;
	public Button b1,b2,b3,b4;
	
	public GameObject ResultCanvas;
	public Text Result;
	
	
	public Animator A;
	public Animator B;
	float time;
	bool active;
	
	public GameObject HUMAN;
	public GameObject SVARKA;
	

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
		B.speed=0;
		
		step=0;
		active=false;
		time=0;
	}
	
	// Update is called once per frame
	void Update () 
	{
		if (step==109)
		{
			time += Time.deltaTime;
			float frame = time/30f;
			if (frame<=30f/300f)
			{	
				A.ForceStateNormalizedTime(frame);
			}
		}
	
		
		if (step==111)
		{
			time += Time.deltaTime;
			float frame = 30f/300f + time/30f;
			if (frame<=90f/300f)
			{	
				A.ForceStateNormalizedTime(frame);
			}
		}
		
		if (step==113)
		{
			time += Time.deltaTime;
			float frame = 90f/300f + time/30f;
			if (frame<=180f/300f)
			{	
				A.ForceStateNormalizedTime(frame);
			}
		}
		
		
		if (step==115)
		{
			time += Time.deltaTime;
			float frame = 180f/300f + time/30f;
			float frame2 = time/5f;
			if (frame2<=30f/30f)
			{
				B.ForceStateNormalizedTime(frame2);
			}
			if (frame<=299f/300f)
			{	
				A.ForceStateNormalizedTime(frame);
			}
			else
			{
				step=116;
				ResultCanvas.SetActive(true);
				string result = "Правильных ответов: " + correct.ToString() + ". Ошибок " + incorrect.ToString() + ".";
				Result.text=result;
			}
		}
		
		
	}
	
	
	public void CORRECT_ANSWER()
	{
		if (step==104)
		{
			HUMAN.SetActive(false);
		}
		if (step==102)
		{
			SVARKA.SetActive(false);
		}
		
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
			Question.text = "Что необходимо сделать?";
			Answer1.text = "Прекратить работу";
			Answer2.text = "Ничего не делаем, опасности нет.";
			Answer3.text = "Отключить электрооборудование";
			Answer4.text = "Выдернуть опломбированную чеку";
			RandonButtons();
			
		}
		
		
		if ((name=="0_2")&&(step==101))
		{
			step=102;
			active=true;
			time=0;
			
			QuestionCanvas.SetActive(true);
			Question.text = "Что необходимо сделать?";
			Answer1.text = "Отключить электрооборудование";
			Answer2.text = "Принять по возможности меры по эвакуации людей";
			Answer3.text = "Сообщить о происшедшем по телефону 03 или с мобильного телефона 113 в пожарную охрану";
			Answer4.text = "Ничего не делаем, опасности нет.";
			RandonButtons();
		}
		
		
		if ((name=="0_3")&&(step==103))
		{
			step=104;
			active=true;
			time=0;
			
			QuestionCanvas.SetActive(true);
			Question.text = "Что необходимо сделать?";
			Answer1.text = "Принять по возможности меры по эвакуации людей";
			Answer2.text = "Позвонить руководству";
			Answer3.text = "Сообщить о происшедшем по телефону 01 или с мобильного телефона 112 в пожарную охрану";
			Answer4.text = "Ничего не делаем, опасности нет.";
			RandonButtons();
		}
		
		if ((name=="0_1")&&(step==105))
		{
			step=106;
			active=true;
			time=0;
			
			QuestionCanvas.SetActive(true);
			Question.text = "Что необходимо сделать?";
			Answer1.text = "Приступить к тушению пожара первичными средствами пожаротушения";
			Answer2.text = "Позвонить руководству";
			Answer3.text = "Сообщить о происшедшем по телефону 02 или с мобильного телефона 114";
			Answer4.text = "Ничего не делаем, опасности нет.";
			RandonButtons();
		}
		
		
		if ((name=="0_1")&&(step==107))
		{
			step=108;
			active=true;
			time=0;
			
			QuestionCanvas.SetActive(true);
			Question.text = "Что необходимо сделать?";
			Answer1.text = "Сорвать пломбу, выдернуть чеку";
			Answer2.text = "Поднять рычаг до отказа";
			Answer3.text = "Направить ствол-насадку на очаг пожара и нажать на курок";
			Answer4.text = "Поднять рычаг на половину";
			RandonButtons();
		}
		
		
		
		
		if ((name=="0_1")&&(step==109))
		{
			step=110;
			active=true;
			time=0;
			
			QuestionCanvas.SetActive(true);
			Question.text = "Что необходимо сделать?";
			Answer2.text = "Вставить пломбу";
			Answer1.text = "Поднять рычаг до отказа";
			Answer3.text = "Направить ствол-насадку на очаг пожара и нажать на курок";
			Answer4.text = "Поднять рычаг на половину";
			RandonButtons();
		}
		
		if ((name=="0_1")&&(step==111))
		{
			step=112;
			active=true;
			time=0;
			
			QuestionCanvas.SetActive(true);
			Question.text = "Что необходимо сделать?";
			Answer2.text = "Вставить пломбу";
			Answer3.text = "Опустить рычаг до отказа";
			Answer1.text = "Направить ствол-насадку на очаг пожара и нажать на курок";
			Answer4.text = "Поднять рычаг на половину";
			RandonButtons();
		}
		
		if ((name=="0_1")&&(step==113))
		{
			step=114;
			active=true;
			time=0;
			
			QuestionCanvas.SetActive(true);
			Question.text = "Что необходимо сделать?";
			Answer2.text = "Через 1 сек приступить к тушению пожара";
			Answer3.text = "Через 2 сек приступить к тушению пожара";
			Answer1.text = "Через 5 сек приступить к тушению пожара";
			Answer4.text = "Через 15 сек приступить к тушению пожара";
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
