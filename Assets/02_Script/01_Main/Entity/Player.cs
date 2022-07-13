using DG.Tweening;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class Player : MonoBehaviour, IDamage
{
    public static Player instance;

    #region 공격 관련 변수
    [SerializeField] private Vector2 rangeSize;
    [SerializeField] private LayerMask enemyLayer;
    [SerializeField] private Image atkGauge;
    [SerializeField] private Transform rangeTransform;
    [SerializeField] private float atkMax;
    [SerializeField] private float atkDelayMax;
    private float atkCurrent;
    private float atkDelay;

    public float ATKCurrent
    {
        get { return atkCurrent; }
        set { atkCurrent = value; }
    }
    #endregion
    #region 이동 관련 변수
    [SerializeField] private Image agiGauge;
    [SerializeField] private float agiMax;
    [SerializeField] private float agiDelayMax;
    private Vector2 moveDirection;
    private float agiCurrent;
    private float agiDelay;

    public Vector2 MoveDirection => moveDirection;
    public float AGICurrent
    {
        get { return agiCurrent; }
        set { agiCurrent = value; }
    }
    #endregion
    #region 체력 관련 변수
    [SerializeField] private Image hpGauge;
    [SerializeField] private float hpMax;
    private float hpCurrent;
    #endregion
    #region 스킬 관련 변수
    [SerializeField] private ObjectPooler skillPooler;
    [SerializeField] private float atkSkillMax;
    [SerializeField] private float atkSkillDelayMax;
    [SerializeField] private float agiSkillDelayMax;
    private float atkSkillCurrent;
    private float atkSkillDelay;
    private float agiSkillDelay;

    public float ATKSkillMax => atkSkillMax;
    public float ATKSkillDelayMax => atkSkillDelayMax;
    public float AGISkillDelayMax => agiSkillDelayMax;
    public float ATKSkillCurrent
    {
        get { return atkSkillCurrent; }
        set { atkSkillCurrent = value; }
    }
    public float ATKSkillDelay => atkSkillDelay;
    public float AGISkillDelay => agiSkillDelay;
    #endregion


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

        atkDelay = atkDelayMax - atkCurrent / 4.0f;
        agiDelay = agiDelayMax;
        hpCurrent = hpMax;
        atkSkillDelay = atkSkillDelayMax;
        agiSkillDelay = agiSkillDelayMax;
    }

    private void Update()
    {
        ATK();
        AGI();
        HP();
        StatusLimit();

        if (atkCurrent >= atkMax)
        {
            ATKSkill();
        }

        if (agiCurrent >= agiMax)
        {
            AGISkill();
        }
    }

    private void OnDrawGizmos()
    {
        Gizmos.color = Color.red;
        Gizmos.DrawCube(rangeTransform.position, rangeSize);
    }

    private void ATK()
    {
        atkDelay -= Time.deltaTime;

        if (atkDelay <= 0.001f && Input.GetKeyDown(KeyCode.K))
        {
            foreach (Collider2D enemy in Physics2D.OverlapBoxAll(rangeTransform.position, rangeSize, 0, enemyLayer))
            {
                enemy.GetComponent<Enemy>().OnDamage(1.0f + atkCurrent);

                if (atkCurrent < atkMax)
                {
                    atkCurrent += 0.2f;
                }
                else
                {
                    atkSkillCurrent += 2.0f;
                }
            }

            atkDelay = atkDelayMax - atkCurrent / 4.0f; ;
        }
    }

    private void AGI()
    {
        moveDirection = new Vector2(Input.GetAxis("Horizontal"), Input.GetAxis("Vertical"));
        rangeTransform.localPosition = moveDirection;

        transform.Translate(moveDirection * (1.0f + agiCurrent) * Time.deltaTime);

        if (moveDirection.x != 0 || moveDirection.y != 0)
        {
            agiDelay -= Time.deltaTime;
        }

        if (agiDelay <= 0.001f)
        {
            if (agiCurrent < agiMax)
            {
                agiCurrent += 0.2f;
            }
            else
            {
                agiSkillDelayMax -= 0.06f;
            }

            agiDelay = agiDelayMax;
        }
    }

    private void HP()
    {
        atkGauge.fillAmount = atkCurrent / atkMax;
        agiGauge.fillAmount = agiCurrent / agiMax;
        hpGauge.fillAmount = hpCurrent / hpMax;
    }

    private void ATKSkill()
    {
        atkSkillDelay -= Time.deltaTime;

        if (atkSkillDelay <= 0.001f && Input.GetKeyDown(KeyCode.L))
        {
            if (moveDirection.x != 0 || moveDirection.y != 0)
            {
                GameObject prefab = skillPooler.SpawnPrefab("Fireball");
                prefab.transform.position = transform.position;

                prefab.GetComponent<Rigidbody2D>().AddForce(new Vector2(moveDirection.x, moveDirection.y) * 20, ForceMode2D.Impulse);
            }

            atkSkillDelay = atkSkillDelayMax;
        }
    }

    private void AGISkill()
    {
        agiSkillDelay -= Time.deltaTime;

        if (agiSkillDelay <= 0.001f && Input.GetKeyDown(KeyCode.LeftShift))
        {
            if (moveDirection.x != 0 || moveDirection.y != 0)
            {
                transform.DOLocalMove(new Vector3(moveDirection.x * 2, moveDirection.y * 2), 0.3f).SetRelative();
            }

            agiSkillDelay = agiSkillDelayMax;
        }
    }

    private void StatusLimit()
    {
        atkCurrent = Mathf.Clamp(atkCurrent, 0, atkMax);
        agiCurrent = Mathf.Clamp(agiCurrent, 0, agiMax);
        hpCurrent = Mathf.Clamp(hpCurrent, 0, hpMax);
        atkSkillCurrent = Mathf.Clamp(atkSkillCurrent, 0, atkSkillMax);
        agiSkillDelayMax = Mathf.Clamp(agiSkillDelayMax, 0.5f, 3.0f);
    }

    public void OnDamage(float damage)
    {
        hpCurrent -= damage;

        if (atkSkillCurrent > 0)
        {
            atkSkillCurrent -= 2.0f;
        }
        else
        {
            atkCurrent -= 0.2f;
        }

        if (agiSkillDelayMax < 3.0f)
        {
            agiSkillDelayMax += 0.06f;
        }
        else
        {
            agiCurrent -= 0.2f;
        }

        if (hpCurrent <= 0.001f)
        {
            SceneManager.LoadScene("03_GameOver");
        }
    }
}
