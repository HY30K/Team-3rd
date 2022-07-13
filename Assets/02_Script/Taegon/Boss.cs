using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Boss : MonoBehaviour, IDamage
{
    #region ���� ���� ����
    [Header("���� ���� ����")]
    [SerializeField] private Vector2 attackRangeSize;
    [SerializeField] private Transform attackRangeTransform;
    [SerializeField] private float atkDelayMax;
    private float atk;
    private float atkDelay;
    #endregion
    #region �̵� ���� ����
    [Header("�̵� ���� ����")]
    [SerializeField] private Vector2 moveDirection;
    private float agi;
    #endregion
    #region ü�� ���� ����
    [Header("ü�� ���� ����")]
    [SerializeField] private float hpMax;
    private ObjectPooler bossPooler;
    private float hpCurrent;
    #endregion
    #region �÷��̾� ���� ����
    [Header("�÷��̾� ���� ����")]
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
    }

    private void ATK()
    {
        atkDelay -= Time.deltaTime;

        if (atkDelay <= 0.001f && Physics2D.OverlapBox(attackRangeTransform.position, attackRangeSize, 0, playerLayer))
        {
            Collider2D player = Physics2D.OverlapBox(attackRangeTransform.position, attackRangeSize, 0, playerLayer);

            player.GetComponent<Player>().OnDamage(0.5f + atk);

            atkDelay = atkDelayMax;
        }
    }

    private void AGI()
    {
        moveDirection = playerObject.transform.position - transform.position;
        gameObject.GetComponent<Rigidbody2D>().velocity = moveDirection.normalized * agi;
        attackRangeTransform.localPosition = moveDirection.normalized;
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