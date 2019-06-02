using System.Collections;
using System.Collections.Generic;
using UnityEngine;


public class Jonction : MonoBehaviour
{


    [Header("debog")]
    public bool bObjectDebeuger = false;




    // zone identifient 
    [Header("zone identifient ")]
    public int iIdActuel;
    public int iIdParent;
    public int[] iIdEnfant;

    //= new int []
    [Header("zone typage")]
    [Tooltip("donne la positivité de la jonction celon le signe du int \n (0 et positif )")]
    [Range(-1, 0)]
    public int iEtat;
    [Range(-1, 0)]
    public int iEtatIniciale;


    [Tooltip(" 0:normal \n 1:générateur architecture vers le haut \n 2:générateur architecture vers le bas \n 3:générateur zone \n ")]
    [Range(0, 3)]
    public int iType;


    // rendering de material
    [Header("tableau de Material")]
    public bool bRendering = true;
    public Material[] mTableMaterialPositive = new Material[0];
    public Material[] mTableMaterialNegative = new Material[0];

    Renderer rend;

    [Header("Parametre Zone")]
    [Range(0.5f, 6)]
    public float fScaleZone;
    [Tooltip(" id de l'architecte (type 1 ou 2) qui agit sur cette jonction ")]
    public int iIdArchitect = 0;



    [Header("ressource")]
    public GameObject hZoneExport;


    [Header("a ne pas changer")]
    public bool bAttacher = true;
    public bool bActive = false;
    public bool bInGame = true; // permet de savoir si la jonction fait partie du score
    private int iEnclanchement = -1;

    private Camera hMainCam;
    //private bool bEnclanchement = true;
    [Header("________________________________________________________________________")]
    [Tooltip("prefab de fruit ")]
    public GameObject[] hTableMeshFraise;
    public GameObject[] hTableMeshFraisePourri;
    public GameObject[] hTableMeshCitron;
    public GameObject[] hTableMeshCitronPourri;
    public GameObject[] hTableMeshPoire;
    public GameObject[] hTableMeshPoirePourri;
    public GameObject[] hTableMeshKiwi;
    public GameObject[] hTableMeshKiwiPourri;
    public GameObject[] hTableMeshCerise;
    public GameObject[] hTableMeshCerisePourri;

    [Header("________________________________________________________________________")]
    [Header("ne pas remplire")]

    public GameObject hMesh;
    public GameObject hSurMesh;
    public GameObject hZone;



    ///////////////////////////////////////////FACE MANAGER et colliders de proximité de souris
    public FaceManager scriptFaceManager;

    public int iEtatAnterieur;

    //permet de voir si il y a un chamgement d'état 
    private int iEtatStockage;//permet de stock l'état actuel dans le bute de voir si il y a un changement en faissent un comparatif



    private int iStackOverflod = 0; // permet de limité la fonction "funcPropagationArchitecture" pour évité le stack over flow




    /////////////////////////parametre pour enlevé les feed back
    [Header("ne pas remplire/laisser a 0")]
    public int iModificationArchitecteSens = 0;
    public int iModificationArchitecteEtat = 20;

    public GameObject hConnection;



