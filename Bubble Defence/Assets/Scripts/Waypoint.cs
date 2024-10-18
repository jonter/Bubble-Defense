using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Waypoint : MonoBehaviour
{
    public bool busy = false;
    public bool explored = false;

    public Waypoint from;

    [SerializeField] GameObject testTowerPrefab;

    public Vector2Int GetGridPos()
    {
        int x = Mathf.RoundToInt(transform.position.x);
        int y = Mathf.RoundToInt(transform.position.z);
        Vector2Int pos = new Vector2Int(x, y);
        return pos;
    }

    public void ChangeColor(Color c)
    {
        GetComponent<MeshRenderer>().material.color = c;
    }

    public void BuildTower()
    {
        busy = true;
        Pathfinder finder = FindAnyObjectByType<Pathfinder>();
        bool canPass = finder.CheckPath();
        if(canPass == true)
        {
            Vector3 pos = transform.position + new Vector3(0, 0.5f, 0);
            Instantiate(testTowerPrefab, pos, Quaternion.identity);
        }
        else
        {
            busy = false;
            print("you cannot place here...");
        }
    }

 
}
