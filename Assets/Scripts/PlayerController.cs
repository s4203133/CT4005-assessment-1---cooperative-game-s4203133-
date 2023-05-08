using System.Collections;
using UnityEngine;
using UnityEngine.InputSystem;
using UnityEngine.InputSystem.UI;
using UnityEngine.SceneManagement;

public class PlayerController : MonoBehaviour {

    // Input variables
    private PlayerInput playerInput;
    private Vector2 moveInput;

    // Spawning Players Variables
    private PlayerManager playerManager;
    private MeshRenderer mesh;

    public enum playerMode {
        stationry, // Player is unable to do anything 
        move, // Player can move around and control as normal
        beingHeld, // Player is being held by another player and can no longer use controls
        beingThrown, // Player is being thrown and cannot use controls until they've landed
        onLadder, // When the player is climbing a ladder
    }

    public playerMode playerState;

    private bool canControl;

    [Header("Movement Variables")]
    public bool isBeingHeld;
    [SerializeField]
    private float acceleration;
    [SerializeField]
    private float maxSpeed;
    [SerializeField]
    private float slowedSpeed;
    private float originalMaxSpeed;
    [SerializeField]
    private float rotateSpeed;
    private Rigidbody rb;
    private PhysicMaterial physicMaterial;
    private bool isGrounded;
    public Transform groundCheck;
    public LayerMask groundLayers;

    [Header("Dash Variables")]
    [SerializeField]
    private float dashForce;
    [Tooltip("The duration of time in seconds that the dash will last")]
    [SerializeField]
    private float dashLength;
    [Tooltip("The duration of time the player has to wait before performing another dash")]
    [SerializeField]
    private float dashCoolDown;
    private bool isDashing;
    private bool canDash;
    private TrailRenderer trailRenderer;
    public bool hasDashed;

    [Header("Interaction Variables")]
    public string[] interactableLayer; // The objects on these layers can be interacted with
    public bool isHoldingItem;
    [Tooltip("The minimum and maximim amount of time in seconds to hold the button down to throw an object")]
    [SerializeField]
    private Vector2 minMaxHoldTime;
    private bool isButtonDown;
    public float interactInputTime; // How long the interact button has been held down

    private GameObject heldItem; // The item currently being held by the player
    public float maxThrowForce;
    public bool isInRangeOfObject;

    [Header("Blocking Variables")]
    public GameObject shield;
    private bool isBlocking;

    private bool isStrafing;

    [Header("Ladder Climbing Variables")]
    [SerializeField]
    private float climbSpeed;
    private bool isOnLadder;
    private bool isInRangeOfLadder;
    private GameObject theLadder;
    private Transform attatchPoint;

    [Header("KnockBack Variables")] // Used to apply knock back when hit by an enemy
    [Tooltip("The length at which a knock back affect will last when the enemy has been hit")]
    [SerializeField]
    private float knockBackLength;
    [SerializeField]
    private float knockBackMultiplyer;
    private float originalKnockBackMultiplyer;
    private float knockBackTimer;
    private Vector3 knockBackForce;

    [Header("Invincibility Varaibles")]
    public float invincibilityLength;
    private float invincibilityTimer;

    [Header("Explosion Power Up Variables")]
    public bool explodeOnLand;
    public float explosionRadius;
    public float explosionForce;
    public LayerMask enemyLayer;
    public ParticleSystem[] ExplosionParticles;
    public GameObject explosionLight;

    // Extra private script variables
    private Throwable throwable;
    private PlayerHealth health;
    private CameraShake cameraShake;
    private PauseMenu pauseMenu;

