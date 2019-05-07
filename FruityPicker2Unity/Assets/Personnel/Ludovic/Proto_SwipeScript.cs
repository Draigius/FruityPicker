using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Proto_SwipeScript : MonoBehaviour
{   
    //Positions de touche initiale et position de touche dynamique sur l'écran
    private Vector2 v2PositionTouchDown, v2PositionTouchUp;

    // Détection Swipe après avoir touché
    [SerializeField]
    public bool bDetectSwipeOnlyAfterRelease = true;

    // Distance minimum en pixels pour le swipe
    [SerializeField]
    private float fMinDistanceForSwipe = 100f;

    //Appelle event OnSwipe (dans Line Renderer)
    public static event System.Action<SwipeData> OnSwipe = delegate { };

    // Update is called once per frame
    private void Update()
    {
        //
        #region Mobile Inputs

        if (Input.touchCount > 0)
        {
            //Prend en compte Multi-touches
            foreach (Touch tTouch in Input.touches)
            {
                //début de la touche
                if (tTouch.phase == TouchPhase.Began)
                {
                    v2PositionTouchUp = tTouch.position;
                    v2PositionTouchDown = tTouch.position;
                }

                //Mouvement touche
                if (!bDetectSwipeOnlyAfterRelease && tTouch.phase == TouchPhase.Moved)
                {
                    v2PositionTouchDown = tTouch.position;
                    funcDetectSwipe();
                }

                //fin de la touche
                if (tTouch.phase == TouchPhase.Ended)
                {
                    v2PositionTouchDown = tTouch.position;
                    funcDetectSwipe();
                }
            }
        }
        #endregion
        //
        #region Mouse Inputs
        else if (Input.GetMouseButton(0)){

            if (Input.GetMouseButtonDown(0))
            {
                v2PositionTouchUp = Input.mousePosition;
                v2PositionTouchDown = Input.mousePosition;
            }

            //fin de la touche
            if (Input.GetMouseButtonUp(0))
            {
                v2PositionTouchDown = Input.mousePosition;
                funcDetectSwipe();
            }
        }
        #endregion
    }

    //Fonction détection de Swipe
    private void funcDetectSwipe()
    {
        //Vérifie la distance du swipe actuel
        if (funcBoolSwipeDistanceCheck())
        {   
            //Vérifie si swipe est vertical ou horizontal

            //Check Vertical
            if (funcBoolIsVerticalSwipe())
            {
                var dirSwipe = v2PositionTouchDown.y - v2PositionTouchUp.y > 0 ? eSwipeDirection.Up : eSwipeDirection.Down;
                funcSendSwipe(dirSwipe);
            }
            else
            //Check Vertical
            {
                var dirSwipe = v2PositionTouchDown.x - v2PositionTouchUp.x > 0 ? eSwipeDirection.Right : eSwipeDirection.Left;
                funcSendSwipe(dirSwipe);
            }

            //v2PositionTouchUp = v2PositionTouchDown;
        }
    }

    //Envoi du OnSwipe aux autres scripts
    private void funcSendSwipe (eSwipeDirection dirSwipe)
    {
        //Nouvelles Données Swipe Data
        SwipeData swipeData = new SwipeData()
        {
            Direction = dirSwipe,
            v2StartPosition = v2PositionTouchDown,
            v2EndPosition = v2PositionTouchUp
        };
        OnSwipe(swipeData);

    }

    #region Check Functions
    //Check si Swipe est Vertical ou non
    private bool funcBoolIsVerticalSwipe()
    {
        //Compare la distance verticale et la distance  horizontale entre Point A et Point B 
        return funcFloatVerticalMoveDist() > funcFloatHorizontalMoveDist();
    }

    //Check Longueur Minimum Swipe
    private bool funcBoolSwipeDistanceCheck ()
    {
        //Check si la distance de Swipe est supérieure à la distance minimum de swipe
        return funcFloatVerticalMoveDist() > fMinDistanceForSwipe || funcFloatHorizontalMoveDist() > fMinDistanceForSwipe;
    }

    //Check Longueur Swipe Verticale
    private float funcFloatVerticalMoveDist()
    {
        return Mathf.Abs(v2PositionTouchDown.y - v2PositionTouchUp.y);
    }

    //Check Longueur Swipe Horizontale
    private float funcFloatHorizontalMoveDist()
    {
        return Mathf.Abs(v2PositionTouchDown.x - v2PositionTouchUp.x);
    }

    #endregion
}

//Struct avec les infos du Swipe
public struct SwipeData
{
    public Vector2 v2StartPosition;
    public Vector2 v2EndPosition;
    public eSwipeDirection Direction;
}

//Enum avec les 4 directions possibles du Swipe
public enum eSwipeDirection
{
    Up,
    Down,
    Left,
    Right
}