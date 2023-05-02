using UnityEngine;
using UnityEngine.UI;

public class PlayerUI : MonoBehaviour {
    [Header("Health Bar Variables")]
    public Image healthBar;
    private Vector2 healthBarScale;

    [Header("Throwing Bar Variables")]
    /*public Image throwingBar;
    public Image throwingBarBackground;
    private float throwingBarWidth;
    private Color throwingBarColor;*/

    private PlayerController playerController;
    private PlayerHealth playerHealth;
    private PlayerManager playerManager; 
    private Camera cam;

    public GameObject directionalArrow;

    // Start is called before the first frame update
    void Start() {

        playerController = GetComponent<PlayerController>();
        playerHealth = GetComponent<PlayerHealth>();
        playerManager = FindObjectOfType<PlayerManager>();
        cam = Camera.main;

        Color playerColor = GetComponent<MeshRenderer>().material.color;
        directionalArrow.GetComponent<MeshRenderer>().material.color = playerColor;

        switch (transform.name) {
            case ("Player1"):
                healthBar = GameObject.Find("Player1HealthBar").GetComponent<Image>();
                break;
            case ("Player2"):
                healthBar = GameObject.Find("Player2HealthBar").GetComponent<Image>();
                break;
            case ("Player3"):
                healthBar = GameObject.Find("Player3HealthBar").GetComponent<Image>();
                break;
            case ("Player4"):
                healthBar = GameObject.Find("Player4HealthBar").GetComponent<Image>();
                break;
        }

        healthBarScale = healthBar.rectTransform.localScale;
        SetUIBarsVisible(true);
        //throwingBarWidth = throwingBar.rectTransform.localScale.x;
        //throwingBarColor = throwingBar.color;
    }

    // Update is called once per frame
    void Update() {
        if (playerManager.currentLevel == PlayerManager.level.Lobby) {
            //SetUIBarsVisible(false);
        } else {
            SetUIBarsVisible(true);
        }

        SetHealthUIBar();

        //SetThrowingUIBar();
        
        HandleDirectionArrow();
    }

    void SetUIBarsVisible(bool visible) {
        if (visible) {
            healthBar.enabled = true;
            //throwingBar.enabled = true;
            //throwingBarBackground.enabled = true;
        } else {
            healthBar.enabled = false;
            //throwingBar.enabled = false;
            //throwingBarBackground.enabled = false;
        }
    }

    void SetHealthUIBar() {

        if (playerHealth.isDead) {
            healthBar.enabled = false;
        } else {
            healthBar.enabled = true;
        }
        float healthBarScalar = 0;
        float playersHealth = playerHealth.GetHealth();

        healthBarScalar = (playersHealth * (100/playerHealth.maxHealth)) / 100;

        healthBar.rectTransform.localScale = new Vector2 (Mathf.Lerp(healthBar.rectTransform.localScale.x, healthBarScalar, 0.25f), healthBarScale.y);
    } 

    /*void SetThrowingUIBar() {
        if (playerController.interactInputTime > 0.2) {
            // Set the throw bar length to how long the throw button has been held down (up to 1 second)
            //throwingBar.rectTransform.localScale = new Vector2(playerController.interactInputTime * throwingBarWidth, throwingBar.rectTransform.localScale.y);
        } else {
            //throwingBar.rectTransform.localScale = new Vector2(0, throwingBar.rectTransform.localScale.y);
        }

        // If the health bar is full (or close to being full by a tiny margin), change it's color
        //if (throwingBar.rectTransform.localScale.x >= throwingBarWidth - float.Epsilon) {
            //throwingBar.color = Color.white;
        } else { 
            //throwingBar.color = throwingBarColor; // Restore color to original color

        }
    }*/

    void HandleDirectionArrow() {
        if (playerController.playerState == PlayerController.playerMode.move) {
            // Only Display the players directional arrow when they can move around
            directionalArrow.SetActive(true);
        } else {
            directionalArrow.SetActive(false);
        }
    }
}
