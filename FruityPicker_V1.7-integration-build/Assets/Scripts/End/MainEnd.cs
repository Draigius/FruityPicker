using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MainEnd : MonoBehaviour
{

    public float iEtape = 0;

    public int iFruitsPositif;
    public int iFruitsNegatif;

    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        if(iEtape == 0)
        {

            //rideau qui ce lève

            //spawn des fruits
            iEtape = 0.5f;
        }

        if(iEtape == 1)//quand tous les fruis sont tombé
        {

            // spawn du capuchon présoir rotatif

        }

        if (iEtape == 2)//quand capuchont rotatif est en position
        {
            //autorisé le joueur a faire le slide


        }

        if (iEtape == 3)//quand le slide et fini
        {

            //faire le mouvement de caméra


        }

        if (iEtape == 4)//quand la caméra est en position final ou pas loin de sa position final
        {

            //faire tourné de 3/4 le roubinet


        }

        if (iEtape == 5)//quand la caméra est en position final ou pas loin de sa position final
        {

            //faire tourné de 3/4 le roubinet


        }

        if (iEtape == 6)//quand robinet a presque fini de tournet
        {

            //faire couler le fluide


        }

        if (iEtape == 7)//quand robinet a presque fini de tournet
        {

            //faire couler le fluide
            //spawn du cylindre multy fruit bon puis /scale vers le haut


        }

        if (iEtape == 8)//quand le scale bon atteint la taill demander
        {

            //arrété l'écoulement du fluide
            //spawn du cylindre multy fruit mauvéééééé / puis scale vers le bas


        }

        if (iEtape == 9)//quand le scale mauvais atteint la taill demander
        {

           // affichage dinamique des score
           // réaction de l'étiquette


        }
    }
}
