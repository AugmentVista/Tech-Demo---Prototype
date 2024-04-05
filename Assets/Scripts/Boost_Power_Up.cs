using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BoostPowerUp : MonoBehaviour
{
    public float speedBoostAmount; 
    public float jumpBoostAmount; 
    public float duration; 
    public float rotationSpeed;

    private void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("Player"))
        {
            CollectItem(other.GetComponent<FirstPersonController>());
            gameObject.SetActive(false); 
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
        playerController.ApplySpeedBoost(speedBoostAmount);

        playerController.ApplyJumpBoost(jumpBoostAmount);

        Invoke(nameof(RemoveBoosts), duration);
    }
    private void RemoveBoosts()
    {
        var playerController = FindObjectOfType<FirstPersonController>();
        if (playerController != null)
        {
            playerController.RemoveSpeedBoost(speedBoostAmount);
            playerController.RemoveJumpBoost(jumpBoostAmount);
        }
    }
}
