using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Teleport : AbilityBase
{
    Vector3 rayOrigin = new Vector3(0.5f, 0.5f, 0f);
    public override void Activation(AbilityMain main)
    {
        Ray ray = Camera.main.ViewportPointToRay(rayOrigin);
        RaycastHit hit;
        RaycastHit hit2;

        if (Input.GetMouseButtonDown(0))
        {
            main.TeleportMarker.SetActive(true);
        }

        if (Physics.Raycast(ray, out hit, main.TeleportRange, main.GroundLayer))
        {
            main.TeleportMarker.transform.position = hit.point + new Vector3(0, 1, 0);
        }
        else if(Physics.Raycast(ray.GetPoint(main.TeleportRange), new Vector3(0, -1, 0), out hit2, Mathf.Infinity, main.GroundLayer))
        {
            main.TeleportMarker.transform.position = hit2.point + new Vector3(0, 1, 0);
        }
        Debug.Log (ray.GetPoint(main.TeleportRange));
        if (Input.GetMouseButtonUp(0))
        {
            main.Player.transform.position = main.TeleportMarker.transform.position;
            main.TeleportMarker.SetActive(false);
        }
    }

    public override void Action(AbilityMain main)
    {

    }
}
