using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Shuriken : AbilityBase
{
    Vector3 rayOrigin = new Vector3(0.5f, 0.5f, 0f);
    public override void Activation(AbilityMain main)
    {
        Ray ray = Camera.main.ViewportPointToRay(rayOrigin);

        if (Input.GetMouseButtonDown(1))
        {
            main.CameraObj.GetComponent<Tempcamera>().distance = 4;
            main.Crosshair.SetActive(true);
        }

        if (Input.GetMouseButtonUp(1))
        {
            main.CameraObj.GetComponent<Tempcamera>().distance = 7;
            main.Crosshair.SetActive(false);
        }

        if (Input.GetMouseButtonDown(0) && Input.GetMouseButton(1))
        {
            GameObject shureken = GameObject.Instantiate(main.ShurikenObj, main.Player.transform.position, Quaternion.identity);
            
            shureken.GetComponent<Rigidbody>().AddForce((ray.GetPoint(1000) - main.Player.transform.position).normalized * main.ShurikenSpeed);
        }
    }
}
