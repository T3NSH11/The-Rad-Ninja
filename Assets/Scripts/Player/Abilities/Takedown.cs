using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Takedown : AbilityBase
{
    //bool takedownActive = false;
    bool takedownAnimPlay = false;

    Ray checkRay;
    Ray checkRay2;
    Ray checkRay3;
    RaycastHit hit;
    Vector3 checkHeight = new Vector3(0, 1, 0);

    GameObject enemy;

    public override void Activation(AbilityMain Main)
    {
        // check for enemy in range if we're not already in the middle of a takedown
        /*if (!takedownActive)
        {
            checkRay = new Ray(Main.transform.position + checkHeight, 
                           ((Main.transform.forward * Main.takedownRange) + Main.transform.position) + checkHeight);
            checkRay2 = new Ray(Main.transform.position + checkHeight, 
                            ((Main.transform.forward * Main.takedownRange) + Main.transform.position) + checkHeight + Vector3.left);
            checkRay3 = new Ray(Main.transform.position + checkHeight, 
                            ((Main.transform.forward * Main.takedownRange) + Main.transform.position) + checkHeight + Vector3.right);

            Debug.DrawLine(Main.transform.position + checkHeight, 
                ((Main.transform.forward * Main.takedownRange) + Main.transform.position) + checkHeight, Color.red);
            Debug.DrawLine(Main.transform.position + checkHeight, 
                ((Main.transform.forward * Main.takedownRange) + Main.transform.position) + checkHeight + Vector3.left, Color.red);
            Debug.DrawLine(Main.transform.position + checkHeight, 
                ((Main.transform.forward * Main.takedownRange) + Main.transform.position) + checkHeight + Vector3.right, Color.red);

            // set our target according to the hit we get.
            if (Physics.Raycast(checkRay, out hit, Main.takedownRange))// && !Interactable.interactionActive)
            {
                if (hit.collider.CompareTag("Enemy"))
                {
                    enemy = hit.collider.gameObject; 
                    Main.enemyInRange = true;
                }
            }
                else
                    Main.enemyInRange = false;
        }*/
        

        if (Input.GetKey(KeyCode.L) && !Main.takedownActive && Main.Enemy != null)
        {
            //Debug.Log("tried takedown");
            // && !enemy.GetComponent<EnemyManager>().FOV.PlayerDetected)
            
            Main.Player.GetComponent<ThirdPersonController>().enabled = false;
            enemy = Main.Enemy;
            Main.takedownActive = true;
            //enemy.transform.rotation = Main.Player.transform.rotation;
            enemy.transform.rotation = Quaternion.LookRotation(enemy.transform.position - Main.Player.transform.position);

        }

        if (Main.takedownActive)
        {
            if (Vector3.Distance(Main.Player.transform.position, -enemy.transform.forward.normalized + enemy.transform.position) >= 0.2f)
            {
                // move player towards enemy if we aren't close enough for a takedown yet
                Main.Player.transform.position = Vector3.MoveTowards(
                                                    Main.Player.transform.position,
                                                    -enemy.transform.forward.normalized + enemy.transform.position,
                                                    0.4f);
                //Main.Player.transform.rotation = enemy.transform.rotation;
            }
            else
            {
                Main.Player.transform.LookAt(enemy.transform.position);
                //Main.Player.transform.rotation = enemy.transform.rotation;

                enemy.transform.rotation = Main.Player.transform.rotation;
                Main.Player.GetComponent<ThirdPersonController>().enabled = true;
                Main.takedownActive = false;
            }
        }

        /*if (Main.takedownActive && Vector3.Distance(Main.Player.transform.position, enemy.transform.position) < 0.6f)
        {
            enemy.transform.forward = Main.Player.transform.forward;
            //takedownAnimPlay = true;
            // execute takedown animation
            // execute enemy's animation for being taken down
            // play sound clips
            Main.takedownActive = false;
        }*/
    }


}
