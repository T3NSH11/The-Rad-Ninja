using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ThrowShuriken : AbilityBase
{
    Vector3 rayOrigin = new Vector3(0.5f, 0.5f, 0f);
    public override void Activation(AbilityMain main)
    {
        Ray ray = Camera.main.ViewportPointToRay(rayOrigin);
        RaycastHit hit;

        if (Input.GetMouseButton(1))
        {
            //main.transform.LookAt((main.CameraObj.transform.position - main.transform.position).normalized);

            main.Crosshair.SetActive(true);

            if (Input.GetMouseButtonDown(0))
            {
                if (Physics.Raycast(ray, out hit, Mathf.Infinity))
                {
                    GameObject shureken = GameObject.Instantiate(main.ShurikenObj, new Vector3(main.Player.transform.position.x, 1, main.Player.transform.position.z), Quaternion.identity);

                    shureken.GetComponent<Rigidbody>().AddForce((hit.point - main.Player.transform.position).normalized * main.ShurikenSpeed);
                }
            }
        }
        else
        {
            main.Crosshair.SetActive(false);
        }

        if (Input.GetKeyDown(KeyCode.Alpha2))
        {
            main.SwitchState(new Teleport());
        }

        if (Input.GetKeyDown(KeyCode.Alpha3))
        {
            main.SwitchState(new Blind());
        }
        if (Input.GetKeyDown(KeyCode.Alpha4))
        {
            main.SwitchState(new Takedown());
        }
    }
}
