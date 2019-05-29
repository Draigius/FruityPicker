using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TransitionScript : MonoBehaviour
{
    private Vector3 v3PositionActuelle;
    private Vector3 v3PositionTarget;
    private Vector3 v3PositionStart;

    private int iScaleTorchon = 2;

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
        fOffsetTransitionY = fOffsetTransitionY * iScaleTorchon;
        fOffsetTransitionZ = fOffsetTransitionZ * iScaleTorchon;
        gameObject.transform.position = new Vector3(Camera.main.transform.position.x, Camera.main.transform.position.y, Camera.main.transform.position.z + fOffsetTransitionZ);

        gameObject.transform.localScale = new Vector3 (iScaleTorchon, iScaleTorchon, iScaleTorchon);

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
            bStart = false;
        }

        ////////////////// End
        if (bEnd && v3PositionActuelle != v3PositionStart)
        {
            transform.position = Vector3.MoveTowards(transform.position, v3PositionStart, fStep);
        }
        else if (bEnd && v3PositionActuelle == v3PositionStart)
        {
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
