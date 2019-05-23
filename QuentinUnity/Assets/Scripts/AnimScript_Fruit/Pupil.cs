using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Pupil : MonoBehaviour
{

    private float fTimer;
    private float fDelay = 0.06f;
    public GameObject hFaceManager;
    private float fSmallSize;
    private float fBaseSize;
    private float fBigSize;

    // Start is called before the first frame update
    void Start()
    {
        fSmallSize = hFaceManager.GetComponent<FaceManager>().fSmallSize;
        fBigSize = hFaceManager.GetComponent<FaceManager>().fBigSize;
        fBaseSize = hFaceManager.GetComponent<FaceManager>().fBaseSize;

        transform.localScale = new Vector3(fBaseSize, fBaseSize, fBaseSize);
    }

    // Update is called once per frame
    void Update()
    {

        fTimer += Time.deltaTime;


        

    }


    public void UpdateExcited()
    {
        if (fTimer > fDelay)
        {
            float _rand = Random.Range(-0.02f, 0.02f);
            transform.localScale = new Vector3(fBigSize + _rand, fBigSize + _rand, fBigSize + _rand);
            fTimer = 0;

        }

    }

    public void UpdateScared()
    {
        float _rand = Random.Range(-0.006f, 0.006f);
        transform.localScale = new Vector3(fSmallSize + _rand, fSmallSize + _rand, fSmallSize + _rand);

    }

    public void ResetScale()
    {
        transform.localScale = new Vector3(fBaseSize, fBaseSize, fBaseSize);

    }
}
