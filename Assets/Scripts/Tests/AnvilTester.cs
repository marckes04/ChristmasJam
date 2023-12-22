using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AnvilTester : MonoBehaviour
{
    private AnvilCarrier _anvilCarrier;

    private void Awake()
    {
        _anvilCarrier = GetComponent<AnvilCarrier>();
    }

    private void Update()
    {
        if (Input.GetKeyDown(KeyCode.E))
        {
            if (_anvilCarrier.IsCarrying)
            {
                _anvilCarrier.Throw(Vector3.right * 5);
            }
            else
            {
                _anvilCarrier.Pull();
            }
        }
    }
}
