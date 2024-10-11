using StarterAssets;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerAnimation : MonoBehaviour
{

    
    Animation motion;
    Animator animator;
    // Start is called before the first frame update
    void Start()
    {
        motion = GetComponent<Animation>();
        animator = GetComponent<Animator>();
        
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public void OnMove()
    {
        
    }
}
