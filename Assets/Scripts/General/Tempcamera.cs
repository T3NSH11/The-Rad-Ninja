using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Tempcamera : MonoBehaviour
{
    private const float YMin = -50.0f;
    private const float YMax = 50.0f;

    public Transform lookAt;

    public Transform Player;

    public float distance = 1.0f;
    private float currentX = 0.0f;
    private float currentY = 0.0f;
    public float sensivity = 4.0f;

    public Vector3 LookatOfset;

    void Start()
    {
        Cursor.lockState = CursorLockMode.Locked;
    }

    void LateUpdate()
    {

        currentX += Input.GetAxis("Mouse X") * sensivity * Time.deltaTime;
        currentY += Input.GetAxis("Mouse Y") * sensivity * Time.deltaTime;

        currentY = Mathf.Clamp(currentY, YMin, YMax);

        Vector3 Direction = new Vector3(0, 0, -distance);
        Quaternion rotation = Quaternion.Euler(currentY, currentX, 0);

        if (Input.GetMouseButton(1))
        {
            Player.rotation = Quaternion.Euler(0, currentX, 0);
        }

        if (Input.GetMouseButtonDown(1))
        {
            lookAt.localPosition = LookatOfset;
            //distance = 2;
        }
        else if (Input.GetMouseButtonUp(1))
        {
            lookAt.localPosition = new Vector3 (0, 2, 0);
            distance = 4;
        }

        transform.position = lookAt.position + rotation * Direction;

        transform.LookAt(lookAt.position);



    }
}