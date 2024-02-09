using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BoostPowerUp : MonoBehaviour
{
    public float speedBoostAmount = 2f; // Amount to increase speed
    public float jumpBoostAmount = 5f; // Amount to increase jump height
    public float duration = 30f; // Duration of the effect in seconds
    public float rotationSpeed = 30f; // Speed of rotation in degrees per second

    private void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("Player"))
        {
            CollectItem(other.GetComponent<FirstPersonController>());
            gameObject.SetActive(false); // Deactivate the pickup object
        }
    }

    private void Update()
    {
        transform.Rotate(Vector3.up * rotationSpeed * Time.deltaTime);
        transform.Rotate(Vector3.right * rotationSpeed * Time.deltaTime);
        transform.Rotate(Vector3.forward * rotationSpeed * Time.deltaTime);
    }

    private void CollectItem(FirstPersonController playerController)
    {
        // Apply speed boost
        playerController.ApplySpeedBoost(speedBoostAmount);

        // Apply jump height boost
        playerController.ApplyJumpBoost(jumpBoostAmount);

        // Start a timer to remove the boosts after the duration
        Invoke(nameof(RemoveBoosts), duration);
    }

    private void RemoveBoosts()
    {
        // Remove the boosts
        var playerController = FindObjectOfType<FirstPersonController>();
        if (playerController != null)
        {
            playerController.RemoveSpeedBoost(speedBoostAmount);
            playerController.RemoveJumpBoost(jumpBoostAmount);
        }
    }
}
