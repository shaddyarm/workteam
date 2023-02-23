/***************************************************************************
ScenarioEditorManager.cs  - редактор/пролигрыватель сценария 
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
using UnityEngine.SceneManagement;


public class ScenarioEditorManager : MonoBehaviour 
{
	private List<ScenarioEditor> Scanarios = new List<ScenarioEditor>();
	private List<ScenarioEditor> EffectsIncedents = new List<ScenarioEditor>();

	public bool AutoStartInEducationMode = false;
	public bool SaveReportToFile = false;
	
	public GameObject Report;
	public GameObject ReportScrollForFix;
	public Text Report_text;
	
	public string НазваниеТренажера;

	//public int Manual_ВсегоДействий = 0;
	
	public GameObject Balls;
	public Text Balls_text;
	
	public InputField FIO;
	public InputField TYPE;
	public InputField ORG;
	
	public GameObject BlockPanel;
	
	private IEnumerator coroutine;
	
	private System.DateTime theStartTime;
	
	public Play player;
	
	private bool isExam=false;
	
	private IEnumerator coroutineEffects;
	
	
	public xAPI ReportLMS;
	//заменено на .... ReportStorage
	[HideInInspector]
	public ReportStorageClass ReportStorage = new ReportStorageClass();



	public void ShowEffectsIncedents()
	{
		//EffectsIncedents
		for (int i = 0; i < Scanarios.Count ; i++)
		{
			//если обучаемый или не выполнил сценарий или выполнил его с ошибками, смотрим есть ли сценарий для последствий
			if (((Scanarios[i].isFinished==false)||(Scanarios[i].ВсегоОшибок>0))&&(Scanarios[i].Последствия!=null))
			{
				//запоминаем сценарий для показа
				EffectsIncedents.Add(Scanarios[i].Последствия);
			}
		}
		//если есть что показывать, запускаем 
		if (EffectsIncedents.Count>0)
		{
			coroutineEffects = ShowAllEffectsIncedents();
			StartCoroutine(coroutineEffects);
		}
	}
	
	
	
	private IEnumerator ShowAllEffectsIncedents()
    {
		int i=0;
		EffectsIncedents[i].ManualStart();
		
        while (i<EffectsIncedents.Count)
        {
            yield return new WaitForSeconds(1f);
			
			if (EffectsIncedents[i].isFinished==true)
			{
				i++;
				if (i<EffectsIncedents.Count)
				{
					EffectsIncedents[i].ManualStart();
				}
			}
        }
		
		Debug.Log("all effect is shows. end.");
		
    }
	
	public void FinishPress()
	{
		BlockPanel.SetActive(true);
		player.mode=-1;
		
		System.DateTime theTime = System.DateTime.Now;
		string datetime = theTime.ToString("yyyy-MM-dd HH:mm:ss");
		
		System.TimeSpan travelTime = theTime - theStartTime;  
		string duration = new System.DateTime(travelTime.Ticks).ToString("HH:mm:ss");
		
		
		
		//1. Показываем сколько нарушений было замечено + сколько было устранено (из числа замеченных)
		//2. Показываем - сколько нарушений не было засечено
		//3. Показываем, к чему привели устраненные нарушения.
		//4. Показываем, к чему привели неверно устраненные и незамеченные нарушения.
		
		//это сценарии, которые закончены (не факт что правильно)
		int НарушенийБылоЗамечено=0;
		//Это количество верных ответов
		int ИзНихИсправлено=0;
		//Это количество ошибочных ответов
		int ИзНихНеИсправлено=0;
		//Это количество сценариев, которые не закончены 
		int НарушенийНебылоЗамечено=0;

//устарело		
		List<string> ДетальныйОтчетПоВопросам= new List<string>();
		//к чему привели устраненные нарушения
		List<string> AllHappyEnds= new List<string>();
		//к чему привели неверно устраненные и незамеченные нарушения
		List<string> AllBadEnds= new List<string>();
//будет заменено на
		//[HideInInspector]
		//public ReportStorageClass ReportStorage = new ReportStorageClass();


		//1. Общая информация... (4 числа)
		for (int i = 0; i < Scanarios.Count ; i++)
		{
			НарушенийБылоЗамечено+=Scanarios[i].ВсегоДействий;
			ИзНихНеИсправлено+=Scanarios[i].ВсегоОшибок;
			if (Scanarios[i].isFinished==false) НарушенийНебылоЗамечено++;
		}
		ИзНихИсправлено = НарушенийБылоЗамечено - ИзНихНеИсправлено;
		
		
		//-----------------------
		if (ReportLMS!=null)
		{
			int all = НарушенийБылоЗамечено + НарушенийНебылоЗамечено;
			int correct_fix = ИзНихИсправлено;
			
			ReportLMS.max = all;
			ReportLMS.min=0;
			ReportLMS.score=correct_fix;
			ReportLMS.completion = true;
			ReportLMS.success = true;

		
			if (isExam)
			{
				ReportStorageStepClass temp = new ReportStorageStepClass();
				temp.guid_id = System.Guid.NewGuid().ToString();
				temp.definition_description = "Выполнение в режиме экзамена";
				temp.datatime_real = datetime;
				temp.datatime_simulation = datetime;
				temp.type = "ScenarioEditorManager";
				temp.completed = 1f;
				temp.passed = 1f;
				temp.categoty = "";
				ReportStorage.ReportStorageStepsList.Add(temp);

			}
			else
			{
				ReportStorageStepClass temp = new ReportStorageStepClass();
				temp.guid_id = System.Guid.NewGuid().ToString();
				temp.definition_description = "Выполнение в режиме самоподготовки";
				temp.datatime_real = datetime;
				temp.datatime_simulation = datetime;
				temp.type = "ScenarioEditorManager";
				temp.completed = 1f;
				temp.passed = 1f;
				temp.categoty = "";
				ReportStorage.ReportStorageStepsList.Add(temp);
			}

            {
				ReportStorageStepClass temp = new ReportStorageStepClass();
				temp.guid_id = System.Guid.NewGuid().ToString();
				temp.definition_description = "Время выполнения: " + duration;
				temp.datatime_real = datetime;
				temp.datatime_simulation = datetime;
				temp.type = "ScenarioEditorManager";
				temp.completed = 1f;
				temp.passed = 1f;
				temp.categoty = "";
				ReportStorage.ReportStorageStepsList.Add(temp);
			}
			{
				ReportStorageStepClass temp = new ReportStorageStepClass();
				temp.guid_id = System.Guid.NewGuid().ToString();
				temp.definition_description = "Возможных нарушений было замечено: " + НарушенийБылоЗамечено.ToString() + " " + "Из них исправлено: " + ИзНихИсправлено.ToString() + " " + "Из них не исправлено: " + ИзНихНеИсправлено.ToString() + " " + "Возможных нарушений не было замечено: " + НарушенийНебылоЗамечено.ToString();
				temp.datatime_real = datetime;
				temp.datatime_simulation = datetime;
				temp.type = "ScenarioEditorManager";
				temp.completed = 1f;
				temp.passed = 1f;
				temp.categoty = "";
				ReportStorage.ReportStorageStepsList.Add(temp);
			}
		}
		//-----------------------
		
		
		
		
		//2. Детальный отчет по ответам...
		for (int i = 0; i < Scanarios.Count ; i++)
		{
			
			//не выводим "неувиденные"
			if (Scanarios[i].isFinished==false) continue;

			//Название сценария
			string toscorm="";
			toscorm+="Задача :" + Scanarios[i].gameObject.name;
			if (Scanarios[i].ReportStorage.ReportStorageStepsList.Count==0)
			{
				toscorm+=". Не выполнена.";
			}
			else
			{
				toscorm+=". Выполнена.";
			}
			toscorm+=System.Environment.NewLine;
			//ReportLMS.report += toscorm;

			ReportStorageStepClass temp = new ReportStorageStepClass();
			temp.guid_id = System.Guid.NewGuid().ToString();
			temp.definition_description = toscorm;
			temp.datatime_real = datetime;
			temp.datatime_simulation = datetime;
			temp.type = "ScenarioEditorManager";
			temp.completed = 1f;
			temp.passed = 1f;
			temp.categoty = "";
			ReportStorage.ReportStorageStepsList.Add(temp);
		}

		//отправляем
		ReportLMS.AppendDataToReport(ReportStorage);
		ReportLMS.examMode = isExam;
		ReportLMS.CommitToLRS();

		/*
		//3. к чему привели устраненные нарушения
		//     +к чему привели неверно устраненные и незамеченные нарушения 
		for (int i = 0; i < Scanarios.Count ; i++)
		{
			if (Scanarios[i].ДетальныйОтчетОДействиях.Count==0)
			{
				continue;
			}
			
			//не выводим "неувиденные"
			if (Scanarios[i].isFinished==false) continue;
				
			for (int z = 0; z < Scanarios[i].ПоследствиеВыполненияДействий.Count ; z++)
			{
				AllHappyEnds.Add (Scanarios[i].ПоследствиеВыполненияДействий[z]);
			}
			
			for (int z = 0; z < Scanarios[i].ПоследствиеНЕвыполненияДействий.Count ; z++)
			{
				AllBadEnds.Add (Scanarios[i].ПоследствиеНЕвыполненияДействий[z]);
			}
			
			if (Scanarios[i].isFinished==true)
			{
				AllHappyEnds.Add (Scanarios[i].ПоследствиеВыполнения);
			}
			else
			{
				AllBadEnds.Add (Scanarios[i].ПоследствиеНЕвыполнения);
			}
		}
		*/
		
		//Теперь пишем все в файл
		{
			//показываем диалог с количеством ответов и ошибками
			
			Report_text.text = "Отчет о выполнении тренажера - " + НазваниеТренажера + "." + System.Environment.NewLine;
			//Report_text.text += "ФИО:"  + FIO.text +System.Environment.NewLine;
			//Report_text.text += "Должность:"  + TYPE.text +System.Environment.NewLine;
			//Report_text.text += "Подразделение:"  + ORG.text +System.Environment.NewLine;
			Report_text.text += "Время выполнения: " + duration + System.Environment.NewLine;
			Report_text.text += System.Environment.NewLine;
			
			Report_text.text += "Основные показатели:" + System.Environment.NewLine;
			Report_text.text += "Возможных нарушений было замечено: " +НарушенийБылоЗамечено.ToString() + System.Environment.NewLine;
			Report_text.text += "Возможных нарушений не было замечено: " + НарушенийНебылоЗамечено.ToString() + System.Environment.NewLine;

			//Report_text.text += "Из них исправлено: " +ИзНихИсправлено.ToString() + System.Environment.NewLine;
			//Report_text.text += "Из них не исправлено: " +ИзНихНеИсправлено.ToString() + System.Environment.NewLine;
			
			
			Report_text.gameObject.SetActive(false);
			Report_text.gameObject.SetActive(true);
			
			if (ReportScrollForFix!=null)
			{
				ReportScrollForFix.SetActive(false);
				ReportScrollForFix.SetActive(true);
			}

			/*
			if (isExam==true)
			{
				Report_text.text += System.Environment.NewLine;
				Report_text.text += "Детальный отчет по ответам...:" + System.Environment.NewLine;
				for (int z = 0; z < ДетальныйОтчетПоВопросам.Count ; z++)
				{
					Report_text.text += ДетальныйОтчетПоВопросам[z] + System.Environment.NewLine;
				}
				
				Report_text.text += System.Environment.NewLine;
				Report_text.text += "К чему привели устраненные нарушения...:" + System.Environment.NewLine;
				for (int z = 0; z < AllHappyEnds.Count ; z++)
				{
					Report_text.text += AllHappyEnds[z] + System.Environment.NewLine;
				}
				
				Report_text.text += System.Environment.NewLine;
				Report_text.text += "К чему привели неверно устраненные и незамеченные нарушения ...:" + System.Environment.NewLine;
				for (int z = 0; z < AllBadEnds.Count ; z++)
				{
					Report_text.text += AllBadEnds[z] + System.Environment.NewLine;
				}
			}
			*/

			if (SaveReportToFile == true)
			{
				if (isExam == true)
				{
					string datetime2 = theTime.ToString("yyyy-MM-dd-HH-mm-ss");
					string reportfile = System.Environment.GetFolderPath(System.Environment.SpecialFolder.Desktop) + "/" + datetime2 + ".txt";
					if (File.Exists(reportfile))
					{
						File.Delete(reportfile);
					}
					var sr = File.CreateText(reportfile);
					sr.WriteLine(Report_text.text);
					FileInfo fInfo = new FileInfo(reportfile);
					fInfo.IsReadOnly = true;
					sr.Close();
				}
			}
			
	 
		}
			
		Balls.SetActive(false);
		Report.SetActive(true);	
			
	}
	
	
	public void StartInEducationMode()
	{
		isExam=false;
		
		theStartTime = System.DateTime.Now;
		BlockPanel.SetActive(false);
		
		for (int i = 0; i < Scanarios.Count ; i++)
		{
			Scanarios[i].SetExamMode(false);
			Scanarios[i].ManualStart();
		}
		
		coroutine = WaitAndPrint(1.0f);
        StartCoroutine(coroutine);
	}
	
	public void StartInExamMode()
	{
		isExam=true;
		theStartTime = System.DateTime.Now;
		BlockPanel.SetActive(false);
		 
		for (int i = 0; i < Scanarios.Count ; i++)
		{
			Scanarios[i].SetExamMode(true);
			Scanarios[i].ManualStart();
		}
		
		coroutine = WaitAndPrint(1.0f);
        StartCoroutine(coroutine);
	}
	
	void MakeList(Transform child_)
	{
		foreach (Transform child in child_)
		{
			ScenarioEditor temp = child.gameObject.GetComponent<ScenarioEditor>();
			if (temp != null)
			Scanarios.Add (temp);
		}
	}
	
	void Start()
	{
		if (AutoStartInEducationMode == true) StartInEducationMode();
	}
	
	void OnSceneLoaded(Scene scene, LoadSceneMode mode)
    {
        //Debug.Log("OnSceneLoaded: " + scene.name);
        //Debug.Log(mode);
		MakeList (this.transform);
    }
	
	// called first
    void OnEnable()
    {
        SceneManager.sceneLoaded += OnSceneLoaded;
    }
	void OnDisable()
    {
        SceneManager.sceneLoaded -= OnSceneLoaded;
    }
	
	// every 2 seconds perform the print()
    private IEnumerator WaitAndPrint(float waitTime)
    {
		bool allFinished = false;
		
        while (allFinished==false)
        {
            yield return new WaitForSeconds(waitTime);
			int Steps=0;
			int Errors=0;
			int sdelal = 0;

			allFinished = true;
			for (int i = 0; i < Scanarios.Count ; i++)
			{
				Steps+=Scanarios[i].ВсегоДействий;
				Errors+=Scanarios[i].ВсегоОшибок;
				if (Scanarios[i].isFinished==false) allFinished = false;
				if (Scanarios[i].isFinished == true) sdelal++;

			}
			
			if (allFinished==false)
			{
				//показываем диалог с количеством ответов и ошибками

				if (isExam == false)
				{
					Balls.SetActive(true);
				}
				
				string errors_correct=" ошибок. Выполнили ";
				string steps_correct=" действий.";
				
				if (Errors==0) errors_correct = " ошибок. Выполнили ";
				if (Errors==1) errors_correct = " ошибку. Выполнили ";
				if (Errors==2) errors_correct = " ошибки. Выполнили ";
				if (Errors==3) errors_correct = " ошибки. Выполнили ";
				if (Errors==4) errors_correct = " ошибки. Выполнили ";
				if (Errors>=5) errors_correct = " ошибок. Выполнили ";
				
				if (sdelal == 0) steps_correct = " возможных нарушений из ";
				if (sdelal == 1) steps_correct = " возможное нарушение из ";
				if (sdelal == 2) steps_correct = " возможных нарушения из ";
				if (sdelal == 3) steps_correct = " возможных нарушений из ";
				if (sdelal == 4) steps_correct = " возможных нарушений из ";
				if (sdelal >= 5) steps_correct = " возможных нарушений из ";

				int vsego = 0;
				//if (Manual_ВсегоДействий==0)
                //{
					vsego = Scanarios.Count;
				//}
				//else
				// {
				//	vsego = Manual_ВсегоДействий;
				//}
				Balls_text.text = "Вы обнаружили " + sdelal.ToString()  + steps_correct + vsego.ToString() + "." + System.Environment.NewLine +  "Количество ошибок - " + Errors.ToString();
			}
			else
			{

				FinishPress();
			}
        }
    }
	
}


