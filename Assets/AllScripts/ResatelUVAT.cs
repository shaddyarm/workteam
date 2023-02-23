using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.Video;

public class ResatelUVAT : MonoBehaviour 
{
	public ClientClass CLIENT=null;
	
	
	int incorrect;
	int correct;
	
	int ДЕЙСТВВИЯ =0 ;
	int УСЛОВИЯ =0;
	
	public int MaxCorrect;
	
	public GameObject QuestionCanvas;
	public Text Question;
	public Text Answer1;
	public Text Answer2;
	public Text Answer3;
	public Text Answer4;
	
	public VideoPlayer player;
public  string newUrl;
	
	public Text StatisticText;
	
	
	public Animator A;
	public Animator B;
	public Animator C;
	public Animator D;
	
	
	public Button b1;
	public Button b2;
	public Button b3;
	public Button b4;
	
	public GameObject ButtonExit;
	
	float time;
	bool active;
	
	ElementNewUvatClass CurrentElement=null;

	// Use this for initialization
	void Start () 
	{
		if (newUrl!="") player.url =  System.IO.Path.Combine (Application.streamingAssetsPath,newUrl);
		
		incorrect=0;
		correct=0;
		
		QuestionCanvas.SetActive(false);
		A.speed=0.01f;
		B.speed=0.1f;
		C.speed=0.1f;
		D.speed=0.1f;
		

		active=false;
		time=0;
		
		
		
		
	}
	
	// Update is called once per frame
	void Update () 
	{
		
	}
	
	
	public void CORRECT_ANSWER()
	{

		QuestionCanvas.SetActive(false);

		active=false;
		
		correct++;
		
		CurrentElement.my.SetActive(false);
		
		
		
		
		
		ДЕЙСТВВИЯ += (int)CurrentElement.ПлюсДействиям;
		УСЛОВИЯ += (int)CurrentElement.ПлюсУсловиям;
		
		CurrentElement.CorrectAnswer();
		
		UpdateStatistic();
		
	}
	
	public void INCORRECT_ANSWER()
	{
		
		
		incorrect++;
		QuestionCanvas.SetActive(false);

		active=false;
		
		CurrentElement.my.SetActive(false);
			
		CurrentElement.InCorrectAnswer();
		
		UpdateStatistic();
	}
	
	void Peretusovka()
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
	}
	
	private void UpdateStatistic()
	{
		if (CLIENT!=null)
		{
			CLIENT.SentData(ДЕЙСТВВИЯ,УСЛОВИЯ,incorrect);
		}
		
		if ((incorrect+correct)>=MaxCorrect)
		{
			StatisticText.text = "Проверка закончена! Вами было предотвращено " + ДЕЙСТВВИЯ.ToString() + "  опасных действий. Устранено " + УСЛОВИЯ.ToString()  + " опасных условий. Число ошибок - " + incorrect.ToString(); 
			ButtonExit.SetActive(true);
		}
		else
		{
			StatisticText.text = "Вами было предотвращено " + ДЕЙСТВВИЯ.ToString() + "  опасных действий. Устранено " + УСЛОВИЯ.ToString()  + " опасных условий. Число ошибок - " + incorrect.ToString(); 
		}
	}
	

	
	
	public void PRESS(ElementNewUvatClass element)
	{
		if (active==true) return;
		
		
		CurrentElement = element;
		active=true;
		QuestionCanvas.SetActive(true);
		Question.text = CurrentElement.Question;
		Answer1.text = CurrentElement.Answer1;
		Answer2.text = CurrentElement.Answer2;
		Answer3.text = CurrentElement.Answer3;
		Answer4.text = CurrentElement.Answer4;
		
		Peretusovka();
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
