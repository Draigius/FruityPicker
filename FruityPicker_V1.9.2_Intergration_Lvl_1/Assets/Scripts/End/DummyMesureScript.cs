using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DummyMesureScript : MonoBehaviour
{
    [Header ("Echelons")]
    public float fReussiteGold;
    public float fReussiteSilver;
    public float fReussiteBronze;


    [Header("Parent")]
    public GameObject hJusParent;


    [Header("Objets à Placer")]
    public GameObject hEchelonUn;
    public GameObject hEchelonDeux;
    public GameObject hEchelonTrois;
    public GameObject hMedaille;

    // Modificateur Scale
    private int iModScale;


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
        hJusParent.transform.localScale = new Vector3(1f, fReussiteGold, 1f);
        hEchelonTrois.transform.position = gameObject.transform.position;

        hJusParent.transform.localScale = new Vector3(1f, 0f, 1f);
    }

    private void Update()
    {
        if (Camera.main.GetComponent<CameraFinScript>().fEtape == 5)
        {
            //SCALE UP
            iModScale = 1;
            // Si scale jus >= Reussite Or
            if (hJusParent.transform.localScale.y == fReussiteGold)
            {
                funcScale(hEchelonTrois, iModScale);
                funcScale(hMedaille, iModScale);
            }
            // Sinon scale jus >= Reussite Argent
            else if (hJusParent.transform.localScale.y >= fReussiteSilver)
            {
                funcScale(hEchelonDeux, iModScale);
                funcScale(hMedaille, iModScale);
            }
            // Sinon scale jus >= Reussite Bronze
            else if (hJusParent.transform.localScale.y >= fReussiteBronze)
            {
                funcScale(hEchelonUn, iModScale);
                funcScale(hMedaille, iModScale);
            }
        }
        else if (Camera.main.GetComponent<CameraFinScript>().fEtape == 8)
        {
            //SCALE DOWN
            iModScale = -1;

            // Si scale jus < Reussite Bronze
            if (hJusParent.transform.localScale.y < fReussiteBronze)
            {
                funcScale(hEchelonUn, iModScale);
                funcScale(hMedaille, iModScale);
            }
            // Sinon scale jus < Reussite Argent
            else if (hJusParent.transform.localScale.y < fReussiteSilver)
            {
                funcScale(hEchelonDeux, iModScale);
                funcScale(hMedaille, iModScale);
            }
            // Sinon scale jus < Reussite Or
            else if(hJusParent.transform.localScale.y < fReussiteGold)
            {
                funcScale(hEchelonTrois, iModScale);
                funcScale(hMedaille, iModScale);
            }
        }
    }

    private void funcScale (GameObject hObjectToScale, int iModScale)
    {

        float fScale = 0.5f * iModScale;

        if (hObjectToScale == hMedaille)
        {
            if ( hObjectToScale.transform.localScale.x > 0.5f && hObjectToScale.transform.localScale.x < 1.5f)
            {
                hObjectToScale.transform.localScale = new Vector3(hObjectToScale.transform.localScale.x + fScale, hObjectToScale.transform.localScale.y + fScale, hObjectToScale.transform.localScale.z + fScale);
            }
        }
        else
        {
            /*float fScale = 0.3f * iModScale;

            if (hObjectToScale.transform.localScale.x == hObjectToScale.transform.localScale.y == hObjectToScale.transform.localScale.z < fScale)
            {
                hObjectToScale.transform.localScale = new Vector3(hObjectToScale.transform.localScale.x + fScale * Time.deltaTime, hObjectToScale.transform.localScale.y + fScale * Time.deltaTime, hObjectToScale.transform.localScale.z + fScale * Time.deltaTime);
            }
            else if (hObjectToScale.transform.localScale.x == hObjectToScale.transform.localScale.y == hObjectToScale.transform.localScale.z >= fScale)
            {
                hObjectToScale.transform.localScale = new Vector3(hObjectToScale.transform.localScale.x + fScale * Time.deltaTime, hObjectToScale.transform.localScale.y + fScale * Time.deltaTime, hObjectToScale.transform.localScale.z + fScale * Time.deltaTime);
            }*/

            if (hObjectToScale.transform.localScale.x > 0.5f && hObjectToScale.transform.localScale.x < 1.5f)
            {
                hObjectToScale.transform.localScale = new Vector3(hObjectToScale.transform.localScale.x + fScale, hObjectToScale.transform.localScale.y + fScale, hObjectToScale.transform.localScale.z + fScale);
            }

        }
    }
    
}
