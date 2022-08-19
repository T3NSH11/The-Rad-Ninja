using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Teleport : AbilityBase
{
    // TP == true if Player is currently teleporting.
    bool TP;
    // sets ray origin to center of the screen.
    Vector3 rayOrigin = new Vector3(0.5f, 0.5f, 0f);

    //True if animation finished
    bool AnimFinished;
    public override void Activation(AbilityMain main)
    {
        Ray ray = Camera.main.ViewportPointToRay(rayOrigin);
        RaycastHit hit;
        RaycastHit hit2;

        // when right mouse button is pressed camera is zoomed in and Teleport marker particle effect is played.
        if (Input.GetMouseButtonDown(1))
        {
            main.TeleportMarker.SetActive(true);
            main.TeleportMarker.GetComponent<ParticleSystem>().Play();
            main.gameObject.GetComponent<ThirdPersonController>().enabled = false;
        }

        // when right mouse button is released camera zoom is set to default and teleport marker particle effect is stopped. particle effect remains playing if player is in the process of teleporting.
        if (Input.GetMouseButtonUp(1))
        {
            if (TP == false)
            {
                main.TeleportMarker.SetActive(true);
                main.TeleportMarker.GetComponent<ParticleSystem>().Stop();
                main.gameObject.GetComponent<ThirdPersonController>().enabled = true;
            }

            //main.CameraObj.GetComponent<CameraController>().distance = 7;
        }

        if (Input.GetMouseButton(1) && TP == false)
        {
            // if ray hits ground teleport marker set to that point.
            if (Physics.Raycast(ray, out hit, main.TeleportRange, main.GroundLayer))
            {
                main.TeleportMarker.transform.position = hit.point;
            }

            // if ray doesn't hit anything then another ray is cast from the the point which is the distance of TeleportRange from the camera and in the origional ray trejectory.
            if (Physics.Raycast(ray.GetPoint(main.TeleportRange), new Vector3(0, -1, 0), out hit2, Mathf.Infinity) && !Physics.Raycast(ray, out hit, main.TeleportRange, main.Climbable))
            {
                main.TeleportMarker.transform.position = hit2.point;
            }

            // if ray hits a climbable object another ray is cast from the point where the origional ray hits the object. Second ray is cast straight upwards to the top of the climbable object.
            if (Physics.Raycast(ray, out hit, main.TeleportRange, main.Climbable))
            {
                if (Physics.Raycast(hit.point, new Vector3(0, 1, 0), out hit2, Mathf.Infinity))
                {
                    main.TeleportMarker.transform.position = hit2.point;
                }
            }

            //if ray hits any object that isn't the ground or a climbable object another ray is cast from the point where the origional ray hits the object. Second ray is cast straight downwards to the ground.
            if (Physics.Raycast(ray, out hit, main.TeleportRange))
            {
                if (!Physics.Raycast(ray, out hit, main.TeleportRange, main.Climbable) && !Physics.Raycast(ray, out hit, main.TeleportRange, main.GroundLayer))
                {
                    if (Physics.Raycast(hit.point, new Vector3(0, -1, 0), out hit2, Mathf.Infinity))
                    {
                        main.TeleportMarker.transform.position = hit2.point;
                    }
                }
            }

            // when player presses left mouse button TP is set to true and smoke trail particle effect is played while the player is set invisible and player collider is disabled.
            if (Input.GetMouseButtonDown(0))
            {
                TP = true;
                main.SmokeTrail.GetComponent<ParticleSystem>().Play();
                main.PlayerRenderer.enabled = false;
                main.Player.GetComponent<CharacterController>().enabled = false;
            }

        }


        if (TP == true && Vector3.Distance(main.Player.transform.position, main.TeleportMarker.transform.position) > 0.2)
        {
            main.Player.transform.position = Vector3.MoveTowards(main.Player.transform.position, main.TeleportMarker.transform.position, main.TeleportSpeed * Time.deltaTime);
            main.gameObject.transform.LookAt(main.TeleportMarker.transform.position);
        }

        // while player is teleporting and the player is more that 0.2 units away from destination player is moved towards the destination at TeleportSpeed.
        //if (TP == true && Vector3.Distance(main.Player.transform.position, main.TeleportMarker.transform.position) > 0.2)
        //{
        //    main.Player.transform.position = Vector3.MoveTowards(main.Player.transform.position, main.TeleportMarker.transform.position, main.TeleportSpeed * Time.deltaTime);
        //}
        // when player is closer that 0.2 units away from the destination TP is set to false, player collider and renderer is enabled, marker particle effect is stopped, and camera zoom is set to default.
        if (Vector3.Distance(main.Player.transform.position, main.TeleportMarker.transform.position) < 0.5)
        {
            TP = false;
            main.SmokeTrail.GetComponent<ParticleSystem>().Stop();
            main.PlayerRenderer.enabled = true;
            main.Player.GetComponent<CharacterController>().enabled = true;
            main.TeleportMarker.SetActive(false);
        }

        if (Input.GetKeyDown(KeyCode.Alpha1))
        {
            main.SwitchState(new ThrowShuriken());
        }
    }
}
