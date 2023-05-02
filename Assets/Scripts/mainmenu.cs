using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class mainmenu : MonoBehaviour
{
    private PlayerInput mainMenuInput;

    public Button playButton;
    public Animator playButtonAnim;

    public Button optionsButton;
    public Animator optionsButtonAnim;

    public Button tutorialButton;
    public Animator tutorialButtonAnim;

    public Button exitButton;
    public Animator exitButtonAnim;

    public enum ButtonsToPress {
        play,
        options,
        tutorial,
        quit
    }

    public ButtonsToPress currentButton;
  
    void Start()
    {
        Cursor.visible = false;
        Cursor.lockState = CursorLockMode.Locked;
        mainMenuInput = new PlayerInput();

        mainMenuInput.Enable();
        currentButton = ButtonsToPress.play;
        SetButtonAnimations(true, false, false, false);
    }

    private void Update() {
        switch (currentButton) {
            case (ButtonsToPress.play):
                SetButtonAnimations(true, false, false, false);
                break;
            case (ButtonsToPress.options):
                SetButtonAnimations(false, true, false, false);
                break;
            case (ButtonsToPress.tutorial):
                SetButtonAnimations(false, false, true, false);
                break;
            case (ButtonsToPress.quit):
                SetButtonAnimations(false, false, false, true);
                break;
        }
    }

    void SetButtonAnimations(bool playHighlighted, bool optionsHighlighted, bool tutoriallighted, bool exitHighlighted) {
        playButtonAnim.SetBool("Highlighted", playHighlighted);
        optionsButtonAnim.SetBool("Highlighted", optionsHighlighted);
        tutorialButtonAnim.SetBool("Highlighted", tutoriallighted);
        exitButtonAnim.SetBool("Highlighted", exitHighlighted);

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
            if(currentButton != ButtonsToPress.play) {
                currentButton--;
            }
        }
    }

    public void SelectButton(InputAction.CallbackContext context) {
        if (context.performed) {
            switch (currentButton) {
                case (ButtonsToPress.play):
                    PlayGame();
                    break;
                case (ButtonsToPress.options):
                    LoadOptions();
                    break;
                case (ButtonsToPress.tutorial):
                    LoadTutorial();
                    break;
                case (ButtonsToPress.quit):
                    QuitGame();
                    break;
            }
        }
    }


    public void PlayGame()
    {
        SceneManager.LoadScene(1);
    }

    public void LoadOptions() {
        Debug.Log("Loaded Options");
    }

    public void LoadTutorial() {
        Debug.Log("Loaded Tutorial");
    }

    public void QuitGame()
    {
        Application.Quit();
    }

}
