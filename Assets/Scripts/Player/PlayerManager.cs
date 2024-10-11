using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerManager : MonoBehaviour
{
    GameObject player;
    
    
    // Start is called before the first frame update
    void Start()
    {
        player = GetComponent<GameObject>();    
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    private void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("Door") ){
            Debug.Log("Door triggered");
        }
    }
}
