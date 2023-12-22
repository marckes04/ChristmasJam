using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;


public class GameManager : MonoBehaviour
{

    [SerializeField] private GameObject startingTransition;
    [SerializeField] private GameObject endTransition;


   public void GameStart()
    {
        SceneManager.LoadScene("MainLevel");
    }
}
