using System.Collections;
using System.Collections.Generic;
using System.Threading;
using UnityEngine;

public class LevelManager : MonoBehaviour
{    
    [SerializeField] private int _maxWave = 5;
    private int _currentWave =  0;
    private int _enemiesRemaining = 0;
    private float _timer = 0;
    [SerializeField] public float waveSpawnInterval = 45f;

    private bool isSpawning = false;

    [SerializeField] private Spawner _spawnerRef;

    private void Start()
    {
       _spawnerRef.StartNextWave();
    }

    private void Update()
    {
        if (isSpawning)
        {
            _timer = 0;
        }
        else
        {
            _timer -= Time.deltaTime;
            if (_timer <= 0)
            {
                if (_currentWave >= _maxWave)
                {
                    StopSpawning();
                }else
                    StartNextWave();
            }
        }
    }

    public void EnemyDestroyed()
    {
        _enemiesRemaining--;
        if( _enemiesRemaining == 0)
        {
            isSpawning = false;
            //reset the timer
            _timer = waveSpawnInterval;
        }
    }

    void StartNextWave()
    {
        _currentWave++;        
        _spawnerRef.StartNextWave();
        _enemiesRemaining = _spawnerRef._maxCount;
        isSpawning = true;
    }

    void StopSpawning()
    {
        _spawnerRef.StopSpawning();
        isSpawning=false;
    }
}
