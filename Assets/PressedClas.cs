using UnityEngine;
using System.Collections.Generic;
using System.Collections;

public class PressedClas : MonoBehaviour
{
	public Xenu.Game.UnityOutlineManager manager;
	
	public GameObject ob1;
	public GameObject ob2;
	public GameObject ob3;
	public GameObject ob4;
	public GameObject ob5;
	public GameObject ob6;
	public GameObject ob7;
	
	
    void Update()
    {
        if (Input.GetKeyDown("1"))
        {
            print("1 key was pressed");
			manager.ClearSelection();
			manager.AddSelect(ob1);
        }
		
		if (Input.GetKeyDown("2"))
        {
            print("2 key was pressed");
			manager.ClearSelection();
			manager.AddSelect(ob2);
        }
		
		if (Input.GetKeyDown("3"))
        {
			manager.ClearSelection();
			manager.AddSelect(ob3);
        }
		
		if (Input.GetKeyDown("4"))
        {
			manager.ClearSelection();
			manager.AddSelect(ob4);
        }
		
		if (Input.GetKeyDown("5"))
        {
			manager.ClearSelection();
			manager.AddSelect(ob5);
        }
		
		if (Input.GetKeyDown("6"))
        {
			manager.ClearSelection();
			manager.AddSelect(ob6);
        }
		
		if (Input.GetKeyDown("7"))
        {
			manager.ClearSelection();
			manager.AddSelect(ob7);
        }
		
		if (Input.GetKeyDown("0"))
        {
            print("3 key was pressed");
			manager.ClearSelection();
        }
		
		
    }
}