using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;

[CreateAssetMenu(menuName = "Wave config", fileName = "New Wave Config")]
public class WaveConfigSO : ScriptableObject
{
    [SerializeField] List<GameObject> enemyPrefabs;
    [SerializeField] Transform pathPrefab;
    [SerializeField] float moveSpeed = 5f;
    [SerializeField] float timeBetweenEnemySpawn = 0.8f;
    [SerializeField] float spawnTimeVariance = 0.6f;
    [SerializeField] float minimumSpawnTime = 0.2f;

    public Transform GetStartingWaypoint() => pathPrefab.GetChild(0);
    public List<Transform> GetWaypoints()
    {
        var children = new List<Transform>();

        foreach(Transform child in pathPrefab)
        {
            children.Add(child);
        }

        return children;
    }

    public float getMoveSpeed() => moveSpeed;

    public int GetEnemyCount() => enemyPrefabs.Count;
    public GameObject GetEnemyPrefab(int index) => enemyPrefabs[index];

    public float GetRandomSpawnTime()
    {
        float spawnTime = Random.Range(timeBetweenEnemySpawn - spawnTimeVariance,
                                          timeBetweenEnemySpawn + spawnTimeVariance);

        return Mathf.Clamp(spawnTime, minimumSpawnTime, float.MaxValue);
    }
}
