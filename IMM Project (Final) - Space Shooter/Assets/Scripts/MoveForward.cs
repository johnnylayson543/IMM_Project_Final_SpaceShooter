using System.Collections;
using System.Collections.Generic;
using UnityEngine;

// This script allows GameObjects to move forward (e.g. Projectiles)
public class MoveForward : MonoBehaviour
{
    private float moveSpeed = 50.0f; // The move speed of a GameObjects
    private Rigidbody projectileRb;
    Vector3 initialDirection;

    // Start is called before the first frame update
    void Start()
    {
        projectileRb = GetComponent<Rigidbody>();

    }

    // Update is called once per frame
    void Update()
    {
        transform.Translate(Vector3.forward * Time.deltaTime * moveSpeed * 1); // Move the projectile forward with speed

    }

}
