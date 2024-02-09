using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FlyingShip : MonoBehaviour
{

    public GameObject FlyingDutchmen;
    public float Speed;

    private void FixedUpdate()
    {
        FlyingDutchmen.transform.Translate(Vector3.forward * Speed * Time.deltaTime);
    }





}
