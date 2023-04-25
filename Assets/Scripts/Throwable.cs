using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Throwable : MonoBehaviour
{
    public bool isBeingThrown;
    public bool hasHitSomething;
    public bool hitSpring;

    private PlayerController player;
    private Rigidbody rb;

    public float damage;
    private float originalDamage;
    public Vector3 thrownDirection;

    int numberOfGroundHits;

    // Start is called before the first frame update
    void Start()
    {
        rb = GetComponent<Rigidbody>();
        player = GetComponent<PlayerController>();
        isBeingThrown = false;
        hasHitSomething = false;
        originalDamage = damage;
    }

    private void Update() {
        /*if (rb.velocity.x < 3 && rb.velocity.z < 3 && hasHitGround) {
            isBeingThrown = false;
        }*/
    }

    private void OnCollisionEnter(Collision collision) {
        numberOfGroundHits++;
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
            transform.position = new Vector3(0, 10, 0);
            rb.velocity = Vector3.zero;
            transform.rotation = Quaternion.Euler(Vector3.zero);
            rb.constraints = RigidbodyConstraints.FreezeRotation;
            return;
        }
        hasHitSomething = true;

        if (other.gameObject.layer == 6) {
            return;
        }

        if (isBeingThrown || (player != null && player.playerState == PlayerController.playerMode.beingThrown)) {
            if (other.tag == "Enemy") {
                EnemyHealth enemy = other.GetComponent<EnemyHealth>();
                rb.velocity *= 0.25f;

                if(player!= null) {
                    player.SetInvincibilityTimer(1.1f);
                }
                if (enemy.knockBackTimer < 0) {
                    enemy.knockBackForce = thrownDirection * enemy.knockBackMultiplyer;
                    enemy.GetComponent<Rigidbody>().isKinematic = false;
                    enemy.knockBackTimer = enemy.knockBackLength;
                    enemy.health -= damage;
                }
            }

            if (other.tag == "Spring") {
                hitSpring = true;
                Spring spring = other.GetComponent<Spring>();
                if (spring != null) {
                    spring.LaunchPlayer(this.gameObject);
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