    private void Awake() {
        playerState = playerMode.move;
        playerManager = FindObjectOfType<PlayerManager>();
        mesh = GetComponent<MeshRenderer>();
        canControl = true;
        isBeingHeld = false;
        playerInput = new PlayerInput();
        rb = GetComponent<Rigidbody>();
        canDash = true;
        trailRenderer = GetComponent<TrailRenderer>();
        shield.SetActive(false);
        originalMaxSpeed = maxSpeed;
        originalKnockBackMultiplyer = knockBackMultiplyer;
        throwable = GetComponent<Throwable>();
        health = GetComponent<PlayerHealth>();
        rb.maxAngularVelocity = 15;
        physicMaterial = GetComponent<CapsuleCollider>().material;
        cameraShake = Camera.main.GetComponent<CameraShake>();
        pauseMenu = GameObject.FindGameObjectWithTag("Pause").GetComponent<PauseMenu>();
    }

    private void Start() {
        if(playerManager != null) {
            playerManager.numberOfPlayers++;
            name = "Player" + playerManager.numberOfPlayers;
            playerManager.players.Add(this.gameObject);
            mesh.material.color = playerManager.playerColors[playerManager.numberOfPlayers - 1];
            trailRenderer.material.color = playerManager.playerColors[playerManager.numberOfPlayers - 1];
            SetPlayerPosition(playerManager.playerPositionsLobby);
            switch (playerManager.currentLevel) {
                case (PlayerManager.level.Lobby):
                    break;
                case (PlayerManager.level.Level1):
                    SpawnPlayerInactive();
                    break;
                case (PlayerManager.level.Level2):
                    SpawnPlayerInactive();
                    break;
                case (PlayerManager.level.Level3):
                    SpawnPlayerInactive();
                    break;
            }
        }
    }

    public void ResetPlayerVariables() {
        canControl = true;
        isBeingHeld = false;
        canDash = true;
        health.SetHealthToMax();
        PlaceObjectOnFloor();
        playerState = playerMode.move;
    }

    void SetPlayerPosition(Vector3[] playerPositions) {
        transform.position = playerPositions[playerManager.numberOfPlayers - 1];
    }

    void SpawnPlayerInactive() {
        health.isDead = true;
        playerManager.ChangeNumberOfActivePlayers(-1);
    }

    void OnEnable() {
        EnableControls();
        playerManager.ChangeNumberOfActivePlayers(+1);
    }

    void EnableControls() {
        playerInput.Controller.Move.Enable();
        playerInput.Controller.Interact.Enable();
        playerInput.Controller.Dash.Enable();
        playerInput.Controller.Block.Enable();
        playerInput.Controller.Enable();
    }

    void OnDisable() {
        playerInput.Controller.Move.Disable();
        playerInput.Controller.Interact.Disable();
        playerInput.Controller.Dash.Disable();
        playerInput.Controller.Block.Disable();
        // playerInput.Controller.Strafe.Enable();
        playerManager.ChangeNumberOfActivePlayers(-1);
    }

    private void FixedUpdate() {
        PlayerControls();
        

        if (isDashing) {
            float dashTimer = 0;
            if (dashTimer < dashLength) {
                rb.AddForce(transform.forward * (dashForce * 10));
                dashTimer += Time.deltaTime;
            }
        }

        knockBackTimer -= Time.deltaTime;
        if (knockBackTimer > 0) {
            ApplyKnockBack();
        } else {
            knockBackMultiplyer = originalKnockBackMultiplyer;
        }

        // Rumble the controllers when holding the button down
        if (interactInputTime >= 0.98 && !health.isDead) {
            Gamepad.current.SetMotorSpeeds(0.015f, 0.015f);
        } else {
            Gamepad.current.SetMotorSpeeds(0f, 0f);
        }
    }

    void Update() {
        // If the interact button is down, increase a timer (used to determine how to drop an item when the button has been released: tap = drop, hold = throw)
        if (isButtonDown == true && interactInputTime < 1) {
            interactInputTime += Time.deltaTime;

        }
        if (interactInputTime > minMaxHoldTime.y) {
            interactInputTime = 1;
        }

        if (canDash) {
            playerInput.Controller.Dash.Enable();
        } else {
            playerInput.Controller.Dash.Disable();
        }

        if(isBlocking || isStrafing) {
            maxSpeed = slowedSpeed;
        }

        invincibilityTimer -= Time.deltaTime;

        if(playerState == playerMode.beingThrown) {
            physicMaterial.dynamicFriction = 0.5f;
        } else {
            physicMaterial.dynamicFriction = 0.1f;
        }

        isGrounded = Physics.CheckSphere(groundCheck.position, 0.1f, groundLayers);
    }


