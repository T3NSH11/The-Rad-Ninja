using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Teleport : AbilityBase
{
    bool TP;
    Vector3 rayOrigin = new Vector3(0.5f, 0.5f, 0f);
    bool activate;
    public override void Activation(AbilityMain main)
    {
        Ray ray = Camera.main.ViewportPointToRay(rayOrigin);
        RaycastHit hit;
        RaycastHit hit2;

<<<<<<< Updated upstream
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
=======
        if (Input.GetMouseButton(1))
        {
            main.TeleportMarker.GetComponent<ParticleSystem>().Play();
        }
        else
        {
            main.TeleportMarker.GetComponent<ParticleSystem>().Pause();
        }

        if (Physics.Raycast(ray, out hit, main.TeleportRange, main.GroundLayer) && TP == false)
        {
            main.TeleportMarker.transform.position = hit.point + new Vector3(0, 0, 0);
        }

        if (Physics.Raycast(ray.GetPoint(main.TeleportRange), new Vector3(0, -1, 0), out hit2, Mathf.Infinity) && TP == false && !Physics.Raycast(ray, out hit, main.TeleportRange, main.Climbable))
        {
            main.TeleportMarker.transform.position = hit2.point + new Vector3(0, 0, 0);
        }

        if (Physics.Raycast(ray, out hit, main.TeleportRange, main.Climbable) && TP == false)
        {
            if (Physics.Raycast(hit.point, new Vector3(0, 1, 0), out hit2, Mathf.Infinity))
            {
                main.TeleportMarker.transform.position = hit2.point + new Vector3(0, 0, 0);
            }
        }

        if (Input.GetMouseButton(1) && Input.GetMouseButtonDown(0))
>>>>>>> Stashed changes
        {
            TP = true;
            main.Player.transform.Find("SmokeTrail").gameObject.GetComponent<ParticleSystem>().Play();
            main.Player.GetComponent<MeshRenderer>().enabled = false;
            main.Player.GetComponent<CapsuleCollider>().enabled = false;
        }

        if (TP == true && Vector3.Distance(main.Player.transform.position, main.TeleportMarker.transform.position + new Vector3(0, 1, 0)) > 0.2)
        {
            main.Player.transform.position = Vector3.MoveTowards(main.Player.transform.position, main.TeleportMarker.transform.position + new Vector3(0, 1, 0), main.TeleportSpeed * Time.deltaTime);
        }
        else if (TP == true && Vector3.Distance(main.Player.transform.position, main.TeleportMarker.transform.position + new Vector3(0, 1, 0)) < 0.2)
        {
            TP = false;
            main.TeleportMarker.SetActive(false);
<<<<<<< Updated upstream
            activate = false;
        }
    }
=======
            main.Player.transform.Find("SmokeTrail").gameObject.GetComponent<ParticleSystem>().Stop();
            main.Player.GetComponent<MeshRenderer>().enabled = true;
            main.Player.GetComponent<CapsuleCollider>().enabled = true;

        }
    }


>>>>>>> Stashed changes
}
