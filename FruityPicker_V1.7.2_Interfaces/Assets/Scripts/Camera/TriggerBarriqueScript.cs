using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TriggerBarriqueScript : MonoBehaviour
{
    private int iCompteur;
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {

    }

    private void OnTriggerEnter(Collider other)
    {
        //Debug.Log("Fruit Restants : " + iCompteur);

        if (other.gameObject.GetComponent<PressoirScript>())
        {
            other.enabled = false;
            Camera.main.GetComponent<CameraFinScript>().fEtape = 3;
        }
        else
        {
            Destroy(other.gameObject);

            iCompteur--;

            if (iCompteur == 0)
            {
                Debug.Log("All Fruit Are Dead");
                Camera.main.GetComponent<CameraFinScript>().fEtape = 1;
            }
        }
    }

    private void OnTriggerStay (Collider other)
    {
        //Debug.Log("Fruit Restants : " + iCompteur);

        Debug.Log("Object Touched : " + other.gameObject);
        
    }

    public void funcSendTotalFruitNumber (int iTotalFruitNumber)
    {
        iCompteur = iTotalFruitNumber;
    }
}
