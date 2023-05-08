using System;
using System.Collections;
using UnityEditor;
using UnityEngine;

public class Throwable : MonoBehaviour
{
    public bool isBeingThrown;
    public bool hasHitSomething;
    public bool hitSpring;

    private PlayerController player;
    private PlayerManager playerManager;
    private PlayerHealth playerHealth;
    private Rigidbody rb;

    public float damage;
    private float originalDamage;
    public Vector3 thrownDirection;

    private CameraShake cameraShake;

    // Start is called before the first frame update
    void Start()
    {
        rb = GetComponent<Rigidbody>();
        player = GetComponent<PlayerController>();
        playerManager = GameObject.FindGameObjectWithTag("PlayerManager").GetComponent<PlayerManager>();
        playerHealth = GetComponent<PlayerHealth>();
        isBeingThrown = false;
        hasHitSomething = false;
        originalDamage = damage;
        cameraShake = Camera.main.GetComponent<CameraShake>();
    }

    private void Update() {
        /*if (rb.velocity.x < 3 && rb.velocity.z < 3 && hasHitGround) {
            isBeingThrown = false;
        }*/
    }

    private void OnCollisionEnter(Collision collision) {
        if (isBeingThrown && !hitSpring) {
            hasHitSomething = true;
            StartCoroutine(ReturnControls(0.85f));
        }
        isBeingThrown = false;
    }

    private IEnumerator ReturnControls(float timeDelay) {
        damage = originalDamage;

        // Check that this object has a player script, and re-enable it
        if (player != null) {
            yield return new WaitForSeconds(timeDelay);

            if (player.playerState == PlayerController.playerMode.beingHeld) {
                yield break;
            }

            //player.ToggleCanControl(true);
            player.playerState = PlayerController.playerMode.move;
        }
    }

    private void OnTriggerEnter(Collider other) {
        if(other.tag == "Catcher") {
            if (playerManager.currentLevel == PlayerManager.level.Lobby) {
                transform.position = new Vector3(0, 10, 0);
                rb.velocity = Vector3.zero;
                transform.rotation = Quaternion.Euler(Vector3.zero);
                rb.constraints = RigidbodyConstraints.FreezeRotation;
                return;
            } else {
                if(playerHealth != null && !playerHealth.isDead) {
                    playerHealth.DisablePlayer();
                }
                return;
            }
        }
        hasHitSomething = true;

        if (other.gameObject.layer == 6) {
            return;
        }

        // If the player hits something while being thrown
        if (isBeingThrown || (player != null && player.playerState == PlayerController.playerMode.beingThrown)) {
            if (other.tag == "Enemy") {
                rb.velocity *= 0.5f;
                float cameraShakeMagnitude = (damage * (100/originalDamage)) / 100;
                cameraShake.ShakeCamera(cameraShakeMagnitude);
                EnemyHealth hitEnemy = other.GetComponent<EnemyHealth>();
                hitEnemy.SetObjectHitBy(this.gameObject);
                hitEnemy.KnockBackEnemy(this, player, 1.1f, true);
            }

            if (other.tag == "Spring") {
                hitSpring = true;
                Spring spring = other.GetComponent<Spring>();
                if (spring != null) {
                    spring.LaunchPlayer(this.gameObject);
                    cameraShake.ShakeCamera(0.4f);
                }
            }


            if (!hitSpring) {
                StartCoroutine(ReturnControls(0.85f));
            }
        }
    }

    private void OnTriggerExit(Collider other) {
        if (other.tag == "Spring") {
            hitSpring = false;
        }
    }
}
