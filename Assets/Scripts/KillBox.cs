using UnityEngine;

public class KillBox : MonoBehaviour
{

    public string targetTag = "Player";


    public Vector3 teleportDestination = new Vector3(-16.5f, 31f, 10f);

    public GameObject Tombstone_4_White;

    public Vector3 initialTombstonePosition = new Vector3(-18f, 31f, 10f);

    public float TombstoneOffsetZ = 1f;
    private void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag(targetTag))
        {
            other.transform.position = teleportDestination;

            Vector3 spawnPosition = initialTombstonePosition - Vector3.forward * TombstoneOffsetZ * GameObject.FindGameObjectsWithTag("Tombstone").Length;
            Instantiate(Tombstone_4_White, spawnPosition, Quaternion.identity);
        }
    }
}