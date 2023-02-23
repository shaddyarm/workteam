/***************************************************************************
Scenario_step_question.cs  - редактор/пролигрыватель сценария 
-------------------
begin                : 27 мая 2020
copyright            : (C) 2020 by Гаммер Максим Дмитриевич (maximum2000)
email                : MaxGammer@gmail.com
site				 : lcontent.ru 
org					 : ИП Гаммер Максим Дмитриевич
***************************************************************************/
//https://github.com/cfoulston/Unity-Reorderable-List

using UnityEngine;
using UnityEngine.UI;
using System.Collections;
using System.Collections.Generic;


//текст сверху, текст с кнопкой и т.д.

public class Scenario_step_question : MonoBehaviour 
{
	//ссылка на сценарий, там все элементы и текст и меню и все остальное
	//к кнопке мы привязываемся
	//там же метод Next, когда сценарий закончился...
	private ScenarioEditor editor = null;

	[Header("Вопрос:")]
	public string Question="";
	[Header("Правильный ответ:")]
	public string AnswerCorrect="";
	[Header("Неправильные ответ:")]
	public string AnswerIncorrect1="";
	public string AnswerIncorrect2="";
	public string AnswerIncorrect3="";

	[Header("Голосовые источники:")]
	public AudioClip clipQuestion = null;
	public AudioClip clipCorrectAnswer = null;
	public AudioClip clipIncorrectAnswer = null;
	
	
	bool bad_answer = false;
	private string Answer="";
	
	[Header("Расширенный отчет (ETA FTA)")]
	//для расширенного отчета...
	//FTA ETA
	//Информация по ответу....
	//
	//Последствие верного ответа (bad_answer==false)
	//ПРИЧИНА(Cause)-ПОСЛЕДСТВИЕ/Исход/Результат(Outcome)
	//например... Обучаемый увидел опасную ситуацию (обнаружение) / диагностировал ее / принял решение / выполнил необходимые действия
	//обучаемый увидел, что стропальщик работает без перчаток
	[Tooltip("например...обучаемый увидел, что стропальщик работает без перчаток")]
	public string ПричинаВерногоОтвета="";
	//например... Стропальщик надел перчатки...
	[Tooltip("например...cтропальщик надел перчатки...")]
	public string ПоследствиеВерногоОтвета="";
	
	//
	//Последствия не-верного ответа (bad_answer==true)
	//ПРИЧИНА(Cause)-ПОСЛЕДСТВИЕ/Исход/Результат(Outcome)
	//например... Обучаемый не увидел опасную ситуацию / не диагностировал ее / не принял решение / не выполнил необходимые действия
	//обучаемый не увидел, что стропальщик работает без перчаток или не придал этому значения
	[Tooltip("например...обучаемый не увидел, что стропальщик работает без перчаток или не придал этому значения...")]
	public string ПричинаОшибочногоОтвета=""; //EffectStorageStepClass.cause , cause_full
	//например... Стропальщик остался без перчаток
	[Tooltip("например...Стропальщик остался без перчаток...")]
	public string ПоследствиеОшибочногоОтвета=""; //EffectStorageStepClass.losses
	[Tooltip("например...Ущерб 100 рублей")]
	public float ПоследствиеОшибочногоОтвета_экономика = 0; //EffectStorageStepClass.losses_money
	[Tooltip("например...потери жизни и здоровья, сломана нога, 1 погиб, 1 находится в коме")]
	public float ПоследствиеОшибочногоОтвета_здоровие = 0; //EffectStorageStepClass.losses_life_health
	[Tooltip("например...потери экология, розлив нефти в количестве 10 тонн")]
	public float ПоследствиеОшибочногоОтвета_экология = 0; //EffectStorageStepClass.losses_ecology

	[Header("Место действия в модели поведения")]
	public bool Обнаружение;
	public bool Диагностика;
	public bool ПринятиеРешений;
	public bool ВыполняемыеДействия;
	
	public GameObject РезультатВыполнения = null;
	public Text РезультатВыполнения_текст = null;
	
	[Header("ID перевода")]
	public string translateID = "";

	[Header("Категория для отчета 'Паук'")]
	public string ReportSpiderCategory = "";


	/////////////////////////////////////////////////
	public void LoadAndUpdateTextsFromFile(ref string jsonString)
	{
		if (translateID!="")
		{
			//Question="";
			//AnswerCorrect="";
			//AnswerIncorrect1="";
			//AnswerIncorrect2="";
			//AnswerIncorrect3="";

			//ПричинаВерногоОтвета
			//ПоследствиеВерногоОтвета
			//ПричинаОшибочногоОтвета
			//ПоследствиеОшибочногоОтвета
			
			SimpleJSON.JSONNode data = SimpleJSON.JSON.Parse(jsonString);
			foreach(SimpleJSON.JSONNode record in data[translateID])
			{
				Question = record["Question"].Value;
				AnswerCorrect = record["AnswerCorrect"].Value;
				AnswerIncorrect1 = record["AnswerIncorrect1"].Value;
				AnswerIncorrect2 = record["AnswerIncorrect2"].Value;
				AnswerIncorrect3 = record["AnswerIncorrect3"].Value;
				ПричинаВерногоОтвета = record["ПричинаВерногоОтвета"].Value;
				ПоследствиеВерногоОтвета = record["ПоследствиеВерногоОтвета"].Value;
				ПричинаОшибочногоОтвета = record["ПричинаОшибочногоОтвета"].Value;
				ПоследствиеОшибочногоОтвета = record["ПоследствиеОшибочногоОтвета"].Value;
			}
		}
	}
	/////////////////////////////////////////////////
	
