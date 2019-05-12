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
    private bool bTouchInputDown = false;

    [Header("Proprietés physiques")]
    [SerializeField]
    [Range(1f, 100f)]
    private float fForceApplied = 20f;

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
        // Touch Input
        if (bTouchInputDown)
        {
            bIsDragging = true;
            rbRigidbodyTouched.useGravity = false;
            fOldDrag = rbRigidbodyTouched.drag;
            rbRigidbodyTouched.drag = 10;
            
            rbRigidbodyTouched.gameObject.GetComponent<MeshRenderer>().material.color = new Color(0.3f, 0.4f, 0.6f);
        }
        else
        {
            if (rbRigidbodyTouched != null)
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

        Debug.Log("Touching Object : " +  bTouchInputDown);
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

            bTouchInputDown = swipeDataInput.bTouchDown;
        } else if (!ReferenceEquals(swipeDataInput.hFirstTouchedObject, swipeDataInput.hCurrentTouchedObject))
        {
            bTouchInputDown = swipeDataInput.bTouchDown;
        }
    }

    //////////////////////////////////////////////// DEPLACEMENT DE L'OBJET \\\\\\\\\\\\\\\\\\\\\\\\\\\\\\\\\\\\\\\\\\\\\\\\\\\\\\\
    
    private void FixedUpdate()
    {
        if (!bIsDragging) return;

        v3UpdatePosition = Camera.main.ScreenToWorldPoint(new Vector3(v2MousePosition.x, v2MousePosition.y, 9));
        v3ForcePosition = v3UpdatePosition - v3ActualPosition;


        fBreakLimit = v3ForcePosition.magnitude;

        if (fBreakLimit >= 2.5 && rbRigidbodyTouched.GetComponent<HingeJoint>() != null) { jJointTouched.breakForce = 0; }

        //check vitesse max

        if (rbRigidbodyTouched.velocity.magnitude > fMaxDragSpeed)
        {
            rbRigidbodyTouched.velocity = Vector3.ClampMagnitude(rbRigidbodyTouched.velocity, fMaxDragSpeed);
        }
        
        
        rbRigidbodyTouched.AddForce(v3ForcePosition*fForceApplied, ForceMode.Force);
        v3ActualPosition = rbRigidbodyTouched.position;
  
    }
}
