using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BossRoom : MonoBehaviour
{
    [SerializeField] private Transform bossStageTarget; // �̵��� Ÿ�� ����

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.gameObject.CompareTag("Player"))
        {
            GameObject.Find("Player").transform.position = bossStageTarget.transform.position;
        }
    }
}

