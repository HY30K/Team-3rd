using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Boss : MonoBehaviour, IDamage
{
    #region 공격 관련 변수
    [Header("공격 관련 변수")]
    [SerializeField] private Vector2 attackRangeSize;
    [SerializeField] private Transform attackRangeTransform;
    [SerializeField] private Transform detectRangeTransform;
    [SerializeField] private float detectRangeSize;
    [SerializeField] private float atkDelayMax;
    private Collider2D detectRange;
    private float atkDelay;
    private float atk;
    private float dashDelay;
    private bool isAttack;
    private bool isSecondPhase;
    private bool isDash;
    #endregion
    #region 이동 관련 변수
    [Header("이동 관련 변수")]
    [SerializeField] private Vector2 moveDirection;
    private float agi;
    #endregion
    #region 체력 관련 변수
    [Header("체력 관련 변수")]
    [SerializeField] private float hpMax;
    private ObjectPooler bossPooler;
    private float hpCurrent;
    #endregion
    #region 플레이어 관련 변수
    [Header("플레이어 관련 변수")]
    [SerializeField] private LayerMask playerLayer;
    private GameObject playerObject;
    #endregion

    private void Awake()
    {
        playerObject = GameObject.Find("Player");
        bossPooler = GameObject.Find("EnemySpawner").GetComponent<ObjectPooler>();
    }

    private void OnEnable()
    {
        moveDirection = Vector2.zero;
        hpCurrent = hpMax;
        detectRange = Physics2D.OverlapCircle(detectRangeTransform.position, detectRangeSize, playerLayer);
    }

    private void Update()
    {
        ATK();
        AGI();
        HP();
    }

    private void OnDrawGizmos()
    {
        Gizmos.color = Color.red;
        Gizmos.DrawCube(attackRangeTransform.position, attackRangeSize);
    }

    private void ATK()
    {
        atkDelay -= Time.deltaTime;

        if (atkDelay <= 0.001f)
        {
            isAttack = true;
        }
        else
        {
            isAttack = false;
        }

        if (isAttack)
        {
            int selector = Random.Range(1, 11);

            if (!isSecondPhase)
            {
                switch (selector)
                {
                    case 1:
                    case 2:
                    case 3:
                    case 4:
                    case 5:
                    case 6:
                        ATKNormal();
                        break;
                    case 7:
                    case 8:
                    case 9:
                    case 10:
                        ATKSkillDash();
                        break;
                }
            }
            else
            {
                switch (selector)
                {
                    case 1:
                    case 2:
                    case 3:
                    case 4:
                    case 5:
                        ATKNormal();
                        break;
                    case 6:
                    case 7:
                    case 8:
                        ATKSkillDash();
                        break;
                    case 9:
                    case 10:
                        ATKSkillCast();
                        break;
                }
            }
        }
    }

    private void AGI()
    {
        moveDirection = playerObject.transform.position - transform.position;

        if (!isDash || (!detectRange && isDash))
        {
            gameObject.GetComponent<Rigidbody2D>().velocity = moveDirection.normalized * agi;
        }

        attackRangeTransform.localPosition = moveDirection.normalized;
    }

    private void HP()
    {
        if (hpCurrent <= (hpMax / 2))
        {
            isSecondPhase = true;
        }
    }

    private void ATKNormal()
    {
        Collider2D player = Physics2D.OverlapBox(attackRangeTransform.position, attackRangeSize, 0, playerLayer);

        player.GetComponent<Player>().OnDamage(0.5f + atk);

        atkDelay = atkDelayMax;
    }

    private void ATKSkillDash()
    {
        isDash = true;

        if (detectRange)
        {
            gameObject.GetComponent<Rigidbody2D>().velocity = moveDirection.normalized * agi * 2;
        }

        if (isDash)
        {
            dashDelay -= Time.deltaTime;
        }

        if (dashDelay <= 0.001f)
        {
            isDash = false;
            dashDelay = 0.1f;
            atkDelay = atkDelayMax;
        }
    }

    private void ATKSkillCast()
    {
        atkDelay = atkDelayMax;
    }

    public void OnDamage(float damage)
    {
        hpCurrent -= damage;

        if (hpCurrent <= 0.001f)
        {
            bossPooler.DespawnPrefab(gameObject);
        }
    }
}