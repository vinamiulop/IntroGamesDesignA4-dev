using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FloorTeleporter : MonoBehaviour
{
    public Vector3 respawnWorldPosition;

    private void OnTriggerEnter(Collider other) 
    {
        other.transform.position = respawnWorldPosition;
    }
}
