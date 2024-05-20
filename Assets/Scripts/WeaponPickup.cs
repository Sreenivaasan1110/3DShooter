using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
public class WeaponPickup : MonoBehaviour
{
    public Transform equip_pos; //Equip position;
    public float distance = 10f; //distrance from where the object can be picked up
    protected GameObject currentWeapon;
    protected GameObject wp; //weapon
    // 

    bool canGrab;
    // Start is called before the first frame update
    void Start()
    {
        

    }

    // Update is called once per frame
    void Update()
    {
        CheckWeapon();

        if(canGrab)
        {
            if (Input.GetKeyDown(KeyCode.E))
            {
                if(currentWeapon != null)
                {
                    Drop();
                }
                PickUp();
            }
        }
        if (currentWeapon != null)
        {
            if (Input.GetKeyDown(KeyCode.Q))
            {
                Drop();
            }
               
        }
    }

    private void CheckWeapon()
    {
        RaycastHit hit;
       // Debug.Log(Physics.Raycast(Camera.main.transform.position, Camera.main.transform.forward, out hit, distance));
        if(Physics.Raycast(Camera.main.transform.position,Camera.main.transform.forward,out hit , distance)) //check for ray hits object with collider
        {
            if(hit.transform.tag == "canGrab") //Checks the object having canGrab tag ie weapon
            {
                Debug.Log("I can grab it");
                canGrab = true;
                wp = hit.transform.gameObject;
            }
        }
        else
        {
            canGrab = false;
        }
    }
    private void PickUp()
    {
        currentWeapon = wp;
        currentWeapon.transform.position = equip_pos.position; //making the weapon to set to player hand
        currentWeapon.transform.parent = equip_pos;
        currentWeapon.transform.localEulerAngles = new Vector3(0f, 0f, 0f);
        currentWeapon.GetComponent<Rigidbody>().isKinematic = true;

        if (currentWeapon == wp && SceneManager.GetActiveScene().buildIndex + 1 <= 2)
        {
            SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex + 1);
            Debug.Log("Next Level");
        }

        RayCastGun rayCastGun = currentWeapon.GetComponent<RayCastGun>();
        if (rayCastGun != null)
        {
            rayCastGun.isPickedUp = true;
        }
    }
    private void Drop()
    {
        currentWeapon.transform.parent = null;
        currentWeapon.GetComponent<Rigidbody>().isKinematic = false;
        currentWeapon = null;
        
    }
}
