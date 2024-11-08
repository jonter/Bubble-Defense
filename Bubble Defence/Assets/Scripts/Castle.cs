using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;
using System;

public class Castle : MonoBehaviour
{
    AttackPoint[] attackPoints;
    float hp = 100;
    float maxHp;

    bool alive = true;
    public event Action<float> OnDamage;

    // Start is called before the first frame update
    void Start()
    {
        maxHp = hp;
        attackPoints = GetComponentsInChildren<AttackPoint>();
    }

    public void GetDamage(float damage)
    {
        if (alive == false) return;
        hp -= damage;
        if(OnDamage != null) OnDamage(hp/maxHp);
        if (hp < 0.001f)
        {
            print("База уничтожена");
            alive = false;
        }
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
