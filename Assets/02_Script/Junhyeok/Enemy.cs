using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Enemy : MonoBehaviour
{
    [Header("Àû ¼Óµµ")]
    [SerializeField] private float speed;
    [SerializeField] private Vector2 range;
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
        Target();
        Move();
    }

    private void Target()
    {
        if (Player.transform.position.x > transform.position.x)
        {

            dir = new Vector2(Player.transform.position.x - transform.position.x - 0.01f, 0);

        }
        else if (Player.transform.position.x < transform.position.x)
        {

            dir = new Vector2(Player.transform.position.x - transform.position.x + 0.01f, 0);
        }
        else if (Player == null)
        {

            dir = Vector2.zero;

        }
    }

    private void Move()
    {
        transform.Translate(dir * speed * Time.deltaTime);
    }
}
