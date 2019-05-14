using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Bouche : MonoBehaviour
{

    float fDelay = 0.1f;
    public float fTimer;

    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {

        fTimer += Time.deltaTime;

        float fRandom = Random.Range(-5, 5);

        if (fTimer > fDelay)
        {
            transform.Rotate(new Vector3(0, 0, fRandom), Space.Self);
            fTimer = 0;

        }


        if (fTimer > fDelay / 2 && transform.localEulerAngles.z != 0)
        {

            transform.localEulerAngles = new Vector3(0, -180, 0);

        }
    }
}
