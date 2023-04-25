using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PowerUp : MonoBehaviour {
    private enum PowerUpTypes {
        health,
        helmet,
        bomb,
        chicken,
    }

    [SerializeField]
    private PowerUpTypes PowerUpType;

    [SerializeField]
    private float powerUpLength;
    private float timer;

    private bool powerUpActive;

    // Update is called once per frame
    void Update() {
        if (powerUpActive) {
            timer -= Time.deltaTime;
            if (timer <= 0) {
                timer = powerUpLength;
                powerUpActive = false;
            }
        }
    }

    private void OnTriggerEnter(Collider other) {
        if (other.tag == "Player") {
            PlayerController player = other.GetComponent<PlayerController>();
            switch (PowerUpType) {
                case (PowerUpTypes.health):
                    PlayerHealth health = other.GetComponent<PlayerHealth>();
                    health.IncreaseHealth(25);
                    Debug.Log("Increased Health");
                    break;
                case (PowerUpTypes.helmet):
                    ActivatePowerUp();
                    Debug.Log("Aquired Helmet");
                    break;
                case (PowerUpTypes.bomb):
                    ActivatePowerUp();
                    Debug.Log("Aquired Bomb");
                    break;
                case (PowerUpTypes.chicken):
                    ActivatePowerUp();
                    Debug.Log("Became a chicken");
                    break;
            }
        }
    }

    void ActivatePowerUp() {
        timer = powerUpLength;
        powerUpActive = true;
    }
}
