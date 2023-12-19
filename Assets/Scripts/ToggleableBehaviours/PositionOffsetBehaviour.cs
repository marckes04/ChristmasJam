using System.Collections;
using System.Collections.Generic;
using DG.Tweening;
using UnityEngine;

[System.Serializable]
public class PositionOffsetBehaviour : ToggleableBehaviour
{
    public Vector2 Offset;
    public Transform Target;

    public override void Activate(GameObject target)
    {
        Target.DOComplete();
        Target.DOBlendableLocalMoveBy(Offset, 0.2f);
    }

    public override void Deactivate(GameObject target)
    {
        Target.DOComplete();
        Target.DOBlendableLocalMoveBy(-Offset, 0.2f);
    }
}
