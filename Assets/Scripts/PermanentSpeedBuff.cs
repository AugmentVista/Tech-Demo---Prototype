using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PermanentSpeedBuff : MonoBehaviour
{
    public float speedBoostAmount;

    private void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("Player"))
        {
            CollectItem(other.GetComponent<FirstPersonController>());
        }
    }
    private void CollectItem(FirstPersonController playerController)
    {
        playerController.ApplySpeedBoost(speedBoostAmount);;
    }
}
