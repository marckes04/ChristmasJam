using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BulletDestruction : MonoBehaviour
{
    // Start is called before the first frame update
    void Start()
    {
        // Start the coroutine to destroy the bullet after 2 seconds
        StartCoroutine(DestroyBulletAfterDelay(2f));
    }

    // Coroutine to destroy the bullet after a specified delay
    IEnumerator DestroyBulletAfterDelay(float delay)
    {
        // Wait for the specified delay
        yield return new WaitForSeconds(delay);

        // Destroy the bullet game object
        Destroy(gameObject);
    }
}
