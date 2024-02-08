using System.Collections;
using System.Collections.Generic;
using UnityEngine;

using UnityEngine;

public class DoorOpenClose : MonoBehaviour
{
    public GameObject door;

    public float rotationSpeed = 90f;

    public Vector3 openRotation = new Vector3(0f, 25f, 0f);

    public Vector3 closedRotation = new Vector3(0f, 140f, 0f);

    private bool isOpen = false;

    private void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("Player"))
        {
            Debug.Log("Door is seeing player");
            isOpen = !isOpen;
            RotateDoor();
        }
    }

    private void RotateDoor()
    {
        Debug.Log("Door is opening");
        Vector3 currentRotation = door.transform.localEulerAngles;
        Vector3 targetRotation;
        if (!isOpen)
        {
            targetRotation = openRotation;
        }
        else
        {
            targetRotation = closedRotation;
        }
        Debug.Log("Target Rotation: " + targetRotation);
        Quaternion targetQuaternion = Quaternion.Euler(targetRotation);
        door.transform.localRotation = Quaternion.RotateTowards(door.transform.localRotation, targetQuaternion, rotationSpeed * Time.deltaTime);
        Debug.Log("Door Rotation: " + door.transform.localRotation.eulerAngles);
    }
}
