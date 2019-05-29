using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Target : MonoBehaviour
{
    Vector3 Pos;
    float fTimer;
    float fDelay = 2;

    // Start is called before the first frame update
    void Start()
    {
        Pos = new Vector3(Random.Range(-500f, 500f), Random.Range(-400f, 500f), Random.Range(-500f, -200f));
    }

    // Update is called once per frame
    void Update()
    {
        fTimer += Time.deltaTime;

        if (fTimer>fDelay)
        {
            transform.position = new Vector3(Random.Range(-500f, 500f), Random.Range(-400f, 500f), Random.Range(-500f, -200f));
            Pos = new Vector3(Random.Range(-500f, 500f), Random.Range(-400f, 500f), Random.Range(-500f, -200f));
            fDelay = 2f + Random.Range(-1.6f, 3f);
            fTimer = 0;

        }
        else
        {
            transform.position = Vector3.MoveTowards(transform.position, Pos, 4f);

        }
        
    }
}
