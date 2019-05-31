using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;


public class Proto_TouchScript : MonoBehaviour
{
    // Fruit séléctionné
    GameObject hTouchedObject;
    // Joint du fruit sélectionné
    HingeJoint jTouched;
    // Rigidbody du fruit sélectionné
    public Rigidbody rbTouched;

    // Distance actuelle entre la souris et le fruit sélectionné 
    private float fBreakLimit;

    // Distance entre la souris et le fruit sélectionné pour laquelle le joint se romp
    private float fBreakLimitMax = 2 ;

    // Position de l'objet
    private Vector3 v3TouchedObjectPosition;
    // Position de la souris ( et destination de l'objet sélectionné)
    public Vector3 v3MousePosition;
    // Force donné à l'objet pour qu'il atteigne la position de la souris
    private Vector3 v3ForcePosition;

    private float fMaxDragSpeed = 15;

    public bool bIsDragging = false;
    private bool bFoundObject = false;
    private bool bSecateurActif = false;
    private bool bPressoirActif = false;

    private float oldDrag;

    //Changement scene
    private string nameScene = "";

    //Scale fruits sur clic
    private bool bGrowing = false;
    private float fScaleBase = 0.2f;
    private float fCurrentScale;
    public float fMaxScale;
    private float fScalingTime = 0.1f;
    private GameObject GOTouchedObject = null;



    /// //////////////////////////////////////////////////////////////////CYRILLE-SAN'S COLLIDER
    /// 
    public GameObject mouseCollider;


    ///////////////////////////////////////////////////\\\\\\\\\\\\\\\\\\\\\\\\\\\\\\\\\\\\\\\\\\\\\\\\\\\\\\\\\\

    //                                               UPDATE

    ///////////////////////////////////////////////////\\\\\\\\\\\\\\\\\\\\\\\\\\\\\\\\\\\\\\\\\\\\\\\\\\\\\\\\\\


    void Update()
    {

        // Clic souris
        if (Input.GetMouseButtonDown(0))
        {
            if (funcTouchReturnObject(Input.mousePosition))
            {
                bIsDragging = true;
                rbTouched.useGravity = false;
                oldDrag = rbTouched.drag;
                rbTouched.drag = 10;

                //if (rbTouched.transform.childCount > 0)
                //{
                    GOTouchedObject = hTouchedObject.GetComponent<Jonction>().hMesh;
                    bGrowing = true;
                //}

                //////////////////////////////////////MODIFICATION CYRILLE-SAN

                hTouchedObject.GetComponent<Jonction>().bActive = true;


            }
        }


        ///////////////////////////////////////////CYRILLE-SAN AGAIN
        ///
        if (Input.GetMouseButton(0) && bFoundObject)
        {

            mouseCollider.transform.position = Camera.main.ScreenToWorldPoint(new Vector3(Input.mousePosition.x, Input.mousePosition.y, 9));

        }

        // Clic souris maintenu
        if (Input.GetMouseButton(0) && jTouched== null)
        {
            //funcGAEL();
        }

        // Clic souris relaché
        if (Input.GetMouseButtonUp(0))
        {

            //Désactive Sécateur
            if (bSecateurActif)
            {
                hTouchedObject.GetComponent<SecateurScript>().funcDesactiveSecateur();
                bSecateurActif = false;
            }

            //Désactive Pressoir
            if (bPressoirActif)
            {
                hTouchedObject.GetComponent<PressoirScript>().funcDesactivePressoir();
                bPressoirActif = false;
            }

            
            //Si l'objet trouvé est un fruit
            if (bFoundObject)
            {
                rbTouched.useGravity = true;
                //RigidbodyTouched.isKinematic = true;

                bIsDragging = false;

                //Si le lien n'est pas cassé
                if (rbTouched.GetComponent<HingeJoint>() != null)
                {
                    rbTouched.drag = oldDrag;
                }
                else
                {
                    rbTouched.drag = 0;
                }

                ///////////////////////////////////////////////////////////////////// CYRILLE-SAN = Ici on remet le bActive à false avant de null le htouched et on remet le collider loinnnnnn
                hTouchedObject.GetComponent<Jonction>().bActive = false;
                mouseCollider.transform.position = new Vector3(1000, 1000, 9);


                bFoundObject = false;
                hTouchedObject = null;
                rbTouched = null;

                // Mouvement caméra sur détachement de l'objet
                this.GetComponent<DC_Camera>().OnDetach();
            }
        }

        // Debug
        if (Input.GetKeyDown(KeyCode.Space))
        {
            Debug.Log("Position A : " + v3TouchedObjectPosition );
            Debug.Log("Position B : " + v3MousePosition);
            Debug.Log("Force : " + fBreakLimit);     
        }

        #region SCALE FRUITS
        // Grossisement fruit
        if (bGrowing == true)
        {
            fCurrentScale += Time.deltaTime * (fMaxScale - fScaleBase) / fScalingTime;
            if (fCurrentScale > fMaxScale)
            {
                fCurrentScale = fMaxScale;
                bGrowing = false;
            }
        }

        // Retrecissement fruit
        if (bGrowing == false && (fCurrentScale > fScaleBase))
        {
            fCurrentScale = fScaleBase;
            fCurrentScale -= Time.deltaTime * (fMaxScale - fScaleBase) / fScalingTime;
        }
        else if (bGrowing == false && (fCurrentScale < fScaleBase))
        {
            fCurrentScale = fScaleBase;
        }

        if (GOTouchedObject != null) GOTouchedObject.transform.localScale = new Vector3(fCurrentScale, fCurrentScale, fCurrentScale);



        #endregion
    }

