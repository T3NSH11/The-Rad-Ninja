using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CameraController : MonoBehaviour
{    

    public Transform player;

    [SerializeField] float mouseSensitivity = 20f;

    Vector3 mouseInput;
    Vector3 currentRotation;
    float horizontalRotation;
    float verticalRotation;

    [SerializeField] float minYRot = -30f;
    [SerializeField] float maxYRot = 70f;
    Vector3 rotationSmoothing;
    [SerializeField] float rotationDuration = .17f;

    [SerializeField] float distanceToPlayer = 4;
    //[SerializeField] float trackingSpeed = 1.5f;

    void Start(){

        player = GameObject.Find("CameraTracker").transform;
        Cursor.lockState = CursorLockMode.Locked;

    }

    void LateUpdate(){

        //mouseInput += new Vector3(Input.GetAxis("Mouse Y"), Input.GetAxis("Mouse X")) * mouseSensitivity * Time.deltaTime;
        //mouseInput.y = Mathf.Clamp(mouseInput.y, minYRot, maxYRot);

        horizontalRotation += Input.GetAxis("Mouse X") * mouseSensitivity;
        verticalRotation -= Input.GetAxis("Mouse Y") * mouseSensitivity;
        verticalRotation = Mathf.Clamp(verticalRotation, minYRot, maxYRot); // split input to separate axes to clamp the y

        currentRotation = Vector3.SmoothDamp(currentRotation,
                    new Vector3(verticalRotation, horizontalRotation), ref rotationSmoothing, rotationDuration);
        transform.eulerAngles = currentRotation;//new Vector3(mouseInput.x, Mathf.Clamp(mouseInput.y, minYRot, maxYRot));

        transform.position = player.position - transform.forward * distanceToPlayer; //Vector3.Lerp(transform.position, player.position - transform.forward * distanceToPlayer, trackingSpeed * Time.deltaTime);


    }



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
