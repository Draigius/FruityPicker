using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Proto_TouchScript : MonoBehaviour
{

    private GameObject hTouchedObject;
    private HingeJoint jJointTouched;
    private Rigidbody rbRigidbodyTouched;

    private float fBreakLimit;
    private Vector3 v3ActualPosition;
    private Vector3 v3UpdatePosition;

    private Vector3 v3ForcePosition;

    private Vector2 v2MousePosition;
    private bool bTouchDown = false;
    private bool bTouchUp = false;

    private float fMaxDragSpeed = 15;

    bool bIsDragging = false;

    float fOldDrag;

    private void Awake()
    {
        Proto_InputManager.OnSwipe += SwipeDetector_OnSwipe;
    }

    // Update is called once per frame
    void Update()
    {
        // Clic souris
        if (bTouchDown)
        {
            Debug.Log("Input Click");

            bIsDragging = true;
            rbRigidbodyTouched.useGravity = false;
            fOldDrag = rbRigidbodyTouched.drag;
            rbRigidbodyTouched.drag = 10;
        }

        // Clic souris relaché
        if (!bTouchDown)
        {
            if (rbRigidbodyTouched)
            {
                rbRigidbodyTouched.useGravity = true;

                bIsDragging = false;

                //à condition que le lien ne soit pas cassé
                if (rbRigidbodyTouched.GetComponent<HingeJoint>() != null)
                {
                    rbRigidbodyTouched.drag = fOldDrag;
                }
                else
                {
                    rbRigidbodyTouched.drag = 0;
                }
            }
            //RigidbodyTouched.isKinematic = true;
        }
    }

    //////////////////////////////////////////////// SELECTION DE L'OBJET \\\\\\\\\\\\\\\\\\\\\\\\\\\\\\\\\\\\\\\\\\\\\\\\\\\\\\\ 

    private void SwipeDetector_OnSwipe(SwipeData swipeDataInput)
    {
        if (swipeDataInput.hFirstTouchedObject)
        {
            hTouchedObject = swipeDataInput.hFirstTouchedObject;
            jJointTouched = hTouchedObject.GetComponent<HingeJoint>();
            rbRigidbodyTouched = hTouchedObject.GetComponent<Rigidbody>();

            v3ActualPosition = rbRigidbodyTouched.position;
            v2MousePosition = swipeDataInput.v2EndPosition;

            bTouchDown = swipeDataInput.bTouchDown;
        }
    }

    //////////////////////////////////////////////// DEPLACEMENT DE L'OBJET \\\\\\\\\\\\\\\\\\\\\\\\\\\\\\\\\\\\\\\\\\\\\\\\\\\\\\\
    
    private void FixedUpdate()
    {
        if (!bIsDragging) return;

        Vector2 MousePos = Input.mousePosition;
        v3UpdatePosition = Camera.main.ScreenToWorldPoint(new Vector3(MousePos.x, MousePos.y, 9));
        v3ForcePosition = v3UpdatePosition - v3ActualPosition;


        fBreakLimit = v3ForcePosition.magnitude;

        if (fBreakLimit >= 2.5 && rbRigidbodyTouched.GetComponent<HingeJoint>() != null) { jJointTouched.breakForce = 0; }

        //check vitesse max

        if (rbRigidbodyTouched.velocity.magnitude > fMaxDragSpeed)
        {
            rbRigidbodyTouched.velocity = Vector3.ClampMagnitude(rbRigidbodyTouched.velocity, fMaxDragSpeed);
        }
        
        
        rbRigidbodyTouched.AddForce(v3ForcePosition*3.5f, ForceMode.Impulse);
        v3ActualPosition = rbRigidbodyTouched.position;
  
    }
}
