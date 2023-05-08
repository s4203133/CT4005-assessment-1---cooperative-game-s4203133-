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

    public Mesh[] powerUpMeshes;
    public Material[] powerUpMateirals;

    private MeshFilter mFilter;
    private MeshRenderer mRenderer;

    private void Start() {
        mRenderer = GetComponent<MeshRenderer>();
    }

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
                mFilter.mesh = powerUpMeshes[0];
                mRenderer.material = powerUpMateirals[0];
                break;
            case (PickUpOptions.helmet):
                PickUpType = PickUpTypes.helmet;
                mFilter.mesh = powerUpMeshes[1];
                mRenderer.material = powerUpMateirals[1];
                break;
            case (PickUpOptions.bomb):
                PickUpType = PickUpTypes.bomb;
                mFilter.mesh = powerUpMeshes[2];
                mRenderer.material = powerUpMateirals[2];
                break;
            case (PickUpOptions.chicken):
                PickUpType = PickUpTypes.chicken;
                mFilter.mesh = powerUpMeshes[3];
                mRenderer.material = powerUpMateirals[3];
                break;
        }
    }
}
