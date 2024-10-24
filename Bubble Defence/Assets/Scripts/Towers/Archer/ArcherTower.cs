using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ArcherTower : Tower
{
    Archer archer;
    // Start is called before the first frame update
    void Start()
    {
        archer = GetComponentInChildren<Archer>();
        StartCoroutine(ScanEnemyCoroutine());
    }

    IEnumerator ScanEnemyCoroutine()
    {
        yield return new WaitForSeconds(0.2f);
        if(target == null) target = FindTargetToShoot();
        
        StartCoroutine(ScanEnemyCoroutine());
    }

    // Update is called once per frame
    void Update()
    {
        if (target == null) return;
        if (target.GetAlive() == false)
        {
            target = null;
            return;
        }
        float dist = Vector3.Distance(transform.position, target.transform.position);
        if(dist > attackRadius)
        {
            target = null;
            return;
        }

        archer.Shoot(target, damage, 1/fireRate);
        
    }
}
