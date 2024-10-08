using UnityEngine.SceneManagement;
using UnityEngine;

public class Menu : MonoBehaviour
{
    //menu function for main menu scene
    public void onPlayButton()
    {
        SceneManager.LoadScene(1);
    }
    //so you can close the game
    public void onQuit()
    {
        Application.Quit();
    }
}
