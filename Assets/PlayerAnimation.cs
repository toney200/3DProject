using StarterAssets;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerAnimation : MonoBehaviour
{

    public GameObject player;
    Animation motion;
    Animator animator;
    // Start is called before the first frame update
    void Start()
    {
        motion = GetComponent<Animation>();
        animator = GetComponent<Animator>();
        player = GetComponent<GameObject>();
        player.GetComponent<FirstPersonController>();
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public void OnMove()
    {
        if (player.GetComponent<FirstPersonController>().isMoving)
        {
            animator.Play("Walking");
        }
    }
}
