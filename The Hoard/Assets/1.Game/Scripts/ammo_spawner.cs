//Dylan G
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ammo_spawner : MonoBehaviour
{
    public GameObject current_gun;
    public bool ammo_spawned = false;
    public Transform[] spawn_points;
    public float wait_time = 120f;
    public Transform ammo;


     void Update()
    {
        current_gun = GameObject.FindGameObjectWithTag("gun");


        if(current_gun.GetComponent<Gun>().ammo_held < current_gun.GetComponent<Gun>().max_held_ammo / 2 && ammo_spawned == false)
        {

             StartCoroutine(ammo_create());

           // spawn_ammo();
        }

    }

    void spawn_ammo()
    {
        ammo_spawned = true;
        Transform ran_point = spawn_points[Random.Range(0, spawn_points.Length)];

        Instantiate(ammo, ran_point.position, ran_point.rotation);

    }

     IEnumerator ammo_create()
    {
        spawn_ammo();
        yield return new WaitForSeconds(60);
        ammo_spawned = false;
        
     
     }
     
     

}