    public void OnMove(InputAction.CallbackContext context) {
        if (context.performed) {
            moveInput = context.ReadValue<Vector2>();

            if (throwable.hasHitSomething && playerState == playerMode.move) {
                rb.constraints = RigidbodyConstraints.FreezeRotation;
                rb.velocity = Vector3.zero;
                throwable.hasHitSomething = false;
            }

        } else if (context.canceled) {
            moveInput = Vector2.zero;
        }
    }

    public void StartDash(InputAction.CallbackContext context) {
        if (context.performed && !isDashing)
            StartCoroutine(Dash());
    }

    public void Interact(InputAction.CallbackContext context) {
        if (context.performed) {
            PickUp();
        } else if (context.canceled) {
            PutObjectDown();
        }
    }

    public void Block(InputAction.CallbackContext context) {
        if (context.performed) {
            if (!isHoldingItem) {
                RaiseShield();
            }
        } else if (context.canceled) {
            LowerShield();
        }
    }

    public void Strafe (InputAction.CallbackContext context) {
        if (context.performed) {
            isStrafing = true;
        } else if (context.canceled) {
            isStrafing = false;
            maxSpeed = originalMaxSpeed;
        }
    }

    public void PauseGame(InputAction.CallbackContext context) {
        if (context.performed) {
            if (pauseMenu.IsTheGamePaused()) {
                pauseMenu.ResumeGame();
            } else {
                pauseMenu.PauseTheGame();
            }
        }
    }
    public void ExitGame(InputAction.CallbackContext context) {
        if (context.performed) {
            if (pauseMenu.IsTheGamePaused()) {
                pauseMenu.ReturnToMenu();
            }
        }
    }

    public void DismountFromPlayer(InputAction.CallbackContext context) {
        if (context.performed) {
            if(playerState == playerMode.beingHeld) {
                PlayerController parentPlayer = transform.parent.GetComponent<PlayerController>();
                if(parentPlayer != null) {
                    parentPlayer.PlaceObjectOnFloor();
                }
            }
        }
    }

    public void AnyButtonPressed(InputAction.CallbackContext context) {
        if (playerManager.GetIsGameOver()) {
            if (context.performed) {
                SceneManager.LoadScene(0);
            }
        }
    }

    private void PlayerControls() {
        if (playerState == playerMode.move) {
            // Get a variable to be the difference between our current velocity and the max speed
            // When the player makes a greater change in direction, this number will be larger, appying more velocity to the rigidbody, creating more responsive movement
            float speedDifX = maxSpeed - rb.velocity.x;
            float speedDifZ = maxSpeed - rb.velocity.z;
            // Return speed difference variable if it's less than the max speed, else just use max speed
            float xVel = Mathf.Min(speedDifX * acceleration, maxSpeed);
            float zVel = Mathf.Min(speedDifZ * acceleration, maxSpeed);

            Vector3 move = new Vector3(moveInput.x * xVel * Time.deltaTime, rb.velocity.y, moveInput.y * zVel * Time.deltaTime);

            if (!isDashing) {
                rb.velocity = move;

                if (!isBlocking && !isStrafing) {
                    if (move.x != 0 && move.z != 0) {
                        Vector3 LookDirection = new Vector3(moveInput.x * xVel, 0, moveInput.y * zVel) * Time.deltaTime;
                        Quaternion targetRotation = Quaternion.LookRotation(LookDirection, Vector3.up);

                        transform.rotation = Quaternion.RotateTowards(transform.rotation, targetRotation, rotateSpeed * Time.deltaTime);
                    }
                }
            }
        }
        if (playerState == playerMode.onLadder) {
            // Booleans to check if another player is below or above us on the ladder
            bool isOtherPlayerAbove = false;
            bool isOtherPlayerBelow = false;
            // Fire 2 raycasts up and down of the player to check that there is a player above or below them on the ladder
            RaycastHit hit;
            Vector3 rayFirePos = new Vector3(transform.position.x, transform.position.y, transform.position.z);
            Ray upperCheck = new Ray(rayFirePos, transform.up);
            Ray LowerCheck = new Ray(rayFirePos, -transform.up);

            // Stop the player from being able to move up the ladder if there is another player above them
            if (Physics.Raycast(upperCheck, out hit, 1.15f)) {
                if(hit.collider.tag == "Player") {
                    isOtherPlayerAbove = true;
                } else {
                    isOtherPlayerAbove = false;
                }
            }
            // Stop the player from being able to move down the ladder if there is another player beneath them
            if (Physics.Raycast(LowerCheck, out hit, 1.15f)) {
                if (hit.collider.tag == "Player") {
                    isOtherPlayerBelow = true;
                } else {
                    isOtherPlayerBelow = false;
                }
            }

            // Check the upwards/downwards direction of the joystick on the game pad
            if (moveInput.y > 0.125 && !isOtherPlayerAbove) {
                rb.velocity = transform.up * climbSpeed;
            } else if (moveInput.y < -0.125 && !isOtherPlayerBelow) {
                rb.velocity = transform.up * -climbSpeed;
            }
            // Stops player from moving if joystick is close to the middle
            else {
                rb.velocity = Vector3.zero;
            }
            
        }
    }

