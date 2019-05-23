using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MeshRender : MonoBehaviour
{

    Renderer rend;

    // Start is called before the first frame update
    void Start()
    {
        rend = GetComponent<Renderer>();
        rend.enabled = true;
    }

    // Update is called once per frame
    void Update()
    {
        
    }


    void funcChangemantMatérial(Material mMaterialApliquer)
    {

        rend = GetComponent<Renderer>();
        rend.enabled = true;
        rend.sharedMaterial = mMaterialApliquer;

    }
}
