using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Q_TouchScript : MonoBehaviour
{

    // Update is called once per frame
    void Update()
    {
        // Récupère le touch input si il est hold plus de 0 frames
        if (Input.touchCount > 0)
        {
            
            Touch tTouch = Input.GetTouch(0);
            //change position écran en position world
            RaycastHit hit;

            if (Physics.Raycast(Camera.main.ScreenPointToRay(tTouch.position), out hit, 1000))
            {
                Debug.Log("MoveToClick_Script - Found Point");
                Vector3 tRayPoint =  hit.point;

                tRayPoint.z = 0;

                transform.position = tRayPoint;
            }

            //Vector3 touchVectorThree = new Vector3 (touchTouch.position.x, touchTouch.position.y, Camera.main.transform.position.z);
            //Vector3 vTouchPosition = Camera.main.ScreenToWorldPoint(touchVectorThree);

            //Debug.Log("Trad 3D : "+ vTouchPosition);
            //vTouchPosition.z = 0f;
            //transform.position = vTouchPosition;

           
        
        }
    }
}
