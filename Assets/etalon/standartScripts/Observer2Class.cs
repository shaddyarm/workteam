using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using System.IO;
using System;

public class Observer2Class : MonoBehaviour 
{
	public xAPI SCORM;

	public ToMainMenuClass breaker;
	
	public string НАЗВАНИЕ;
	
	float StartTime;
	
	public string mode;
	public List <Element2Class> elements;
	
	public bool ЭКЗАМЕН;
		
	public GameObject textPanel;
	public Text text;
	
	int Индекс;
	
	int balls;
	
	public GameObject ВОПРОСЫ;
	public Text Q;
	public Button b1;
	public Button b2;
	public Button b3;
	public Button b4;
	
	float timeElapsed;
	
	//Верхнее меню...
	public GameObject textPanelWelcome;
	public Text textWelcome;
	public String EducationWelcomeText ="Выполнение действий - обучение. Вам необходимо выполнить определенные действия. Варианты действий будут предложены. Текст правильного варианта выделен жирным шрифтом. При правильном выборе действия будет показана полусфера, где это действие может быть выполнено, также появятся варианты дальнейших действий для выбора. И так далее.";
	public String ExamWelcomeText="Выполнение действий - экзамен Вам необходимо выполнить определенные действия. Варианты действий будут предложены. При правильном выборе действия будет показана полусфера, где это действие может быть выполнено, также появятся варианты дальнейших действий для выбора. И так далее. В случае правильного выбора с первого раза, Вы получаете +1 балл. При неправильном выборе, баллы не присуждаются, будет предложено выбирать варианты, пока не выберите правильный вариант. При правильном выборе не с первого раза, также баллы не присуждаются.";
	bool active=false;
	
	
	bool fakeAnswer=false;
	
	
	
	
	public void SetExamMode()
	{
		ЭКЗАМЕН=true;
		active=false;
	}
	public void SetEducationMode()
	{
		ЭКЗАМЕН=false;
		active=false;
	}
	
	public void Reset()
	{
		Start () ;
		active=false;
	}
	
	public void SetMode(string _mode)
	{
		timeElapsed=0;
		
		mode=_mode;
		balls=0;

		if (ЭКЗАМЕН==true)
		{
			textPanelWelcome.SetActive(true);
			textWelcome.text = ExamWelcomeText;
		}
		else
		{
			textPanelWelcome.SetActive(true);
			textWelcome.text = EducationWelcomeText;
		}
		active=true;
		
	}
	
	public void GO()
	{
		if (active==false) return;
		if (Индекс>0) return;
		if (mode=="ДЕЙСТВИЯ")
		{
			textPanel.SetActive(true);
			text.text = "";
			
			if (ЭКЗАМЕН==true)
			{
				breaker.isEkzamen=true;
				//экзамен.interactable  =false;
				//экзамен.isOn =false;
			}
							
			Индекс=0;
			ShowQuestion();
		}
	}
	
	void Update () 
	{
		if (mode=="") return;
		timeElapsed+=Time.deltaTime;
	}
	
	public void CorrectAnswer()
	{
		if (mode=="") return;
		
		//показываем точки, если они есть
		//и ждем когда пользователь все понажимает
		elements[Индекс].show();
		//если нажимать нечего, не ждем
		if (elements[Индекс].subElements.Count ==0)
		{
			Press(elements[Индекс].ID);
		}
		
	}
	
	public void InCorrectAnswer()
	{
		if (mode=="") return;
		
		//balls--;
		text.text = "Баллы: " + balls.ToString("N0") + " из " + elements.Count.ToString("N0");
		fakeAnswer=true;
	}
	
	
	void ShowQuestion()
	{
		ВОПРОСЫ.SetActive(true);
		Q.text  = elements[Индекс].QUESTION;
		b1.GetComponentInChildren<Text>().text = elements[Индекс].ANSWER1;
		b2.GetComponentInChildren<Text>().text = elements[Индекс].ANSWER2;
		b3.GetComponentInChildren<Text>().text = elements[Индекс].ANSWER3;
		b4.GetComponentInChildren<Text>().text = elements[Индекс].ANSWER4;
		
		/*
		float[] YArray = new float[4];
		YArray [0] = 100f;
		YArray [1] = 20f;
		YArray [2] = -60f;
		YArray [3] = -140f;
		
		
		for (int XX=0; XX<=3; XX++)
		for (int YY=0; YY<=3; YY++)
		{
			if (UnityEngine.Random.Range(0.0f,1.0f)>0.5f)
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
		*/
		
		var r1 = b1.transform as RectTransform;
		var r2 = b2.transform as RectTransform;
		var r3 = b3.transform as RectTransform;
		var r4 = b4.transform as RectTransform;
		
		float q1 = UnityEngine.Random.Range(1.0f,4.0f);
		float q2 = UnityEngine.Random.Range(1.0f,4.0f);
		float q3 = UnityEngine.Random.Range(1.0f,4.0f);
		float q4 = UnityEngine.Random.Range(1.0f,4.0f);
		
		r1.SetSiblingIndex ((int)q1);
		r2.SetSiblingIndex ((int)q2);
		r3.SetSiblingIndex ((int)q3);
		r4.SetSiblingIndex ((int)q4);
		
		
		if (ЭКЗАМЕН==false)
		{
			b1.GetComponentInChildren<Text>().fontStyle = FontStyle.Bold;
		}
		else
		{
			b1.GetComponentInChildren<Text>().fontStyle = FontStyle.Normal;
		}
		
		
	}
		
