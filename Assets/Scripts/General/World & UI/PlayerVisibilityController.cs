using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class PlayerVisibilityController : MonoBehaviour
{
    [SerializeField] Slider visLevelSlider;
    public float visibility { get; private set; }

    float currentTotalDistance = 0f;
    float currentDistance = 0f;

    [SerializeField] float distanceDivideBy = 2f;

    [SerializeField] float intensity = 1.8f; // higher values mean the player will be more visible at their distance from the nearest lightsource.
    [SerializeField] float obstructionMultiplier = 2.88f;
    [SerializeField] float maxDistance = 30f; // maximum distance a light can be 
    public float lerpTime;

    GameObject closestLight; 
    GameObject[] lightsources;

    Ray ray;
    RaycastHit hit;

    void Start()
    {
        lightsources = GameObject.FindGameObjectsWithTag("Light"); // get references to all lights in scene
        visLevelSlider.minValue = 0f;
        visLevelSlider.maxValue = 100f;
    }

    void Update()
    {
        currentDistance = Mathf.Infinity;
        currentTotalDistance = 0f;

        // compare each light's distance to the current closest light
        for (int i = 0; i < lightsources.Length; i++)
        {
            /*
            float checkDistance = Vector3.Distance(transform.position, lightsources[i].transform.position);

            if (checkDistance < currentDistance)
            {
                closestLight = lightsources[i];
                currentDistance = checkDistance;
            }
            //else
                //currentDistance = Mathf.Infinity;
            */

            currentTotalDistance += GetLightDistance(lightsources[i]) / distanceDivideBy;

        }

        //Debug.Log(currentTotalDistance);

        /*ray = new Ray(transform.position, closestLight.transform.position - transform.position);
        
        if (Physics.Raycast(ray, out hit, Vector3.Distance(transform.position, closestLight.transform.position)))
        {
            currentDistance *= obstructionMultiplier;
        }*/

        
        //currentDistance = Mathf.Clamp(currentDistance, 0, 100);
        currentTotalDistance = Mathf.Clamp(currentTotalDistance, 0, 100);
        visibility = Mathf.Lerp(visibility, currentTotalDistance * intensity, lerpTime);


        visLevelSlider.value = visibility; // the lower the value, the more visible.


        //Debug.Log($"closest light: {closestLight.name}\ndistance: {currentDistance}");
    }

    float GetLightDistance(GameObject light)
    {
        float dist = Vector3.Distance(transform.position, light.transform.position);

        if (dist < maxDistance)
        {
            ray = new Ray(transform.position, light.transform.position - transform.position);

            if (Physics.Raycast(ray, out hit, dist))
                dist *= obstructionMultiplier;


            return dist;
        }
        else
            return 0;
    }


}
