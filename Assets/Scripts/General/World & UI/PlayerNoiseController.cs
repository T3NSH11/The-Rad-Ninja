using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class PlayerNoiseController : MonoBehaviour
{
    [SerializeField] Slider noiseLevelSlider;

    public float currentNoiseLevel { get; private set; }
    static float noiseAdjustment;  // how much to adjust noise by

    [SerializeField] float rateOfIncrease;
    [SerializeField] float rateOfDecrease; // used to smooth between current noise and adjustment

    float increaseRef;
    float decreaseRef;


    public float increaseBy;
    public float maxSound; //test values.


    void Start(){
        currentNoiseLevel = 0;
        noiseAdjustment = 0;

        noiseLevelSlider.maxValue = 100;
    }

    
    void Update(){

        noiseLevelSlider.value = currentNoiseLevel;

        // continually move current level to desired level (added to through action, decreased over time)
        currentNoiseLevel = Mathf.SmoothDamp(currentNoiseLevel, 
                                              noiseAdjustment, 
                                              ref increaseRef,
                                              rateOfIncrease);
        // decrease the desired level
        noiseAdjustment = Mathf.SmoothDamp(noiseAdjustment, 
                                            0,
                                            ref decreaseRef,
                                            rateOfDecrease);

        /*
        if (DialogueHandler.dialogueActive)
        {
            noiseLevelSlider.gameObject.SetActive(false);
        }

        if (!DialogueHandler.dialogueActive)
        {
            noiseLevelSlider.gameObject.SetActive(true);
        }*/


        if (Input.GetKey(KeyCode.B))
        {
            IncreaseNoise(increaseBy, maxSound);
        }
    }

    // to be called in any functions where the ninja would make some noise (attacking, teleport, etc.)
    // set maxIncrease to maximum noise the function should make.
     static void IncreaseNoise(float increment, float maxIncrease){

        //float originalValue = noiseAdjustment;
        noiseAdjustment += increment;
        noiseAdjustment = Mathf.Clamp(noiseAdjustment, 0, maxIncrease); // make sure the adjustment doesn't go any higher than max volume from an action 
    }

    /*static float IncreaseNoise(float increment)
    {
        float noise = noiseAdjustment;
        noise += increment;

    }*/


}
