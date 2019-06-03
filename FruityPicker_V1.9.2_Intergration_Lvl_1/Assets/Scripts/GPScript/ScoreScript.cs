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

    public static float fReussiteEchelonTrois = 1;
    public static float fReussiteEchelonDeux;
    public static float fReussiteEchelonUn;

    public float fTime;
    public float fTimeStep = 4;

    public GameObject DummyMesureObject;

    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        /*if (fTime>fTimeStep)
        {
            Debug.Log("Score Parfait : " + iScoreParfait);
            Debug.Log("Score Total : " + iScoreTotal);
            Debug.Log("Fruits Negatif : " + iNbFruitsNegatifs);
            Debug.Log("Fruits Positif : " + iNbFruitsPositifs);
            Debug.Log("Fruits Total : " + iNbFruitsTotal);

            fTime = 0;
        }

        fTime += Time.deltaTime; 
        */
    }

    public void funcGetScore(int iScore, int iNombreFruitsPos, int iNombreFruitsNeg, int iNombreTotalFruits, int iScoreP, float fReussiteUn,float fReussiteDeux)
    {
        iScoreParfait = iScoreP;
        iScoreTotal = iScore;
        iNbFruitsNegatifs = iNombreFruitsNeg;
        iNbFruitsPositifs = iNombreFruitsPos;
        iNbFruitsTotal = iNombreTotalFruits;

        fReussiteEchelonUn = fReussiteUn;
        fReussiteEchelonDeux = fReussiteDeux;
    }

    public bool funcReset()
    {
        iScoreParfait = 0;
        iScoreTotal = 0;
        iNbFruitsNegatifs = 0;
        iNbFruitsPositifs = 0;
        iNbFruitsTotal = 0;

        fReussiteEchelonUn = 0;
        fReussiteEchelonDeux = 0;
        fReussiteEchelonTrois = 1;

        return true;
    }

    public void funcSendScore()
    {
        gameObject.GetComponent<CameraFinScript>().iFruitsPositif = iNbFruitsPositifs;
        gameObject.GetComponent<CameraFinScript>().iFruitsNegatif = iNbFruitsNegatifs;

        gameObject.GetComponent<CameraFinScript>().iFruitsPositifsMaxScorePossible = iScoreParfait;
        gameObject.GetComponent<CameraFinScript>().iScoreNiveau = iScoreTotal;

        gameObject.GetComponent<CameraFinScript>().fReussiteMedium = fReussiteEchelonDeux;
        gameObject.GetComponent<CameraFinScript>().fReussitePetite = fReussiteEchelonUn;

        DummyMesureObject.GetComponent<DummyMesureScript>().fReussiteSilver = fReussiteEchelonDeux;
        DummyMesureObject.GetComponent<DummyMesureScript>().fReussiteBronze = fReussiteEchelonUn;
        DummyMesureObject.GetComponent<DummyMesureScript>().fReussiteGold = fReussiteEchelonTrois;
    }

}
