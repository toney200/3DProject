using System.Collections;
using System.Collections.Generic;
using TMPro;
using Unity.VisualScripting;
using UnityEngine;

public class ProjectileGun : MonoBehaviour
{
    public GameObject bullet;
    public GameObject muzzleFlash;
    public TextMeshProUGUI ammoDisplay;
    public Camera fpsCam;
    public Transform attackPoint;

    //bullet force
    public float shootForce;
    public float upwardForce;

    //Gun stats
    public int magazineSize;
    public float timeBetweenShots = 0.2f;
    //public int bulletsPerTap;
    public bool allowButonHold;
    int bulletsLeft;
    int bulletsShot;

    //Checkers
    bool shooting;
    bool readyToShoot;
    bool reloading;


    private void Awake()
    {
        bulletsLeft = magazineSize;
        readyToShoot = true;
    }

    private void Update()
    {
        MyInput();

        if(ammoDisplay != null)
        {
            ammoDisplay.SetText(bulletsLeft + " / " + magazineSize);
        }

    }

    private void  MyInput()
    {
        //Check if weapon is automatic
        if (allowButonHold)
        {
            shooting = Input.GetKey(KeyCode.Mouse0);
        }
        else
        {
            shooting = Input.GetKeyDown(KeyCode.Mouse0);
        }
        //Check if player can shoot 
        if(readyToShoot && shooting && !reloading && bulletsLeft > 0)
        {
            bulletsShot = 0;

            Shoot();
        }

        //Reload
        if(Input.GetKeyDown(KeyCode.R) && bulletsLeft < magazineSize && !reloading)
        {
            reloading = true;
            bulletsLeft = magazineSize;
        }
        else
        {
            reloading = false;
        }

        //Autoamtic reload if no bullets left in mag
        if(readyToShoot && shooting && !reloading && bulletsLeft <= 0)
        {
            reloading = true;
            bulletsLeft = magazineSize;
        }
        else
        {
            reloading = false;
        }


    }

    private void Shoot()
    {
        readyToShoot = false;
        //Find position to hit using raycast
        //ray through middle of screen
        Ray ray = fpsCam.ViewportPointToRay(new Vector3(0.5f, 0.5f, 0));
        RaycastHit hit;

        //check if ray hit something
        Vector3 targetPoint;
        if(Physics.Raycast(ray, out hit)){
            targetPoint = hit.point;
        }
        else
        {
            targetPoint = ray.GetPoint(75);
        }

        //Claculate direction
        Vector3 direction = targetPoint - attackPoint.position;

        //Create bullet
        GameObject currentBullet = Instantiate(bullet, attackPoint.position, Quaternion.identity);

        //rotate bullet to shoot forward
        currentBullet.transform.forward = direction.normalized;

        //Add force to bullets
        currentBullet.GetComponent<Rigidbody>().AddForce(direction.normalized * shootForce, ForceMode.Impulse);


        bulletsLeft--;
        bulletsShot++;

        StartCoroutine(ShootCooldown());

        Destroy(currentBullet, 3f);
    }

    IEnumerator ShootCooldown()
    {
        yield return new WaitForSeconds(timeBetweenShots);
        readyToShoot = true;
    }

}
