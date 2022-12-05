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
        float speed = 0.0f;
        float stepDistance = speed * Time.deltaTime;

        
        Target[] Targets = loadTargets(gameObject, stepDistance);
        Target[] enemySpacecraft = Targets.Where(x => x.getTargetObj().tag == "Enemy").ToArray();


    }

    private static Target[] loadTargets(GameObject source, float stepDistance)
    {
        sceneObjects = FindObjectsOfType<GameObject>();
        Target[] y = new Target[sceneObjects.Length];

        for (int i = 0; i < sceneObjects.Length; i++)
        {
            y[i] = (new Target(source, sceneObjects[i], stepDistance));
        }
        return y;
    }

    public static Target swarmFormationTargetPerSwarmMember (GameObject source, float stepDistance)
    {
        Target[] y = loadTargets(source, stepDistance);
        Target[] owngroup = y.Where(x => x.getTargetObj().tag == source.tag).ToArray();
        Target z = new Target(source, owngroup, stepDistance, true);

        return z;
    }
}
