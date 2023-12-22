using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AnvilObject : MonoBehaviour, IPressureWeight
{
    private Rigidbody2D _rigidbody;

    public float Weight => 1;

    private void Awake()
    {
        _rigidbody = GetComponent<Rigidbody2D>();
    }

    public void TogglePhysics(bool value)
    {
        _rigidbody.isKinematic = !value;
    }

    public void AddImpulse(Vector2 force)
    {
        _rigidbody.AddForce(force, ForceMode2D.Impulse);
    }
}
