using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.InputSystem;
using UnityEngine.SceneManagement;
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

    // Start is called before the first frame update
    void Start()
    {
        currentLevel = level.Lobby;
        players = new List <GameObject>();
        inputManager = GetComponent<PlayerInputManager>();
        cam = Camera.main;
        enemySpawner = FindObjectOfType<EnemySpawner>();
        enemySpawner.PrepareToSpawn(0);
    }

    private void Update() {
        if((playersReadyToStart == numberOfPlayersToProgress) && players.Count > 0) {
            levelStartCounter += Time.deltaTime;
        } else {
            levelStartCounter = 0;
        }

        if(currentLevel == level.Lobby) {
            numberOfPlayersToProgress = players.Count;
        } else {
            numberOfPlayersToProgress = numberOfActivePlayers;
        }

        if(levelStartCounter > timeToStartLevel && numberOfEnemiesKilled == enemySpawner.numberOfEnemiesSpawned) {
            /*for(int i = 0; i < numberOfPlayers; i++) {
                players[i].SetActive(false);
            }*/
            switch (currentLevel) {
                case (level.Lobby):
                    ChangeLevel(cameraPositionLev1, playerPositionsLev1);
                    numberOfPlayersToProgress = playersToStartLevel1;
                    currentLevel = level.Level1;
                    enemySpawner.PrepareToSpawn(1);
                    StartCoroutine(enemySpawner.SpawnEnemies());
                    break;
                case (level.Level1):
                    ChangeLevel(cameraPositionLev2, playerPositionsLev2);
                    numberOfPlayersToProgress = playersToStartLevel2;
                    currentLevel = level.Level2;
                    enemySpawner.PrepareToSpawn(2);
                    StartCoroutine(enemySpawner.SpawnEnemies());
                    break;
                case (level.Level2):
                    ChangeLevel(cameraPositionLev3, playerPositionsLev3);
                    numberOfPlayersToProgress = playersToStartLevel3;
                    currentLevel = level.Level3;
                    enemySpawner.PrepareToSpawn(3);
                    StartCoroutine(enemySpawner.SpawnEnemies());
                    break;
                case (level.Level3):
                    ChangeLevel(cameraPositionBoss, playerPositionsBoss);
                    numberOfPlayersToProgress = playersToStartBoss;
                    currentLevel = level.Boss;
                    enemySpawner.PrepareToSpawn(4);
                    StartCoroutine(enemySpawner.SpawnEnemies());
                    break;
                case (level.Boss):
                    ChangeLevel(cameraPositionLobby, playerPositionsLobby);
                    currentLevel = level.Lobby;
                    enemySpawner.PrepareToSpawn(5);
                    StartCoroutine(enemySpawner.SpawnEnemies());
                    break;
            }
            levelStartCounter = 0;
        }

        if(inputManager != null) {
            if(numberOfPlayers < players.Count - 1) {
                inputManager.enabled = true;
            }
        }
    }

    private void ChangeLevel(Vector3 cameraPosition, Vector3[] playerPositions) {
        cam.transform.position = cameraPosition;
        for (int i = 0; i < numberOfPlayers; i++) {
            players[i].GetComponent<PlayerController>().ResetPlayerVariables();
            players[i].transform.position = playerPositions[i];
            players[i].SetActive(true);
        }
    }

    public void ChangeNumberOfActivePlayers(int amount) {
        numberOfActivePlayers += amount;
    }

    public int GetNumberOfActivePlayers() {
        return numberOfActivePlayers;
    }
}
