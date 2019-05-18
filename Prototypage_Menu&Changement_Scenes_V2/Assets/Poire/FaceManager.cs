using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FaceManager : MonoBehaviour
{

    public GameObject hSourcilG;
    public GameObject hSourcilD;
    public GameObject hOeilG;
    public GameObject hOeilD;
    public GameObject hPaupiere;
    public GameObject hBouche;
    public Mesh mBoucheHappy;
    public Mesh mBoucheSad;
    public Mesh mBoucheOh;

    [Range(0.86f, 1.2f)]
    public float fBaseSize;

    [Range(0.9f, 1.2f)]
    public float fSmallSize;

    [Range(0.9f, 1.2f)]
    public float fBigSize;

    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {

        //pupilles
        if (Input.GetMouseButton(0) == true)
        {
            hOeilG.GetComponent<Pupil>().UpdateExcited();
            hOeilD.GetComponent<Pupil>().UpdateExcited();
            Debug.Log("updateExcited");

            hBouche.GetComponent<MeshFilter>().mesh = mBoucheOh;

        }

        if (Input.GetMouseButton(1) == true)
        {
            hOeilG.GetComponent<Pupil>().UpdateScared();
            hOeilD.GetComponent<Pupil>().UpdateScared();

        }

        if (Input.GetMouseButtonUp(0) == true | Input.GetMouseButtonUp(1) == true)
        {
            hOeilG.GetComponent<Pupil>().ResetScale();
            hOeilD.GetComponent<Pupil>().ResetScale();

        }



        //sourcils levés
        if (Input.GetKeyDown(KeyCode.A))
        {
                        
            hSourcilG.GetComponent<Sourcil>().fAngle = 40;
            hSourcilD.GetComponent<Sourcil>().fAngle = -40;

            hBouche.GetComponent<MeshFilter>().mesh = mBoucheHappy;

        }

        //sourcils froncés
        if (Input.GetKeyDown(KeyCode.Z))
        {
            hSourcilG.GetComponent<Sourcil>().fAngle = -40;
            hSourcilD.GetComponent<Sourcil>().fAngle = 40;

            hBouche.GetComponent<MeshFilter>().mesh = mBoucheSad;
        }

        //sourcil droit baissé gauche levé
        if (Input.GetKeyDown(KeyCode.E))
        {

            hSourcilG.GetComponent<Sourcil>().fAngle = -20;
            hSourcilD.GetComponent<Sourcil>().fAngle = -50;


        }

        //sourcil gauche baissé droit levé
        if (Input.GetKeyDown(KeyCode.R))
        {
            hSourcilG.GetComponent<Sourcil>().fAngle = 50;
            hSourcilD.GetComponent<Sourcil>().fAngle = 20;
        }

        if (Input.GetKeyUp(KeyCode.A))
        {
            ResetFace();
        }

        if (Input.GetKeyUp(KeyCode.Z))
        {
            ResetFace();
        }

        if (Input.GetKeyUp(KeyCode.E))
        {
            ResetFace();
        }

        if (Input.GetKeyUp(KeyCode.R))
        {
            ResetFace();
        }


        if (Input.GetKeyDown(KeyCode.W))
        {
            ResetFace();
        }


        


    }


    void ResetFace()
    {

        hSourcilG.GetComponent<Sourcil>().fAngle = 0;
        hSourcilD.GetComponent<Sourcil>().fAngle = 0;

        hSourcilG.GetComponent<Sourcil>().fTimer = 50000;
        hSourcilD.GetComponent<Sourcil>().fTimer = 50000;

        hPaupiere.GetComponent<Paupiere>().Clign();
        hBouche.GetComponent<MeshFilter>().mesh = mBoucheHappy;
    }
}
