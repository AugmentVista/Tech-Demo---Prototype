using UnityEngine;

public class CollectibleItem : MonoBehaviour
{
    // Reference to FloatingPlatformsAbility script
    public FloatingPlatformsAbility floatingPlatformsAbility;

    private void OnTriggerEnter(Collider other)
    {
        
        if (other.CompareTag("Player"))
        {
            CollectItem();
        }
    }

    private void CollectItem()
    {
        floatingPlatformsAbility.RockCount++;
        gameObject.SetActive(false);
    }
}