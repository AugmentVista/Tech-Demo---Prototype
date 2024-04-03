using System.Collections;
using System.Collections.Generic;
using System.Net;
using UnityEngine;
public class MovingPlatform : MonoBehaviour
{
    public GameObject ShipStartingPoint;
    public GameObject ShipTurnAroundPoint;
    private Vector3 endPoint;
    private Vector3 startPoint;
    public float movementSpeed;
    public bool EndPointReached;
    public GameObject Ship;
    private Transform player;
    private Vector3 targetPoint;

    private void Start()
    {
        endPoint = ShipTurnAroundPoint.transform.position;
        startPoint = ShipStartingPoint.transform.position;
        targetPoint = endPoint;
        EndPointReached = false;
    }
    private void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("Player"))
        {
            player = other.transform;
            player.SetParent(Ship.transform);
            Debug.Log("Player has been parented to Ship");
        }
    }
    private void OnTriggerExit(Collider other)
    {
        if (other.CompareTag("Player"))
        {
            player.SetParent(null);
            Debug.Log("Player has been UNparented to Ship");
        }
    }
    private void ShipGO()
    {
        if (!EndPointReached)
        {
            Ship.transform.position = Vector3.Lerp(Ship.transform.position, targetPoint, movementSpeed * Time.deltaTime);
            if (Vector3.Distance(Ship.transform.position, targetPoint) <= 2.0f)
            {
                EndPointReached = true;
            }
        }
        else if (EndPointReached)
        {
            targetPoint = startPoint;
            Ship.transform.position = Vector3.Lerp(Ship.transform.position, targetPoint, movementSpeed * Time.deltaTime);
            if (Vector3.Distance(Ship.transform.position, targetPoint) <= 0.1f)
            {
                targetPoint = endPoint;
                EndPointReached = false;
            }
        }
    }
    private void FixedUpdate()
    {
        ShipGO();
        if (player != null && player.parent != null)
        {
            Vector3 deltaMovement = Vector3.zero;
            player.Translate(deltaMovement, Space.World);
            if (Input.GetKeyDown(KeyCode.Space))
            {
                player.SetParent(null);
            }
        }
    }
}