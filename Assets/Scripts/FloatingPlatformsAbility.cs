using TMPro;
using UnityEngine;
public class FloatingPlatformsAbility : MonoBehaviour
{
    [SerializeField]
    private UIManager UImanager;

    private Transform playerTransform;
    public GameObject RockPrefab;
    public TextMeshProUGUI RockCountText;

    public float RockSpawnRange;
    public int RockCount = 10;

    private void Awake()
    {
        playerTransform = transform;
        UImanager.UpdateRockUI();
    }

    private void Update()
    {
        if (RockCount > 0 && Input.GetKeyDown(KeyCode.E))
        {
            UseRockAbility();
            RockCount--;
            UImanager.UpdateRockUI();
        }
    }

    private void UseRockAbility()
    {
        Vector3 PlayerForwardSight = playerTransform.forward;
        Vector3 spawnPosition = playerTransform.position + PlayerForwardSight * RockSpawnRange + Vector3.up * -1f;
        RaycastHit hit;
        if (Physics.Raycast(playerTransform.position, PlayerForwardSight, out hit, RockSpawnRange))
        {
            spawnPosition = hit.point + Vector3.up * 0.1f;
        }
        GameObject rockInstance = Instantiate(RockPrefab, spawnPosition, Quaternion.identity);
        rockInstance.AddComponent<RockCollisionHandler>();
        Rigidbody rockRigidbody = rockInstance.GetComponent<Rigidbody>();
        rockRigidbody.isKinematic = true;
    }
}
