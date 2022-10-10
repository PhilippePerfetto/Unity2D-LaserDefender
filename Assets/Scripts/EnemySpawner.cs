using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemySpawner : MonoBehaviour
{
    [SerializeField] List<WaveConfigSO> waveConfigs;
    [SerializeField] WaveConfigSO currentWave;
    [SerializeField] float timeBetweenWaves = 1f;
    [SerializeField] bool isLooping = true;

    // Start is called before the first frame update
    void Start()
    {
        StartCoroutine(SpawnEnemyWaves());
    }

    public WaveConfigSO GetCurrentWave() => currentWave;

    IEnumerator SpawnEnemyWaves()
    {
        do {
            foreach(var wave in waveConfigs)
            {
                currentWave = wave;
                for(int i = 0; i < currentWave.GetEnemyCount(); i++)
                {
                    Instantiate(currentWave.GetEnemyPrefab(i), 
                                currentWave.GetStartingWaypoint().position,
                                Quaternion.Euler(0,0,180),
                                transform);

                    yield return new WaitForSeconds(currentWave.GetRandomSpawnTime());
                }

                yield return new WaitForSeconds(timeBetweenWaves);
            }
        } while(isLooping);
    }
}
