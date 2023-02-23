/// Credit mgear, SimonDarksideJ
/// Sourced from - https://forum.unity3d.com/threads/radial-slider-circle-slider.326392/#post-3143582
/// Updated to include lerping features and programmatic access to angle/value

using System;
using UnityEngine.Events;
using UnityEngine.EventSystems;
using UnityEngine;
using System.Collections;
using UnityEngine.UI;

[System.Serializable]
public class Gnvp_FloatEvent : UnityEvent<float>
{
}


[RequireComponent(typeof(Image))]
public class RadialSlider : MonoBehaviour, IPointerEnterHandler, IPointerDownHandler, IPointerUpHandler, IDragHandler
{
	public Gnvp_FloatEvent m_MyEvent;
	
	private bool isPointerDown, isPointerReleased;
	public Image RadialImage;
	
	public float min=0f;
	public float max=360f;
	public float angle=0;
	public float turns=0;
	
	float xxx,yyy;
	float previus_angle=0;

	private void Update()
	{
		if (isPointerDown)
		{
			UpdateRadialImage();
		}
	}

	void Start()
	{
		if (m_MyEvent == null)
		m_MyEvent = new Gnvp_FloatEvent();
	
		//UpdateRadialImage();
		RectTransform  rt = GetComponent<RectTransform>();
		Vector3[] v = new Vector3[4];
        rt.GetWorldCorners(v);

        xxx = v[0].x + (v[3].x-v[0].x)/2f;
		yyy = v[3].y + (v[2].y-v[0].y)/2f;;
	}

	
	
	private void UpdateRadialImage()
	{
		float a = Mathf.Atan2(Input.mousePosition.y - yyy, Input.mousePosition.x - xxx) *  Mathf.Rad2Deg + 180f;

		if(a < 0)
		{
			a += 360f;
		}
		
		
		
		if (Mathf.Abs(a - previus_angle)>300f)
		{
			if (previus_angle<a)
			{
				turns-=1f;
			}
			else
			{
				turns+=1f;
			}
		}
		
		previus_angle =a;
		
		
		
		angle = turns*360f + a;
		
		if ((angle>=min)&&(angle<=max))
		{
			RadialImage.transform.rotation = Quaternion.Euler (0, 0, a);
		}
		
		
		if (angle<min) 
		{
			RadialImage.transform.rotation = Quaternion.Euler (0, 0, 0);
			angle=min;
			previus_angle=0;
			turns=0f;
		}
		if (angle>max)
		{
			RadialImage.transform.rotation = Quaternion.Euler (0, 0, 0);
			angle=max;
			previus_angle=0;
			turns=10f;
		}	
		
		
		m_MyEvent.Invoke(angle);
	}



	#region Interfaces
	// Called when the pointer enters our GUI component.
	// Start tracking the mouse
	public void OnPointerEnter(PointerEventData eventData)
	{
		
	}

	public void OnPointerDown(PointerEventData eventData)
	{
		isPointerDown = true;
	}

	public void OnPointerUp(PointerEventData eventData)
	{
		isPointerDown = false;
		isPointerReleased = true;
	}

	public void OnDrag(PointerEventData eventData)
	{
	}
	#endregion
}
