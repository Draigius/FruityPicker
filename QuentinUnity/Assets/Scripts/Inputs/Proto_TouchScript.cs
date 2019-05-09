using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Proto_TouchScript : MonoBehaviour
{

    // Update is called once per frame
    void Update()
    {
        // Récupère le touch input si il est hold plus de 0 frames
        if (Input.touchCount > 0)
        {

            Debug.Log("Input Touch");
            Touch tTouch = Input.GetTouch(0);

            funcTouchReturnObject (tTouch.position);
        }
        else if (Input.GetMouseButtonDown(0))
        {
            Debug.Log("Input Click");
            funcTouchReturnObject (Input.mousePosition);
        }
           
    }

    void funcTouchReturnObject( Vector2 V2ScreenPos )
    {
        RaycastHit hit;

        if (Physics.Raycast(Camera.main.ScreenPointToRay(V2ScreenPos), out hit, 100))
        {

            GameObject hTouchedObject = hit.transform.gameObject;
        
            Debug.Log(hit.transform.gameObject);
        }
    }
}
