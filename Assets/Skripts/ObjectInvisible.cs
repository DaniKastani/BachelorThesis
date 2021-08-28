using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ObjectInvisible : MonoBehaviour
{

    public bool isvisible;
    // Start is called before the first frame update
    void Start()
    {

        
    }

    // Update is called once per frame
    void Update()
    {
        if(isvisible == false)
        {
            gameObject.GetComponent<Renderer>().enabled = false;

        }

        if (isvisible == true)
        {
            gameObject.GetComponent<Renderer>().enabled = true;
        }
        
    }
}
