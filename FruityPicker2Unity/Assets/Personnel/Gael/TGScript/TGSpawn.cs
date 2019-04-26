using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TGSpawn : MonoBehaviour
{

    //au nombre de ligne de la grappe
    static public int iLigne;


    //permettre de répartition des embrochements [numéro de ligne, interval]
    float[,] fTableParametre = new float[iLigne, 2];


    //iMinEmbronchement >= iLigne
    public static int iMinEmbronchement;
    //iMaxEmbronchement >= iMinEmbronchement
    public static int iMaxEmbronchement;


    //tableau de stockage de position de chaque jonction par rapport a chaque ligne [ selection de l'emprochement , parametre désiré]
    //0 =  numéro de la ligne 
    //1 = position X
    //2 = position Y
    float [,] fTablePosition = new float[iMaxEmbronchement, 3];

    public float fEspacementY;


    void Start()
    {

        ////////////////////////////////////////Choix du niveaux


        if (iLigne == 10){

            fTableParametre[0, 0] = 0.01f;
            fTableParametre[0, 1] = 0.05f;

            fTableParametre[1, 0] = 0.05f;
            fTableParametre[1, 1] = 0.1f;

            fTableParametre[2, 0] = 0.08f;
            fTableParametre[2, 1] = 0.15f;

            fTableParametre[3, 0] = 0.1f;
            fTableParametre[3, 1] = 0.25f;

            fTableParametre[4, 0] = 0.1f;
            fTableParametre[4, 1] = 0.18f;

            fTableParametre[5, 0] = 0.08f;
            fTableParametre[5, 1] = 0.12f;

            fTableParametre[6, 0] = 0.06f;
            fTableParametre[6, 1] = 0.1f;

            fTableParametre[7, 0] = 0.04f;
            fTableParametre[7, 1] = 0.08f;

            fTableParametre[8, 0] = 0f;
            fTableParametre[8, 1] = 0.04f;

            fTableParametre[9, 0] = 0f;
            fTableParametre[9, 1] = 0.03f;

        }

    
        // point de liaison entre les branches
        int iEmbronchement = (int)Mathf.Floor(Random.Range(iMinEmbronchement, iMaxEmbronchement+1));

        //////////////////////////////////////////////////////////////////Répartition des embrochements
        //permettra de comté le nombre d’embronchement restant
        int iStockEnbronchement = iEmbronchement;
        //permettra de stock le nombre d’embronchement qu’il y aura par ligne
        int[] iTableStockPositionEmbronchement = new int[iLigne - 1]; 

        for (int i = 0; i < iLigne; i++)
        {

            if (iStockEnbronchement > 0)
            {
                int iEbrouchementLigne = (int)Mathf.Floor((float)iEmbronchement *Random.Range(fTableParametre[i, 0], fTableParametre[i, 1]));

                if (iEbrouchementLigne <= iStockEnbronchement)
                {
                    
                    iStockEnbronchement = iStockEnbronchement - iEbrouchementLigne;

                }
                else
                {

                    iEbrouchementLigne = iStockEnbronchement;

                    iStockEnbronchement = 0;

                }
        
            }
            else
            {

                i = iLigne;
        
            }

        }

        // positionnement 









    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
