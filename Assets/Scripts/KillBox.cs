using UnityEngine;

public class KillBox : MonoBehaviour
{
    private string targetTag = "Player";
    public CheckPoints checkPoint;
    private FloatingPlatformsAbility Ability;
    private UIManager UI;
    public Vector3 teleportDestination;

    public GameObject Tombstone_4_White;
    public Vector3 initialTombstonePosition;
    public float TombstoneOffsetZ = 1f;
    public int NumberOfDeaths = 0;

    private void Start()
    {
        checkPoint = FindObjectOfType<CheckPoints>();
        Ability = FindAnyObjectByType<FloatingPlatformsAbility>();
        UI = FindObjectOfType<UIManager>();
    }
    private void Tombstone()
    {
        NumberOfDeaths++;
        initialTombstonePosition = checkPoint.checkpoint;
        Vector3 spawnPosition = initialTombstonePosition - Vector3.forward * TombstoneOffsetZ * GameObject.FindGameObjectsWithTag("Tombstone").Length;
        Instantiate(Tombstone_4_White, spawnPosition, Quaternion.identity);
    }
    public void ResetAbilityUses() 
    {
        Ability.RockCount = 10;
        UI.UpdateRockUI();
    }
    private void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag(targetTag))
        {
            if (checkPoint != null && checkPoint.checkpoint != Vector3.zero)
            {
                Debug.Log("Checkpoint has been set");
                teleportDestination = checkPoint.checkpoint;
            }
            else
            {
                teleportDestination = checkPoint.checkpoint;
            }
            other.transform.position = teleportDestination;
            if (NumberOfDeaths >= 1)
            { 
            ResetAbilityUses();
            }
            Tombstone();
        }
    }
}
