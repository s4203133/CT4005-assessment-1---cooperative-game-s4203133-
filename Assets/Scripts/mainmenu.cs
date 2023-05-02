using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class MainMenu : MonoBehaviour
{
    public Button playButton;
    public Button exitButton;
  
    void Start()
    {

        playButton.onClick.AddListener(PlayGame);

        exitButton.onClick.AddListener(QuitGame);
    }
    void PlayGame()
    {

        SceneManager.LoadScene(1);
    }
    void QuitGame()
    {
        Application.Quit();
    }

}
