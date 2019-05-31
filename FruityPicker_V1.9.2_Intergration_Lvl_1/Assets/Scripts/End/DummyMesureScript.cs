using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DummyMesureScript : MonoBehaviour
{
    [Header ("Echelons")]
    public float fReussiteSilver;
    public float fReussiteBronze;


    [Header("Parent")]
    public GameObject hJusParent;


    [Header("Objets à Placer")]
    public GameObject hEchelonUn;
    public GameObject hEchelonDeux;
    public GameObject hEchelonTrois;

    // Start is called before the first frame update
    void Start()
    {
        Debug.Log(fReussiteBronze);
        Debug.Log(fReussiteSilver);
        // Echelon Un
        hJusParent.transform.localScale = new Vector3(1f, fReussiteBronze, 1f);
        hEchelonUn.transform.position = gameObject.transform.position;
        // Echelon Deux
        hJusParent.transform.localScale = new Vector3(1f, fReussiteSilver, 1f);
        hEchelonDeux.transform.position = gameObject.transform.position;
        // Echelon Un
        hJusParent.transform.localScale = new Vector3(1f, 1f, 1f);
        hEchelonTrois.transform.position = gameObject.transform.position;

        hJusParent.transform.localScale = new Vector3(1f, 0f, 1f);
    }
}
