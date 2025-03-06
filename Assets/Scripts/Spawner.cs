using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Spawner : MonoBehaviour
{
    public GameObject enemyPrefab;

    public float spawnRate = 0.5f;

    public List<Transform> wayPoints;

    public int _maxCount = 10;

    private int _count = 0;

    void Spawn()
    {
        GameObject enemy = Instantiate(enemyPrefab, transform.position, Quaternion.identity);
        enemy.GetComponent<EnemyController>().SetDestination(wayPoints);

        _count++;

        if (_count >= _maxCount)
        {
            CancelInvoke();
        }
    }

    public void StartNextWave()
    {
        _count = 0;
        InvokeRepeating("Spawn", 1, spawnRate);
    }

    public void StopSpawning()
    {
        CancelInvoke();
    }
}
