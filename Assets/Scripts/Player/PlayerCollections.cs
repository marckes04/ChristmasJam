using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

public class PlayerCollections : MonoBehaviour
{
    public int value;



    private void OnTriggerEnter2D(Collider2D other)
    {
        if(other.gameObject.CompareTag("Player"))
        {
            Destroy(gameObject);
            ItemCounter.instance.IncreaseCoins(value);
        }
    }
}
