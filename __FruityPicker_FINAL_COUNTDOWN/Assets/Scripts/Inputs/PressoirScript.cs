using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PressoirScript : MonoBehaviour
{
    private bool bPressoirIsActive = false;
    private Vector3 v3PositionActuelle;
    private Vector3 v3PositionLastFrame;
    private Vector3 v3PositionTarget;
    private Vector3 v3PositionStart;

    private float fSensRotation;

    [Header ("Dummy position Start")]
    public GameObject hDummyCibleStart;

    private float fVitesseTranslate = 8f;
    private float fVitesseRotate = 180f;
    private float fDistanceCorrection;

    [Header("Etape du Setup du Pressoir")]
    public float fSetupState = 0;
    //0 = Not setup
    //1 = Is Settuping
    //2 = Set Up

    // Start is called before the first frame update
    void Start()
    {
        // Penser à mettre un dummy pour retrouver position
        //Ou enregistrer posisiton de départ

        v3PositionStart = hDummyCibleStart.transform.position;
    }

    // Update is called once per frame
    void Update()
    {
        fSetupState = Camera.main.GetComponent<CameraFinScript>().fEtape;

        //Debug.Log("State Setup : " + fSetupState);
        
        //Interdiction move
        if (fSetupState == 0.5f)
        {
            if (gameObject.GetComponent<MeshCollider>().enabled == true)
            {
                gameObject.GetComponent<MeshCollider>().enabled = false;
            }
            return;
        }
        else if (fSetupState == 1)
        {
            //Mise en place
            if (transform.position != v3PositionStart)
            {
                //Bouge
                float fStep = fVitesseTranslate * Time.deltaTime;
                transform.position = Vector3.MoveTowards(transform.position, v3PositionStart, fStep);
                transform.Rotate(new Vector3(0, fVitesseRotate * Time.deltaTime, 0), Space.Self);
            }
            else
            {
                //Change SetupState
                Camera.main.GetComponent<CameraFinScript>().fEtape = 2;
            }
        }
        else if (fSetupState == 2)
        {
            //Réactiver Collider
            if (gameObject.GetComponent<MeshCollider>().enabled == false)
            {
                gameObject.GetComponent<MeshCollider>().enabled = true;
            }

            float fStep = fVitesseTranslate * Time.deltaTime;
            v3PositionActuelle = gameObject.transform.position;
            //Deplacement
            if (!bPressoirIsActive && v3PositionActuelle != v3PositionStart)
            //Move vers position initiale
            {
                fSensRotation = -1;
                transform.position = Vector3.MoveTowards(transform.position, v3PositionStart, fStep);

                if (Mathf.Abs(transform.position.y - v3PositionStart.y) > 0.1)
                {
                    transform.Rotate(new Vector3(0, fVitesseRotate * Time.deltaTime * fSensRotation, 0), Space.Self);
                }
                
            }
            else if (bPressoirIsActive)
            //Move vers position souris
            {
                ///////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////
            
                v3PositionTarget = Camera.main.ScreenToWorldPoint(new Vector3(Input.mousePosition.x, Input.mousePosition.y, Camera.main.transform.position.z));

                //transform.position = new Vector3( v3PositionStart.x, (v3PositionTarget.y * -1) - fDistanceCorrection, v3PositionStart.z);

                Vector3 v3PosDebug = new Vector3(v3PositionStart.x, (v3PositionTarget.y * -1) - fDistanceCorrection, v3PositionStart.z);
                transform.position = Vector3.MoveTowards(v3PositionActuelle, v3PosDebug, fStep);

                fSensRotation = 1;

                if (v3PositionLastFrame != transform.position)
                {
                    if (v3PositionLastFrame.y > transform.position.y)
                    {
                        fSensRotation = 1;
                    }
                    else
                    {
                        fSensRotation = -1;
                    }

                    transform.Rotate(new Vector3(0, fVitesseRotate * Time.deltaTime * fSensRotation, 0), Space.Self);
                }
                
                v3PositionLastFrame = transform.position;

                ///////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////
            }
        }
    }
    
    public void funcActivePressoir()
    {
        bPressoirIsActive = true;

        v3PositionTarget = Camera.main.ScreenToWorldPoint(new Vector3(Input.mousePosition.x, Input.mousePosition.y, Camera.main.transform.position.z));
        fDistanceCorrection = v3PositionTarget.y * -1 - v3PositionActuelle.y;

        Camera.main.GetComponent<AI_AudioMaster>().PlayEvent("onPressage");
    }

    public void funcDesactivePressoir()
    {
        bPressoirIsActive = false;
    }
}
