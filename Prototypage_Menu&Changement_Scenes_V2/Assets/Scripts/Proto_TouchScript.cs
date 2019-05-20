using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Proto_TouchScript : MonoBehaviour
{
    // Fruit séléctionné
    GameObject hTouchedObject;
    // Joint du fruit sélectionné
    HingeJoint jTouched;
    // Rigidbody du fruit sélectionné
    public Rigidbody rbTouched;

    // Distance actuelle entre la souris et le fruit sélectionné 
    private float fBreakLimit;

    // Distance entre la souris et le fruit sélectionné pour laquelle le joint se romp
    public float fBreakLimitMax ;

    // Position de l'objet
    private Vector3 v3TouchedObjectPosition;
    // Position de la souris ( et destination de l'objet sélectionné)
    public Vector3 v3MousePosition;
    // Force donné à l'objet pour qu'il atteigne la position de la souris
    private Vector3 v3ForcePosition;

    private float fMaxDragSpeed = 15;

    public bool isDragging = false;

    float oldDrag;

    private string nameScene="";


    ///////////////////////////////////////////////////\\\\\\\\\\\\\\\\\\\\\\\\\\\\\\\\\\\\\\\\\\\\\\\\\\\\\\\\\\
   
    //                                               UPDATE

    ///////////////////////////////////////////////////\\\\\\\\\\\\\\\\\\\\\\\\\\\\\\\\\\\\\\\\\\\\\\\\\\\\\\\\\\
    

    void Update()
    {

        // Clic souris
        if (Input.GetMouseButtonDown(0))
        {
            Debug.Log("Input Click");
            funcTouchReturnObject (Input.mousePosition);

            if (rbTouched!=null)
            {
                isDragging = true;
                rbTouched.useGravity = false;
                oldDrag = rbTouched.drag;
                rbTouched.drag = 10;

            }
            
        }

        // Clic souris maintenu
        if (Input.GetMouseButton(0) && jTouched== null)
        {
            funcGAEL();
        }

        // Clic souris relaché
        if (Input.GetMouseButtonUp(0))
        {
            if (rbTouched != null)
            {
                rbTouched.useGravity = true;
                //RigidbodyTouched.isKinematic = true;

                isDragging = false;

                //Si le lien n'est pas cassé
                if (rbTouched.GetComponent<HingeJoint>() != null)
                {
                    rbTouched.drag = oldDrag;
                }

                else
                {
                    rbTouched.drag = 0;
                }

                hTouchedObject = null;
                rbTouched = null;


                //IL FAUT UNE FONCTION RETOUR SLOW
                //this.GetComponent<DC_Camera>().OnDetach()


            }

        }

        // Debug
        if (Input.GetKeyDown(KeyCode.Space))
        {
            Debug.Log("Position A : " + v3TouchedObjectPosition );
            Debug.Log("Position B : " + v3MousePosition);
            Debug.Log("Force : " + fBreakLimit);     
        }
    }

    //////////////////////////////////////////////// SELECTION DE L'OBJET \\\\\\\\\\\\\\\\\\\\\\\\\\\\\\\\\\\\\\\\\\\\\\\\\\\\\\\
    
    void funcTouchReturnObject( Vector2 V2ScreenPos )
    {
        RaycastHit hit;

        if (Physics.Raycast(Camera.main.ScreenPointToRay(V2ScreenPos), out hit, 100))
        {

            hTouchedObject = hit.transform.gameObject;
            jTouched = hTouchedObject.GetComponent<HingeJoint>();
            rbTouched = hTouchedObject.GetComponent<Rigidbody>();

            //ActualPosition = Camera.main.ScreenToWorldPoint(new Vector3(V2ScreenPos.x, V2ScreenPos.y, 9));
            v3TouchedObjectPosition = rbTouched.position;
            Debug.Log(hit.transform.gameObject);
        }
    }

    //////////////////////////////////////////////// DEPLACEMENT DE L'OBJET \\\\\\\\\\\\\\\\\\\\\\\\\\\\\\\\\\\\\\\\\\\\\\\\\\\\\\\

    private void FixedUpdate()
    {
        if (!isDragging) return;

        // Actualise la position de l'objet.
        v3TouchedObjectPosition = rbTouched.position;
        // Tchek de la position de la souris
        v3MousePosition = Camera.main.ScreenToWorldPoint(new Vector3(Input.mousePosition.x, Input.mousePosition.y, 9));
        // Force qu'il faut donner pour déplacer l'objet séléctionné vers la position de la souris
        v3ForcePosition = v3MousePosition - v3TouchedObjectPosition;

        // Distance à laquelle la souris se trouve de l'objet sélectionné
        fBreakLimit = v3ForcePosition.magnitude;

        // Si la souris est plus loin que la limite fixé, le joint est rompu et on reset la cam
        if (fBreakLimit >= fBreakLimitMax && rbTouched.GetComponent<HingeJoint>() != null)
        {
            jTouched.breakForce = 0;
            this.GetComponent<DC_Camera>().OnDetach();
            if (rbTouched.GetComponent<HingeJoint>().tag == "Kiwi") nameScene = "Levels"; Invoke("changeScene", 1.0f); ;
            if (rbTouched.GetComponent<HingeJoint>().tag == "Poire") nameScene = "Credits"; Invoke("changeScene", 1.0f); ;
            //if (rbTouched.GetComponent<HingeJoint>().tag == "Citron") Application.LoadLevel("Options"); ;

        }

        // Si la velocité de l'objet est trop élevé, on la refixe
        if (rbTouched.velocity.magnitude > fMaxDragSpeed)
        {
            rbTouched.velocity = Vector3.ClampMagnitude(rbTouched.velocity, fMaxDragSpeed);
            
        }
        // Active la force qui doit déplaçer l'objet sélectionné
        rbTouched.AddForce(v3ForcePosition*3.5f, ForceMode.Impulse);





    }

    void changeScene ()
    {
        Application.LoadLevel(nameScene);
    }

    void funcGAEL ()
    {

    }
}
