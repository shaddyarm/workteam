using UnityEditor;
 using UnityEngine;
 using System.IO;
 using System.Linq;
 using System.Collections;
 
 [ExecuteInEditMode]
 public class MakeTranslateFile : EditorWindow 
 {
//     [SerializeField] string lastScene="";
	 
	 
	[MenuItem("Window/ClearAllTranslate")]
	public static void ClearAllTranslate() 
	{
	Debug.Log ("Clear TranslateFile!");
	 
	
	//Сценарии
	{
		ScenarioEditor[] scenarions = Resources.FindObjectsOfTypeAll<ScenarioEditor>();	
		foreach (ScenarioEditor scenario in scenarions)
		{
			scenario.translateID="";

		}
	}
	//Тексты сценария
	{
		Scenario_step_text[] texts = Resources.FindObjectsOfTypeAll<Scenario_step_text>();	
		foreach (Scenario_step_text text in texts)
		{
			text.translateID="";
		}
	}
	//Вопросы сценария
	{
		Scenario_step_question[] questions = Resources.FindObjectsOfTypeAll<Scenario_step_question>();	
		foreach (Scenario_step_question question in questions)
		{
			question.translateID="";
			
		}
	}
	Debug.Log ("Clear finish!");
	}

	

	[MenuItem("Window/ImportAndMergeTranslateFile _F11")]
	public static void LoadAndMergeTranslateFile() 
	{
		//EditorWindow.GetWindow<PlayFromScene>();
		Debug.Log ("LoadTranslateFile!");
		 
		string path = EditorUtility.OpenFilePanel("Open tr file", "", "tr");
		
		if (path.Length == 0) return;
		if (System.IO.File.Exists(path)==false) return;
		
		string JSONstring = System.IO.File.ReadAllText(path);
		if (JSONstring=="") return;
		
		SimpleJSON.JSONNode data = SimpleJSON.JSON.Parse(JSONstring);
		
		//Сценарии
		{
			ScenarioEditor[] scenarions = Resources.FindObjectsOfTypeAll<ScenarioEditor>();	
			foreach (ScenarioEditor scenario in scenarions)
			{
				if (scenario.translateID!="")
				{
					foreach(SimpleJSON.JSONNode record in data[scenario.translateID])
					{
						if (record["Название"].Value!="") scenario.gameObject.name = record["Название"].Value;
						
						scenario.ПричинаВыполнения = record["ПричинаВыполнения"].Value;
						scenario.ПоследствиеВыполнения = record["ПоследствиеВыполнения"].Value;
						scenario.ПричинаНЕвыполнения = record["ПричинаНЕвыполнения"].Value;
						scenario.ПоследствиеНЕвыполнения = record["ПоследствиеНЕвыполнения"].Value;
					}
				}
			}
		}
		//Тексты сценария
		{
			Scenario_step_text[] texts = Resources.FindObjectsOfTypeAll<Scenario_step_text>();	
			foreach (Scenario_step_text text in texts)
			{
				if (text.translateID!="")
				{
					foreach(SimpleJSON.JSONNode record in data[text.translateID])
					{
						text.Message = record["Сообщение"].Value;
					}
				}
			}
		}
		//Вопросы сценария
		{
			Scenario_step_question[] questions = Resources.FindObjectsOfTypeAll<Scenario_step_question>();	
			foreach (Scenario_step_question question in questions)
			{
				if (question.translateID!="")
				{
					foreach(SimpleJSON.JSONNode record in data[question.translateID])
					{
						question.Question = record["Question"].Value;
						question.AnswerCorrect = record["AnswerCorrect"].Value;
						question.AnswerIncorrect1 = record["AnswerIncorrect1"].Value;
						question.AnswerIncorrect2 = record["AnswerIncorrect2"].Value;
						question.AnswerIncorrect3 = record["AnswerIncorrect3"].Value;
						question.ПричинаВерногоОтвета = record["ПричинаВерногоОтвета"].Value;
						question.ПоследствиеВерногоОтвета = record["ПоследствиеВерногоОтвета"].Value;
						question.ПричинаОшибочногоОтвета = record["ПричинаОшибочногоОтвета"].Value;
						question.ПоследствиеОшибочногоОтвета = record["ПоследствиеОшибочногоОтвета"].Value;
					}
				}
			}
		}
		Debug.Log ("Load and merge TranslateFile finish!");
	}


 
     [MenuItem("Window/ExportTranslateFile _F12")]
     public static void Apply() 
	 {
		//EditorWindow.GetWindow<PlayFromScene>();
		Debug.Log ("MakeTranslateFile!");
		 
		string path = EditorUtility.SaveFilePanel("Save tr", "xxxx", "test.tr","tr");
        if (path.Length != 0)
        {
			//SimpleJSON.JSONNode data = SimpleJSON.JSON.Parse("{}");
			//data[scenario.translateID]["Название"] = scenario.gameObject.name; 
			
			string ALL="";
			
			ALL+="{"+System.Environment.NewLine;
			
			int I=0;
			
			{
				ScenarioEditor[] scenarions = Resources.FindObjectsOfTypeAll<ScenarioEditor>();	
				foreach (ScenarioEditor scenario in scenarions)
				{
					try 
					{
						int result = System.Convert.ToInt32(scenario.translateID);
						if (I<result) I=result;
					}
					catch (System.OverflowException) 
					{

					}
					catch (System.FormatException) 
					{

					}
				}
				Scenario_step_text[] texts = Resources.FindObjectsOfTypeAll<Scenario_step_text>();	
				foreach (Scenario_step_text text in texts)
				{
					try 
					{
						int result = System.Convert.ToInt32(text.translateID);
						if (I<result) I=result;
					}
					catch (System.OverflowException) 
					{

					}
					catch (System.FormatException) 
					{

					}
				}
				Scenario_step_question[] questions = Resources.FindObjectsOfTypeAll<Scenario_step_question>();	
				foreach (Scenario_step_question question in questions)
				{
					try 
					{
						int result = System.Convert.ToInt32(question.translateID);
						if (I<result) I=result;
					}
					catch (System.OverflowException) 
					{

					}
					catch (System.FormatException) 
					{

					}
				}
				I++;
			}
			

			{
				ScenarioEditor[] scenarions = Resources.FindObjectsOfTypeAll<ScenarioEditor>();	
				foreach (ScenarioEditor scenario in scenarions)
				{
					//создаем автоматически
					if (scenario.translateID=="")
					{
						scenario.translateID = I.ToString();
						I++;
					}
						
					if (scenario.translateID!="")
					{
						ALL+= '"' + scenario.translateID + '"' + ":" +  System.Environment.NewLine;
						ALL+= "[{" + System.Environment.NewLine;
						ALL+= '"' + "Название" + '"' + ":" + '"' + scenario.gameObject.name + '"' + ","+ System.Environment.NewLine;
						ALL+= '"' + "ПричинаВыполнения" + '"' + ":" + '"' + scenario.ПричинаВыполнения + '"' + ","+  System.Environment.NewLine;
						ALL+= '"' + "ПоследствиеВыполнения" + '"' + ":" + '"' + scenario.ПоследствиеВыполнения + '"' + ","+  System.Environment.NewLine;
						ALL+= '"' + "ПричинаНЕвыполнения" + '"' + ":" + '"' + scenario.ПричинаНЕвыполнения + '"' + ","+  System.Environment.NewLine;
						ALL+= '"' + "ПоследствиеНЕвыполнения" + '"' + ":" + '"' + scenario.ПоследствиеНЕвыполнения + '"' + ","+  System.Environment.NewLine;
						ALL+= "}]," +  System.Environment.NewLine;
					}
				}

				
				Scenario_step_text[] texts = Resources.FindObjectsOfTypeAll<Scenario_step_text>();	
				foreach (Scenario_step_text text in texts)
				{
					//создаем автоматически
					if (text.translateID=="")
					{
						text.translateID = I.ToString();
						I++;
					}
					
					if (text.translateID!="")
					{
						ALL+= '"' + text.translateID + '"' + ":" +  System.Environment.NewLine;
						ALL+= "[{" + System.Environment.NewLine;
						ALL+= '"' + "Сообщение" + '"' + ":" + '"' + text.Message + '"' + ","+ System.Environment.NewLine;
						ALL+= "}]," +  System.Environment.NewLine;
					}
				}
				
				Scenario_step_question[] questions = Resources.FindObjectsOfTypeAll<Scenario_step_question>();	
				foreach (Scenario_step_question question in questions)
				{
					
					//создаем автоматически
					if (question.translateID=="")
					{
						question.translateID = I.ToString();
						I++;
					}
					
					if (question.translateID!="")
					{
						ALL+= '"' + question.translateID + '"' + ":" +  System.Environment.NewLine;
						ALL+= "[{" + System.Environment.NewLine;
						ALL+= '"' + "Question" + '"' + ":" + '"' + question.Question + '"' + ","+  System.Environment.NewLine;
						ALL+= '"' + "AnswerCorrect" + '"' + ":" + '"' + question.AnswerCorrect + '"' + ","+  System.Environment.NewLine;
						ALL+= '"' + "AnswerIncorrect1" + '"' + ":" + '"' + question.AnswerIncorrect1 + '"' + ","+  System.Environment.NewLine;
						ALL+= '"' + "AnswerIncorrect2" + '"' + ":" + '"' + question.AnswerIncorrect2 + '"' + ","+  System.Environment.NewLine;
						ALL+= '"' + "AnswerIncorrect3" + '"' + ":" + '"' + question.AnswerIncorrect3 + '"' + ","+  System.Environment.NewLine;
						ALL+= '"' + "ПричинаВерногоОтвета" + '"' + ":" + '"' + question.ПричинаВерногоОтвета + '"' + ","+  System.Environment.NewLine;
						ALL+= '"' + "ПоследствиеВерногоОтвета" + '"' + ":" + '"' + question.ПоследствиеВерногоОтвета + '"' + ","+  System.Environment.NewLine;
						ALL+= '"' + "ПричинаОшибочногоОтвета" + '"' + ":" + '"' + question.ПричинаОшибочногоОтвета + '"' + ","+  System.Environment.NewLine;
						ALL+= '"' + "ПоследствиеОшибочногоОтвета" + '"' + ":" + '"' + question.ПоследствиеОшибочногоОтвета + '"' + ","+  System.Environment.NewLine;
						ALL+= "}]," +  System.Environment.NewLine;
					}
				}
			}

			if (System.IO.File.Exists(path))
			{
				System.IO.File.Delete(path);
			}
			var sr = System.IO.File.CreateText(path);
			sr.WriteLine(ALL);
			sr.Close();     
			
			
        }
		else
		{
			Debug.Log ("cancel");
		}
		
     }
     
 
     void OnEnable() 
	 {
         
     }
 
     void Update() 
	 {

     }
 
     void OnGUI() 
	 {
		 if(GUILayout.Button("Play")) 
		 {
             ///
         }
     }
	 
	 
 
    
 }