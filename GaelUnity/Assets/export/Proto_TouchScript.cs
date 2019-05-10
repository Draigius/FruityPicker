using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Proto_TouchScript : MonoBehaviour
{

    GameObject hTouchedObject;
    HingeJoint JointTouched;
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

        // Clic souris
        else if (Input.GetMouseButtonDown(0))
        {
            Debug.Log("Input Click");
            funcTouchReturnObject (Input.mousePosition);
        }

        // Clic souris relaché
        if (Input.GetMouseButton(0))
        {
            funcMoveObject(Input.mousePosition);
        }
    }

    // Sélectionne l'objet
    void funcTouchReturnObject( Vector2 V2ScreenPos )
    {
        RaycastHit hit;

        if (Physics.Raycast(Camera.main.ScreenPointToRay(V2ScreenPos), out hit, 100))
        {

            hTouchedObject = hit.transform.gameObject;
            JointTouched = hTouchedObject.GetComponent<HingeJoint>();

            Debug.Log(hit.transform.gameObject);
        }
    }

    // Deplacer l'objet sélectionné

    void funcMoveObject (Vector2 MousePos)
    {

        hTouchedObject.transform.position = Camera.main.ScreenToWorldPoint(new Vector3(MousePos.x, MousePos.y, 9));

       // Destroy ( JointTouched);
    }
}
