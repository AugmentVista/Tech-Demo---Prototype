using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CheckPoints : MonoBehaviour // This one goes on the Player
{
    public GameObject StartPosition;
    public GameObject checkpoint1;
    public GameObject checkpoint2;
    public GameObject checkpoint3;

    private KillBox killBox;
    [HideInInspector]
    public Vector3 checkpoint;
    private Vector3 tempCheckPoint;

    private void Start()
    {
        killBox = GetComponent<KillBox>();
      checkpoint = StartPosition.transform.position;
    }

    private void OnTriggerEnter(Collider other)
    {
        Debug.Log(checkpoint);
        if (other.gameObject.CompareTag("Checkpoint"))
        {
            tempCheckPoint = other.gameObject.transform.position;
            Debug.Log("temp checkpoint is " + tempCheckPoint);
            checkpoint = tempCheckPoint;
            other.gameObject.SetActive(false);
            Debug.Log("Checkpoint is" + checkpoint );
        }
    }
}
