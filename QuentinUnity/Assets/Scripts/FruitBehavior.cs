using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FruitBehavior : MonoBehaviour
{
    private GameObject hFruit;
    private Rigidbody rbFruit;

    [Header ("Proprietés physiques")]
    [SerializeField]
    [Range(0.1f,100f)]
    private float fForceApplique = 50f;

    [SerializeField]
    [Range(1, 5)]
    private int iMassFruit = 1;

    [SerializeField]
    [Range(0f, 50f)]
    private int iDragFruit = 1;

    // Start is called before the first frame update
    void Start()
    {
        hFruit = gameObject;
        rbFruit = GetComponent<Rigidbody>();
        rbFruit.mass = iMassFruit;
        rbFruit.drag = iDragFruit;
    }

    private void Awake()
    {
        

        Proto_InputManager.OnSwipe += SwipeDetector_OnSwipe;
    }

    private void SwipeDetector_OnSwipe(SwipeData swipeDataInput)
    {
        if (swipeDataInput.hFirstTouchedObject == hFruit){
            rbFruit.AddForce((hFruit.transform.position - swipeDataInput.v3RealPositionEnd).normalized * fForceApplique, ForceMode.Impulse);
        }
        //rbFruit.AddForce(swipeDataInput.v3RealPositionEnd.normalized * fForceApplique,ForceMode.Force);
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
