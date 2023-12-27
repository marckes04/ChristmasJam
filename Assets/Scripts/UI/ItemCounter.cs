using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

public class ItemCounter : MonoBehaviour
{
    public static ItemCounter instance;

    public TMP_Text itemText;
    public int currentCandies = 0;

    private void Awake()
    {
        instance = this;
    }

    // Start is called before the first frame update
    void Start()
    {
        itemText.text = "CANDIES: " + currentCandies.ToString();
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public void IncreaseCoins(int v)
    {
        currentCandies += v;
        itemText.text = "CANDIES: "+ currentCandies.ToString();
    }

}
