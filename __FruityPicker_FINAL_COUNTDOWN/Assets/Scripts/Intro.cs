using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

public class Intro : MonoBehaviour
{

    public GameObject[] hTableInstructions;
    public GameObject[] hTableDummyForTP;
    public GameObject[] hTablePlaquesImpact;

    public CameraShake cameraShake;

    // Etapes de l'intro
    private float fEtape = 0;

    // Durée du chargement
    public float fTimeObjectif = 5f;

    // Progression actuelle du chargement
    private float fTimeProgress = -1.0f;

    // Affichage en % de la progression de la capture
    public Text textCaptureProgress;




    private float fPercent;
    private float fTruePercent;

    [Header("Shake des TITRES")]

    [Tooltip("Force du Shake lorsque les titres viennent percuter les plans")]
    public float fForceShakeTitres;
    [Tooltip("Durée du Shake lorsque les titres viennent percuter les plans")]
    public float fDureeShakeTitres;

    [Header("Shake des PLANS")]

    [Tooltip("Force du Shake lorsque les plans arrivent sur l'écran")]
    public float fForceShakePlans;
    [Tooltip("Durée du Shake lorsque les plans arrivent sur l'écran")]
    public float fDureeShakePlans;

    [Header("IMPACT plan/titre")] 

    [Tooltip("Distance du recul lorsqu'un titre percute un plan")]
    public float fDistanceReculImpact;
    [Tooltip("Délai aprés l'apparrition d'un plan avant qu'il ne se fasse percuter par un titre ( + des constantes définis dans le code) ")]
    [Range(0, 14)]
    public float fDélaiAvantImpact;


    ////////////////////////////////////////////////////////////////////////   FOR ZOOM   //////////////////////////////////////////////////////////////////////

  
    float normal = 60;

    [Header("ZOOM IN")]
    public float zoom = 20;
    public float dézoom = 100;
    public float smooth = 5;

   ////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////
   
    
    void Start()
    {
        //tabInstructions = GameObject.FindGameObjectsWithTag("Instructions");
        //target.transform.position = new Vector3(0.0f, -6.0f, 0.0f);
        GetComponent<AI_AudioMaster>().LoadBank();
    }


