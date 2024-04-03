using System;
using UnityEngine;

public class RockCollisionHandler : MonoBehaviour
{
    private bool hasCollided = false;
    private float interactionDelay = 3f; 
    private float elapsedTime = 0f; 

    private void Update()
    {
        if (hasCollided)
        {
            elapsedTime += Time.deltaTime;

            if (elapsedTime >= interactionDelay)
            {
                Rigidbody rockRigidbody = GetComponent<Rigidbody>();
                rockRigidbody.isKinematic = false;
                rockRigidbody.useGravity = true;
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

