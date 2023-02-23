using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.EventSystems;
using UnityEngine.Events;

public class ButtonUI1 : MonoBehaviour, IPointerDownHandler, IPointerUpHandler
{

    public UnityEvent MyEventDown;
    public UnityEvent MyEventUp;


    public bool repeat = false;



    bool _pressed = false;
    public void OnPointerDown(PointerEventData eventData)
    {
        _pressed = true;
        MyEventDown.Invoke();
    }

    public void OnPointerUp(PointerEventData eventData)
    {
        _pressed = false;
        MyEventUp.Invoke();
    }

   // public void  OnPointerExit(PointerEventData eventData)
    //{
        //
    //}

    void Update()
    {
        if ((_pressed==true)&&(repeat)) MyEventDown.Invoke();
        if ((_pressed==false) && (repeat)) MyEventUp.Invoke();
    }

}
