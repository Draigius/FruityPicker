using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LL_Main_test : MonoBehaviour
{

    public GameObject raisin;
    public GameObject branche;

    // Composant Hinge Joint du Raisin
    private HingeJoint JointRaisin;

    // Composant RigidBody de la branche
    private Rigidbody RigidbodyBranche;

    private GameObject ActualRaisin;
    private GameObject ActualBranche;


    void Start()
    {

    }

    void Update()
    {
        // Spawn branche (et raisin)
        /*if (Input.GetKeyDown(KeyCode.Space))
        {
            // Génére une branche à partir du modele de la prefab
            ActualBranche = Instantiate(branche, new Vector3(0, 0, 0), Quaternion.identity);
            // Récupére le component Rigidbody de la branche ainsi généré
            RigidbodyBranche = ActualBranche.GetComponent<Rigidbody>();

            // Génére un raisin à partir du modele de la prefab
            ActualRaisin = Instantiate(raisin, new Vector3(0, -1, 0), Quaternion.identity);
            // Récupére le component HingeJoint du raisin ainsi généré
            JointRaisin = ActualRaisin.GetComponent<HingeJoint>();

            // On attache le Rigidbody de la branche au HingeJoint du raisin
            JointRaisin.connectedBody = RigidbodyBranche;
        }*/

        if (Input.GetMouseButtonDown(0))
        {
           
        }
    }
}