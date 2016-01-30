using UnityEngine;
using System.Collections;
using UnityEngine.UI;

public class PlayerPuck : MonoBehaviour {

    Ray shootRay;
    RaycastHit shootHit;
    int shootableMask;
    Rigidbody playerRigidbody;

    int floorMask;
    float camRayLength = 100f;

    public float controlForceToward;
    public float controlForceAway;

    float controlForce = 0.0f;
    float controlForceX = 0.0f;
    float controlForceZ = 0.0f;

    public Text MousePosText;
    public Text PlayerToMouseText;

    Vector3 playerToMousePrev;
    Vector3 playerVelPrev;

    // Use this for initialization
    void Start () {
	
	}

    void Awake()
    {
        floorMask = LayerMask.GetMask("Floor");
        playerRigidbody = GetComponent<Rigidbody>();
    }

    void FixedUpdate()
    {

    }

    // Update is called once per frame
    void Update () {
        CalcPosition();
	}

    void CalcPosition ()
    {
        Ray camRay = Camera.main.ScreenPointToRay(Input.mousePosition);
        RaycastHit floorHit;

        if (Physics.Raycast(camRay, out floorHit, camRayLength, floorMask))
        {
            Vector3 playerToMouse = floorHit.point - transform.position;
            playerToMouse.y = 0f;

            Vector3 projectedPos = new Vector3(playerRigidbody.transform.position.x + playerToMouse.x * 0.5f, 0f, playerRigidbody.transform.position.z + playerToMouse.z * 0.5f);

            //MousePosText.text = "Mouse Pos:" + playerToMouse.x + " " + playerToMouse.z;
            //MousePosText.text = "Projected Pos:" + projectedPos.x + " " + projectedPos.z;
            //MousePosText.text = "playerToMouse Pos:" + playerToMouse.x + " " + playerToMouse.z;
            //playerRigidbody.MovePosition(new Vector3(floorHit.point.x, playerRigidbody.transform.position.y, floorHit.point.z));
            //transform.position = new Vector3(floorHit.point.x, playerRigidbody.transform.position.y, floorHit.point.z);

            controlForce = controlForceToward;
            controlForceX = controlForceToward;
            controlForceZ = controlForceToward;
            Vector3 playerVel = playerRigidbody.velocity;


            //if(playerToMousePrev != null)
            if ((playerVelPrev != null) && true)
            {
                float distanceCurrent = Vector3.Distance(playerRigidbody.transform.position, playerToMouse);
                float distancePrev = Vector3.Distance(playerRigidbody.transform.position, playerToMousePrev);
                float distanceDiff = distanceCurrent - distancePrev;
                float velDiff = playerVel.magnitude - playerVelPrev.magnitude;

                float difXCurrent = playerRigidbody.transform.position.x - playerToMouse.x;
                float difZCurrent = playerRigidbody.transform.position.z - playerToMouse.z;

                float difXPrev = playerRigidbody.transform.position.x - playerToMousePrev.x;
                float difZPrev = playerRigidbody.transform.position.z - playerToMousePrev.z;

                float velDiffX = Mathf.Abs(playerVel.x) - Mathf.Abs(playerVelPrev.x);
                float velDiffZ = Mathf.Abs(playerVel.z) - Mathf.Abs(playerVelPrev.z);

                if(velDiffX < 0)
                {
                    controlForceX = controlForceAway;
                }
                if (velDiffZ < 0)
                {
                    controlForceZ = controlForceAway;
                }




                //PlayerToMouseText.text = "PTMc:" + distanceCurrent + "\nPTMp:" + distancePrev + "\n PTMd:" + (distanceDiff);
                PlayerToMouseText.text = "Velc:" + playerVel.magnitude + "\nVelp:" + playerVelPrev.magnitude + "\n Veld:" + (velDiff);
                //if (Vector3.Distance(playerRigidbody.transform.position, playerToMouse) > Vector3.Distance(playerRigidbody.transform.position, playerToMousePrev))
                //if (distanceDiff >= 0f)
                if (velDiff >= 0f)
                {
                    MousePosText.text = "Toward";
                }
                else
                {
                    MousePosText.text = "Away";
                    controlForce = controlForceAway;
                }
            }
            
            //Vector3 forceMove = new Vector3(playerToMouse.x * controlForce, 0.0f, playerToMouse.z * controlForce);
            //Vector3 forceMove = new Vector3(Mathf.Clamp(playerToMouse.x, -4f, 4f) * controlForce, 0.0f, Mathf.Clamp(playerToMouse.z, -4f, 4f) * controlForce);
            Vector3 forceMove = new Vector3(Mathf.Clamp(playerToMouse.x, -10f, 10f) * controlForceX, 0.0f, Mathf.Clamp(playerToMouse.z, -10f, 10f) * controlForceZ);
            playerRigidbody.AddForce(forceMove);
            

            playerToMousePrev = playerToMouse;
            playerVelPrev = playerVel;
        }

    }
}
