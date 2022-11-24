using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EarthController : MonoBehaviour
{
    private int impactCounter = 0;
    private GameManager gameManager; // A GameManager variable to link & use the GM GameObject, its script & components (its classes, methods and properties)
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
        
        if (other.CompareTag("EnemyProjectile")) {
            impactCounter += 1;
            if (impactCounter >= 1)
            {
                Destroy(gameObject); // Destroy Earth object
                gameManager.GameOver(); // Call the gameManager variable (which contains the GameObject and Component "Game Manager") and it's method GameOver() to call a game over

            }
            Destroy(other.gameObject);  // Destroy the porjectile gameObject
        }

    }
}

