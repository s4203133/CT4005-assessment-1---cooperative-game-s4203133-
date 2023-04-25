using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;
using UnityEngine.AI;

public class EnemyPatrol : MonoBehaviour
{
    public GameObject target;
    public PlayerController[] players;

    [Tooltip("A minimum and maximum range for generating a random move speed for the enemy")]
    public Vector2 minMaxSpeedRange;
    private NavMeshAgent agent;
    private float originalSpeed;

    // Start is called before the first frame update
    void Start() {
        players = FindObjectsOfType<PlayerController>();

        agent = GetComponent<NavMeshAgent>();
        originalSpeed = Random.Range(minMaxSpeedRange.x, minMaxSpeedRange.y);
        agent.speed = originalSpeed;

        if (players.Length > 0) {
            int randomPlayer = Random.Range(0, players.Length);
            target = players[randomPlayer].gameObject;
        }
    }

    // Update is called once per frame
    void Update()
    {
        if(target != null) {
            agent.destination = target.transform.position;
            transform.LookAt(new Vector3(target.transform.position.x, transform.position.y, target.transform.position.z));

            if (!target.activeInHierarchy || target.GetComponent<PlayerHealth>().isDead) {
                PlayerManager playerManager = FindObjectOfType<PlayerManager>();
                if(playerManager.GetNumberOfActivePlayers() <= 0) {
                    agent.isStopped = true;
                    Debug.Log(name + " couldn't find any active players in the scene!");
                } else {
                    int randomPlayer = Random.Range(0, players.Length);
                    target = players[randomPlayer].gameObject;
                    Debug.Log(name + " has changed target to " + target.name);
                }
            }
        }
        
    }

    public void FreezeAgent() {
        agent.speed = 0;
    }

    public void UnFreezeAgent() {
        agent.speed = originalSpeed;
    }
}
