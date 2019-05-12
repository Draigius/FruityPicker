using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Proto_SwipeLogger : MonoBehaviour
{
    [Header("Debug Settings")]
    public bool bShowDirection;
    public bool bShowFirstHitObject;
    public bool bShowCurrentTouchedObject;
    public bool bShowDistanceUnits;
    public bool bShowTouch;

    private void Update()
    {
        
    }

    private void Awake()
    {
        Proto_InputManager.OnSwipe += SwipeDetector_OnSwipe;
    }

    private void SwipeDetector_OnSwipe (SwipeData swipeDataInput)
    {

        if (bShowDirection)
        {
            Debug.Log("Swipe in Direction : " + swipeDataInput.Direction);
        }

        if (bShowFirstHitObject)
        {
            Debug.Log("First touched Object  : " + swipeDataInput.hFirstTouchedObject);
        }

        if (bShowCurrentTouchedObject)
        {
            Debug.Log("Current touched Object : " + swipeDataInput.hCurrentTouchedObject);
        }

        if (bShowDistanceUnits)
        {
            Debug.Log("Distance BetweenObjects  : " + swipeDataInput.fDistance3DSwipe);
        }
    }
}
