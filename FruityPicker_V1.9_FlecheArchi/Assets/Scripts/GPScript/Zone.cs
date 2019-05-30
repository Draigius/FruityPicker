using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Zone : MonoBehaviour
{
    [Header("resource")]
    public Material[] mTableMaterial= new Material[2];

    Renderer rend;


    [Header("a ne pas modifier")]
    public bool bEtatPositif;

    public int iIdGenerateurOrigine;

    public bool bGénérateurAttacher = true;

    private bool bEtatSauv;

    public int iIdArchitectPasToucher;

    public GameObject[] hTableObjectStock= new GameObject[20];

    
    /// //////////////////////////////////////////////////
    /// 
    public GameObject hJonctionOrigine;
    


    // Start is called before the first frame update
    void Start()
    {
        bEtatSauv = bEtatPositif;

        rend = GetComponent<Renderer>();
        rend.enabled = true;

        if(bEtatPositif == false)
        {

            funcRendu(0);

        }
        else
        {

            funcRendu(1);

        }
        
    }


    void funcRendu(int i)
    {

        rend.sharedMaterial = mTableMaterial[i];

    }

    // Update is called once per frame
    void Update()
    {
        if (bEtatSauv != bEtatPositif)
        {

            for (int i = 0; i < hTableObjectStock.Length; i++)
            {
                if (hTableObjectStock[i] != null )
                {

                    if (hTableObjectStock[i].GetComponent<Jonction>().iType != 0)
                    {
                        if(bGénérateurAttacher == false)
                        {

                            hTableObjectStock[i].GetComponent<Jonction>().funcModifEtat(bEtatPositif, iIdGenerateurOrigine, 2);

                        }else if(hTableObjectStock[i].GetComponent<Jonction>().iType == 3)
                        {

                            hTableObjectStock[i].GetComponent<Jonction>().funcModifEtat(bEtatPositif, iIdGenerateurOrigine, 2);

                        }
                        

                    }
                    else
                    {

                        hTableObjectStock[i].GetComponent<Jonction>().funcModifEtat(bEtatPositif, iIdGenerateurOrigine, 2);

                    }
                    //faire le changement

                }


            }

            bEtatSauv = bEtatPositif;

        }

    }

    private void OnTriggerEnter(Collider other)
    {

        if (other.gameObject.GetComponent<Jonction>())
        {

            if (other.gameObject.GetComponent<Jonction>().iIdActuel != iIdGenerateurOrigine)
            {
                for (int i = 0; i < hTableObjectStock.Length; i++)//stock les objects dans un tableau
                {
                    if (hTableObjectStock[i] == null)
                    {

                        hTableObjectStock[i] = other.gameObject;

                        i = hTableObjectStock.Length;

                        //////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////Cyrillesan
                        ///

                        other.gameObject.GetComponent<Jonction>().scriptFaceManager.hJonctionZone = hJonctionOrigine;

                    }

                }


                
                if (bGénérateurAttacher == false ||  other.gameObject.GetComponent<Jonction>().iIdActuel != iIdArchitectPasToucher)/////////////////////ici ajir 
                {

                    //Debug.Log("entre");

                    funcUplaodZone(other, bEtatPositif, 1);//lance la modif
                    
                }

            }

        }
        
    }

    
    private void OnTriggerExit(Collider other)
    {
        
        if (other.gameObject.GetComponent<Jonction>())
        {

            if (other.gameObject.GetComponent<Jonction>().iIdActuel != iIdGenerateurOrigine)
            {

                for (int i = 0; i < hTableObjectStock.Length; i++)
                {
                    if (hTableObjectStock[i] == other.gameObject)
                    {

                        hTableObjectStock[i] = null;

                        i = hTableObjectStock.Length;

                        ///////////////////////////////////////////////////////////////////////////////CYRILLE SANNNNN
                        ///
                        other.gameObject.GetComponent<Jonction>().scriptFaceManager.hJonctionZone = null;

                    }

                }

                if (bGénérateurAttacher == false || other.gameObject.GetComponent<Jonction>().iIdActuel != iIdArchitectPasToucher)
                {

                    //Debug.Log("exit");

                    funcUplaodZone(other, !bEtatPositif, 1);
                    
                    
                }

            }

        }

    }


    public void funcUplaodEtat()
    {

        bEtatPositif = !bEtatPositif;


        if (bEtatPositif == false)
        {

            funcRendu(0);

        }
        else
        {

            funcRendu(1);

        }

    }


    public void funcUplaodZone(Collider other , bool bEtatInverce , int iNumbrItération)
    {


        Camera cCampPrincipale = Camera.main;
        //Debug.Log("object toucher : "+other);

        GameObject hObjectToucher = other.gameObject;

        for (int i = 1; i < cCampPrincipale.GetComponent<MainGame>().hTableJunction.Length; i++)
        {

            if (hObjectToucher == cCampPrincipale.GetComponent<MainGame>().hTableJunction[i])
            {

                hObjectToucher.GetComponent<Jonction>().funcModifEtat(bEtatInverce, iIdGenerateurOrigine, iNumbrItération);

            }

        }

    }

    public void funcUpdateForType()
    {

        for (int i = 0; i < hTableObjectStock.Length; i++)
        {
            

            if (hTableObjectStock[i] != null)
            {

                if (hTableObjectStock[i].GetComponent<Jonction>().iType == 1 || hTableObjectStock[i].GetComponent<Jonction>().iType == 2)
                {

                    Debug.Log("rentre funcUpdateForType");
                    hTableObjectStock[i].GetComponent<Jonction>().funcModifEtat(bEtatPositif, iIdGenerateurOrigine,1);

                }

            }

        }

    }

}
