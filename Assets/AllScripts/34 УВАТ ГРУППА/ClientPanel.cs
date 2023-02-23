
using UnityEngine;
using UnityEngine.UI;
using TMPro;

public class ClientPanel : MonoBehaviour 
{
	public TextMeshProUGUI DetailText;
	public Slider Progress;


	void Start ()
	{
		SetProgress(0,0,0);
	}

	public void SetProgress (int ОбнаруженныеДействия, int ОбнаруженныеУсловия, int Ошибки)
	{
		Progress.value = ((float)ОбнаруженныеДействия + (float)ОбнаруженныеУсловия) / 44f;
		DetailText.text = ОбнаруженныеДействия.ToString() +  " / " + ОбнаруженныеУсловия.ToString() + " / " + Ошибки.ToString();
	}
	
	
}
