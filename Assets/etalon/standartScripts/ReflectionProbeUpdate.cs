using System.Collections;
using System.Collections.Generic;
using UnityEngine;
 
public class ReflectionProbeUpdate : MonoBehaviour {
    public ReflectionProbe ef ;
    RenderTexture targetTexture ;
 
    // Use this for initialization
    void Start () {
       
    }
   
    // Update is called once per frame
    public void update2 ()
    {
        //ef.refreshMode=1;
        ef.RenderProbe(targetTexture = null);
   
    }
}