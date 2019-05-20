using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PressoirScript : MonoBehaviour
{
    private bool bPressoirIsActive = false;
    private Vector3 v3PositionActuelle;
    private Vector3 v3PositionTarget;
    private Vector3 v3PositionStart;


    private float fVitesse = 1f;
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
        if (!bPressoirIsActive && v3PositionActuelle != v3PositionStart)
        //Move vers position initiale
        {
            transform.position = Vector3.MoveTowards(transform.position, v3PositionStart, fStep);
        }
        else if (bPressoirIsActive)
        //Move vers position souris
        {
            ///////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////
            
            v3PositionTarget = Camera.main.ScreenToWorldPoint(new Vector3(Input.mousePosition.x, Input.mousePosition.y, Camera.main.transform.position.z));
            
            Debug.Log("v3PositionTarget.x :" + v3PositionTarget.x);
            Debug.Log("fDistanceCorrection :" + fDistanceCorrection);
            Debug.Log("position :" + v3PositionActuelle);
            
            transform.position = new Vector3( v3PositionStart.x, (v3PositionTarget.y * -1) - fDistanceCorrection, v3PositionStart.z);

            ///////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////
        }
    }
    
    public void funcActivePressoir()
    {
        bPressoirIsActive = true;

        v3PositionTarget = Camera.main.ScreenToWorldPoint(new Vector3(Input.mousePosition.x, Input.mousePosition.y, Camera.main.transform.position.z));
        fDistanceCorrection = v3PositionTarget.y * -1 - v3PositionActuelle.y;
    }

    public void funcDesactivePressoir()
    {
        bPressoirIsActive = false;
    }
}
