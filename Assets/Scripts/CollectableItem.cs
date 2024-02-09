using UnityEngine;
public class CollectibleItem : MonoBehaviour
{
    // Reference to FloatingPlatformsAbility script
    public FloatingPlatformsAbility floatingPlatformsAbility;
    public float rotationSpeed = 30f; // Speed of rotation in degrees per second
    private void OnTriggerEnter(Collider other)
    {
        
        if (other.CompareTag("Player"))
        {
            CollectItem();
        }
    }
    private void Update()
    {
        transform.Rotate(Vector3.up * rotationSpeed * Time.deltaTime);
        transform.Rotate(Vector3.right * rotationSpeed * Time.deltaTime);
        transform.Rotate(Vector3.forward * rotationSpeed * Time.deltaTime);
    }
    private void CollectItem()
    {
        floatingPlatformsAbility.RockCount++;
        gameObject.SetActive(false);
    }
}