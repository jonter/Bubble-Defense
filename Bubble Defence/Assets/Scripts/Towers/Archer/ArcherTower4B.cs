using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ArcherTower4B : Tower
{
    [SerializeField] GameObject arrowPrefab;
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
        List<EnemyLogic> enemies = FindEnemiesInRadius();
        foreach(EnemyLogic e in enemies)
        {
            Shoot(e);
        }
        yield return new WaitForSeconds(1 / fireRate);
        reloaded = true;
    }

    void Shoot(EnemyLogic enemy)
    {
        EnemyHealth eh = enemy.GetComponent<EnemyHealth>();
        if (eh.GetAlive() == false) return;
        GameObject newArrow = Instantiate(arrowPrefab, 
            shootPoint.position, Quaternion.identity);
        Arrow a = newArrow.GetComponent<Arrow>();
        
        a.Launch(eh, damage);
    }

}
