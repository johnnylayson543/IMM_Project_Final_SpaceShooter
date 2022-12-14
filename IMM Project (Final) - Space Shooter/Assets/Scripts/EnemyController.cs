using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;

public class EnemyController : MonoBehaviour
{
    private Rigidbody enemyRb;  // the rigid body component
    public float speed = 25.0f;  // the speed of the spacecraft
    public float speedLimitFactor = 0.75f; // the speed limit factor of the spacecraft
    


    // Start is called before the first frame update
    void Start()
    {
        enemyRb = GetComponent<Rigidbody>();  // get rigid body component and assign it a variable 
    }

    // Update is called once per frame
    void Update()
    {
        float stepDistance = speed * Time.deltaTime; // the distance travelled by a spacecraft in delta time  
        

        // Declare Targets with GameObjects if they exist
        Target earthTarget = (GameObject.Find("Earth")) ? new Target(gameObject, GameObject.Find("Earth"), stepDistance) : null;
        Target playerTarget = (GameObject.Find("Player")) ? new Target(gameObject, GameObject.Find("Player"), stepDistance) : null;
        Target originTarget = (GameObject.Find("Origin")) ? new Target(gameObject, GameObject.Find("Origin"), stepDistance) : null;
        Target enemyCentreOfMass = TargetOverwatch.swarmFormationTargetPerSwarmMember(gameObject, stepDistance);
        enemyCentreOfMass.setType(Target.Type.Tactical);

        // if they exist, mark the physical targets
        if (playerTarget != null) playerTarget.setType(Target.Type.Physical);
        if(earthTarget != null) earthTarget.setType(Target.Type.Physical);

        // place all targets into an array, and use Where array method to create sub-array categories "existing" and "physical"
        Target[] allTargets = { earthTarget, playerTarget, originTarget, enemyCentreOfMass, enemyCentreOfMass };
        Target[] existingTargets = allTargets.Where(x => x != null).ToArray();
        Target[] physicalTargets = existingTargets.Where(x => x.getType() == Target.Type.Physical).ToArray();
        Target[] tacticalTargets = existingTargets.Where(x => x.getType() == Target.Type.Tactical).ToArray();

        // count the physical yargets that exist
        int physicalTargetsLeft =  physicalTargets != null ? physicalTargets.Length : 0;
        int tacticalTargetsLeft = tacticalTargets != null ? tacticalTargets.Length : 0;

        // deduction: if no physical targets, go to the orgin; if there is one physical target, go to it; if there are two, go to nearest or farthest depending on the objective
        Target closest =  ((physicalTargetsLeft == 0 && tacticalTargetsLeft == 0 ) ? originTarget : (physicalTargetsLeft == 1 ? physicalTargets[0] : ((physicalTargetsLeft == 2) ? (physicalTargets[0].getDistance() > physicalTargets[1].getDistance() ? physicalTargets[1] : physicalTargets[0]): null)));
        Target farthest = ((physicalTargetsLeft == 0 && tacticalTargetsLeft == 0 ) ? originTarget : (physicalTargetsLeft == 1 ? physicalTargets[0] : ((physicalTargetsLeft == 2) ? (physicalTargets[0].getDistance() < physicalTargets[1].getDistance() ? physicalTargets[1] : physicalTargets[0]): null)));

        // if more than 2 physical targets, find min or max distance and find the respective array indices,
        //   then get those items from physical targets category array
        //   LINQ is used to make Lambda queries in LINQ array methods Min and Max
        if (physicalTargetsLeft > 2 && (closest == null && farthest == null))
        {
            Target closestT = tacticalTargets[Array.IndexOf(physicalTargets, physicalTargets.Min(x => x.getDistance()))];
            Target farthestT = physicalTargets[Array.IndexOf(physicalTargets, physicalTargets.Max(x => x.getDistance()))];

            Target closestP = physicalTargets[ Array.IndexOf(physicalTargets, physicalTargets.Min(x => x.getDistance() )) ];
            Target farthestP = physicalTargets[ Array.IndexOf(physicalTargets, physicalTargets.Max(x => x.getDistance() )) ];

            closest = closestP.getDistance() > closestT.getDistance() ? closestT : closestP;
            farthest = farthestP.getDistance() > farthestP.getDistance() ? farthestP : farthestT;
        }

        // targeting strategy is set to farthest as it displays better behaviour, (if the targeting object was destroyed target the centre of mass of own group)
        Target targeting = (farthest != null) ? farthest : enemyCentreOfMass;

        // if the target is far away, set a higher speed multipler based on the log10 of power of 1.3 of the distance;
        float speedMultiplier = (targeting != null) ? (float) Math.Log10(Math.Pow(targeting.getDistance(),1.3f))/2 *speedLimitFactor : 1;

        // set the rotation toward the target
        transform.rotation = targeting.getTurn();
        
        // add a relative force to the rigid body of the enemy spacecraft
        enemyRb.AddRelativeForce(Vector3.forward * stepDistance * speedMultiplier, ForceMode.Impulse);
        
    }

    
}