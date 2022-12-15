using System.Collections;
using System.Collections.Generic;
using UnityEngine;

// This code manages enemy spawn on the game
public class SpawnManager : MonoBehaviour
{
    public List<GameObject> enemies;  // Create a list of enemy gameObjects

    // Enemy Spawn Properties
    public float spawnRate = 10.0f; // The rate enemy spawns in the game
    public float spawnRange = 1000.0f; // The range the enemy spawns in the game
    public float minRangeFactor = 0.5f; // how close inside range should they spawn as a factor.

    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    // Enumerator which spawns an enemy
    public IEnumerator SpawnEnemy()
    {
        // While gameplay is remains true, instantiate a new, random enemy and spawn it on a randomly generated position
        // How fast the spawn is depends on the enemies' spawn rate
        while (true)
        {
            // Create thread to increase or decrease the enemy spawn rate
            yield return new WaitForSeconds(spawnRate);

            // Spawn a random type of enemy using random range and the enemy count in the list. Apply the random enemy on the index variable
            int index = Random.Range(0, enemies.Count);

            // Instantiate or spawn random enemies with a randomly generated spawn position
            Instantiate(enemies[index], GenerateSpawnPosition(), enemies[index].transform.rotation);

            int randomExtra = Random.Range(1, 2);
            if (randomExtra == 2) Instantiate(enemies[index], GenerateSpawnPosition(), enemies[index].transform.rotation);

        }
    }

    // A private method to Generate Spawn Position
    private Vector3 GenerateSpawnPosition()
    {

        // 
        // Local method variables to set a random range of x or z positions the enemy will spawn in the game
        float spawnPosX = Random.Range(minRangeFactor * spawnRange, spawnRange) * Mathf.Cos(Random.Range(0, 2 * Mathf.PI));  
        float spawnPosZ = Random.Range(minRangeFactor * spawnRange, spawnRange) * Mathf.Sin(Random.Range(0, 2 * Mathf.PI));  
                               
        Vector3 randomPos = new Vector3(spawnPosX, 0, spawnPosZ);  // Add the random range of x and z positions on a new Vector3 to create a new random position
        return randomPos; // Return the value of the new randomPos
    }
}
