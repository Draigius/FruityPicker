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
    Rigidbody rbTouched;

    // Distance actuelle entre la souris et le fruit sélectionné 
    private float fBreakLimit;

    // Distance entre la souris et le fruit sélectionné pour laquelle le joint se romp
    public float fBreakLimitMax =  2;

    // Position de l'objet
    private Vector3 v3TouchedObjectPosition;
    // Position de la souris ( et destination de l'objet sélectionné)
    private Vector3 v3MousePosition;
    // Force donné à l'objet pour qu'il atteigne la position de la souris
    private Vector3 v3ForcePosition;

    private float fMaxDragSpeed = 15;

    private bool bIsDragging = false;
    private bool bFoundObject = false;

    private float oldDrag;


    ///////////////////////////////////////////////////\\\\\\\\\\\\\\\\\\\\\\\\\\\\\\\\\\\\\\\\\\\\\\\\\\\\\\\\\\
   
    //                                               UPDATE

    ///////////////////////////////////////////////////\\\\\\\\\\\\\\\\\\\\\\\\\\\\\\\\\\\\\\\\\\\\\\\\\\\\\\\\\\
    

    void Update()
    {

        // Clic souris
        if (Input.GetMouseButtonDown(0))
        {
            if (funcTouchReturnObject(Input.mousePosition))
            {
                bIsDragging = true;
                rbTouched.useGravity = false;
                oldDrag = rbTouched.drag;
                rbTouched.drag = 10;
            }
        }

        // Clic souris maintenu
        if (Input.GetMouseButton(0) && jTouched== null)
        {
            //funcGAEL();
        }

        // Clic souris relaché
        if (Input.GetMouseButtonUp(0) && bFoundObject)
        {
            rbTouched.useGravity = true;
            //RigidbodyTouched.isKinematic = true;

            bIsDragging = false;

            //Si le lien n'est pas cassé
            if (rbTouched.GetComponent<HingeJoint>() != null)
            {
                rbTouched.drag = oldDrag;
            }

            else
            {
                rbTouched.drag = 0;
            }

            bFoundObject = false;
            hTouchedObject = null;
            rbTouched = null;

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
    
    bool funcTouchReturnObject( Vector2 V2ScreenPos )
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

            return bFoundObject = true;
        }
        else
        {
            return bFoundObject = false;
        }
    }

    //////////////////////////////////////////////// DEPLACEMENT DE L'OBJET \\\\\\\\\\\\\\\\\\\\\\\\\\\\\\\\\\\\\\\\\\\\\\\\\\\\\\\

    private void FixedUpdate()
    {
        if (!bIsDragging) return;

        // Actualise la position de l'objet.
        v3TouchedObjectPosition = rbTouched.position;
        // Tchek de la position de la souris
        v3MousePosition = Camera.main.ScreenToWorldPoint(new Vector3(Input.mousePosition.x, Input.mousePosition.y, 9));
        // Force qu'il faut donner pour déplacer l'objet séléctionné vers la position de la souris
        v3ForcePosition = v3MousePosition - v3TouchedObjectPosition;

        // Distance à laquelle la souris se trouve de l'objet sélectionné
        fBreakLimit = v3ForcePosition.magnitude;

        // Si la souris est plus loin que la limite fixé, le joint est rompu
        if (fBreakLimit >= fBreakLimitMax && rbTouched.GetComponent<HingeJoint>() != null) { jTouched.breakForce = 0; }

        // Si la velocité de l'objet est trop élevé, on la refixe
        if (rbTouched.velocity.magnitude > fMaxDragSpeed)
        {
            rbTouched.velocity = Vector3.ClampMagnitude(rbTouched.velocity, fMaxDragSpeed);
            
        }
        // Active la force qui doit déplaçer l'objet sélectionné
        rbTouched.AddForce(v3ForcePosition*3.5f, ForceMode.Impulse);
    }
}
