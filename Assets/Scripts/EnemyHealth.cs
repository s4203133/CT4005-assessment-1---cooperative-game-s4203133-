using System.Collections;
using UnityEngine;
using UnityEngine.AI;
using UnityEngine.UI;

public class EnemyHealth : MonoBehaviour
{
    public float health;
    [SerializeField]
    private EnemyPatrol enemyPatrol;
    [SerializeField]
    private Rigidbody rb;
    [SerializeField]
    private NavMeshAgent agent;
    [SerializeField]
    private CapsuleCollider enemyCollider;

    private GameObject objectHitBy;
    [SerializeField]
    private ParticleSystem damageParticles;
    [SerializeField]
    private ParticleSystem deathParticles;
    [SerializeField]
    private bool oneHitKill;

    public bool isDead = false;
    [SerializeField]
    [Tooltip("The time in seconds for the enemy to despawn after dying")]
    private float despawnTime;

    private Coroutine deathCountdown;

    [Tooltip("The length at which a knock back affect will last when the enemy has been hit")]
    public float knockBackLength;
    public Vector2 knockBackMultiplyerRange;
    public float knockBackTimer;
    public Vector3 knockBackForce;

    [Header("Power Up Spawning variables")]
    public GameObject powerUpObject;
    [Range(0f, 100f)]
    public int powerUpSpawnChance;
    private int powerUpSpawnChanceIndex;

    [SerializeField]
    private CameraShake camShake;
    [SerializeField]
    private PlayerManager playerManager;
    [SerializeField]
    private PauseMenu pauseMenu;
    private Vector3 velocityToReturn; // The velocity of the object before the game was paused
                                      // to apply back on the rigidbody when the game resumes

    void Start()
    {
        powerUpSpawnChanceIndex = Random.Range(0, 100);
        rb.isKinematic = true;
    }

    private void FixedUpdate() {
        //knockBackTimer -= Time.deltaTime;
        //if (knockBackTimer > 0) {
            //rb.isKinematic = false;
            //ApplyKnockBack();
        //} else {
            //rb.isKinematic = true;
            //rb.velocity = Vector3.zero;
            //enemyPatrol.UnFreezeAgent();
            //knockBackMultiplyer = originalKnockBackMultiplyer;
        //}
    }

    private IEnumerator Death(float delay) {
        yield return new WaitForSeconds(delay);
        if (pauseMenu.IsTheGamePaused()) {
            yield break;
        }
        ParticleSystem newDeathParticles = Instantiate(deathParticles, transform.position, Quaternion.identity);
        if(objectHitBy != null && objectHitBy.tag == "Player") {
            if(powerUpSpawnChanceIndex <= powerUpSpawnChance) {
                SpawnPowerUp();
            }
        }
        GameObject.FindGameObjectWithTag("Enemy Spawner").GetComponent<EnemySpawner>().allEnemies.Remove(this.gameObject);
        Destroy(gameObject);
    }

    public void ApplyKnockBack() {
        rb.velocity = knockBackForce;
        enemyPatrol.FreezeAgent();
    }

    public void SetObjectHitBy(GameObject theObject) {
        objectHitBy = theObject;
    }

    public void SpawnPowerUp() {
        bool canSpawnPowerUp = false;
        int powerUpToSpawn = 0;
        do {
            powerUpToSpawn = Random.Range(0, 100);
            if(powerUpToSpawn <= 25) {
                canSpawnPowerUp = true;
            } // If the type of power up to spawn already has an instance in the level, choose a different one
            // (condition is not true for health power ups to allow more of them)
            else if (powerUpToSpawn > 25 && powerUpToSpawn <= 50 && !playerManager.helmetPowerUpInLevel) {
                canSpawnPowerUp = true;
            } else if (powerUpToSpawn > 50 && powerUpToSpawn <= 75 && !playerManager.bombPowerUpInLevel) {
                canSpawnPowerUp = true;
            } else if(powerUpToSpawn > 75 && !playerManager.chickenPowerUpInLevel) {
                canSpawnPowerUp = true;
            }
        }
        while (canSpawnPowerUp == false);


        if (powerUpToSpawn <= 25) {
            GameObject newPowerUp = Instantiate(powerUpObject, transform.position, Quaternion.identity);
            newPowerUp.GetComponent<PickUp>().SetPickUpType(PickUp.PickUpOptions.health);
        } else if (powerUpToSpawn > 25 && powerUpToSpawn <= 50) {
            GameObject newPowerUp = Instantiate(powerUpObject, transform.position, Quaternion.identity);
            newPowerUp.GetComponent<PickUp>().SetPickUpType(PickUp.PickUpOptions.helmet);
        } else if (powerUpToSpawn > 50 && powerUpToSpawn <= 75) {
            GameObject newPowerUp = Instantiate(powerUpObject, transform.position, Quaternion.identity);
            newPowerUp.GetComponent<PickUp>().SetPickUpType(PickUp.PickUpOptions.bomb);
        } else {
            GameObject newPowerUp = Instantiate(powerUpObject, transform.position, Quaternion.identity);
            newPowerUp.GetComponent<PickUp>().SetPickUpType(PickUp.PickUpOptions.chicken);
        }

    }

    private void OnTriggerEnter(Collider other) {
        if (other.tag == "PlayerShield") {
            KnockBackEnemy(other.transform.parent.GetComponent<Throwable>(), false);
        }
    }

    public void KnockBackEnemy(Throwable thrownObject, bool dealDamage) {
        if (dealDamage) {
            enemyCollider.isTrigger = false;
            ParticleSystem newDamageParticles = Instantiate(damageParticles, transform.position, Quaternion.identity);
            enemyPatrol.enabled = false;
            rb.isKinematic = false;
            agent.enabled = false;
            transform.position += (Vector3.up * 2);
            knockBackForce = (thrownObject.thrownDirection * Random.Range(knockBackMultiplyerRange.x, knockBackMultiplyerRange.y)) + (Vector3.up * 50);
            rb.AddForce(knockBackForce, ForceMode.Impulse);
            rb.AddTorque(knockBackForce + (-transform.forward * Random.Range(20, 60)), ForceMode.Impulse);
            isDead = true;
            playerManager.numberOfEnemiesKilled++;
            StartDeathCountdown();
        }
    }

    public void StartDeathCountdown() {
        deathCountdown = StartCoroutine(Death(despawnTime));
    }

    public void CancelDeath() {
        StopCoroutine(deathCountdown);
    }

    public void PausePhysics() {
        velocityToReturn = rb.velocity;
        rb.isKinematic = true;
    }

    public void ResumePhysics() {
        rb.isKinematic = false;
        rb.velocity = velocityToReturn;
    }
}