    void Start()
    {

        hConnection = gameObject.GetComponent<HingeJoint>().connectedBody.gameObject;


        rend = GetComponent<Renderer>();

        rend.enabled = false;

        hMainCam = Camera.main;

        if (iType == 2)
        {

            Debug.Log("debug");

        }


        iEtatStockage = iEtat; // stock l'état actuel pour me permètre plus loins de voir si l'état change durant le jeu

        if (iType == 0) //si type neutre
        {
            ///////////////////////////////////////////////////////////////////////////////////////////////////////////////////
            if (iEtatIniciale == 0)
            {
                hMesh = Instantiate(hTableMeshCerise[Mathf.FloorToInt(Random.Range(0, hTableMeshCerise.Length - 0.001f))], transform.position, Quaternion.identity); // création du préfab
            }
            else
            {
                hMesh = Instantiate(hTableMeshCerisePourri[Mathf.FloorToInt(Random.Range(0, hTableMeshCerisePourri.Length - 0.001f))], transform.position, Quaternion.identity);

            }
            ///////////////////////////////////////////////////////////////////////////////////////////////////////////////////



            hSurMesh = GetChildWithName(hMesh, "cerise_SurMesh"); // récupération dans une variable du surmesh qui donne le retour de positivité par la couleur
            hMesh.transform.localScale = new Vector3(0.2f, 0.2f, 0.2f); // mise a l'échelle du préfab

        }

        //si c'est un type architecte alors lancement du propagation
        if (iType == 1 || iType == 2)
        {



            if (iType == 1)
            {

                ///////////////////////////////////////////////////////////////////////////////////////////////////////////////////
                if (iEtatIniciale == 0)
                {
                    hMesh = Instantiate(hTableMeshPoire[Mathf.FloorToInt(Random.Range(0, hTableMeshPoire.Length - 0.001f))], transform.position, Quaternion.identity); // création du préfab
                }
                else
                {
                    hMesh = Instantiate(hTableMeshPoirePourri[Mathf.FloorToInt(Random.Range(0, hTableMeshPoirePourri.Length - 0.001f))], transform.position, Quaternion.identity);

                }
                ///////////////////////////////////////////////////////////////////////////////////////////////////////////////////



                hSurMesh = GetChildWithName(hMesh, "poire_SurMesh"); // récupération dans une variable du surmesh qui donne le retour de positivité par la couleur
                hMesh.transform.localScale = new Vector3(0.2f, 0.2f, 0.2f);// mise a l'échelle du préfab

            }
            else
            {
                ///////////////////////////////////////////////////////////////////////////////////////////////////////////////////
                if (iEtatIniciale == 0)
                {
                    hMesh = Instantiate(hTableMeshCitron[Mathf.FloorToInt(Random.Range(0, hTableMeshCitron.Length - 1.001f))], transform.position, Quaternion.identity); // création du préfab
                }
                else
                {
                    hMesh = Instantiate(hTableMeshCitronPourri[Mathf.FloorToInt(Random.Range(0, hTableMeshCitronPourri.Length - 1.001f))], transform.position, Quaternion.identity);

                }
                ///////////////////////////////////////////////////////////////////////////////////////////////////////////////////




                hSurMesh = GetChildWithName(hMesh, "citron_SurMesh"); // récupération dans une variable du surmesh qui donne le retour de positivité par la couleur
                hMesh.transform.localScale = new Vector3(0.2f, 0.2f, 0.2f);// mise a l'échelle du préfab

                Debug.Log("hMesh :" + hMesh);

            }

            //Debug.Log("hMainCam.GetComponent<MainGame>().hTableJunction.Length :" + hMainCam);

            funcPropagationArchitecture(iType, gameObject, iEtat, 1);// lancement de la fonction de propagation de l'état actual

        }

        //si c'est un générateur
        if (iType == 3)
        {

            //ZONE DE CONTACT OU ZOOOOOONE
            if (fScaleZone < 0.9f)
            {
                ///////////////////////////////////////////////////////////////////////////////////////////////////////////////////
                if (iEtatIniciale == 0)
                {
                    hMesh = Instantiate(hTableMeshKiwi[0], transform.position, Quaternion.identity); // création du préfab
                }
                else
                {
                    hMesh = Instantiate(hTableMeshKiwiPourri[0], transform.position, Quaternion.identity);

                }
                ///////////////////////////////////////////////////////////////////////////////////////////////////////////////////


                hSurMesh = GetChildWithName(hMesh, "kiwi_SurMesh"); // récupération dans une variable du surmesh qui donne le retour de positivité par la couleur
            }
            else
            {
                ///////////////////////////////////////////////////////////////////////////////////////////////////////////////////
                if (iEtatIniciale == 0)
                {
                    hMesh = Instantiate(hTableMeshFraise[Mathf.FloorToInt(Random.Range(0, hTableMeshFraise.Length - 0.001f))], transform.position, Quaternion.identity); // création du préfab
                }
                else
                {
                    hMesh = Instantiate(hTableMeshFraisePourri[Mathf.FloorToInt(Random.Range(0, hTableMeshFraisePourri.Length - 0.001f))], transform.position, Quaternion.identity);

                }
                ///////////////////////////////////////////////////////////////////////////////////////////////////////////////////


                hSurMesh = GetChildWithName(hMesh, "fraise_SurMesh"); // récupération dans une variable du surmesh qui donne le retour de positivité par la couleur

            }



            hMesh.transform.localScale = new Vector3(0.2f, 0.2f, 0.2f);// mise a l'échelle du préfab

            // création de la zone d'effect
            hZone = Instantiate(hZoneExport, transform.position, Quaternion.identity);

            //modification d'une variable qui a pour but de donné le type de propagation, positive ou négative
            if (iEtat == -1)
            {

                hZone.GetComponent<Zone>().bEtatPositif = false;

            }
            else
            {

                hZone.GetComponent<Zone>().bEtatPositif = true;

            }


            //////////////////////////////////////////////////////////////////////////////////////////////////////////////////////// Cyrille
            ///
            hZone.GetComponent<Zone>().hJonctionOrigine = gameObject;



            hZone.GetComponent<Zone>().iIdGenerateurOrigine = iIdActuel;// modifie une variable pour que la zone agice pas sur le générateur

            hZone.GetComponent<Zone>().iIdArchitectPasToucher = iIdArchitect;// modifie une variable pour que la zone agice pas sur les générateur qui ajice sur lui



            hZone.transform.localScale = new Vector3(fScaleZone, 1, fScaleZone);//donne la taille de la zone d'influance défini par la variable public 

            hZone.transform.Rotate(new Vector3(-90, 0, 0));// redresse la zone face a nous

        }

        ///////////////////////////////////////////////INIT FACE MANAGER
        ///

        scriptFaceManager = hMesh.transform.GetChild(1).GetComponent<FaceManager>();
        scriptFaceManager.FuncGetJonction(GetComponent<Jonction>());


    }

