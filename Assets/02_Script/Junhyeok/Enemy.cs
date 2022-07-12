using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Enemy : MonoBehaviour
{
    [Header("적 속도")]
    [SerializeField] private float speed;
    [SerializeField] LayerMask playerLayer = 1 << 7;
    private Collider2D col = null;
    private Rigidbody2D rb = null;
    private Animator animator;
    private SpriteRenderer spriteRenderer;
    private Vector2 dir;

    private Transform Player;

    private void Awake()
    {
        col = GetComponent<Collider2D>();
        rb = GetComponent<Rigidbody2D>();
        animator = GetComponent<Animator>();
        spriteRenderer = GetComponent<SpriteRenderer>();
        Player = GameObject.Find("Player").GetComponent<Transform>();
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

        //플레이어없으면 위치 초기화
        if (Player == null)
        {
            dir = Vector2.zero;
        }
    }

    private void Move()
    {
        transform.Translate(dir.normalized * speed * Time.deltaTime);
    }
}
