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
        owner = tag.Substring(0, tag.Length - "Projectile".Length); // which side fired the projectile
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    // Projectile and Enemy Collision Detection
    private void OnTriggerEnter(Collider other)
    {

        ParticleSystem particleSystem2 = other.GetComponent<ParticleSystem>();
        // is it a target of thw projectile's owner 
        // checks who the owner of projectile is, and checks if 'other' gameObject is one of its targets
        // and returns true or false
        bool targets = ( owner == "Enemy") ? (other.tag == "Earth" || other.tag == "Player") : 
                                             ( ( owner == "Player" ) ? other.tag == "Enemy" : false ) ;

        // checks when either the player or the Earth are set for destruction and returns true or false
        // and then what game over happened
        bool isGameOver = (other.tag == "Player" || (EarthController.getImpactCounter() >= EarthController.getImpactLimit() && other.tag == "Earth"));
        
        // checks whether other game object should be destroyed, if either isGameOver or other gameObject
        // is an enemy
        bool isDestroyed = isGameOver || (other.tag == "Enemy");

        // If the colliding projectile hits an enemy that shares a tag called "Enemy"
        if (targets)
        { 
            //SelfDestructManager.detonateExplosionHere(gameObject.transform.position, gameObject.transform.rotation);
            Destroy(gameObject); // Destroy the projectile gameObject

            if (isDestroyed) {
                other.GetComponent<ParticleSystem>().Play(); // play particle system explosion effect 
                other.GetComponent<AudioSource>().Play();
                Destroy(other.gameObject, 1); // destroy object but wait 2 seconds for particle effect to finish
                gameManager.UpdateScore(killScoreValue); // Call the UpdateScore method in the gameManager to update the score by 10 for each kill
            }
            
            if (isGameOver) gameManager.GameOver(); // Call the gameManager variable (which contains the GameObject and Component "Game Manager") and it's method GameOver() to call a game over
            
        }
    }
}
