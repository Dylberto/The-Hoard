using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ammo_spawner : MonoBehaviour
{
    private GameObject gun;

    public Transform[] spawn_points;

    public Transform ammo;

    void Start()
    {
        gun = GameObject.FindGameObjectWithTag("gun");
    }



    void Update()
    {
        if (gun.GetComponent<Gun>().ammo_held <=  gun.GetComponent<Gun>().max_held_ammo / 4  && GameObject.FindGameObjectWithTag("ammo") == null)
        {
            spawn_ammo();
        }
        

        
    }

    void spawn_ammo()
    {
        Transform ran_point = spawn_points[Random.Range(0, spawn_points.Length)];

        Instantiate(ammo, ran_point.position, ran_point.rotation);

    }


}
