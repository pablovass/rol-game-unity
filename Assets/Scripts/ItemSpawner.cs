using System.Collections;
using System.Collections.Generic;
using Unity.Mathematics;
using UnityEngine;
using Random = UnityEngine.Random;

public class ItemSpawner : MonoBehaviour
{
    [SerializeField]  GameObject checkpointPrefab;
    [SerializeField] int checkpointSpawnDelay = 10;
    [SerializeField] float spawnRadius;
    [SerializeField]  GameObject[] powerUpPrefab;
    [SerializeField] int powerUpSpawnDelay = 12;
    
    void Start()
    {
        StartCoroutine(SpawnCheackpointRoutine());
        StartCoroutine(SpawnPowerUpRoutine());
    }

    IEnumerator SpawnCheackpointRoutine()
    {
        while (true)
        {
            yield return new WaitForSeconds(checkpointSpawnDelay);
            Vector2 randomPosition = Random.insideUnitCircle * spawnRadius;
            Instantiate(checkpointPrefab, randomPosition, quaternion.identity);
        }

    }

    IEnumerator SpawnPowerUpRoutine()
    {
        while (true)
        {
            yield return new WaitForSeconds(powerUpSpawnDelay);
            Vector2 randomPosition = Random.insideUnitCircle * spawnRadius;
            int random = Random.Range(0, powerUpPrefab.Length);
            Instantiate(powerUpPrefab[random], randomPosition,quaternion.identity); 
        }
    }
}
