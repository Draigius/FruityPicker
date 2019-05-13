using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Proto_LineRenderer : MonoBehaviour
{
    // Composant Line Renderer
    private LineRenderer lineRendererComp;

    //Offset en z Par rapport à là caméra pour donner la profondeur de placement de la ligne
    private float fZOffSet = 1;

    //Se lance quand Un event est appelé
    private void Awake()
    {
        //récupère le Line Renderer sur le mesh
        lineRendererComp = GetComponent<LineRenderer>();

        //Call la fonction SwipeDetector_OnSwipe quand l'event  Proto_InputManager.OnSwipe est appelé dans l'input manager (ligne 24) 
        Proto_InputManager.OnSwipe += SwipeDetector_OnSwipe;
    }

    //Fonction affichage de la ligne avec en paramètre la data de l'input
    private void SwipeDetector_OnSwipe(SwipeData swipeDataInput)
    {
        //Si il y a un input actif, affiche la ligne
        if (swipeDataInput.bTouchDown)
        {
            lineRendererComp.enabled = true;
            Vector3[] v3TablePositions = new Vector3[2];
            v3TablePositions[0] = Camera.main.ScreenToWorldPoint(new Vector3(swipeDataInput.v2StartPosition.x, swipeDataInput.v2StartPosition.y, fZOffSet));
            v3TablePositions[1] = Camera.main.ScreenToWorldPoint(new Vector3(swipeDataInput.v2EndPosition.x, swipeDataInput.v2EndPosition.y, fZOffSet));

            lineRendererComp.positionCount = 2;
            lineRendererComp.SetPositions(v3TablePositions);
        }
        //Sinon, cache la ligne
        else
        {
            lineRendererComp.enabled=false;
        }
        
    }

}
