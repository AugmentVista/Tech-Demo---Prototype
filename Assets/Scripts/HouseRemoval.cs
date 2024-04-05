using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class HouseRemoval : MonoBehaviour
{
    public GameObject House;
    private void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("Player"))
        {
            House.SetActive(false);
        }
    }
}
