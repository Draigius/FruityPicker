using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DummyFleche : MonoBehaviour
{

    public GameObject hFleche;
    public GameObject hDummy;
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }



    private void OnTriggerEnter(Collider other)
    {

        if(other.gameObject == hFleche)
        {

            other.gameObject.transform.position = hDummy.transform.position;
            other.gameObject.transform.Translate(new Vector3(0, 0.4f * (hFleche.GetComponent<Fleche>().iSens), 0), Space.Self);
            //Debug.Log("collide");
        }


    }

}
