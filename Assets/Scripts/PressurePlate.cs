using System.Collections.Generic;
using UnityEngine;

public class PressurePlate : MonoBehaviour
{
    private List<IPressureWeight> _weightObjects = new List<IPressureWeight>();
    private float _totalWeight;

    [SerializeField]
    private float _activationWeight;

    private bool _isActivated;

    [SerializeReference, SubclassSelector]
    private ToggleableBehaviour[] _behaviours;


    private void OnTriggerEnter2D(Collider2D other)
    {
        if (other.TryGetComponent<IPressureWeight>(out var weight))
        {
            if (_weightObjects.Contains(weight)) return;

            _totalWeight += weight.Weight;

            if (_totalWeight >= _activationWeight && !_isActivated)
            {
                Activate();
            }

            _weightObjects.Add(weight);
        }
    }

    private void OnTriggerExit2D(Collider2D other)
    {
        if (other.TryGetComponent<IPressureWeight>(out var weight))
        {
            if (_weightObjects.Remove(weight))
            {
                _totalWeight -= weight.Weight;
                if (_totalWeight < _activationWeight && _isActivated)
                {
                    Deactivate();
                }
            }
        }
    }

    public void Activate()
    {
        _isActivated = true;
        foreach (var i in _behaviours)
        {
            i.Activate(gameObject);
        }
    }
    public void Deactivate()
    {
        _isActivated = false;
        foreach (var i in _behaviours)
        {
            i.Deactivate(gameObject);
        }
    }
}
