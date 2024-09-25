using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Pathfinder : MonoBehaviour
{
    Dictionary<Vector2Int, Waypoint> grid = new Dictionary<Vector2Int, Waypoint>();

    Queue<Waypoint> search = new Queue<Waypoint>();

    [SerializeField] Waypoint startPoint;
    [SerializeField] Waypoint endPoint;

    bool isEndFound = false;
    
    // Start is called before the first frame update
    void Start()
    {
        FillGrid();
        startPoint.ChangeColor(Color.green);
        endPoint.ChangeColor(Color.magenta);
    }

    void FillGrid()
    {
        grid.Clear();
        Waypoint[] points = FindObjectsOfType<Waypoint>();
        for (int i = 0; i < points.Length; i++)
        {
            points[i].ChangeColor(Color.gray);
            points[i].explored = false;
            if (points[i].busy == true) continue;
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



    public List<Waypoint> FindPath()
    {
        isEndFound = false;
        FillGrid();
        search.Clear();
        search.Enqueue(startPoint);
        startPoint.explored = true;

        while (search.Count > 0)
        {
            Waypoint p = search.Dequeue();
            ExploreNear(p);
            if (isEndFound == true) break;
        }
       
        List<Waypoint> path = new List<Waypoint>();

        Waypoint point = endPoint;
        while (point != null)
        {
            path.Add(point);
            point.ChangeColor(Color.cyan);
            point = point.from;
        }
        path.Reverse();
        return path;
    }

    public bool CheckPath()
    {
        isEndFound = false;
        FillGrid();
        search.Clear();
        search.Enqueue(startPoint);
        startPoint.explored = true;

        while (search.Count > 0)
        {
            Waypoint p = search.Dequeue();
            ExploreNear(p);
            if (isEndFound == true) break;
        }

        return isEndFound;

    }

    void ExploreNear(Waypoint p)
    {
        Vector2Int pos = p.GetGridPos();
        Vector2Int left = pos + new Vector2Int(-1, 0);
        Vector2Int up = pos + new Vector2Int(0, 1);
        Vector2Int right = pos + new Vector2Int(1, 0);
        Vector2Int down = pos + new Vector2Int(0, -1);

        ExplorePoint(left, p);
        ExplorePoint(up, p);
        ExplorePoint(right, p);
        ExplorePoint(down, p);

    }

    void ExplorePoint(Vector2Int pos, Waypoint from)
    {
        if (grid.ContainsKey(pos) == false) return;
        if (grid[pos].explored == true) return;

        search.Enqueue(grid[pos]);
        grid[pos].explored = true;
        grid[pos].from = from;
        grid[pos].ChangeColor(Color.blue);

        if (grid[pos] == endPoint)
        {
            isEndFound = true;
            grid[pos].ChangeColor(Color.red);
        }

    }


}
