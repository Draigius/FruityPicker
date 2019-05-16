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


    private bool bEtatSauv;

    public GameObject[] hTableObjectStock= new GameObject[20];


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
                if (hTableObjectStock[i] != null)
                {

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

                funcUplaodZone(other, bEtatPositif, 1);

                for(int i = 0; i<hTableObjectStock.Length; i++)
                {
                    if(hTableObjectStock[i] == null)
                    {

                        hTableObjectStock[i] = other.gameObject;

                        i = hTableObjectStock.Length;

                    }


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

                funcUplaodZone(other, !bEtatPositif, 1);

                for (int i = 0; i < hTableObjectStock.Length; i++)
                {
                    if (hTableObjectStock[i] == other.gameObject)
                    {

                        hTableObjectStock[i] = null;

                        i = hTableObjectStock.Length;

                    }

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

        for (int i = 0; i < cCampPrincipale.GetComponent<MainGame>().hTableJunction.Length; i++)
        {

            if (hObjectToucher == cCampPrincipale.GetComponent<MainGame>().hTableJunction[i])
            {

                hObjectToucher.GetComponent<Jonction>().funcModifEtat(bEtatInverce, iIdGenerateurOrigine, iNumbrItération);

            }

        }

    }

}
