using System.Collections;
using System.Collections.Generic;
using UnityEngine;

// This code manages enemy spawn on the game
public class SpawnManager : MonoBehaviour
{
    public List<GameObject> enemies;  // Create a list of enemy gameObjects

    // Enemy Spawn Properties
    public float spawnWaitSeconds = 10.0f; // The waiting time in seconds between enemy spawns in the game
    public float[] rangeDistanceFromEarth = new float[2] { 500.0f, 1000.0f }; // range of distances from the Earth that the enemy can spawn from
    public float[] rangeAngleOfApproachAroundEarth = new float[2] { 0, 2 * Mathf.PI }; // range of angles of approach around the Earth that the enemy spawns ... or compass direction of enemy spawn locations

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
            // Create thread to time the interval between enemy spawns in seconds
            yield return new WaitForSeconds(spawnWaitSeconds);

            // Spawn a random type of enemy using random range and the enemy count in the list. Apply the random enemy on the index variable
            int index = Random.Range(0, enemies.Count);

            // Instantiate or spawn random enemies with a randomly generated spawn position
            Instantiate(enemies[index], GenerateSpawnPosition(), enemies[index].transform.rotation);

        }
    }

    // A private method to Generate Spawn Position
    private Vector3 GenerateSpawnPosition()
    {

        // Generate a random distance and angle from a range of values (proximity to Earth and arc around the axis of the Earth)   
        float randomRadius = Random.Range(rangeDistanceFromEarth[0], rangeDistanceFromEarth[1]);
        float randomAzimuth = Random.Range(rangeAngleOfApproachAroundEarth[0], rangeAngleOfApproachAroundEarth[1]);

        // Local method variables to set a random range of x or z positions the enemy will spawn in the game
        float spawnPosX = randomRadius * Mathf.Cos(randomAzimuth);  
        float spawnPosZ = randomRadius * Mathf.Sin(randomAzimuth);  
                               
        Vector3 randomPos = new Vector3(spawnPosX, 0, spawnPosZ);  // Add the random range of x and z positions on a new Vector3 to create a new random position
        return randomPos; // Return the value of the new randomPos
    }
}
