using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class pupil : MonoBehaviour
{

    private float timer;
    private float delay = 0.06f;

    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {

        timer += Time.deltaTime;


        if (Input.GetMouseButton(0) == true)
        {
            if (timer>delay)
            {
                float rand = Random.Range(-0.02f, 0.02f);
                transform.localScale = new Vector3(1.1f + rand, 1.1f + rand, 1.1f + rand);
                timer = 0;

            }
                      
        }

        if (Input.GetMouseButton(1) == true)
        {
            float rand = Random.Range(-0.006f, 0.006f);
            transform.localScale = new Vector3(0.9f + rand, 0.9f + rand, 0.9f + rand);

        }

        if (Input.GetMouseButtonUp(0) == true | Input.GetMouseButtonUp(1) == true)
        {
            transform.localScale = new Vector3(1, 1, 1);

        }

    }
}
