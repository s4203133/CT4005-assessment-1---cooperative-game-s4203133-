using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class PlayerManager : MonoBehaviour
{
    public enum level{ 
        Lobby,
        Level1,
        Level2,
        Level3,
        Boss
    }

    public level currentLevel;

    public int numberOfPlayers;
    public List <GameObject> players;
    public Color[] playerColors;
    private int numberOfActivePlayers;
    [Tooltip("The amount of players required to reach the end trigger to move onto the next level")]
    public int numberOfPlayersToProgress;

    public int numberOfEnemiesKilled;

    private PlayerInputManager inputManager;
    private EnemySpawner enemySpawner;
    private GameOver gameOver;
    private Camera cam;

    public int playersReadyToStart;
    private float levelStartCounter;
    [Tooltip("The time in seconds to start the level once all players are ready")]
    public float timeToStartLevel;

    [Header("Lobby Start Levels")]
    public Vector3 cameraPositionLobby;
    public Vector3[] playerPositionsLobby;
    public int playersToStartLevel1;
    [Header("Level 1 Start Variables")]
    public Vector3 cameraPositionLev1;
    public Vector3[] playerPositionsLev1;
    public int playersToStartLevel2;
    [Header("Level 2 Start Varaibles")]
    public Vector3 cameraPositionLev2;
    public Vector3[] playerPositionsLev2;
    public int playersToStartLevel3;
    [Header("Level 3 Start Variables")]
    public Vector3 cameraPositionLev3;
    public Vector3[] playerPositionsLev3;
    public int playersToStartBoss;
    [Header("Boss Room Start Variables")]
    public Vector3 cameraPositionBoss;
    public Vector3[] playerPositionsBoss;

    public bool helmetPowerUpInLevel, bombPowerUpInLevel, chickenPowerUpInLevel;

    public GameObject joinGamePrompt;
    public GameObject loadingNextLevelText;
    public Image loadingLevelBar;

    public Image blackOverlay;
    private bool loadingLevel;

    private bool isGameOver;

    // Start is called before the first frame update
    void Start()
    {
        currentLevel = level.Lobby;
        players = new List <GameObject>();
        inputManager = GetComponent<PlayerInputManager>();
        gameOver = GameObject.FindGameObjectWithTag("Finish").GetComponent<GameOver>();
        cam = Camera.main;
        enemySpawner = FindObjectOfType<EnemySpawner>();
        enemySpawner.PrepareToSpawn(0, numberOfPlayers);
        FadeOutBlackScreen();
    }

    private void Update() {
        if (numberOfActivePlayers == 0) {
            switch (currentLevel) {
                case (level.Level1):
                    gameOver.EndGame();
                    break;
                case (level.Level2):
                    gameOver.EndGame();
                    break;
                case (level.Level3):
                    gameOver.EndGame();
                    break;
                case (level.Boss):

                    break;
            }
        }

        if((playersReadyToStart == numberOfPlayersToProgress) && players.Count > 0 && !loadingLevel) {
            if(numberOfEnemiesKilled == enemySpawner.numberOfEnemiesSpawned) {
                levelStartCounter += Time.deltaTime;
                loadingNextLevelText.SetActive(true);
                loadingLevelBar.rectTransform.localScale = new Vector2(levelStartCounter * (100 / timeToStartLevel) / 100, loadingLevelBar.rectTransform.localScale.y);
                loadingLevelBar.enabled = true;
            }
        } else {
            levelStartCounter = 0;
            loadingNextLevelText.SetActive(false);
            loadingLevelBar.enabled = false;
            loadingLevelBar.rectTransform.localScale = new Vector2(0, loadingLevelBar.rectTransform.localScale.y);
        }

        // If in the lobby set number of players to start level the amount of players
        if (currentLevel == level.Lobby) {
            numberOfPlayersToProgress = players.Count;
            if(players.Count < 4) {
                joinGamePrompt.SetActive(true);
            } else {
                joinGamePrompt.SetActive(false);
            }
        } // Otherwise set the amount of players required to start next level the amount of players still alive 
        else {
            numberOfPlayersToProgress = numberOfActivePlayers;
        }

        if(levelStartCounter > timeToStartLevel && numberOfEnemiesKilled == enemySpawner.numberOfEnemiesSpawned) {
            switch (currentLevel) {
                case (level.Lobby):
                    joinGamePrompt.SetActive(false);
                    StartCoroutine(ChangeLevel(cameraPositionLev1, playerPositionsLev1));
                    numberOfPlayersToProgress = playersToStartLevel1;
                    currentLevel = level.Level1;
                    enemySpawner.PrepareToSpawn(1, numberOfPlayers);
                    StartCoroutine(enemySpawner.SpawnEnemies());
                    break;
                case (level.Level1):
                    StopCoroutine(enemySpawner.SpawnEnemies());
                    StartCoroutine(ChangeLevel(cameraPositionLev2, playerPositionsLev2));
                    numberOfPlayersToProgress = playersToStartLevel2;
                    currentLevel = level.Level2;
                    enemySpawner.PrepareToSpawn(2, numberOfPlayers);
                    StartCoroutine(enemySpawner.SpawnEnemies());
                    break;
                case (level.Level2):
                    StopCoroutine(enemySpawner.SpawnEnemies());
                    StartCoroutine(ChangeLevel(cameraPositionLev3, playerPositionsLev3));
                    numberOfPlayersToProgress = playersToStartLevel3;
                    currentLevel = level.Level3;
                    enemySpawner.PrepareToSpawn(3, numberOfPlayers);
                    StartCoroutine(enemySpawner.SpawnEnemies());
                    break;
                case (level.Level3):
                    StopCoroutine(enemySpawner.SpawnEnemies());
                    StartCoroutine(ChangeLevel(cameraPositionBoss, playerPositionsBoss));
                    numberOfPlayersToProgress = playersToStartBoss;
                    currentLevel = level.Boss;
                    enemySpawner.PrepareToSpawn(4, numberOfPlayers);
                    StartCoroutine(enemySpawner.SpawnEnemies());
                    break;
                case (level.Boss):
                    StartCoroutine(ChangeLevel(cameraPositionLobby, playerPositionsLobby));
                    currentLevel = level.Lobby;
                    joinGamePrompt.SetActive(true);
                    break;
            }
        }

        if(inputManager != null) {
            if(numberOfPlayers < players.Count - 1) {
                inputManager.enabled = true;
            }
        }
    }

    private IEnumerator ChangeLevel(Vector3 cameraPosition, Vector3[] playerPositions) {
        loadingLevel = true;
        blackOverlay.CrossFadeColor(Color.black, 1f, false, true);
        yield return new WaitForSeconds(1f);
        cam.transform.position = cameraPosition;
        numberOfEnemiesKilled = 0;
        for (int i = 0; i < numberOfPlayers; i++) {
            players[i].GetComponent<PlayerController>().ResetPlayerVariables();
            PlayerHealth playerHealth = players[i].GetComponent<PlayerHealth>();
            players[i].transform.position = playerPositions[i];
            players[i].SetActive(true);
            if (playerHealth.isDead) {
                playerHealth.isDead = false;
                ChangeNumberOfActivePlayers(1);
                playerHealth.SetHealthToMax();
            }
        }
        levelStartCounter = 0;
        loadingLevel = false;
        FadeOutBlackScreen();
    }

    public void FadeInBlackScreen() {
        blackOverlay.CrossFadeColor(Color.blue, 1f, false, true);
    }

    public void FadeOutBlackScreen() {
        blackOverlay.CrossFadeColor(Color.clear, 1f, false, true);
    }

    public void ChangeNumberOfActivePlayers(int amount) {
        numberOfActivePlayers += amount;
    }

    public int GetNumberOfActivePlayers() {
        return numberOfActivePlayers;
    }

    public bool GetIsGameOver() {
        return isGameOver;
    }

    public void SetIsGameOver(bool value) {
        isGameOver = value;
    }
}
