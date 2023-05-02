using UnityEngine;

public class GameOver : MonoBehaviour
{
    public GameObject gameOverScreen;

    public void EndGame() {
        GameObject.FindGameObjectWithTag("PlayerManager").GetComponent<PlayerManager>().SetIsGameOver(true);
        gameOverScreen.SetActive(true);
    }
}
