using System.Collections;
using System.Collections.Generic;
using UnityEngine;

// This script allows detection of projectile and enemy collisions

public class DetectCollision : MonoBehaviour
{
    private string owner = "Player";
    private int killScoreValue = 10; // The score value that player gets per enemy kill
    private GameManager gameManager; // A GameManager variable to link & use the GM GameObject, its script & components (its classes, methods and properties)

    // Start is called before the first frame update
    void Start()
    {
        gameManager = GameObject.Find("Game Manager").GetComponent<GameManager>(); // Find the "Game Manager" gameObject/GameObject and apply the GameManager Script & Component to the gameManager variable (via the "Game Manager" gameObject/GameObject)
        owner = tag.Substring(0, tag.Length - "Projectile".Length);
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    // Projectile and Enemy Collision Detection
    private void OnTriggerEnter(Collider other)
    {
        // is it a target of thw projectile's owner 
        bool targets = ( owner == "Enemy") ? (other.tag == "Earth" || other.tag == "Player") : 
                                             ( ( owner == "Player" ) ? other.tag == "Enemy" : false ) ;

        bool isGameOver = (other.tag == "Player" || (EarthController.getImpactCounter() >= EarthController.getImpactLimit() && other.tag == "Earth"));
        bool isDestroyed = isGameOver || (other.tag == "Enemy");

        // If the colliding projectile hits an enemy that shares a tag called "Enemy"
        if (targets)
        {
            //SelfDestructManager.detonateExplosionHere(gameObject.transform.position, gameObject.transform.rotation);
            Destroy(gameObject); // Destroy the projectile gameObject
            if (isDestroyed) {
                Destroy(other.gameObject);
                gameManager.UpdateScore(killScoreValue); // Call the UpdateScore method in the gameManager to update the score by 10 for each kill
            }
            
            if (isGameOver) gameManager.GameOver(); // Call the gameManager variable (which contains the GameObject and Component "Game Manager") and it's method GameOver() to call a game over
            
        }
    }
}
