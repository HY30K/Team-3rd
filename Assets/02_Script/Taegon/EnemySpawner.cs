using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemySpawner : MonoBehaviour
{
    [SerializeField] private StageData stageData;
    [SerializeField] private ObjectPooler enemyPooler;
    [SerializeField] private float enemySpawnDelay;

    private void Start()
    {
        StartCoroutine("EnemySpawn");
    }

    private IEnumerator EnemySpawn()
    {
        float x = Random.Range(stageData.LimitMin.x, stageData.LimitMax.x);
        float y = Random.Range(stageData.LimitMin.y, stageData.LimitMax.y);

        GameObject enemy = enemyPooler.SpawnPrefab("Enemy");
        enemy.transform.position = new Vector2(x, y);

        yield return new WaitForSeconds(enemySpawnDelay);
    }
}
