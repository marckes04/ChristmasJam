using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;

public class AnvilCarrier : MonoBehaviour, IPressureWeight
{
    [SerializeField]
    private AnvilObject _targetAnvil;

    private bool _isPulling;

    private bool _isCarrying;
    public bool IsCarrying => _isCarrying;


    // TODO move it to more appropriate script
    public float Weight => 1;

    [SerializeField]
    private Rigidbody2D _attachPoint;

    public void Throw(Vector2 force)
    {
        if (!IsCarrying || _isPulling) return;
        _targetAnvil.TogglePhysics(true);

        _targetAnvil.AddImpulse(force);

        _isCarrying = false;

        Detach();
    }
    public void Pull()
    {
        if (IsCarrying) return;
        _isPulling = true;
        _targetAnvil.TogglePhysics(false);

        // consider something else
        Physics2D.IgnoreCollision(GetComponentInChildren<Collider2D>(), _targetAnvil.GetComponentInChildren<Collider2D>(), true);
    }


    public void Drop()
    {
        if (!IsCarrying || _isPulling) return;

        _targetAnvil.TogglePhysics(true);

        _isCarrying = false;
        Detach();
    }

    private void Detach()
    {
        var joint = _targetAnvil.GetComponent<FixedJoint2D>();
        joint.enabled = false;


        // consider something else, as initial collision will be ugly
        Physics2D.IgnoreCollision(GetComponentInChildren<Collider2D>(), _targetAnvil.GetComponentInChildren<Collider2D>(), false);

        joint.connectedBody = null;
    }


    public void Attach()
    {
        if (IsCarrying) return;

        var joint = _targetAnvil.GetComponent<FixedJoint2D>();
        joint.enabled = true;

        joint.connectedBody = _attachPoint;

        // consider something else
        Physics2D.IgnoreCollision(GetComponentInChildren<Collider2D>(), _targetAnvil.GetComponentInChildren<Collider2D>(), true);

        _isCarrying = true;
        _isPulling = false;
    }

    private void FixedUpdate()
    {
        if (_isPulling)
        {
            var t = _targetAnvil.transform;
            var attachT = _attachPoint.transform;

            t.position = Vector3.MoveTowards(t.position, attachT.position, 12 * Time.deltaTime);

            if (Vector2.Distance(attachT.position, t.position) < 0.1f)
            {
                Attach();
            }
        }
    }
}
