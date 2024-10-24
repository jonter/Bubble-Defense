using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Waypoint : MonoBehaviour
{
    public bool busy = false;
    public bool explored = false;

    public Waypoint from;

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

 
}
