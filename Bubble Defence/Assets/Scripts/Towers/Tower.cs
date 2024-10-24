using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Tower : MonoBehaviour
{
    [SerializeField] protected float attackRadius = 2;
    [SerializeField] protected float damage = 10;
    [SerializeField] protected float fireRate = 1.5f;

    protected EnemyHealth target;

    public Waypoint placePoint;

    public int price = 10;

    private void OnDrawGizmosSelected()
    {
        Gizmos.color = Color.red;
        Gizmos.DrawWireSphere(transform.position, attackRadius);
    }

    

    protected List<EnemyLogic> FindEnemiesInRadius()
    {
        EnemyLogic[] enemies = FindObjectsOfType<EnemyLogic>();
        List<EnemyLogic> enemiesInRadius = new List<EnemyLogic>();

        foreach(EnemyLogic en in enemies)
        {
            float distance = Vector3.Distance(transform.position, en.transform.position);
            if(distance < attackRadius)
            {
                enemiesInRadius.Add(en);
            }
        }
        return enemiesInRadius;

    }

    protected EnemyHealth FindTargetToShoot()
    {
        List<EnemyLogic> enemies = FindEnemiesInRadius();
        if (enemies.Count == 0) return null;

        EnemyLogic target = null;
        float max = 0;
        for(int i = 0; i < enemies.Count; i++)
        {
            if (enemies[i].GetComponent<EnemyHealth>().GetAlive() == false)
                continue;
            if (enemies[i].distanceGone > max)
            {
                target = enemies[i];
                max = enemies[i].distanceGone;
            }
        }

        if (target == null) return null;
        return target.GetComponent<EnemyHealth>();
    }

}
