using UnityEngine;
using System.Collections;
using TMPro;

//Dylan G

public class Gun : MonoBehaviour
{

    //public variables
    public float dmg = 1;                         //damage done by gun
    public float range = 100f;                    //range of the bullets
    public float impact_force = 30f;             //impact the bullets on objects with a rigidody
    public float fire_rate = 15f;               //rate at which the gun can fire
    public ParticleSystem muzzle_flash;        //particle system
    public GameObject impact_effect;          //particle system
    public int ammo_held = 100;              // the amount of ammo held by the player
    public int max_held_ammo = 100;
    public int max_ammo = 15;               //sets max ammo
    public int current_ammo;               //empty value for current ammo
    public float reload_time = 1f;        //time it takes to reload
    public TextMeshProUGUI ammo_text;

    //private variables
    private Camera fps_cam;                  //camera needed for raycast of the bullets
    private float cooldown = 0f;            //cooldown for fire rate
    private bool is_reloading;          //stats and stops reloading
    private Animator anim;              //lets animations be used
    private AudioSource sound;


    void Start()
    {
        sound = GetComponent<AudioSource>();
        fps_cam = Camera.main;      //finds main camera in the scene

        current_ammo = max_ammo;   //sets current ammo to be max ammo

        anim = GetComponent<Animator>();   // gets animator on game object


        ammo_text.text = current_ammo + " / " + ammo_held;


    }                                    // fuction that plays at the start of the scene

    void Update()
    {
        ammo_text.text = current_ammo + " / " + ammo_held;

        

        if (is_reloading == true)
        {
            return;
        }       // if reloading stop the update method until done                   

        if (ammo_held > 0 && Input.GetKeyDown(KeyCode.R))
        {
            StartCoroutine(reload());
            return;
        }          // if current ammo = 0 start reloading


        if (Input.GetButton("Fire1") && Time.time >= cooldown)
        {

            cooldown = Time.time + 1 / fire_rate;
            shoot();

            sound.Play();

        }       // if left mouse is clicked and cooldown 



    }                                  //fuctions that is begin run every second

    void shoot()
    {
        if (current_ammo > 0)
        {
            muzzle_flash.Play();       //plays the particle system

            current_ammo--;           //takes 1 ammo away from current ammo

            RaycastHit hit;           //creats raycast (invisible line) called hit

            if (Physics.Raycast(fps_cam.transform.position, fps_cam.transform.forward, out hit, range))
            {
                Enemy enemy = hit.transform.GetComponent<Enemy>();                                             //locates an enemy script on the transform in the raycast
                if (enemy != null)                                                                             //if the transform has the enemy script
                {
                    enemy.take_dmg(dmg);                                                                       //takes damage of weapon away from health of the enemy (done in enemy script)
                }

                if (hit.rigidbody != null)                                                                     //if the transform has a rigidbody
                {
                    hit.rigidbody.AddForce(-hit.normal * impact_force);                                        //get knocked backwards (-) by impat force
                }

                GameObject impact = Instantiate(impact_effect, hit.point, Quaternion.LookRotation(hit.normal));  //creates the impact particle system where the raycast lands on the object
                Destroy(impact, 0.25f);                                                                          //destroys the particle gameobject after 1/4 of a second

            }        //if there is an object within range and in the centre of the cameras current orientation

        }
    }                                   //Fuction that takes care of anything to do with bullets

    IEnumerator reload()
    {
            is_reloading = true;                            //makes reloading start in update 

            anim.SetBool("reloading", true);               //triggers the reload animation to start

            yield return new WaitForSeconds(reload_time);  //wait for reload time to happen

        if (ammo_held > 0)
        {

            ammo_held -= (max_ammo - current_ammo);

            if(ammo_held > max_ammo)
            {
                current_ammo = max_ammo - ammo_held; 
            }

            current_ammo = max_ammo;                              //sets current ammo = max ammo
        }

        if (ammo_held <= 0)
        {
            ammo_held = 0;
        }

            anim.SetBool("reloading", false);              //triggers the reload animation to stop

            is_reloading = false;                         //makes reloading stop in update
        
    }                           //function that takes care of reloading

}
