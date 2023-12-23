using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RestrictionMovement : MonoBehaviour
{
    public float minX = -5.15f;

    public GameObject player;


    private void FixedUpdate()
    {
        Vector3 temp = player.transform.position;// Variable to stablish the current position


        if (temp.x < minX) // command to check if the player goes beyond maximum allowed place in Y axis
            temp.x = minX;

        player.transform.position = temp;
    }

}
