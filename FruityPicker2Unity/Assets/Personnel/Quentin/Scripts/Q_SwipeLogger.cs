using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Q_SwipeLogger : MonoBehaviour
{
    private void Awake()
    {
        Q_SwipeScript.OnSwipe += SwipeDetector_OnSwipe;
    }

    private void SwipeDetector_OnSwipe (SwipeData swipeDataInput)
    {
        Debug.Log("Swipe in Direction : " + swipeDataInput.Direction);

    }
}
