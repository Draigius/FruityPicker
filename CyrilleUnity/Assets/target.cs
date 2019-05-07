using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class target : MonoBehaviour
{
    Vector3 Pos;
    float timer;
    float delay = 2;

    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        timer += Time.deltaTime;

        if (timer>delay)
        {
            transform.position = new Vector3(Random.Range(-500f, 500f), Random.Range(-400f, 500f), Random.Range(-500f, -200f));
            Pos = new Vector3(Random.Range(-500f, 500f), Random.Range(-400f, 500f), Random.Range(-500f, -200f));
            delay = 2f + Random.Range(-1.6f, 3f);
            timer = 0;

        }
        else
        {
            transform.position = Vector3.MoveTowards(transform.position, Pos, 4f);

        }
        
    }
}
