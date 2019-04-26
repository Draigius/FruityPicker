using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Q_SwipeScript : MonoBehaviour
{
    private Vector2 v2PositionTouchDown, v2PositionTouchUp;

    [SerializeField]
    public bool bDetectSwipeOnlyAfterRelease = false;

    [SerializeField]
    private float fMinDistanceForSwipe = 20f;

    public static event System.Action<SwipeData> OnSwipe = delegate { };

    // Update is called once per frame
    private void Update()
    { 
        #region Mobile Inputs

        foreach (Touch tTouch in Input.touches)
        {
            if (tTouch.phase == TouchPhase.Began)
            {
                v2PositionTouchUp = tTouch.position;
                v2PositionTouchDown = tTouch.position;
            }

            if (!bDetectSwipeOnlyAfterRelease && tTouch.phase == TouchPhase.Moved)
            {
                v2PositionTouchDown = tTouch.position;
                funcDetectSwipe();
            }

            if (tTouch.phase == TouchPhase.Ended)
            {
                v2PositionTouchDown = tTouch.position;
                funcDetectSwipe();
            }
        }
        #endregion
    }

    private void funcDetectSwipe()
    {
        if (funcBoolSwipeDistanceCheck())
        {
            if (funcBoolIsVerticalSwipe())
            {
                var dirSwipe = v2PositionTouchDown.y - v2PositionTouchUp.y > 0 ? SwipeDirection.Up : SwipeDirection.Down;
                funcSendSwipe(dirSwipe);
            }
            else
            {
                var dirSwipe = v2PositionTouchDown.x - v2PositionTouchUp.x > 0 ? SwipeDirection.Right : SwipeDirection.Left;
                funcSendSwipe(dirSwipe);
            }
            //v2PositionTouchUp = v2PositionTouchDown;
        }
    }

    private void funcSendSwipe (SwipeDirection dirSwipe)
    {
        SwipeData swipeData = new SwipeData()
        {
            Direction = dirSwipe,
            v2StartPosition = v2PositionTouchDown,
            v2EndPosition = v2PositionTouchUp
        };
        OnSwipe(swipeData);

    }

    #region Check Functions
    private bool funcBoolIsVerticalSwipe()
    {
        return funcFloatVerticalMoveDist() > funcFloatHorizontalMoveDist();
    }

    private bool funcBoolSwipeDistanceCheck ()
    {
        return funcFloatVerticalMoveDist() > fMinDistanceForSwipe || funcFloatHorizontalMoveDist() > fMinDistanceForSwipe;
    }

    private float funcFloatVerticalMoveDist()
    {
        return Mathf.Abs(v2PositionTouchDown.y - v2PositionTouchUp.y);
    }


    private float funcFloatHorizontalMoveDist()
    {
        return Mathf.Abs(v2PositionTouchDown.x - v2PositionTouchUp.x);
    }

    #endregion
}

public struct SwipeData
{
    public Vector2 v2StartPosition;
    public Vector2 v2EndPosition;
    public SwipeDirection Direction;
}

public enum SwipeDirection
{
    Up,
    Down,
    Left,
    Right
}