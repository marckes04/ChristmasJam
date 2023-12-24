using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;


public class GameManager : MonoBehaviour
{

    [SerializeField] private GameObject startingTransition;
    [SerializeField] private GameObject endTransition;

     void Start()
    {
       startingTransition.SetActive(true);
        Invoke("DisableStartingTransition", 1.0f);
    }


    private void DisableStartingTransition()
    {
        startingTransition.SetActive(false);
    }

    public void GameStart()
    {
        endTransition.SetActive(true);
        Invoke("LoadLevel", 0.5f);
    }


    private void LoadLevel()
    {
        SceneManager.LoadScene("MainLevel");
    }


  
}