    GameObject GetChildWithName(GameObject obj, string name)
    {
        Transform trans = obj.transform;
        Transform childTrans = trans.Find(name);
        if (childTrans != null)
        {
            return childTrans.gameObject;
        }
        else
        {
            return null;
        }
    }

    private void FixedUpdate()
    {

        if (iType == 0 || iType == 1 || iType == 2 || iType == 3)
        {

            hMesh.transform.position = transform.position;

            if (bAttacher == true)
            {

                Rigidbody rbObjetAttache = gameObject.GetComponent<HingeJoint>().connectedBody;
                hMesh.transform.rotation = rbObjetAttache.transform.rotation;
                hMesh.transform.Rotate(new Vector3(0, 0, 90), Space.Self);
                hMesh.transform.Rotate(new Vector3(-90, 0, 0), Space.Self);
            }

        }

    }

    void OnJointBreak(float breakForce)
    {
        bAttacher = false;

        if (iType == 1)
        {
            iStackOverflod = 0;
            funcPropagationArchitecture(iType, gameObject, iEtat, -1);


        }

        if (iType == 3)
        {
            hZone.GetComponent<Zone>().funcUpdateForType();
            Debug.Log("break");
            hZone.GetComponent<Zone>().bGénérateurAttacher = false;

            iEnclanchement = 0;

        }

        if (iType == 0 || iType == 3)
        {

            funcArretDesFeedBack(iModificationArchitecteSens, gameObject);

            
            Debug.Log("hConnection : "+ hConnection);
            hConnection.GetComponent<ConectionBras>().bActif = false;


        }



        ///////////////////////////////////////////////////////////EMOTION//

        scriptFaceManager.FuncOnSmallStress();


        // reset 

        iEtat = iEtatIniciale;

        if(iModificationArchitecteSens == 1)
        {

            //funcDetectAcroche(gameObject);

        }

        //Debug.Log("iEtatIniciale :" + iEtatIniciale); 
        //Debug.Log("iEtat :" + iEtat); 


    }
    //1 : lancer de fonction
    private void funcDetectAcroche(GameObject hLanceurFunc)
    {

        for(int i = 0; i < hLanceurFunc.GetComponent<Jonction>().iIdEnfant.Length; i++)
        {

            int iIdRechercher = hLanceurFunc.GetComponent<Jonction>().iIdEnfant[i];


            for (int j = 0; j < hMainCam.GetComponent<MainGame>().hTableJunction.Length; j++)
            {

                int iIdEnVerification = hMainCam.GetComponent<MainGame>().hTableJunction[]


            }
                

        }

    }

