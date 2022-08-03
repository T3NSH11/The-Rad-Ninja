using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class PlayerVisibilityController : MonoBehaviour
{
    [SerializeField] Slider visLevelSlider;
    public float visibility { get; private set; }

    float currentDistance = 0f;
    [SerializeField] float distanceMultiplier = 1.8f; // higher values mean the player will be less visible at their distance from the nearest lightsource.
    [SerializeField] float obstructionMultiplier = 2.88f; 
    public float lerpTime;

    GameObject closestLight; 
    GameObject[] lightsources;



    void Start()
    {
        lightsources = GameObject.FindGameObjectsWithTag("Light"); // get references to all lights in scene
    }

    void Update()
    {
        visLevelSlider.minValue = -100f;
        visLevelSlider.maxValue = 0f;
        visLevelSlider.value = -visibility; // the lower the value, the more visible.
        

        currentDistance = Mathf.Infinity;

        // compare each light's distance to the current closest light
        for (int i = 0; i < lightsources.Length; i++)
        {
            float checkDistance = Vector3.Distance(transform.position, lightsources[i].transform.position);

            if (checkDistance < currentDistance)
            {
                closestLight = lightsources[i];
                currentDistance = checkDistance;
            }

        }

        Ray ray = new Ray(transform.position, closestLight.transform.position - transform.position);
        RaycastHit hit;

        if (Physics.Raycast(ray, out hit, Vector3.Distance(transform.position, closestLight.transform.position)))
        {
            currentDistance *= obstructionMultiplier;
        }


        currentDistance = Mathf.Clamp(currentDistance, 0, 100);
        visibility = Mathf.Lerp(visibility, currentDistance * distanceMultiplier, lerpTime);


        //Debug.Log($"closest light: {closestLight.name}\ndistance: {currentDistance}");
    }

    private void OnDrawGizmos()
    {
        for (int i = 0; i < lightsources.Length; i++)
        {
            Gizmos.DrawSphere(lightsources[i].transform.position, 0.5f);
        }
    }
}
