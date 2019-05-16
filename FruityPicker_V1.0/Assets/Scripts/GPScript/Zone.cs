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

        Debug.Log("update");




    }

    private void OnTriggerEnter(Collider other)
    {

        funcUplaodZone(other, bEtatPositif,1);

    }

    private void OnTriggerStay(Collider other)
    {

        Debug.Log("OnTriggerStay");

        if (bEtatPositif != bEtatSauv)
        {

            //Debug.Log("OnTriggerStay");
            //Debug.Log("bEtatPositif :" + bEtatPositif);
            funcUplaodZone(other, bEtatPositif,2);
            bEtatSauv = bEtatPositif;

        }

        

    }



    private void OnTriggerExit(Collider other)
    {

        funcUplaodZone(other, !bEtatPositif,1);

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
