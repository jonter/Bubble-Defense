using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ArcherTower : Tower
{
    Archer archer;
    // Start is called before the first frame update

    protected override IEnumerator Start()
    {
        archer = GetComponentInChildren<Archer>();
        archer.gameObject.SetActive(false);
        yield return StartCoroutine(base.Start());
        archer.gameObject.SetActive(true);
    }

    

    // Update is called once per frame
    void Update()
    {
        if (isReady == false) return;
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
