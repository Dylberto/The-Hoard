using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ammo_pickup : MonoBehaviour
{
    public int value = 100;
    public GameObject gun;

    void Awake()
    {
        gun = GameObject.FindGameObjectWithTag("gun");
    }

    private void OnTriggerEnter(Collider other)
    {
        if(other.tag == "Player")
        {
            gun.GetComponent<Gun>().ammo_held += value;

            gun.GetComponent<Gun>().current_ammo = gun.GetComponent<Gun>().max_ammo;

            if(gun.GetComponent<Gun>().ammo_held > gun.GetComponent<Gun>().max_held_ammo)
            {
                gun.GetComponent<Gun>().ammo_held = gun.GetComponent<Gun>().max_held_ammo;
            }
            
                Destroy(gameObject);
        }
    }

    

}
