using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemySpawner : MonoBehaviour
{
    [SerializeField] private StageData stageData;
    [SerializeField] private StageData stageData2;
    [SerializeField] private StageData stageData3;
    [SerializeField] private ObjectPooler enemyPooler;
    [SerializeField] private float enemySpawnDelay;

    private void Start()
    {
        StartCoroutine("EnemySpawn");
        StartCoroutine("EnemySpawn2");
        StartCoroutine("EnemySpawn3");
    }

    private IEnumerator EnemySpawn()
    {
        float x = Random.Range(stageData.LimitMin.x, stageData.LimitMax.x);
        float y = Random.Range(stageData.LimitMin.y, stageData.LimitMax.y);

        GameObject enemy = enemyPooler.SpawnPrefab("Goblin");
        enemy.transform.position = new Vector2(x, y);

        yield return new WaitForSeconds(enemySpawnDelay);

        StartCoroutine("EnemySpawn");
    }
    private IEnumerator EnemySpawn2()
    {
        float x2 = Random.Range(stageData2.LimitMin.x, stageData2.LimitMax.x);
        float y2 = Random.Range(stageData2.LimitMin.y, stageData2.LimitMax.y);

        GameObject enemy = enemyPooler.SpawnPrefab("Skelleton");
        enemy.transform.position = new Vector2(x2, y2);

        yield return new WaitForSeconds(enemySpawnDelay);

        StartCoroutine("EnemySpawn2");
    }
    private IEnumerator EnemySpawn3()
    {
        float x3 = Random.Range(stageData3.LimitMin.x, stageData3.LimitMax.x);
        float y3 = Random.Range(stageData3.LimitMin.y, stageData3.LimitMax.y);

        GameObject enemy = enemyPooler.SpawnPrefab("Golem");
        enemy.transform.position = new Vector2(x3, y3);

        yield return new WaitForSeconds(enemySpawnDelay);

        StartCoroutine("EnemySpawn3");
    }
}
