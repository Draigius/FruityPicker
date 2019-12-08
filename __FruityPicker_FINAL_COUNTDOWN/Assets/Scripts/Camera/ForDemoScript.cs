using System.Collections;
using System.Collections.Generic;
using UnityEngine;





public class ForDemoScript : MonoBehaviour
{
    public Material[] mTableMaterial = new Material[2];

    private float fDistance;
    
    private bool bSuivi = false;

    public bool bObjectSaisi = false;

    Renderer rend;
    // Start is called before the first frame update
    void Start()
    {
        rend = GetComponent<Renderer>();
        rend.enabled = true;

        fDistance = Mathf.Abs(Camera.main.transform.position.z);
    }

    // Update is called once per frame
    void Update()
    {


        if(Input.GetMouseButtonDown(0))
        {

            transform.localScale = new Vector3(0.7f, 0.7f, 0.7f);
            bSuivi = true;

        }

        if(Input.GetMouseButtonUp(0))
        {

            transform.localScale = new Vector3(0.3f, 0.3f, 0.3f);
            bSuivi = false;
            bObjectSaisi = false;

        }


        if(bSuivi == true)
        {

            transform.position = Camera.main.ScreenToWorldPoint(new Vector3(Input.mousePosition.x, Input.mousePosition.y, fDistance-1));

        }
        else
        {

            transform.position = new Vector3(-20, -20, -20);

        }

        if(bObjectSaisi == true)
        {

            funcRendu(0);

        }
        else
        {

            funcRendu(1);

        }

    }

    void funcRendu(int i)
    {

        rend.sharedMaterial = mTableMaterial[i];

    }
}