	void Start () 
	{
		ВОПРОСЫ.SetActive(false);
		textPanel.SetActive(false);
		Индекс=0;
		mode="";
		foreach (Element2Class child in elements)
		{
			child.hide();
			child.Reset();
		}
		
	}
	
	
	//нажали на элемент с ID, нужно решить что делать с ним 
	public void Press (string ID)
	{
		if (fakeAnswer==false)
		{
			balls++;
		}
		else
		{
			fakeAnswer=false;
		}
		text.text = "Баллы: " + balls.ToString("N0") + " из " + elements.Count.ToString("N0");
		
		if (Индекс==(elements.Count-1))
		{
			float z = (float)balls / (float)elements.Count * 100f;
			string temp = " (" +  z.ToString("N0") + "% правильных ответов)";
			
			text.text = "Ваш результат: " + balls.ToString("N0") + " из " + elements.Count.ToString("N0") + temp;
			if (ЭКЗАМЕН) 
			{
				breaker.isEkzamen=false;
				WriteReport();
			}
		}
		else
		{
			Индекс++;
			ShowQuestion();
		}
	}
	
	void WriteReport()
	{


		if (SCORM != null)
		{
			SCORM.examMode = ЭКЗАМЕН;
			SCORM.max = (float)elements.Count;
			SCORM.min = 0;
			SCORM.score = (float)balls;
			SCORM.completion = true;
			SCORM.success = true;
			//ReportLMS.AppendDataToReport(ReportStorage);
			SCORM.CommitToLRS();
		}

		/*
		try
		{
		ToScormScript.StartLMS();
		ToScormScript.SendData ("cmi.score.min", "0");
		ToScormScript.SendData ("cmi.score.max", elements.Count.ToString("N0"));
		ToScormScript.SendData ("cmi.score.raw", balls.ToString("N0"));
		ToScormScript.SendData ("cmi.progress_measure", "0");
		ToScormScript.SendData ("cmi.score.min", "1");
		ToScormScript.SendData ("cmi.success_status", "passed");
		ToScormScript.SendData ("cmi.completion_status", "completed");
		ToScormScript.SendData ("cmi.interactions.0.id", "1");
		ToScormScript.SendData ("cmi.interactions.0.description", НАЗВАНИЕ);
		ToScormScript.SendData ("cmi.session_time", timeElapsed.ToString("N0"));
		ToScormScript.FinishLMS();
		}
		catch (Exception ex)
		{
			Debug.Log ("ToScormScript false-" + ex);
		}
		*/
		
		/*
		string myString = "";
		myString+="cmi.score.min"+"\n";
		myString+="0"+"\n";
		myString+="cmi.score.max"+"\n";
		myString+= elements.Count.ToString("N0")+"\n";
		myString+="cmi.score.raw"+"\n";
		myString+= balls.ToString("N0")+"\n";
		myString+="cmi.progress_measure"+"\n";
		myString+="1"+"\n";
		myString+="cmi.success_status"+"\n";
		myString+="passed"+"\n";
		myString+="cmi.completion_status"+"\n";
		myString+="completed" + "\n";
		myString+="cmi.interactions.0.id" + "\n";
		myString+="1" + "\n";
		myString+="cmi.interactions.0.description" + "\n";
		myString+=НАЗВАНИЕ+ "\n";
		myString+="cmi.session_time" + "\n";
		myString+=timeElapsed.ToString("N0");
		
		
		string folderPath = System.Environment.GetFolderPath(System.Environment.SpecialFolder.Desktop);
		var bytes = System.Text.Encoding.UTF8.GetBytes(myString);
		File.WriteAllBytes (folderPath + "/SCORM.output",bytes );
		*/
		//
	}
		
}
