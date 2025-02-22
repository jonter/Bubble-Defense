using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using DG.Tweening;


public class CanonTower : Tower
{
    [SerializeField] GameObject topPart;
    [SerializeField] GameObject projectilePrefab;
    bool reloaded = true;
    [SerializeField] float burstRadius = 1;

    protected override IEnumerator Start()
    {
        topPart.SetActive(false);
        yield return StartCoroutine(base.Start());
        topPart.SetActive(true);
    }

    private void OnDisable()
    {
        topPart.transform.DOKill();
    }

    // Update is called once per frame
    void Update()
    {
        if (reloaded == false) return;
        if(CanShoot() == false) return;

        StartCoroutine(ShootCoroutine());
    }

    public override void SellTower()
    {
        topPart.SetActive(false);
        base.SellTower();
    }



    IEnumerator ShootCoroutine()
    {
        reloaded = false;
        Vector3 spawnPos = topPart.transform.position;
        GameObject proj = Instantiate(projectilePrefab, spawnPos, Quaternion.identity);
        proj.GetComponent<CanonProjectile>()
            .Launch(target.transform.position, damage, burstRadius);
        float delay = 1 / fireRate;
        float h = topPart.transform.localPosition.y - 1;
        topPart.transform.DOLocalMoveY(h, delay / 4).SetEase(Ease.OutQuad)
            .SetLoops(2, LoopType.Yoyo);
        yield return new WaitForSeconds(delay);
        reloaded = true;
    }

}
