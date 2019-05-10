using System.Collections;
using System.Collections.Generic;
using UnityEngine;


public class DCMain : MonoBehaviour
{

    # region VARIABLES
    public GameObject dummyCam;
    private Vector3 vPosCam;

    // Variables rotation en Y
    public int iDeltaAngleY;
    public float fAngleSpeedY;


    // Variables rotation en X
    public int iDeltaAngleX;
    public float fAngleSpeedX;

    #endregion


    // --------------------------------------------------- START ---------------------------------------------------
    void Start()
    {
        Input.gyro.enabled = true;
        vPosCam = Input.gyro.rotationRate;
    }



    // --------------------------------------------------- UPDATE ---------------------------------------------------
    void Update()
    {
        // Récupération des valeurs de rotation du gyroscope, et transformation des coordonnées du dummy à l'aide des angles d'euler en vecteur3 "TempRotation"
        vPosCam = Input.gyro.rotationRate;
        Vector3 TempRotation = dummyCam.transform.eulerAngles;

        TempRotation.y += vPosCam.y * fAngleSpeedY;
        TempRotation.x += vPosCam.x * fAngleSpeedX;


        // Blocage de la rotation en Y de la caméra : Valeur à modifier par le viewport "iDeltaAngleY"
        // Vitesse de la rotation en Y de la caméra : Valeur à modifier par le viewport "fAngleSpeedY"
        if (TempRotation.y > iDeltaAngleY && TempRotation.y < 360 - iDeltaAngleY)
        {
            TempRotation.y -= vPosCam.y * fAngleSpeedY;
        }

        // Blocage de la rotation en X de la caméra : Valeur à modifier par le viewport "iDeltaAngleX"
        // Vitesse de la rotation en X de la caméra : Valeur à modifier par le viewport "fAngleSpeedX"
        if (TempRotation.x > iDeltaAngleX && TempRotation.x < 360 - iDeltaAngleX)
        {
            TempRotation.x -= vPosCam.x * fAngleSpeedX;
        }


        // Application de TempRotation sur le transform du Dummy
        dummyCam.transform.eulerAngles = TempRotation;


    }




}
