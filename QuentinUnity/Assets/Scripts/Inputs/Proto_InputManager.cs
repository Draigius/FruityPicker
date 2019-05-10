using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Proto_InputManager : MonoBehaviour
{   
    //Positions de touche initiale et position de touche dynamique sur l'écran
    private Vector2 v2PositionTouchDown, v2PositionTouchUp, v2PositionFirstTouch;
    private Vector3 v3WorldPointStart, v3WorldPointEnd;

    private GameObject hFirstItemTouched, hDynamicItemTouched;

    private bool bIsTouching;

    // Détection Swipe après avoir touché
    [SerializeField]
    public bool bDetectSwipeOnlyAfterRelease = true;

    // Distance minimum en pixels pour le swipe
    [SerializeField]
    private float fMinDistanceForSwipe = 100f;

    //Appelle event OnSwipe (par exemple dans Line Renderer)
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
                    v2PositionFirstTouch = tTouch.position;

                    bIsTouching = true;
                }

                //Mouvement touche
                if (!bDetectSwipeOnlyAfterRelease && tTouch.phase == TouchPhase.Moved)
                {
                    v2PositionTouchDown = tTouch.position;

                    //bIsTouching = false;

                    funcDetectSwipe();
                }

                //fin de la touche
                if (tTouch.phase == TouchPhase.Ended)
                {
                    v2PositionTouchDown = tTouch.position;

                    bIsTouching = false;
                    funcDetectSwipe();
                }
            }
        }
        #endregion
        //
        /*#region Mouse Inputs
        else {
            if (Input.GetMouseButtonDown(0))
            {
                v2PositionTouchUp = Input.mousePosition;
                v2PositionTouchDown = Input.mousePosition;
            }

            //Mouvement touche
            if (!bDetectSwipeOnlyAfterRelease && funcMovedMouseCheck())
            {
                v2PositionTouchDown = Input.mousePosition;
                funcDetectSwipe();
            }

            //fin de la touche
            if (Input.GetMouseButtonUp(0))
            {
                v2PositionTouchDown = Input.mousePosition;
                funcDetectSwipe();
            }
        }
        #endregion*/
    }

    //Fonction détection de Swipe
    private void funcDetectSwipe()
    {
        //Lance Ray Cast 
        hFirstItemTouched = funcTouchReturnObject(v2PositionFirstTouch);
        hDynamicItemTouched = funcTouchReturnObject(v2PositionTouchDown);
    

        //Vérifie la distance du swipe actuel
        if (funcBoolSwipeDistanceCheck())
        {   
            //Vérifie si swipe est vertical ou horizontal

            //Check Vertical
            if (funcBoolIsVerticalSwipe())
            {
                var dirSwipe = v2PositionTouchDown.y - v2PositionTouchUp.y > 0 ? eSwipeDirection.Up : eSwipeDirection.Down;
                funcSendSwipe(dirSwipe, hFirstItemTouched, hDynamicItemTouched);
            }
            else
            //Check Horizontal
            {
                var dirSwipe = v2PositionTouchDown.x - v2PositionTouchUp.x > 0 ? eSwipeDirection.Right : eSwipeDirection.Left;
                funcSendSwipe(dirSwipe, hFirstItemTouched, hDynamicItemTouched);
            }

            //v2PositionTouchUp = v2PositionTouchDown;
        }
    }

    GameObject funcTouchReturnObject(Vector2 V2ScreenPos)
    {
        GameObject hTouchedObject;
        RaycastHit hit;


        if (Physics.Raycast(Camera.main.ScreenPointToRay(V2ScreenPos), out hit, 100))
        {
            hTouchedObject = hit.transform.gameObject;

            //Debug.Log(hTouchedObject.layer.ToString());

            //Collider bob = hTouchedObject.GetComponent<SphereCollider>();
            //string billy = bob.gameObject.layer.ToString();
            //Debug.Log(billy);
            
            
            ///////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////
            //A OPTIMISER
            if (hTouchedObject.layer.ToString() != "9")
            {
                hTouchedObject = null;
            }
            ///////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////
        }
        else
        {
            hTouchedObject = null;
        }

        return hTouchedObject;
    }

    //Envoi du OnSwipe aux autres scripts
    private void funcSendSwipe (eSwipeDirection dirSwipe, GameObject hFirstItemTouched, GameObject hDynamicItemTouched)
    {
        //Nouvelles Données Swipe Data
        SwipeData swipeData = new SwipeData()
        {
            Direction = dirSwipe,
            v2StartPosition = v2PositionTouchDown,
            v2EndPosition = v2PositionTouchUp,

            hFirstTouchedObject = hFirstItemTouched,
            hCurrentTouchedObject = hDynamicItemTouched,

            fDistance3DSwipe = funcDistance3DSwipe(),
            v3RealPositionStart = v3WorldPointStart,
            v3RealPositionEnd = v3WorldPointEnd,

            bTouchDown = bIsTouching
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


    //Vérification dynamique de mouvement de souris
    private bool funcMovedMouseCheck ()
    {
        bool bMouseMoved;
        Vector2 v2PositionTouchNow = Input.mousePosition;

        if (v2PositionTouchDown != v2PositionTouchNow)
        {
            bMouseMoved = true;
        }
        else
        {
            bMouseMoved = false;
        }

        return bMouseMoved;
    }

    private float funcDistance3DSwipe()
    {
        v3WorldPointStart = Camera.main.ScreenToWorldPoint(new Vector3(v2PositionTouchDown.x, v2PositionTouchDown.y, Camera.main.transform.position.z));
        v3WorldPointEnd = Camera.main.ScreenToWorldPoint(new Vector3(v2PositionTouchUp.x, v2PositionTouchUp.y, Camera.main.transform.position.z));

        float fDistance3DSwipe = Vector3.Distance(v3WorldPointStart, v3WorldPointEnd);

        return fDistance3DSwipe;
    }
    #endregion
}

//Struct avec les infos du Swipe
public struct SwipeData
{
    public GameObject hFirstTouchedObject;
    public GameObject hCurrentTouchedObject;
    public Vector2 v2StartPosition;
    public Vector2 v2EndPosition;
    public eSwipeDirection Direction;

    public float fDistance3DSwipe;
    public Vector3 v3RealPositionStart;
    public Vector3 v3RealPositionEnd;

    public bool bTouchDown;
}

//Enum avec les 4 directions possibles du Swipe
public enum eSwipeDirection
{
    Up,
    Down,
    Left,
    Right
}