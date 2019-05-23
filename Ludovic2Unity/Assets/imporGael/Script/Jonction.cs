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
    [Range(-1,0)]
    public int iEtat;

    [Tooltip(" 0:normal \n 1:générateur architecture vers le haut \n 2:générateur architecture vers le bas \n 3:générateur zone \n ")]
    [Range(0, 3)]
    public int iType;


    // rendering de material
    [Header("tableau de Material")]
    public Material[] mTableMaterialPositive = new Material[0];
    public Material[] mTableMaterialNegative = new Material[0];

    Renderer rend;

    [Header("Parametre Zone")]
    [Range(0.5f, 6)]
    public float fScaleZone;



    [Header("ressource")]
    public GameObject hZoneExport;
    private GameObject hZone;
    private Camera hMainCam;

    [Header("a ne pas changer")]
    public bool bAttacher = true;

    [Tooltip("prefab de fruit ")]
    public GameObject[] hTableMesh;

    [Header("ne pas remplire")]

    //public GameObject hMesh;



    private int iEtatStockage;

    void Start()
    {

        iEtatStockage = iEtat;

        if(iType == 0)
        {

            //hMesh = Instantiate(hTableMesh[0], transform.position, Quaternion.identity);

            //hMesh.transform.localScale = new Vector3(0.2f,0.2f,0.2f);

        }

        hMainCam = Camera.main;

        //si c'est un type architecte alors lancement du propagation
        if (iType == 1 || iType == 2)
        {

            funcPropagationArchitecture(iType, gameObject, iEtat,1);

            if (iType == 1)
            {

                //hMesh = Instantiate(hTableMesh[1], transform.position, Quaternion.identity);
                //hMesh.transform.localScale = new Vector3(0.2f, 0.2f, 0.2f);

            }
            else
            {

                //hMesh = Instantiate(hTableMesh[2], transform.position, Quaternion.identity);
                //hMesh.transform.localScale = new Vector3(0.2f, 0.2f, 0.2f);

            }

               

        }


        //si c'est un générateur
        if (iType == 3)
        {
            // création de la zone d'effect
            hZone =  Instantiate(hZoneExport, transform.position, Quaternion.identity);

            //modification d'une variable qui a pour but de donné le type de propagation, positive ou négative
            if(iEtat == -1)
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

            //hMesh.transform.position = transform.position;
            //hMesh.transform.Rotate(0, 0, transform.rotation.z);

        }

    }


    // Update is called once per frame
    void Update()
    {

        

        // si c'est un type générateur de zone
        if (iType == 3)
        {
            // la zone suit le générateur
            hZone.transform.position = transform.position;
        }

        // change le matériaux en temps réelle 
        funcMaterial();

        //variable de debeug pour plus tard
        if(bObjectDebeuger == true)
        {

            funcDebug();

        }

        //Debug.Log(Mathf.Sign(10));

        if (Mathf.Sign(iEtatStockage) != Mathf.Sign(iEtat))
        {

            funcPropagationArchitecture(iType, gameObject, iEtat, 1);

            iEtatStockage = iEtat;
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

    // limiteur de stack over flow
    private int iConteur =0;

    // fonction de propagation 
    // 1 : permet de dire si sa monte ou decent 
    // 2 : l'object de référance pour la fonction 
    // 3 : dit si sa propage du positif ou négatif
    // 4 : savoir si la fonction ce déclenche pour activer la propagation ou l'arreter
    public void funcPropagationArchitecture(int iSensPropagation , GameObject hLanceurFonction, int iEtatPropagation , int iActivation)
    {

        if (iSensPropagation == 1)
        {

            for (int i = 0; i < hMainCam.GetComponent<MainGame>().hTableJunction.Length; i++)
            {

                int iIdVerif = hMainCam.GetComponent<MainGame>().hTableJunction[i].GetComponent<Jonction>().iIdActuel;

                if (iIdVerif == hLanceurFonction.GetComponent<Jonction>().iIdParent)
                {
                    if (iEtatPropagation >= 0)
                    {
                        hMainCam.GetComponent<MainGame>().hTableJunction[i].GetComponent<Jonction>().iEtat = hMainCam.GetComponent<MainGame>().hTableJunction[i].GetComponent<Jonction>().iEtat + (1* iActivation);
                    }
                    else
                    {

                        hMainCam.GetComponent<MainGame>().hTableJunction[i].GetComponent<Jonction>().iEtat = hMainCam.GetComponent<MainGame>().hTableJunction[i].GetComponent<Jonction>().iEtat - (1 * iActivation);

                    }

                    if(hMainCam.GetComponent<MainGame>().hTableJunction[i].GetComponent<Jonction>().iIdParent != 0)
                    {

                        funcPropagationArchitecture(iSensPropagation, hMainCam.GetComponent<MainGame>().hTableJunction[i], iEtatPropagation, 1);

                    }

                    i = 1000;
                }
            }

        }


        if (iSensPropagation == 2)
        {

            for (int i = 0; i < hMainCam.GetComponent<MainGame>().hTableJunction.Length; i++)
            {
                int iIdVerif = hMainCam.GetComponent<MainGame>().hTableJunction[i].GetComponent<Jonction>().iIdActuel;

                int iLongeurTable = hLanceurFonction.GetComponent<Jonction>().iIdEnfant.Length;

                for (int j = 0; j < iLongeurTable; j++)
                {

                    int iIdLanceurFonction = hLanceurFonction.GetComponent<Jonction>().iIdEnfant[j];

                    if (iIdVerif == iIdLanceurFonction)
                    {

                        if (iEtatPropagation >= 0)
                        {

                            hMainCam.GetComponent<MainGame>().hTableJunction[i].GetComponent<Jonction>().iEtat = hMainCam.GetComponent<MainGame>().hTableJunction[i].GetComponent<Jonction>().iEtat + (1 * iActivation);

                        }
                        else
                        {

                            //Debug.Log("Etat :" + hMainCam.GetComponent<MainGame>().hTableJunction[i].GetComponent<Jonction>().iEtat);
                            //Debug.Log("object :" + hMainCam.GetComponent<MainGame>().hTableJunction[i]);
                            //Debug.Log("i :" + i);
                            hMainCam.GetComponent<MainGame>().hTableJunction[i].GetComponent<Jonction>().iEtat = hMainCam.GetComponent<MainGame>().hTableJunction[i].GetComponent<Jonction>().iEtat - (1 * iActivation);

                        }

                        if (hMainCam.GetComponent<MainGame>().hTableJunction[i].GetComponent<Jonction>().iIdEnfant.Length != 0)
                        {

                            iConteur = iConteur + 1;

                            if (iConteur < 1000)
                            {

                                //Debug.Log("Mdr :" + iConteur);
                                funcPropagationArchitecture(iSensPropagation, hMainCam.GetComponent<MainGame>().hTableJunction[i], iEtatPropagation, 1);

                            }
                            
                        }

                        if(j == iLongeurTable - 1)
                        {

                            i = 1000;

                        }

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

    }

    //fonction appeler par le script des zones de générateur
    public void funcModifEtat(bool bModifier, int iIdGenerateur)
    {
        if(iIdGenerateur != iIdActuel)
        {

            if (bModifier == true)
            {

                iEtat = iEtat + 1;

            }
            else
            {

                iEtat = iEtat - 1;

            }

        }

    }





}
