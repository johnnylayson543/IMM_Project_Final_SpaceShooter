using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;

public class EnemyController : MonoBehaviour
{
    private Rigidbody enemyRb;
    private float speed = 50.0f;


    // Start is called before the first frame update
    void Start()
    {
        enemyRb = GetComponent<Rigidbody>();
    }

    // Update is called once per frame
    void Update()
    {
        float stepDistance = speed * Time.deltaTime;
        // Declare Targets with GameObjects if they exist
        Target earthTarget = (GameObject.Find("Earth")) ? new Target(gameObject, GameObject.Find("Earth"), stepDistance) : null;
        Target playerTarget = (GameObject.Find("Player")) ? new Target(gameObject, GameObject.Find("Player"), stepDistance) : null;
        Target originTarget = (GameObject.Find("Origin")) ? new Target(gameObject, GameObject.Find("Origin"), stepDistance) : null;

        // if they exist, mark the physical targets
        if(playerTarget != null) playerTarget.setType(Target.Type.Physical);
        if(earthTarget != null) earthTarget.setType(Target.Type.Physical);

        // place all targets into an array, and use Where array method to create sub-array categories "existing" and "physical"
        Target[] allTargets = { earthTarget, playerTarget, originTarget };
        Target[] existingTargets = allTargets.Where(x => x != null).ToArray();
        Target[] physicalTargets = existingTargets.Where(x => x.getType() == Target.Type.Physical).ToArray();

        // count the physical objects that exist
        int physicalTargetsLeft =  physicalTargets != null ? physicalTargets.Length : 0;

        // deduction: if no physical targets, go to the orgin; if there is one physical target, go to it; if there are two, go to nearest or farthest depending on the objective
        Target closest = (physicalTargetsLeft == 0) ? originTarget : (physicalTargetsLeft == 1 ? physicalTargets[0] : (physicalTargets[0].getDistance() > physicalTargets[1].getDistance() ? physicalTargets[1] : physicalTargets[0]));
        Target farthest = (physicalTargetsLeft == 0) ? originTarget : (physicalTargetsLeft == 1 ? physicalTargets[0] : (physicalTargets[0].getDistance() < physicalTargets[1].getDistance() ? physicalTargets[1] : physicalTargets[0]));
        
        // if the target is far away, set a higher speed multipler based on the log10 of power of 1.3 of the distance;
        float speedMultiplier = (float) Math.Log10(Math.Pow(closest.getDistance(),1.3f));

        // set the rotation toward the target
        transform.rotation = closest.getTurn();
        
        // add a relative force to the rigid body of the enemy spacecraft
        enemyRb.AddRelativeForce(Vector3.forward * stepDistance * speedMultiplier, ForceMode.Impulse);
        
    }



    Target circleTarget(Target t)
    {
        return t;
    }

    
}