using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Q_LineRenderer : MonoBehaviour
{
    private LineRenderer lineRendererComp;

    private float fZOffSet = 1;

    private void Awake()
    {
        lineRendererComp = GetComponent<LineRenderer>();

        Q_SwipeScript.OnSwipe += SwipeDetector_OnSwipe;
    }

    private void SwipeDetector_OnSwipe(SwipeData swipeDataInput)
    {
        Vector3[] v3TablePositions = new Vector3[2];
        v3TablePositions[0] = Camera.main.ScreenToWorldPoint (new Vector3(swipeDataInput.v2StartPosition.x,swipeDataInput.v2StartPosition.y, fZOffSet));
        v3TablePositions[1] = Camera.main.ScreenToWorldPoint (new Vector3(swipeDataInput.v2EndPosition.x,swipeDataInput.v2EndPosition.y, fZOffSet));

        lineRendererComp.positionCount = 2;
        lineRendererComp.SetPositions(v3TablePositions);
    }

}
