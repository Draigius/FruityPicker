using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class changeMap: MonoBehaviour
{
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }


    public void changeScene (string sceneName)
    {

        

        Application.LoadLevel(sceneName);
    }

}
