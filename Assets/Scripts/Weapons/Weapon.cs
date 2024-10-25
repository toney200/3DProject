
using StarterAssets;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEditor.Experimental.GraphView;
using UnityEngine;
using UnityEngine.InputSystem;

public class Weapon : MonoBehaviour
{
    public float damage = 10f;
    public float range = 100f;
    public float fireRate = 15f;
    public float impactForce = 40f;

    public Camera fpsCam;
    public ParticleSystem muzzleFlash;
    public GameObject impactEffect;
   
    private void Update()
    {
        if (Input.GetButtonDown("Fire1"))
        {
            {
                Shoot();
            }
        }
    }

    void Shoot()
    {
        muzzleFlash.Play();

        RaycastHit hit;
        //Shoots raycast from camera position
        if (Physics.Raycast(fpsCam.transform.position, fpsCam.transform.forward, out hit))
        {
            //get the name of object that is hit
            Debug.Log(hit.transform.name);
            //check if target component is found. 
            //if yes target takes damage.
            Target target = hit.transform.GetComponent<Target>();
            if (target != null)
            {
                target.TakeDamage(damage);
            }
           

            //Add force to object that is hit
            if (hit.rigidbody != null)
            {
                hit.rigidbody.AddForce(-hit.normal * impactForce);
            }
         }
          /*  GameObject impactGameObject = Instantiate(impactEffect, hit.point, Quaternion.LookRotation(hit.normal));
            Destroy(impactGameObject, 0.5f);*/


    }
}


