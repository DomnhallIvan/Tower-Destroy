using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "NewWaveConfig", menuName = "TowerDefense/WaveConfig")]
public class WaveSO : ScriptableObject
{
    [System.Serializable]
    public class EnemySpawnData
    {
        public GameObject enemyPrefab;
        public int count;
    }

    public List<EnemySpawnData> enemies; // List of enemies and their counts
    public float spawnRate = 1f;         // Time between spawns
}
