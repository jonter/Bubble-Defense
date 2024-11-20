using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using DG.Tweening;

public class CanonProjectile : MonoBehaviour
{
    [SerializeField] float flyTime = 1;
    [SerializeField] float height = 5;
    bool launched = false;
    float damage;
    float radius;

    public void Launch(Vector3 pos, float d, float r)
    {
        damage = d;
        radius = r;
        launched = true;
        transform.DOMoveX(pos.x, flyTime).SetEase(Ease.Linear);
        transform.DOMoveZ(pos.z, flyTime).SetEase(Ease.Linear);
        transform.DOMoveY(height, flyTime / 2).SetEase(Ease.OutQuad)
            .SetLoops(2, LoopType.Yoyo);
        StartCoroutine(BoomCoroutine());
    }

    IEnumerator BoomCoroutine()
    {
        yield return new WaitForSeconds(flyTime);
        ParticleSystem boomVFX = GetComponentInChildren<ParticleSystem>();
        
        boomVFX.transform.parent = null;
        boomVFX.transform.localScale = Vector3.one;
        boomVFX.Play();
        DamageEnemies();
        Destroy(gameObject);
    }

    void DamageEnemies()
    {
        EnemyHealth[] enemies = FindObjectsOfType<EnemyHealth>();
        foreach(EnemyHealth enemy in enemies)
        {
            float dist = 
                Vector3.Distance(enemy.transform.position, transform.position);
            if (dist <= radius) enemy.GetDamage(damage);
        }

    }

    

    // Update is called once per frame
    void Update()
    {
        if(Input.GetKeyDown(KeyCode.D))
        {
            Vector3 coord = new Vector3(-1, 1, 1);
            Launch(coord, 100, 1);
        }
    }
}
