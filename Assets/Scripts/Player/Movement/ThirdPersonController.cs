using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ThirdPersonController : MonoBehaviour
{

    public Camera MyCamera;
    public float Speed = 2f;
    public float SprintSpeed = 5f;

    public float RotationSpeed = 15f;
    public float AnimationBlendSpeed = 2f;
    CharacterController MyController;
    Animator MyAnimator;

    float mDesiredRotation = 0f;
    float mDesiredAnimationSpeed = 0f;
    bool mSprinting = false;

    float mSpeedY = 0f;
    float mGravity = -9.81f;

    void Start()
    {
        Cursor.lockState = CursorLockMode.Locked;
        MyController = GetComponent<CharacterController>();
        MyAnimator = GetComponent<Animator>();
    }

    // Update is called once per frame
    void Update()
    {
        float x = Input.GetAxisRaw("Horizontal");
        float z = Input.GetAxisRaw("Vertical");

        mSpeedY += mGravity * Time.deltaTime;
        mSprinting = Input.GetKey(KeyCode.LeftShift);

        Vector3 movement = new Vector3(x, 0, z).normalized;

        Vector3 rotatedMovement = Quaternion.Euler(0, MyCamera.transform.rotation.eulerAngles.y, 0) * movement;
        Vector3 verticalMovement = Vector3.up * mSpeedY;

        MyController.Move((verticalMovement + (rotatedMovement * (mSprinting ? SprintSpeed : Speed))) * Time.deltaTime);

        if (rotatedMovement.magnitude > 0)
        {
            mDesiredRotation = Mathf.Atan2(rotatedMovement.x, rotatedMovement.z) * Mathf.Rad2Deg;
            mDesiredAnimationSpeed = mSprinting ? 1f : .5f;
        }
        else
        {
            mDesiredAnimationSpeed = 0;
        }

        if (Input.GetKey(KeyCode.LeftControl))
        {
            MyAnimator.SetBool("Crouching", true);
        }
        else
        {
            MyAnimator.SetBool("Crouching", false);
        }

        MyAnimator.SetFloat("Speed", Mathf.Lerp(MyAnimator.GetFloat("Speed"), mDesiredAnimationSpeed, AnimationBlendSpeed * Time.deltaTime));
        Quaternion currentRotation = transform.rotation;
        Quaternion targetRotation = Quaternion.Euler(0, mDesiredRotation, 0);
        transform.rotation = Quaternion.Lerp(currentRotation, targetRotation, RotationSpeed * Time.deltaTime);
    }
}
