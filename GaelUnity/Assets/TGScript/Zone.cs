using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Zone : MonoBehaviour
{
    [Header("resource")]
    public Material[] mTableMaterial= new Material[2];

    Renderer rend;


    [Header("a ne pas modifier")]
    public int iEtat;


    // Start is called before the first frame update
    void Start()
    {
        rend = GetComponent<Renderer>();
        rend.enabled = true;
        rend.sharedMaterial = mTableMaterial[iEtat+1];
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    private void OnTriggerEnter(Collider other)
    {

        Debug.Log("object toucher : "+other);
        string sLayerObject = other.gameObject.layer.ToString();

        if (sLayerObject == "13")
        {

            GameObject hObjectToucher = other.gameObject;
            hObjectToucher.GetComponent<Jonction>().funcModifEtat(iEtat);

        }


    }
}
