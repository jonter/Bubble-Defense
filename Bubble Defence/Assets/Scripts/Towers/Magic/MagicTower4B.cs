using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;

public class MagicTower4B : MagicTower
{

    protected override IEnumerator ShootCoroutine()
    {
        reloaded = false;
        StartCoroutine(MassiveShoot());
        yield return new WaitForSeconds(1 / fireRate);
        reloaded = true;
    }

    IEnumerator MassiveShoot()
    {
        List<EnemyHealth> enemies = FindEnemiesInRadius();
        shootLine.positionCount = enemies.Count * 2;
        int pointIndex = 0;
        shootLine.enabled = true;
        foreach(EnemyHealth e in enemies)
        {
            e.GetComponent<EnemyLogic>().Slow(slowness, slowDuration);
            e.GetDamage(damage, DamageType.MAGIC);
            DrawShootLine(e, pointIndex);
            pointIndex += 2;
        }
        yield return new WaitForSeconds(0.1f);
        
        shootLine.enabled = false;
    }

    void DrawShootLine(EnemyHealth enemy, int index)
    {
        
        shootLine.SetPosition(index, topCrystal.transform.position);
        shootLine.SetPosition(index + 1, enemy.transform.position);
        frostVFX.transform.position = enemy.transform.position + new Vector3(0, 0.2f, 0);
        frostVFX.Emit(10);
    }



}