    private void OnTriggerStay(Collider other)
    {
        if (iEnclanchement >= 0)
        {

            Debug.Log("iEnclanchement :" + iEnclanchement);

            if (other.gameObject.GetComponent<Zone>())
            {
                Debug.Log(" contacte avec une zone");
                if (other.gameObject.GetComponent<Zone>().iIdGenerateurOrigine != iIdActuel)
                {
                    Debug.Log(" contacte avec une zone qui n'ai pas la mien");
                    if (other.gameObject.GetComponent<Zone>().bEtatPositif == true)
                    {

                        iEtat = iEtat + 1;

                    }
                    else
                    {

                        iEtat = iEtat - 1;

                    }

                }

            }
            iEnclanchement = iEnclanchement + 1;

        }

    }



    // Update is called once per frame
    void Update()
    {
        foncRenduEtat();

        if (iEnclanchement > 0)
        {

            iEnclanchement = -1;

        }

        //bEnclanchement = true;

        // si c'est un type générateur de zone
        if (iType == 3)
        {
            // la zone suit le générateur
            hZone.transform.position = transform.position;

        }

        //____________________________________________________________________________________________________________________________________________________________________________________________________Rendu
        //change le matériaux en temps réelle
        //if (bRendering == true)
        //{

        //    funcMaterial();

        //}

        //____________________________________________________________________________________________________________________________________________________________________________________________________


        //variable de debeug pour plus tard
        if (bObjectDebeuger == true)
        {

            funcDebug();

        }

        //Debug.Log(Mathf.Sign(10));

        if (Mathf.Sign(iEtatStockage) != Mathf.Sign(iEtat))
        {
            if (iType == 1 || iType == 2)
            {
                //Debug.Log("iType :" + iType);
                //Debug.Log("gameObject :" + gameObject);
                //Debug.Log("iEtat :" + iEtat);

                iStackOverflod = 0;

                funcPropagationArchitecture(iType, gameObject, iEtat, 2);

                iEtatStockage = iEtat;

            }

            if (iType == 3)
            {
                //Debug.Log("iEtatIniciale : " + iEtatIniciale);

                hZone.GetComponent<Zone>().funcUplaodEtat();

                iEtatStockage = iEtat;

            }

        }

    }


    void foncRenduEtat()
    {

        if (iEtat >= 0)
        {

            hSurMesh.GetComponent<Renderer>().sharedMaterial = mTableMaterialPositive[0];

        }
        else
        {

            hSurMesh.GetComponent<Renderer>().sharedMaterial = mTableMaterialNegative[0];

        }


    }



    // change le matériaux  
    public void funcMaterial()
    {
        rend = GetComponent<Renderer>();
        rend.enabled = true;


        //choix de la couleur selon le etat puis fois du motif selon le type
        if (iEtat >= 0)
        {

            rend.sharedMaterial = mTableMaterialPositive[iType];
            //funcChangemantMatérial(mTableMaterialPositive[iType]);

        }
        else
        {
            rend.sharedMaterial = mTableMaterialNegative[iType];
            //funcChangemantMatérial(mTableMaterialNegative[iType]);

        }

    }

