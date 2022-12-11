using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SelfDestructManager : MonoBehaviour
{

    public static GameObject explosionEffect;
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public static void detonateExplosionHere(Vector3 sourcePosition, Quaternion sourceRotation)
    {
        GameObject exposion = Instantiate(explosionEffect, sourcePosition, sourceRotation);
    }
}
