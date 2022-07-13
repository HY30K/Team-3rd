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
    [SerializeField] private Animator anim;
    [SerializeField] private SpriteRenderer spriteRenderer;
    #endregion

    private void Awake()
    {
        playerObject = GameObject.Find("Player");
        enemyPooler = GameObject.Find("EnemySpawner").GetComponent<ObjectPooler>();
    }

    private void OnEnable()
    {
        moveDirection = Vector2.zero;
        hpCurrent = hpMax;
    }

    private void Update()
    {
        ATK();
        AGI();

        if (playerObject.transform.position.x < transform.position.x)
        {
            spriteRenderer.flipX = true;
        }
        else
        {
            spriteRenderer.flipX = false;
        }
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
            anim.SetTrigger("lsAttack");

            Collider2D player = Physics2D.OverlapBox(attackRangeTransform.position, attackRangeSize, 0, playerLayer);

            player.GetComponent<Player>().OnDamage(0.5f + atk);


            atkDelay = atkDelayMax;

        }
    }

    private void AGI()
    {
        if (Physics2D.OverlapCircle(detectRangeTransform.position, detectRangeSize, playerLayer))
        {
            moveDirection = playerObject.transform.position - transform.position;

            moveDirection.Normalize();

            anim.SetBool("lsWalk", true);
            

        }
        else
        {
            moveDirection = Vector2.zero;
            anim.SetBool("lsIdle", true);
        }

        gameObject.GetComponent<Rigidbody2D>().velocity = moveDirection.normalized * agi;
        attackRangeTransform.localPosition = moveDirection;
    }

    public void OnDamage(float damage)
    {
        hpCurrent -= damage;
        if (hpCurrent <= 0.001f)
        {
            anim.SetTrigger("lsDeath");
            enemyPooler.DespawnPrefab(gameObject);
        }
    }
}