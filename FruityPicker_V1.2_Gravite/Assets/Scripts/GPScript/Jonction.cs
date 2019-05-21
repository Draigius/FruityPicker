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
    private int iEtatIniciale;

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



    [Header("ressource")]
    public GameObject hZoneExport;
    private GameObject hZone;

    [Header("cam Principale")]
    public Camera hMainCam;

    [Header("a ne pas changer")]
    public bool bAttacher = true;
    public bool bActive = true;
    private int iEnclanchement = -1;
    //private bool bEnclanchement = true;

    [Tooltip("prefab de fruit ")]
    public GameObject[] hTableMesh;

    [Header("ne pas remplire")]

    public GameObject hMesh;


    //permet de voir si il y a un chamgement d'état 
    private int iEtatStockage;

    void Start()
    {
        iEtatIniciale = iEtat;
        iEtatStockage = iEtat;

        if (iType == 0)
        {
            
            hMesh = Instantiate(hTableMesh[0], transform.position, Quaternion.identity);

            hMesh.transform.localScale = new Vector3(0.2f, 0.2f, 0.2f);

        }
        

        //si c'est un type architecte alors lancement du propagation
        if (iType == 1 || iType == 2)
        {

            funcPropagationArchitecture(iType, gameObject, iEtat, 1);

            if (iType == 1)
            {

                hMesh = Instantiate(hTableMesh[1], transform.position, Quaternion.identity);
                hMesh.transform.localScale = new Vector3(0.2f, 0.2f, 0.2f);

            }
            else
            {

                hMesh = Instantiate(hTableMesh[2], transform.position, Quaternion.identity);
                hMesh.transform.localScale = new Vector3(0.2f, 0.2f, 0.2f);

            }



        }


        //si c'est un générateur
        if (iType == 3)
        {
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

            // modifie une variable pour que la zone agice pas sur le générateur
            hZone.GetComponent<Zone>().iIdGenerateurOrigine = iIdActuel;


            //donne la taille défini par la variable public 
            hZone.transform.localScale = new Vector3(fScaleZone, 1, fScaleZone);
            // redresse la zone face a nous
            hZone.transform.Rotate(new Vector3(-90, 0, 0));

        }

    }


    private void FixedUpdate()
    {

        if (iType == 0 || iType == 1 || iType == 2)
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

            funcPropagationArchitecture(iType, gameObject, iEtat, -1);

        }

        if (iType == 3)
        {
            hZone.GetComponent<Zone>().funcUpdateForType();
            //Debug.Log("break");
            hZone.GetComponent<Zone>().bGénérateurAttacher = false;

            iEnclanchement = 0;

        }


        // reset 

        iEtat = iEtatIniciale;


    }


    private void OnTriggerStay(Collider other)
    {
        if (iEnclanchement >= 0)
        {

            //Debug.Log("iEnclanchement :" + iEnclanchement);

            if (other.gameObject.GetComponent<Zone>())
            {
                //Debug.Log(" contacte avec une zone");
                if (other.gameObject.GetComponent<Zone>().iIdGenerateurOrigine != iIdActuel)
                {
                    //Debug.Log(" contacte avec une zone qui n'ai pas la mien");
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

        // change le matériaux en temps réelle 
        if (bRendering == true)
        {

            funcMaterial();

        }

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
                funcPropagationArchitecture(iType, gameObject, iEtat, 2);

                iEtatStockage = iEtat;

            }

            if (iType == 3)
            {

                hZone.GetComponent<Zone>().funcUplaodEtat();

                iEtatStockage = iEtat;

            }


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

        if (iSensPropagation == 1)
        {

            for (int i = 0; i < hMainCam.GetComponent<MainGame>().hTableJunction.Length; i++)
            {
                
                int iIdVerif = hMainCam.GetComponent<MainGame>().hTableJunction[i].GetComponent<Jonction>().iIdActuel;
                int iTypeVerif = hMainCam.GetComponent<MainGame>().hTableJunction[i].GetComponent<Jonction>().iType;

                if (iIdVerif == hLanceurFonction.GetComponent<Jonction>().iIdParent && iTypeVerif != 1 && iTypeVerif != 2)
                {
                    if (iEtatPropagation >= 0)
                    {
                        hMainCam.GetComponent<MainGame>().hTableJunction[i].GetComponent<Jonction>().iEtat = hMainCam.GetComponent<MainGame>().hTableJunction[i].GetComponent<Jonction>().iEtat + (1 * iActivation);
                    }
                    else
                    {

                        hMainCam.GetComponent<MainGame>().hTableJunction[i].GetComponent<Jonction>().iEtat = hMainCam.GetComponent<MainGame>().hTableJunction[i].GetComponent<Jonction>().iEtat - (1 * iActivation);

                    }

                    if (hMainCam.GetComponent<MainGame>().hTableJunction[i].GetComponent<Jonction>().iIdParent != 0)
                    {

                        funcPropagationArchitecture(iSensPropagation, hMainCam.GetComponent<MainGame>().hTableJunction[i], iEtatPropagation, iActivation);

                    }

                    i = 1000;
                }
            }

        }


        if (iSensPropagation == 2)
        {
            //Debug.Log(hMainCam.GetComponent<MainGame>().hTableJunction.Length);
            //Debug.Log("Camera : " + hMainCam.GetComponent<MainGame>().hTableJunction.Length);


            for (int i = 0; i < hMainCam.GetComponent<MainGame>().hTableJunction.Length; i++)
            {
                
                int iIdVerif = hMainCam.GetComponent<MainGame>().hTableJunction[i].GetComponent<Jonction>().iIdActuel;///id de l'object a verifier
                int iTypeVerif = hMainCam.GetComponent<MainGame>().hTableJunction[i].GetComponent<Jonction>().iType;/// type de l'object a verifier

                int iLongeurTable = hLanceurFonction.GetComponent<Jonction>().iIdEnfant.Length; // nombre de d'enfant du lanceur de la fonction

                for (int j = 0; j < iLongeurTable; j++)
                {
                    //debug
                    int iIdEnfantLanceurFonction = hLanceurFonction.GetComponent<Jonction>().iIdEnfant[j]; // id enfant du lanceur de la fonction

                    if (iIdVerif == iIdEnfantLanceurFonction && iTypeVerif != 1 && iTypeVerif != 2) // verife si l'ID de l'object verifier et le meme que l'id d'un des enfant du lancer de fonction // et que ce soit pas type architecture
                    {

                        if (iEtatPropagation >= 0) // verifie si c'est une propagation de type positif ou négatif
                        {

                            hMainCam.GetComponent<MainGame>().hTableJunction[i].GetComponent<Jonction>().iEtat = hMainCam.GetComponent<MainGame>().hTableJunction[i].GetComponent<Jonction>().iEtat + (1 * iActivation);

                        }
                        else
                        {

                            hMainCam.GetComponent<MainGame>().hTableJunction[i].GetComponent<Jonction>().iEtat = hMainCam.GetComponent<MainGame>().hTableJunction[i].GetComponent<Jonction>().iEtat - (1 * iActivation);

                        }

                        if (hMainCam.GetComponent<MainGame>().hTableJunction[i].GetComponent<Jonction>().iIdEnfant.Length != 0) // si l'object modifier a des enfant on continue la propagation
                        {

                            funcPropagationArchitecture(iSensPropagation, hMainCam.GetComponent<MainGame>().hTableJunction[i], iEtatPropagation, iActivation);

                        }

                        //if (j == iLongeurTable - 1)
                        //{

                        //    i = 1000;

                        //}

                    }

                }

            }

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

            //Debug.Log("Input.GetKeyDown(KeyCode.X)");
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






}