    //////////////////////////////////////////////// SELECTION DE L'OBJET \\\\\\\\\\\\\\\\\\\\\\\\\\\\\\\\\\\\\\\\\\\\\\\\\\\\\\\

    bool funcTouchReturnObject( Vector2 V2ScreenPos )
    {
        RaycastHit hit;

        if (Physics.Raycast(Camera.main.ScreenPointToRay(V2ScreenPos), out hit, 100))
        {

            hTouchedObject = hit.transform.gameObject;
            Camera hMainCam = Camera.main;

            

            //Check Si on a selectionné le sécateur
            if (hTouchedObject.GetComponent<SecateurScript>())
            {
                bSecateurActif = true;
                hTouchedObject.GetComponent<SecateurScript>().funcActiveSecateur();
                return bFoundObject = false;
            }

            //Check Si on a selectionné le pressoir
            if (hTouchedObject.GetComponent<PressoirScript>())
            {
                bPressoirActif = true;
                hTouchedObject.GetComponent<PressoirScript>().funcActivePressoir();
                return bFoundObject = false;
            }

            //Comparer à tablea de jonctions$
            if (hTouchedObject.GetComponent<Jonction>())
            {
                for (int i = 0; i < hMainCam.GetComponent<MainGame>().hTableJunction.Length; i++)
                {
                    GameObject hJonctionCompare = hMainCam.GetComponent<MainGame>().hTableJunction[i];

                    if (hTouchedObject == hJonctionCompare)
                    {
                        //Si a la fin du scan, il y a eu aucun match, return false
                        jTouched = hTouchedObject.GetComponent<HingeJoint>();
                        rbTouched = hTouchedObject.GetComponent<Rigidbody>();

                        //ActualPosition = Camera.main.ScreenToWorldPoint(new Vector3(V2ScreenPos.x, V2ScreenPos.y, 9));
                        v3TouchedObjectPosition = rbTouched.position;
                        //Debug.Log(hit.transform.gameObject);

                        return bFoundObject = true;
                    }
                }
            }
               
            return bFoundObject = false;
        }
        else
        {
            return bFoundObject = false;
        }
    }

    //////////////////////////////////////////////// DEPLACEMENT DE L'OBJET \\\\\\\\\\\\\\\\\\\\\\\\\\\\\\\\\\\\\\\\\\\\\\\\\\\\\\\

    private void FixedUpdate()
    {
        if (!bIsDragging) return;

        // Actualise la position de l'objet.
        v3TouchedObjectPosition = rbTouched.position;
        // Tchek de la position de la souris
        v3MousePosition = Camera.main.ScreenToWorldPoint(new Vector3(Input.mousePosition.x, Input.mousePosition.y, 9));
        // Force qu'il faut donner pour déplacer l'objet séléctionné vers la position de la souris
        v3ForcePosition = v3MousePosition - v3TouchedObjectPosition;

        // Distance à laquelle la souris se trouve de l'objet sélectionné
        fBreakLimit = v3ForcePosition.magnitude;

        // Si la souris est plus loin que la limite fixé, le joint est rompu
        if (fBreakLimit >= fBreakLimitMax && rbTouched.GetComponent<HingeJoint>() != null) {
            this.GetComponent<DC_Camera>().OnDetach();
            jTouched.breakForce = 0;

            //Changement de scene
            if (SceneManager.GetActiveScene().name == "Menu_Fruits")
            {
                
                if (hTouchedObject.GetComponent<HingeJoint>().tag == "Kiwi")
                {

                    nameScene = "Levels";

                    Invoke("changeScene", 1f);
                }


                if (hTouchedObject.GetComponent<HingeJoint>().tag == "Poire")
                {


                    nameScene = "Credits";

                    Invoke("changeScene", 1f);

                }


                if (hTouchedObject.GetComponent<HingeJoint>().tag == "Citron")
                {


                    nameScene = "Options";

                    Invoke("changeScene", 1f);

                }


            }
        }

        // Si la velocité de l'objet est trop élevé, on la refixe
        if (rbTouched.velocity.magnitude > fMaxDragSpeed)
        {
            rbTouched.velocity = Vector3.ClampMagnitude(rbTouched.velocity, fMaxDragSpeed);
            
        }
        // Active la force qui doit déplaçer l'objet sélectionné
        rbTouched.AddForce(v3ForcePosition*3.5f, ForceMode.Impulse);
    }

    void changeScene()
    {
        Application.LoadLevel(nameScene);
    }
}
