using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Tutorial : MonoBehaviour {
    public Vector3[] cameraPositions, room1PlayerPositions, room2PlayerPositions, room3PlayerPositions, room4PlayerPositions, room5PlayerPositions, pressurePadPositions;

    [SerializeField]
    [Tooltip("The amount of time a box has to be placed on the pad for the next room to start")]
    private float timeToOpenDoor;
    private float timer;

    private int currentRoom = 1;

    [SerializeField]
    private Camera cam;
    [SerializeField]
    private PlayerManager playerManager;

    // Start is called before the first frame update
    void Start() {
        timer = timeToOpenDoor;
    }

    // Update is called once per frame
    void Update() {
        if (timer < 0) {
            MoveToNextRoom(currentRoom);
        }
    }

    private void OnTriggerStay(Collider other) {
        if(other.tag == "PickUp") {
            timer -= Time.deltaTime;
        }
    }

    private void OnTriggerExit(Collider other) {
        if(other.tag == "PickUp") {
            timer = timeToOpenDoor;
        }
    }

    void MoveToNextRoom(int roomNumber) {
        timer = timeToOpenDoor;
        cam.transform.position = cameraPositions[roomNumber];
        transform.position = pressurePadPositions[roomNumber];
        switch (roomNumber) {
            case (0):
                SetPlayerPositions(room1PlayerPositions);
                break;
            case (1):
                SetPlayerPositions(room2PlayerPositions);
                break;
            case (2):
                SetPlayerPositions(room3PlayerPositions);
                break;
            case (3):
                SetPlayerPositions(room4PlayerPositions);
                break;
            case (4):
                SetPlayerPositions(room5PlayerPositions);
                break;
        }
        currentRoom++;

    }

    void SetPlayerPositions(Vector3[] newPositions) {
        for(int i = 0; i < playerManager.players.Count; i++) {
            playerManager.players[i].transform.position = newPositions[i];
        }
    }
}