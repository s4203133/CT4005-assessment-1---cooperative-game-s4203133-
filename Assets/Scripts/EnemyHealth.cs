using UnityEngine;
using UnityEngine.UI;

public class EnemyHealth : MonoBehaviour
{
    public float health;
    private EnemyPatrol enemyPatrol;
    private Rigidbody rb;
    private GameObject objectHitBy;
    public ParticleSystem damageParticles;
    public ParticleSystem deathParticles;

    [Tooltip("The length at which a knock back affect will last when the enemy has been hit")]
    public float knockBackLength;
    public float knockBackMultiplyer;
    private float originalKnockBackMultiplyer;
    public float knockBackTimer;
    public Vector3 knockBackForce;

    [Header("Enemy Health Bar UI Variables")]
    //public Image healthBar;
    private Transform healthBarParent;
    private Vector2 healthBarScale;

    [Header("Power Up Spawning variables")]
    public GameObject powerUpObject;
    [Range(0f, 100f)]
    public int powerUpSpawnChance;
    private int powerUpSpawnChanceIndex;

    private CameraShake camShake;
    private PlayerManager playerManager;

    // Start is called before the first frame update
    void Start()
    {
        enemyPatrol = GetComponent<EnemyPatrol>();
        rb = GetComponent<Rigidbody>();
        //rb.isKinematic = true;
        originalKnockBackMultiplyer = knockBackMultiplyer;

        camShake = Camera.main.GetComponent<CameraShake>();
        playerManager = GameObject.FindGameObjectWithTag("PlayerManager").GetComponent<PlayerManager>();

        powerUpSpawnChanceIndex = Random.Range(0, 100);
        //healthBarScale = healthBar.rectTransform.localScale;
        //healthBarParent = healthBar.transform.parent;
    }

    // Update is called once per frame
    void Update()
    {
        if(health <= 0) {
            Death();
        }

        float healthBarScalar = 0;
        healthBarScalar = 50 / healthBarScale.x;
        //healthBar.rectTransform.localScale = new Vector2(Mathf.Lerp(healthBar.rectTransform.localScale.x, (float)health / healthBarScalar, 0.1f), healthBar.rectTransform.localScale.y);
        //healthBarParent.rotation = cam.transform.rotation;
    }

    private void FixedUpdate() {
        knockBackTimer -= Time.deltaTime;
        if (knockBackTimer > 0) {
            //rb.isKinematic = false;
            ApplyKnockBack();
        } else {
            //rb.isKinematic = true;
            rb.velocity = Vector3.zero;
            enemyPatrol.UnFreezeAgent();
            knockBackMultiplyer = originalKnockBackMultiplyer;
        }
    }

    private void Death() {
        ParticleSystem newDeathParticles = Instantiate(deathParticles, transform.position, Quaternion.identity);
        camShake.ShakeCamera(0.9f);
        if(objectHitBy.tag == "Player") {
            if(powerUpSpawnChanceIndex <= powerUpSpawnChance) {
                SpawnPowerUp();
            }
        }
        playerManager.numberOfEnemiesKilled++;
        GameObject.FindGameObjectWithTag("Enemy Spawner").GetComponent<EnemySpawner>().allEnemies.Remove(this.gameObject);
        Destroy(gameObject);
    }

    public void ApplyKnockBack() {
        rb.velocity = knockBackForce;
        knockBackMultiplyer *= 0.1f;
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
            KnockBackEnemy(other.transform.parent.GetComponent<Throwable>(), null, 0, false);
        }
    }

    public void KnockBackEnemy(Throwable thrownObject, PlayerController player, float invincibleTime, bool dealDamage) {
        //rb.isKinematic = false;
        rb.velocity *= 0.25f;
        //If the thrown object is a player, set them invincible so they can escape after being thrown
        if (player != null) {
            player.SetInvincibilityTimer(1.1f);
        }

        // Calculate angle to knock the enemy back
            knockBackForce = (transform.position - thrownObject.transform.position).normalized * knockBackMultiplyer;
            knockBackTimer = knockBackLength;
            if (dealDamage) {
                health -= thrownObject.damage;
                ParticleSystem newDamageParticles = Instantiate(damageParticles, transform.position, Quaternion.identity);
            }  
    }
}
