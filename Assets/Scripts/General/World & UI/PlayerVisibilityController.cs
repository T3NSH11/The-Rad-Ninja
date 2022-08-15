using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class PlayerVisibilityController : MonoBehaviour
{
    [SerializeField] Slider visLevelSlider;
    public float visibility { get; private set; }

    //float currentTotalDistance = 0f;
    //float currentDistance = 0f;
    float currentExposure = 0f;

    [SerializeField] float intensityLevel = 132f; // higher values mean the player will be more visible at their distance from the nearest lightsource.

    [SerializeField] float distanceMultiplier = 1.5f; // controls how the distance of light affects its intensity - the further, the less intense
    [SerializeField] float obstructionMultiplier = 1.6f; // conrtols how much objects in the way reduce the intensity of lights
    [SerializeField] float maxCheckingDistance = 30f; // maximum distance from the player to check lights 
    float lerpTime = 0.06f; // controls how fast the slider moves

    //GameObject closestLight; 
    //GameObject[] lightsourceObjs;
    Light[] lightsources;

    Ray ray;
    RaycastHit hit;

    void Start()
    {
        //lightsourceObjs = GameObject.FindGameObjectsWithTag("Light");
        GameObject[] lights = GameObject.FindGameObjectsWithTag("Light"); // get references to all lights in scene
        lightsources = new Light[lights.Length];

        for (int i = 0; i < lights.Length; i++)
            lightsources[i] = lights[i].GetComponent<Light>();
        

        visLevelSlider.minValue = 0f;
        visLevelSlider.maxValue = 100f;
    }

    void Update()
    {
        //currentDistance = Mathf.Infinity;
        //currentTotalDistance = 0f;
        currentExposure = 0f;

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
            //currentTotalDistance += GetLightDistance(lightsourceObjs[i]) / distanceMultiplier;

            currentExposure += (lightsources[i].intensity * intensityLevel) / (GetLightDistance(lightsources[i].gameObject) * distanceMultiplier);
        }

        //currentTotalDistance /= lightsourceObjs.Length;


        /*ray = new Ray(transform.position, closestLight.transform.position - transform.position);
        
        if (Physics.Raycast(ray, out hit, Vector3.Distance(transform.position, closestLight.transform.position)))
        {
            currentDistance *= obstructionMultiplier;
        }*/

        //Debug.Log(currentTotalDistance);
        //print(currentExposure);

        //currentDistance = Mathf.Clamp(currentDistance, 0, 100);
        //currentTotalDistance = Mathf.Clamp(currentTotalDistance, 0, 100);
        currentExposure = Mathf.Clamp(currentExposure, 0, 100);
        visibility = Mathf.Lerp(visibility, currentExposure, lerpTime);


        visLevelSlider.value = visibility; // the lower the value, the more visible.


        //Debug.Log($"closest light: {closestLight.name}\ndistance: {currentDistance}");
    }

    float GetLightDistance(GameObject light)
    {
        float dist = Vector3.Distance(transform.position, light.transform.position);

        if (dist < maxCheckingDistance) // check to see if light is within a certain range
        {
            ray = new Ray(transform.position, light.transform.position - transform.position);

            if (Physics.Raycast(ray, out hit, dist))
                dist *= obstructionMultiplier;


            return dist;
        }
        else
            return 0;
    }

    private void OnDrawGizmos()
    {
        for (int i = 0; i < lightsources.Length; i++)
        {
            Gizmos.DrawSphere(lightsources[i].transform.position, 0.5f);
        }
    }
}
