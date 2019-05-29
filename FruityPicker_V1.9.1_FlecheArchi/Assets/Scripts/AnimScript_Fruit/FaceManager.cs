using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

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
    public Mesh mBoucheCri;

    

    [Range(0.86f, 1.2f)]
    public float fBaseSize;

    [Range(0.9f, 1.2f)]
    public float fSmallSize;

    [Range(0.9f, 1.2f)]
    public float fBigSize;

    private Jonction scriptJonction;
    AI_AudioMaster audioMaster;

    public int iCurState;
    private bool bOldContactProche = false;
    public bool bContactProche = false;

    private float timer_cri;
    private float delay_cri = 1f;
    private int compteur_cri = 0;

    //jonction de zone influançant la jonction
    public GameObject hJonctionZone;




    public bool bDebug = false;
    public int iEtatJonction;
    public int iEtatJonctionAnterieur;



    [Header("ne pas replir sauf cas exlisqg ")]
    public GameObject hJonction;




    // Start is called before the first frame update
    void Start()
    {

        if(hJonction != null)
        {

            scriptJonction = hJonction.GetComponent<Jonction>();

        }

        audioMaster = scriptJonction.GetComponent<AI_AudioMaster>();
        
    }

    // Update is called once per frame
    void Update()
    {
        /*
        if (bDebug == false)
        {

            iEtatJonction = scriptJonction.iEtat;
            iEtatJonctionAnterieur = scriptJonction.iEtatAnterieur;

        }*/

        //limite de temps d'un bigStress

        if (iCurState == 5)
        {
            timer_cri += Time.deltaTime;

            if (timer_cri > delay_cri)
            {
                if( scriptJonction.gameObject.GetComponent<Rigidbody>().velocity.magnitude < 0.5)
                {
                    compteur_cri++;
                    
                    timer_cri = 0;

                    if (compteur_cri>3)
                    {
                        FuncOnChange(true);
                        compteur_cri = 0;
                    }
                }

            }

        }


        //CHECK PROXIMITE
        //on exclue le fruit selectionné

        
        if (bContactProche != bOldContactProche && bContactProche == true)
        {
            if(scriptJonction.bActive == false)
            {
                FuncOnSurprise();

                bOldContactProche = bContactProche;

            }
            else { bContactProche = bOldContactProche; }
            

           // Debug.Log("babar");
           

        }
        else if (bContactProche != bOldContactProche)
        {
            bOldContactProche = bContactProche;
            FuncOnChange(false);
        }
        




        //update sound switch
        if (iEtatJonction == 0)
        {
            audioMaster.setSwitch("arrache", "positive");

        }
        else if (iEtatJonction >= 1)
        {
            audioMaster.setSwitch("arrache", "veryPositive");
        }
        else if (iEtatJonction == -1)
        {
            audioMaster.setSwitch("arrache", "negative");
        }
        else if (iEtatJonction <= -2)
        {
            audioMaster.setSwitch("arrache", "veryNegative");
        }

        if (hJonctionZone != null)
        {
            FuncOnAngry();
            //Debug.Log(hJonctionZone);

        }

        //check changement d'état
        if (iEtatJonction != iEtatJonctionAnterieur)
        {
            iEtatJonctionAnterieur = iEtatJonction;

            if(iCurState != 5)
            {
                FuncOnChange(true);
            }
            

        }




        //////////////////////
        if (Input.GetMouseButton(0) == true && scriptJonction.bActive==true)
        {
            
            

        }

        if (Input.GetMouseButtonDown(0) == true && scriptJonction.bActive == true)
        {
            audioMaster.PlayEvent("Stop_All");
            FuncOnEcstasy();
            //Debug.Log("test");
            

        }

        if (Input.GetMouseButton(1) == true)
        {
            

        }

        if (Input.GetMouseButtonUp(0) == true)
        {
            
            if (scriptJonction.bActive == false && scriptJonction.bAttacher == false)
            {
                audioMaster.PlayEvent("Stop_All");
                if (SceneManager.GetActiveScene().name != "Menu_Fruits")
                {
                    FuncOnBigStress();
                }

            }

        }





        


    }


    void ResetFace()
    {
        iCurState = 0;

        hOeilG.GetComponent<Pupil>().ResetScale();
        hOeilD.GetComponent<Pupil>().ResetScale();

        hSourcilG.GetComponent<Sourcil>().fAngle = 0;
        hSourcilD.GetComponent<Sourcil>().fAngle = 0;

        hSourcilG.GetComponent<Sourcil>().fTimer = 50000;
        hSourcilD.GetComponent<Sourcil>().fTimer = 50000;

        hPaupiere.GetComponent<Paupiere>().Clign();
        hBouche.GetComponent<MeshFilter>().mesh = mBoucheHappy;

        timer_cri = 0;
        compteur_cri = 0;
    }


    //FONCTIONS D'EXPRESSIONS

    

    public void FuncOnHappy()
    {
        iCurState = 1;

        hSourcilG.GetComponent<Sourcil>().fAngle = 0;
        hSourcilD.GetComponent<Sourcil>().fAngle = 0;

        hBouche.GetComponent<MeshFilter>().mesh = mBoucheHappy;

        hOeilG.GetComponent<Pupil>().ScaleBig();
        hOeilD.GetComponent<Pupil>().ScaleBig();

        audioMaster.PlayEvent("Stop_All");

        //Debug.Log("happy");

    }

    public void FuncOnSurprise()
    {
        iCurState = 2;
        timer_cri = 0;
        compteur_cri = 0;

        if (Mathf.Sign(Random.Range(-1,1)) == -1 )
        {
            hSourcilG.GetComponent<Sourcil>().fAngle = 50;
            hSourcilD.GetComponent<Sourcil>().fAngle = 20;

        }
        else
        {
            hSourcilG.GetComponent<Sourcil>().fAngle = 20;
            hSourcilD.GetComponent<Sourcil>().fAngle = 50;

        }



        //hOeilG.GetComponent<Pupil>().UpdateScared();
        //hOeilD.GetComponent<Pupil>().UpdateScared();

        hBouche.GetComponent<MeshFilter>().mesh = mBoucheOh;

        audioMaster.PlayEvent("Stop_All");
        audioMaster.PlayEvent("onProximite");
        //Debug.Log("Surprise");
    }

    public void FuncOnEcstasy()
    {
        iCurState = 3;
        timer_cri = 0;
        compteur_cri = 0;

        hOeilG.GetComponent<Pupil>().UpdateExcited();
        hOeilD.GetComponent<Pupil>().UpdateExcited();
        //Debug.Log("onEcstasy");

        hBouche.GetComponent<MeshFilter>().mesh = mBoucheOh;


        audioMaster.PlayEvent("onSaisi");
        //besoin d'une cible

        //audioMaster.PlayEvent("Stop_All");
        //Debug.Log("Ecstasy");
    }

    public void FuncOnSmallStress()
    {
        ResetFace();

        iCurState = 4;

        hSourcilG.GetComponent<Sourcil>().fAngle = -40;
        hSourcilD.GetComponent<Sourcil>().fAngle = 40;

        hBouche.GetComponent<MeshFilter>().mesh = mBoucheOh;

        hOeilG.GetComponent<Pupil>().ResetScale();
        hOeilD.GetComponent<Pupil>().ResetScale();

        audioMaster.PlayEvent("onArrache");
        //Debug.Log(iEtatJonction);
        //Debug.Log("Arrache");

    }

    public void FuncOnBigStress()
    {
        ResetFace();

        hOeilG.GetComponent<Pupil>().UpdateScared();
        hOeilD.GetComponent<Pupil>().UpdateScared();

        iCurState = 5;

        hBouche.GetComponent<MeshFilter>().mesh = mBoucheCri;

        audioMaster.PlayEvent("Stop_All");
        audioMaster.PlayEvent("cri_continu");

        //Debug.Log("BigStress");

    }

    public void FuncOnAngry()
    {
        iCurState = 6;

        hOeilG.GetComponent<Pupil>().UpdateScared();
        hOeilD.GetComponent<Pupil>().UpdateScared();
        //besoin d'une cible

        //audioMaster.PlayEvent("Stop_All");
        //Debug.Log("Angry");
    }

    public void FuncOnSad()
    {
        iCurState = 7;

        hSourcilG.GetComponent<Sourcil>().fAngle = 40;
        hSourcilD.GetComponent<Sourcil>().fAngle = -40;

        hBouche.GetComponent<MeshFilter>().mesh = mBoucheSad;

        hOeilG.GetComponent<Pupil>().ScaleBig();
        hOeilD.GetComponent<Pupil>().ScaleBig();

        audioMaster.PlayEvent("Stop_All");
        //Debug.Log("Sad");
    }

    public void FuncOnChange(bool _sound)
    {


        if (iEtatJonction > -1)
        {
            FuncOnHappy();
        }
        else
        {
            FuncOnSad();

        }


        audioMaster.PlayEvent("Stop_All");
        if (_sound)
        {
            audioMaster.PlayEvent("onChange");
        }
        
        

    }

    public void funcOnStop()
    {

        Debug.Log("truite");
        audioMaster.PlayEvent("Stop_All");


    }
    

    //RECUP DU SCRIPT JONCTION ENVOYE PAR JONCTION

    public void FuncGetJonction (Jonction jonction)
    {

        scriptJonction = jonction;

    }


}
