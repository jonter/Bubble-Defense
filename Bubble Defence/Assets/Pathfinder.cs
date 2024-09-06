using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Pathfinder : MonoBehaviour
{
    Dictionary<Vector2Int, Waypoint> grid = new Dictionary<Vector2Int, Waypoint>();
    
    // Start is called before the first frame update
    void Start()
    {
        FillGrid();
    }

    void FillGrid()
    {
        grid.Clear();
        Waypoint[] points = FindObjectsOfType<Waypoint>();
        for (int i = 0; i < points.Length; i++)
        {
            Vector2Int pos = points[i].GetGridPos();
            if(grid.ContainsKey(pos) == true)
            {
                Debug.LogWarning("Waypoint is duplicated: " + grid[pos]);
                grid[pos].ChangeColor(Color.red);
                Destroy(points[i].gameObject);
            }
            else grid.Add(pos, points[i]);
        }
    }


    // Update is called once per frame
    void Update()
    {
        
    }
}
