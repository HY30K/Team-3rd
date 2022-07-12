using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Enemy : MonoBehaviour, IDamage
{
    [Header("�� �ӵ�")]
    [SerializeField] private float speed;
    [SerializeField] private float maxHp;
    [SerializeField] LayerMask playerLayer = 1 << 7;
    private ObjectPooler enemyPooler;
    private Collider2D col = null;
    private Rigidbody2D rb = null;
    private Animator animator;
    private SpriteRenderer spriteRenderer;
    private Vector2 dir;
    private float hp;

    private Transform Player;

    private void Awake()
    {
        col = GetComponent<Collider2D>();
        rb = GetComponent<Rigidbody2D>();
        animator = GetComponent<Animator>();
        spriteRenderer = GetComponent<SpriteRenderer>();
        Player = GameObject.Find("Player").GetComponent<Transform>();
        enemyPooler = GameObject.Find("EnemySpawner").GetComponent<ObjectPooler>();
        hp = maxHp;
    }

    private void Update()
    {
        if (Physics2D.OverlapCircle(transform.position, 5f, playerLayer))
        {
            Target();
        }
        
        Move();
    }

    private void Target()
    {
        dir = Player.transform.position - transform.position;

        //�÷��̾������ ��ġ �ʱ�ȭ
        if (Player == null)
        {
            dir = Vector2.zero;
        }
    }

    private void Move()
    {
        transform.Translate(dir.normalized * speed * Time.deltaTime);
    }

    public void OnDamage(float damage)
    {
        hp -= damage;

        if (hp <= 0)
        {
            enemyPooler.DespawnPrefab(gameObject);
        }
    }
}
