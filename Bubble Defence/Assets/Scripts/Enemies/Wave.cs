using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "Wave")]
public class Wave : ScriptableObject
{
    [SerializeField] GameObject[] enemyPrefabs;
    [SerializeField] float timeBetween = 1;
    [SerializeField] float hpMult = 1;
    [SerializeField] float coinsMult = 1;

    public float GetSpawnTime()
    {
        float time = timeBetween * (enemyPrefabs.Length-1) + 0.1f;
        return time;
    }

    public IEnumerator SpawnWaveCoroutine(Pathfinder pf, Vector3 origin)
    {
        List<Waypoint> points = pf.FindPath();
        for(int i = 0; i < enemyPrefabs.Length; i++)
        {
            GameObject clone = 
                Instantiate(enemyPrefabs[i], origin, Quaternion.identity);
            EnemyHealth eh = clone.GetComponent<EnemyHealth>();
            eh.IncreaseHP(hpMult);
            eh.IncreaseCoins(coinsMult);
            EnemyLogic el = clone.GetComponent<EnemyLogic>();
            el.Go(points);
            yield return new WaitForSeconds(timeBetween);
        }
    }

}
