using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;

public class TargetOverwatch : MonoBehaviour
{
    private static GameObject[] sceneObjects;
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        float speed = 0.0f;  // the target overwatch empty is stationary
        float stepDistance = speed * Time.deltaTime; // distance travel by the empty is zero

        
        Target[] Targets = loadTargets(gameObject, stepDistance); // loads all scene game objects as targets from the empty object's perspective at the origin of the scene
        Target[] enemySpacecraft = Targets.Where(x => x.getTargetObj().tag == "Enemy").ToArray();  // enemy spacecraft targets are assigned to a variable


    }

    // loades all game objects in the scene relative to give source game object and distance travelled per delta time
    private static Target[] loadTargets(GameObject source, float stepDistance)
    {
        sceneObjects = FindObjectsOfType<GameObject>();  // gets all scene gane objects and assigns to a variable
        Target[] y = new Target[sceneObjects.Length]; // creates an array of Targets the length of game objects in the scene

        // for each game object, a new target object is added to the array
        for (int i = 0; i < sceneObjects.Length; i++)
        {
            y[i] = (new Target(source, sceneObjects[i], stepDistance));
        }

        // returns the list of targets as an array of target objects
        return y;
    }

    // finds the target which represents the centre of mass of the enemies
    public static Target swarmFormationTargetPerSwarmMember (GameObject source, float stepDistance)
    {
        Target[] y = loadTargets(source, stepDistance);  // load all geme objects relative to an emeny
        Target[] owngroup = y.Where(x => x.getTargetObj().tag == source.tag).ToArray(); // finds the game objects that are part of the enemy's own group
        Target z = new Target(source, owngroup, stepDistance, true); // feeds the list of fellow enemy targets into the Target construct to get the swarm centre of mass as a target

        return z; // returns the representative centre of mass target as a Target object
    }
}
