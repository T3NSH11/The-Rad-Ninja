using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CameraController : MonoBehaviour
{

    public Camera cam;

    public Transform player;
    [SerializeField] Vector3 offset = new Vector3(0, 0, 0); 
    Vector3 playerPosition;  // player position, plus our camera's offset, aka our target




    float horizontalInput;
    float verticalInput;
    [SerializeField] float mouseSensitivity = 20f;

    [SerializeField] float minYRot = -30f;
    [SerializeField] float maxYRot = 70f;  // control how high and low the camera can move

    KeyCode resetPositionKey = KeyCode.P;


    Vector3 desiredPosition;     // final position for the camera to move to after calculation
    Vector3 adjustedDesiredPos; 

    [SerializeField] public float distanceFromPlayer = 4;
    [SerializeField] float adjustedDistance;   // used in calculating desiredPosition and adjustedDesiredPos, respectively

    Vector3 followSmoothing;
    [SerializeField] float movementSpeed = 0.2f; // used for the SmoothDamp in camera movement.


    Vector3 rotationSmoothing;
    [SerializeField] float rotationDuration = .17f; // for SmoothDamp in rotation




    public LayerMask collisionLayer;
    CameraCollisionHandler collisionHandler; // see collisionhandler below




    void Start(){

        player = GameObject.Find("CameraTracker").transform;
        Cursor.lockState = CursorLockMode.Locked;

        
        cam = Camera.main;
        collisionHandler = new CameraCollisionHandler(cam, collisionLayer);
        
    }


    // get and adjust player input values here.
    void Update(){


        horizontalInput += Input.GetAxis("Mouse X") * mouseSensitivity;
        verticalInput -= Input.GetAxis("Mouse Y") * mouseSensitivity;

        verticalInput = Mathf.Clamp(verticalInput, minYRot, maxYRot); // split input to separate axes to clamp the y

        if (Input.GetKey(resetPositionKey))
        {
            horizontalInput = -player.forward.x;
            verticalInput = -player.forward.y;
        }

    }


    // move and rotate camera according to input.
    void FixedUpdate(){


        /*
        if (!colliding && Vector3.Distance(playerPosition, transform.position) < 0.2)
        {
            transform.position = Vector3.MoveTowards(playerPosition, transform.position, avoidanceSpeed * Time.deltaTime);
        }

        if (Physics.Linecast(playerPosition, transform.position * collisionSpacing, out hit, collisionLayer))
        {

            desiredPosition = Quaternion.Euler(verticalInput, horizontalInput, 0)
                                * (Vector3.back * hit.distance * distanceAdjustment);//new Vector3(0, 0, -Vector3.Distance(transform.position, hit.point));
            Debug.DrawLine(playerPosition, transform.position, Color.red);
            
        }
        else
        {
            Debug.DrawLine(playerPosition, transform.position, Color.green);
        }

        desiredPosition = Quaternion.Euler(verticalInput, horizontalInput, 0) * (Vector3.back * distanceFromPlayer);
        //desiredPosition += playerPosition;
        transform.position = desiredPosition; //Vector3.SmoothDamp(transform.position, desiredPosition, ref followSmoothing, movementSpeed);
        */
        

        /// movement
        playerPosition = player.position + offset;
        
        desiredPosition = Quaternion.Euler(verticalInput, horizontalInput, 0) * (Vector3.back * distanceFromPlayer);
        desiredPosition += playerPosition;


        if (collisionHandler.ClipPointCollisionDetected(playerPosition))
        {
            adjustedDistance = collisionHandler.GetClipDistance(playerPosition);

            adjustedDesiredPos = Quaternion.Euler(verticalInput, horizontalInput, 0) * (Vector3.back * adjustedDistance);
            adjustedDesiredPos += playerPosition;

            transform.position = Vector3.SmoothDamp(transform.position, adjustedDesiredPos, ref followSmoothing, movementSpeed);
            //transform.position = adjustedDesiredPos;
        }
        else
        {
            transform.position = Vector3.SmoothDamp(transform.position, desiredPosition, ref followSmoothing, movementSpeed);
            //transform.position = desiredPosition;
        }




        /// rotation 
        transform.rotation = Quaternion.Euler(
                        Vector3.SmoothDamp(transform.rotation.eulerAngles, playerPosition - transform.position, 
                                ref rotationSmoothing, rotationDuration));
        transform.LookAt(playerPosition);

        //Quaternion desiredRotation = Quaternion.LookRotation(playerPosition - transform.position);
        //transform.rotation = Quaternion.Lerp(transform.rotation, desiredRotation, rotationDuration);




        /// collision update
        collisionHandler.UpdateClipPoints(desiredPosition, transform.rotation, collisionHandler.desiredClipPoints);

        adjustedDistance = collisionHandler.GetClipDistance(playerPosition);

        /*for (int i = 0; i < 5; i++)
        {
            //Debug.DrawLine(playerPosition, collisionHandler.adjustedClipPoints[i], Color.white);
            Debug.DrawLine(playerPosition, collisionHandler.desiredClipPoints[i], Color.yellow);
        }*/

    }


}




[System.Serializable]
public class CameraCollisionHandler
{
    public LayerMask collisionLayer;


    //public Vector3[] adjustedClipPoints;
    public Vector3[] desiredClipPoints;
    public float clippingDistance = 3.41f;

    public Camera camera;
    
    public CameraCollisionHandler(Camera cam, LayerMask layer){

        camera = cam;
        //adjustedClipPoints = new Vector3[5];
        desiredClipPoints = new Vector3[5];
        collisionLayer = layer;
        
    }


    // use near clip plane / corners of the camera view as reference for collision checking raycast.
    public void UpdateClipPoints(Vector3 cameraPos, Quaternion currentRotation, Vector3[] clipPointArray){


        // get the corners and middle of the start of the camera's frustrum, depending on FOV and aspect
        float z = camera.nearClipPlane; 
        float x = Mathf.Tan(camera.fieldOfView / clippingDistance) * z;
        float y = x / camera.aspect;


        // add each corner to an array
        clipPointArray[0] = (currentRotation * new Vector3(-x, y, z)) + cameraPos; // top left
        clipPointArray[1] = (currentRotation * new Vector3(x, y, z)) + cameraPos; // top right
        clipPointArray[2] = (currentRotation * new Vector3(-x, -y, z)) + cameraPos; // bottom left
        clipPointArray[3] = (currentRotation * new Vector3(x, -y, z)) + cameraPos; // bottom right
        clipPointArray[4] = cameraPos - camera.transform.forward;

    }


    // check each point of our array for ray collision
    public bool ClipPointCollisionDetected(Vector3 fromPosition){


        for (int i = 0; i < desiredClipPoints.Length; i++){

            Ray ray = new Ray(fromPosition, desiredClipPoints[i] - fromPosition);
            float distance = Vector3.Distance(desiredClipPoints[i], fromPosition);


            if (Physics.Raycast(ray, distance, collisionLayer))
                return true;

        }

        return false;

    }


    // find the closest collision point out of the array and return it.
    public float GetClipDistance(Vector3 fromPosition){


        float clipDistance = -1; // set clip distance to -1 so we can set it in our loop

        for (int i = 0; i < desiredClipPoints.Length; i++)
        {

            Ray ray = new Ray(fromPosition, desiredClipPoints[i] - fromPosition);
            RaycastHit hit;


            if (Physics.Raycast(ray, out hit))
            {
                if (clipDistance == -1)
                    clipDistance = hit.distance;


                else if (hit.distance < clipDistance)
                    clipDistance = hit.distance; 
            }

        }

        return clipDistance;

    }


}
