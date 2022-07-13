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
    [Header("공격 관련 변수")]
    [SerializeField] private Vector2 rangeSize;
    [SerializeField] private LayerMask enemyLayer;
    [SerializeField] private Image atkGauge;
    [SerializeField] private Transform rangeTransform;
    [SerializeField] private float atkDelayMax;
    AudioSource punch;
    AudioSource hit;
    AudioSource skill;
    AudioSource dash;
    private int atkLevel;
    public int ATKLevel
    {
        get { return atkLevel; }
        set { atkLevel = value; }
    }
    private float atkDelay;
    #endregion
    #region 이동 관련 변수
    [Header("이동 관련 변수")]
    [SerializeField] private Image agiGauge;
    [SerializeField] private float agiDelayMax;
    private Vector2 moveDirection;
    public Vector2 MoveDirection => moveDirection;
    private int agiLevel;
    public int AGILevel
    {
        get { return agiLevel; }
        set { agiLevel = value; }
    }
    private float agiDelay;
    #endregion
    #region 체력 관련 변수
    [Header("체력 관련 변수")]
    [SerializeField] private Image hpGauge;
    private int hpLevel = 1;
    private float hpCurrent;
    #endregion
    #region 스킬 관련 변수
    [Header("스킬 관련 변수")]
    [SerializeField] private ObjectPooler skillPooler;
    [SerializeField] private Image atkSkillGauge;
    [SerializeField] private Image agiSkillGauge;
    [SerializeField] private float atkSkillDelayMax;
    public float ATKSkillDelayMax => atkSkillDelayMax;
    private float agiSkillDelayMax;
    public float AGISkillDelayMax => agiSkillDelayMax;
    private int atkSkillLevel;
    public int ATKSkillLevel
    {
        get { return atkSkillLevel; }
        set { atkSkillLevel = value; }
    }
    private int agiSkillLevel;
    public int AGISkillLevel
    {
        get { return agiSkillLevel; }
        set { agiSkillLevel = value; }
    }
    private float atkSkillDelay;
    public float ATKSkillDelay => atkSkillDelay;
    private float agiSkillDelay;
    public float AGISkillDelay => agiSkillDelay;
    #endregion

    private void Awake()
    {
        punch = gameObject.GetComponent<AudioSource>();
        skill = GameObject.Find("SkillSound").GetComponent<AudioSource>();
        dash = GameObject.Find("DashSound").GetComponent<AudioSource>();

        if (instance == null)
        {
            instance = this;
        }
        else
        {
            Destroy(gameObject);
        }

        agiDelay = agiDelayMax;
        hpCurrent = hpLevel * 10;
    }

    private void Update()
    {
        ATK();
        AGI();
        HP();
        StatusGauge();
        StatusDelay();
        StatusLimit();

        if (atkLevel == 10)
        {
            ATKSkill();
        }
    }

    private void FixedUpdate()
    {
        if (agiLevel == 10)
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
                enemy.GetComponent<Enemy>().OnDamage(atkLevel);

                if (atkLevel < 10)
                {
                    atkLevel++;
                }
                else
                {
                    atkSkillLevel++;
                }
            }
            punch.Play();
            atkDelay = atkDelayMax;
        }
    }

    private void AGI()
    {
        agiSkillDelay -= Time.deltaTime;

        moveDirection = new Vector2(Input.GetAxisRaw("Horizontal"), Input.GetAxisRaw("Vertical"));

        if (!(agiSkillDelay <= 0.001f && Input.GetKeyDown(KeyCode.LeftShift)))
        {
            gameObject.GetComponent<Rigidbody2D>().velocity = moveDirection.normalized * agiLevel;
        }

        if (moveDirection.x != 0 || moveDirection.y != 0)
        {
            agiDelay -= Time.deltaTime;
            rangeTransform.localPosition = moveDirection;
        }

        if (agiDelay <= 0.001f)
        {
            if (agiLevel < 10)
            {
                agiLevel++;
            }
            else
            {
                agiSkillLevel++;
            }

            agiDelay = agiDelayMax;
        }
    }

    private void HP()
    {
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

                prefab.GetComponent<Rigidbody2D>().AddForce(moveDirection * 20, ForceMode2D.Impulse);
                
                skill.Play();

                atkSkillDelay = atkSkillDelayMax;
            }
        }
    }

    private void AGISkill()
    {
        if (agiSkillDelay <= 0.001f && Input.GetKeyDown(KeyCode.LeftShift) && (moveDirection.x != 0 || moveDirection.y != 0))
        {
            gameObject.GetComponent<Rigidbody2D>().velocity = moveDirection.normalized * (agiLevel + agiSkillLevel * 10);
            agiSkillDelay = agiSkillDelayMax;
            dash.Play();
        }
    }

    private void StatusGauge()
    {
        atkGauge.fillAmount = (float)atkLevel / 10;
        agiGauge.fillAmount = (float)agiLevel / 10;
        hpGauge.fillAmount = hpCurrent / (hpLevel * 10);
        atkSkillGauge.fillAmount = (float)atkSkillLevel / 10;
        agiSkillGauge.fillAmount = (float)agiSkillLevel / 10;
    }

    private void StatusDelay()
    {
        atkDelayMax = 3.0f - atkLevel * 0.25f;
        agiSkillDelayMax = 4.0f - agiSkillLevel * 0.25f; 
    }

    private void StatusLimit()
    {
        atkLevel = Mathf.Clamp(atkLevel, 1, 10);
        agiLevel = Mathf.Clamp(agiLevel, 1, 10);
        hpCurrent = Mathf.Clamp(hpCurrent, 0, hpLevel * 10);
        atkSkillLevel = Mathf.Clamp(atkSkillLevel, 0, 10);
        agiSkillLevel = Mathf.Clamp(agiSkillLevel, 0, 10);
    }

    public void OnDamage(float damage)
    {
        hpCurrent -= damage;

        if (atkSkillLevel > 0)
        {
            atkSkillLevel--;
        }
        else
        {
            atkLevel--;
        }

        if (agiSkillLevel > 0)
        {
            agiSkillLevel--;
        }
        else
        {
            agiLevel--;
        }

        if (hpCurrent <= 0.001f)
        {
            SceneManager.LoadScene("03_GameOver");
        }
    }
}
