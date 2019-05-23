using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CameraFinScript : MonoBehaviour
{

    private GameObject[] hTableFruitSpawn;

    [Header("Nombres Fruits")]
    public int iFruitsPositif;
    public int iFruitsNegatif;

    [Header("Data Dummy")]
    public GameObject hDummyCentreBarrique;
    public GameObject hDummyCibleOrbitale;

    public int iAngleSpawn;
    public float fDummySpawnOffsetY;

    [Header ("Tableau Prefabs")]
    public GameObject[] hTablePrefabeFruitPourris;
    public GameObject[] hTablePrefabeFruitSains;

    [Header("Trigger barrique")]
    public GameObject TriggerBarrique;
    private TriggerBarriqueScript ScriptTriggerBarrique;

    [Header("Pressoir Couvercle Scene")]
    public GameObject PressoirCouvercle;

    [Header("Debug")]
    public float fEtape = 0;

    [Header("Camera Data")]
    public GameObject hCamera1;
    public GameObject hCamera2;
    public float fSpeed;


    // Start is called before the first frame update
    void Start()
    {
        hTableFruitSpawn = new GameObject [iFruitsNegatif+iFruitsPositif];

        //Envoi Data au script de Trigger Barrique
        ScriptTriggerBarrique = TriggerBarrique.GetComponent<TriggerBarriqueScript>();
        ScriptTriggerBarrique.funcSendTotalFruitNumber(iFruitsPositif + iFruitsNegatif);

        //Positionnement Camera
        gameObject.transform.position = hCamera1.transform.position;

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
                    hStockObjectTemporaire = Instantiate(hTablePrefabeFruitPourris[ Mathf.FloorToInt(Random.RandomRange(0, 3.99f)) ], new Vector3(0,100,0), Quaternion.identity);

                    hTableFruitSpawn[i] = hStockObjectTemporaire;

                    iCompteur = iCompteur - 1;
                }
                else
                //Si i est pair
                {
                    //Quel type de fruit spawn
                    hStockObjectTemporaire = Instantiate(hTablePrefabeFruitSains[Mathf.FloorToInt(Random.RandomRange(0, 3.99f))], new Vector3(0, 100, 0), Quaternion.identity);

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
            //faire le mouvement de caméra

            //setup Vitesse
            fSpeed = 8f;

            //Setup vitesse dt
            float fStep = fSpeed * Time.deltaTime;

            //Move Camera
            transform.position = Vector3.MoveTowards(transform.position, hCamera2.transform.position, fStep);

        }

        if (fEtape == 4)//quand la caméra est en position final ou pas loin de sa position final
        {

            //faire tourné de 3/4 le roubinet


        }
        
        if (fEtape == 5)//quand robinet a presque fini de tournet
        {

            //faire couler le fluide


        }

        if (fEtape == 6)//quand robinet a presque fini de tournet
        {

            //faire couler le fluide
            //spawn du cylindre multy fruit bon puis /scale vers le haut


        }

        if (fEtape == 7)//quand le scale bon atteint la taill demander
        {

            //arrété l'écoulement du fluide
            //spawn du cylindre multy fruit mauvéééééé / puis scale vers le bas


        }

        if (fEtape == 8)//quand le scale mauvais atteint la taill demander
        {

            // affichage dinamique des score
            // réaction de l'étiquette


        }
    }
}
