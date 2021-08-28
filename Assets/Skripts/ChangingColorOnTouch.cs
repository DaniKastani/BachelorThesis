using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ChangingColorOnTouch : MonoBehaviour
{

    public GameObject productToChangeColour;
  

    
    public Material colour;
    public Material darkercolour;
    public Material changeToColor;

    // Start is called before the first frame update
    void Start()
    {
        gameObject.GetComponent<Renderer>().material = colour;
    
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    void OnCollisionEnter(Collision collision)
    {
        gameObject.GetComponent<Renderer>().material = colour;


        if (!(collision.collider.name == "Table"))
        {


            productToChangeColour.GetComponent<Renderer>().material = changeToColor;

            gameObject.GetComponent<Renderer>().material = darkercolour;
           
            
           
        }


    }

    private void OnCollisionExit(Collision collision)
    {
        gameObject.GetComponent<Renderer>().material = colour;
       
    }

}
