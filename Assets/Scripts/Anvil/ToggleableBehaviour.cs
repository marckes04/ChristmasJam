using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[System.Serializable]
public abstract class ToggleableBehaviour 
{
    public abstract void Activate(GameObject target);
    public abstract void Deactivate(GameObject target);
}