    // fonction de propagation 
    // 1 : permet de dire si sa monte ou decent 
    // 2 : l'object de référance pour la fonction 
    // 3 : dit si sa propage du positif ou négatif
    // 4 : savoir si la fonction ce déclenche pour activer la propagation ou l'arreter
    public void funcPropagationArchitecture(int iSensPropagation, GameObject hLanceurFonction, int iEtatPropagation, int iActivation)
    {

        iStackOverflod = iStackOverflod + 1;

        if (iSensPropagation == 1 && iStackOverflod < 50)
        {

            if (iStackOverflod == 1)
            {

                if (iEtatPropagation >= 0)
                {

                    gameObject.GetComponent<HingeJoint>().connectedBody.gameObject.GetComponent<ConectionBras>().iEtatFB = 0;/// feedback join

                }
                else
                {

                    gameObject.GetComponent<HingeJoint>().connectedBody.gameObject.GetComponent<ConectionBras>().iEtatFB = -1;/// feedback join

                }

                if (iActivation == 1)
                {

                    gameObject.GetComponent<HingeJoint>().connectedBody.gameObject.GetComponent<ConectionBras>().bActif = true;/// feedback join

                }
                else
                {

                    gameObject.GetComponent<HingeJoint>().connectedBody.gameObject.GetComponent<ConectionBras>().bActif = false;/// feedback join

                }

            }

            for (int i = 1; i < hMainCam.GetComponent<MainGame>().hTableJunction.Length; i++)
            {

                int iIdVerif = hMainCam.GetComponent<MainGame>().hTableJunction[i].GetComponent<Jonction>().iIdActuel;///id de l'object a verifier
                int iTypeVerif = hMainCam.GetComponent<MainGame>().hTableJunction[i].GetComponent<Jonction>().iType;/// type de l'object a verifier

                if (iIdVerif == hLanceurFonction.GetComponent<Jonction>().iIdParent && iTypeVerif != 1 && iTypeVerif != 2)
                {

                    hMainCam.GetComponent<MainGame>().hTableJunction[i].GetComponent<Jonction>().iModificationArchitecteSens = 1;

                    if (iEtatPropagation >= 0)//iModificationArchitecteEtat
                    {
                        hMainCam.GetComponent<MainGame>().hTableJunction[i].GetComponent<Jonction>().iModificationArchitecteEtat = 0;
                        hMainCam.GetComponent<MainGame>().hTableJunction[i].GetComponent<Jonction>().iEtat = hMainCam.GetComponent<MainGame>().hTableJunction[i].GetComponent<Jonction>().iEtat + (1 * iActivation);

                        //-------------feedback join
                        if (iActivation == 1)
                        {

                            hMainCam.GetComponent<MainGame>().hTableJunction[i].GetComponent<HingeJoint>().connectedBody.gameObject.GetComponent<ConectionBras>().iEtatFB = 0;/// feedback join

                        }
                        //------------

                    }
                    else
                    {
                        hMainCam.GetComponent<MainGame>().hTableJunction[i].GetComponent<Jonction>().iModificationArchitecteEtat = -1;
                        hMainCam.GetComponent<MainGame>().hTableJunction[i].GetComponent<Jonction>().iEtat = hMainCam.GetComponent<MainGame>().hTableJunction[i].GetComponent<Jonction>().iEtat - (1 * iActivation);


                        //-------------feedback join
                        if (iActivation == 1)
                        {

                            hMainCam.GetComponent<MainGame>().hTableJunction[i].GetComponent<HingeJoint>().connectedBody.gameObject.GetComponent<ConectionBras>().iEtatFB = -1;/// feedback join

                        }
                        //------------

                    }


                    if (hMainCam.GetComponent<MainGame>().hTableJunction[i].GetComponent<Jonction>().iIdParent != 0)
                    {

                        funcPropagationArchitecture(iSensPropagation, hMainCam.GetComponent<MainGame>().hTableJunction[i], iEtatPropagation, iActivation);

                        //-------------feedback join
                        hMainCam.GetComponent<MainGame>().hTableJunction[i].GetComponent<HingeJoint>().connectedBody.gameObject.GetComponent<ConectionBras>().bActif = true;/// feedback join
                        //------------
                    }

                    //-------------feedback join
                    if (iActivation == -1)
                    {

                        hMainCam.GetComponent<MainGame>().hTableJunction[i].GetComponent<HingeJoint>().connectedBody.gameObject.GetComponent<ConectionBras>().bActif = false;/// feedback join

                    }
                    //------------

                    i = 1000;
                }
            }

        }


        if (iSensPropagation == 2 && iStackOverflod < 50)
        {

            for (int i = 1; i < hMainCam.GetComponent<MainGame>().hTableJunction.Length; i++)
            {
                Debug.Log("for");
                int iIdVerif = hMainCam.GetComponent<MainGame>().hTableJunction[i].GetComponent<Jonction>().iIdActuel;///id de l'object a verifier
                int iTypeVerif = hMainCam.GetComponent<MainGame>().hTableJunction[i].GetComponent<Jonction>().iType;/// type de l'object a verifier

                int iLongeurTable = hLanceurFonction.GetComponent<Jonction>().iIdEnfant.Length; // nombre de d'enfant du lanceur de la fonction

                for (int j = 0; j < iLongeurTable; j++)
                {
                    //debug
                    int iIdEnfantLanceurFonction = hLanceurFonction.GetComponent<Jonction>().iIdEnfant[j]; // id enfant du lanceur de la fonction

                    if (iIdVerif == iIdEnfantLanceurFonction && iTypeVerif != 1 && iTypeVerif != 2) // verife si l'ID de l'object verifier et le meme que l'id d'un des enfant du lancer de fonction // et que ce soit pas type architecture
                    {

                        hMainCam.GetComponent<MainGame>().hTableJunction[i].GetComponent<Jonction>().iModificationArchitecteSens = 2;

                        hMainCam.GetComponent<MainGame>().hTableJunction[i].GetComponent<HingeJoint>().connectedBody.gameObject.GetComponent<ConectionBras>().funcChangeSens();

                        if (iEtatPropagation >= 0) // verifie si c'est une propagation de type positif ou négatif
                        {

                            hMainCam.GetComponent<MainGame>().hTableJunction[i].GetComponent<Jonction>().iEtat = hMainCam.GetComponent<MainGame>().hTableJunction[i].GetComponent<Jonction>().iEtat + (1 * iActivation);

                            //-------------feedback join
                            if (iActivation == 1)
                            {

                                hMainCam.GetComponent<MainGame>().hTableJunction[i].GetComponent<HingeJoint>().connectedBody.gameObject.GetComponent<ConectionBras>().iEtatFB = 0;/// feedback join

                            }
                            //------------

                        }
                        else
                        {

                            //Debug.Log("verif du nombre ");
                            hMainCam.GetComponent<MainGame>().hTableJunction[i].GetComponent<Jonction>().iEtat = hMainCam.GetComponent<MainGame>().hTableJunction[i].GetComponent<Jonction>().iEtat - (1 * iActivation);

                            //-------------feedback join
                            if (iActivation == 1)
                            {

                                hMainCam.GetComponent<MainGame>().hTableJunction[i].GetComponent<HingeJoint>().connectedBody.gameObject.GetComponent<ConectionBras>().iEtatFB = -1;/// feedback join

                            }
                            //------------

                        }

                        if (hMainCam.GetComponent<MainGame>().hTableJunction[i].GetComponent<Jonction>().iIdEnfant.Length != 0) // si l'object modifier a des enfant on continue la propagation
                        {

                            funcPropagationArchitecture(iSensPropagation, hMainCam.GetComponent<MainGame>().hTableJunction[i], iEtatPropagation, iActivation);

                        }

                        hMainCam.GetComponent<MainGame>().hTableJunction[i].GetComponent<HingeJoint>().connectedBody.gameObject.GetComponent<ConectionBras>().bActif = true;/// feedback join

                        //-------------feedback join
                        if (iActivation == -1)
                        {

                            hMainCam.GetComponent<MainGame>().hTableJunction[i].GetComponent<HingeJoint>().connectedBody.gameObject.GetComponent<ConectionBras>().bActif = false;/// feedback join

                        }

                    }

                }

            }

        }



        if (iStackOverflod == 50)
        {

            Debug.Log("!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!! STACK OVER FLOW !!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!");

        }

    }





