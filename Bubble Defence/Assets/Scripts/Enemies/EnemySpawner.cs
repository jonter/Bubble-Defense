using System.Collections;
using System.Collections.Generic;

using UnityEngine;

public class EnemySpawner : MonoBehaviour
{
    [SerializeField] Wave[] waves;
    Pathfinder pathfinder;
    WaveDisplay display;

    int waveNum = 0;
    [SerializeField] float timeBetweenWaves = 5;

    public static bool Spawning = false;

    // Start is called before the first frame update
    void Start()
    {
        display = FindAnyObjectByType<WaveDisplay>();
        Spawning = false;
        pathfinder = FindAnyObjectByType<Pathfinder>();
    }

    void Update()
    {
        if (Input.GetKeyDown(KeyCode.Space))
        {
            if (waveNum > 0) return;
            StartCoroutine(SpawnNewWave());
        }
         
    }

    IEnumerator SpawnNewWave()
    {
        Spawning = true;
        Wave current = waves[waveNum];
        Vector3 pos = transform.position + new Vector3(0, 0.3f, 0);
        waveNum++;
        string info = $"Волна: {waveNum}/{waves.Length}";
        float fillTime = current.GetSpawnTime();
        display.SetWave(info, fillTime);
        yield return StartCoroutine(current.SpawnWaveCoroutine(pathfinder, pos));

        StartCoroutine(WaitBeforeNewWave());
    }

    IEnumerator WaitBeforeNewWave()
    {
        EnemyHealth[] enemies = FindObjectsOfType<EnemyHealth>();
        while(enemies.Length > 0)
        {
            yield return new WaitForSeconds(1);
            enemies = FindObjectsOfType<EnemyHealth>();
        }
        Spawning = false;
        if(waveNum >= waves.Length)
        {
            print("Ты прошел уровень");
        }
        else
        {
            display.SetBuild("Строим", timeBetweenWaves);
            yield return new WaitForSeconds(timeBetweenWaves);
            StartCoroutine(SpawnNewWave());
        }
    }

    

    
}
