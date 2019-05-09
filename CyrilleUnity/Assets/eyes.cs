using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class eyes : MonoBehaviour
{
    public Transform reference;
    public float factor = 0.25f;
    public float limit = 0.08f;
    private Vector3 center;
    private Vector3 referencePoint;

    public GameObject hTarget;

    private float timer;
    private float timerLookAround;
    private float delay = 0.12f;
    private float delayLookAround = 0.4f;

    void Start()
    {
        referencePoint = transform.position;
        center = RectTransformUtility.WorldToScreenPoint(Camera.main, transform.position);
    }
    void Update()
    {

        timer += Time.deltaTime;
        timerLookAround += Time.deltaTime;

        if (timer>delay)
        {
            Vector3 mousePos;

            if (Input.GetMouseButton(0) == true || Input.GetMouseButton(1) == true)
            {
                mousePos = Input.mousePosition;

                Vector3 dir = (mousePos - center) * factor;
                dir = Vector3.ClampMagnitude(dir, limit);
                //Debug.Log(dir);
                Vector3 tt = referencePoint + dir;
                transform.position = tt;

                timer = 0;

               
            }
           
            //change la direction du regard automatiquement
            else if(timerLookAround > delayLookAround && Input.GetMouseButton(0) == false || Input.GetMouseButton(1) == false)
            {

                

                Vector3 dir = hTarget.transform.position - transform.position * factor;
               

                dir = Vector3.ClampMagnitude(dir, limit);

                Debug.Log(dir);
                //Debug.Log(dir);
                Vector3 tt = referencePoint + dir;
                transform.position = tt;

                delayLookAround = 0.4f - Random.Range(0, 0.2f);
                timerLookAround = 0;


            }
 

        }
        
    }
}
