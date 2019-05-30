using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

public class DC_Camera : MonoBehaviour
{


    # region VARIABLES
    public GameObject dummyCam;
    private Vector3 vPosCam;
    private Vector3 TempRotation;


    // Variables mouvement camera lorsqu'un objet est tiré
    private Vector3 initialCamPos;
    private bool bDragging;
    private Rigidbody RBObjet;
    private Vector3 MousePos;
    private Vector3 TempObjectForce;            // Force appliquée lors du déplacement de l'objet
    private Vector3 v3InitPos;

    // Variables rotation en Y
    public int iDeltaAngleY;
    public float fAngleSpeedY;


    // Variables rotation en X
    public int iDeltaAngleX;
    public float fAngleSpeedX;


    // Variables affichage FPS
    public Text HUD_FPS_Text;
    private float CounterTimer = 0;
    private int CounterFPS = 0;


    // Gravité gyroscope
    private float fGyroAccelerationX;
    private float dbX = 0;
    private float fGravityX;
    private float fGravityY;
    public float gravityForce = 9.8f;
    public float fGravitySpeed;

    //[Range (0f, 1f)]
    public float minAngle;


    //[Range(0f, 1f)]
    public float maxAngle;

    #endregion




    // --------------------------------------------------- START ---------------------------------------------------
    void Start()
    {
        Input.gyro.enabled = true;
        vPosCam = Input.gyro.rotationRate;
        initialCamPos = vPosCam;

        // Changement gravité gyro
        fGyroAccelerationX = Input.acceleration.x;
    }



    // --------------------------------------------------- UPDATE ---------------------------------------------------
    void Update()
    {
        // Récupération des valeurs de rotation du gyroscope, et transformation des coordonnées du dummy à l'aide des angles d'euler en vecteur3 "TempRotation"
        vPosCam = Input.gyro.rotationRate;
        TempRotation = dummyCam.transform.eulerAngles;

        TempRotation.y += vPosCam.y * fAngleSpeedY;
        TempRotation.x += vPosCam.x * fAngleSpeedX;


        // Force objet drag
        bDragging = this.GetComponent<Proto_TouchScript>().bIsDragging;
        RBObjet = this.GetComponent<Proto_TouchScript>().rbTouched;
        MousePos = this.GetComponent<Proto_TouchScript>().v3MousePosition;



        TempObjectForce = new Vector3 (0, 0, 0);
        if (bDragging && RBObjet.GetComponent<HingeJoint>() != null)
        {
            if(v3InitPos ==new Vector3 (0,0,0))
            {
                v3InitPos = MousePos;
            }
            //BIZARRE ????§§§?§?§?§?§?§?!!!

            TempObjectForce = new Vector3 (-(v3InitPos.y - MousePos.y), v3InitPos.x - MousePos.x, 0);

            //Debug.Log("INIT  " + v3InitPos);
            //Debug.Log("mousepos  " + MousePos.x);
            //Debug.Log("temps    " + TempObjectForce.x);
        }


        TempRotation = TempRotation + 0.1f*TempObjectForce;



        // Blocage de la rotation en Y de la caméra : Valeur à modifier par le viewport "iDeltaAngleY"
        // Vitesse de la rotation en Y de la caméra : Valeur à modifier par le viewport "fAngleSpeedY"
        if (TempRotation.y > iDeltaAngleY && TempRotation.y < 360 - iDeltaAngleY)
        {
            TempRotation.y -= vPosCam.y * fAngleSpeedY;

            TempRotation = TempRotation - 0.1f * TempObjectForce;
        }

        // Blocage de la rotation en X de la caméra : Valeur à modifier par le viewport "iDeltaAngleX"
        // Vitesse de la rotation en X de la caméra : Valeur à modifier par le viewport "fAngleSpeedX"
        if (TempRotation.x > iDeltaAngleX && TempRotation.x < 360 - iDeltaAngleX)
        {
            TempRotation.x -= vPosCam.x * fAngleSpeedX;
            TempRotation = TempRotation - 0.1f * TempObjectForce;
        }


        // Application de TempRotation sur le transform du Dummy
        dummyCam.transform.eulerAngles = TempRotation;


        //// Affichage FPS 
        //CounterTimer += Time.deltaTime;
        //CounterFPS += 1;

        //if (CounterTimer >= 1)
        //{
        //    CounterTimer = 0;
        //    HUD_FPS_Text.text = "FPS : " + CounterFPS;
        //    CounterFPS = 0;
        //}


    }



    // ----------------------------------------- CHANGMENT GRAVITE ----------------------------------------- 
    void FixedUpdate()
    {
        if (SceneManager.GetActiveScene().name != "Scene_score")
        {
            dbX = 0;
            fGyroAccelerationX = Input.acceleration.x;

            // Limitation angles
            if (Mathf.Abs(fGyroAccelerationX) <= minAngle)
            {
                dbX = 0;
            }
            else if (Mathf.Abs(fGyroAccelerationX) >= maxAngle)
            {
                dbX = maxAngle * (fGyroAccelerationX < 0 ? -1 : 1);
            }
            else
            {
                //Debug.Log("Else : " + fGyroAccelerationX);
                dbX = fGyroAccelerationX * fGravitySpeed;
            }


            // Application gravité 
            fGravityX = dbX * gravityForce;
            fGravityY = -1 * gravityForce;

            Physics.gravity = new Vector2(fGravityX, fGravityY);
        }


    }


    public void OnDetach()
    {

        v3InitPos = new Vector3(0, 0, 0);
        TempObjectForce = new Vector3(0, 0, 0);
        dummyCam.transform.eulerAngles = initialCamPos;
        //dummyCam.transform.eulerAngles = Vector3.MoveTowards(transform.eulerAngles, TempObjectForce, 1);
    }

}
