using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EarthController : MonoBehaviour
{
    private static int impactCounter = 0; // how many times Earth was impacted by a projectile
    private static int impactLimit = 100; // the maximum impacts the Earth can endure
    private GameManager gameManager; // A GameManager variable to link & use the GM GameObject, its script & components (its classes, methods and properties)

    // Earth Explosion Particle Properties
    public ParticleSystem earthExplosionParticle;

    // Start is called before the first frame update
    void Start()
    {
        gameManager = GameObject.Find("Game Manager").GetComponent<GameManager>(); // Find the "Game Manager" gameObject/GameObject and apply the GameManager Script & Component to the gameManager variable (via the "Game Manager" gameObject/GameObject)
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    // Projectile and Enemy Collision Detection
    private void OnTriggerEnter(Collider other)
    {
        // if an enemy prokectile hits Earth 
        if (other.CompareTag("EnemyProjectile")) {
            impactCounter += 1;  // add an impact to the counter
            int livesLeft = impactLimit - impactCounter; // get the remaining survivable impacts
            gameManager.UpdateEarthLivesCounter(livesLeft); // update the Earth Lives Health counter
        }

    }

    // get the impact counter results
    public static int getImpactCounter()
    {
        return impactCounter;
    }

    // get the impact limit of Earth
    public static int getImpactLimit()
    {
        return impactLimit;
    }
}

