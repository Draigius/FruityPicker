using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Eyes : MonoBehaviour
{
    public Transform reference;
    public float fFactor = 0.25f;
    public float fLimit = 0.08f;
    private Vector3 v3Center;
    private Vector3 v3ReferencePoint;

    public GameObject hTarget;

    private float fTimer;
    private float fTimerLookAround;
    private float fDelay = 0.12f;
    private float fDelayLookAround = 0.4f;


    public FaceManager scriptFaceManager;

    void Start()
    {
        v3ReferencePoint = transform.localPosition;
        v3Center = RectTransformUtility.WorldToScreenPoint(Camera.main, transform.localPosition);
    }
    void Update()
    {

        fTimer += Time.deltaTime;
        fTimerLookAround += Time.deltaTime;

        if (fTimer>fDelay)
        {
            Vector3 _mousePos;
            
            if (Input.GetMouseButton(0) == true && scriptFaceManager.bContactProche == true)
            {/*
                _mousePos = new Vector3(Input.mousePosition.x, Input.mousePosition.y, 2);

                Vector3 _dir = (_mousePos - v3Center) * fFactor;
                _dir = Vector3.ClampMagnitude(_dir, fLimit);
                //Debug.Log(dir);
                Vector3 _tt = v3ReferencePoint + _dir;
                transform.localPosition = _tt;

                fTimer = 0;
             */
               
            }
            //angry
            else if (fTimerLookAround > fDelayLookAround && Input.GetMouseButton(0) == false && scriptFaceManager.iCurState==6 && scriptFaceManager.hJonctionZone && scriptFaceManager.hJonctionZone.GetComponent<Jonction>().iEtat<0)
            {
                /*
                Vector3 _dir = scriptFaceManager.hJonctionZone.transform.position - transform.localPosition * fFactor;


                _dir = Vector3.ClampMagnitude(_dir, fLimit);

                
                Vector3 _tt = v3ReferencePoint + _dir;
                transform.localPosition = _tt;

                fDelayLookAround = 0.4f - Random.Range(0, 0.2f);
                fTimerLookAround = 0;

                //Debug.Log("Angry");
                */
            }
            //change la direction du regard automatiquement
            else if(fTimerLookAround > fDelayLookAround && Input.GetMouseButton(0) == false && scriptFaceManager.hJonctionZone == null)
            {

                

                Vector3 _dir = hTarget.transform.position - transform.localPosition * fFactor;
               

                _dir = Vector3.ClampMagnitude(_dir, fLimit);

                //Debug.Log(dir);
                //Debug.Log(dir);
                Vector3 _tt = v3ReferencePoint + _dir;
                transform.localPosition = _tt;

                fDelayLookAround = 0.4f - Random.Range(0, 0.2f);
                fTimerLookAround = 0;


            }
 

        }
        
    }
}
