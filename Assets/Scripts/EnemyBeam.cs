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
    private IEnumerator StartSlow()
    {
        yield return new WaitForSeconds(0.3f);
        playerController.rb.velocity = Vector3.zero;
    }

    private void OnTriggerEnter(Collider other)
    {
        if (playerController == null)
        {
            return;
        }
        if (other.CompareTag("Player"))
        {
            Slow();
            Debug.Log("Beans");
        }
    }
    

    private void Slow()
    {
        StartCoroutine(StartSlow());
        playerController.rb.useGravity = false;
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
        playerController.rb.useGravity = true;
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
        if (playerController == null)
        {
            return;
        }
        if (other.CompareTag("Player"))
        {
           ResetSpeed();
        }
    }
}