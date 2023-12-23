using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class LifeScore : MonoBehaviour
{
    [SerializeField] private int startingLives = 1;
    public static  int currentLives;

    [Header("UI Elements")]
    [SerializeField]private  Text lifeScoreText;

    


void Start()
    {
        currentLives = startingLives;
        UpdateLifeScoreUI();
    }

    public void UpdateLifeScoreUI()
    {
        if (lifeScoreText != null)
        {
            lifeScoreText.text = "LIFE : " + currentLives.ToString();
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
   public static void RespawnPlayer()
    {
        // Implement respawn logic here
        Debug.Log("Respawning Player");
    }

    // This method should be called when the player gains an extra life
    public void GainLife()
    {
        currentLives++;
        UpdateLifeScoreUI();
    }
}
