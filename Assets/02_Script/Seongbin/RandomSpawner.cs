using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RandomSpawner : MonoBehaviour
{
    [SerializeField] private Vector2 spawnSize;
    [SerializeField] private ObjectPooler enemyPooler;
    [SerializeField] private float enemySpawnDelay;
    private void Start()
    {

    }
    void OnDrawGizmos()
    {
        Gizmos.color = Color.red;
        Gizmos.DrawWireCube(transform.position, spawnSize);
    }
    public IEnumerator EnemySpawn()
    {
        GameObject enemy = enemyPooler.SpawnPrefab("Goblin");
        enemy.transform.position = spawnSize;

        yield return new WaitForSeconds(enemySpawnDelay);

        StartCoroutine("EnemySpawn");
    }
    public IEnumerator EnemySpawn2()
    {
        GameObject enemy = enemyPooler.SpawnPrefab("Skelleton");
        enemy.transform.position = spawnSize;

        yield return new WaitForSeconds(enemySpawnDelay);

        StartCoroutine("EnemySpawn2");
    }
    public IEnumerator EnemySpawn3()
    {
        GameObject enemy = enemyPooler.SpawnPrefab("Golem");
        enemy.transform.position = spawnSize;

        yield return new WaitForSeconds(enemySpawnDelay);

        StartCoroutine("EnemySpawn3");
    }

    void Update()
    {
        
    }
}
