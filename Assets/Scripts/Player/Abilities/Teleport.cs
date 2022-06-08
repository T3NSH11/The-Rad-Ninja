using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Teleport : AbilityBase
{
    Vector3 rayOrigin = new Vector3(0.5f, 0.5f, 0f);
    bool activate;
    public override void Activation(AbilityMain main)
    {
        Ray ray = Camera.main.ViewportPointToRay(rayOrigin);
        RaycastHit hit;
        RaycastHit hit2;

        if (Input.GetMouseButtonDown(1))
        {
            activate = true;
            main.CameraObj.GetComponent<Tempcamera>().distance = 4;
        }

        if (Input.GetMouseButton(1) && activate == true)
        {
            main.TeleportMarker.SetActive(true);

            if (Physics.Raycast(ray, out hit, main.TeleportRange, main.GroundLayer) && !Physics.Raycast(ray, out hit2, main.TeleportRange, main.Climbable))
            {
                main.TeleportMarker.transform.position = hit.point + new Vector3(0, 1, 0);
            }
            else if (Physics.Raycast(ray, out hit, main.TeleportRange, main.Climbable))
            {
                float DistanceToHit = Vector3.Distance(hit.point, main.CameraObj.transform.position);

                if (Physics.Raycast(ray.GetPoint(DistanceToHit), new Vector3(0, 1, 0), out hit, Mathf.Infinity, main.GroundLayer))
                {
                    main.TeleportMarker.transform.position = hit.point + new Vector3(0, 1, 0);
                }
            }
            else if (Physics.Raycast(ray.GetPoint(main.TeleportRange), new Vector3(0, -1, 0), out hit, Mathf.Infinity, main.GroundLayer))
            {
                main.TeleportMarker.transform.position = hit.point + new Vector3(0, 1, 0);
            }
        }

        if (Input.GetMouseButtonUp(1))
        {
            main.TeleportMarker.SetActive(false);
            main.CameraObj.GetComponent<Tempcamera>().distance = 7;
        }

        if (Input.GetMouseButtonDown(0) && main.TeleportMarker.activeSelf == true)
        {
            main.Player.transform.position = main.TeleportMarker.transform.position;
            main.TeleportMarker.SetActive(false);
            activate = false;
        }
    }
}
