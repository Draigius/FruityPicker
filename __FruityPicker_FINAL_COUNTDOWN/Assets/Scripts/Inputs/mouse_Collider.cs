using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class mouse_Collider : MonoBehaviour
{
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }


    void OnTriggerEnter(Collider collider)
    {

        //if (collision.gameObject.tag == "mouseCollider")
        //{
        //scriptFaceManager.bContactProche = true;

        if (collider.gameObject.GetComponent<Jonction>() != null)
        {
            collider.gameObject.GetComponent<Jonction>().scriptFaceManager.bContactProche = true;

            //Debug.Log("colliding");

        }

        
        // }
    }

    void OnTriggerExit(Collider collider)
    {

        // if (collision.gameObject.tag == "mouseCollider")
        // {
        //scriptFaceManager.bContactProche = false;
        if (collider.gameObject.GetComponent<Jonction>() != null)
        {
            collider.gameObject.GetComponent<Jonction>().scriptFaceManager.bContactProche = false;

            //Debug.Log("stop collide");

        }
        // }

    }


}
