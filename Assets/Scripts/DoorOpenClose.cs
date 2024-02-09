using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DoorOpenClose : MonoBehaviour
{
    public GameObject door;

    public Vector3 closedRotation = new Vector3(0f, 310f, 0f);
    public float openRotation;

    private bool isOpen = false;
    private bool isOpening = false;

    private void Start()
    {
        door.transform.eulerAngles = closedRotation;
    }

    private void OnTriggerEnter(Collider other)
    {
        if (isOpen || isOpening) return;

        if (other.CompareTag("Player"))
        {
            Debug.Log("Door is seeing player");
            isOpening = true;
        }
    }

    private void RotateDoor()
    {
        Debug.Log("Rotate Door is being called");
        door.transform.RotateAround(door.transform.position, door.transform.up, -50 * Time.deltaTime);
        if (door.transform.eulerAngles.y <= openRotation)
        {
            isOpening = false;
            Debug.Log("Door is opening");
            isOpen = true;
        }
    }

    private void Update()
    {
        if (isOpening)
        {
            RotateDoor();
        }
    }
}