	//настройка, привязываем обработчики
	public void Setup(ScenarioEditor _editor)
	{
		editor = _editor;
		
		bad_answer=false;
		
		
		editor.QuestionMenu.SetActive(true);
		editor.QuestionText.text = Question;
		editor.Answer1Button.GetComponentInChildren<Text>().text = AnswerCorrect;
		editor.Answer2Button.GetComponentInChildren<Text>().text = AnswerIncorrect1;
		editor.Answer3Button.GetComponentInChildren<Text>().text = AnswerIncorrect2;
		editor.Answer4Button.GetComponentInChildren<Text>().text = AnswerIncorrect3;
		
		//тусуем
		{
			//
			var r1 = editor.Answer1Button.transform as RectTransform;
			var r2 = editor.Answer2Button.transform as RectTransform;
			var r3 = editor.Answer3Button.transform as RectTransform;
			var r4 = editor.Answer4Button.transform as RectTransform;
			//
			float q1 = UnityEngine.Random.Range(1.0f,4.0f);
			float q2 = UnityEngine.Random.Range(1.0f,4.0f);
			float q3 = UnityEngine.Random.Range(1.0f,4.0f);
			float q4 = UnityEngine.Random.Range(1.0f,4.0f);
			//
			r1.SetSiblingIndex ((int)q1);
			r2.SetSiblingIndex ((int)q2);
			r3.SetSiblingIndex ((int)q3);
			r4.SetSiblingIndex ((int)q4);
			//
		}
		
	
		//
		editor.Answer1Button.onClick.AddListener(delegate { CorrectAnswer(); });
		editor.Answer2Button.onClick.AddListener(delegate { IncorrectAnswer1(); });
		editor.Answer3Button.onClick.AddListener(delegate { IncorrectAnswer2(); });
		editor.Answer4Button.onClick.AddListener(delegate { IncorrectAnswer3(); });
		
		//если нет звука на вопрос, тогда говорим стандартную фразу "Отведте на вопрос"
		if (clipQuestion!=null)
		{
			editor.AudioSourceForMessage.clip = clipQuestion;
		}
		else
		{
			editor.AudioSourceForMessage.clip = editor.StandartQuestionClip;
		}
		editor.AudioSourceForMessage.Play();
	}
	
