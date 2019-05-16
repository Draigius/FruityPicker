using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class InputManagerFinNiveau : MonoBehaviour
{
    private bool bSecateurIsActive = false;
    private Vector3 v3PositionActuelle;
    private Vector3 v3PositionTarget;
    private Vector3 v3PositionStart;


    private float fVitesse = 10f;
    private float fDistanceCorrection;

    // Start is called before the first frame update
    void Start()
    {
        // Penser à mettre un dummy pour retrouver position
        //Ou enregistrer posisiton de départ
        v3PositionStart = gameObject.transform.position;
    }

    // Update is called once per frame
    void Update()
    {

        float fStep = fVitesse * Time.deltaTime;
        v3PositionActuelle = gameObject.transform.position;
        //Deplacement
        if (!bSecateurIsActive && v3PositionActuelle != v3PositionStart)
        //Move vers position initiale
        {
            transform.position = Vector3.MoveTowards(transform.position, v3PositionStart, fStep);
        }
        else if (bSecateurIsActive)
        //Move vers position souris
        {
            ///////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////

            //Debug.Log("Target PreDistance" + v3PositionTarget.x);

            v3PositionTarget = Camera.main.ScreenToWorldPoint(new Vector3(Input.mousePosition.x, Input.mousePosition.y, Camera.main.transform.position.z));



            Debug.Log("v3PositionTarget.x :" + v3PositionTarget.x);
            Debug.Log("fDistanceCorrection :" + fDistanceCorrection);
            Debug.Log("position :" + v3PositionActuelle);

            //transform.position = Vector3.MoveTowards(transform.position, v3PositionTarget, fStep);
            transform.position = new Vector3((v3PositionTarget.x * -1) - fDistanceCorrection, v3PositionStart.y, v3PositionStart.z);

            ///////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////
        }
    }

    //Collision
    
    public void funcActiveSecateur()
    {
        bSecateurIsActive = true;

        v3PositionTarget = Camera.main.ScreenToWorldPoint(new Vector3(Input.mousePosition.x, Input.mousePosition.y, Camera.main.transform.position.z));
        fDistanceCorrection = v3PositionTarget.x * -1 - v3PositionActuelle.x;

        //Debug.Log("Secateur Actif");
    }

    public void funcDesactiveSecateur()
    {
        bSecateurIsActive = false;
        //Debug.Log("Secateur Inactif");
    }
}
