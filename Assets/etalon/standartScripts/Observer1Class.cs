using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using System.IO;
using System;

public class Observer1Class : MonoBehaviour 
{

	
	public xAPI SCORM;
	
	public ToMainMenuClass breaker;
	
	
	public string НАЗВАНИЕ;
	
	public bool nextOK;
	
	public string mode;
	public List <Element1Class> elements;
	
	public bool ЭКЗАМЕН;
	
	//Баллы 
	public GameObject textPanel;
	public Text text;
	
	
	//Верхнее меню...
	public GameObject textPanelWelcome;
	public Text textWelcome;
	public String EducationWelcomeText = "Изучение конструкции - обучение. Для изучения конструкции нажимайте активные зоны (красные сферы). При этом будет озвучено название элемента, даны необходимые текстовые пояснения. ";
	public String ExamWelcomeText = "Изучение конструкции - экзамен. После заданного вопроса, Вам нужно выбрать требуемый элемент конструкции. В случае правильного выбора с первого раза, Вы получаете +1 балл. При неправильном выборе, баллы не присуждаются, будет предложено выбирать варианты, пока не выберите правильный вариант. При правильном выборе не с первого раза, также баллы не присуждаются. ";
	
	int ПравильныйИндекс;
	
	int balls;
	
	float timeElapsed;
	
	bool active =false;
	
	
	bool fakeAnswer=false;
	
	
	
	public void Reset()
	{
		Start () ;
		active=false;
		fakeAnswer=false;
		
	}
	
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
		
	public void SetMode(string _mode)
	{
		mode=_mode;
		balls=0;
		
		nextOK=true;
		
		timeElapsed=0;
		
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
		
		if (mode=="КОНСТРУКЦИЯ")
		{
				
			textPanel.SetActive(true);

			foreach (Element1Class child in elements)
			{
				child.show();
			}
				
			if (ЭКЗАМЕН==true)
			{
				breaker.isEkzamen=true;
				//экзамен.interactable  =false;
				//экзамен.isOn =false;
				
				ПравильныйИндекс=0;
				elements[0].ShowQuestion();
			}
		}
	}
		
	void Start () 
	{
		nextOK=true;
		ПравильныйИндекс=0;
		balls=0;
		text.text="";
		
		textPanel.SetActive(false);
		mode="";
		foreach (Element1Class child in elements)
		{
			child.hide();
			child.Reset();
		}
		
	}
	
	// Update is called once per frame
	void Update () 
	{
		if (mode=="") return;
		
		timeElapsed+=Time.deltaTime;
	}
	
	
	//нажали на элемент с ID, нужно решить что делать с ним (ОДНОзначно режим экзамена)
	public void Press (string ID)
	{
		if (ЭКЗАМЕН==true)
		{
			if (ID==elements[ПравильныйИндекс].ID)
			{
				if (fakeAnswer==false)
				{
					balls++;	
				}
				else
				{
					fakeAnswer=false;
				}
				
				elements[ПравильныйИндекс].hide();
				//если это был последний вопрос - все
				if (ПравильныйИндекс==(elements.Count-1))
				{
					float z = (float)balls / (float)elements.Count * 100f;
					string temp = " (" +  z.ToString("N0") + "% правильных ответов)";
	
					
					
					text.text = "Ваш результат: " + balls.ToString("N0") + " из " + elements.Count.ToString("N0") + temp;
					breaker.isEkzamen=false;
					WriteReport();
					
				}
				else
				{
					ПравильныйИндекс++;
					elements[ПравильныйИндекс].ShowQuestion();
					text.text = "Баллы: " + balls.ToString("N0") + " из " + elements.Count.ToString("N0");
				}
				
			}
			else
			{
				fakeAnswer=true;
				//balls--;
				//if (balls<0) balls=0;
				text.text = "Баллы: " + balls.ToString("N0") + " из " + elements.Count.ToString("N0");
			}
			
			
		}
		else
		{
			balls++;
			text.text = "Изучено: " + balls.ToString("N0") + " из " + elements.Count.ToString("N0");
		}
	}
	
	void WriteReport()
	{
		
		if (SCORM!=null)
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
		
		
	}
		
}


