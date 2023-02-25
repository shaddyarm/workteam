/***************************************************************************
ScenarioEditor.cs  - редактор/пролигрыватель сценария 
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
using System.IO;

public class ScenarioEditor : MonoBehaviour 
{
	public string ScenarioName = ""; //название сценария
	public bool exam_mode = false; //режим экзамена
	public bool ShowCorrectAnswerInEduMode = true;
	
	public bool AutoStart = true;
	public bool isFinished=false;
	
	public int ВсегоДействий = 0;
	public int ВсегоОшибок =0;
	[Header("Показать отчет по завершении и отправить в LRS")]
	public bool ShowResult = true;

	[Header("не показывать отчет по завершении , но отправить в LRS")]
	public bool labMode = false;

	[Header("доп. сохранить в файл на рабочий стол")]
	public bool saveToFile = false;
	float timeElapsed; //только для записи в файл

	public float GetTimeElapsed() { return timeElapsed; }


	public GameObject start_point = null;
	public void SetStartPoint(GameObject _start_point)
    {
		start_point = _start_point;
	}
	
	//Текущий номер шага
	int CurrentStepIndex=0;
	//ссылка на Gameobjct текущего шага
	public GameObject CurrentStepObject=null;
	
	//Тут все необходимые GUI-элементы (меню, тексты и т.д.)
	public Text sss;
	
	
	public GameObject TopMenu;
	public Text TopMenuText;
	public Button TopMenuButton;
	public AudioSource AudioSourceForMessage;
	
	public GameObject DocumentViewer;
	public DocumentShowClass DocClass;
	public Button DocumentViewerCloseButton;
	
	public ArrowClass Arrow;
	public PilonClass Pilon;
	
	public GameObject QuestionMenu;
	public Text QuestionText;
	public Button Answer1Button;
	public Button Answer2Button;
	public Button Answer3Button;
	public Button Answer4Button;
	public AudioClip StandartQuestionClip = null;
	public AudioClip StandartCorrectQuestionClip = null;
	public AudioClip StandartIncorrectQuestionClip = null;
	
	
	public GameObject Напоминалка;
	public Text Напоминалка_текст;
	
	public GameObject Report;
	public Text Report_text;
	
	public enum ExampleEnum {
		Текст,						//текст сверху, текст с кнопкой и т.д.
		Звук,  						//синхронно, асинхронно 
		Сценарий_перейти, 			//субсценарий с возвратом или без возврата к предыдущему
		//Сценарий_ветвление,		//переход на субсценарий по условию (выбор ответа или случайно)
		Скрипт,						//запуск 1 или набора скриптов / команд (типа SetActive)
		Документ_показать,			//показ документа-картинки с прокруткой и клавишей закрыть
		Стрелка,					//нужно нажатие или нет в настройках
		Пилон,						//
		//Объект_выделить,			//нужно нажатие или нет в настройках	
		Анимация,					//проиграть анимацию (с времени до времени, с состояния до состояния)
		Человек_идти,				//набор координат для перемещения, поворота, ждать не ждать
		//Человек_говорить,			//+
		//Человек_делать,			//+
		//ММ_Условие,				//Набор условий из ММ (P1>10MPa типа) для продолжения выполнения, сценарий, содержащий только условие выполняется асинхронно и может включать уже другой сценарий при определенных условиях
		Ждать,						//время
		Ничего,
		Вопрос
	}

	private List<GameObject> ScenarioStepList = new List<GameObject>();
	
	
	
	
	[Header("Расширенный отчет (ETA FTA)")]
 	//для расшмренного отчета...
	//FTA ETA
	//Информация по сценарию....
	//
	//Последствие прохождения до конца (isFinished==true)
	//ПРИЧИНА(Cause)-ПОСЛЕДСТВИЕ/Исход/Результат(Outcome)
	//например... Обучаемый увидел опасную ситуацию, диагностировал ее, принял решение и выполнил необходимые действия
	[Tooltip("например...обучаемый увидел, что стропальщик работает без перчаток и указал на это")]
	//обучаемый увидел, что стропальщик работает без перчаток и указал на это
	public string ПричинаВыполнения="";
	[Tooltip("например... Стропальщик надел перчатки и не повредил руку острым выступом груза который ему попался...")]
	//например... Стропальщик надел перчатки и не повредил руку острым выступом груза который ему попался...
	public string ПоследствиеВыполнения="";
	//
	//Последствия не-прохождения до конца (isFinished==false)
	//ПРИЧИНА(Cause)-ПОСЛЕДСТВИЕ/Исход/Результат(Outcome)
	//например... Обучаемый не увидел опасную ситуацию, не диагностировал ее, не принял решение и не выполнил необходимые действия
	//обучаемый не увидел, что стропальщик работает без перчаток или не указал на это
	[Tooltip("например... обучаемый не увидел, что стропальщик работает без перчаток или не указал на это")]
	public string ПричинаНЕвыполнения="";
	//например... Стропальщик без перчаток повредил руку острым выступом груза который ему попался...
	[Tooltip("например... Стропальщик без перчаток повредил руку острым выступом груза который ему попался...")]
	public string ПоследствиеНЕвыполнения="";
	
	[Header("Сценарий последствий")]
	[Tooltip("Сценарий, показывающий последствия невыполнения или неверного выполнения действий этого сценария")]
	public ScenarioEditor Последствия=null;
	
	//
	[Header("Вывод краткой информации по выполнению")]
	public GameObject РезультатВыполнения = null;
	public Text РезультатВыполнения_текст = null;

	[Header("Вывод подробного отчета по выполнению в конце")]
	public GameObject ДетальныйОтчетВыполнения = null;
	public Text ДетальныйОтчетВыполнения_текст = null;


	//Устарело, будет удалено!
		//[HideInInspector]
		//public List<string> ДетальныйОтчетОДействиях= new List<string>();
		//[HideInInspector]
		//public List<string> ПоследствиеВыполненияДействий= new List<string>();
		//[HideInInspector]
		//public List<string> ПоследствиеНЕвыполненияДействий= new List<string>();
	
	//заменено на .... ReportStorage
	[HideInInspector]
	public ReportStorageClass ReportStorage = new ReportStorageClass();

	[Header("ID перевода")]
	public string translateID = "";
	
	
	[Header("Ответ в LRS")]
	public xAPI ReportLMS;
	












	public void LoadAndUpdateTextsFromFile(ref string jsonString)
	{
		if (translateID!="")
		{
			//ПричинаВыполнения
			//ПоследствиеВыполнения
			//ПричинаНЕвыполнения
			//ПоследствиеНЕвыполнения
			SimpleJSON.JSONNode data = SimpleJSON.JSON.Parse(jsonString);
			foreach(SimpleJSON.JSONNode record in data[translateID])
			{
				this.name = record["Название"].Value;
				
				ПричинаВыполнения = record["ПричинаВыполнения"].Value;
				ПоследствиеВыполнения = record["ПоследствиеВыполнения"].Value;
				ПричинаНЕвыполнения = record["ПричинаНЕвыполнения"].Value;
				ПоследствиеНЕвыполнения = record["ПоследствиеНЕвыполнения"].Value;
			}
		}
	}
	

    void MakeList(Transform child_)
	{
		foreach (Transform child in child_)
		{
			//детект группы
			Component[] allComponents = child.gameObject.GetComponents<Component>();
			 // Contains only Transform?
			if (allComponents.Length == 1)
			{
				//Debug.Log("That gameobject is Group");
				//считать группой
				//но если это не Scenario_wait_group_values, там элементы не должны быть добавлены в сценарий как отдельные шаги
				if (child.gameObject.GetComponent<Scenario_wait_group_values>()!=null)
				{
					ScenarioStepList.Add (child.gameObject);
				}
				else
				{
					MakeList (child);
				}
			}
			else
			{
				ScenarioStepList.Add (child.gameObject);
			}
		}
	}
	

	void Start()
	{
		//
		MakeList (this.transform);
		
		
		if (AutoStart==true) 
		{
			ManualStart(0);
		}
	}

	void Update()
	{
		timeElapsed += Time.deltaTime;
	}

	public void SetExamMode(bool value)
	{
		exam_mode = value;
		Debug.Log("Получилось");
	}
	
	public void ManualStart(int num_step=0)
	{
		timeElapsed = 0;


		isFinished =false;
		int _num_step = num_step;
		if (exam_mode==false)
		{
			if (ShowCorrectAnswerInEduMode==true)
			{
				Answer1Button.GetComponentInChildren<Text>().fontStyle = FontStyle.Bold;
			}
		}
		else
		{
			Answer1Button.GetComponentInChildren<Text>().fontStyle = FontStyle.Normal;
		}
		
		if (start_point!=null)
		{
			for (int i=0; i < ScenarioStepList.Count ; i++)
			{
				if (ScenarioStepList[i]==start_point)
				{
					_num_step=i;
					break;
				}
			}
			
		}

		
		if (ScenarioStepList.Count <= _num_step) return;
		//на первый шаг
		CurrentStepIndex=_num_step;
		CurrentStepObject = ScenarioStepList[CurrentStepIndex];
		//поехали
		PrepareStep();
	}
	
	//прерывание сценария
	public void StepEnd()
	{
		Debug.Log("!!!Finish!!!");
		isFinished=true;
	}
	
	//следующий шаг
	public void StepFinish()
	{
		//я знаю текущий элемент, могу что-нужно из него прочитать, например, что ответил пользователь
		
		//тут должен быть разбор ситуации если что-то типа теста или перехода на другой сценарий
		
		
		
		if (CurrentStepIndex<ScenarioStepList.Count - 1)
		{
			Debug.Log("!!!next step!!!");
			CurrentStepIndex++;
			CurrentStepObject = ScenarioStepList[CurrentStepIndex];
			//поехали
			PrepareStep();
		}
		else
		{
			Debug.Log("!!!Finish!!!");
			isFinished=true;

			//если режим экзамена
			if (ShowResult == true)
			{
				if (exam_mode == true)
				{
					//показываем диалог с количеством ответов и ошибками
					Report.SetActive(true);
					Report_text.text = "Экзамен закончен. Вы совершили " + ВсегоОшибок.ToString() + " ошибок из " + ВсегоДействий.ToString() + " возможных";
				}
				else
				{
					Report.SetActive(true);
					Report_text.text = "Самоподготовка закончена. Вы совершили " + ВсегоОшибок.ToString() + " ошибок из " + ВсегоДействий.ToString() + " возможных";
				}
			}

				
				string stepsToXapi = "";
				//-----------------------
				for (int z = 0; z < ReportStorage.ReportStorageStepsList.Count; z++)
				{
					stepsToXapi += ReportStorage.ReportStorageStepsList[z].datatime_real + " " + ReportStorage.ReportStorageStepsList[z].definition_description + System.Environment.NewLine;
				}
				stepsToXapi += System.Environment.NewLine + "Последствие принятых решений или действий:" + System.Environment.NewLine;
				for (int z = 0; z < ReportStorage.ReportStorageEffextsList.Count; z++)
				{
					stepsToXapi += ReportStorage.ReportStorageEffextsList[z].datatime_real + " " + ReportStorage.ReportStorageEffextsList[z].definition_description + System.Environment.NewLine;
				}

			/*
			for (int z = 0; z < ПоследствиеВыполненияДействий.Count; z++)
			{
				stepsToXapi += ПоследствиеВыполненияДействий[z] + System.Environment.NewLine;
			}
			stepsToXapi += System.Environment.NewLine + "Последствие НЕ верно принятых решений или действий:" + System.Environment.NewLine;
			for (int z = 0; z < ПоследствиеНЕвыполненияДействий.Count; z++)
			{
				stepsToXapi += ПоследствиеНЕвыполненияДействий[z] + System.Environment.NewLine;
			}
			*/


			if (ShowResult == true)
			{
				//---------------------------
				if (ДетальныйОтчетВыполнения != null)
				{
					ДетальныйОтчетВыполнения.SetActive(true);
					ДетальныйОтчетВыполнения_текст.text = stepsToXapi;
				}
			}

			if (saveToFile==true)
			{
				string myString = "Подключение абонента" + System.Environment.NewLine;
				if (exam_mode==true)
                {
					myString += "Экзамен" + System.Environment.NewLine;
				}
				else
                {
					myString += "Обучение" + System.Environment.NewLine;
				}
				myString += "cmi.score.min" + System.Environment.NewLine;
				myString += "0" +System.Environment.NewLine;
				myString += "cmi.score.max" + System.Environment.NewLine;
				myString += ВсегоДействий.ToString("N0") + System.Environment.NewLine;
				myString += "cmi.score.raw" + System.Environment.NewLine;
				myString += (ВсегоДействий - ВсегоОшибок).ToString("N0") +System.Environment.NewLine;
				myString += "cmi.progress_measure" + System.Environment.NewLine;
				myString += "1" +System.Environment.NewLine;
				myString += "cmi.success_status"+ System.Environment.NewLine;
				myString += "passed" + System.Environment.NewLine;
				myString += "cmi.completion_status"+ System.Environment.NewLine;
				myString += "completed" + System.Environment.NewLine;
				myString += "cmi.session_time" + System.Environment.NewLine;
				myString += timeElapsed.ToString("N0");

				string folderPath = System.Environment.GetFolderPath(System.Environment.SpecialFolder.Desktop);
				var bytes = System.Text.Encoding.UTF8.GetBytes(myString);
				File.WriteAllBytes(folderPath + "/SCORM.output", bytes);
			}

			if (ReportLMS!=null)
			{
				int all = ВсегоДействий;
				int correct_fix = ВсегоДействий - ВсегоОшибок;
				ReportLMS.max = (float)ВсегоДействий;
				ReportLMS.min = 0;
				ReportLMS.score = (float)correct_fix;
				ReportLMS.completion = true;
				ReportLMS.success = true;
				ReportLMS.examMode = exam_mode;

				ReportLMS.AppendDataToReport(ReportStorage);

				if (ShowResult == true)
				{
					ReportLMS.CommitToLRS();
				}

				if (labMode == true)
				{
					ReportLMS.CommitToLRS();
				}
			}
			//-----------------------
			

			//
			
			//ETA FTA
			if ((ПричинаВыполнения!="")&&(ПоследствиеВыполнения!=""))
			{
				if ((РезультатВыполнения!=null)&&(РезультатВыполнения_текст!=null))
				{
					РезультатВыполнения.SetActive(true);
					РезультатВыполнения_текст.text = ПричинаВыполнения + " / " + ПоследствиеВыполнения;
				}
			}
		}
	}
	
	//добавить количество действий и ошибок
	public void AddToReport(bool correct)
	{
		ВсегоДействий++;
		if (correct==false) ВсегоОшибок++;
	}
	
	

	void PrepareStep()
	{
		
		//текст
		{
			Scenario_step_text temp = CurrentStepObject.GetComponent<Scenario_step_text>();
			if (temp != null)
			{
				temp.Setup (this);
				return;
			}
		}
		
		//звук
		{
			Scenario_step_sound temp = CurrentStepObject.GetComponent<Scenario_step_sound>();		
			if (temp != null)
			{
				temp.Setup (this);
				return;
			}
		}
		
		//Сценарий_перейти
		{
			Scenario_GoTo_Other temp = CurrentStepObject.GetComponent<Scenario_GoTo_Other>();
			if (temp != null)
			{
				temp.Setup (this);
				return;
			}
		}
		
		//Скрипт
		{
			Scenario_step_script temp = CurrentStepObject.GetComponent<Scenario_step_script>();
			if (temp != null)
			{
				temp.Setup (this);
				return;
			}
		}
		
		//Документ_показать
		{
			Scenario_step_showDocument temp = CurrentStepObject.GetComponent<Scenario_step_showDocument>();
			if (temp != null)
			{
				temp.Setup (this);
				return;
			}
		}
		
		//Стрелка
		{
			Scenario_step_showArrow temp = CurrentStepObject.GetComponent<Scenario_step_showArrow>();
			if (temp != null)
			{
				temp.Setup (this);
				return;
			}
		}
		
		//Пилон
		{
			Scenario_step_showPilon temp = CurrentStepObject.GetComponent<Scenario_step_showPilon>();
			if (temp != null)
			{
				temp.Setup (this);
				return;
			}
		}
		
		//Анимация
		{
			Scenario_step_Animation temp = CurrentStepObject.GetComponent<Scenario_step_Animation>();
			if (temp != null)
			{
				temp.Setup (this);
				return;
			}
		}
		
		
		//Человек_идти
		{
			Scenario_step_HumanGoTo temp = CurrentStepObject.GetComponent<Scenario_step_HumanGoTo>();
			if (temp != null)
			{
				temp.Setup (this);
				return;
			}
		}
		
		
		
		//Ждать
		{
			Scenario_step_waiter temp = CurrentStepObject.GetComponent<Scenario_step_waiter>();
			if (temp != null)
			{
				temp.Setup (this);
				return;
			}
		}
		
		//Вопрос
		{
			Scenario_step_question temp = CurrentStepObject.GetComponent<Scenario_step_question>();
			if (temp != null)
			{
				temp.Setup (this);
				return;
			}
		}
		
		//пробел клавиша
		{
			Scenario_step_key temp = CurrentStepObject.GetComponent<Scenario_step_key>();
			if (temp != null)
			{
				temp.Setup (this);
				return;
			}
		}
		
		//триггер bool
		{
			Scenario_wait_trigger temp = CurrentStepObject.GetComponent<Scenario_wait_trigger>();
			if (temp != null)
			{
				temp.Setup (this);
				return;
			}
		}
		
		
		//Значение x>value<y
		{
			Scenario_wait_value temp = CurrentStepObject.GetComponent<Scenario_wait_value>();
			if (temp != null)
			{
				temp.Setup (this);
				return;
			}
		}
		
		//Группа значений x>value<y
		{
			Scenario_wait_group_values temp = CurrentStepObject.GetComponent<Scenario_wait_group_values>();
			if (temp != null)
			{
				temp.Setup (this);
				return;
			}
		}


		//сообщение в отчет
		{
			Scenario_AddToReport temp = CurrentStepObject.GetComponent<Scenario_AddToReport>();
			if (temp != null)
			{
				temp.Setup(this);
				return;
			}
		}






		Debug.Log ("step width name ='" + ScenarioStepList[CurrentStepIndex].name + "' is wrong!");
	}


}


