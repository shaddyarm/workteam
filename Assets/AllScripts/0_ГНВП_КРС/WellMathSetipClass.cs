using UnityEngine;
using System.Collections;
using UnityEngine.UI;

public class WellMathSetipClass : MonoBehaviour
{
	public WellMathClass well;
	
	public InputField H;
	public InputField D;
	public InputField Pzab;
	public InputField Ro_water;
	public InputField waterCoeff; // /100
	public InputField Dnkt;
	public InputField P_false_1;
	public InputField P_false_2;
	
	//кинематическая вязкость воды, м2/сек
	public InputField v_koeff;
	//10 сек  до полного давления на закрытом дросселе
	public InputField DempherTime;
	
	public void Initialization()
	{
		well.H_well = float.Parse(H.text);
		well.D_well = float.Parse(D.text);
		well.P_down = float.Parse(Pzab.text);
		well.standart_Ro_water = float.Parse(Ro_water.text);
		well.D_nkt = float.Parse(Dnkt.text);
		well.P_false_1 = float.Parse(P_false_1.text);
		well.P_false_2 = float.Parse(P_false_2.text);
		
		well.v_koeff = float.Parse(v_koeff.text);
		well.DempherTime = float.Parse(DempherTime.text);
		
		well.Initialization();
	}
	
	public void ReCalc()
	{
		float _H = float.Parse(H.text);
		float g = 9.81f;
		float _Ro = float.Parse(Ro_water.text);
	
		float NewP = _Ro * g * _H ; //+ 100000f
		Pzab.text = NewP.ToString("G17");
	}
	
	void Start()
	{
		ReCalc();
	}
	
	
}