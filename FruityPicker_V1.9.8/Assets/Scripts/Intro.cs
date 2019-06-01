using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

public class Intro : MonoBehaviour
{

    public GameObject[] tabInstructions;
    public GameObject[] tabDummyForTP;
    public GameObject[] tabPlaquesImpact;

    public CameraShake cameraShake;

    // Etapes de l'intro
    private int iEtape = 0;

    // Durée du chargement
    public float fTimeObjectif = 5f;

    // Progression actuelle du chargement
    private float fTimeProgress = -1.0f;

    // Affichage en % de la progression de la capture
    public Text textCaptureProgress;




    private float fPercent;
    private float dTruePercent;

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
    }


    // Update is called once per frame
    void Update()
    {

        // CODE

        if (dTruePercent < 105 || iEtape <= 18)
        {
            // Progression de la capture
            fTimeProgress += Time.deltaTime;

            // Conversion de la progression de la capture en pourcentage
            fPercent = (fTimeProgress * 100) / fTimeObjectif;
            // Arrondi le % 
            dTruePercent = Mathf.Round(fPercent);

            // Affiche la progression 

            //textCaptureProgress.text = dTruePercent + " : % ";
        }


       //////////// PLAN 1 

        if (dTruePercent >= 0 && iEtape == 0)
        {
            tabInstructions[0].transform.Translate(0, -7f, 0);
            StartCoroutine(cameraShake.cShake(fForceShakePlans, fDureeShakePlans));
            iEtape = 1;
        }

                if (dTruePercent >= 5 + fDélaiAvantImpact && iEtape == 1)
                {
                    tabPlaquesImpact[0].transform.position = tabDummyForTP[0].transform.position;
                    tabPlaquesImpact[0].transform.rotation = tabDummyForTP[0].transform.rotation;
                    iEtape = 2;
                }

                        if (dTruePercent >= 6 + fDélaiAvantImpact && iEtape == 2)
                        {
                            tabInstructions[0].transform.Translate(0, -fDistanceReculImpact, 0);
                            tabPlaquesImpact[0].transform.position = tabDummyForTP[0].transform.position;
                            StartCoroutine(cameraShake.cShake(fForceShakeTitres, fDureeShakeTitres));

                            iEtape = 3;
                        }

                                if (dTruePercent >= 8 + fDélaiAvantImpact && iEtape == 3)
                                {
                                    tabInstructions[0].transform.Translate(0, fDistanceReculImpact, 0);
                                    tabPlaquesImpact[0].transform.position = tabDummyForTP[0].transform.position;

                                    iEtape = 4;
                                }


        //////////// PLAN 2 

        if (dTruePercent >= 25 && iEtape == 4)
        {
            tabInstructions[1].transform.Translate(0, -7f, 0);
            StartCoroutine(cameraShake.cShake(fForceShakePlans, fDureeShakePlans));
            iEtape = 5;
        }

                if (dTruePercent >= 30 + fDélaiAvantImpact  && iEtape == 5)
                {
                    tabPlaquesImpact[1].transform.position = tabDummyForTP[1].transform.position;
                    tabPlaquesImpact[1].transform.rotation = tabDummyForTP[1].transform.rotation;
                    iEtape = 6;
                }

                        if (dTruePercent >= 31 + fDélaiAvantImpact && iEtape == 6)
                        {
                            tabInstructions[1].transform.Translate(0, -fDistanceReculImpact, 0);
                            tabPlaquesImpact[1].transform.position = tabDummyForTP[1].transform.position;
                            StartCoroutine(cameraShake.cShake(fForceShakeTitres, fDureeShakeTitres));

                            iEtape = 7;
                        }

                                if (dTruePercent >= 33 + fDélaiAvantImpact  && iEtape == 7)
                                {
                                    tabInstructions[1].transform.Translate(0, fDistanceReculImpact, 0);
                                    tabPlaquesImpact[1].transform.position = tabDummyForTP[1].transform.position;

                                    iEtape = 8;
                                }

        //////////// PLAN 3 

        if (dTruePercent >= 50 && iEtape == 8)
        {
            tabInstructions[2].transform.Translate(0, -7f, 0);
            StartCoroutine(cameraShake.cShake(fForceShakePlans, fDureeShakePlans));
            iEtape = 9;
        }

                if (dTruePercent >= 55 + fDélaiAvantImpact && iEtape == 9)
                {
                    tabPlaquesImpact[2].transform.position = tabDummyForTP[2].transform.position;
                    tabPlaquesImpact[2].transform.rotation = tabDummyForTP[2].transform.rotation;
                    iEtape = 10;
                }

                        if (dTruePercent >= 56 + fDélaiAvantImpact && iEtape == 10)
                        {
                            tabInstructions[2].transform.Translate(0, -fDistanceReculImpact, 0);
                            tabPlaquesImpact[2].transform.position = tabDummyForTP[2].transform.position;
                            StartCoroutine(cameraShake.cShake(fForceShakeTitres, fDureeShakeTitres));

                            iEtape = 11;
                        }

                                if (dTruePercent >= 58 + fDélaiAvantImpact && iEtape == 11)
                                {
                                    tabInstructions[2].transform.Translate(0, fDistanceReculImpact, 0);
                                    tabPlaquesImpact[2].transform.position = tabDummyForTP[2].transform.position;

                                    iEtape = 12;
                                }


        //////////// PLAN 4 

        if (dTruePercent >= 75 && iEtape == 12)
        {
            tabInstructions[3].transform.Translate(0, -7f, 0);
            StartCoroutine(cameraShake.cShake(fForceShakePlans, fDureeShakePlans));
            iEtape = 13;
        }

                if (dTruePercent >= 80 + fDélaiAvantImpact && iEtape == 13)
                {
                    tabPlaquesImpact[3].transform.position = tabDummyForTP[2].transform.position;
                    tabPlaquesImpact[3].transform.rotation = tabDummyForTP[3].transform.rotation;
                    iEtape = 14;
                }

                        if (dTruePercent >= 81 + fDélaiAvantImpact && iEtape == 14)
                        {
                            tabInstructions[3].transform.Translate(0, -fDistanceReculImpact, 0);
                            tabPlaquesImpact[3].transform.position = tabDummyForTP[3].transform.position;
                            StartCoroutine(cameraShake.cShake(fForceShakeTitres, fDureeShakeTitres));

                            iEtape = 15;
                        }

                                if (dTruePercent >= 83 + fDélaiAvantImpact && iEtape == 15)
                                {
                                    tabInstructions[3].transform.Translate(0, fDistanceReculImpact, 0);
                                    tabPlaquesImpact[3].transform.position = tabDummyForTP[3].transform.position;

                                    iEtape = 16;
                                }

      
        // Dézoom
        if (dTruePercent >= 97 && iEtape == 16)
        {
            
            
            GetComponent<Camera>().fieldOfView = Mathf.Lerp(GetComponent<Camera>().fieldOfView, dézoom, Time.deltaTime * smooth);

            if (GetComponent<Camera>().fieldOfView >= dézoom -1)
            {
                iEtape = 17;
            }
        }

        // Zoom
        if (dTruePercent >= 100 && iEtape ==17)
        {
            GetComponent<Camera>().fieldOfView = Mathf.Lerp(GetComponent<Camera>().fieldOfView, zoom, Time.deltaTime * smooth * 2);
            if (GetComponent<Camera>().fieldOfView <= zoom +1)
            {
                iEtape = 18;
            }
        }

        // Lancement du niveau
        if (dTruePercent >= 103 && iEtape == 18)
        {
        
            // APPELLER LE NIVEAU 1
            SceneManager.LoadScene("Menu_Fruits");
        }
    }
}
