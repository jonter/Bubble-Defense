using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Arrow : MonoBehaviour
{
    float flyTime = 0.5f;
    float damage = 10;
    public void Launch(EnemyHealth enemy, float d)
    {
        damage = d;
        StartCoroutine(FlyCoroutine(enemy));
        Vector3 pos = enemy.transform.position + new Vector3(0, 0.4f, 0);
        transform.LookAt(pos);
        transform.parent = null;
    }

    IEnumerator FlyCoroutine(EnemyHealth enemy)
    {
        float timer = 0;
        Vector3 startPos = transform.position;
        while (timer < 1)
        {
            timer += Time.deltaTime/flyTime;
            Vector3 pos = enemy.transform.position + new Vector3(0, 0.4f, 0);
            transform.position = Vector3.Lerp(startPos, pos, timer);
            yield return null;
        }
        enemy.GetDamage(damage);
        ArrowPool.instance.Return(this);
    }


}
