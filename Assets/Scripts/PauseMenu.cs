using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class PauseMenu : MonoBehaviour {
    private bool isPaused;

    [SerializeField]
    private Animator resumeButtonAnim;
    [SerializeField]
    private Animator quitButtonAnim;
    [SerializeField]
    private Animator cancelQuitButtonAnim;
    [SerializeField]
    private Animator confirmQuitButtonAnim;

    [SerializeField]
    private Canvas pauseMenuScreen;
    [SerializeField]
    private Canvas quitMenuScreen;

    [SerializeField]
    private PlayerManager playerManager;
    [SerializeField]
    private EnemySpawner enemySpawner;
    [SerializeField]
    private GameObject[] allThrowableObjects;
    [SerializeField]
    private List<Animator> animsToPause;
    [SerializeField]
    private List<ParticleSystem> particlesToPause;

    private enum PauseButtons {
        resume,
        quit
    }

    private enum QuitButtons {
        cancel,
        yes
    }

    [SerializeField]
    private PauseButtons currentPauseButton;
    [SerializeField]
    private QuitButtons currentQuitButton;

    void Start()
    {
        Cursor.visible = false;
        Cursor.lockState = CursorLockMode.Locked;
        allThrowableObjects = GameObject.FindGameObjectsWithTag("PickUp");

        currentPauseButton = PauseButtons.resume;
        SetPauseButtonAnimations(true, false);
    }

    void SetPauseButtonAnimations(bool resumeHighlighted, bool quitHighlighted) {
        resumeButtonAnim.SetBool("Highlighted", resumeHighlighted);
        quitButtonAnim.SetBool("Highlighted", quitHighlighted);
    }

    void SetQuitButtonAnimations(bool cancelHighlighted, bool confirmHighlighted) {
        cancelQuitButtonAnim.SetBool("Highlighted", cancelHighlighted);
        confirmQuitButtonAnim.SetBool("Highlighted", confirmHighlighted);
    }

    public void MoveUp() {
        if (currentPauseButton != PauseButtons.resume) {
            currentPauseButton--;
        }
    }

    public void MoveDown() {
        if (currentPauseButton != PauseButtons.quit) {
            currentPauseButton++;
        }
    }

    public void MoveLeft() {
        if (currentQuitButton != QuitButtons.cancel) {
            currentQuitButton--;
        }
    }

    public void moveRight() {
        if (currentQuitButton != QuitButtons.yes) {
            currentQuitButton++;
        }
    }

    public void SelectButton() {
        if (pauseMenuScreen.enabled && !quitMenuScreen.enabled) {
            switch (currentPauseButton) {
                case (PauseButtons.resume):
                    ResumeGame();
                    break;
                case (PauseButtons.quit):
                    OpenQuitMenu();
                    break;
            }
        }
        else if (pauseMenuScreen.enabled && quitMenuScreen.enabled) {
            switch (currentQuitButton) {
                case (QuitButtons.cancel):
                    BackToPauseMenu();
                    break;
                case (QuitButtons.yes):
                    ReturnToMenu();
                    break;
            }
        }
    }

    void Update()
    {
        if (pauseMenuScreen.enabled && !quitMenuScreen.enabled) {
            switch (currentPauseButton) {
                case (PauseButtons.resume):
                    SetPauseButtonAnimations(true, false);
                    break;
                case (PauseButtons.quit):
                    SetPauseButtonAnimations(false, true);
                    break;
            }
        } 
         else if (pauseMenuScreen.enabled && quitMenuScreen.enabled) {
            switch (currentQuitButton) {
                case (QuitButtons.cancel):
                    SetQuitButtonAnimations(true, false);
                    break;
                case (QuitButtons.yes):
                    SetQuitButtonAnimations(false, true);
                    break;
            }
        }
    }

    public void PauseTheGame() {
        currentPauseButton = PauseButtons.resume;
        foreach (GameObject player in playerManager.players) {
            PlayerController playerController = player.GetComponent<PlayerController>();
            playerController.stateBeforePaused = playerController.playerState;
            playerController.playerState = PlayerController.playerMode.stationary;
            player.GetComponent<Rigidbody>().isKinematic = true;
        }
        foreach(GameObject throwableObejct in allThrowableObjects) {
            throwableObejct.GetComponent<Throwable>().PausePhysics();
        }
        foreach (GameObject enemy in enemySpawner.allEnemies) {
            enemy.GetComponent<EnemyPatrol>().FreezeAgent();
            EnemyHealth enemyHealth = enemy.GetComponent<EnemyHealth>();
            enemyHealth.PausePhysics();
            if (enemyHealth.isDead) {
                enemyHealth.CancelDeath();
            }
        }
        foreach(Animator anim in animsToPause) {
            anim.speed = 0;
        }
        foreach(ParticleSystem particle in particlesToPause) {
            particle.Pause();
        }
        isPaused = true;
        pauseMenuScreen.enabled = true;
    }

    public void ResumeGame() {
        foreach (GameObject player in playerManager.players) {
            PlayerController playerController = player.GetComponent<PlayerController>();
            playerController.playerState = playerController.stateBeforePaused;
            player.GetComponent<Rigidbody>().isKinematic = false;
            playerController.CancelStrafe();
            playerController.LowerShield();
        }
        foreach (GameObject throwableObejct in allThrowableObjects) {
            throwableObejct.GetComponent<Throwable>().ResumePhysics();
        }
        foreach (GameObject enemy in enemySpawner.allEnemies) {
            enemy.GetComponent<EnemyPatrol>().UnFreezeAgent();
            EnemyHealth enemyHealth = enemy.GetComponent<EnemyHealth>();
            enemyHealth.ResumePhysics();
            if (enemyHealth.isDead) {
                enemyHealth.StartDeathCountdown();
            }
        }
        foreach (Animator anim in animsToPause) {
            anim.speed = 1;
        }
        foreach (ParticleSystem particle in particlesToPause) {
            particle.Play();
        }
        isPaused = false;
        pauseMenuScreen.enabled = false;
    }

    private void OpenQuitMenu() {
        currentQuitButton = QuitButtons.cancel;
        quitMenuScreen.enabled = true;
    }

    private void BackToPauseMenu() {
        currentPauseButton = PauseButtons.quit;
        quitMenuScreen.enabled = false;
    }

    public void ReturnToMenu() {
        playerManager.FadeInBlackScreen();
        StartCoroutine(LoadSceneWithDelay(1));
    }

    IEnumerator LoadSceneWithDelay(float delay) {
        yield return new WaitForSeconds(delay);
        SceneManager.LoadScene(0);
    }

    public bool IsTheGamePaused() {
        return isPaused;
    }

    public void AddItemToPause(Throwable throwableToAdd) {
        
    }

    public void AddItemToPause() {

    }
}
