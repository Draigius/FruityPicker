using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Proto_TouchScript : MonoBehaviour
{

    GameObject hTouchedObject;
    HingeJoint jTouched;
    Rigidbody rbTouched;

    private float BreakLimit;
    private Vector3 v3TouchedObjectPosition;
    private Vector3 v3MousePosition;

    private Vector3 v3ForcePosition;

    private float fMaxDragSpeed = 15;

    bool isDragging = false;

    float oldDrag;

    // Update is called once per frame
    void Update()
    {
        // Récupère le touch input si il est hold plus de 0 frames
        if (Input.touchCount > 0)
        {

            Debug.Log("Input Touch");
            Touch tTouch = Input.GetTouch(0);

            funcTouchReturnObject (tTouch.position);
        }


        // Clic souris
        else if (Input.GetMouseButtonDown(0))
        {
            Debug.Log("Input Click");
            funcTouchReturnObject (Input.mousePosition);

            isDragging = true;
            rbTouched.useGravity = false;
            oldDrag = rbTouched.drag;
            rbTouched.drag = 10;
        }

        // Clic souris maintenu
        if (Input.GetMouseButton(0))
        {
           // funcMoveObject(Input.mousePosition);

           
        }

        // Clic souris relaché
        if (Input.GetMouseButtonUp(0))
        {
            rbTouched.useGravity = true;
            //RigidbodyTouched.isKinematic = true;
            
            isDragging = false;

            //à condition que le lien ne soit pas cassé
            if (rbTouched.GetComponent<HingeJoint>() != null)
            {
                rbTouched.drag = oldDrag;

            }

            else
            {
                
                rbTouched.drag = 0;
                hTouchedObject = null ;
                rbTouched = null ;
            }
            
        }



        // Debug
        if (Input.GetKeyDown(KeyCode.Space))
        {
            Debug.Log("Position A : " + v3TouchedObjectPosition );
            Debug.Log("Position B : " + v3MousePosition);
            Debug.Log("Force : " + BreakLimit);

            
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

    void funcMoveObject (Vector2 MousePos)
    {
        
    
        // hTouchedObject.transform.position = Camera.main.ScreenToWorldPoint(new Vector3(MousePos.x, MousePos.y, 9));
        // Destroy ( JointTouched);*/
    }

    private void FixedUpdate()
    {
        if (!isDragging) return;

        Vector2 MousePos = Input.mousePosition;
        v3MousePosition = Camera.main.ScreenToWorldPoint(new Vector3(MousePos.x, MousePos.y, 9));
        v3ForcePosition = v3MousePosition - v3TouchedObjectPosition;


        BreakLimit = v3ForcePosition.magnitude;

        //Debug.Log(RigidbodyTouched.velocity.magnitude);

        if (BreakLimit >= 2.5 && rbTouched.GetComponent<HingeJoint>() != null) { jTouched.breakForce = 0; }

        //check vitesse max

        if (rbTouched.velocity.magnitude > fMaxDragSpeed)
        {
            rbTouched.velocity = Vector3.ClampMagnitude(rbTouched.velocity, fMaxDragSpeed);
            //Debug.Log("SLOW");
        }
        
        
        rbTouched.AddForce(v3ForcePosition*3.5f, ForceMode.Impulse);
        v3TouchedObjectPosition = rbTouched.position;
  

        

       


        


    }
}
