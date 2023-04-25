using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerHealth : MonoBehaviour
{
    public int maxHealth;
    private int health;

    private PlayerManager playerManager; 
    // The area the player is sent to after they die, to keep them in the level before spawning them back in
    private GameObject playerWaitingZone;
    public bool isDead;

    // Start is called before the first frame update
    void Start()
    {
        health = maxHealth;
        playerManager = FindObjectOfType<PlayerManager>();
        playerWaitingZone = GameObject.Find("PlayerWaitingZone");
    }

    private void Update() {
        if(health <= 0) {
            DisablePlayer();
        }
    }

    public void SubtractHealth(int amountToTake) {
        health -= amountToTake;
    }

    public void IncreaseHealth(int amountToGive) {
        health += amountToGive;
    }

    public void SetHealthToMax() {
        health = maxHealth;
    }

    public int GetHealth() {
        return health;
    }

    void DisablePlayer() {
        transform.position = playerWaitingZone.transform.position;
        isDead = true;
        playerManager.ChangeNumberOfActivePlayers(-1);
        SetHealthToMax();
    }
}
