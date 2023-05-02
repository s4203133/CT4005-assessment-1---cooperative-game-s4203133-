using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;

public class PickUp : MonoBehaviour {
    public enum PickUpOptions {
        health,
        helmet,
        bomb,
        chicken,
    }
    private enum PickUpTypes {
        health,
        helmet,
        bomb,
        chicken,
    }

    [SerializeField]
    private PickUpTypes PickUpType;

    private void OnTriggerEnter(Collider other) {
        if (other.tag == "Player") {
            PowerUp playerPowerUp = other.GetComponent<PowerUp>();
            switch (PickUpType) {
                case (PickUpTypes.health):
                    playerPowerUp.ActivatePowerUp(PowerUp.PowerUpTypes.health, this.gameObject);
                    break;
                case (PickUpTypes.helmet):
                    playerPowerUp.ActivatePowerUp(PowerUp.PowerUpTypes.helmet, this.gameObject);
                    break;
                case (PickUpTypes.bomb):
                    playerPowerUp.ActivatePowerUp(PowerUp.PowerUpTypes.bomb, this.gameObject);
                    break;
                case (PickUpTypes.chicken):
                    playerPowerUp.ActivatePowerUp(PowerUp.PowerUpTypes.chicken, this.gameObject);
                    break;
            }
        }
    }

    public void SetPickUpType(PickUpOptions type) {
        switch (type) {
            case (PickUpOptions.health):
                PickUpType = PickUpTypes.health;
                break;
            case (PickUpOptions.helmet):
                PickUpType = PickUpTypes.helmet;
                break;
            case (PickUpOptions.bomb):
                PickUpType = PickUpTypes.bomb;
                break;
            case (PickUpOptions.chicken):
                PickUpType = PickUpTypes.chicken;
                break;
        }
    }
}
