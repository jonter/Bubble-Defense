using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;

public class EnemySpawner : MonoBehaviour
{
    [SerializeField] Wave[] waves;
    Pathfinder pathfinder;
    WaveDisplay display;

    int waveNum = 0;
    [SerializeField] float timeBetweenWaves = 5;

    public static bool Spawning = false;

    public event Action OnStartSpawn;
    public event Action OnEndSpawn;
    public event Action OnLevelComplete;

    // Start is called before the first frame update
    void Start()
    {
        EnemyHealth.AliveEnemies = new List<EnemyHealth>();
        display = FindAnyObjectByType<WaveDisplay>();
        Spawning = false;
        pathfinder = FindAnyObjectByType<Pathfinder>();
    }

    public void StartSpawning()
    {
        if (Spawning == true) return;
        if (waveNum >= waves.Length) return;
        StartCoroutine(SpawnNewWave());

    }

    IEnumerator SpawnNewWave()
    {
        if(OnStartSpawn != null) OnStartSpawn();
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
        while(EnemyHealth.AliveEnemies.Count > 0)
        {
            yield return new WaitForSeconds(1);
        }

        Spawning = false;
        if(waveNum >= waves.Length)
        {
            print("Ты прошел уровень");
            if(OnLevelComplete != null) OnLevelComplete();
        }
        else
        {
            display.SetBuild("Строим", timeBetweenWaves);
            if(OnEndSpawn != null) OnEndSpawn();
            yield return new WaitForSeconds(timeBetweenWaves);
            if (Spawning == true) yield break; 
            StartCoroutine(SpawnNewWave());
        }
    }

    

    
}
