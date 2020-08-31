//Dylan G
using UnityEngine;
using System.Collections;
using TMPro;
using UnityEngine.SceneManagement;

public class wave_spawner : MonoBehaviour
{
    public enum spawn_state { spawning, waiting, counting };

    public TextMeshProUGUI zombies_alive;

    public TextMeshProUGUI wave_timer;

    public int health_buff = 5;
    public int dmg_buff = 3;
    
    public GameObject enemy;

    public GameObject holster;

    [System.Serializable]
    public class Wave
    {
        public string name;
        public Transform enemy;
        public int num_of_zombies;
        public float spawn_rate;
    }

    public Wave[] waves;
    public int next_wave = 1;

    public Transform[] spawn_points;


    public float time_between_waves = 10;
    public float wave_count_down;

    private float check_timer = 1f;

    private spawn_state state = spawn_state.counting;


    private void Start()
    {
       enemy.GetComponent<Enemy>().max_health = enemy.GetComponent<Enemy>().start_health;
        wave_count_down = time_between_waves;



    }

    private void Update()
    {
        wave_timer.text = "Next Wave spawning in: " + (int)wave_count_down;


        zombies_alive.text = "zombies: " + GameObject.FindGameObjectsWithTag("Enemy").Length;


        if(state == spawn_state.waiting)
        {
            if (!enemy_alive())
            {
                wave_complete();
            }
            else
            {
                return;
            }
        }

        if(wave_count_down <= 0  && state != spawn_state.spawning)
        {
            wave_timer.enabled = false;
            StartCoroutine(Spawn_wave (waves[next_wave]));
        }
        else
        {
            wave_count_down -= Time.deltaTime;
        }

        
    }

    void wave_complete()
    {

        state = spawn_state.counting;
        wave_timer.enabled = true;
        wave_timer.text = "Next Wave spawning in: " + wave_count_down;

        wave_count_down = time_between_waves;

        holster.GetComponent<weapon_switch>().selected_weapon = Random.Range(0, 3);

        holster.GetComponent<weapon_switch>().select_weapon();

        if(next_wave + 1 > waves.Length - 1)
        {
            SceneManager.LoadScene("wins");
        }
        else
        {

            enemy.GetComponent<Enemy>().max_health += health_buff;
            next_wave++;
            

        }

    }

    bool enemy_alive()
    {
        check_timer -= Time.deltaTime;
        if (check_timer <= 0)
        {
            check_timer = 1f;

            if (GameObject.FindGameObjectWithTag("Enemy") == null)
            {
                return false;
            }
        }
        return true;
    }


    IEnumerator Spawn_wave(Wave current_wave)
    {
        if(current_wave.num_of_zombies < 30)
        {
            current_wave.num_of_zombies = next_wave * 5;
        }
        else
        {
            current_wave.num_of_zombies = 30;
            current_wave.spawn_rate = 1;
        }
      
        state = spawn_state.spawning;

        for(int i = 0; i < current_wave.num_of_zombies; i++)
        {
            spawn(current_wave.enemy);
            yield return new WaitForSeconds(1f / current_wave.spawn_rate);
        }

        state = spawn_state.waiting;
        yield break;
    }


    void spawn(Transform enemy)
    {
        Transform ran_point = spawn_points[ Random.Range (0, spawn_points.Length)];

        Instantiate(enemy, ran_point.position, ran_point.rotation);

    }


}
