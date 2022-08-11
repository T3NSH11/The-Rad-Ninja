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

    static bool invulnerable;
    static float invulnerabilityTimer;
    static float invulnerabilityLength;



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


        if (Input.GetKeyDown(KeyCode.Q))
        {
            TakeDamage(5);
        }
    }

    public void TakeDamage(float damageToDeal)
    {
        if (!invulnerable)
        {
            invulnerable = true;
            currentHealth -= damageToDeal;

            invulnerabilityTimer = invulnerabilityLength;
            Countdown();
        }
    }

    static void Countdown()
    {
        invulnerabilityTimer -= Time.deltaTime;

        if (invulnerabilityTimer > 0)
            Countdown();
        else
            invulnerable = false;
    }

    /*static void Heal(float healAmount){
        currentHealth += healAmount;
    }*/

    public static void SetHealth(float setTo)
    {
        currentHealth = setTo;
    }

}