    private IEnumerator Dash() {
        hasDashed = true;
        if (playerState != playerMode.move || !canDash || isStrafing) {
            yield break;
        }
        Vector3 move = new Vector3(moveInput.x, 0, moveInput.y);

        // Don't dash if the player is standing still or blocking with their shield
        if (moveInput.x != 0 && moveInput.y != 0 && !isBlocking) {
            isDashing = true;
            canDash = false;
            if (trailRenderer != null) {
                trailRenderer.emitting = true;
            }
            // Wait for the 'dashLength' time and stop the dashing affect
            yield return new WaitForSeconds(dashLength);
            isDashing = false;
            if (trailRenderer != null) {
                trailRenderer.emitting = false;
            }
            // After the cool down, allow the player to dash again
            float dashWaitTime = dashCoolDown - dashLength; // Subtract the dash length from the cool down length because the 'WaitForSeconds' above means we've already waited for a small period of time
            yield return new WaitForSeconds(dashWaitTime);
            canDash = true;
        }
    }

    public void PickUp() {
        if(isDashing || isBlocking) {
            return;
        }
        interactInputTime = 0;
        // If the player is currently not holding an item, pick one up if there's one in range.
        if (!isHoldingItem) {
            RaycastHit hit;
            float numberOfHorizontalChecks = 3;
            float numberOfVerticalChecks = 5;
            // Adjusted the i and j values so that they can be multiplied by a scalar to get a position to check for a ray cast from
            for(int i = -2; i < (-2 + numberOfHorizontalChecks); i++) {
                for(int j = -1; j < (-1 + numberOfVerticalChecks); j++) {
                    // Horizontal Check Positions: Players X position + -0.85, 0, 0.85
                    // Vertical Check positions: Players Y position + -1, -0.5, 0, 0.5,
                    Vector3 rayFirePos = new Vector3(transform.position.x + (0.85f * j), transform.position.y + (i * 0.5f), transform.position.z);

                    Ray pickUpObjectCheck = new Ray(rayFirePos, transform.forward);

                    // Check all of the raycasts, and if one hits an interactable item, pick it up
                    if (Physics.Raycast(pickUpObjectCheck, out hit, 3f)) {
                        // Loop through all the interactable layers and check if the objects tag matches with any of them (string value)
                        for (int x = 0; x < interactableLayer.Length; x++) {
                            if (hit.collider.tag == interactableLayer[x] && hit.transform.name != this.gameObject.name) {
                                PickUpObject(hit);
                                // End the loop
                                i = 100;
                                j = 100;
                                break;
                            } else {
                                continue;
                            }
                        }
                    }
                }
            }

            
        } 
        else {
            isButtonDown = true;
        }
    }

