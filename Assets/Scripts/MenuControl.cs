using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class MenuControl : MonoBehaviour
{

    public void StartLevel1()
    {
        SceneManager.LoadScene("Level_1");
    }

    public void StartLevel2()
    {
        SceneManager.LoadScene("Level_2");
    }

    public void StartLevel3()
    {
        SceneManager.LoadScene("Level_3");
    }

    public void ToMenu()
    {
        SceneManager.LoadScene("Menu");
    }

    public void ToPauseMenu()
    {
        SceneManager.LoadScene("PauseMenu");
    }

    public void ContinueGame()
    {
        SceneManager.LoadScene(PlayerPrefs.GetInt("lastScene"));
    }
    public void QuitGame()
    {
        Application.Quit();
    }
}
