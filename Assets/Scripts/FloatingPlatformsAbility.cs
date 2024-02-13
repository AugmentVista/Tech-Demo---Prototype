using TMPro;
using UnityEngine;
public class FloatingPlatformsAbility : MonoBehaviour
{

    public GameObject RockPrefab;
    private Transform playerTransform;
    public float RockSpawnRange;
    public int RockCount = 10;
    public TextMeshProUGUI RockCountText;

    private void Awake()
    {
        playerTransform = transform;
        UpdateRockUI();
    }

    private void Update()
    {
        if (RockCount > 0 && Input.GetKeyDown(KeyCode.E))
        {
            UseRockAbility();
            RockCount--;
            UpdateRockUI();
        }
    }
    public void UpdateRockCount(int count)
    {
        RockCountText.text = "Rock-Platforms\n" + count.ToString();
        Debug.Log(RockCountText.text);
    }

    public void UpdateRockUI()
    {
        UpdateRockCount(RockCount);
        Debug.Log(RockCount);
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

        rockInstance.AddComponent<RockCollisionHandler>();
        //Debug.Log("Rock script added");

        Rigidbody rockRigidbody = rockInstance.GetComponent<Rigidbody>();
        rockRigidbody.isKinematic = true;
        //Debug.Log("Kinematic has been set");
    }
}
