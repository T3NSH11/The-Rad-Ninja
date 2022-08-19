using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Blind : AbilityBase
{
    Vector3 rayOrigin = new Vector3(0.5f, 0.5f, 0f);
    public override void Activation(AbilityMain Main)
    {
        GameObject Blinder = null;
        Ray ray = Camera.main.ViewportPointToRay(rayOrigin);
        RaycastHit hit;

        if (Input.GetMouseButton(1))
        {
            //main.transform.LookAt((main.CameraObj.transform.position - main.transform.position).normalized);

            Main.Crosshair.SetActive(true);

            if (Input.GetMouseButtonDown(0) && Blinder == null)
            {
                if (Physics.Raycast(ray, out hit, Mathf.Infinity))
                {
                    Blinder = GameObject.Instantiate(Main.BlindingOBJ, new Vector3(Main.Player.transform.position.x, 1, Main.Player.transform.position.z), Quaternion.Euler(180, 0, 0));
                    Blinder.GetComponentInChildren<ParticleSystem>().Play();

                    //Blinder.transform.position += Vector3.MoveTowards(Main.transform.position, hit.point, Main.BlinderSpeed * Time.deltaTime);
                    Blinder.GetComponent<Rigidbody>().AddForce((hit.point - Main.Player.transform.position).normalized * Main.BlinderSpeed);
                }
            }
        }
        else
        {
            Main.Crosshair.SetActive(false);
        }

        if (Input.GetKeyDown(KeyCode.Alpha2))
        {
            Main.SwitchState(new Teleport());
        }

        if (Input.GetKeyDown(KeyCode.Alpha1))
        {
            Main.SwitchState(new ThrowShuriken());
        }
    }
}
