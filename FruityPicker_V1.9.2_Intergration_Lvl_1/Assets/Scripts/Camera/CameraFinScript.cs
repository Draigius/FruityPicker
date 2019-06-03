using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CameraFinScript : MonoBehaviour
{

    private GameObject[] hTableFruitSpawn;
    
    [Header("FRUITS QUI TOMBENT")]
    [Header("_____________________________________________")]
    [Header("Nombres Fruits")]
    public int iFruitsPositif;
    public int iFruitsNegatif;
    public int iFruitsPositifsMaxScorePossible;

    public int iScoreNiveau;

    public float fReussiteMedium;
    public float fReussitePetite;

    [Header("Tableau Prefabs Fruits")]
    public GameObject[] hTablePrefabeFruitPourris;
    public GameObject[] hTablePrefabeFruitSains;

    [Header("Data Dummy Position Fruits")]
    public GameObject hDummyCentreBarrique;
    public GameObject hDummyCibleOrbitale;

    public int iAngleSpawn;
    public float fDummySpawnOffsetY;

    [Header("FOND BARRIQUE ET COUVERCLE")]
    [Header("_____________________________________________")]
    [Header("Trigger barrique")]
    public GameObject TriggerBarrique;
    private TriggerBarriqueScript ScriptTriggerBarrique;

    [Header("Pressoir Couvercle Scene")]
    public GameObject PressoirCouvercle;

    [Header("MOUVEMENT CAMERA")]
    [Header("_____________________________________________")]
    [Header("Camera Data")]
    public GameObject hCamera1;
    public GameObject hCamera2;
    public float fSpeed;

    [Header("JUS")]
    [Header("_____________________________________________")]
    public GameObject hTourneRobinet;
    private float fVitesseRotate = 180f;
    private float fSensRotation;

    public GameObject hFluxRobinet;
    private Vector3 v3FluxOrigine;
    private float fVitesseScaleFlux = 0.7f;

    public GameObject hJusPositif;
    private float fVitesseScaleJusPositif = 0.8f;
    private float fScaleJusPositif;

    public GameObject hJusNegatif;
    private float fVitesseScaleJusNegatif = 0.8f;
    private float fScaleJusNegatif;

    private float fVitesseScaleDecompte = 0.4f;
    float fPourcentage;


    [Header("TEXTES")]
    [Header("_____________________________________________")]

    public GameObject hMeshTextFruitPos;
    public GameObject hMeshTextFruitNeg;
    public GameObject hMeshTextMinus;
    public GameObject hMeshTextBar;
    public GameObject hMeshTextScoreTotal;
    public GameObject hMeshTextScoreMax;
    public GameObject hMeshTextScoreMaxBarre;

    [Header("Debug")]
    [Header("_____________________________________________")]
    public float fEtape = 0;

    private static float fTimerOrigine1 = 1;
    private float fTimerDeconte = fTimerOrigine1;

    // Start is called before the first frame update
    void Awake ()
    {
        /// RECUPERER DATA SCORE ///

        gameObject.GetComponent<ScoreScript>().funcSendScore();
        
        /*if (iFruitsNegatif == iFruitsPositif  && iFruitsPositif == 0)
        {
            iFruitsNegatif = 15;
            iFruitsPositif = 1;

            iFruitsPositifsMaxScorePossible = 16;
            iScoreNiveau = iFruitsPositif - iFruitsNegatif;

            fReussitePetite = 0.5f;
            fReussiteMedium = 
        }*/

        /////////////////////////////

        hTableFruitSpawn = new GameObject [iFruitsNegatif+iFruitsPositif];

        //Envoi Data au script de Trigger Barrique
        ScriptTriggerBarrique = TriggerBarrique.GetComponent<TriggerBarriqueScript>();
        ScriptTriggerBarrique.funcSendTotalFruitNumber(iFruitsPositif + iFruitsNegatif);

        //Positionnement Camera
        gameObject.transform.position = hCamera1.transform.position;

        v3FluxOrigine = hFluxRobinet.transform.position;

        //Calcul scale max des jus

        //Calcul Ratio Bons/Mauvais fruits
        fScaleJusPositif = (float)iFruitsPositif * 100f / iFruitsPositifsMaxScorePossible;
        fScaleJusNegatif = (float)iFruitsPositif * 100f / iFruitsPositifsMaxScorePossible;
        fScaleJusPositif = fScaleJusPositif / 100;
        fScaleJusNegatif = fScaleJusNegatif / 100;

        //Debug.Log("fScale Jus = " + fScaleJusPositif);

        fPourcentage = iFruitsNegatif * 100 / iFruitsPositif;
        fPourcentage = (100 - fPourcentage) / 100;

        //Debug.Log("Pourcentage = " + fPourcentage);

        // DESAFFICHER TEXTES 3D
        hMeshTextFruitPos.GetComponent<MeshRenderer>().enabled = false;
        hMeshTextFruitNeg.GetComponent<MeshRenderer>().enabled = false;
        hMeshTextMinus.GetComponent<MeshRenderer>().enabled = false;
        hMeshTextBar.GetComponent<MeshRenderer>().enabled = false;
        hMeshTextScoreTotal.GetComponent<MeshRenderer>().enabled = false;
        hMeshTextScoreMax.GetComponent<MeshRenderer>().enabled = false;
        hMeshTextScoreMaxBarre.GetComponent<MeshRenderer>().enabled = false;

    }

    // Update is called once per frame
    void Update()
    {
        //Debug.Log("Etape : " + fEtape);

        if (fEtape == 0)
        {
            ///////////////////////////////////////////////////  Choix Fruits
            //spawn des fruits
            int iCompteur= iFruitsNegatif;
            GameObject hStockObjectTemporaire;

            for(int i=0;i< iFruitsPositif + iFruitsNegatif; i++)
            {
                //Si i est impair
                if (i % 2 == 1 && iCompteur>0)
                {
                    //Quel type de fruit spawn
                    hStockObjectTemporaire = Instantiate(hTablePrefabeFruitPourris[0], new Vector3(0,100,0), Quaternion.identity);
                    //Mathf.FloorToInt(Random.RandomRange(0, 3.99f))
                    //Debug.Log("hStockObjectTemporaire :"+ hStockObjectTemporaire);

                    hStockObjectTemporaire.transform.localScale = new Vector3(0.3f, 0.3f, 0.3f);
                    //GameObject hFaceManager = GetChildWithName(hStockObjectTemporaire, "kiwi_SurMesh");

                    hTableFruitSpawn[i] = hStockObjectTemporaire;

                    iCompteur = iCompteur - 1;
                }
                else
                //Si i est pair
                {
                    //Quel type de fruit spawn
                    hStockObjectTemporaire = Instantiate(hTablePrefabeFruitSains[0], new Vector3(0, 100, 0), Quaternion.identity);

                    hTableFruitSpawn[i] = hStockObjectTemporaire;
                }

            ///////////////////////////////////////////////////  Positionnement Fruits
            
            //Positionnement Fruit sur Dummy Orbital

            hStockObjectTemporaire.transform.position = hDummyCibleOrbitale.transform.position;

                
                // Ajout Collider
                hStockObjectTemporaire.AddComponent<SphereCollider>();

                // Ahout Rigidbody
                hStockObjectTemporaire.AddComponent<Rigidbody>();
                // Add Torque
                Vector3 v3Torque;

                v3Torque.x = Random.Range(-200, 200);
                v3Torque.y = Random.Range(-200, 200);
                v3Torque.z = Random.Range(-200, 200);

                hStockObjectTemporaire.GetComponent<Rigidbody>().AddTorque(v3Torque);

                //Réglage Distance Optionnel
                // ??????

                // Rotation Du parent Dummy Centre
                hDummyCentreBarrique.transform.Rotate ( new Vector3 (0, iAngleSpawn, 0), Space.Self);

            //Déplacement Up du Dummy Spawn
            hDummyCibleOrbitale.transform.position = new Vector3 (hDummyCibleOrbitale.transform.position.x, hDummyCibleOrbitale.transform.position.y + fDummySpawnOffsetY, hDummyCibleOrbitale.transform.position.z);

            }

            fEtape = 0.5f;
        }

        if (fEtape == 1)//quand tous les fruis sont tombé
        {
            // spawn du capuchon pressoir rotatif
            PressoirCouvercle.GetComponent<PressoirScript>().fSetupState = fEtape;
        }

        if (fEtape == 2)//quand capuchont rotatif est en position
        {
            //autorisé le joueur a faire le slide
        }

        if (fEtape == 3)//quand le slide et fini
        {
            //faire le mouvement de caméra + ouvrir roubinet

            //setup Vitesse
            fSpeed = 8f;
            fSensRotation = 1;

            //Setup vitesse dt
            float fStep = fSpeed * Time.deltaTime;

            //Move Camera
            transform.position = Vector3.MoveTowards(transform.position, hCamera2.transform.position, fStep);

            hTourneRobinet.transform.Rotate(new Vector3(0, fVitesseRotate * Time.deltaTime * fSensRotation, 0), Space.Self);

            if (hTourneRobinet.transform.rotation.y < 180 && transform.position == hCamera2.transform.position)
            {
                fEtape = 4;
            }
        }

        if (fEtape == 4)//quand la caméra est en position final ou pas loin de sa position final
        {
            //faire tourner de 3/4 le roubinet tout en faisant grossir le flux : ^)
            fSensRotation = 1f;

            if (hFluxRobinet.transform.localScale.y<1)
            {
                if (hFluxRobinet.transform.localScale.y < 0.5f)
                {
                    hFluxRobinet.transform.localScale = new Vector3(hFluxRobinet.transform.localScale.x, hFluxRobinet.transform.localScale.y + fVitesseScaleFlux * Time.deltaTime, hFluxRobinet.transform.localScale.z);
                    hTourneRobinet.transform.Rotate(new Vector3(0, fVitesseRotate * Time.deltaTime * fSensRotation, 0), Space.Self);
                }
                else
                {
                    hFluxRobinet.transform.localScale = new Vector3(hFluxRobinet.transform.localScale.x, hFluxRobinet.transform.localScale.y + fVitesseScaleFlux * Time.deltaTime, hFluxRobinet.transform.localScale.z);
                }
            }
            else if (hFluxRobinet.transform.localScale.y >= 1)
            {
                fEtape = 5;
            }
        }
        
        if (fEtape == 5)//quand robinet a fini de tourner
        {
            //Monter le juuuus

            //Sens de rotation Roubinet inversé
            fSensRotation = -1f;

            //ActiverMeshRenderer
            if (hJusPositif.GetComponent<MeshRenderer>().enabled== false || hJusNegatif.GetComponent<MeshRenderer>().enabled == false)
            {
                hJusPositif.GetComponent<MeshRenderer>().enabled = true;
                hJusNegatif.GetComponent<MeshRenderer>().enabled = true;
            }

            //Faire monter les Deux cylindres de jus
            if (hJusPositif.transform.localScale.y < 0.8f*fScaleJusPositif && hJusNegatif.transform.localScale.y < 0.8*fScaleJusNegatif)
            {
                if (hJusPositif.transform.localScale.y < 0.5 * fScaleJusPositif && hJusNegatif.transform.localScale.y < 0.5f * fScaleJusNegatif)
                {
                    hJusPositif.transform.localScale = new Vector3(1, hJusPositif.transform.localScale.y + fVitesseScaleJusPositif * Time.deltaTime, 1);
                    hJusNegatif.transform.localScale = new Vector3(1, hJusNegatif.transform.localScale.y + fVitesseScaleJusNegatif * Time.deltaTime, 1);
                }
                else
                {
                    hJusPositif.transform.localScale = new Vector3(1, hJusPositif.transform.localScale.y + fVitesseScaleJusPositif * Time.deltaTime, 1);
                    hJusNegatif.transform.localScale = new Vector3(1, hJusNegatif.transform.localScale.y + fVitesseScaleJusNegatif * Time.deltaTime, 1);
                    hTourneRobinet.transform.Rotate(new Vector3(0, fVitesseRotate * Time.deltaTime * fSensRotation, 0), Space.Self);

                    hFluxRobinet.transform.localScale = new Vector3(1, hFluxRobinet.transform.localScale.y - fVitesseScaleFlux * Time.deltaTime, 1);
                }


            }
            else if (hJusPositif.transform.localScale.y >= 0.5 * fScaleJusPositif && hJusNegatif.transform.localScale.y >= 0.5 * fScaleJusNegatif)
            {
                fEtape = 6;
            }
        }

        if (fEtape == 6)//quand Jus finit de monter, le flux s'inverse et le roubinet finit de tourner
        {
            // AFFICHER SCORE EN VERT
            hMeshTextFruitPos.GetComponent<TextMesh>().text = iFruitsPositif.ToString();
            hMeshTextFruitPos.GetComponent<MeshRenderer>().enabled = true;

            //Inverser le flux
            if ( v3FluxOrigine == hFluxRobinet.transform.position)
            {
                hFluxRobinet.transform.Rotate(new Vector3(180, 0, 0), Space.Self);
                hFluxRobinet.transform.position = new Vector3(hFluxRobinet.transform.position.x, hJusPositif.transform.position.y, hFluxRobinet.transform.position.z);
            }

            //Faire monter les Deux cylindres de jus
            if (hJusPositif.transform.localScale.y < 1f * fScaleJusPositif && hJusNegatif.transform.localScale.y < 1f * fScaleJusNegatif)
            {
                //Faire baisser le flux
               
                if (hJusPositif.transform.localScale.y < 0.8f*fScaleJusPositif && hJusNegatif.transform.localScale.y < 0.8f*fScaleJusNegatif)
                {
                    hJusPositif.transform.localScale = new Vector3(1, hJusPositif.transform.localScale.y + fVitesseScaleJusPositif * Time.deltaTime, 1);
                    hJusNegatif.transform.localScale = new Vector3(1, hJusNegatif.transform.localScale.y + fVitesseScaleJusNegatif * Time.deltaTime, 1);
                    hTourneRobinet.transform.Rotate(new Vector3(0, fVitesseRotate * Time.deltaTime * fSensRotation, 0), Space.Self);
                }
                else
                {
                    hJusPositif.transform.localScale = new Vector3(1, hJusPositif.transform.localScale.y + fVitesseScaleJusPositif * Time.deltaTime, 1);
                    hJusNegatif.transform.localScale = new Vector3(1, hJusNegatif.transform.localScale.y + fVitesseScaleJusNegatif * Time.deltaTime, 1);

                    if (hFluxRobinet.transform.localScale.y > 0.1f)
                    {
                        hFluxRobinet.transform.localScale = new Vector3(1, hFluxRobinet.transform.localScale.y - fVitesseScaleFlux * Time.deltaTime, 1);
                    }
                }
            }
            else if (hJusPositif.transform.localScale.y >= 1 * fScaleJusPositif && hJusNegatif.transform.localScale.y >= 1 * fScaleJusNegatif)
            {
                hJusPositif.transform.localScale = new Vector3(1, fScaleJusPositif, 1);
                hJusNegatif.transform.localScale = new Vector3(1, fScaleJusNegatif, 1);
                fEtape = 7;
            }
        }

        if (fEtape == 7)
        {
            if (hFluxRobinet.transform.localScale.y > 0.1f)
            {
                hFluxRobinet.transform.localScale = new Vector3(1, hFluxRobinet.transform.localScale.y - fVitesseScaleFlux * Time.deltaTime, 1);
            }

            if (fTimerDeconte > 0)
            {
                fTimerDeconte = fTimerDeconte - Time.deltaTime;
            }
            else
            {
                fEtape = 8;
            }
        }

        if (fEtape == 8)//quand le scale bon atteint la taille demandée
        {
            //spawn du cylindre multy fruit mauvéééééé / puis scale vers le bas
            hMeshTextFruitNeg.GetComponent<TextMesh>().text = iFruitsNegatif.ToString();
            hMeshTextFruitNeg.GetComponent<MeshRenderer>().enabled = true;
            hMeshTextMinus.GetComponent<MeshRenderer>().enabled = true;

            if (hFluxRobinet.transform.localScale.y > 0.1f)
            {
                hFluxRobinet.transform.localScale = new Vector3(1, hFluxRobinet.transform.localScale.y - fVitesseScaleFlux * Time.deltaTime, 1);
            }

            if (hJusPositif.transform.localScale.y > fPourcentage*fScaleJusPositif)
            {
                hJusPositif.transform.localScale = new Vector3(1, hJusPositif.transform.localScale.y - fVitesseScaleDecompte * Time.deltaTime, 1);
            }
            else if (hJusPositif.transform.localScale.y <= fPourcentage* fScaleJusPositif)
            {
                fEtape = 9;
            }
        }

        //Debug.Log("fEtape" + fEtape);

        if (fEtape == 9)//quand le scale mauvais atteint la taill demander
        {
            if (hFluxRobinet.transform.localScale.y > 0.1f)
            {
                hFluxRobinet.transform.localScale = new Vector3(1, hFluxRobinet.transform.localScale.y - fVitesseScaleFlux * Time.deltaTime, 1);
            }
            // affichage dinamique des score
            // réaction de l'étiquette
            hMeshTextScoreTotal.GetComponent<TextMesh>().text = (iFruitsPositif-iFruitsNegatif).ToString();
            hMeshTextScoreMax.GetComponent<TextMesh>().text = iFruitsPositifsMaxScorePossible.ToString();
            hMeshTextScoreTotal.GetComponent<MeshRenderer>().enabled = true;
            hMeshTextBar.GetComponent<MeshRenderer>().enabled = true;
            hMeshTextScoreMax.GetComponent<MeshRenderer>().enabled = true;
            hMeshTextScoreMaxBarre.GetComponent<MeshRenderer>().enabled = true;


            //lol on l'a pas encore décidé de comment on faisait
            fEtape = 10;

        }

        if (fEtape == 10)//quand le scale mauvais atteint la taill demander
        {
            if ( Input.GetMouseButtonDown(0))
            {
                Application.LoadLevel("Levels");
            }

        }
    }
}
