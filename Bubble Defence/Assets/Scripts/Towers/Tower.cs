using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Tower : MonoBehaviour
{
    [SerializeField] protected float attackRadius = 2;
    [SerializeField] protected float damage = 10;
    [SerializeField] protected float fireRate = 1.5f;

    protected EnemyHealth target;

    public Waypoint placePoint;

    public int price = 10;

    protected bool isReady = false;
    MeshFilter filter;
    [SerializeField] protected Mesh buildingMesh;
    [SerializeField] protected Mesh readyMesh;
    [SerializeField] protected ParticleSystem buildVFX;

    public Tower upgradedTower;

    [Tooltip("Вставить сюда еще башню, если мы можем прокачаться в другой тип башен")]
    public Tower extraUpgradeTower;

    protected virtual IEnumerator Start()
    {
        filter = GetComponent<MeshFilter>();
        filter.mesh = buildingMesh;
        buildVFX.Play();
        isReady = false;
        yield return new WaitForSeconds(2);
        filter.mesh = readyMesh;
        isReady = true;
        buildVFX.Stop();
        StartCoroutine(ScanEnemyCoroutine());
    }

    public bool GetIsReady() { return isReady; }


    public virtual void SellTower()
    {
        StartCoroutine(SellCoroutine());
    }

    IEnumerator SellCoroutine()
    {
        GetComponent<Collider>().enabled = false;
        buildVFX.Play();
        filter.mesh = buildingMesh;
        isReady = false;
        GameCoins.AddCoins(price);
        yield return new WaitForSeconds(2);
        placePoint.busy = false;
        Destroy(gameObject);
    }

    public void Upgrade()
    {
        GameObject newTower = Instantiate(upgradedTower.gameObject);
        newTower.transform.position = transform.position;
        newTower.GetComponent<Tower>().placePoint = placePoint;
        Destroy(gameObject);
    }

    public void ExtraUpgrade()
    {
        GameObject newTower = Instantiate(extraUpgradeTower.gameObject);
        newTower.transform.position = transform.position;
        newTower.GetComponent<Tower>().placePoint = placePoint;
        Destroy(gameObject);
    }

    protected IEnumerator ScanEnemyCoroutine()
    {
        yield return new WaitForSeconds(0.2f);
        target = FindTargetToShoot();

        StartCoroutine(ScanEnemyCoroutine());
    }

    private void OnDrawGizmosSelected()
    {
        Gizmos.color = Color.red;
        Gizmos.DrawWireSphere(transform.position, attackRadius);
    }

    

    protected List<EnemyHealth> FindEnemiesInRadius()
    {
        
        List<EnemyHealth> enemiesInRadius = new List<EnemyHealth>();

        foreach(EnemyHealth en in EnemyHealth.AliveEnemies)
        {
            float distance = Vector3.Distance(transform.position, en.transform.position);
            if(distance < attackRadius)
            {
                enemiesInRadius.Add(en);
            }
        }
        return enemiesInRadius;

    }

    protected EnemyHealth FindTargetToShoot()
    {
        List<EnemyHealth> enemies = FindEnemiesInRadius();
        if (enemies.Count == 0) return null;

        EnemyHealth target = null;
        float max = 0;
        for(int i = 0; i < enemies.Count; i++)
        {
            EnemyLogic el = enemies[i].GetComponent<EnemyLogic>();
            if (el.distanceGone > max)
            {
                target = enemies[i];
                max = el.distanceGone;
            }
        }
        if (target == null) return null;
        return target;
    }


    protected bool CanShoot()
    {
        if (isReady == false) return false;
        if (target == null) return false;
        if (target.GetAlive() == false)
        {
            target = null;
            return false;
        }
        float dist = Vector3.Distance(transform.position, target.transform.position);
        if (dist > attackRadius)
        {
            target = null;
            return false;
        }

        return true;
    }
}
