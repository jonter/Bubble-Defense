using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;

public class Castle : MonoBehaviour
{
    AttackPoint[] attackPoints;

    // Start is called before the first frame update
    void Start()
    {
        attackPoints = GetComponentsInChildren<AttackPoint>();
    }

    public AttackPoint GetAttackPoint()
    {
        AttackPoint point = null;
        for(int i = 0; i < attackPoints.Length; i++)
        {
            if (attackPoints[i].enemy == null)
            {
                point = attackPoints[i];
                break;
            }

        }
        return point;
    }

    
}
