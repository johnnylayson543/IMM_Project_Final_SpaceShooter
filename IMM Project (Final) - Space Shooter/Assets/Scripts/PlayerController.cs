using System.Collections;
using System.Collections.Generic;
using UnityEngine;

// This script is for the Player Controls and Movements
public class PlayerController : MonoBehaviour
{
    // Player Movement Variables and Properties
    private Rigidbody playerRb; // The Rigidbody variable of the Player
    private float speed = 100.0f; // The speed of the Player
    private float turnSpeed = 80.0f; // The turning speed of the player
    private float horizontalInput;  // Horizontal Input Movements
    private float forwardInput;  // Vertical/Forward Input Movements
    
    // Player's Map Boundary Properties
    private float xRange = 105; // The Map Boundary range of the Player in the map (x-axis)
    private float zRange = 70; // The Map Boundary range of the Player in the map (z-axis)

    // Start is called before the first frame update
    void Start()
    {
        playerRb = GetComponent<Rigidbody>();  // Get the RigidBody component and apply it to the RigidBody playerRb variable
    }

    // Update is called once per frame
    void Update()
    {

        // Player Input (Get the Horizontal and Vertical Inputs)
        horizontalInput = Input.GetAxis("Horizontal"); 
        forwardInput = Input.GetAxis("Vertical");

        // Movement and Force of the Player
        playerRb.AddRelativeForce(Vector3.forward * Time.deltaTime * speed * forwardInput, ForceMode.Impulse); // Add a relative movement force for the Player
        transform.Rotate(Vector3.up, Time.deltaTime * turnSpeed * horizontalInput); // Add a rotation for the Player

        setBoundaries(); // Set the Player Boundary
    }

    // Method to set the Player Boundary
    public void setBoundaries()
    {
        // Boundaries
        if (transform.position.x < -xRange)  // If player x position is less than -xRange (left)
        {
            transform.position = new Vector3(-xRange, transform.position.y, transform.position.z); // Transform, stay and keep the player position in the left boundary
        }

        if (transform.position.x > xRange)  // If player x position is greater than xRange (right)
        {
            transform.position = new Vector3(xRange, transform.position.y, transform.position.z); // Transform, stay and keep the player position in the right boundary
        }

        if (transform.position.z < -zRange)  // If player z position is less than -zRange (bottom)
        {
            transform.position = new Vector3(transform.position.x, transform.position.y, -zRange); // Transform, stay and keep the player position in the bottom boundary
        }

        if (transform.position.z > zRange)  // If player z position is greater than zRange (top)
        {
            transform.position = new Vector3(transform.position.x, transform.position.y, zRange); // Transform, stay and keep the player position in the top boundary
        }
    }
}
