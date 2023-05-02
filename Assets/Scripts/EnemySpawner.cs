using JetBrains.Annotations;
using System.Collections;
using System.Collections.Generic;
using System.Runtime.CompilerServices;
using UnityEngine;

public class EnemySpawner : MonoBehaviour
{
    public enum spawnTypes{
        spawnSetNumberOfEnemies,
        spawnSetNumberOfWaves,
        spawnUntilTimeRunsOut
    }

    public spawnTypes spawnMode;

    [SerializeField]
    private GameObject[] spawnPoints;
    public List<GameObject> allEnemies;

    public GameObject enemyObject;
    public float spawnDelay;
    public float startDelay;
    public bool spawnInGroups;
    [Tooltip("The minimum and maximum amount of enemies to spawn in a group.\nThe values must be between 1 and 5\n(Set the 2 values the same if you want the amount that spawns in each group to stay consistent)")]
    public Vector2 groupSizeRange;
    public  int numberOfEnemiesSpawned; 
    [SerializeField]
    private int numberOfWavesSpawned;

    [Header("SpawnMode Variables")]
    [Tooltip("Assign either the number of enmies to spawn, waves to spawn, or the length of the timer per level in the corresponding indexes -\0 = Lobby \n1 = Level 1 \n2 = Level 2 \n3 = Level 3 \n4 = Boss")]
    public int[] enemiesToSpawnPerPlayer;
    private int numberOfEnemiesToSpawn;
    private int numberOfWavesToSpawn;
    private float timer;

    // Start is called before the first frame update
    void Start()
    {
        allEnemies = new List<GameObject>();
    }

    private void Update() {
        timer -= Time.deltaTime;

        if(groupSizeRange.x < 1) {
            groupSizeRange.x = 1;
        }
        if(groupSizeRange.y > 5) {
            groupSizeRange.y = 5;
        }
    }

    public void PrepareToSpawn(int level, int numberOfPlayers) {
        enemiesToSpawnPerPlayer[level] *= numberOfPlayers;
        numberOfEnemiesToSpawn = enemiesToSpawnPerPlayer[level];
        numberOfWavesToSpawn = enemiesToSpawnPerPlayer[level];
        timer = enemiesToSpawnPerPlayer[level];
    }

    public IEnumerator SpawnEnemies() {
        // Wait for a short delay before the spawning begins
        yield return new WaitForSeconds(startDelay);
        // Check which spawn mode is selected, and spawn the enemies accordingly
        switch (spawnMode) {
            case (spawnTypes.spawnSetNumberOfEnemies):
                for (int i = 0; i < numberOfEnemiesToSpawn; i++) {
                    if(numberOfEnemiesSpawned >= numberOfEnemiesToSpawn) {
                        yield break;
                    }
                    SpawnEnemy();
                    yield return new WaitForSeconds(spawnDelay);
                }
                break;
            case(spawnTypes.spawnSetNumberOfWaves):
                
                for (int i = 0; i < numberOfWavesToSpawn; i++) {
                    SpawnEnemy();
                    numberOfWavesSpawned++;
                    yield return new WaitForSeconds(spawnDelay);
                }
                break;
            case (spawnTypes.spawnUntilTimeRunsOut):
                // Used a large for loop because it needs to run until the timer runs out, and then break
                for (int i = 0; i < 100; i++) {
                    if (timer <= spawnDelay) {
                        yield break;
                    } else {
                        SpawnEnemy();
                        yield return new WaitForSeconds(spawnDelay);

                    }
                }
                break;
        }
    }

    void SpawnEnemy() {
        if (!spawnInGroups) {
            // Select one of the spawn points in the level at random to place the enemies
            int spawnPoint = Random.Range(0, spawnPoints.Length);
            GameObject newEnemy = Instantiate(enemyObject, spawnPoints[spawnPoint].transform.position, Quaternion.identity);
            numberOfEnemiesSpawned++;
            allEnemies.Add(newEnemy);
        } else {
            // Generate a random amount of enemies to spawn in one group
            int amountToSpawn = Random.Range((int)groupSizeRange.x, (int)groupSizeRange.y);
            int spawnPoint = Random.Range(0, spawnPoints.Length);
     
            for (int i = 0; i < amountToSpawn; i++) {
                // If the game has already spawned the desired amount of enemies, break out of the loop
                if(numberOfEnemiesSpawned >= numberOfEnemiesToSpawn) {
                    return;
                }
                GameObject newEnemy = Instantiate(enemyObject, spawnPoints[spawnPoint].transform.position, Quaternion.identity);
                numberOfEnemiesSpawned++;
                allEnemies.Add(newEnemy);
            }

        }

    }
}