    void funcDebug()
    {

        if (Input.GetKeyDown(KeyCode.A) && bObjectDebeuger == true)
        {

            if (iType == 1)
            {



            }

        }


        if (Input.GetKeyDown(KeyCode.X))
        {

            Debug.Log("Input.GetKeyDown(KeyCode.X)");
            if (bRendering == true)
            {

                rend.enabled = false;
                bRendering = false;

            }
            else
            {

                rend.enabled = true;
                bRendering = true;

            }

        }

    }

    //fonction appeler par le script des zones de générateur
    public void funcModifEtat(bool bModifier, int iIdGenerateur, int iNumbreIteration)
    {
        if (iIdGenerateur != iIdActuel)
        {

            if (bModifier == true)
            {

                iEtat = iEtat + iNumbreIteration;

            }
            else
            {

                iEtat = iEtat - iNumbreIteration;

            }

        }

    }


    ////////////////////////////////////////////////////COLLISION DETECTION CYRILLE-SAN
    ///
    //fonctions de collide

    void OnCollisionEnter(Collision collision)
    {
        if (bAttacher == false)
        {
            scriptFaceManager.FuncOnSurprise();
            //Invoke("FuncOnHappy", 0.5f);
            //Debug.Log("contact");



        }
    }



    private void OnCollisionExit(Collision collision)
    {
        if (bAttacher == false && bActive == false)
        {
            scriptFaceManager.FuncOnBigStress();
            //Debug.Log("CollisionExit");


        }
    }






