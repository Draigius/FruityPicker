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
    public GameObject hMedailleMesh;

    // Modificateur Scale
    private int iModScale;

    private bool bScalingBronze = false;
    private bool bScalingSilver = false;
    private bool bScalingGold = false;
    private bool bScalingMedal = false;

    private bool bScalingBronzeImpulse = false;
    private bool bScalingSilverImpulse = false;
    private bool bScalingGoldImpulse = false;
    private bool bScalingMedalDown = false;

    private float fScaleExtremeUp = 0.5f;
    private float fScaleFollowThrough = 0.1f;
    
    private float fScaleSpeed = 1.5f;


    private bool trigger_1 = false;
    private bool trigger_2 = false;
    private bool trigger_3 = false;
    private bool trigger_4 = false;
    private bool trigger_5 = false;
    private bool trigger_6 = false;

    //MATERIAUX

    [Header("MATERIAUX")]
    public Material[] mTableMedaille;


    // Start is called before the first frame update
    void Start()
    {
        //Debug.Log(fReussiteBronze);
        //Debug.Log(fReussiteSilver);
        //Debug.Log(fReussiteGold);

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
        if (Camera.main.GetComponent<CameraFinScript>().fEtape == 5 || Camera.main.GetComponent<CameraFinScript>().fEtape == 6)
        {
            //SCALE UP
            iModScale = 1;
            // Si scale jus >= Reussite Or

            if (hJusParent.transform.localScale.y >= fReussiteGold)
            {
                bScalingGold = true;
                bScalingMedal = true;

                if (trigger_1 == false)
                {
                    Camera.main.GetComponent<AI_AudioMaster>().PlayEvent("onRing");
                    trigger_1 = true;
                }

                hMedailleMesh.GetComponent<Renderer>().sharedMaterial = mTableMedaille[3];
            }
            // Sinon scale jus >= Reussite Argent
            else if (hJusParent.transform.localScale.y >= fReussiteSilver)
            {
                bScalingSilver = true;
                bScalingMedal = true;

                if (trigger_2 == false)
                {
                    Camera.main.GetComponent<AI_AudioMaster>().PlayEvent("onRing");
                    trigger_2 = true;
                }

                hMedailleMesh.GetComponent<Renderer>().sharedMaterial = mTableMedaille[2];
            }
            // Sinon scale jus >= Reussite Bronze
            else if (hJusParent.transform.localScale.y >= fReussiteBronze)
            {
                bScalingBronze = true;
                bScalingMedal = true;

                if (trigger_3 == false)
                {
                    Camera.main.GetComponent<AI_AudioMaster>().PlayEvent("onRing");
                    trigger_3 = true;
                }

                hMedailleMesh.GetComponent<Renderer>().sharedMaterial = mTableMedaille[1];
            }
        }
        else if (Camera.main.GetComponent<CameraFinScript>().fEtape == 8)
        {
            //SCALE DOWN
            iModScale = -1;

            // Sinon scale jus < Reussite Or
            if (hJusParent.transform.localScale.y < fReussiteGold)
            {
                hEchelonTrois.GetComponent<BoxCollider>().enabled = true;
                hEchelonTrois.GetComponent<Rigidbody>().useGravity = true;

                if (bScalingGoldImpulse)
                {
                    hEchelonTrois.GetComponent<Rigidbody>().AddForce(0, 25, 0, ForceMode.Impulse);
                    bScalingGoldImpulse = false;
                }

                if (hEchelonTrois.transform.localScale.x > 1)
                {
                    hEchelonTrois.transform.localScale = new Vector3(hEchelonTrois.transform.localScale.x + fScaleSpeed * iModScale * Time.deltaTime, hEchelonTrois.transform.localScale.y + fScaleSpeed * iModScale * Time.deltaTime, hEchelonTrois.transform.localScale.z + fScaleSpeed * iModScale * Time.deltaTime);
                }

                bScalingMedalDown = true;
                hMedailleMesh.GetComponent<Renderer>().sharedMaterial = mTableMedaille[2];

                if (trigger_4 == false)
                {
                    Camera.main.GetComponent<AI_AudioMaster>().PlayEvent("onClac");
                    trigger_4 = true;
                }

            }
            // Sinon scale jus < Reussite Argent
            if (hJusParent.transform.localScale.y < fReussiteSilver)
            {
                hEchelonDeux.GetComponent<BoxCollider>().enabled = true;
                hEchelonDeux.GetComponent<Rigidbody>().useGravity = true;

                if (bScalingSilverImpulse)
                {
                    hEchelonDeux.GetComponent<Rigidbody>().AddForce(0, 25, 0, ForceMode.Impulse);
                    bScalingSilverImpulse = false;
                }

                if (hEchelonDeux.transform.localScale.x > 1)
                {
                    hEchelonDeux.transform.localScale = new Vector3(hEchelonDeux.transform.localScale.x + fScaleSpeed * iModScale * Time.deltaTime, hEchelonDeux.transform.localScale.y + fScaleSpeed * iModScale * Time.deltaTime, hEchelonDeux.transform.localScale.z + fScaleSpeed * iModScale * Time.deltaTime);
                }

                bScalingMedalDown = true;
                hMedailleMesh.GetComponent<Renderer>().sharedMaterial = mTableMedaille[1];

                if (trigger_5 == false)
                {
                    Camera.main.GetComponent<AI_AudioMaster>().PlayEvent("onClac");
                    trigger_5 = true;
                }

            }
            // Si scale jus < Reussite Bronze
            if (hJusParent.transform.localScale.y < fReussiteBronze)
            {

                hEchelonUn.GetComponent<BoxCollider>().enabled = true;
                hEchelonUn.GetComponent<Rigidbody>().useGravity = true;

                if (bScalingBronzeImpulse)
                {
                    hEchelonUn.GetComponent<Rigidbody>().AddForce(0, 25, 0, ForceMode.Impulse);
                    bScalingBronzeImpulse = false;
                }


                if (hEchelonUn.transform.localScale.x > 1)
                {
                    hEchelonUn.transform.localScale = new Vector3(hEchelonUn.transform.localScale.x + fScaleSpeed * iModScale * Time.deltaTime, hEchelonUn.transform.localScale.y + fScaleSpeed * iModScale * Time.deltaTime, hEchelonUn.transform.localScale.z + fScaleSpeed * iModScale * Time.deltaTime);
                }

                bScalingMedalDown = true;
                hMedailleMesh.GetComponent<Renderer>().sharedMaterial = mTableMedaille[0];

                if (trigger_6 == false)
                {
                    Camera.main.GetComponent<AI_AudioMaster>().PlayEvent("onClac");
                    trigger_6 = true;
                }

            }
        }
        //////////////////////////
        #region scaleUp
        ///////////////SCALE INTERPOLATION UP

        if (bScalingBronze && hEchelonUn.transform.localScale.x <= 1 + fScaleExtremeUp)
        {
            if (hEchelonUn.transform.localScale.x + fScaleSpeed* iModScale *Time.deltaTime > 1 + fScaleExtremeUp)
            {
                bScalingBronze = false;
            }
            else
            {
                hEchelonUn.transform.localScale = new Vector3(hEchelonUn.transform.localScale.x + fScaleSpeed * iModScale * Time.deltaTime, hEchelonUn.transform.localScale.y + fScaleSpeed * iModScale * Time.deltaTime, hEchelonUn.transform.localScale.z + fScaleSpeed * iModScale * Time.deltaTime);
            }
        }

        if (!bScalingBronze && hEchelonUn.transform.localScale.x > 1 + fScaleFollowThrough)
        {
            Debug.Log("Mettre Bonne taile ScUp");
            hEchelonUn.transform.localScale = new Vector3(hEchelonUn.transform.localScale.x + fScaleSpeed * iModScale * Time.deltaTime * -1, hEchelonUn.transform.localScale.y + fScaleSpeed * iModScale * Time.deltaTime * -1, hEchelonUn.transform.localScale.z + fScaleSpeed * iModScale * Time.deltaTime * -1);
        }

        /////////////////////////

        if (bScalingSilver && hEchelonDeux.transform.localScale.x <= 1 + fScaleExtremeUp)
        {
            if (hEchelonDeux.transform.localScale.x + fScaleSpeed * iModScale * Time.deltaTime > 1 + fScaleExtremeUp)
            {
                bScalingSilver = false;
            }
            else
            {
                hEchelonDeux.transform.localScale = new Vector3(hEchelonDeux.transform.localScale.x + fScaleSpeed * iModScale * Time.deltaTime, hEchelonDeux.transform.localScale.y + fScaleSpeed * iModScale * Time.deltaTime, hEchelonDeux.transform.localScale.z + fScaleSpeed * iModScale * Time.deltaTime);
            }
        }

        if (!bScalingSilver && hEchelonDeux.transform.localScale.x > 1 + fScaleFollowThrough)
        {
            hEchelonDeux.transform.localScale = new Vector3(hEchelonDeux.transform.localScale.x + fScaleSpeed * iModScale * Time.deltaTime * -1, hEchelonDeux.transform.localScale.y + fScaleSpeed * iModScale * Time.deltaTime * -1, hEchelonDeux.transform.localScale.z + fScaleSpeed * iModScale * Time.deltaTime * -1);
        }

        /////////////////////////

        if (bScalingGold && hEchelonTrois.transform.localScale.x <= 1 + fScaleExtremeUp)
        {
            if (hEchelonTrois.transform.localScale.x + fScaleSpeed * iModScale * Time.deltaTime > 1 + fScaleExtremeUp)
            {
                bScalingGold = false;
            }
            else
            {
                hEchelonTrois.transform.localScale = new Vector3(hEchelonTrois.transform.localScale.x + fScaleSpeed * iModScale * Time.deltaTime, hEchelonTrois.transform.localScale.y + fScaleSpeed * iModScale * Time.deltaTime, hEchelonTrois.transform.localScale.z + fScaleSpeed * iModScale * Time.deltaTime);
            }
        }

        if (!bScalingGold && hEchelonTrois.transform.localScale.x > 1 + fScaleFollowThrough)
        {
            hEchelonTrois.transform.localScale = new Vector3(hEchelonTrois.transform.localScale.x + fScaleSpeed * iModScale * Time.deltaTime * -1, hEchelonTrois.transform.localScale.y + fScaleSpeed * iModScale * Time.deltaTime * -1, hEchelonTrois.transform.localScale.z + fScaleSpeed * iModScale * Time.deltaTime * -1);
        }

        #endregion
        //////////////////////////
        ///
        /// LAST FONCTIONS

        if (Camera.main.GetComponent<CameraFinScript>().fEtape == 10 && Camera.main.GetComponent<CameraFinScript>().bScoreNegative == true)
        {
            hEchelonUn.GetComponent<BoxCollider>().enabled = true;
            hEchelonUn.GetComponent<Rigidbody>().useGravity = true;
            hEchelonDeux.GetComponent<BoxCollider>().enabled = true;
            hEchelonDeux.GetComponent<Rigidbody>().useGravity = true;
            hEchelonTrois.GetComponent<BoxCollider>().enabled = true;
            hEchelonTrois.GetComponent<Rigidbody>().useGravity = true;
        }
    }
}
