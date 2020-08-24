using UnityEngine;
using UnityEngine.SceneManagement;

//Dylan G

public class main_menu : MonoBehaviour {

    private void Start()
    {
        Cursor.visible = true;
        Cursor.lockState = CursorLockMode.None;
    }


    public void play_game()
    {
        SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex + 1);
    }

    public void Exit()
    {
        if(SceneManager.GetActiveScene().buildIndex == 0)
        {
            Application.Quit();

        }
        else
        {
            SceneManager.LoadScene("start");
        }
    }
}