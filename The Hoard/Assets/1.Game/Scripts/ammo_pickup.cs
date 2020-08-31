//Dylan G
using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ammo_pickup : MonoBehaviour
{
    public GameObject ammo_spawner;
    public GameObject gun;


    void Update()
    {
        gun = GameObject.FindGameObjectWithTag("gun");
    }

    private void OnTriggerEnter(Collider other)
    {
        if(other.tag == "Player")
        {
            gun.GetComponent<Gun>().ammo_held = gun.GetComponent<Gun>().max_held_ammo;

            gun.GetComponent<Gun>().current_ammo = gun.GetComponent<Gun>().max_ammo;

            if(gun.GetComponent<Gun>().ammo_held > gun.GetComponent<Gun>().max_held_ammo)
            {
                gun.GetComponent<Gun>().ammo_held = gun.GetComponent<Gun>().max_held_ammo;
            }

            ammo_spawner.GetComponent<ammo_spawner>().ammo_spawned = false;

            Destroy(gameObject);

            

        }
    }


}
