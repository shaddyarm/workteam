using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TestScript : MonoBehaviour {

	public Animator anim;

    void Start()
    {
        //anim = GetComponent<Animator>();
    }

    void Update()
    {
        if (Input.GetKeyDown(KeyCode.Space))
        {
            if (null != anim)
            {
                // play Bounce but start at a quarter of the way though
                anim.Play("AAA", 0, 0.25f);
				
				
				anim.ResetTrigger("Jump");
				anim.SetTrigger("Jump");
				
            }
        }
    }
}
