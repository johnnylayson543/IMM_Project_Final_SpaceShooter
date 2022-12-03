using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TargetOverwatch : MonoBehaviour
{
    GameObject[] sceneObjects;
    // Start is called before the first frame update
    void Start()
    {
        sceneObjects = FindObjectsOfType<GameObject>();
    }

    // Update is called once per frame
    void Update()
    {
        float speed = 0;
        float stepDistance = speed * Time.deltaTime;
        

        Target[] Targets = loadTargets(gameObject, stepDistance);
    }

    Target[] loadTargets(GameObject source, float stepDistance)
    {
        Target[] y = new Target[sceneObjects.Length];

        for (int i = 0; i < sceneObjects.Length; i++)
        {
            y[i] = (new Target(source, sceneObjects[i], stepDistance));
        }
        return y;
    }
}
