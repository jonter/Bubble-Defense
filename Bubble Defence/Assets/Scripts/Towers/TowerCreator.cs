using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TowerCreator : MonoBehaviour
{
    [SerializeField] GameObject archerTowerPrefab;
    [SerializeField] GameObject magicTowerPrefab;
    [SerializeField] GameObject canonTowerPrefab;

    Pathfinder pathfinder;

    public static TowerCreator instance;
    public void SpawnArcherTower(Waypoint point)
    {
        bool can = CanSpawn(point);
        if(can == true)
        {
            SpawnTower(archerTowerPrefab, point);
        }
    }

    public void SpawnMagicTower(Waypoint point)
    {
        bool can = CanSpawn(point);
        if (can == true)
        {
            SpawnTower(magicTowerPrefab, point);
        }
    }

    public void SpawnCanonTower(Waypoint point)
    {
        bool can = CanSpawn(point);
        if (can == true)
        {
            SpawnTower(canonTowerPrefab, point);
        }
    }

    void SpawnTower(GameObject towerPrefab, Waypoint point)
    {
        Vector3 spawnPos = point.transform.position + new Vector3(0, 0.5f, 0);
        GameObject newTower = Instantiate(towerPrefab, spawnPos, Quaternion.identity);
        Tower t = newTower.GetComponent<Tower>();
        t.placePoint = point;
        point.busy = true;
    }

    bool CanSpawn(Waypoint point)
    {
        bool can = true;
        point.busy = true;
        if (pathfinder.CheckPath() == false) can = false;
        // проверить хватает ли денег

        point.busy = false;
        return can;
    }

    // Start is called before the first frame update
    void Start()
    {
        pathfinder = FindAnyObjectByType<Pathfinder>();
        instance = this;
    }

    
}
