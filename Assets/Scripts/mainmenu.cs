using System.Collections;
using UnityEngine;
using UnityEngine.InputSystem;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class mainmenu : MonoBehaviour
{
    private PlayerInput mainMenuInput;

    [Header("Menu Screens")]
    [SerializeField] private Canvas mainMenu;
    [SerializeField] private Canvas optionsMenu;

    [Header("Individual UI Elements")]
    public Button playButton;
    public Animator playButtonAnim;

    public Button optionsButton;
    public Animator optionsButtonAnim;

    public Button tutorialButton;
    public Animator tutorialButtonAnim;

    public Button exitButton;
    public Animator exitButtonAnim;

    public Image blackOverlay;

    private enum UIScreens {
        mainMenuScreen,
        optionsMenuScreen
    }

    private UIScreens currentScreen;

    private enum MainMenuButtonsToPress {
        play,
        options,
        tutorial,
        quit
    }

    private enum OptionMenuButtonsToPress {
        volume,
        back
    }

    private MainMenuButtonsToPress currentMenuButton;
    private OptionMenuButtonsToPress currentOptionsButton;

    void Start()
    {
        Cursor.visible = false;
        Cursor.lockState = CursorLockMode.Locked;
        mainMenuInput = new PlayerInput();

        mainMenuInput.Enable();
        currentScreen = UIScreens.mainMenuScreen;
        currentMenuButton = MainMenuButtonsToPress.play;
        SetMainMenuButtonAnimations(true, false, false, false);

        FadeOutBlackScreen();
    }

    private void Update() {
        if (currentScreen == UIScreens.mainMenuScreen) {
            switch (currentMenuButton) {
                case (MainMenuButtonsToPress.play):
                    SetMainMenuButtonAnimations(true, false, false, false);
                    break;
                case (MainMenuButtonsToPress.options):
                    SetMainMenuButtonAnimations(false, true, false, false);
                    break;
                case (MainMenuButtonsToPress.tutorial):
                    SetMainMenuButtonAnimations(false, false, true, false);
                    break;
                case (MainMenuButtonsToPress.quit):
                    SetMainMenuButtonAnimations(false, false, false, true);
                    break;
            }
        } else if (currentScreen == UIScreens.optionsMenuScreen) {
            switch (currentOptionsButton) {
                case (OptionMenuButtonsToPress.back):
                    // Set animations
                    break;
                case (OptionMenuButtonsToPress.volume):
                    // Set animations
                    break;
            }
        }
    }

    void SetMainMenuButtonAnimations(bool playHighlighted, bool optionsHighlighted, bool tutoriallighted, bool exitHighlighted) {
        playButtonAnim.SetBool("Highlighted", playHighlighted);
        optionsButtonAnim.SetBool("Highlighted", optionsHighlighted);
        tutorialButtonAnim.SetBool("Highlighted", tutoriallighted);
        exitButtonAnim.SetBool("Highlighted", exitHighlighted);
    }

    public void MoveDown(InputAction.CallbackContext context) {
        if (context.performed) {
            if (currentMenuButton != MainMenuButtonsToPress.quit) {
                currentMenuButton++;
            }
        }
    }

    public void MoveUp(InputAction.CallbackContext context) {
        if (context.performed) {
            if(currentMenuButton != MainMenuButtonsToPress.play) {
                currentMenuButton--;
            }
        }
    }

    public void SelectButton(InputAction.CallbackContext context) {
        if (context.performed) {

            if (currentScreen == UIScreens.mainMenuScreen) {
                switch (currentMenuButton) {
                    case (MainMenuButtonsToPress.play):
                        PlayGame();
                        break;
                    case (MainMenuButtonsToPress.options):
                        LoadOptions();
                        break;
                    case (MainMenuButtonsToPress.tutorial):
                        LoadTutorial();
                        break;
                    case (MainMenuButtonsToPress.quit):
                        QuitGame();
                        break;
                }
            } else if (currentScreen == UIScreens.optionsMenuScreen) {
                switch (currentOptionsButton) {
                    case (OptionMenuButtonsToPress.back):
                        ReturnToMenu();
                        break;
                    case (OptionMenuButtonsToPress.volume):
                        // Set animations
                        break;
                }
            }
        }
    }

    public void Back(InputAction.CallbackContext context) {
        if (context.performed) {
            if(currentScreen == UIScreens.optionsMenuScreen) {
                ReturnToMenu();
            }
        }
    }


    public void PlayGame()
    {
        StartCoroutine(LoadLevel(1));
    }

    public void LoadOptions() {
        mainMenu.enabled = false;
        optionsMenu.enabled = true;
        currentScreen = UIScreens.optionsMenuScreen;
        currentOptionsButton = OptionMenuButtonsToPress.back;
    }

    public void ReturnToMenu() {
        mainMenu.enabled = true;
        optionsMenu.enabled = false;
        currentScreen = UIScreens.mainMenuScreen;
        currentMenuButton = MainMenuButtonsToPress.options;
        SetMainMenuButtonAnimations(false, true, false, false);
    }

    public void LoadTutorial() {
        StartCoroutine(LoadLevel(2));
    }

    public void QuitGame()
    {
        Application.Quit();
    }

    private IEnumerator LoadLevel(int index) {
        FadeInBlackScreen();
        yield return new WaitForSeconds(1f);
        SceneManager.LoadScene(index);
    }

    public void FadeInBlackScreen() {
        blackOverlay.CrossFadeColor(Color.blue, 1f, false, true);
    }

    public void FadeOutBlackScreen() {
        blackOverlay.CrossFadeColor(Color.clear, 1f, false, true);
    }
}
