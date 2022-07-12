using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Enemy : MonoBehaviour, IDamage
{
    #region 공격 관련 변수
    [SerializeField] private Vector2 attackRangeSize;
    [SerializeField] private Transform attackRangeTransform;
    [SerializeField] private float atkDelayMax;
    private float atk;
    private float atkDelay;
    #endregion
    #region 이동 관련 변수
    [SerializeField] private Vector2 moveDirection;
    [SerializeField] private Transform detectRangeTransform;
    [SerializeField] private float detectRangeSize;
    private float agi;
    #endregion
    #region 체력 관련 변수
    [SerializeField] private float hpMax;
    private ObjectPooler enemyPooler;
    private float hpCurrent;
    #endregion
    #region 플레이어 관련 변수
    [SerializeField] private LayerMask playerLayer;
    private GameObject playerObject;
    private Player playerScript;
    #endregion

    private void Awake()
    {
        playerObject = GameObject.Find("Player");
        playerScript = playerObject.GetComponent<Player>();
        enemyPooler = GameObject.Find("EnemySpawner").GetComponent<ObjectPooler>();
        atkDelay = atkDelayMax - atk / 2.0f;
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

        atk = playerScript.ATKCurrent;
        agi = playerScript.AGICurrent;
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
            Collider2D player = Physics2D.OverlapBox(attackRangeTransform.position, attackRangeSize, 0, playerLayer);

            player.GetComponent<Player>().OnDamage(0.5f + atk);

            atkDelay = atkDelayMax - atk / 2.0f;
        }
    }

    private void AGI()
    {
        if (Physics2D.OverlapCircle(detectRangeTransform.position, detectRangeSize, playerLayer))
        {
            moveDirection = playerObject.transform.position - transform.position;

            moveDirection.Normalize();
        }
        else
        {
            moveDirection = Vector2.zero;
        }

        attackRangeTransform.localPosition = moveDirection;

        transform.Translate(moveDirection * (0.5f + agi) * Time.deltaTime);
    }

    public void OnDamage(float damage)
    {
        hpCurrent -= damage;

        if (hpCurrent <= 0.001f)
        {
            enemyPooler.DespawnPrefab(gameObject);
        }
    }
}
