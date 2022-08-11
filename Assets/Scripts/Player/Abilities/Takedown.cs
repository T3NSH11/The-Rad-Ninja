using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Takedown : AbilityBase
{
    bool takedownActive;

    Ray checkRay;
    RaycastHit hit;

    GameObject enemy;

    public override void Activation(AbilityMain Main)
    {
        // check for enemy in range if we're not already in the middle of a takedown
        if (!takedownActive)
        {
            checkRay = new Ray(Main.transform.position, Main.transform.forward * Main.takedownRange);

            // set our target according to the hit we get.
            if (Physics.Raycast(checkRay, out hit, Main.takedownRange))// && !Interactable.interactionActive)
            {
                if (hit.collider.CompareTag("Enemy"))
                {
                    enemy = hit.collider.gameObject; 
                    Main.enemyInRange = true;
                }
                else
                    Main.enemyInRange = false;
            }
        }


        if (Input.GetMouseButtonDown(0))
        {
            if (Main.enemyInRange && !enemy.GetComponent<EnemyManager>().FOV.PlayerDetected)
            {
                takedownActive = true;
            }
        }

        if (takedownActive && Vector3.Distance(Main.Player.transform.position, enemy.transform.position) > 0.5)
        {
            // move player towards enemy if we aren't close enough for a takedown yet
            Main.Player.transform.position = Vector3.MoveTowards(
                                                    Main.Player.transform.position,
                                                    enemy.transform.position,
                                                    2f);

        }

        if (takedownActive && Vector3.Distance(Main.Player.transform.position, enemy.transform.position) > 0.5)
        {
            enemy.transform.forward = Main.Player.transform.forward;
            // execute takedown animation
            // execute enemy's animation for being taken down
            // play sound clips
        }
    }


}
