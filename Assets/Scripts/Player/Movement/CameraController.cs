using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CameraController : MonoBehaviour
{

    GameObject cameraObj; // the camera object. in the prefab, this is the first child of the obj with this script
    Camera cam; // reference to above object's camera component

    public Transform playerTarget; // the target to follow.

    
    /// input
    float horizontalInput;
    float verticalInput;
    [SerializeField] float mouseSensitivity = 16.3f;

    [SerializeField] float minYRotation = -30f;
    [SerializeField] float maxYRotation = 60f;  // control how high and low the camera can move



    /// movement
    //Vector3 desiredPosition;     // final position for the camera to move to after calculation
    //Vector3 adjustedDesiredPos; 

              public float distanceFromPlayer = 4f; // how far the camera should be
                     float adjustedDistance;  // used to calculate new camera distance when colliding.

    [SerializeField] float shoulderOffset = 0.8f; // offsets camera position to the side of the player character, depending on how high the value is.


    Vector3 followSmoothing;
    Vector3 collisionSmoothing;
    [SerializeField] float movementSpeed = 0.11f; // used for the Lerp in camera movement.

    [SerializeField] float rotationSpeed = .17f; // for Lerp in rotation.


    
    /// collision handling
    //public LayerMask collisionLayer;

    Vector3[] clipPoints;
    [SerializeField] float clippingRadius = 3.41f; // how wide the space for checking collision should be


    
    void Start(){

        playerTarget = GameObject.Find("CameraTracker").transform;
        Cursor.lockState = CursorLockMode.Locked;

        
        cameraObj = gameObject.transform.GetChild(0).gameObject;
        cam = cameraObj.GetComponent<Camera>();

        clipPoints = new Vector3[5];

    }


    // get and adjust player input values here.
    void Update(){

        horizontalInput += Input.GetAxis("Mouse X") * mouseSensitivity;
        verticalInput -= Input.GetAxis("Mouse Y") * mouseSensitivity;

        verticalInput = Mathf.Clamp(verticalInput, minYRotation, maxYRotation); // split input to separate axes to clamp the y

        /*if (Input.GetKey(resetPositionKey))
        {
            horizontalInput = -player.forward.x;
            verticalInput = -player.forward.y;
        }*/

    }


    // move and rotate camera according to input.
    void LateUpdate() {

        //Vector3 currentCamPos = new Vector3(transform.position.x + shoulderOffset, transform.position.y, transform.position.z);
        if (Time.timeScale > 0f)
        {
            // move this object to the player and rotate it through mouse input (child obj camera moves with it).
            transform.position = Vector3.SmoothDamp(transform.position,
                                                    playerTarget.position, ref followSmoothing,
                                                    movementSpeed);

            transform.rotation = Quaternion.Lerp(transform.rotation,
                                                 Quaternion.Euler(verticalInput, horizontalInput, 0),
                                                 rotationSpeed);


            // adjust camera's relative position on z depending on collision.
            if (ClipPointCollisionDetected(transform.position))
            {
                adjustedDistance = GetCollisionDistance(transform.position);

                cameraObj.transform.localPosition = Vector3.SmoothDamp(cameraObj.transform.localPosition,
                                                             new Vector3(shoulderOffset, 0, -adjustedDistance + 0.2f),
                                                             ref collisionSmoothing, movementSpeed);
            }
            else
            {
                cameraObj.transform.localPosition = Vector3.SmoothDamp(cameraObj.transform.localPosition,
                                                                 new Vector3(shoulderOffset, 0, -distanceFromPlayer),
                                                                 ref collisionSmoothing, movementSpeed);
            }
            //transform.rotation = transform.rotation * Quaternion.Euler(0, offset2, 0);//transform.rotation.z * -1.77f);



            // update collision-checking ray positions with the camera position
            UpdateClipPoints(transform.position, distanceFromPlayer + 0.3f);
        }


        
        for (int i = 0; i < 5; i++)
        {
            Debug.DrawLine(transform.position, clipPoints[i], Color.yellow);
        }


    }

        /*if (collisionHandler.ClipPointCollisionDetected(playerPosition))
        {
            adjustedDistance = collisionHandler.GetClipDistance(playerPosition);
            //adjustedDistance = Mathf.Clamp(adjustedDistance, minDistFromPlayer, distanceFromPlayer);

            cameraObj.transform.localPosition = Vector3.back * adjustedDistance;
        }
        else
        {
            cameraObj.transform.localPosition = Vector3.back * distanceFromPlayer;
        }*/


        /*
        /// movement
        playerPosition = player.position + offset;
        
        desiredPosition = Quaternion.Euler(verticalInput, horizontalInput, 0) * (Vector3.back * distanceFromPlayer);
        desiredPosition += playerPosition;


        if (collisionHandler.ClipPointCollisionDetected(playerPosition))
        {
            adjustedDistance = collisionHandler.GetClipDistance(playerPosition);

            adjustedDesiredPos = Quaternion.Euler(verticalInput, horizontalInput, 0) * (Vector3.back * adjustedDistance);
            adjustedDesiredPos += playerPosition;

            transform.position = Vector3.Lerp(transform.position, adjustedDesiredPos, movementSpeed);
            //transform.position = adjustedDesiredPos;
        }
        else
        {
            transform.position = Vector3.Lerp(transform.position, desiredPosition, movementSpeed);
            //transform.position = desiredPosition;
        }



        
        /// rotation 
        //transform.rotation = Quaternion.Euler(
                        //Vector3.SmoothDamp(transform.rotation.eulerAngles, playerPosition - transform.position, 
                                //ref rotationSmoothing, rotationDuration));
        transform.LookAt(playerPosition + new Vector3(0,0,2));

        //Quaternion desiredRotation = Quaternion.LookRotation(playerPosition - transform.position);
        //transform.rotation = Quaternion.Lerp(transform.rotation, desiredRotation, rotationDuration);
        */







    // use near clip plane / corners of the camera view as reference for collision checking raycast.
    void UpdateClipPoints(Vector3 cameraPos, float distanceToCam){


        // get the corners and middle of the start of the camera's frustrum, depending on FOV and aspect
        float z = cam.nearClipPlane; 
        float x = Mathf.Tan(cam.fieldOfView / clippingRadius) * z;
        float y = x / cam.aspect;

        //Quaternion rotation = Quaternion.Euler(cameraObj.transform.rotation.x * shoulderOffset, cameraObj.transform.rotation.y, cameraObj.transform.rotation.z);
        // add each corner to an array
        clipPoints[0] = (cameraObj.transform.rotation * new Vector3(-x, y, z - distanceToCam)) + cameraPos; // top left
        clipPoints[1] = (cameraObj.transform.rotation * new Vector3(x, y, z - distanceToCam)) + cameraPos; // top right
        clipPoints[3] = (cameraObj.transform.rotation * new Vector3(x, -y, z - distanceToCam)) + cameraPos; // bottom right
        clipPoints[2] = (cameraObj.transform.rotation * new Vector3(-x, -y, z - distanceToCam)) + cameraPos; // bottom left
        clipPoints[4] = cameraPos - cam.transform.forward * distanceToCam;// (cameraObj.transform.rotation * new Vector3(cameraPos.x * (shoulderOffset * 2), cameraPos.y, cameraPos.z - distanceToCam)) + cameraPos;// - cam.transform.forward * distanceFromPlayer;// cameraPos - cam.transform.forward + new Vector3(shoulderOffset, 0, distanceToCam);
                                                                                            //new Vector3(cameraPos.x + shoulderOffset, cameraPos.y, cameraPos.z);
                                                                                            // cameraObj.transform.rotation * new Vector3(cameraObj.transform.position.x + shoulderOffset, cameraObj.transform.position.y, cameraObj.transform.position.z - distanceToCam);
    }


    // check each point of our array for ray collision
    bool ClipPointCollisionDetected(Vector3 fromPosition){


        for (int i = 0; i < clipPoints.Length; i++){

            Ray ray = new Ray(fromPosition, clipPoints[i] - fromPosition);
            float distance = Vector3.Distance(clipPoints[i], fromPosition);


            if (Physics.Raycast(ray, distance))
                return true;

        }

        return false;

    }


    // find the closest collision point out of the array and return it.
    float GetCollisionDistance(Vector3 fromPosition){


        float collidingDistance = -1; // set raycast hit distance to -1 so we can set it in our loop


        for (int i = 0; i < clipPoints.Length; i++)
        {

            Ray ray = new Ray(fromPosition, clipPoints[i] - fromPosition);
            RaycastHit hit;

            if (Physics.Raycast(ray, out hit) && hit.collider.enabled)
            {
                //colliding = true;


                if (collidingDistance == -1)
                    collidingDistance = hit.distance;


                else if (hit.distance < collidingDistance)
                    collidingDistance = hit.distance; 
            }
            //else
                //colliding = false;
        }

        return collidingDistance;

    }


}
