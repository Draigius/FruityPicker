using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ScoreScript : MonoBehaviour
{
    public static int iScoreParfait;
    public static int iScoreTotal;
    public static int iNbFruitsNegatifs;
    public static int iNbFruitsPositifs;
    public static int iNbFruitsTotal;

    public static float fReussiteEchelonDeux;
    public static float fReussiteEchelonUn;

    public static int iTestChange = 0;
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        iTestChange++;
        Debug.Log (iTestChange);
    }

    public void funcGetScore()
    {

    }
}
