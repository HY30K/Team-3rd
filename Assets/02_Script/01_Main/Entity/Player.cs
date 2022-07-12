using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class Player : MonoBehaviour, IDamage
{
    public static Player instance;

    [SerializeField] private Vector2 rangeSize;
    [SerializeField] private LayerMask enemyLayer;
    [SerializeField] private Transform rangeTransform;
    [SerializeField] private Rigidbody2D rigidbody2D;
    [SerializeField] private Image hpGauge;
    [SerializeField] private float hpMax;
    private Vector2 moveDirection;
    private float atk;
    private float atkDelay;
    private float agi;
    private float agiDelay;
    private float hpCurrent;

    public Vector2 MoveDirection => moveDirection;
    public float Atk => atk;
    public float Agi => agi;

    private void Awake()
    {
        if (instance == null)
        {
            instance = this;
        }
        else
        {
            Destroy(gameObject);
        }

        hpCurrent = hpMax;
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
        Gizmos.DrawCube(rangeTransform.position, rangeSize);
    }

    private void ATK()
    {
        atkDelay += Time.deltaTime;

        if (atkDelay >= 3.0f - atk / 4.0f && Input.GetKeyDown(KeyCode.K))
        {
            foreach (Collider2D enemy in Physics2D.OverlapBoxAll(rangeTransform.position, rangeSize, 0, enemyLayer))
            {
                enemy.GetComponent<Enemy>().OnDamage(1.0f + atk);

                if (atk < 10.0f)
                {
                    atk += 0.2f;
                }
                else
                {
                    atk = 10.0f;
                    //스킬 성장으로 전환
                }
            }

            atkDelay = 0.0f;
        }
    }

    private void AGI()
    {
        moveDirection = new Vector2(Input.GetAxis("Horizontal"), Input.GetAxis("Vertical"));
        rangeTransform.localPosition = moveDirection;
        transform.Translate(moveDirection.normalized * (1.0f + agi) * Time.deltaTime);

        if (moveDirection.x != 0 || moveDirection.y != 0)
        {
            agiDelay += Time.deltaTime;
        }

        if (agiDelay >= 5.0f)
        {
            if (agi < 10.0f)
            {
                agi += 0.2f;
            }
            else
            {
                agi = 10.0f;
                //스킬 성장으로 전환
            }

            agiDelay = 0.0f;
        }
    }

    private void HP()
    {
        hpGauge.fillAmount = hpCurrent / hpMax;
    }

    public void OnDamage(float damage)
    {
        hpCurrent -= damage;

        if (hpCurrent <= 0.001f)
        {
            SceneManager.LoadScene("03_GameOver");
        }
    }
}
