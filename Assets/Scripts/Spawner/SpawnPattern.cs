using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "New Pattern", menuName = "Spawn Pattern")]
public class SpawnPattern : ScriptableObject
{
    public float duration = 30;

    public SpawnData[] spawnData;

    [System.Serializable]
    public class SpawnData
    {
        public GameObject EnemyPrefab;
        public float initialWait = 0f;
        [Tooltip("Base time between spawns")]
        public float timeBetweenSpawns = 50f;
        [Tooltip("The spawn time can be anywhere between (base - this) to (base + this)")]
        public float timeFluctuation = 20f;
        [Tooltip("the spawn time cannot be smaller than this")]
        public float minTime = 35f;
        [Range(0f, 1f)]
        public float spawnChance = 1;

        public float NextSpawn
        {
            get { return Mathf.Clamp(Random.Range(timeBetweenSpawns - timeFluctuation, timeBetweenSpawns + timeFluctuation), minTime, timeBetweenSpawns + timeFluctuation); }
        }
    }
}
