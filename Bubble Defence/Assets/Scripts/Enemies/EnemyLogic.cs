using System.Collections;
using System.Collections.Generic;
using System.Drawing;
using UnityEngine;

public class EnemyLogic : MonoBehaviour
{
    [SerializeField] float speed = 1;
    Animator anim;

    [HideInInspector] public float distanceGone = 0;
    [SerializeField] float damage = 10;

    public virtual void Slow(float slowness, float duration)
    {
        StartCoroutine(SlowCoroutine(slowness, duration));
    }

    IEnumerator SlowCoroutine(float slowness, float duration)
    {
        speed /= slowness;
        yield return new WaitForSeconds(duration);
        speed *= slowness;
    }

    public void Attack()
    {
        FindAnyObjectByType<Castle>().GetDamage(damage);
    }

    // Start is called before the first frame update
    void Start()
    {
        anim = GetComponent<Animator>();
    }

    private void OnDisable()
    {
        StopAllCoroutines();
    }
    public void Go(List<Waypoint> path)
    {
        StartCoroutine(GoCoroutine(path));
    }

    IEnumerator GoCoroutine(List<Waypoint> path)
    {
        for (int i = 0; i < path.Count; i++) 
        {
            Vector3 point = path[i].transform.position + new Vector3(0, 1, 0);
            RotateTowards(point);
            yield return StartCoroutine(GoToPoint(point));
        }

        Castle c = FindAnyObjectByType<Castle>(); 
        AttackPoint ap = c.GetAttackPoint();
        while (ap == null)
        {
            yield return new WaitForSeconds(1);
            ap = c.GetAttackPoint();
        }
        ap.enemy = gameObject;
        RotateTowards(ap.transform.position);
        yield return StartCoroutine(GoToPoint(ap.transform.position));

        anim.SetBool("attack", true);
    }


    void RotateTowards(Vector3 point)
    {
        Vector3 dir = point - transform.position;
        transform.forward = dir;
    }

    IEnumerator GoToPoint(Vector3 end)
    {
        Vector3 start = transform.position;
        float timer = 0;
        while(timer < 1)
        {
            timer += Time.deltaTime * speed;
            distanceGone += Time.deltaTime * speed;
            transform.position = Vector3.Lerp(start, end, timer);
            yield return null;
        }

    }

}
