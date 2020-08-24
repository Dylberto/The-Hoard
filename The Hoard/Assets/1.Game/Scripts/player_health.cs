using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class player_health : MonoBehaviour
{
    private GameObject player;
    public int health;
    public int num_of_hearts;


    public Image[] hearts;
    public Sprite full_heart;
    public Sprite empty_heart;


    void Start()
    {
        player = GameObject.FindGameObjectWithTag("Player");
        num_of_hearts = player.GetComponent<player>().max_health;
    }

    void Update()
    {
        health = player.GetComponent<player>().current_health;

        for(int i = 0; i < hearts.Length; i++)
        {
            if(i < health)
            {
                hearts[i].sprite = full_heart;
            }
            else
            {
                hearts[i].sprite = empty_heart;
            }


            if(i < num_of_hearts)
            {
                hearts[i].enabled = true;
            }
            else
            {
                hearts[i].enabled = false;
            }
        }
    }

}
