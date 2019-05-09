using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Proto_TouchScript : MonoBehaviour
{

    GameObject hTouchedObject;
    HingeJoint JointTouched;
    Rigidbody RigidbodyTouched;

    private float BreakLimit;
    private Vector3 ActualPosition;
    private Vector3 UpdatePosition;

    private Vector3 ForcePosition;

    private float maxDragSpeed = 15;

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
            RigidbodyTouched.useGravity = false;
            oldDrag = RigidbodyTouched.drag;
            RigidbodyTouched.drag = 10;
        }

        // Clic souris maintenu
        if (Input.GetMouseButton(0))
        {
           // funcMoveObject(Input.mousePosition);

           
        }

        // Clic souris relaché
        if (Input.GetMouseButtonUp(0))
        {
            RigidbodyTouched.useGravity = true;
            //RigidbodyTouched.isKinematic = true;
            
            isDragging = false;

            //à condition que le lien ne soit pas cassé
            if (RigidbodyTouched.GetComponent<HingeJoint>() != null)
            {
                RigidbodyTouched.drag = oldDrag;

            }
            else
            {

                RigidbodyTouched.drag = 0;
            }
            
        }



        // Debug
        if (Input.GetKeyDown(KeyCode.Space))
        {
            Debug.Log("Position A : " + ActualPosition );
            Debug.Log("Position B : " + UpdatePosition);
            Debug.Log("Force : " + ForcePosition);
        }
    }

    //////////////////////////////////////////////// SELECTION DE L'OBJET \\\\\\\\\\\\\\\\\\\\\\\\\\\\\\\\\\\\\\\\\\\\\\\\\\\\\\\
    
    void funcTouchReturnObject( Vector2 V2ScreenPos )
    {
        RaycastHit hit;

        if (Physics.Raycast(Camera.main.ScreenPointToRay(V2ScreenPos), out hit, 100))
        {

            hTouchedObject = hit.transform.gameObject;
            JointTouched = hTouchedObject.GetComponent<HingeJoint>();
            RigidbodyTouched = hTouchedObject.GetComponent<Rigidbody>();

            //ActualPosition = Camera.main.ScreenToWorldPoint(new Vector3(V2ScreenPos.x, V2ScreenPos.y, 9));
            ActualPosition = RigidbodyTouched.position;
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
        UpdatePosition = Camera.main.ScreenToWorldPoint(new Vector3(MousePos.x, MousePos.y, 9));
        ForcePosition = UpdatePosition - ActualPosition;


        BreakLimit = Vector2.Distance(ActualPosition, ForcePosition);

        //Debug.Log(RigidbodyTouched.velocity.magnitude);

        if (BreakLimit >= 2.5 && RigidbodyTouched.GetComponent<HingeJoint>() != null) { JointTouched.breakForce = 0; }

        //check vitesse max

        if (RigidbodyTouched.velocity.magnitude > maxDragSpeed)
        {
            RigidbodyTouched.velocity = Vector3.ClampMagnitude(RigidbodyTouched.velocity, maxDragSpeed);
            //Debug.Log("SLOW");
        }
        
        
        RigidbodyTouched.AddForce(ForcePosition*3.5f, ForceMode.Impulse);
        ActualPosition = RigidbodyTouched.position;
        ActualPosition = RigidbodyTouched.position;

        

       


        


    }
}
