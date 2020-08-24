using UnityEngine;
using UnityEngine.SceneManagement;
using System.Collections;
using UnityEngine.PostProcessing;

//Dylan G

public class player : MonoBehaviour
{
    public int max_health = 2;
    public int current_health;
    private int regen_rate = 1;

    public PostProcessingProfile motion_blur;

    public void Start()
    {
        current_health = max_health;



    }


    public void take_dmg (int dmg)
    {
        current_health -= dmg;
    }


    private void Update()
    {
        if (current_health < max_health)
        {
            StartCoroutine(Regen());
        }


        if( current_health <= 0)
        {
            died();
        }
        
     


    }



    IEnumerator Regen()
    {

        yield return new WaitForSeconds(10);

        current_health += regen_rate;

        if (current_health > max_health)
        {
            current_health = max_health;
        }

    }

    void died()
    {
        SceneManager.LoadScene("loss");

    }
}
