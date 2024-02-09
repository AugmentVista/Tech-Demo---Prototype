using System;
using UnityEngine;

public class RockCollisionHandler : MonoBehaviour
{
    private bool hasCollided = false;
    private float interactionDelay = 3f; // Delay before interaction is allowed
    private float elapsedTime = 0f; // Time elapsed since the script started

    private void Update()
    {
        // Update the elapsed time if collision has occurred
        if (hasCollided)
        {
            //Debug.Log("Has collided");
            elapsedTime += Time.deltaTime;

            // Check if the elapsed time has reached the interaction delay
            if (elapsedTime >= interactionDelay)
            {
                // Enable gravity after the delay
                Rigidbody rockRigidbody = GetComponent<Rigidbody>();
                rockRigidbody.isKinematic = false;
                rockRigidbody.useGravity = true;
                //Debug.Log("Rock is using gravity");
                Destroy(gameObject, 10f);
            }
        }
        else { Destroy(gameObject, 15f); }
    }

    private void OnCollisionEnter(Collision collision)
    {
        // Check if the collision is with the player
        if (collision.gameObject.CompareTag("Player"))
        {
            hasCollided = true;
        }
    }
}

