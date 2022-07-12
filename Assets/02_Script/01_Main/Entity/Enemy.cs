using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Enemy : MonoBehaviour, IDamage
{
    [SerializeField] private Vector2 attackRangeSize;
    [SerializeField] private LayerMask playerLayer;
    [SerializeField] private Transform attackRangeTransform;
    [SerializeField] private Transform detectRangeTransform;
    [SerializeField] private Rigidbody2D rigidbody2D;
    [SerializeField] private float detectRangeSize;
    [SerializeField] private float hpMax;
    [SerializeField] private Vector2 moveDirection;
    private GameObject playerObject;
    private Player playerScript;
    private ObjectPooler enemyPooler;
    private float atk;
    private float atkDelay;
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

    private void Update()
    {
        ATK();
        AGI();

        atk = playerScript.Atk;
        agi = playerScript.Agi;
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
        atkDelay += Time.deltaTime;

        if (atkDelay >= 6.0f - atk / 2.0f)
        {
            if (Physics2D.OverlapBox(attackRangeTransform.position, attackRangeSize, 0, playerLayer))
            {
                Collider2D player = Physics2D.OverlapBox(attackRangeTransform.position, attackRangeSize, 0, playerLayer);

                player.GetComponent<Player>().OnDamage(0.5f + atk);

                atkDelay = 0.0f;
            }
        }
    }

    private void AGI()
    {
        if (Physics2D.OverlapCircle(detectRangeTransform.position, detectRangeSize, playerLayer))
        {
            moveDirection = playerObject.transform.position - transform.position;
        }
        else
        {
            moveDirection = Vector2.zero;
        }

        transform.Translate(moveDirection.normalized * (0.5f + agi) * Time.deltaTime);
        attackRangeTransform.localPosition = moveDirection.normalized;
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
