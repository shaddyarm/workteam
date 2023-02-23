using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.EventSystems;// Required when using Event data.

public class cursorClass : MonoBehaviour, IPointerEnterHandler, IPointerExitHandler
{
    //Otherwise you can do it publicly.  
    public Texture2D cursor;


    void OnMouseEnter()
    {
        
    }

    void OnMouseExit()
    {
        
    }

   
    //Do this when the cursor enters the rect area of this selectable UI object.
    public void OnPointerEnter(PointerEventData eventData)
    {
        Cursor.SetCursor(cursor, Vector2.zero, CursorMode.Auto);
    }

    public void OnPointerExit(PointerEventData eventData)
    {
        Cursor.SetCursor(null, Vector2.zero, CursorMode.Auto);
    }



}