    void PickUpObject(RaycastHit hit) {
        heldItem = hit.collider.gameObject;
        PlayerController otherPlayer = heldItem.GetComponent<PlayerController>();
        if (otherPlayer != null) {
            otherPlayer.playerState = PlayerController.playerMode.beingHeld;
        }

        // Reset the hit ground variable in picked up objects throwable script
        Throwable otherThrowable = heldItem.GetComponent<Throwable>();
        otherThrowable.hasHitSomething = false;

        // Set the position of the picked up object above the player
        Vector3 pickUpPos = new Vector3(transform.position.x, (transform.position.y + transform.localScale.y) + (heldItem.transform.localScale.y + 0.25f), transform.position.z);
        heldItem.transform.position = pickUpPos;
        heldItem.transform.rotation = transform.rotation;
        //heldItem.transform.rotation = Quaternion.Euler(0, heldItem.transform.rotation.y, 0);
        heldItem.transform.parent = transform;

        // Reset Rigidbody values when picking item up
        ReturnRigidbodyValues(heldItem.GetComponent<Rigidbody>(), true, false);

        StartCoroutine(Wait(1f));
        isHoldingItem = true;
    }

    public void PutObjectDown() {
        if (isHoldingItem && isButtonDown) {
            // The script of the player being held
            PlayerController otherPlayer = heldItem.GetComponent<PlayerController>();

            // If the button is tapped
            if (interactInputTime < minMaxHoldTime.x) {
                PlaceObjectOnFloor();
            } 

            // THROW THE OBJECT 
            // If the button has been held down rather than tapped 
            else if (interactInputTime >= minMaxHoldTime.x) {

                if (otherPlayer != null) {
                    // If the player being held is also holding an object, don't throw them
                    if (otherPlayer.isHoldingItem) {
                        isButtonDown = false;
                        interactInputTime = 0;
                        return;
                    }

                    otherPlayer.isBeingHeld = false;
                    otherPlayer.playerState = PlayerController.playerMode.beingThrown;
                }
                // Unparent the held item object, and adjust variables in the damage script on it so it can be registered as thrown and harm enemies
                heldItem.transform.parent = null;
                Throwable thrownObject = heldItem.GetComponent<Throwable>();
                if (thrownObject != null) {
                    thrownObject.isBeingThrown = true;
                    thrownObject.damage *= interactInputTime;
                    thrownObject.thrownDirection = transform.forward;
                }

                // Return Rigidbody values when putting item down
                ReturnRigidbodyValues(heldItem.GetComponent<Rigidbody>(), false, true);
                // Throw the object
                ThrowObject(heldItem.GetComponent<Rigidbody>(), this.gameObject, true);

            }
            // Reset the held item variables
            heldItem = null;
            // Check if the player has a parent object, if so, see if it has a player controller script attatched to it
            PlayerController parentController = null;
            if (transform.parent != null) {
                parentController = transform.parent.GetComponent<PlayerController>();
            }
            // If parent object has a player controller, then we are being held by another player, so set player's state to being held
            if (parentController == null) {
                playerState = playerMode.move;
            }
            // If the player isn't being held, then set the current state to move
            else {
                playerState = playerMode.beingHeld;
            }
            isHoldingItem = false;
            if (otherPlayer != null) {
                //otherPlayer.isBeingHeld = false;
                //otherPlayer.ToggleCanControl(true);
            }
        }
        // Reset the input variables
        isButtonDown = false;
        interactInputTime = 0;

    }

