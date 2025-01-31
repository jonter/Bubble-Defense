using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ArcherTower4B : Tower
{
    bool reloaded = true;
    [SerializeField] Transform shootPoint;

    void Update()
    {
        if (reloaded == false) return;
        if (CanShoot() == false) return;

        StartCoroutine(MassiveShoot());
    }

    IEnumerator MassiveShoot()
    {
        reloaded = false;
        List<EnemyHealth> enemies = FindEnemiesInRadius();
        foreach(EnemyHealth e in enemies)
        {
            Shoot(e);
        }
        yield return new WaitForSeconds(1 / fireRate);
        reloaded = true;
    }

    void Shoot(EnemyHealth enemy)
    {
        
        if (enemy.GetAlive() == false) return;
        Arrow newArrow = ArrowPool.instance.Get();
        newArrow.transform.position = shootPoint.position;
        
        newArrow.transform.localScale = new Vector3(0.15f, 0.15f, 0.15f);
        newArrow.Launch(enemy, damage);
    }

}
