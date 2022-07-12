using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Enemy : MonoBehaviour, IDamage
{
    [SerializeField] private Vector2 attackRangeSize;
    [SerializeField] private Vector2 detectRangeSize;
    [SerializeField] private Transform attackRangeTransform;
    [SerializeField] private Transform detectRangeTransform;
    [SerializeField] private Rigidbody2D rigidbody2D;
    [SerializeField] private float hpMax;
    private Vector2 moveDirection;
    private GameObject playerObject;
    private Player playerScript;
    private ObjectPooler enemyPooler;
    private float atk;
    private float agi;
    private float hpCurrent;

    private void Awake()
    {
        playerObject = GameObject.Find("Player");
        playerScript = playerObject.GetComponent<Player>();
        enemyPooler = GameObject.Find("EnemySpawner").GetComponent<ObjectPooler>();
    }

    private void OnEnable()
    {
        moveDirection = Vector2.zero;
        hpCurrent = hpMax;
    }

    public void OnDamage(float damage)
    {
        hpCurrent -= damage;

        if (hpCurrent <= 0)
        {
            enemyPooler.DespawnPrefab(gameObject);
        }
    }
    /*[Header("적 속도")]
    [SerializeField] private float speed;
    [SerializeField] private float maxHP;
    [SerializeField] LayerMask playerLayer = 1 << 7;
    private Collider2D col;
    private Rigidbody2D rb;
    private SpriteRenderer spriteRenderer;
    private ObjectPooler enemyPooler;
    private Transform player;
    private PlayerAttack playerAttack;
    private PlayerMove playerMove;
    private Vector2 dir;
    private float currentHP;


    private void OnEnable()
    {
        col = GetComponent<Collider2D>();
        rb = GetComponent<Rigidbody2D>();
        spriteRenderer = GetComponent<SpriteRenderer>();
        enemyPooler = GameObject.Find("EnemySpawner").GetComponent<ObjectPooler>();
        player = GameObject.Find("Player").GetComponent<Transform>();
        playerAttack = GameObject.Find("Player").GetComponent<PlayerAttack>();
        playerMove = GameObject.Find("Player").GetComponent<PlayerMove>();
        currentHP = maxHP;
        dir = Vector2.zero;
    }

    private void Update()
    {
        if (Physics2D.OverlapCircle(transform.position, 5f, playerLayer))
        {
            Target();
        }
        
        Move();

        speed = playerMove.Agility;
    }

    private void Target()
    {
        dir = player.transform.position - transform.position;

        //플레이어없으면 위치 초기화
        if (player == null)
        {
            dir = Vector2.zero;
        }
    }

    private void Move()
    {
        transform.Translate(dir.normalized * (0.5f + speed) * Time.deltaTime);
    }

    public void OnDamage(float damage)
    {
        currentHP -= damage;

        if (currentHP <= 0)
        {
            enemyPooler.DespawnPrefab(gameObject);
        }
    }*/
}