    public void funcArretDesFeedBack(int iSensPropagation, GameObject hLanceurFonction)
    {

        iStackOverflod = iStackOverflod + 1;

        if (iSensPropagation == 1 && iStackOverflod < 50)
        {

            for (int i = 1; i < hMainCam.GetComponent<MainGame>().hTableJunction.Length; i++)
            {

                int iIdVerif = hMainCam.GetComponent<MainGame>().hTableJunction[i].GetComponent<Jonction>().iIdActuel;///id de l'object a verifier
                int iTypeVerif = hMainCam.GetComponent<MainGame>().hTableJunction[i].GetComponent<Jonction>().iType;/// type de l'object a verifier

                if (iIdVerif == hLanceurFonction.GetComponent<Jonction>().iIdParent)
                {

                    hMainCam.GetComponent<MainGame>().hTableJunction[i].GetComponent<Jonction>().iModificationArchitecteSens = 0;

                    hMainCam.GetComponent<MainGame>().hTableJunction[i].GetComponent<HingeJoint>().connectedBody.gameObject.GetComponent<ConectionBras>().bActif = false;/// feedback join
                    hMainCam.GetComponent<MainGame>().hTableJunction[i].GetComponent<Jonction>().iEtat = hMainCam.GetComponent<MainGame>().hTableJunction[i].GetComponent<Jonction>().iEtatIniciale;


                    funcArretDesFeedBack(iSensPropagation, hMainCam.GetComponent<MainGame>().hTableJunction[i]);

                    if (hMainCam.GetComponent<MainGame>().hTableJunction[i].GetComponent<Jonction>().iIdParent != 0)
                    {

                        
                        
                    }

                    i = 1000;

                }

            }

        }

        if (iSensPropagation == 2 && iStackOverflod < 50)
        {

            for (int i = 1; i < hMainCam.GetComponent<MainGame>().hTableJunction.Length; i++)
            {

                int iIdVerif = hMainCam.GetComponent<MainGame>().hTableJunction[i].GetComponent<Jonction>().iIdActuel;///id de l'object a verifier
                int iTypeVerif = hMainCam.GetComponent<MainGame>().hTableJunction[i].GetComponent<Jonction>().iType;/// type de l'object a verifier

                int iLongeurTable = hLanceurFonction.GetComponent<Jonction>().iIdEnfant.Length; // nombre de d'enfant du lanceur de la fonction

                for (int j = 0; j < iLongeurTable; j++)
                {
                    
                    int iIdEnfantLanceurFonction = hLanceurFonction.GetComponent<Jonction>().iIdEnfant[j]; // id enfant du lanceur de la fonction

                    if (iIdVerif == iIdEnfantLanceurFonction) // verife si l'ID de l'object verifier et le meme que l'id d'un des enfant du lancer de fonction // et que ce soit pas type architecture
                    {

                        hMainCam.GetComponent<MainGame>().hTableJunction[i].GetComponent<Jonction>().iModificationArchitecteSens = 0;

                        hMainCam.GetComponent<MainGame>().hTableJunction[i].GetComponent<HingeJoint>().connectedBody.gameObject.GetComponent<ConectionBras>().bActif = false;/// feedback join
                        hMainCam.GetComponent<MainGame>().hTableJunction[i].GetComponent<Jonction>().iEtat = hMainCam.GetComponent<MainGame>().hTableJunction[i].GetComponent<Jonction>().iEtatIniciale;

                        if (hMainCam.GetComponent<MainGame>().hTableJunction[i].GetComponent<Jonction>().iIdEnfant.Length != 0)
                        {

                            funcArretDesFeedBack(iSensPropagation, hMainCam.GetComponent<MainGame>().hTableJunction[i]);

                        }

                        i = 1000;

                    }

                }

            }
            
        }

    }

}
