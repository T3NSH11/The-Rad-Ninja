using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CameraController : MonoBehaviour
{

    public Camera cam;

    public Transform player;
           Vector3 playerPosition;
           Vector3 desiredPosition;
           Vector3 adjustedDesiredPos;

           Vector3 followSmoothing;

    public float movementSpeed = 0.3f;


    [SerializeField] Vector3 offset = new Vector3(0, 0, 0);
    [SerializeField] float distanceFromPlayer = 4;
    [SerializeField] float distanceAdjustment;


    float horizontalInput;
    float verticalInput;
    [SerializeField] float mouseSensitivity = 20f;

    [SerializeField] float minYRot = -30f;
    [SerializeField] float maxYRot = 70f;
    [SerializeField] float rotationDuration = .17f;
    Vector3 rotationSmoothing;

    public LayerMask collisionLayer;
    CameraCollisionHandler collisionHandler;
    RaycastHit hit;
    public float collisionSpacing = 1f;



    void Start(){

        player = GameObject.Find("CameraTracker").transform;
        Cursor.lockState = CursorLockMode.Locked;

        
        //cam = Camera.main;
        //collisionHandler = new CameraCollisionHandler(cam, collisionLayer);

        //collisionHandler.Initialise(Camera.main, collisionLayer);
        
    }

    // get and adjust player input values here.
    void Update(){

        horizontalInput += Input.GetAxis("Mouse X") * mouseSensitivity;
        verticalInput -= Input.GetAxis("Mouse Y") * mouseSensitivity;

        verticalInput = Mathf.Clamp(verticalInput, minYRot, maxYRot); // split input to separate axes to clamp the y


        if (Input.GetKeyDown(KeyCode.K))
        {
            CheckpointHandler.Respawn();
        }

    }

    // move and rotate camera according to input.
    void LateUpdate(){

        /// movement
        playerPosition = player.position + offset;
        
        if (Physics.Linecast(playerPosition, transform.position * collisionSpacing, out hit, collisionLayer))
        {

            desiredPosition = Quaternion.Euler(verticalInput, horizontalInput, 0)
                                * (Vector3.back * hit.distance * distanceAdjustment);//new Vector3(0, 0, -Vector3.Distance(transform.position, hit.point));
            Debug.DrawLine(playerPosition, transform.position, Color.red);
        }
        else
        {
            desiredPosition = Quaternion.Euler(verticalInput, horizontalInput, 0) * (Vector3.back * distanceFromPlayer);
            Debug.DrawLine(playerPosition, transform.position, Color.green);
        }

        desiredPosition += playerPosition;
        transform.position = Vector3.SmoothDamp(transform.position, desiredPosition, ref followSmoothing, movementSpeed);
        


        /*
        desiredPosition = Quaternion.Euler(verticalInput, horizontalInput, 0) * (Vector3.back * distanceFromPlayer);
        desiredPosition += playerPosition;

        if (collisionHandler.colliding)
        {
            adjustedDesiredPos = Quaternion.Euler(verticalInput, horizontalInput, 0) * (Vector3.back * -distanceAdjustment);
            adjustedDesiredPos += playerPosition;

            //transform.position = Vector3.SmoothDamp(transform.position, desiredPosAdjustedForCollision, ref followSmoothing, followTime);
            transform.position = adjustedDesiredPos;
        }
        else
        {
            //transform.position = Vector3.SmoothDamp(transform.position, desiredPosition, ref followSmoothing, followTime);
            transform.position = desiredPosition;
        }*/



        /// rotation 
        transform.rotation = Quaternion.Euler(
                        Vector3.SmoothDamp(transform.rotation.eulerAngles, playerPosition - transform.position, 
                                ref rotationSmoothing, rotationDuration));
        transform.LookAt(playerPosition);

        //Quaternion desiredRotation = Quaternion.LookRotation(playerPosition - transform.position);
        //transform.rotation = Quaternion.Lerp(transform.rotation, desiredRotation, rotationDuration);




        /// collision update
        /*for (int i = 0; i < 5; i++)
        {
            Debug.DrawLine(playerPosition, collisionHandler.desiredClipPoints[i], Color.white);
            Debug.DrawLine(playerPosition, collisionHandler.desiredClipPoints[i], Color.green);
        }

        collisionHandler.UpdateClipPoints(transform.position, transform.rotation, ref collisionHandler.adjustedClipPoints);
        collisionHandler.UpdateClipPoints(desiredPosition, transform.rotation, ref collisionHandler.desiredClipPoints);

        collisionHandler.CheckForCollision(playerPosition);
        distanceAdjustment = collisionHandler.GetClipDistance(playerPosition);*/

    }

        /*
        //mouseInput += new Vector3(Input.GetAxis("Mouse Y"), Input.GetAxis("Mouse X")) * mouseSensitivity * Time.deltaTime;
        //mouseInput.y = Mathf.Clamp(mouseInput.y, minYRot, maxYRot);
        horizontalRotation += Input.GetAxis("Mouse X") * mouseSensitivity;
        verticalRotation -= Input.GetAxis("Mouse Y") * mouseSensitivity;
        verticalRotation = Mathf.Clamp(verticalRotation, minYRot, maxYRot); // split input to separate axes to clamp the y

        currentRotation = Vector3.SmoothDamp(currentRotation,
                    new Vector3(verticalRotation, horizontalRotation), ref rotationSmoothing, rotationDuration);
        transform.eulerAngles = currentRotation;//new Vector3(mouseInput.x, Mathf.Clamp(mouseInput.y, minYRot, maxYRot));

        transform.position = player.position - transform.forward * distanceToPlayer; //Vector3.Lerp(transform.position, player.position - transform.forward * distanceToPlayer, trackingSpeed * Time.deltaTime);
        */

    /*[SerializeField] GameObject player;
    Vector3 distanceFromPlayer;
    [SerializeField] float moveSpeed;

    void Start(){
        distanceFromPlayer = transform.position - player.transform.position; 
    }

    void LateUpdate(){
        Quaternion rotation = new Quaternion(0, player.transform.rotation.y, 0, 0);
        transform.position = Vector3.Lerp(transform.position, player.transform.position - (distanceFromPlayer * rotation), moveSpeed * Time.deltaTime);
        transform.LookAt(player.transform.position);
    }

    void ResetPosition(){
        // set rotation to behind the player.
        // need to find a quaternion equal to the reverse (negative) of the player's current facing direction, then set the camera to it.
    }*/

}



