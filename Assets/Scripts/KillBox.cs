using UnityEngine;

using UnityEngine;

using UnityEngine;

public class KillBox : MonoBehaviour
{
    public GameObject Tombstone_4_White;
    public Vector3 initialTombstonePosition = new Vector3(-18f, 31f, 10f);
    public float TombstoneOffsetZ = 1f;
    public string targetTag = "Player";
    public GameObject StartPosition;
    public GameObject checkpoint1;
    public GameObject checkpoint2;
    public GameObject checkpoint3;

    private Vector3 teleportDestination;

    private void Start()
    {
        // Set the initial teleport destination
        teleportDestination = StartPosition.transform.position;

        // Activate the initial checkpoint
        if (StartPosition != null)
        {
            StartPosition.SetActive(true);
        }
    }

    private void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag(targetTag))
        {
            if (other.gameObject.CompareTag("Checkpoint"))
            {
                // Update teleport destination and deactivate checkpoint
                SetCheckpoint(other.gameObject);
            }
            else
            {
                // Teleport player to the last checkpoint or starting position
                GameObject.FindGameObjectWithTag("Player").transform.position = teleportDestination;
            }
        }
    }

    private void SetCheckpoint(GameObject checkpoint)
    {
        // Update teleport destination to checkpoint position
        teleportDestination = checkpoint.transform.position;

        // Deactivate checkpoint
        checkpoint.SetActive(false);

        // Instantiate tombstone
        Vector3 spawnPosition = initialTombstonePosition - Vector3.forward * TombstoneOffsetZ * GameObject.FindGameObjectsWithTag("Tombstone").Length;
        Instantiate(Tombstone_4_White, spawnPosition, Quaternion.identity);
    }
}

