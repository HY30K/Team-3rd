using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class Player : MonoBehaviour, IDamage
{
    [SerializeField] private Vector2 rangeSize;
    [SerializeField] private Transform rangeTransform;
    [SerializeField] private Rigidbody2D rigidbody2D;
    [SerializeField] private Image hpGauge;
    [SerializeField] private float hpMax;
    private float atk;
    private float agi;
    private float agiDelay;
    private float hpCurrent;

    private void Awake()
    {
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
        Collider2D[] enemys = Physics2D.OverlapBoxAll(rangeTransform.position, rangeSize, 0);

        if (Input.GetKeyDown(KeyCode.K))
        {
            foreach (Collider2D enemy in enemys)
            {
                if (enemy.CompareTag("Enemy"))
                {
                    enemy.GetComponent<Enemy>().OnDamage(0.2f + atk);

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
            }
        }
    }

    private void AGI()
    {
        Vector2 moveDirection = new Vector2(Input.GetAxis("Horzontal"), Input.GetAxis("Vertical"));
        rangeTransform.localPosition = moveDirection;
        rigidbody2D.velocity = moveDirection.normalized * (0.2f + agi);

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

            agiDelay = 0;
        }
    }

    private void HP()
    {
        hpGauge.fillAmount = hpCurrent / hpMax;
    }

    public void OnDamage(float damage)
    {
        hpCurrent -= damage;

        if (hpCurrent <= 0)
        {
            SceneManager.LoadScene("03_GameOver");
        }
    }
}
