using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;
public class Weaponshooter : MonoBehaviour
{
    //Gun Stats
    public int damage; //Amount of damage to be done when he shoots
       public float timeBetweenShooting, spread, range, reloadTime, timeBetweenShots;
       public bool allowButtonHold;
       int bulletsLeft, BulletsShot;
       public int magazineSize, bulletsPerTap;

    //bools to check
    bool shooting, readyToShoot, reloading;

    //reference t be added
    public Camera fpsCam;
    public Transform attackPoint;
    public RaycastHit rayHit;
    public LayerMask whatIsEnemy;

    //Graphics 
    public GameObject muzzleFlash, bulletHoleGraphic;
    public TextMeshProUGUI text;


    // Start is called before the first frame update
    void Start()
    {
        bulletsLeft = magazineSize;
        readyToShoot = true;
    }

    // Update is called once per frame
    void Update()
    {
        shootInput();

        text.SetText(bulletsLeft + "/" + magazineSize);
    }

    private void shootInput()
    {
        if(allowButtonHold ) shooting = Input.GetKey(KeyCode.Mouse0);
        else shooting = Input.GetKeyDown(KeyCode.Mouse0);

        if (Input.GetKeyDown(KeyCode.R) && bulletsLeft < magazineSize && !reloading) Reload();
        if (readyToShoot && shooting && !reloading && bulletsLeft > 0)
        {
            BulletsShot = bulletsPerTap;
            Shoot();
        }


    }

    private void Shoot()
    {
        readyToShoot = false;

        float x = Random.Range(-spread, spread);
        float y = Random.Range(-spread, spread);

        //calculate Direction with Spread
        Vector3 direction = fpsCam.transform.forward + new Vector3(x, y, 0);

        //RayCast for checking the object is Enemy
        if (Physics.Raycast(fpsCam.transform.position ,direction, out rayHit , range , whatIsEnemy))
        {
            Debug.Log(rayHit.collider.name);

            if (rayHit.collider.CompareTag("Enemy"))
            {
                // rayHit.collider.GetComponent<ShootingAi>().TakeDamage(damage);
                Debug.Log("Enemy got hit");
            }
        }
        //Graphics 
        
        if(bulletsLeft >=0) {
            Instantiate(bulletHoleGraphic, rayHit.point, Quaternion.Euler(0, 180, 0));
            Instantiate(muzzleFlash, attackPoint.position, Quaternion.identity);
        }
        
        bulletsLeft--;
        BulletsShot--;
        if (bulletsLeft <= 0)
        {
            readyToShoot = true;
            return;
        }
       
        Invoke("ResetShot", timeBetweenShooting);

        if(BulletsShot >0 && bulletsLeft >0)
        {
            Invoke("Shoot", timeBetweenShots);
        }
    }
    private void ResetShot()
    {
        readyToShoot = true;
    }
    private void Reload()
    {
        reloading = true;
        Invoke("ReloadFinished", reloadTime);
    }

    private void ReloadFinished()
    {
        bulletsLeft = magazineSize;
        reloading = false;
    }
}
