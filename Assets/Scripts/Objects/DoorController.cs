using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DoorController : MonoBehaviour
{

    private Animator doorAnimator;
    

    private void Start()
    {
        doorAnimator = GetComponent<Animator>();    
        
    }
    //Check for player
    private void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("Player") && Collectable.collected)
        {
            Debug.Log("Door opened");
            //Door opens
            doorAnimator.SetBool("character_nearby", true);
        }
        
    }

    private void OnTriggerExit(Collider other)
    {
        if (other.CompareTag("Player"))
        {
            //Debug.Log("Door closed");
            //Door opens
            doorAnimator.SetBool("character_nearby", false);
        }
    }
}
