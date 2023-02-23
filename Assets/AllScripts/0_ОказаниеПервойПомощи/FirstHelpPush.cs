using UnityEngine;
using System.Collections;
using TMPro;
 
public class FirstHelpPush : MonoBehaviour
{
	public GameObject HeardButton;
	public GameObject HeardText;
	public TextMeshProUGUI HeardTextText;
	

    //public SkinnedMeshRenderer skinnedMeshRenderer;
    //private Mesh skinnedMesh;
	
	public Animator anim1;
	public string animation_name1;
	public Animator anim2;
	public string animation_name2;
	
	int count=0;
	
	public Scenario_Trigger trigger;

	private bool active=false;
	private bool play=false;
	
	float time_ = 0;
	
	
	public void SetActive(bool _value)
	{
		active=_value;
		
		HeardButton.SetActive(_value);
		HeardText.SetActive(_value);
		HeardTextText.text="";
		count=0;
		time_=0;
	}

    void Awake ()
    {
        //skinnedMesh = skinnedMeshRenderer.sharedMesh;
		//skinnedMeshRenderer.SetBlendShapeWeight (num, 100f);
		//int blendShapeCount; = skinnedMesh.blendShapeCount; 
		//Debug.Log ("blendShapeCount=" + blendShapeCount);
    }

    void Start ()
    {
        
    }

    void Update ()
    {
		if (active!=true) return;
		
		time_+=Time.deltaTime;
		
		if (Input.GetKeyDown(KeyCode.Return ))
        {
			ButtonPush();
        }
    }
	
	
	public void ButtonPush()
	{
		if (active!=true) return;
		
		play=true;
		
		anim1.Play(animation_name1, -1, 0);
		anim2.Play(animation_name2, -1, 0);
		
		count++;
		
		HeardTextText.text=count.ToString();
		
		if (count>=30)
		{
			
			float z = 30f *60f / time_; 
			active=false;
			HeardTextText.text = "Ваш темп = " +  z.ToString() +  " нажатий в минуту.";
			
			if (z<100f)
			{
				HeardTextText.text += " Слишком медленно.";
			}
			else if (z>110f)
			{
				HeardTextText.text += " Слишком быстро.";
			}
			else
			{
				HeardTextText.text += " Норма.";	
			}
			
			
			StartCoroutine(TestCoroutine());
		}
			
	}
	
	IEnumerator TestCoroutine()
	{
		yield return new WaitForSeconds(8);
		
		count=0;
		SetActive(false);
		trigger.On();
	}
	
	
}