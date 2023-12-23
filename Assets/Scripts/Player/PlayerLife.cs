using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class PlayerLife : MonoBehaviour
{

    [SerializeField] private int startingLives = 3;
    private int currentLives;

    [Header("UI Elements")]
    [SerializeField] private Text lifeScoreText;

    private void Start()
    {
        currentLives = startingLives;
        UpdateLifeScoreUI();
    }

    private void UpdateLifeScoreUI()
    {
        if (lifeScoreText != null)
        {
            lifeScoreText.text = "Lives: " + currentLives.ToString();
        }
    }

    // This method should be called when the player loses a life
    public void LoseLife()
    {
        currentLives--;

        if (currentLives <= 0)
        {
            // Game over logic, reset level, show game over screen, etc.
            Debug.Log("Game Over");
        }
        else
        {
            // Respawn player or perform other actions for losing a life
            RespawnPlayer();
        }

        UpdateLifeScoreUI();
    }

    // This method should be called when the player respawns after losing a life
    private void RespawnPlayer()
    {
        // Implement respawn logic here
        Debug.Log("Respawning Player");
    }

    private void OnTriggerEnter2D(Collider2D other)
    {
        if (other.CompareTag("Bullet"))
        {
            // Assuming the bullet causes the player to lose a life
            LoseLife();

            // Destroy the bullet
            Destroy(other.gameObject);
        }
    }

    // This method should be called when the player gains an extra life
    public void GainLife()
    {
        currentLives++;
        UpdateLifeScoreUI();
    }

}
