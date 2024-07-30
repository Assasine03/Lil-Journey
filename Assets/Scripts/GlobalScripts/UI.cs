using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class UI : MonoBehaviour
{

    [SerializeField] GameObject PausePanel;
    [SerializeField] GameObject PlayerFrame;
    [SerializeField] GameObject SettingsFrame;

    public void Continue()
    {
        Time.timeScale = 1;
        PausePanel.SetActive(false);
        PlayerFrame.SetActive(true);
    }

    public void Pause()
    {
        Time.timeScale = 0;
        PausePanel.SetActive(true);
        PlayerFrame.SetActive(false);
    }

    public void Exit()
    {
        Application.Quit();
    }

    public void Settings()
    {

    }
}