    public void PlaceObjectOnFloor() {
        if (!isHoldingItem) {
            return;
        }
        // Unparent heldItem from the player and fire a raycast downwards from in front the player.
        heldItem.transform.parent = null;
        Vector3 putDownPos = heldItem.transform.position + transform.forward * 1.5f + transform.up * 1.1f;
        heldItem.transform.position = putDownPos;
        RaycastHit groundCheck;

        // If the raycast hits something, compare the distance between the object and the ground, and move the object down that distance, placing it correctly on the surface
        Ray ray = new Ray(heldItem.transform.position, Vector3.down);
        if (Physics.Raycast(ray, out groundCheck)) {
            float yOffset = heldItem.transform.localScale.y / 2;
            float putDownPosition = groundCheck.distance - yOffset;
            Vector3 groundPos = new Vector3(heldItem.transform.position.x, heldItem.transform.position.y - putDownPosition, heldItem.transform.position.z);
            heldItem.transform.position = groundPos;
            // Return the object Rigidbody values and player script when putting it down
            ReturnRigidbodyValues(heldItem.GetComponent<Rigidbody>(), false, true);
        }

        PlayerController otherPlayer = heldItem.GetComponent<PlayerController>();
        if(otherPlayer != null) {
            otherPlayer.playerState = PlayerController.playerMode.move;

        }
        heldItem = null;
        isHoldingItem = false;
    }

    void RaiseShield() {
        // Reduce the players move speed, and activate their shield object
        maxSpeed = slowedSpeed;
        shield.SetActive(true);
        isBlocking = true;
    }

    void LowerShield() {
        // Return the players move speed, and de-activate their shield object
        maxSpeed = originalMaxSpeed;
        shield.SetActive(false);
        isBlocking = false;
    }

    // Apply knockback force to the players rigid body, and decrease it over time to smooth it out
    public void ApplyKnockBack() {
        rb.velocity = knockBackForce;
        knockBackMultiplyer *= 0.1f;
    }

    // Simple wait function that waits for the specified time in seconds
    private IEnumerator Wait(float waitTime) {
        yield return new WaitForSeconds(waitTime);
    }

    void ReturnRigidbodyValues(Rigidbody rb, bool pickUp, bool putDown) {
        // When an item is being picked up
        if (pickUp) {
            rb.useGravity = false;
            rb.velocity = Vector3.zero;
            rb.rotation = Quaternion.Euler(0, 0, 0);
            rb.constraints = RigidbodyConstraints.FreezeRotation;
            rb.isKinematic = true;
        }
        // When an item is being put down
        else if (putDown) {
            Rigidbody heldItemRB = heldItem.GetComponent<Rigidbody>();
            heldItemRB.useGravity = true;
            //heldItemRB.rotation = Quaternion.Euler(0, 0, 0);
            heldItemRB.isKinematic = false;
            playerState = playerMode.move;
        } else {
            return;
        }
    }

    void ThrowObject(Rigidbody thrownObjectRB, GameObject playerThatsHolding, bool spinOnThrow) {
        float throwSustain = 0;
        if(interactInputTime < 0.5f) {
            throwSustain = 0.5f;
        } else {
            throwSustain = interactInputTime;
        }
        // Apply a forwards and downWards force on the object based on how long the button has been held down
        thrownObjectRB.constraints = RigidbodyConstraints.None;
        thrownObjectRB.velocity = (playerThatsHolding.transform.forward * maxThrowForce * throwSustain) + (-transform.up * (maxThrowForce / 4) * interactInputTime);

        // Option to add torque and spin the object when thrown
        if (spinOnThrow) {
            thrownObjectRB.AddTorque(Random.Range(0, 12), Random.Range(1, 2), Random.Range(-12, 12), ForceMode.Impulse);
        }
    }

    public void ToggleCanControl(bool trueOrFalse) {
        canControl = trueOrFalse;
    }

    void TriggerExplosion() {
        explodeOnLand = false;
        SpawnExplosionParticles();
        cameraShake.ShakeCamera(1f);
        Vector3 explosionPos = transform.position;
        Collider[] colliders = Physics.OverlapSphere(explosionPos, explosionRadius, enemyLayer);
        foreach (Collider hit in colliders) {
            EnemyHealth enemy = hit.GetComponent<EnemyHealth>();
            enemy.KnockBackEnemy(throwable, this, 0f, true);

        }
        GetComponent<PowerUp>().SetPowerUpActive(false);

    }

