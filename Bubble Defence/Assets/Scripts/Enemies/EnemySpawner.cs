using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemySpawner : MonoBehaviour
{
    Pathfinder pathfinder;
    [SerializeField] GameObject enemyPrefab;
    // Start is called before the first frame update
    void Start()
    {
        pathfinder = FindAnyObjectByType<Pathfinder>();
    }

    // Update is called once per frame
    void Update()
    {
        if(Input.GetKeyDown(KeyCode.Space))
        {
            List<Waypoint> path = pathfinder.FindPath();
            Vector3 startPos = transform.position + new Vector3(0, 0.5f, 0);
            GameObject clone = Instantiate(enemyPrefab, startPos, Quaternion.identity);
            clone.GetComponent<EnemyLogic>().Go(path);
        }
    }
}
