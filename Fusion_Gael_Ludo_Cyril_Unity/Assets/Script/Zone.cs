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


    // Start is called before the first frame update
    void Start()
    {
        rend = GetComponent<Renderer>();
        rend.enabled = true;

        if(bEtatPositif == false)
        {

            rend.sharedMaterial = mTableMaterial[0];

        }
        else
        {

            rend.sharedMaterial = mTableMaterial[1];

        }
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    private void OnTriggerEnter(Collider other)
    {

        //Debug.Log("object toucher : "+other);
        string sLayerObject = other.gameObject.layer.ToString();

        if (sLayerObject == "13")
        {

            GameObject hObjectToucher = other.gameObject;
            hObjectToucher.GetComponent<Jonction>().funcModifEtat(bEtatPositif, iIdGenerateurOrigine);

        }


    }

    private void OnTriggerExit(Collider other)
    {

        //Debug.Log("object toucher : "+other);
        string sLayerObject = other.gameObject.layer.ToString();

        if (sLayerObject == "13")
        {

            GameObject hObjectToucher = other.gameObject;
            hObjectToucher.GetComponent<Jonction>().funcModifEtat(!bEtatPositif, iIdGenerateurOrigine);

        }


    }
    
}
