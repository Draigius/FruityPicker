using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TransitionScript : MonoBehaviour
{
    private Vector3 v3PositionActuelle;
    private Vector3 v3PositionTarget;
    private Vector3 v3PositionStart;

    private bool bStart = false;
    private bool bEnd = false;

    [SerializeField]
    private float fOffsetTransitionZ = 1.5f;
    [SerializeField]
    private float fOffsetTransitionY = 2.5f;
    private float fVitesse = 2f;
    private float fDistanceCorrection;

    // Start is called before the first frame update
    void Start()
    {
        gameObject.transform.position = new Vector3(Camera.main.transform.position.x, Camera.main.transform.position.y, Camera.main.transform.position.z + fOffsetTransitionZ);

        v3PositionStart = gameObject.transform.position;
        v3PositionTarget = new Vector3(v3PositionStart.x, v3PositionStart.y + fOffsetTransitionY, v3PositionStart.z);

        bStart = true;
    }

    // Update is called once per frame
    void Update()
    {
        float fStep = fVitesse * Time.deltaTime;
        v3PositionActuelle = gameObject.transform.position;

        ////////////////// START
        if (bStart && v3PositionActuelle != v3PositionTarget)
        {
            transform.position = Vector3.MoveTowards(transform.position, v3PositionTarget, fStep);
        }
        else if (bStart && v3PositionActuelle == v3PositionTarget)
        {
            //Debug.Log("truite");
            bStart = false;
        }

        ////////////////// End
        if (bEnd && v3PositionActuelle != v3PositionStart)
        {
            transform.position = Vector3.MoveTowards(transform.position, v3PositionStart, fStep);
        }
        else if (bEnd && v3PositionActuelle == v3PositionStart)
        {
            //Debug.Log("fumé");
            bEnd = false;
        }

    }

    //Func Activation End
    public void funcActivateEnd()
    {
        bEnd = true;
    }

    public void funcActivateStart()
    {
        bStart = true;
    }
}
