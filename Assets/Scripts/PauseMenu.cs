using UnityEngine;
using UnityEngine.SceneManagement;

public class PauseMenu : MonoBehaviour
{
    public GameObject pauseMenuScreen;

    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public void PauseTheGame() {
        pauseMenuScreen.SetActive(true);
        Debug.Log("Paused Game");
        Time.timeScale = 0;
    }

    public void ResumeGame() {
        Time.timeScale = 1;
        Debug.Log("Resumed Game");
        pauseMenuScreen.SetActive(false);
    }

    public void ReturnToMenu() {
        SceneManager.LoadScene(0);
    }
}
