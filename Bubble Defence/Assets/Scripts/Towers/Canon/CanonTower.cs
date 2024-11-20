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

    // Update is called once per frame
    void Update()
    {
        if (reloaded == false) return;
        if (isReady == false) return;
        if (target == null) return;
        if (target.GetAlive() == false)
        {
            target = null;
            return;
        }
        float dist = Vector3.Distance(transform.position, target.transform.position);
        if (dist > attackRadius)
        {
            target = null;
            return;
        }

        StartCoroutine(ShootCoroutine());
    }

    IEnumerator ShootCoroutine()
    {
        reloaded = false;
        Vector3 spawnPos = topPart.transform.position;
        GameObject proj = Instantiate(projectilePrefab, spawnPos, Quaternion.identity);
        proj.GetComponent<CanonProjectile>()
            .Launch(target.transform.position, damage, burstRadius);
        float delay = 1 / fireRate;
        topPart.transform.DOLocalMoveY(2.5f, delay / 3).SetEase(Ease.OutQuad)
            .SetLoops(2, LoopType.Yoyo);
        yield return new WaitForSeconds(delay);
        reloaded = true;
    }

}
