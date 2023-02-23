//https://github.com/cfoulston/Unity-Reorderable-List

using UnityEngine;
using UnityEngine.UI;
using System.Collections;
using System.Collections.Generic;
using UnityEngine.AI;

//звук

public class Scenario_step_HumanGoTo : MonoBehaviour 
{
	//ссылка на сценарий, там все элементы и текст и меню и все остальное
	//там же метод Next, когда сценарий закончился...
	private ScenarioEditor editor = null;
	
	public float RemainingDistance =0.5f;
	
	public Transform[] points;
    private int destPoint = 0;
    public NavMeshAgent agent;
	
	public Animator anim;
	
	public AudioSource stepsound;
	
	public string text_for_notify = "Необходимо подождать пока персонал займет нужную позицию.";
	
	//тип 
	public StepEnum Ждем_Окончания;
	public enum StepEnum {
		Да,	
		Нет
	}
	Coroutine lastRoutine = null;
	
	bool isStairMode = false;
		
	
	//настройка, привязываем обработчики
	public void Setup(ScenarioEditor _editor)
	{
		destPoint=0;
		editor = _editor;


		agent.isStopped = true;
		agent.velocity = Vector3.zero;
		agent.Stop();
		agent.ResetPath();

		agent.autoBraking = false;
		agent.isStopped = false;
		agent.stoppingDistance = 0;
		
        GotoNextPoint();
		
		if (Ждем_Окончания==StepEnum.Нет)
		{
			OK();
		}
		else
		{
			if (text_for_notify!="")
			{
				editor.Напоминалка.SetActive(true);
				editor.Напоминалка_текст.text = text_for_notify;
			}
			else
			{
				editor.Напоминалка.SetActive(false);
				editor.Напоминалка_текст.text = "";
			}
		}
	}
	
		
	void GotoNextPoint() 
	{
		// Returns if no points have been set up
		if (points.Length == 0)
		{
			return;
		}

		

		// Choose the next point in the array as the destination,
		if (destPoint<points.Length)
		{
			NavMeshPath navMeshPath = new NavMeshPath();
			agent.CalculatePath(points[destPoint].position, navMeshPath);
			
			//проверка
			bool navMeshPathStatus = false;
			bool navMeshSamplePosition = false;
			if (navMeshPath.status != NavMeshPathStatus.PathComplete)
			{
				navMeshPathStatus = false;
			}
			else
			{
				navMeshPathStatus = true;
			}
			NavMeshHit hit;
			if (NavMesh.SamplePosition(points[destPoint].position, out hit, RemainingDistance, NavMesh.AllAreas))
			{
				navMeshSamplePosition = true;
			}
			else
			{
				navMeshSamplePosition = false;
			}
	 
			//смотри проверку ниже
			//
			if ((navMeshPathStatus==false) ) //|| (navMeshSamplePosition==false)
			{
				Debug.Log("WRONG WAY !");
				anim.Play("IDLE", -1, 0);
				agent.isStopped = true;
				stepsound.Pause();
				
				agent.nextPosition = points[destPoint].position;
				destPoint++;
				GotoNextPoint();
				
				/*
				if (Ждем_Окончания==StepEnum.Да)
				{
				this.gameObject.SetActive(false);
				OK();
				}
				*/
			}
			else 
			{
				stepsound.Play();
				anim.Play("WALK", -1, 0);
				// Set the agent to go to the currently selected destination.
				agent.destination = points[destPoint].position;
				destPoint++;
			}
		}
		else
		{
			anim.Play("IDLE", -1, 0);
			agent.isStopped = true;
			stepsound.Pause();
			
			if (Ждем_Окончания==StepEnum.Да)
			{
				this.gameObject.SetActive(false);
				OK();
			}
			
			if (Ждем_Окончания==StepEnum.Нет)
			{
				this.gameObject.SetActive(false);
				//editor=null;
			}

		}
	}
		
	void Update () 
	{
		if (editor==null) return;
		//if (agent.isStopped == true) return;
		

		if (agent.isOnOffMeshLink) 
		{
			if (isStairMode==false)
			{
				Debug.Log ("Лестница началась!!!!");
				string linkType = agent.currentOffMeshLinkData.linkType.ToString();
				Debug.Log (linkType);
				isStairMode=true;
			}
		}
		else
		{
			if (isStairMode==true)
			{
				Debug.Log ("Лестница закончилась!!!!");
				isStairMode=false;
			}
		}
		
		
		
		
		//Путь в ожидании == false   &&  Оставшееся расстояние < .....
		if ((!agent.pathPending) &&(agent.remainingDistance < RemainingDistance))
		{
			GotoNextPoint();
			return;
		}
		else if (agent.path.status!=null)
		{
			if (agent.path.status == NavMeshPathStatus.PathInvalid || agent.path.status == NavMeshPathStatus.PathPartial) 
			{
				Debug.Log("WRONG WAY");
				GotoNextPoint();
				return;
			}
			//fix
			//путь не может быть закончен
			//if(NavAgent.isPathStale)
			if(agent.isPathStale==true)
			{
				if (agent.pathStatus != NavMeshPathStatus.PathComplete)
				{                      
					if (agent.pathStatus != NavMeshPathStatus.PathInvalid)
					{
						Debug.Log("WRONG WAY22222");
						GotoNextPoint();
						return;
					}
				}
			}
		}
		//
		
		


	}
	
	
	
	
	
