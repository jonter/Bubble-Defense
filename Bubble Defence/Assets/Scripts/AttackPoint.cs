using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AttackPoint : MonoBehaviour
{
    public GameObject enemy = null;

    private void Update()
    {
        if (enemy == null) return;

        EnemyHealth enHealth = enemy.GetComponent<EnemyHealth>();
        bool alive = enHealth.GetAlive();
        if (alive == false) enemy = null;
    }

}
