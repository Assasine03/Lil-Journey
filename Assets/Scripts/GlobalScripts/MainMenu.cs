using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class MainMenu : MonoBehaviour
{

    [SerializeField] GameObject MainMenuPanel;
    [SerializeField] GameObject SettingsFrame;

    public void Continue()
    {
        SceneManager.LoadSceneAsync(1);
    }


    public void Exit()
    {
        Application.Quit();
    }

    public void Settings()
    {

    }
}
