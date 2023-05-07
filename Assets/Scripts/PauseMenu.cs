using UnityEngine;
using UnityEngine.InputSystem;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class PauseMenu : MonoBehaviour {
    private bool isPaused; 

    private PlayerInput menuInput;

    public Button resumeButton;
    public Animator resumeButtonAnim;
    public Button quitButton;
    public Animator quitButtonAnim;

    public GameObject pauseMenuScreen;

    [SerializeField]
    private PlayerManager playerManager;

    public enum ButtonsToPress {
        resume,
        quit
    }

    public ButtonsToPress currentButton;

    // Start is called before the first frame update
    void Start()
    {
        Cursor.visible = false;
        Cursor.lockState = CursorLockMode.Locked;
        menuInput = new PlayerInput();

        currentButton = ButtonsToPress.resume;
        SetButtonAnimations(true, false);
    }

    void SetButtonAnimations(bool resumeHighlighted, bool quitHighlighted) {
        resumeButtonAnim.SetBool("Highlighted", resumeHighlighted);
        quitButtonAnim.SetBool("Highlighted", quitHighlighted);

    }

    public void MoveDown(InputAction.CallbackContext context) {
        if (context.performed) {
            if (currentButton != ButtonsToPress.quit) {
                currentButton++;
            }
        }
    }

    public void MoveUp(InputAction.CallbackContext context) {
        if (context.performed) {
            if (currentButton != ButtonsToPress.resume) {
                currentButton--;
            }
        }
    }

    public void SelectButton(InputAction.CallbackContext context) {
        if (context.performed) {
            switch (currentButton) {
                case (ButtonsToPress.resume):
                    ResumeGame();
                    break;
                case (ButtonsToPress.quit):
                    ReturnToMenu();
                    break;
            }
        }
    }

    // Update is called once per frame
    void Update()
    {
        switch (currentButton) {
            case (ButtonsToPress.resume):
                SetButtonAnimations(true, false);
                break;
            case (ButtonsToPress.quit):
                SetButtonAnimations(false, true);
                break;

        }
    }

    public void PauseTheGame() {
        isPaused = true;
        pauseMenuScreen.SetActive(true);
        SetButtonAnimations(true, false);
        menuInput.Enable();
        Time.timeScale = 0;
    }

    public void ResumeGame() {
        isPaused = false;
        menuInput.Disable();
        Time.timeScale = 1;
        pauseMenuScreen.SetActive(false);
    }

    public void ReturnToMenu() {
        SceneManager.LoadScene(0);
    }

    public bool IsTheGamePaused() {
        return isPaused;
    }
}
