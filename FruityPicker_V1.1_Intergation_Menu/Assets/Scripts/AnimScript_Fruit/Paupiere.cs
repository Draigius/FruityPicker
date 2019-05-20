using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Paupiere : MonoBehaviour
{
    private float fTimerClign;
    private float fTimerTremble;
    private Vector3 v3Stock;

    public float fDelayClign = 1f;
    public float fDelayTremble = 0.1f;



    // Start is called before the first frame update
    void Start()
    {
        


    }

    // Update is called once per frame
    void Update()
    {


        fTimerClign += Time.deltaTime;
        fTimerTremble += Time.deltaTime;

        
        if (fTimerTremble > fDelayTremble)
        {
            float _rand = Random.Range(-5f * fTimerClign / 2, 5f * fTimerClign / 2);


            transform.Rotate(new Vector3(_rand, 0, 0), Space.Self);

            fTimerTremble = 0;
        }
        
        



        
  




        if (fTimerClign > fDelayClign)
        {
            Clign();
            //transform.eulerAngles = new Vector3(-100, 0, 0);
            //timerReset = 0;

        }



        // (new Vector3(0, 0, 0), Space.World);

        //transform.eulerAngles = new Vector3(-90, 0, 0);

    

        
        //transform.localEulerAngles = new Vector3(Random.Range(-25f, 0), 0, 0);
    }


    public void Clign()
    {


        v3Stock = transform.localEulerAngles;
        transform.localEulerAngles = new Vector3(0, 0, 0);
        //transform.eulerAngles = new Vector3(-90, 0, 0);
        Invoke("ResetPos", 0.1f);




    }

    void ResetPos()
    {
        

        transform.localEulerAngles = new Vector3(-100 + Random.Range(-25f, 0),0, 0);

        fDelayClign = Random.Range(0.5f, 5f);
        fTimerClign = 0;
    }
}
