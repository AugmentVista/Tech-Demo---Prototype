using UnityEngine;

public class KillBox : MonoBehaviour
{
    private string targetTag = "Player";

    private CheckPoints checkPoint;

    public Vector3 teleportDestination;

    public GameObject Tombstone_4_White;
    public Vector3 initialTombstonePosition = new Vector3(-18f, 31f, 10f);
    public float TombstoneOffsetZ = 1f;

    private void Start()
    {
        checkPoint = FindObjectOfType<CheckPoints>();
    }
    private void Tombstone()
    {
        Vector3 spawnPosition = initialTombstonePosition - Vector3.forward * TombstoneOffsetZ * GameObject.FindGameObjectsWithTag("Tombstone").Length;
        Instantiate(Tombstone_4_White, spawnPosition, Quaternion.identity);
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
            teleportDestination = new Vector3(-16.5f, 31f, 10f);
            }
            other.transform.position = teleportDestination;
            Tombstone();
        }
    }
}
