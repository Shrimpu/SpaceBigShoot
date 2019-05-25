using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Spawner : MonoBehaviour
{
    public bool cycle = false;

    public SpawnPattern[] patterns;

    int iteration;
    public int Iteration
    {
        get { return Mathf.Clamp(iteration, 0, patterns.Length); }
        set { iteration = value; }
    }
    public int IterationCycled
    {
        get { return iteration % (patterns.Length); }
        set { iteration = value; }
    }

    Coroutine[] spawnCoroutines;

    private void Start()
    {
        InitiatePattern(patterns[Iteration]);
        Iteration++;
    }

    void InitiatePattern(SpawnPattern pattern)
    {
        StartCoroutine(ChangePattern(pattern.duration));
        // initializes spawnCourotines so it can hold all coroutines
        spawnCoroutines = new Coroutine[pattern.spawnData.Length];
        for (int i = 0; i < pattern.spawnData.Length; i++)
        {
            spawnCoroutines[i] = StartCoroutine(Spawn(pattern.spawnData[i]));
        }
    }

    IEnumerator Spawn(SpawnPattern.SpawnData data)
    {
        yield return new WaitForSeconds(data.initialWait);
        while (true)
        {
            if (data.spawnChance > Random.Range(0f, 1f))
            {
                Instantiate(data.EnemyPrefab, transform.position, Quaternion.identity);
            }
            yield return new WaitForSeconds(data.NextSpawn);
        }
    }

    IEnumerator ChangePattern(float time)
    {
        yield return new WaitForSeconds(time);
        for (int i = 0; i < spawnCoroutines.Length; i++)
        {
            StopCoroutine(spawnCoroutines[i]);
        }

        int nextPattern;
        // decides what the next pattern will be
        if (!cycle)
        {
            nextPattern = Iteration;
        }
        else
        {
            nextPattern = IterationCycled;
        }
        Iteration++;
        // sends result to start all over again
        InitiatePattern(patterns[nextPattern]);
    }

    public void Stop()
    {
        StopAllCoroutines();
    }
}