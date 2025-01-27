using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SquidEnemyLogic : EnemyLogic
{
    public override void Slow(float slowness, float duration)
    {
        slowness = Mathf.Sqrt(slowness);
        base.Slow(slowness, duration);
    }
}
