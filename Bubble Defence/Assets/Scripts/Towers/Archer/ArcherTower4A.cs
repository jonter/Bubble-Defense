using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ArcherTower4A : Tower
{
    [SerializeField] Archer archer1;
    [SerializeField] Archer archer2;

    protected override IEnumerator Start()
    {
        archer1.gameObject.SetActive(false);
        archer2.gameObject.SetActive(false);
        yield return StartCoroutine(base.Start());
        archer1.gameObject.SetActive(true);
        archer2.gameObject.SetActive(true);
    }

    public override void SellTower()
    {
        archer1.gameObject.SetActive(false);
        archer2.gameObject.SetActive(false);
        base.SellTower();
    }

    // Update is called once per frame
    void Update()
    {
        if (CanShoot() == false) return;

        archer1.Shoot(target, damage, 1 / fireRate);
        archer2.Shoot(target, damage, 1 / fireRate);
    }
}
