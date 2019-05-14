using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LL_Main_test : MonoBehaviour
{
    
    public GameObject PrefabPanier;
    private GameObject LePanier;

    public int Score = 0;

    Jonction Etats_fruits;

    void Start()
    {

    }

    void Update()
    {



        if (Input.GetKeyDown(KeyCode.L))
       {
           // Génére une branche à partir du modele de la prefab
           LePanier = Instantiate(PrefabPanier, new Vector3(0, -4, 0), Quaternion.identity);
           LePanier.transform.Rotate(new Vector3(-90, 0, 0));
            // Etats_fruits.GiveFinalValue();

            Camera hMainCamera = Camera.main;

            for (int i = 0; i < hMainCamera.GetComponent<MainGame>().hTableJunction.Length; i++)
            {
                //hMainCamera.GetComponent<MainGame>().hTableJunction[i].GetComponent<Jonction>().GiveFinalValue();
               Score += hMainCamera.GetComponent<MainGame>().hTableJunction[i].GetComponent<Jonction>().iEtat;
                Debug.Log(Score);
            }


        }

        if (Input.GetMouseButtonDown(0))
        {
           
        }
    }

    /*public void ScoreUpdate(int)
    {

    }*/
}