using UnityEngine;
using UnityEngine.UI;

public class EnemyHealth : MonoBehaviour
{
    public float health;
    private EnemyPatrol enemyPatrol;
    private Rigidbody rb;

    [Tooltip("The length at which a knock back affect will last when the enemy has been hit")]
    public float knockBackLength;
    public float knockBackMultiplyer;
    private float originalKnockBackMultiplyer;
    public float knockBackTimer;
    public Vector3 knockBackForce;

    [Header("Enemy Health Bar UI Variables")]
    public Image healthBar;
    private Transform healthBarParent;
    private Vector2 healthBarScale;

    private Camera cam;

    // Start is called before the first frame update
    void Start()
    {
        enemyPatrol = GetComponent<EnemyPatrol>();
        rb = GetComponent<Rigidbody>();
        rb.isKinematic = true;
        originalKnockBackMultiplyer = knockBackMultiplyer;

        cam = Camera.main;
        healthBarScale = healthBar.rectTransform.localScale;
        healthBarParent = healthBar.transform.parent;
    }

    // Update is called once per frame
    void Update()
    {
        if(health <= 0) {
            Death();
        }

        float healthBarScalar = 0;
        healthBarScalar = 50 / healthBarScale.x;
        healthBar.rectTransform.localScale = new Vector2(Mathf.Lerp(healthBar.rectTransform.localScale.x, (float)health / healthBarScalar, 0.1f), healthBar.rectTransform.localScale.y);
        healthBarParent.rotation = cam.transform.rotation;
    }

    private void FixedUpdate() {
        knockBackTimer -= Time.deltaTime;
        if (knockBackTimer > 0) {
            ApplyKnockBack();
        } else {
            rb.isKinematic = true;
            enemyPatrol.UnFreezeAgent();
            knockBackMultiplyer = originalKnockBackMultiplyer;
        }
    }

    private void Death() {
        Destroy(gameObject);
        FindObjectOfType<PlayerManager>().numberOfEnemiesKilled++;
    }

    public void ApplyKnockBack() {
        rb.velocity = knockBackForce;
        knockBackMultiplyer *= 0.1f;
        enemyPatrol.FreezeAgent();
    }
}
