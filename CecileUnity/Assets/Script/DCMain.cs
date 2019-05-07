using System.Collections;
using System.Collections.Generic;
using UnityEngine;


public class DCMain : MonoBehaviour
{
    public GameObject dummyCam;
    private Vector3 vPosCam;


    public int iDeltaAngle;
    public float fAngleSpeed;

    // Start is called before the first frame update
    void Start()
    {
        Input.gyro.enabled = true;
        vPosCam = Input.gyro.rotationRate;
    }

    // Update is called once per frame
    void Update()
    {
        vPosCam = Input.gyro.rotationRate;

        Vector3 TempRotation = dummyCam.transform.eulerAngles;
        Debug.Log(dummyCam.transform.eulerAngles);

        TempRotation.y += vPosCam.y * fAngleSpeed;

        if (TempRotation.y > iDeltaAngle && TempRotation.y < 360 - iDeltaAngle)
        {
            TempRotation.y -= vPosCam.y * fAngleSpeed;
        }
        dummyCam.transform.eulerAngles = TempRotation;


    }
}
