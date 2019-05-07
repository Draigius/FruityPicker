using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Proto_SwipeLogger : MonoBehaviour
{
    private void Awake()
    {
        Proto_SwipeScript.OnSwipe += SwipeDetector_OnSwipe;
    }

    private void SwipeDetector_OnSwipe (SwipeData swipeDataInput)
    {
        Debug.Log("Swipe in Direction : " + swipeDataInput.Direction);

    }
}
