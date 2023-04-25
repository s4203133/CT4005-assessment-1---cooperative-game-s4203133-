using UnityEngine;
using UnityEngine.UI;

public class PlayerUI : MonoBehaviour {
    [Header("Health Bar Variables")]
    public Image healthBar;
    public Image healthBarBackground;
    private Transform uiBarParent;
    private Vector2 healthBarScale;

    [Header("Throwing Bar Variables")]
    public Image throwingBar;
    public Image throwingBarBackground;
    private float throwingBarWidth;
    private Color throwingBarColor;

    private PlayerController playerController;
    private PlayerHealth playerHealth;
    private PlayerManager playerManager; 
    private Camera cam;

    public GameObject directionalArrow;
    public Image a_Button;
    public Image pressButtonPrompt;
    public GameObject dashPrompt;
    private Animator pressButtonAnim;

    // The phases of the tutorial that the player has to learn
    private enum tutorialState {
        PickObjectUp, // Player starts on this state
        PutObjectDown,
        PickObjectBackUp,
        ThrowObject,
        Dash,
        Finished
    }

    private tutorialState playerLearningState;

    // Start is called before the first frame update
    void Start() {
        playerController = GetComponent<PlayerController>();
        playerHealth = GetComponent<PlayerHealth>();
        playerManager = FindObjectOfType<PlayerManager>();
        cam = Camera.main;

        healthBarScale = healthBar.rectTransform.localScale;
        uiBarParent = healthBar.transform.parent;
        throwingBarWidth = throwingBar.rectTransform.localScale.x;
        throwingBarColor = throwingBar.color;

        Color playerColor = GetComponent<MeshRenderer>().material.color;
        directionalArrow.GetComponent<MeshRenderer>().material.color = playerColor;

        playerLearningState = tutorialState.PickObjectUp;

        pressButtonAnim = pressButtonPrompt.GetComponent<Animator>();
    }

    // Update is called once per frame
    void Update() {
        if (playerManager.currentLevel == PlayerManager.level.Lobby) {
            SetUIBarsVisible(false);
        } else {
            SetUIBarsVisible(true);
        }

        SetHealthUIBar();

        SetThrowingUIBar();
        
        HandleDirectionArrow();

        CheckForObject();

        PromptToPutDownObject();

        PromptToThrowObject();

        PromptToDash();

        if(playerManager.currentLevel != PlayerManager.level.Lobby && playerLearningState != tutorialState.Finished) {
            FinishTutorial();
        }
    }

    void SetUIBarsVisible(bool visible) {
        if (visible) {
            healthBar.enabled = true;
            healthBarBackground.enabled = true;
            throwingBar.enabled = true;
            throwingBarBackground.enabled = true;
        } else {
            healthBar.enabled = false;
            healthBarBackground.enabled = false;
            throwingBar.enabled = false;
            throwingBarBackground.enabled = false;
        }
    }

    void SetHealthUIBar() {
        float healthBarScalar = 0;
        healthBarScalar = 100 / healthBarScale.x;
        healthBar.rectTransform.localScale = new Vector2(Mathf.Lerp(healthBar.rectTransform.localScale.x, (float)playerHealth.GetHealth() / healthBarScalar, 0.1f), healthBar.rectTransform.localScale.y);
        uiBarParent.rotation = cam.transform.rotation;
    } 

    void SetThrowingUIBar() {
        if (playerController.interactInputTime > 0.2) {
            // Set the throw bar length to how long the throw button has been held down (up to 1 second)
            throwingBar.rectTransform.localScale = new Vector2(playerController.interactInputTime * throwingBarWidth, throwingBar.rectTransform.localScale.y);
        } else {
            throwingBar.rectTransform.localScale = new Vector2(0, throwingBar.rectTransform.localScale.y);
        }

        // If the health bar is full (or close to being full by a tiny margin), change it's color
        if (throwingBar.rectTransform.localScale.x >= throwingBarWidth - float.Epsilon) {
            throwingBar.color = Color.white;
        } else { 
            throwingBar.color = throwingBarColor; // Restore color to original color

        }
    }

