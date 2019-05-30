using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class MainGame : MonoBehaviour
{

    [Header("Menu")]
    public bool bMenu = false;
    [Header("___________________________________________________________________")]


    public int iGrappeTotal;
    public int iGrappeActuel = 1;

    [Header("tableau de jonction 1")]
    [Header("TOUS LES TABLEAUX DE JOIN ON A LA POSITION 0 L'ANCRAGE")]
    public static int iNombreJonctionPrimaire1;
    public GameObject[] hTableJunction1;


    [Header("tableau de jonction 2")]
    public static int iNombreJonctionPrimaire2;
    public GameObject[] hTableJunction2;


    [Header("tableau de jonction 3")]
    public static int iNombreJonctionPrimaire3;
    public GameObject[] hTableJunction3;


    [Header("tableau de jonction 4")]
    public static int iNombreJonctionPrimaire4;
    public GameObject[] hTableJunction4;


    [Header("tableau de jonction 5")]
    public static int iNombreJonctionPrimaire5;
    public GameObject[] hTableJunction5;

    [Header("tableau de jonction 6")]
    public static int iNombreJonctionPrimaire6;
    public GameObject[] hTableJunction6;


    [Header("tableau de jonction Actuel")]
    public GameObject[] hTableJunction;

    
    public GameObject hLePanier;

    private GameObject hMainFruit;


    [Header("Score")]
    public int iScore = 0;
    
    [Header("gestion des transition")]
    public GameObject hTransition;
    public bool bTimer = false;

    [Header("gestion des transition")]
    private float fTimerOrigine1 = 2;
    private float fTimerDeconte1 = 2;

    [Header("gestion des transition")]
    public bool bTimer2 = false;
    private float fTimerOrigine2 = 2;
    private float fTimerDeconte2 = 2;

    [Header("gestion du timer du jeu")]
    [Tooltip("timer en seconde")]
    public float fTimerGameOrigine;
    private float fTimerGameDéconte;
    private bool bPause = false;

    [Header("UI")]
    public Text textTimer;
    public Text textNgrappe;
    public Text textScore;



    private bool bAwake = true;

    void Awake()
    {
        fTimerGameDéconte = fTimerGameOrigine;
        hTableJunction = hTableJunction1;

        ///////////////////////////////////////////////////////////////////Cyrille-san
        GetComponent<AI_AudioMaster>().LoadBank();

    }



    public void funcScore()
    {
        {
            iScore = 0;
            
            hLePanier.transform.position = new Vector3(0, -4, 0);

            // Etats_fruits.GiveFinalValue();

            int iEtatVerif;

            for (int i = 1; i < hTableJunction.Length; i++)
            {
                //hMainCamera.GetComponent<MainGame>().hTableJunction[i].GetComponent<Jonction>().GiveFinalValue();
                iEtatVerif = hTableJunction[i].GetComponent<Jonction>().iEtat;
                //Debug.Log("hTableJunction[i] :"+ hTableJunction[i]);
                if (hTableJunction[i].GetComponent<Jonction>().bInGame == true)
                {

                    if(iEtatVerif >= 0)
                    {

                        iScore = iScore + 1;
                        //Debug.Log("+1");

                    }
                    else
                    {

                        iScore = iScore - 1;
                        //Debug.Log("-1");

                    }
                    
                }
               
            }
        }

        textScore.text = "SCORE :" + iScore;

        if (iScore <= 5)
        {
            textScore.text = textScore.text + " \n \n  Ce jus est..... \n \n Dégueulasse !!!!!!";
        }

        if (iScore > 5 && iScore <= 9)
        {
            textScore.text = textScore.text + "\n \n  Ce jus est..... \n \n Plus ou moins dans les normes...";
        }

        if (iScore >= 10)
        {
            textScore.text = textScore.text + " \n \n Ce jus est..... \n \n Délicieux !!!!!!";
        }
        //Debug.Log(textScore.text);
    }



    void Update()
    {
        if(bMenu == false)
        {

            funcUpdateUI();


            if (bPause == false && fTimerGameDéconte > 0)
            {

                fTimerGameDéconte = fTimerGameDéconte - Time.deltaTime;

            }
            else if (bPause == false)
            {

                //fin du time

                // lancer scene de fin

            }



            if (bTimer == true && fTimerDeconte1 > 0)
            {
                bPause = true;
                fTimerDeconte1 = fTimerDeconte1 - Time.deltaTime;

            }
            else if (bTimer == true)
            {
                bTimer = false;
                bTimer2 = true;
                fTimerDeconte1 = fTimerOrigine1;

                hTransition.GetComponent<TransitionScript>().funcActivateEnd();


            }


            if (bTimer2 == true && fTimerDeconte2 > 0)
            {

                fTimerDeconte2 = fTimerDeconte2 - Time.deltaTime;

            }
            else if (bTimer2 == true && fTimerDeconte2 <= 0)
            {
                bTimer2 = false;
                fTimerDeconte2 = fTimerOrigine2;
                //Debug.Log("fin timer2");
                hTransition.GetComponent<TransitionScript>().funcActivateStart();

                hLePanier.transform.position = new Vector3(-20, -4, 0);

                Debug.Log("TA MERE LA PUTE");

                textScore.text = " ";

                iGrappeActuel = iGrappeActuel + 1;

                funcChangeGrape();

                bPause = false;

            }

        }
        
    }


    void funcUpdateUI()
    {
        int iGrappeRestant = iGrappeTotal - iGrappeActuel-1;
        textNgrappe.text = "Grappe Restante =" + iGrappeRestant;


        if(bPause == false)
        {

            textTimer.text = "Tmer =" + Mathf.Floor (fTimerGameDéconte) + "s";

        }
        else
        {

            textTimer.text = "PAUSE \n" + Mathf.Floor(fTimerGameDéconte) + "s";

        }

    }

    void funcChangeGrape()
    {

        if (iGrappeActuel > iGrappeTotal)
        {
            /// variable static 

            Application.LoadLevel("SceneScore");

        }


        for(int i = 0; i< hTableJunction.Length; i++)
        {
            if(i != 0)
            {
                
                Object.Destroy(hTableJunction[i].GetComponent<Jonction>().hMesh);
                

                if (hTableJunction[i].GetComponent<Jonction>().iType == 3)
                {
                    Debug.Log("hTableJunction[i].GetComponent<Jonction>().hMesh :" + hTableJunction[i].GetComponent<Jonction>().hMesh);
                    Object.Destroy(hTableJunction[i].GetComponent<Jonction>().hZone);

                }

            }

            Object.Destroy(hTableJunction[i]);

        }





        if (iGrappeActuel == 1)
        {
            hTableJunction = hTableJunction1;

        }
        else if (iGrappeActuel == 2)
        {

            hTableJunction = hTableJunction2;

        }
        else if (iGrappeActuel == 3)
        {

            hTableJunction = hTableJunction3;

        }
        else if (iGrappeActuel == 4)
        {

            hTableJunction = hTableJunction4;

        }
        else if (iGrappeActuel == 5)
        {

            hTableJunction = hTableJunction5;

        }
        else if (iGrappeActuel == 6)
        {

            hTableJunction = hTableJunction6;

        }

        hTableJunction[0].transform.position = new Vector3(0, 5.88f, 0);

        for(int i = 1; i< hTableJunction.Length; i++)
        {

            hTableJunction[i].GetComponent<Jonction>().enabled = true;

        }

    }

}
