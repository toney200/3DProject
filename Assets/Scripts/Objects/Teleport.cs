using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Teleport : MonoBehaviour
{

    public Transform teleportLocation;

    private void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("Player"))
        {
            TeleportPlayer(other.GetComponent<CharacterController>());
        }
    }

    private void TeleportPlayer(CharacterController player)
    {
        if(teleportLocation != null)
        {
            player.enabled = false;

            player.transform.position = teleportLocation.position;

            player.enabled = true;
            Debug.Log("Player teleported" + teleportLocation);
        }
    }
}
