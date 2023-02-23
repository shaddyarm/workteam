using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class transform : MonoBehaviour
{
    public GameObject palka;
    public GameObject coord;
    // Start is called before the first frame update
    void Start()
    {
        palka.transform.position = coord.transform.position;
        palka.transform.rotation = coord.transform.rotation;
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
