using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class PlayerHealth : MonoBehaviour
{

    [SerializeField] Slider healthbarSlider; // set to the health slider in the UI.
    float slideSpeed;

    public static float currentHealth { get; private set; } // in case other scripts need current health
    [SerializeField] float maxHealth = 100;
    float lowHealth;
    float healthbarLerpValue;



    void Start(){

        currentHealth = maxHealth;
        lowHealth = maxHealth / 3;

        //healthbarSlider.maxValue = maxHealth;
        //healthbarSlider.value = currentHealth;
    }


    void Update(){

        //healthbarLerpValue = Mathf.MoveTowards(healthbarSlider.value, currentHealth, slideSpeed);
        //healthbarSlider.value = currentHealth;//Mathf.Lerp(healthbarSlider.value, currentHealth, slideSpeed);


        if (currentHealth <= lowHealth)
        {
            // low on health things like tired animation, flashing healthbar etc
        }
        
        if (currentHealth <= 0)
        {
            // death animation
            // go to game over screen
        }

        /*
        if (DialogueHandler.dialogueActive)
        {
            healthbarSlider.gameObject.SetActive(false);
        }

        if (!DialogueHandler.dialogueActive)
        {
            healthbarSlider.gameObject.SetActive(true);
        }*/


        if (Input.GetKeyDown(KeyCode.Q))
        {
            TakeDamage(5);
        }

        if(currentHealth <= 0)
        {
            Debug.Log("Dead");
        }
    }

    public void TakeDamage(float damageToDeal)
    {
        currentHealth -= damageToDeal;
    }

    /*static void Heal(float healAmount){
        currentHealth += healAmount;
    }*/

}
