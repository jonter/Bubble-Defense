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

    public override void SellTower()
    {
        archer.gameObject.SetActive(false);
        base.SellTower();
    }



    // Update is called once per frame
    void Update()
    {
        if (CanShoot() == false) return;

        archer.Shoot(target, damage, 1/fireRate);
        
    }
}
