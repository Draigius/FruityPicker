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
    public int iEtat;

    [Tooltip(" 0:normal \n 1:générateur architecture \n 2:générateur zone \n ")]
    public int iType;


    // rendering de material
    Renderer rend;
    [Header("tableau de Material")]
    public Material[] mTableMaterialPositive = new Material[0];
    public Material[] mTableMaterialNegative = new Material[0];

    // Start is called before the first frame update

    [Header("ressource")]
    public GameObject hZoneExport;
    private GameObject hZone;

    void Start()
    {
        if(iType == 2)
        {

            hZone =  Instantiate(hZoneExport, transform.position, Quaternion.identity);

            hZone.transform.Rotate(new Vector3(-90, 0, 0));

        }
    }

    // Update is called once per frame
    void Update()
    {

        if (iType == 2)
        {

            hZone.transform.position = transform.position;
        }


        funcMaterial();


        if(bObjectDebeuger == true)
        {

            funcDebug();

        }
        

        


    }




    public void funcMaterial()
    {
        //Debug.Log(nNum);
        rend = GetComponent<Renderer>();
        rend.enabled = true;
        //rend.sharedMaterial = tMaterial[iSigne, iType];

        if (iEtat >= 0)
        {

            rend.sharedMaterial = mTableMaterialPositive[iType];

        }
        else
        {

            rend.sharedMaterial = mTableMaterialNegative[iType];

        }

    }

    void funcPropagationArchitecture()
    {



    }

    void funcGenératorZone()
    {
        


    }




    void funcDebug()
    {

        if (Input.GetKeyDown(KeyCode.A) && bObjectDebeuger == true)
        {

            if (iType == 1)
            {

                //funcPropagationArchitecture();
                funcGenératorZone();

            }

        }

    }
}
