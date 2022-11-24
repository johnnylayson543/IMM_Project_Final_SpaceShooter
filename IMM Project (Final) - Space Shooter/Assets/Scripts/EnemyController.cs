using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyController : MonoBehaviour
{
    private Rigidbody enemyRb;
    private float speed = 50.0f;
    private float earthDistance = 0;
    private float playerDistance = 0;
    private Vector3 targetPosition;
    private Vector3 targetDirection;
    private Quaternion targetRotation;


    // Start is called before the first frame update
    void Start()
    {
        enemyRb = GetComponent<Rigidbody>();
    }

    // Update is called once per frame
    void Update()
    {
        GameObject closestObj;
        // Measure distance to the player and the earth
        if (GameObject.Find("Earth") != null && GameObject.Find("Player") != null)
        {
            earthDistance = Vector3.Distance(GameObject.Find("Earth").transform.position, transform.position);
            playerDistance = Vector3.Distance(GameObject.Find("Player").transform.position, transform.position);


            // which is the closest object Earth or the Player
            if (playerDistance > earthDistance)
            {
                closestObj = GameObject.Find("Player");

            }
            else if (playerDistance < earthDistance)
            {
                closestObj = GameObject.Find("Earth");
            }
            else
            {
                closestObj = GameObject.Find("Main Camera");
            }
        } else
        {
            if (GameObject.Find("Earth")) { closestObj = GameObject.Find("Earth"); }
            else { closestObj = GameObject.Find("Origin"); }
        }

        // assign position, rotation, and direction of the target object
        targetPosition = Vector3.MoveTowards(transform.position, closestObj.transform.position, speed * Time.deltaTime);
        targetDirection = (closestObj.transform.position - transform.position).normalized;
        targetRotation = Quaternion.LookRotation(targetDirection);

        // find the distance to the target object
        float targetDistance = Vector3.Distance(closestObj.transform.position, transform.position);
        

        // if the target is far away, set a higher speed multipler based on the log10 of power of 1.3 of the distance;
        float speedMultiplier = (float) Math.Log10(Math.Pow(targetDistance,1.3f));

        // set the rotation toward the target
        transform.rotation = Quaternion.RotateTowards(transform.rotation, targetRotation, 0.5f);
        

        // add a relative force to the rigid body of the enemy spacecraft
        enemyRb.AddRelativeForce(Vector3.forward * Time.deltaTime * speed * speedMultiplier, ForceMode.Impulse);
        
    }

    
}