using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class paupiere : MonoBehaviour
{
    private float timerClign;
    private float timerTremble;
    private Vector3 stock;

    public float delayClign = 1f;
    public float delayTremble = 0.1f;



    // Start is called before the first frame update
    void Start()
    {
        


    }

    // Update is called once per frame
    void Update()
    {


        timerClign += Time.deltaTime;
        timerTremble += Time.deltaTime;


        if (timerTremble > delayTremble)
        {
            float rand = Random.Range(-5f * timerClign / 2, 5f * timerClign / 2);


            transform.Rotate(new Vector3(rand, 0, 0), Space.World);

            timerTremble = 0;
        }
        
        



        
  




        if (timerClign > delayClign)
        {
            Clign();
            //transform.eulerAngles = new Vector3(-100, 0, 0);
            //timerReset = 0;

        }



        // (new Vector3(0, 0, 0), Space.World);

        //transform.eulerAngles = new Vector3(-90, 0, 0);
    }


    void Clign()
    {


        stock = transform.eulerAngles;
        transform.eulerAngles = new Vector3(0, 0, 0);
        //transform.eulerAngles = new Vector3(-90, 0, 0);
        Invoke("ResetPos", 0.1f);




    }

    void ResetPos()
    {
        

        transform.eulerAngles = new Vector3(-100 + Random.Range(-25f, 0),0, 0);

        delayClign = Random.Range(0.5f, 5f);
        timerClign = 0;
    }
}
