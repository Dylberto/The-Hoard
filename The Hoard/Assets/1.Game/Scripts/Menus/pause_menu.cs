//Dylan G
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class pause_menu : MonoBehaviour
{
    public static bool game_paused = false;

    public GameObject pause_UI;
    public GameObject game_UI;
    
    void Update()
    {
        if (Input.GetKeyDown(KeyCode.Escape))
        {
            if (game_paused)
            {
                resume();
            }
            else
            {
                pause();
            }
        }
    }
    void pause()
    {
        pause_UI.SetActive(true);
        Time.timeScale = 0f;
        game_paused = true;
        game_UI.SetActive(false);

        Cursor.lockState = CursorLockMode.None;
        Cursor.visible = true;
    }

    public void resume()
    {
        pause_UI.SetActive(false);
        Time.timeScale = 1f;
        game_paused = false;
        game_UI.SetActive(true);

        Cursor.lockState = CursorLockMode.Confined;
        Cursor.visible = false;
    }

    public void load_menu()
    {
        SceneManager.LoadScene("Start");
    }


    public void quit()
    {
        Application.Quit();
    }
   
}
