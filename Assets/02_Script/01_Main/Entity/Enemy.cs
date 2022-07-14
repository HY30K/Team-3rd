using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Enemy : MonoBehaviour, IDamage
{
    #region 공격 관련 변수
    [Header("공격 관련 변수")]
    [SerializeField] private Vector2 attackRangeSize;
    [SerializeField] private Transform attackRangeTransform;
    [SerializeField] private float atk;
    [SerializeField] private float atkDelayMax;
    private float atkDelay;
    private SpriteRenderer spriteRenderer;
    #endregion
    #region 이동 관련 변수
    [Header("이동 관련 변수")]
    [SerializeField] private Vector2 moveDirection;
    [SerializeField] private Transform detectRangeTransform;
    [SerializeField] private float detectRangeSize;
    [SerializeField] private float agi;
    #endregion
    #region 체력 관련 변수
    [Header("체력 관련 변수")]
    [SerializeField] private float hpMax;
    private ObjectPooler enemyPooler;
    private float hpCurrent;
    #endregion
    #region 플레이어 관련 변수
    [Header("플레이어 관련 변수")]
    [SerializeField] private LayerMask playerLayer;
    private GameObject playerObject;
    private Animator anim = null;
    #endregion

    private void Awake()
    {
        playerObject = GameObject.Find("Player");
        enemyPooler = GameObject.Find("EnemySpawner").GetComponent<ObjectPooler>();
        anim = GetComponent<Animator>();
        spriteRenderer = GetComponent<SpriteRenderer>();
    }

    private void OnEnable()
    {
        moveDirection = Vector2.zero;
        hpCurrent = hpMax;
        atkDelay = atkDelayMax;
    }

    private void Update()
    {
        ATK();
        AGI();
    }

    private void OnDrawGizmos()
    {
        Gizmos.color = Color.red;
        Gizmos.DrawCube(attackRangeTransform.position, attackRangeSize);
        Gizmos.color = Color.green;
        Gizmos.DrawWireSphere(detectRangeTransform.position, detectRangeSize);
    }

    private void ATK()
    {
        atkDelay -= Time.deltaTime;

        if (atkDelay <= 0.001f && Physics2D.OverlapBox(attackRangeTransform.position, attackRangeSize, 0, playerLayer))
        {
            anim.SetTrigger("isAttack");

            Collider2D player = Physics2D.OverlapBox(attackRangeTransform.position, attackRangeSize, 0, playerLayer);

            player.GetComponent<Player>().OnDamage(0.5f + atk);

            atkDelay = atkDelayMax;
        }
    }

    private void AGI()
    {
        if (hpCurrent > 0.001f)
        {
            if (Physics2D.OverlapCircle(detectRangeTransform.position, detectRangeSize, playerLayer))
            {
                anim.SetBool("isStop", false);
                anim.SetBool("isWalk", true);

                moveDirection = playerObject.transform.position - transform.position;
                if (transform.position.x > playerObject.transform.position.x)
                {
                    spriteRenderer.flipX = true;
                }
                else if (transform.position.x < playerObject.transform.position.x)
                {
                    spriteRenderer.flipX = false;
                }
                else
                {
                    anim.SetBool("isStop", true);
                }

                moveDirection.Normalize();
            }
            else
            {
                anim.SetBool("isWalk", false);
                anim.SetBool("isStop", true);
                moveDirection = Vector2.zero;
            }
        }    

        gameObject.GetComponent<Rigidbody2D>().velocity = moveDirection.normalized * agi;
        attackRangeTransform.localPosition = moveDirection;
    }

    public void OnDamage(float damage)
    {
        hpCurrent -= damage;

        if (hpCurrent <= 0.001f)
        {
            moveDirection = Vector2.zero;
            anim.SetTrigger("isDead");
            Invoke("isDead", 1);

        }
    }

    public void isDead()
    {
        enemyPooler.DespawnPrefab(gameObject);
        PlayerMoney.money++;
    }
}