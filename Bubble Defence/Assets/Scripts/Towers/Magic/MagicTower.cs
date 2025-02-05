using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using DG.Tweening;

public class MagicTower : Tower
{
    [SerializeField] protected GameObject topCrystal;
    protected LineRenderer shootLine;
    [SerializeField] protected ParticleSystem frostVFX;
    [SerializeField] protected float slowness = 2;
    [SerializeField] protected float slowDuration = 1;
    protected bool reloaded = true;

    protected override IEnumerator Start()
    {
        shootLine = GetComponentInChildren<LineRenderer>();
        shootLine.enabled = false;
        topCrystal.SetActive(false);
        yield return StartCoroutine(base.Start());
        topCrystal.SetActive(true);
        AnimCrystal();
    }

    public override void SellTower()
    {
        topCrystal.SetActive(false);
        base.SellTower();
    }

    private void OnDisable()
    {
        topCrystal.transform.DOKill();
    }

    void AnimCrystal()
    {
        Vector3 rot = new Vector3(0, 360, 0);
        topCrystal.transform.DORotate(rot, 5, RotateMode.WorldAxisAdd)
            .SetEase(Ease.Linear).SetLoops(-1);

        float h = topCrystal.transform.localPosition.y + 1;
        topCrystal.transform.DOLocalMoveY(h, 1).SetEase(Ease.InOutQuad)
            .SetLoops(-1, LoopType.Yoyo);

    }

    // Update is called once per frame
    void Update()
    {
        if (reloaded == false) return;
        if (CanShoot() == false) return;

        StartCoroutine(ShootCoroutine());
    }

    protected virtual IEnumerator ShootCoroutine()
    {
        reloaded = false;
        target.GetDamage(damage, DamageType.MAGIC);
        target.GetComponent<EnemyLogic>().Slow(slowness, slowDuration);
        StartCoroutine(ShowEffectCoroutine());
        yield return new WaitForSeconds(1 / fireRate);
        reloaded = true;
    }

    IEnumerator ShowEffectCoroutine()
    {
        frostVFX.transform.position = target.transform.position;
        frostVFX.Play();
        shootLine.enabled = true;
        shootLine.SetPosition(0, topCrystal.transform.position);
        Vector3 end = target.transform.position + new Vector3(0, 0.2f, 0);
        shootLine.SetPosition(1, end);
        yield return new WaitForSeconds(0.1f);
        shootLine.enabled = false;
    }

}
