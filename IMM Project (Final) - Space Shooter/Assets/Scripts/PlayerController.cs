using System.Collections;
using System.Collections.Generic;
using UnityEngine;

// This script is for the Player Controls and Movements
public class PlayerController : MonoBehaviour
{
    // Player Movement Variables and Properties
    private Rigidbody playerRb; // The Rigidbody variable of the Player
    private float speed = 80.0f; // The speed of the Player
    private float turnSpeed = 50.0f; // The turning speed of the player
    private float horizontalInput;  // Horizontal Input Movements
    private float forwardInput;  // Vertical/Forward Input Movements

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
    }
}
