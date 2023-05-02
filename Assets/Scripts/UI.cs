using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class UI : MonoBehaviour
{
    [SerializeField]
    private GameObject player1HealthBar;
    [SerializeField]
    private GameObject player2HealthBar;
    [SerializeField]
    private GameObject player3HealthBar;
    [SerializeField]
    private GameObject player4HealthBar;

    [SerializeField]
    private GameObject player1PowerUpTimer;
    [SerializeField]
    private GameObject player2PowerUpTimer;
    [SerializeField]
    private GameObject player3PowerUpTimer;
    [SerializeField]
    private GameObject player4PowerUpTimer;

    private PlayerHealth player1Health;
    private PlayerHealth player2Health;
    private PlayerHealth player3Health;
    private PlayerHealth player4Health;

    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
