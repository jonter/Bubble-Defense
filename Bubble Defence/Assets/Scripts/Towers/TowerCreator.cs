using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TowerCreator : MonoBehaviour
{
    [SerializeField] GameObject archerTowerPrefab;
    [SerializeField] GameObject magicTowerPrefab;
    [SerializeField] GameObject canonTowerPrefab;

    Pathfinder pathfinder;
    BuildButtons bb;

    public static TowerCreator instance;
    public void SpawnArcherTower(Waypoint point)
    {
        SpawnTower(archerTowerPrefab, point);
    }

    public void SpawnMagicTower(Waypoint point)
    {
        SpawnTower(magicTowerPrefab, point);
    }

    public void SpawnCanonTower(Waypoint point)
    {
        SpawnTower(canonTowerPrefab, point);
    }

    void SpawnTower(GameObject towerPrefab, Waypoint point)
    {
        if (CheckCoins(towerPrefab) == false) return;
        Vector3 spawnPos = point.transform.position + new Vector3(0, 0.5f, 0);
        GameObject newTower = Instantiate(towerPrefab, spawnPos, Quaternion.identity);
        Tower t = newTower.GetComponent<Tower>();
        t.placePoint = point;
        point.busy = true;
    }

    bool CheckCoins(GameObject towerPrefab)
    {
        Tower t = towerPrefab.GetComponent<Tower>();
        bool canBuy = GameCoins.SpendCoins(t.price);
        if(canBuy == false)
        {
            HintText.Show("Ќедостаточно монет дл€ строительства");
            return false;
        }
        return true;
    }

    bool CanSpawn(Waypoint point)
    { 
        bool can = true;
        if (point.busy == true) return false;
        point.busy = true;
        if (pathfinder.CheckPath() == false) can = false;

        point.busy = false;
        return can;
    }

    // Start is called before the first frame update
    void Start()
    {
        pathfinder = FindAnyObjectByType<Pathfinder>();
        instance = this;
        bb = FindAnyObjectByType<BuildButtons>();
    }

    public void ShowBuildButtons(Waypoint p)
    {
        if (CanSpawn(p) == false)
        {
            HintText.Show("“ы не можешь поставить здесь башню", 1);
            return;
        }
        bb.ShowButtons(p);
    }

    public void HideBuildButtons()
    {
        bb.HideButtons();
    }
    
}
