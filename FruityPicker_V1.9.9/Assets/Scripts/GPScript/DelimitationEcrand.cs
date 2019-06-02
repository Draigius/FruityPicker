using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DelimitationEcrand : MonoBehaviour
{

    private void OnTriggerEnter(Collider other)
    {

        if (other.gameObject.GetComponent<Jonction>())
        {

            other.gameObject.GetComponent<Jonction>().bInGame = false;


        }
        else
        {

           // Destroy(other.gameObject);


        }

    }
}
