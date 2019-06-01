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

    public int iMultiplicateur = 0;//dépent de iSens, permet de fait des calcules

    [Header("ne pas toucher")]
    [Header("________________________________________________________________________")]
    
    public GameObject hDummy0;
    public GameObject hDummy1;
    public GameObject hMeshCube;
    public GameObject hMeshFleche;


    private int iSensFBSauvgarde;




    private float fDistanceActuelle;
    private float fDistanceDummyDummy;


    public Material[] mTableMaterial = new Material[2];

    Renderer rend;

    // Start is called before the first frame update
    void Start()
    {
        iSensFBSauvgarde = iSensFB;
        fDistanceDummyDummy = Vector3.Distance(hDummy0.transform.position, hDummy1.transform.position);

    }

    // Update is called once per frame
    void Update()
    {

        // calcule de distance 
        fDistanceActuelle = Vector3.Distance(hDummy0.transform.position, hMeshCube.transform.position);
        //Debug.Log(fDistanceActuelle);
        //Debug.Log(fDistanceDummyDummy);



        ////systeme de rendu
        funcRenduFleche(bActif);
        //Debug.Log("iSensFB :" + iSensFB);



        //change la rotation du mesh flache
        if(iSensFBSauvgarde != iSensFB && iSensFB != 0)
        {

            hMeshFleche.transform.Rotate(new Vector3(0, 180, 0), Space.Self);

        }

        iSensFBSauvgarde = iSensFB;

        

        if (iSensFB == 1)
        {

            iMultiplicateur = 1;

        }
        else
        {

            iMultiplicateur = -1;

        }

        /////////////mouvement 
        if(bActif == true)
        {

            if (iSensFB == 2)
            {

                if (fDistanceActuelle > fDistanceDummyDummy)
                {

                    hMeshCube.transform.position = hDummy0.transform.position;

                }

            }
            else
            {

                if (fDistanceActuelle <0.15)
                {

                    hMeshCube.transform.position = hDummy1.transform.position;

                }

            }

            hMeshCube.transform.Translate(new Vector3(0, (fVitesseFleche * Time.deltaTime) * iMultiplicateur, 0), Space.Self);

        }



        if (Input.GetKeyDown(KeyCode.P))
        {



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




    public void funcRenduFleche(bool bRenduActive)
    {

        rend = hMeshFleche.GetComponent<Renderer>();
        rend.enabled = bRenduActive;

        rend.sharedMaterial = mTableMaterial[Mathf.Abs(iEtatFB)];
        
    }

}
