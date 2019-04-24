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
            
            Touch touchTouch = Input.GetTouch(0);
            //change position écran en position world

            Vector3 touchVectorThree = new Vector3 (touchTouch.position.x, touchTouch.position.y, Camera.main.transform.position.z);

            Vector3 vTouchPosition = Camera.main.ScreenToWorldPoint(touchVectorThree);

            //Debug.Log("Camera :"+ Camera.main.ScreenToWorldPoint(touchVectorThree));
            //Debug.Log("Camera :"+ Camera.main.ScreenToWorldPoint(touchTouch.position));

            Debug.Log("Trad 3D : "+ vTouchPosition);
            vTouchPosition.z = 0f;
            transform.position = vTouchPosition;


        }
    }
}