    // Update is called once per frame
    void Update()
    {
        //Debug.Log("iEtape =" + iEtape);
        Debug.Log("fEtape =" + fEtape);
        // CODE

        
        //////////////////////////////////////////////////////////////////////----------------------------------------------------------------------------
        ///


        if(fEtape == 1)
        {

            if (Input.GetMouseButtonDown(0))
            {

                fEtape = 2;

            }

        }




        if (fEtape == 3.5f)
        {

            if (Input.GetMouseButtonDown(0))
            {

                fEtape = 4;
                //fTimeProgress = 0.767f;

                fTimeProgress = (fTimeObjectif * 25) / 100;

            }

        }

        if (fEtape == 7.5f)
        {

            if (Input.GetMouseButtonDown(0))
            {

                fEtape = 8;
                //fTimeProgress = 2.77f;
                fTimeProgress = (fTimeObjectif * 50) / 100;

            }

        }

        if (fEtape == 11.5f)
        {

            if (Input.GetMouseButtonDown(0))
            {

                fEtape = 12;
                //fTimeProgress = 4.76f;
                fTimeProgress = (fTimeObjectif * 75) / 100;

            }

        }

        if (fEtape == 15.5f)
        {

            if (Input.GetMouseButtonDown(0))
            {

                fEtape = 16;
                //fTimeProgress = 6.76f;
                fTimeProgress = (fTimeObjectif * 97) / 100;

            }

        }


        if (fTruePercent < 105 || fEtape <= 18)
        {
            // Progression de la capture
            fTimeProgress += Time.deltaTime;

            // Conversion de la progression de la capture en pourcentage
            fPercent = (fTimeProgress * 100) / fTimeObjectif;
            
            // Arrondi le % 
            fTruePercent = Mathf.Round(fPercent);

            // Affiche la progression 

            //textCaptureProgress.text = dTruePercent + " : % ";
        }

        //////////////////////////////////////------------------------------------------------------------------------------------------------

        //////////// PLAN 1 

        if (fTruePercent >= 0 && fEtape == 0)
        {
            hTableInstructions[0].transform.Translate(0, -7f, 0);
            StartCoroutine(cameraShake.cShake(fForceShakePlans, fDureeShakePlans));

            fEtape = 1;
            GetComponent<AI_AudioMaster>().PlayEvent("onSlap");
        }

                if (fTruePercent >= 5 + fDélaiAvantImpact && fEtape == 1)
                {
                    hTablePlaquesImpact[0].transform.position = hTableDummyForTP[0].transform.position;
                    hTablePlaquesImpact[0].transform.rotation = hTableDummyForTP[0].transform.rotation;

                    fEtape = 2;
            GetComponent<AI_AudioMaster>().PlayEvent("onDing");
        }

                        if (fTruePercent >= 6 + fDélaiAvantImpact && fEtape == 2)
                        {
                            hTableInstructions[0].transform.Translate(0, -fDistanceReculImpact, 0);
                            hTablePlaquesImpact[0].transform.position = hTableDummyForTP[0].transform.position;
                            StartCoroutine(cameraShake.cShake(fForceShakeTitres, fDureeShakeTitres));

                            fEtape = 3;
                        }

                                if (fTruePercent >= 8 + fDélaiAvantImpact && fEtape == 3)
                                {
                                    hTableInstructions[0].transform.Translate(0, fDistanceReculImpact, 0);
                                    hTablePlaquesImpact[0].transform.position = hTableDummyForTP[0].transform.position;

                                    Debug.Log("fTimeProgress 4 :" + fTimeProgress);
                                    fEtape = 3.5f;
                                }


        //////////// PLAN 2 

        if (fTruePercent >= 25 && fEtape == 4)
        {


            hTableInstructions[1].transform.Translate(0, -7f, 0);
            StartCoroutine(cameraShake.cShake(fForceShakePlans, fDureeShakePlans));
            fEtape = 5;

            GetComponent<AI_AudioMaster>().PlayEvent("onSlap");

        }

                if (fTruePercent >= 30 + fDélaiAvantImpact  && fEtape == 5)
                {
                    hTablePlaquesImpact[1].transform.position = hTableDummyForTP[1].transform.position;
                    hTablePlaquesImpact[1].transform.rotation = hTableDummyForTP[1].transform.rotation;
                    fEtape = 6;

                    GetComponent<AI_AudioMaster>().PlayEvent("onDing");
                }

                        if (fTruePercent >= 31 + fDélaiAvantImpact && fEtape == 6)
                        {
                            hTableInstructions[1].transform.Translate(0, -fDistanceReculImpact, 0);
                            hTablePlaquesImpact[1].transform.position = hTableDummyForTP[1].transform.position;
                            StartCoroutine(cameraShake.cShake(fForceShakeTitres, fDureeShakeTitres));

                            fEtape = 7;
                        }

                                if (fTruePercent >= 33 + fDélaiAvantImpact  && fEtape == 7)
                                {
                                    hTableInstructions[1].transform.Translate(0, fDistanceReculImpact, 0);
                                    hTablePlaquesImpact[1].transform.position = hTableDummyForTP[1].transform.position;

                                    fEtape = 7.5f;

            Debug.Log("fTimeProgress 8 :" + fTimeProgress);
        }

        //////////// PLAN 3 

        if (fTruePercent >= 50 && fEtape == 8)
        {
            hTableInstructions[2].transform.Translate(0, -7f, 0);
            StartCoroutine(cameraShake.cShake(fForceShakePlans, fDureeShakePlans));
            fEtape = 9;
            GetComponent<AI_AudioMaster>().PlayEvent("onSlap");
        }

                if (fTruePercent >= 55 + fDélaiAvantImpact && fEtape == 9)
                {
                    hTablePlaquesImpact[2].transform.position = hTableDummyForTP[2].transform.position;
                    hTablePlaquesImpact[2].transform.rotation = hTableDummyForTP[2].transform.rotation;
                    fEtape = 10;
                    GetComponent<AI_AudioMaster>().PlayEvent("onDing");
                }

                        if (fTruePercent >= 56 + fDélaiAvantImpact && fEtape == 10)
                        {
                            hTableInstructions[2].transform.Translate(0, -fDistanceReculImpact, 0);
                            hTablePlaquesImpact[2].transform.position = hTableDummyForTP[2].transform.position;
                            StartCoroutine(cameraShake.cShake(fForceShakeTitres, fDureeShakeTitres));

                            fEtape = 11;
                        }

                                if (fTruePercent >= 58 + fDélaiAvantImpact && fEtape == 11)
                                {
                                    hTableInstructions[2].transform.Translate(0, fDistanceReculImpact, 0);
                                    hTablePlaquesImpact[2].transform.position = hTableDummyForTP[2].transform.position;

                                    fEtape = 11.5f;
            Debug.Log("fTimeProgress 12 :" + fTimeProgress);
        }


        //////////// PLAN 4 

        if (fTruePercent >= 75 && fEtape == 12)
        {
            hTableInstructions[3].transform.Translate(0, -7f, 0);
            StartCoroutine(cameraShake.cShake(fForceShakePlans, fDureeShakePlans));
            fEtape = 13;
            GetComponent<AI_AudioMaster>().PlayEvent("onSlap");
        }

                if (fTruePercent >= 80 + fDélaiAvantImpact && fEtape == 13)
                {
                    hTablePlaquesImpact[3].transform.position = hTableDummyForTP[2].transform.position;
                    hTablePlaquesImpact[3].transform.rotation = hTableDummyForTP[3].transform.rotation;
                    fEtape = 14;
                    GetComponent<AI_AudioMaster>().PlayEvent("onDing");
                }

                        if (fTruePercent >= 81 + fDélaiAvantImpact && fEtape == 14)
                        {
                            hTableInstructions[3].transform.Translate(0, -fDistanceReculImpact, 0);
                            hTablePlaquesImpact[3].transform.position = hTableDummyForTP[3].transform.position;
                            StartCoroutine(cameraShake.cShake(fForceShakeTitres, fDureeShakeTitres));

                            fEtape = 15;
                        }

                                if (fTruePercent >= 83 + fDélaiAvantImpact && fEtape == 15)
                                {
                                    hTableInstructions[3].transform.Translate(0, fDistanceReculImpact, 0);
                                    hTablePlaquesImpact[3].transform.position = hTableDummyForTP[3].transform.position;

                                    fEtape = 15.5f;
                                }

      
        // Dézoom
        if (fTruePercent >= 97 && fEtape == 16)
        {
            
            
            GetComponent<Camera>().fieldOfView = Mathf.Lerp(GetComponent<Camera>().fieldOfView, dézoom, Time.deltaTime * smooth);

            if (GetComponent<Camera>().fieldOfView >= dézoom -1)
            {
                fEtape = 17;
            }
        }

        // Zoom
        if (fTruePercent >= 100 && fEtape ==17)
        {
            GetComponent<Camera>().fieldOfView = Mathf.Lerp(GetComponent<Camera>().fieldOfView, zoom, Time.deltaTime * smooth * 2);
            if (GetComponent<Camera>().fieldOfView <= zoom +1)
            {
                fEtape = 18;
            }
        }

        // Lancement du niveau
        if (fTruePercent >= 103 && fEtape == 18)
        {
        
            // APPELLER LE NIVEAU 1
            SceneManager.LoadScene("Level_1");
        }
    }
}
