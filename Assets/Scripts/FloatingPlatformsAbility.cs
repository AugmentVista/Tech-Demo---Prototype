using UnityEngine;

using UnityEngine;

public class FloatingPlatformsAbility : MonoBehaviour
{

    public GameObject RockPrefab;
    private Transform playerTransform;
    public float RockSpawnRange;
    public int RockCount;

    private void Awake()
    {
        playerTransform = transform;
    }

    private void Update()
    {
        if (RockCount > 0 && Input.GetKeyDown(KeyCode.E))
        {
            UseRockAbility();
            RockCount--;
        }
    }

    private void UseRockAbility()
    {
        Vector3 spawnDirection = playerTransform.forward;
        Vector3 spawnPosition = playerTransform.position + spawnDirection * RockSpawnRange + Vector3.up * -1f;

        RaycastHit hit;
        if (Physics.Raycast(playerTransform.position, spawnDirection, out hit, RockSpawnRange))
        {
            spawnPosition = hit.point + Vector3.up * 0.1f;
        }

        GameObject rockInstance = Instantiate(RockPrefab, spawnPosition, Quaternion.identity);

        // Add a script to handle the rock's behavior when the player collides with it
        rockInstance.AddComponent<RockCollisionHandler>();
        Debug.Log("Rock script added");

        Rigidbody rockRigidbody = rockInstance.GetComponent<Rigidbody>();
        rockRigidbody.isKinematic = true;
        Debug.Log("Kinematic has been set");
    }
}
