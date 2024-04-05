using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyBeam : MonoBehaviour
{
    public float slowedSpeed;
    private FirstPersonController playerController; 

    private void Start()
    {
        playerController = FindObjectOfType<FirstPersonController>();
        if (playerController == null)
        {
            Debug.LogError("PlayerController not found!");
        }
    }

    private void OnTriggerEnter(Collider other)
    {
        // Check if playerController is assigned
        if (playerController == null)
        {
            return; // Exit early if playerController is not assigned
        }
        if (other.CompareTag("Player"))
        {
            Slow();
            Debug.Log("Beans");
        }
    }
    private void Slow()
    {
        playerController.sprintSpeed -= slowedSpeed;
        playerController.walkSpeed -= slowedSpeed;
        if (playerController.sprintSpeed <= 2)
        { 
        playerController.sprintSpeed = 2;
        }
        if (playerController.walkSpeed <= 2)
        {
            playerController.walkSpeed = 2;
        }
    }
    private void ResetSpeed()
    {
        playerController.sprintSpeed += slowedSpeed;
        playerController.walkSpeed += slowedSpeed;
        if (playerController.sprintSpeed <= 2)
        {
            playerController.sprintSpeed = 2;
        }
        if (playerController.walkSpeed <= 2)
        {
            playerController.walkSpeed = 2;
        }
    }

    private void OnTriggerExit(Collider other)
    {
        // Check if playerController is assigned
        if (playerController == null)
        {
            return; // Exit early if playerController is not assigned
        }
        if (other.CompareTag("Player"))
        {
           ResetSpeed();
        }
    }
}