[System.Serializable]
public class CameraCollisionHandler
{
    public LayerMask collisionLayer;

    public bool colliding = false;
    public Vector3[] adjustedClipPoints;
    public Vector3[] desiredClipPoints;
    public float clippingDistance = 3.41f;

    public Camera camera;
    
    public CameraCollisionHandler(Camera cam, LayerMask layer){

        camera = cam;
        adjustedClipPoints = new Vector3[5];
        desiredClipPoints = new Vector3[5];
        collisionLayer = layer;
        
    }
    /*public void Initialise(Camera cam, LayerMask layer){

        camera = cam;
        adjustedClipPoints = new Vector3[5]; 
        desiredClipPoints = new Vector3[5];
        collisionLayer = layer;

    }*/


    public void UpdateClipPoints(Vector3 cameraPos, Quaternion currentRotation, ref Vector3[] clipPointArray){

        float z = camera.nearClipPlane; 
        float x = Mathf.Tan(camera.fieldOfView / clippingDistance) * z;
        float y = x / camera.aspect;

        clipPointArray[0] = (currentRotation * new Vector3(-x, y, z)) + cameraPos; // top left
        clipPointArray[1] = (currentRotation * new Vector3(x, y, z)) + cameraPos; // top right
        clipPointArray[2] = (currentRotation * new Vector3(-x, -y, z)) + cameraPos; // bottom left
        clipPointArray[3] = (currentRotation * new Vector3(x, -y, z)) + cameraPos; // bottom right
        clipPointArray[4] = cameraPos - camera.transform.forward;
    }

    
    bool ClipPointCollisionDetected(Vector3[] clipPoints, Vector3 fromPosition){

        for (int i = 0; i < clipPoints.Length; i++){

            Ray ray = new Ray(fromPosition, clipPoints[i] - fromPosition);
            float distance = Vector3.Distance(clipPoints[i], fromPosition);

            if (Physics.Raycast(ray, distance, collisionLayer))
                return true;
        }

        return false;
    }

    
    public float GetClipDistance(Vector3 fromPosition)
    {
        float clipDistance = -1;

        for (int i = 0; i < desiredClipPoints.Length; i++)
        {
            Ray ray = new Ray(fromPosition, desiredClipPoints[i] - fromPosition);
            RaycastHit hit;
            if (Physics.Raycast(ray, out hit))
            {
                if (clipDistance == -1)
                    clipDistance = hit.distance;

                else
                {
                    if (hit.distance < clipDistance)
                        clipDistance = hit.distance;
                }
            }
        }

        if (clipDistance == -1)
            return 0;

        else
            return clipDistance;
    }

    public void CheckForCollision(Vector3 targetPos)
    {
        if (ClipPointCollisionDetected(desiredClipPoints, targetPos))
        {
            colliding = true;
        }
        else
        {
            colliding = false;
        }
    }
}
