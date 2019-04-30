using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TGSpawn : MonoBehaviour
{

    //au nombre de ligne de la grappe
    public static int iLigne = 10;


    //permettre de répartition des embrochements [numéro de ligne, interval]
    float[,] fTableParametre = new float[iLigne, 2];


    //iMinEmbronchement >= iLigne
    public static int iMinEmbronchement =30;
    //iMaxEmbronchement >= iMinEmbronchement
    public static int iMaxEmbronchement = 50;


    //tableau de stockage de position de chaque jonction par rapport a chaque ligne [ selection de l'emprochement , parametre désiré]
    //0 =  numéro de la ligne 
    //1 = position X
    //2 = position Y
    float [,] fTablePosition = new float[iMaxEmbronchement, 3];

    public float fEspacementY;
    public float fMaxRandomY;

    public float fEspacementX;
    public float fMaxRandomX;





    public GameObject hCube;


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
        Debug.Log("iEmbronchement :"+ iEmbronchement);
        //////////////////////////////////////////////////////////////////Répartition des embrochements
        //permettra de comté le nombre d’embronchement restant
        int iStockEnbronchement = iEmbronchement;

        
        //permettra de stock le nombre d’embronchement qu’il y aura par ligne
        int[] iTableStockPositionEmbronchement = new int[iLigne];

        int iDebugConte = 0;
        for (int i = 0; i < iLigne; i++)
        {
            //Debug.Log("boucle de for 1");
            if (iStockEnbronchement > 0)
            {
                float fDebugRandom = (float)iEmbronchement * Random.Range(fTableParametre[i, 0], fTableParametre[i, 1])+ 1;
                int iEbrouchementLigne = (int)Mathf.Floor(fDebugRandom);

                Debug.Log("fDebugRandom :" + fDebugRandom);
                Debug.Log("iEbrouchementLigne :" + iEbrouchementLigne);

                if (i == 0 && iEbrouchementLigne==0)
                {

                    iEbrouchementLigne = 1;

                }



                if (iEbrouchementLigne <= iStockEnbronchement)
                {
                    
                    iStockEnbronchement = iStockEnbronchement - iEbrouchementLigne;
                    iTableStockPositionEmbronchement[i] = iEbrouchementLigne;

                }
                else
                {

                    iEbrouchementLigne = iStockEnbronchement;
                    iStockEnbronchement = 0;
                    iTableStockPositionEmbronchement[i] = iEbrouchementLigne;

                }


                iDebugConte = iDebugConte + iEbrouchementLigne;
                Debug.Log("iDebugConte :" + iDebugConte);


            }
            else
            {

                i = iLigne;
        
            }

            if (i==iLigne-1 && iStockEnbronchement>0)
            {
                Debug.Log("re boucle");
                i = 1;

            }

        }

        // positionnement 

        iStockEnbronchement = 0;

        //Debug.Log("1");



        for (int i = 0; i < iLigne; i++)
        {
            //Debug.Log("2");
            for (int j = 0; j < iTableStockPositionEmbronchement[i]; j++)
            {
                //Debug.Log("3");
                fTablePosition[iStockEnbronchement, 0] = iStockEnbronchement;
                fTablePosition[iStockEnbronchement, 2] = (fEspacementY * i + Random.Range(-fMaxRandomY, fMaxRandomY)) * -1;

                if (j == 0)
                {

                    fTablePosition[iStockEnbronchement, 1] = 0 + Random.RandomRange(-fMaxRandomX/2, fMaxRandomX/2);

                }
                else if(j==1 || j == 3 || j == 5 || j == 7 || j == 9 || j == 11 || j == 13 ) // droite 
                {

                    fTablePosition[iStockEnbronchement, 1] = fEspacementX*(j/2+0.5f) + Random.RandomRange(0f, fMaxRandomX);

                }
                else if (j == 2 || j == 4 || j == 6 || j == 8 || j == 10 || j == 12 || j == 14) // gauche
                {

                    fTablePosition[iStockEnbronchement, 1] = (fEspacementX * (j / 2) + Random.RandomRange(-fMaxRandomX, fMaxRandomX)) * -1;

                }


                iStockEnbronchement = iStockEnbronchement + 1;

            }

            if(iStockEnbronchement == iEmbronchement)
            {

                i = iLigne;

            }

        }

        for (int i = 0; i < iEmbronchement; i++)
        {

            Vector3 v3Position = new Vector3(fTablePosition[i, 1], fTablePosition[i, 2], 0f);
            GameObject hPoint = Instantiate(hCube, v3Position, Quaternion.identity);
            hPoint.transform.localScale = new Vector3(0.2f, 0.2f, 0.2f);
        }


        funcDebug(iEmbronchement, iTableStockPositionEmbronchement);

    }

    string TABLO;

    void funcDebug(int num1,int[] tableauStock)
    {
        
        for (int i = 0; i < iLigne; i++)
        {

            TABLO = TABLO + tableauStock[i];
            TABLO = TABLO + "\n";

        }

        TABLO = TABLO + "\n";


        for (int j = 0; j < num1; j++)
        {

            for (int i = 0; i < 3; i++)
            {

                TABLO = TABLO + fTablePosition[j, i];
                TABLO = TABLO + "  |  ";

            }

            TABLO = TABLO + "\n";
        }





        Debug.Log(TABLO);
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