    void HandleDirectionArrow() {
        if (playerController.playerState == PlayerController.playerMode.move) {
            // Only Display the players directional arrow when they can move around
            directionalArrow.SetActive(true);
            // Use a raycast to find the disired positon on the surface in front of the player for the directional arrow to be placed
            RaycastHit hit;
            Vector3 rayFirePos = new Vector3(directionalArrow.transform.position.x, transform.position.y + 2, directionalArrow.transform.position.z);
            Ray floorCheck = new Ray(rayFirePos, Vector3.down);

            if (Physics.Raycast(floorCheck, out hit, 4f)) {
                // Snap the directinal arrow to a point just above where on the ground the raycast hit.
                Vector3 newPosition = new Vector3(hit.point.x, hit.point.y + 0.15f, hit.point.z); ;
                directionalArrow.transform.position = newPosition;
            }
        } else {
            directionalArrow.SetActive(false);
        }
    }

    // Check if an object is infront of the player that can be picked up, and display UI to help show the player what to do
    void CheckForObject() {
        if (playerLearningState == tutorialState.PickObjectUp || playerLearningState == tutorialState.PickObjectBackUp) {
            pressButtonPrompt.enabled = false;
            RaycastHit hit;
            // Do 3 checks in front of the player (one in middle, one on either side, facing forwards)
            for (int i = -1; i < 2; i++) {
                // Horizontal Check Positions at the players X position + (-0.85, 0, 0.85)
                Vector3 rayFirePos = new Vector3(transform.position.x + (0.85f * i), transform.position.y - 0.25f, transform.position.z);

                Ray pickUpObjectCheck = new Ray(rayFirePos, transform.forward);

                // If the raycast hit an object, check to see if it's a pick up-able object
                if (Physics.Raycast(pickUpObjectCheck, out hit, 3f)) {
                    // Loop through all the interactable layers and check if the objects tag matches with any ofthem  (string value)
                    if ((hit.collider.tag == "Player" || hit.collider.tag == ("PickUp")) && hit.transform.name != this.name) {
                        // Set the A button above the character visible to prompt the player to press the button
                        a_Button.enabled = true;
                        // End the loop
                        i = 100;
                        break;
                    } else {
                        a_Button.enabled = false;
                    }

                }
            }
        }
        if(playerLearningState == tutorialState.PickObjectUp && playerController.isHoldingItem) {
            playerLearningState = tutorialState.PutObjectDown;
        }
        if (playerLearningState == tutorialState.PickObjectBackUp && playerController.isHoldingItem) {
            playerLearningState = tutorialState.ThrowObject;
        }
    }

    void PromptToPutDownObject() {
        if(playerLearningState == tutorialState.PutObjectDown) {
            pressButtonPrompt.enabled = true;
            if (!playerController.isHoldingItem) {
                playerLearningState = tutorialState.PickObjectBackUp;
            }
        }
    }

    void PromptToThrowObject() {
        if (playerLearningState == tutorialState.ThrowObject) {
            pressButtonAnim.SetInteger("TutorialState", 2);
            pressButtonPrompt.enabled = true;
            if (!playerController.isHoldingItem) {
                pressButtonPrompt.enabled = false;
                a_Button.enabled = false;
                playerLearningState = tutorialState.Dash;
            }
        }
    }

    void PromptToDash() {
        if(playerLearningState == tutorialState.Dash) {
            dashPrompt.SetActive(true);

            if(playerController.hasDashed == true) {
                FinishTutorial();
            }
        }
    }

    public void FinishTutorial() {
        playerLearningState = tutorialState.Finished;
        dashPrompt.SetActive(false);
        pressButtonPrompt.enabled = false;
        a_Button.enabled = false;
    }
}
