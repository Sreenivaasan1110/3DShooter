using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using TMPro;
public class RayCastGun : WeaponPickup
{
    public GameObject playAgainButton;
    public TextMeshProUGUI Win;
    // Start is called before the first frame update
    public GameObject objectToDisplay;
    public TextMeshProUGUI BulletCount;
    public TextMeshProUGUI Animalcount;
    public Camera playerCamera;
    public Transform laserOrigin;
    public float gunRange = 500f;
    public float fireRate = 0.2f;
    public float laserDuration = 0.05f;
    LineRenderer laserLine;
    float fireTimer;
    Rigidbody movement;
    public float speed = 20f;
    public float rspeed = 2f;
    public static int count = 0;
    public   static int  laserc = 5;
    public bool isPickedUp = false;
    public bool setRaycast = true;
    void Awake()
    {
        laserLine = GetComponent<LineRenderer>();
        movement = GetComponent<Rigidbody>();
    }

    void Update()
    {
        
        fireTimer += Time.deltaTime;
        
        bool a = fireTimer > fireRate;
        Debug.Log("Fire Timer " + a  );
        Debug.Log("Laserc used " + laserc );
        Debug.Log("Checking Button" + Input.GetButtonDown("Fire1"));
        if (isPickedUp && Input.GetButtonDown("Fire1") && fireTimer > fireRate  && setRaycast)
        {
            Debug.Log("Inside Raycast");
          //  laserLine.enabled = true;
            
            Debug.Log("You can shoot");
            laserLine.SetPosition(0, laserOrigin.position);
            RaycastHit hit;
            Vector3 rayOrigin = playerCamera.ViewportToWorldPoint(new Vector3(0.5f, 0.5f, 0));
            
                if (Physics.Raycast(rayOrigin, playerCamera.transform.forward, out hit, gunRange))
                {
                laserc--;
                laserLine.SetPosition(1, hit.point);
                    

                    if (hit.transform.gameObject.tag == "Barrel")
                    {
                        count++;
                        Destroy(hit.transform.gameObject);
                    Vector3 instantiatePosition = hit.point + new Vector3(0,0.4f, 0);
                    Instantiate(objectToDisplay, instantiatePosition, Quaternion.identity);
                }
                if (hit.transform.gameObject.tag == "Energy")
                {
                    laserc = 5;
                    Destroy(hit.transform.gameObject);
                }

                if (laserc ==0)
                {
                    setRaycast = false;
                    playAgainButton.SetActive(true);

                    // Stop the game (optional)
                    Time.timeScale = 0f;

                }
                }
                else
                {
                    laserLine.SetPosition(0, laserOrigin.position);
                    laserLine.SetPosition(1, rayOrigin + (playerCamera.transform.forward * gunRange));
                }

                StartCoroutine(ShootLaser());
                fireTimer = 0f; // Reset the fire timer

         

        }
        try
        {
            BulletCount.text = "Bullets :" + laserc.ToString() + "/5";
            Animalcount.text = "Animals Killed:" + count.ToString() + "/4";
        }
        catch (System.Exception e) // Catch any type of exception
        {
            Debug.Log("An exception occurred: " + e.Message);
        }

        if (count == 4)
        {
            Win.text = "You Win !!";

            // Stop the game (optional)
            Time.timeScale = 0f;
        }

    }


    public void Meat()
    {

    }

   
    IEnumerator ShootLaser()
    {
        laserLine.enabled = true;
        yield return new WaitForSeconds(laserDuration);
        laserLine.enabled = false;
    }
}