/*
cmi.score.min
0
cmi.score.max
100
cmi.score.raw
100
cmi.score.scaled
1
cmi.progress_measure
1
cmi.success_status
passed
cmi.completion_status
completed
cmi.interactions.0.id
Step1
cmi.interactions.0.description
17:14:28	Произвести аварийный останов работающих компрессорных станций.
cmi.interactions.0.result
correct
cmi.interactions.1.id
Step22
cmi.interactions.1.type
fill-in
cmi.interactions.1.objectives.0.id
urn:ADL:objectiveid-0001
cmi.interactions.1.description
privet
cmi.interactions.1.learner_response
privet
cmi.interactions.1.timestamp
2005-10-11T09:00:30
cmi.interactions.1.correct_responses.0.pattern
privet
cmi.interactions.1.weighting
1
cmi.interactions.1.result
unanticipated
cmi.interactions.1.latency
PT0H0M5.0S
cmi.interactions.2.id
Step3
cmi.interactions.2.type
fill-in
cmi.interactions.2.objectives.0.id
urn:ADL:objectiveid-0001
cmi.interactions.2.description
{lang=ru}Нужно-ли сообщить оператору ПУ об аварии
cmi.interactions.2.learner_response
{lang=ru}Нет
cmi.interactions.2.timestamp
2005-10-11T09:00:30
cmi.interactions.2.correct_responses.0.pattern
{lang=ru}Да
cmi.interactions.2.weighting
1
cmi.interactions.2.result
incorrect
cmi.interactions.2.latency
PT0H0M5.0S
cmi.interactions.3.id
Step4
cmi.interactions.3.type
fill-in
cmi.interactions.3.objectives.0.id
urn:ADL:objectiveid-0001
cmi.interactions.3.description
{lang=ru}Следующий шаг - оповестить об аварии криком
cmi.interactions.3.learner_response
{lang=ru}Оповестить работающих на объекте криком об аварии
cmi.interactions.3.timestamp
2005-10-11T09:00:30
cmi.interactions.3.correct_responses.0.pattern
{lang=ru}Оповестить работающих на объекте криком об аварии
cmi.interactions.3.weighting
1
cmi.interactions.3.result
correct
cmi.interactions.3.latency
PT0H0M5.0S
cmi.interactions.4.id
Step5
cmi.interactions.4.type
fill-in
cmi.interactions.4.objectives.0.id
urn:ADL:objectiveid-0001
cmi.interactions.4.description
{lang=ru}Следующий шаг - уйти с куста
cmi.interactions.4.learner_response
{lang=ru}Зайти на куст
cmi.interactions.4.timestamp
2005-10-11T09:00:30
cmi.interactions.4.correct_responses.0.pattern
{lang=ru}Следующий шаг - уйти с куста
cmi.interactions.4.weighting
1
cmi.interactions.4.result
neutral
cmi.interactions.4.latency
PT0H0M5.0S
cmi.comments_from_learner.0.comment
Привет, как дела?
cmi.comments_from_learner.1.comment
Хорошо!
cmi.session_time
8
cmi.exit
normal

*/

/*

АВАРИЯ

Какая аварийная ситуация произошла? 
	Разгерметизация запорной арматуры
	

Окриком предупредить людей находящихся в опасной зоне
	Произошла авария! Разгерметизация РВС. Покинуть территорию резервуарного парка

Оповестить по рации начальника смены ЦППН
	Оператор товарный Ивнов, произошла аварийная ситуация: разгерметизация РВС, не герметичность фланцевого соединения запорной арматуры
	
Выставить аншлаги «Огнеопасно - Газ», «Проход запрещен»

Подготовить первичные средства пожаротушения

Закрыть запорную арматуру на входе жидкости в РВС

Раскачайте жидкость из резервуара на ЦПС

Уровень в резервуаре номер один минимальный
Выполнять действия согласно плана ликвидации аварий

Жидкость из РВС направить в дренаж

Произвести замеры загазованности вблизи места аварии

Сообщить результаты анализа воздушной среды результаты анализа воздушной среды начальнику ЦППН

Ожидать прибытие ремонтной бригады для ликвидации последствий аварии
	

*/

