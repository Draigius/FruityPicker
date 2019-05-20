using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MainGame : MonoBehaviour
{
    public int iGrappeActuel = 1;



    [Header("tableau de jonction 1")]
    public int iNombreJonctionPrimaire1;
    public GameObject[] hTableJunction1 = new GameObject[0];


    [Header("tableau de jonction 2")]
    public int iNombreJonctionPrimaire2;
    public GameObject[] hTableJunction2 = new GameObject[0];


    [Header("tableau de jonction 3")]
    public int iNombreJonctionPrimaire3;
    public GameObject[] hTableJunction3 = new GameObject[0];


    [Header("tableau de jonction 4")]
    public int iNombreJonctionPrimaire4;
    public GameObject[] hTableJunction4 = new GameObject[0];


    [Header("tableau de jonction 5")]
    public int iNombreJonctionPrimaire5;
    public GameObject[] hTableJunction5 = new GameObject[0];


    [Header("tableau de jonction Actuel")]
    public GameObject[] hTableJunction = new GameObject[0];


    public GameObject hPrefabPanier;
    private GameObject hLePanier;

    private GameObject hMainFruit;

    public int iScore = 0;


    private bool bAwake = true;

    void Awake()
    {

        hTableJunction = hTableJunction1;

    }

    void Update()
    {
        
        if(iGrappeActuel == 1)
        {
            hTableJunction = hTableJunction1;

        }else if(iGrappeActuel == 2)
        {

            hTableJunction = hTableJunction2;

        }
        else if (iGrappeActuel == 3)
        {

            hTableJunction = hTableJunction3;

        }
        else if (iGrappeActuel == 4)
        {

            hTableJunction = hTableJunction4;

        }
        else if (iGrappeActuel == 5)
        {

            hTableJunction = hTableJunction5;

        }


        if (Input.GetKeyDown(KeyCode.L))
        {
            // Génére une branche à partir du modele de la prefab
            hLePanier = Instantiate(hPrefabPanier, new Vector3(0, -4, 0), Quaternion.identity);
            hLePanier.transform.Rotate(new Vector3(-90, 0, 0));
            // Etats_fruits.GiveFinalValue();


            for (int i = 0; i < hTableJunction.Length; i++)
            {
                //hMainCamera.GetComponent<MainGame>().hTableJunction[i].GetComponent<Jonction>().GiveFinalValue();
                int iTchekScore = hTableJunction[i].GetComponent<Jonction>().iEtat;

                if (iTchekScore >= 0)
                {
                    iTchekScore += 1;

                }

                iScore += iTchekScore;
                Debug.Log(iScore);
            }


        }

        if (Input.GetMouseButtonDown(0))
        {

        }
        if(bAwake == true)
        {

            for (int i = 0; i < hTableJunction.Length; i++)
            {

                //Debug.Log("Tableau :" + hTableJunction[i]);


                //hTableJunction[i].GetComponent<Jonction>().Awake();

            }

            bAwake = false;

        }
        



    }

}
