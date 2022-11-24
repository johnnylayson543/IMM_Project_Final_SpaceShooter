using System.Collections;
using System.Collections.Generic;
using UnityEngine;

// This script allows detection of projectile and enemy collisions

public class DetectCollision : MonoBehaviour
{
    private string owner = "Player";
    private string target = "Enemy";

    // Start is called before the first frame update
    void Start()
    {
        owner = tag.Substring(0, tag.Length - "Projectile".Length);

    }

    // Update is called once per frame
    void Update()
    {
        
    }

    // Projectile and Enemy Collision Detection
    private void OnTriggerEnter(Collider other)
    {
        
        if (owner == "Player") { target = "Enemy"; }
        else if (owner == "Enemy") { target = "Player"; }

        // If the colliding projectile hits an enemy that shares a tag called "Enemy"
        if (other.CompareTag(target))
        {
            Destroy(gameObject); // Destroy the porjectile gameObject
            Destroy(other.gameObject); // Destroy the other gameObject (the Enemy)
        }
    }
}
