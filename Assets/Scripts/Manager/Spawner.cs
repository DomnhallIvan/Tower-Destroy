using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Spawner : MonoBehaviour
{
    public List<Transform> wayPoints;

    private WaveSO currentWave;  
    private int _currentenemycount = 0;
    public int _enemytotalCount = 0;

    private List<GameObject> activeEnemies = new List<GameObject>();

    void Spawn(GameObject enemyPrefab)
    {
        GameObject enemy = Instantiate(enemyPrefab, transform.position, Quaternion.identity);
        enemy.GetComponent<EnemyController>().SetDestination(wayPoints);
        activeEnemies.Add(enemy);
        _currentenemycount++;
    }

    public void StartNextWave(WaveSO waveSO)
    {
        StopAllCoroutines();
        currentWave = waveSO;

        _currentenemycount = 0;
        _enemytotalCount = 0;
        activeEnemies.Clear();

        foreach (var enemyData in currentWave.enemies)
        {
            _enemytotalCount += enemyData.count;
        }
        StartCoroutine(SpawnWave());
    }

    public void NoMoreEnemies()
    {
        foreach (GameObject enemy in activeEnemies)
        {
            if (enemy != null)
            {
                enemy.GetComponent<EnemyController>().KillEnemy();
            }
        }
        activeEnemies.Clear();
    }

    private IEnumerator SpawnWave()
    {
        foreach(var enemyData in currentWave.enemies)
        {
            for(int i=0;i<enemyData.count;i++)
            {
                Spawn(enemyData.enemyPrefab);
                yield return new WaitForSeconds(currentWave.spawnRate);
            }
        }
    }

    public void StopSpawning()
    {
        StopAllCoroutines();
    }
}