	public void CorrectAnswer()
	{
		//bad_answer=false;
		if (clipCorrectAnswer!=null)
		{
			editor.AudioSourceForMessage.clip = clipCorrectAnswer;
		}
		else
		{
			editor.AudioSourceForMessage.clip = editor.StandartCorrectQuestionClip;
		}
		editor.AudioSourceForMessage.Play();
		//
		OK();
	}
	public void IncorrectAnswer1()
	{
		bad_answer = true;
		Answer = AnswerIncorrect1;
		if (clipIncorrectAnswer!=null)
		{
			editor.AudioSourceForMessage.clip = clipIncorrectAnswer;
		}
		else
		{
			editor.AudioSourceForMessage.clip = editor.StandartIncorrectQuestionClip;
		}
		editor.AudioSourceForMessage.Play();
	}
	public void IncorrectAnswer2()
	{
		bad_answer = true;
		Answer = AnswerIncorrect2;
		if (clipIncorrectAnswer!=null)
		{
			editor.AudioSourceForMessage.clip = clipIncorrectAnswer;
		}
		else
		{
			editor.AudioSourceForMessage.clip = editor.StandartIncorrectQuestionClip;
		}
		editor.AudioSourceForMessage.Play();
	}
	public void IncorrectAnswer3()
	{
		bad_answer = true;
		Answer = AnswerIncorrect3;
		if (clipIncorrectAnswer!=null)
		{
			editor.AudioSourceForMessage.clip = clipIncorrectAnswer;
		}
		else
		{
			editor.AudioSourceForMessage.clip = editor.StandartIncorrectQuestionClip;
		}
		editor.AudioSourceForMessage.Play();
	}
	
	
	//когда нажали на ОК...
	public void OK()
	{
		//отвязываем обработчики
		editor.Answer1Button.onClick.RemoveListener(delegate { OK(); });
		editor.Answer1Button.onClick.RemoveAllListeners();
		editor.Answer2Button.onClick.RemoveListener(delegate { OK(); });
		editor.Answer2Button.onClick.RemoveAllListeners();
		editor.Answer3Button.onClick.RemoveListener(delegate { OK(); });
		editor.Answer3Button.onClick.RemoveAllListeners();
		editor.Answer4Button.onClick.RemoveListener(delegate { OK(); });
		editor.Answer4Button.onClick.RemoveAllListeners();
		
		
		//
		//ETA FTA

		if ((РезультатВыполнения!=null)&&(РезультатВыполнения_текст!=null))
		{
			if (bad_answer==false)
			{
				if ((ПричинаВерногоОтвета!="")&&(ПоследствиеВерногоОтвета!=""))
				{
					РезультатВыполнения.SetActive(true);
					РезультатВыполнения_текст.text = ПричинаВерногоОтвета + " / " + ПоследствиеВерногоОтвета;
				}
			}
		
			if (bad_answer==true)
			{
				if ((ПричинаОшибочногоОтвета!="")&&(ПоследствиеОшибочногоОтвета!=""))
				{
					РезультатВыполнения.SetActive(true);
					РезультатВыполнения_текст.text = ПричинаОшибочногоОтвета + " / " + ПоследствиеОшибочногоОтвета;
				}
			}
		}
	
		//
		
		
		//скрываем если нужно
		editor.QuestionMenu.SetActive(false);
		
		this.gameObject.SetActive(false);
		
		//аргументы передаем в Editor, типа правильно/неправильно
		editor.AddToReport(!bad_answer);
		
		System.DateTime theTime = System.DateTime.Now;
		string datetime = theTime.ToString("yyyy-MM-dd\\ HH:mm:ss\\ ");
		
		if (bad_answer==false)
		{
			Answer = AnswerCorrect;

			ReportStorageStepClass temp = new ReportStorageStepClass();
			temp.guid_id = System.Guid.NewGuid().ToString();
			temp.definition_description = "Вопрос:" + Question + ". " + "Ответ пользователя: " + Answer + ". Ответ верен.";
			temp.datatime_real = datetime;
			temp.datatime_simulation = datetime;
			temp.type = "Scenario_step_question";
			temp.completed = 1f;
			temp.passed = 1f;
			temp.categoty = ReportSpiderCategory;
			editor.ReportStorage.ReportStorageStepsList.Add(temp);

			if ((ПричинаВерногоОтвета!="")|| (ПоследствиеВерногоОтвета != ""))
			{
				EffectStorageStepClass temp2 = new EffectStorageStepClass();
				temp2.guid_id = System.Guid.NewGuid().ToString();
				temp2.definition_description = ПоследствиеВерногоОтвета;
				temp2.datatime_real = datetime;
				temp2.datatime_simulation = datetime;
				temp2.cause = ПричинаВерногоОтвета;
				string comment = "Место в модели поведения-" + "Обнаружение=" + Обнаружение.ToString() + ", Диагностика=" + Диагностика.ToString() + ", ПринятиеРешений=" + ПринятиеРешений.ToString() + ", ВыполняемыеДействия=" + ВыполняемыеДействия.ToString();
				temp2.cause_full = comment;
				temp2.losses = "";
				temp2.losses_money = 0;
				temp2.losses_life_health = 0;
				temp2.losses_ecology = 0;
				editor.ReportStorage.ReportStorageEffextsList.Add(temp2);
			}

		}
		else
		{
			ReportStorageStepClass temp = new ReportStorageStepClass();
			temp.guid_id = System.Guid.NewGuid().ToString();
			temp.definition_description = "Вопрос:" + Question + ". " + "Ответ пользователя: " + Answer + ". Ответ неверен. Верный ответ: " + AnswerCorrect;
			temp.datatime_real = datetime;
			temp.datatime_simulation = datetime;
			temp.type = "Scenario_step_question";
			temp.completed = 1f;
			temp.passed = 0;
			temp.categoty = ReportSpiderCategory;
			editor.ReportStorage.ReportStorageStepsList.Add(temp);

			if ((ПричинаОшибочногоОтвета != "")||(ПоследствиеОшибочногоОтвета != ""))
			{
				EffectStorageStepClass temp2 = new EffectStorageStepClass();
				temp2.guid_id = System.Guid.NewGuid().ToString();
				temp2.definition_description = ПоследствиеОшибочногоОтвета;
				temp2.datatime_real = datetime;
				temp2.datatime_simulation = datetime;
				temp2.cause = ПричинаОшибочногоОтвета;
				string comment = "Место в модели поведения-" + "Обнаружение=" + Обнаружение.ToString() + ", Диагностика=" + Диагностика.ToString() + ", ПринятиеРешений=" + ПринятиеРешений.ToString() + ", ВыполняемыеДействия=" + ВыполняемыеДействия.ToString();
				temp2.cause_full = comment;

				temp2.losses = ПоследствиеОшибочногоОтвета;
				temp2.losses_money = ПоследствиеОшибочногоОтвета_экономика;
				temp2.losses_life_health = ПоследствиеОшибочногоОтвета_здоровие;
				temp2.losses_ecology = ПоследствиеОшибочногоОтвета_экология;
				
				editor.ReportStorage.ReportStorageEffextsList.Add(temp2);
			}
		}
		
		
		
		//посылаем команду на следующий шаг
		editor.StepFinish();
	}
	
}