	//когда нажали на ОК...
	public void OK()
	{
		//
		editor.Напоминалка.SetActive(false);

		//this.gameObject.SetActive(false);
		//никакие аргументы не передаем в Editor, типа правильно/неправильно

		//
		System.DateTime theTime = System.DateTime.Now;
		string datetime = theTime.ToString("yyyy-MM-dd\\ HH:mm:ss\\ ");

		ReportStorageStepClass temp = new ReportStorageStepClass();
		temp.guid_id = System.Guid.NewGuid().ToString();
		temp.definition_description = "Один из персонажей перешел на необходимую позицию " + agent.gameObject.name;
		temp.datatime_real = datetime;
		temp.datatime_simulation = datetime;
		temp.type = "Scenario_step_HumanGoTo";
		temp.completed = 1f;
		temp.passed = 1f;
		temp.categoty = "";
		editor.ReportStorage.ReportStorageStepsList.Add(temp);
		//



		//посылаем команду на следующий шаг
		editor.StepFinish();
		//editor=null;
	}
	
}


/*
				if(NavAgent.isPathStale)
                {
                    if (NavAgent.pathStatus != NavMeshPathStatus.PathComplete)
                    {                      
                        if (NavAgent.pathStatus != NavMeshPathStatus.PathInvalid)
                        {
                            Vector3 randomVector = Random.onUnitSphere;
                            Vector3 position = PathfindTarget - randomVector;
                            position.y = PathfindTarget.y;
                            NavMeshHit hit;
                            if(NavMesh.SamplePosition(position,out hit,1,1))
                            {
                                Move (position);
                            }
                            else Move(transform.position) //reset move
                        }
                        else
                        {
                            Move (PathfindTarget);
                        }
                    }                  
                }
				//agent.destination = goal.position; 

*/







/*
OffMeshLink

// AgentLinkMover.cs
using UnityEngine;
using System.Collections;
 
public enum OffMeshLinkMoveMethod {
   Teleport,
   NormalSpeed,
   Parabola,
   Curve
}
 
[RequireComponent (typeof (NavMeshAgent))]
public class AgentLinkMover : MonoBehaviour {
   public OffMeshLinkMoveMethod method = OffMeshLinkMoveMethod.Parabola;
   public AnimationCurve curve = new AnimationCurve ();
   IEnumerator Start () {
     NavMeshAgent agent = GetComponent<NavMeshAgent> ();
     agent.autoTraverseOffMeshLink = false;
     while (true) {
       if (agent.isOnOffMeshLink) {
         if (method == OffMeshLinkMoveMethod.NormalSpeed)
           yield return StartCoroutine (NormalSpeed (agent));
         else if (method == OffMeshLinkMoveMethod.Parabola)
           yield return StartCoroutine (Parabola (agent, 2.0f, 0.5f));
         else if (method == OffMeshLinkMoveMethod.Curve)
           yield return StartCoroutine (Curve (agent, 0.5f));
         agent.CompleteOffMeshLink ();
       }
       yield return null;
     }
   }
   IEnumerator NormalSpeed (NavMeshAgent agent) {
     OffMeshLinkData data = agent.currentOffMeshLinkData;
     Vector3 endPos = data.endPos + Vector3.up*agent.baseOffset;
     while (agent.transform.position != endPos) {
       agent.transform.position = Vector3.MoveTowards (agent.transform.position, endPos, agent.speed*Time.deltaTime);
       yield return null;
     }
   }
   IEnumerator Parabola (NavMeshAgent agent, float height, float duration) {
     OffMeshLinkData data = agent.currentOffMeshLinkData;
     Vector3 startPos = agent.transform.position;
     Vector3 endPos = data.endPos + Vector3.up*agent.baseOffset;
     float normalizedTime = 0.0f;
     while (normalizedTime < 1.0f) {
       float yOffset = height * 4.0f*(normalizedTime - normalizedTime*normalizedTime);
       agent.transform.position = Vector3.Lerp (startPos, endPos, normalizedTime) + yOffset * Vector3.up;
       normalizedTime += Time.deltaTime / duration;
       yield return null;
     }
   }
   IEnumerator Curve (NavMeshAgent agent, float duration) {
     OffMeshLinkData data = agent.currentOffMeshLinkData;
     Vector3 startPos = agent.transform.position;
     Vector3 endPos = data.endPos + Vector3.up*agent.baseOffset;
     float normalizedTime = 0.0f;
     while (normalizedTime < 1.0f) {
       float yOffset = curve.Evaluate (normalizedTime);
       agent.transform.position = Vector3.Lerp (startPos, endPos, normalizedTime) + yOffset * Vector3.up;
       normalizedTime += Time.deltaTime / duration;
       yield return null;
     }
   }
}
*/