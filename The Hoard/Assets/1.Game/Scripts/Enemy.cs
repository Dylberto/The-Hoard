//Dylan G
using UnityEngine;
using System.Collections;
using UnityEngine.AI;
using UnityEngine.UI;



public class Enemy : MonoBehaviour
{

    //Public Variables
    public int start_health = 20;
    public int max_health = 20;                                                    // health of the enemy
    public float radius = 100f;                                                  // radius of following player
    public int enemy_dmg = 1;                                                 // damage done by enemy
    public float cooldown = 0.01f;                                                // damage cooldown
    public GameObject player;                                                 // reference to the player
    public bool attacking = false;
    public Image health_bar;
    //Private Variables
    private bool dead = false;                                                // is the enemy dead?
    private NavMeshAgent navi;                                               // reference Nav Mesh agent
    private Animator anim;                                                  // reference Animator
    private float despawn_timer = 2;                                      // time it takes to destroy game object
    private AudioSource sound;
    public float current_health;



    private void Start()
    {
        player = GameObject.FindGameObjectWithTag("Player");   // finds the player in the scene using its tag
        navi = GetComponent<NavMeshAgent>();                  // gets nav mesh agent on this game object
        anim = GetComponent<Animator>();                     // gets animator on this game object
        sound = GetComponent<AudioSource>();

        StartCoroutine(play_sound());

        current_health = max_health;

    }                                                                                            // functions that take place when scene loads


    private void Update()
    {

        if (dead == true)
        {
            return;
        }                                                                                          // if the enemy is dead don't run other functions

        float dist = Vector3.Distance(player.transform.position, transform.position);                                // distance from this enemy to the player

        if (dist <= radius)                                                                                        
        {
            if (attacking == false )
            {
                navi.SetDestination(player.transform.position);         // change this game objects postion to the player
                anim.SetBool("running", true);                         // set the running bool in animator to true

                if (dist <= navi.stoppingDistance)                    // if the distance between the enemy and the player is less then the Nav Mesh Agents stopping distance
                {
                    face_player();                                   // run the face_player function
                }
            }                                                // if the player is visible

        }                                                                                     // if the player is withinn the radius of the enemy
        else                                                                                                       
        {
            anim.SetBool("running", false);                     // set running bool in animator to false
        }                                                                                                   // if the above is not true


    }                                                                                         // functions that take place every frame 


    void face_player()
    {
        Vector3 direct = (player.transform.position - transform.position).normalized;                              // difference between the player postion and this enemy
        Quaternion look_ro = Quaternion.LookRotation(new Vector3(direct.x, 0, direct.z));                         // quaternion that takes the vaules
        transform.rotation = Quaternion.Slerp(transform.rotation, look_ro, Time.deltaTime * 5f);                 // changes this enemies rotation to look at the players postion
    }                                                                                         // function that rotates the enemy to face the player


    public void take_dmg (float dmg)  
    {
        current_health -= dmg;                //takes dmg away from health

        health_bar.fillAmount = current_health / max_health;

        if(health_bar.fillAmount <= 0.5f)
        {
            health_bar.color = Color.yellow;
        }

        if (health_bar.fillAmount <= 0.3f)
        {
            health_bar.color = Color.red;
        }

        if (current_health <= 0)               
        {
            die();                   //activats die function
        }           //if health = 0
    }                                                                        // function that deals with enemy taking damage


    void die()
    {
        StartCoroutine(dying());

    }                                                                                               // function that kills enemy


    private void OnTriggerEnter(Collider other)
    {
        if(other.tag == "Player" && attacking == false)
        {
            StartCoroutine(attack());
        }
    }


    IEnumerator play_sound()
    {
        while (true)
        {
            yield return new WaitForSeconds(Random.Range(1, 20));
            sound.Play();
        }
    }

    IEnumerator dying()
    {
        sound.Stop();
        GetComponent<Collider>().enabled = false;              // disables capsule collider to stop the player from taking damage
        dead = true;                                          // sets dead bool to true (see Update for why)
        navi.enabled = false;                                // disables the Nav Mesh agent (stops this enemy from moving)
        anim.SetBool("dead", true);                         // sets the dead bool in animator to true

        yield return new WaitForSeconds(despawn_timer);  //wait for  time to happen

        Destroy(gameObject);              //destroys object attach with this script

    }                                                                                    // function that takes care of what happens when this enemy has died

    IEnumerator attack()
    {
        attacking = true;

        anim.SetBool("running", false);

        anim.SetTrigger("attack");

        yield return new WaitForSeconds(0.05f);

        anim.SetBool("attacking", false);
        player.GetComponent<player>().take_dmg(enemy_dmg);

        yield return new WaitForSeconds(cooldown);


        anim.SetBool("running", true);
        anim.SetBool("attacking", true);

        attacking = false;


    }







    private void OnDrawGizmosSelected()
    {
        Gizmos.color = Color.magenta;                           // color of gizmo lines
        Gizmos.DrawWireSphere(transform.position, radius);     // draws a sphere with the centre begin the enemy and the raduis begin the radius variable


        Gizmos.color = Color.black;                                              // color of gizmo line
        Gizmos.DrawLine(transform.position, player.transform.position);         // draws a line between this enemy and the player
    }                                                                 // gizmos, visual representitive of some values

}
