using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FruitBehavior : MonoBehaviour
{
    private Rigidbody rbFruit;

    [Header ("Proprietés physiques")]
    [SerializeField]
    [Range(0.1f,1f)]
    private float fForceApplique = 0.5f;

    [SerializeField]
    [Range(1, 5)]
    private int iMassFruit = 1;

    // Start is called before the first frame update
    void Start()
    {
        rbFruit = GetComponent<Rigidbody>();
        rbFruit.mass = iMassFruit;
    }

    private void Awake()
    {
        

        Proto_InputManager.OnSwipe += SwipeDetector_OnSwipe;
    }

    private void SwipeDetector_OnSwipe(SwipeData swipeDataInput)
    {
        rbFruit.AddForce(swipeDataInput.v2EndPosition*fForceApplique);
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
