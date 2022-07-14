using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class StageLock : MonoBehaviour
{
    [SerializeField] private Transform stagePos;
    [SerializeField] private Vector2 size;
    public RandomSpawner randomSpanwer;
/*    private Coroutine coroutine;
    private Coroutine coroutine2;
    private Coroutine coroutine3;*/

    public enum StageLockType
    {
        stage1,
        stage2,
        stage3,
        shop,
        boss
    }
    public StageLockType lockType;
    // Start is called before the first frame update
    void Start()
    {
        RaycastHit2D hit = Physics2D.BoxCast(stagePos.position, size, 0, new Vector2(1, 0), 5f);
    }
    void OnDrawGizmos()
    {
        Gizmos.color = Color.red;
        Gizmos.DrawWireCube(transform.position, size);
    }
    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.gameObject.CompareTag("Player"))
        {
            Debug.Log(lockType);
            /*if (StageLockType.stage1 == lockType)
            {
                coroutine = StartCoroutine(randomSpanwer.EnemySpawn());
                *//*StopCoroutine(coroutine2);
                StopCoroutine(coroutine3);*//*
            }
            else if (StageLockType.stage2 == lockType)
            {
                coroutine2 = StartCoroutine(randomSpanwer.EnemySpawn2());
                *//*StopCoroutine(coroutine);
                StopCoroutine(coroutine3);*//*
            }
            else if (StageLockType.stage3 == lockType)
            {
                coroutine3 = StartCoroutine(randomSpanwer.EnemySpawn3());
                *//*StopCoroutine(coroutine);
                StopCoroutine(coroutine2);*//*
            }
            else
            {
                StopCoroutine(coroutine);
                StopCoroutine(coroutine2);
                StopCoroutine(coroutine3);
            }*/
        }
    }
}
