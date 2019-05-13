using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Proto_TouchScript : MonoBehaviour
{
    // Target Object, Joint, And Rigid body 
    private GameObject hTouchedObject;
    private HingeJoint jJointTouched;
    private Rigidbody rbRigidbodyTouched;
    
    // Positions in space of the object and the target position
    private Vector3 v3ActualPosition;
    private Vector3 v3UpdatePosition;
    private Vector3 v3ForcePosition;

    //Input existant et position on screen
    private Vector2 v2MousePosition;
    private bool bTouchInputDown = false;

    //Forces, Drag et Limites appliquées
    [Header("Proprietés physiques")]
    [SerializeField]
    [Range(1f, 100f)]
    private float fForceApplied = 5f;

    float fOldDrag;
    private float fMaxDragSpeed = 15;
    private float fBreakLimit;
    private float fBreakThreshold = 2f;


    //Check si un objet a été selectionné et est en train d'être déplacé
    bool bIsDragging = false;


    //Se lance quand Un event est appelé
    private void Awake()
    {
        //Call la fonction SwipeDetector_OnSwipe quand l'event  Proto_InputManager.OnSwipe est appelé dans l'input manager (ligne 24) 
        Proto_InputManager.OnSwipe += SwipeDetector_OnSwipe;
    }

    // Update is called once per frame
    void Update()
    {
        // Touch Input présent
        if (bTouchInputDown)
        {
            bIsDragging = true;
            rbRigidbodyTouched.useGravity = false;
            fOldDrag = rbRigidbodyTouched.drag;
            rbRigidbodyTouched.drag = 10;
        }
        // Touch Input absent
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
    }

    //////////////////////////////////////////////// SELECTION DE L'OBJET \\\\\\\\\\\\\\\\\\\\\\\\\\\\\\\\\\\\\\\\\\\\\\\\\\\\\\\ 

    private void SwipeDetector_OnSwipe(SwipeData swipeDataInput)
    {
        if (swipeDataInput.hFirstTouchedObject)
        {
            //Récupère Objet Touché
            hTouchedObject = swipeDataInput.hFirstTouchedObject;
            //Récupère rigid body & hinge à partir de l'objet
            jJointTouched = hTouchedObject.GetComponent<HingeJoint>();
            rbRigidbodyTouched = hTouchedObject.GetComponent<Rigidbody>();


            //Récupère positions dans l'espace du rigidbody et la position sur l'écran du touch
            v3ActualPosition = rbRigidbodyTouched.position;
            v2MousePosition = swipeDataInput.v2EndPosition;

            //Récupère information sur input up ou down
            bTouchInputDown = swipeDataInput.bTouchDown;

        }
        else if (!ReferenceEquals(swipeDataInput.hFirstTouchedObject, swipeDataInput.hCurrentTouchedObject))
        {
            //Récupère information sur input up ou down
            bTouchInputDown = swipeDataInput.bTouchDown;
        }
    }

    //////////////////////////////////////////////// DEPLACEMENT DE L'OBJET \\\\\\\\\\\\\\\\\\\\\\\\\\\\\\\\\\\\\\\\\\\\\\\\\\\\\\\
    
    private void FixedUpdate()
    {
        if (!bIsDragging) return;

        // Actualise la position de l'objet.
        v3ActualPosition = rbRigidbodyTouched.position;
        // Check de la position de la souris
        v3UpdatePosition = Camera.main.ScreenToWorldPoint(new Vector3(v2MousePosition.x, v2MousePosition.y, 9));
        // Force qu'il faut donner pour déplacer l'objet séléctionné vers la position de la souris
        v3ForcePosition = v3UpdatePosition - v3ActualPosition;

        // Distance à laquelle la souris se trouve de l'objet sélectionné
        fBreakLimit = v3ForcePosition.magnitude;

        // Si la souris est plus loin que la limite fixé, le joint est rompu
        if (fBreakLimit >= fBreakThreshold && rbRigidbodyTouched.GetComponent<HingeJoint>() != null) { jJointTouched.breakForce = 0; }

        // Si la velocité de l'objet est trop élevé, on la refixe
        if (rbRigidbodyTouched.velocity.magnitude > fMaxDragSpeed)
        {
            rbRigidbodyTouched.velocity = Vector3.ClampMagnitude(rbRigidbodyTouched.velocity, fMaxDragSpeed);
        }

        // Active la force qui doit déplaçer l'objet sélectionné
        rbRigidbodyTouched.AddForce(v3ForcePosition*fForceApplied, ForceMode.Impulse);
  
    }
}
