using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ConectionBras : MonoBehaviour
{
    [Range(0.1f, 5)]
    public float fVitesseFleche = 0.5f;

    public bool bActif = false;

    public int iEtatFB = 0;

    [Tooltip("1 = vers le haut ; \n 2 = vers le bas ;")]
    public int iSensFB = 1;

    int iMultiplicateur = 0;//dépent de iSens, permet de fait des calcules

    [Header("ne pas toucher")]
    [Header("________________________________________________________________________")]
    
    public GameObject hDummy0;
    public GameObject hDummy1;
    public GameObject hMeshFleche;


    private int iSensFBSauvgarde;


    public Material[] mTableMaterial = new Material[2];

    Renderer rend;

    // Start is called before the first frame update
    void Start()
    {

        

    }

    // Update is called once per frame
    void Update()
    {
        funcRenduFleche(bActif);
        //Debug.Log("iSensFB :" + iSensFB);

        if(iSensFBSauvgarde != iSensFB && iSensFB != 0)
        {

            hMeshFleche.GetComponent<Fleche>().hMesh.transform.Rotate(new Vector3(0, 180, 0), Space.Self);

        }

        iSensFBSauvgarde = iSensFB;




        hMeshFleche.GetComponent<Fleche>().iSens = iMultiplicateur;


        if (iSensFB == 1)
        {

            iMultiplicateur = -1;

        }
        else
        {

            iMultiplicateur = 1;

        }


        if(hMeshFleche.GetComponent<Fleche>().bStart == true)
        {

            hMeshFleche.transform.Translate(new Vector3(0, (fVitesseFleche * Time.deltaTime) * iMultiplicateur, 0), Space.Self);

        }



        if (Input.GetKeyDown(KeyCode.P))
        {

            hMeshFleche.GetComponent<Fleche>().bStart = true;

        }

        if (Input.GetKeyDown(KeyCode.M))
        {
            funcChangeSens();

        }

    }



    

    public void funcChangeSens()
    {

        if(iSensFB == 1)
        {

            iSensFB = 2;

        }
        else
        {

            iSensFB = 1;

        }
        
    }

    public void funcStart()
    {

        hMeshFleche.GetComponent<Fleche>().funcRendu();

    }



    public void funcRenduFleche(bool bRenduActive)
    {

        rend = hMeshFleche.GetComponent<Fleche>().hMesh.GetComponent<Renderer>();
        rend.enabled = bRenduActive;

        rend.sharedMaterial = mTableMaterial[Mathf.Abs(iEtatFB)];
        
    }

}