    void SpawnExplosionParticles() {
        foreach(ParticleSystem particle in ExplosionParticles) {
            ParticleSystem newExplosion = Instantiate(particle, transform.position, Quaternion.identity); 
        }
        GameObject explosionFlash = Instantiate(explosionLight, transform.position, Quaternion.identity);
    }

    private void OnCollisionEnter(Collision collision) {
        if(playerState == playerMode.beingThrown && explodeOnLand) {
            TriggerExplosion();
        }
        if (collision.gameObject.tag == "Ladder" && isInRangeOfLadder && isGrounded) {
            if (playerState == playerMode.move && !isDashing && !isHoldingItem && !isBlocking) {
                isInRangeOfLadder = false;
                rb.useGravity = false;
                rb.velocity = Vector3.zero;
                knockBackTimer = 0;
                playerState = playerMode.onLadder;
                theLadder = collision.gameObject;
                transform.position = attatchPoint.position;
                transform.rotation = theLadder.transform.rotation;
            }
        }
    }

    private void OnTriggerEnter(Collider other) {
        if(other.tag == "LevelStart") {
            playerManager.playersReadyToStart++;
        }
        if(other.tag == "LadderTrigger") {
            if (playerState != playerMode.onLadder) {
                // Set the attatch point to the entry transform on the ladder so the players position can be set correctly
                attatchPoint = other.transform.GetChild(0);
                isInRangeOfLadder = true;
            } else {
                playerState = playerMode.move;
                rb.useGravity = true;
                // Reset the players rotation based on the direction of the ladder
                if (transform.rotation.x != 0) {
                    transform.rotation = Quaternion.Euler(0, transform.rotation.y, transform.rotation.z);
                } else if(transform.rotation.z != 0) {
                    transform.rotation = Quaternion.Euler(transform.rotation.x, transform.rotation.y, 0);
                }
                // Set the players position to the exit point of the ladder
                Transform exitPoint = other.transform.GetChild(1);
                transform.position = exitPoint.position;
            }
        }

        if (other.tag == "Enemy") {
            if (playerState == playerMode.beingThrown && explodeOnLand) {
                TriggerExplosion();
            }

            if ((playerState != playerMode.beingThrown || !throwable.isBeingThrown) && invincibilityTimer < 0) {
                // If the player collides with an enemy, generate a knock back force 
                Vector3 knockBackAngle = new Vector3(transform.position.x, 0f, transform.position.z) - new Vector3(other.transform.position.x, 0f, other.transform.position.z);
                transform.rotation = Quaternion.Inverse(transform.rotation);
                PlaceObjectOnFloor();

                if (playerState == playerMode.onLadder) {
                    // If the player is on a ladder, change the knockback angle
                    knockBackAngle = transform.up * 1.125f;
                    transform.position += -transform.forward * 0.2f;

                    playerState = playerMode.move;
                    rb.useGravity = true;
                    // Reset the players rotation based on the direction of the ladder
                    if (transform.rotation.x != 0) {
                        transform.rotation = Quaternion.Euler(0, transform.rotation.y, transform.rotation.z);
                    } else if (transform.rotation.z != 0) {
                        transform.rotation = Quaternion.Euler(transform.rotation.x, transform.rotation.y, 0);
                    }
                }

                rb.isKinematic = false;
                knockBackForce = knockBackAngle * knockBackMultiplyer;

                // Subtract health from the player health Script
                if (invincibilityTimer < 0) {
                    health.SubtractHealth(20);
                }

                // Set timer for knock back length and invincibility frames (these are decreased by delta time in update, so effects are only applied while the timers are greater than 0)
                knockBackTimer = knockBackLength;
                SetInvincibilityTimer(invincibilityLength);
            }
        }
    }

    public void SetInvincibilityTimer(float length) {
        invincibilityTimer = length;
    }

    private void OnTriggerExit(Collider other) {
        if (other.tag == "LevelStart") {
            playerManager.playersReadyToStart--;
        }
        if (other.tag == "LadderTrigger") {
            isInRangeOfLadder = false;
        }
    }
}
