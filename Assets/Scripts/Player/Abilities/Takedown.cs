using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Takedown : AbilityBase
{
    bool takedownActive = false;
    bool takedownAnimPlay = false;

    Ray frontCheckRay;
    Ray leftCheckRay;
    Ray rightCheckRay;
    RaycastHit hit;
    Vector3 checkHeight = new Vector3(0, 1, 0);

    GameObject enemy;

    public override void Activation(AbilityMain Main)
    {
        /*
        // check for enemy in range if we're not already in the middle of a takedown
        if (!takedownActive)
        {

            Vector3 checkVectorOrigin = Main.transform.position + checkHeight;
            Vector3 checkVectorDir = ((Main.transform.forward * Main.takedownRange) + Main.transform.position) + checkHeight;

            frontCheckRay = new Ray(Main.transform.position + checkHeight,
                                   ((Main.transform.forward * Main.takedownRange) + Main.transform.position) + checkHeight);

            leftCheckRay = new Ray(Main.transform.position + checkHeight,
                                  ((Main.transform.forward + Vector3.left).normalized * Main.takedownRange + (Main.transform.position + Vector3.left).normalized) + checkHeight);

            rightCheckRay = new Ray(Main.transform.position + checkHeight,
                                   ((Main.transform.forward + Vector3.right).normalized * Main.takedownRange + (Main.transform.position + Vector3.right).normalized) + checkHeight);


            Debug.DrawLine(Main.transform.position + checkHeight,
                ((Main.transform.forward * Main.takedownRange) + Main.transform.position) + checkHeight, Color.red);
            Debug.DrawLine(Main.transform.position + checkHeight,
                ((Main.transform.forward * Main.takedownRange) + Main.transform.position) + checkHeight + Vector3.left, Color.green);
            Debug.DrawLine(Main.transform.position + checkHeight,
                ((Main.transform.forward * Main.takedownRange) + Main.transform.position) + checkHeight + Vector3.right, Color.yellow);

            // set our target according to the hit we get.
            if (Physics.Raycast(frontCheckRay, out hit, Main.takedownRange))// && !Interactable.interactionActive)
            {
                if (hit.collider.CompareTag("Enemy"))
                {
                    //enemy = hit.collider.gameObject; 
                    Main.Enemy = hit.collider.gameObject;
                }
            }
                else
                    Main.Enemy = null;
        }
        */
        

        if (Input.GetMouseButtonDown(0) && !Main.takedownActive && Main.Enemy != null)
        {
            // && !enemy.GetComponent<EnemyManager>().FOV.PlayerDetected)
            
            Main.GetComponent<ThirdPersonController>().enabled = false;
            enemy = Main.Enemy;
            //takedownActive = true;
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
                Main.StartCoroutine(TakedownExecution(Main));
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

    IEnumerator TakedownExecution(AbilityMain Main)
    {
        Main.Player.transform.LookAt(enemy.transform.position);
        //Main.Player.transform.rotation = enemy.transform.rotation;
        //enemy.transform.rotation = Main.Player.transform.rotation;

        //Main.PlayerAnim.Play("Takedown");
        //enemy.GetComponent<Animator>().Play("td");
        //play a sound effect for the takedown.

        //yield return new WaitUntil(Main.PlayerAnim.animDone);
        yield return new WaitForSeconds(2f);

        Main.GetComponent<ThirdPersonController>().enabled = true;
        Main.takedownActive = false;
    }
}
