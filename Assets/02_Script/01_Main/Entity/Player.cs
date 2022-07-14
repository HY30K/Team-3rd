using DG.Tweening;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class Player : MonoBehaviour, IDamage
{
    public static Player instance;
    [SerializeField] private Animator anim;
    public enum LayerName
    {
        IdleLayer = 0,
        WalkLayer = 1,
        AttackLayer = 2,
        DashLayer = 3
    }
    

    #region 공격 관련 변수
    [Header("공격 관련 변수")]
    [SerializeField] private Vector2 rangeSize;
    [SerializeField] private LayerMask enemyLayer;
    [SerializeField] private Image atkGauge;
    [SerializeField] private Transform rangeTransform;
    [SerializeField] private float atkDelayMax;
    private int atkLevel;
    private Coroutine attackRoutine;
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
    [SerializeField] private float agiSkillDelayMax;
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

    private bool isMove;
    private bool isAttack;
    private bool isDash;
    private bool isIdle;

    float animDelay;

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

        agiDelay = agiDelayMax;
        hpCurrent = hpLevel * 10;
    }
    private void Start()
    {
        anim = GetComponent<Animator>();
    }

    private void Update()
    {
        ATK();
        AGI();
        HP();
        StatusGauge();
        StatusDelay();
        StatusLimit();
        HandleLayers();

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
        if (atkDelay <= 0.001f && Input.GetKeyDown(KeyCode.K) && !isDash)
        {
            isAttack = true;
            foreach (Collider2D enemy in Physics2D.OverlapBoxAll(rangeTransform.position, rangeSize, 0, enemyLayer))
            {
                enemy.GetComponent<Enemy>().OnDamage(atkLevel);
                AudioManager.instance.SFXS[2].Play();

                if (atkLevel < 10)
                {
                    atkLevel++;
                }
                else
                {
                    atkSkillLevel++;
                }
            }
            // AudioManager.instance.SFXS[1].Play();
        }

        if (isAttack)
        {
            animDelay -= Time.deltaTime;
        }

        if (animDelay <= 0.001f)
        {
            isAttack = false;
            animDelay = 0.5f;
            atkDelay = atkDelayMax;
        }
    }

    float dirDelay;

    private void AGI()
    {
        dirDelay -= Time.deltaTime;

        moveDirection = new Vector2(Input.GetAxisRaw("Horizontal"), Input.GetAxisRaw("Vertical"));

        if (!isDash)
        {
            gameObject.GetComponent<Rigidbody2D>().velocity = moveDirection.normalized * agiLevel;
        }

        if (moveDirection.x != 0 || moveDirection.y != 0)
        {
            agiDelay -= Time.deltaTime;

            if (dirDelay <= 0)
            {
                rangeTransform.localPosition = moveDirection;
                dirDelay = 0.1f;
            }

            isIdle = false;
            isMove = true;
        }
        else
        {
            isMove = false;
            isIdle = true;
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
    public void HandleLayers()
    {
        anim.SetFloat("atkX", rangeTransform.localPosition.x);
        anim.SetFloat("atkY", rangeTransform.localPosition.y);
        if (isAttack)
        {
            ActivateLayer(LayerName.AttackLayer);
        }
        else
        {
            if (isMove)
            {
                if (!isDash)
                {
                    ActivateLayer(LayerName.WalkLayer);
                    anim.SetFloat("x", moveDirection.x);
                    anim.SetFloat("y", moveDirection.y);
                }
                else
                {
                    ActivateLayer(LayerName.DashLayer);
                    anim.SetFloat("x", moveDirection.x);
                    anim.SetFloat("y", moveDirection.y);
                }
            }
            else if (isIdle)
            {
                ActivateLayer(LayerName.IdleLayer);
            }
        }
    }
    public void ActivateLayer(LayerName layerName)
    {
        for(int i =0; i< anim.layerCount; i++)
        {
            anim.SetLayerWeight(i, 0);
        }
        anim.SetLayerWeight((int)layerName, 1);
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

                AudioManager.instance.SFXS[3].Play();

                atkSkillDelay = atkSkillDelayMax;
            }
        }
    }

    float dashCool;
    private void AGISkill()
    {
        agiSkillDelay -= Time.deltaTime;
        if (agiSkillDelay <= 0.001f && Input.GetKey(KeyCode.LeftShift) && isMove)
        {
            isDash = true;
            gameObject.GetComponent<Rigidbody2D>().velocity = moveDirection.normalized * (agiLevel + agiSkillLevel * 10);
            //AudioManager.instance.SFXS[4].Play();
        }

        if (isDash)
        {
            dashCool -= Time.deltaTime;
        }

        if (dashCool <= 0.001f)
        {
            isDash = false;
            dashCool = 0.1f;
            agiSkillDelay = agiSkillDelayMax;
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
        //agiSkillDelayMax = 4.0f - agiSkillLevel * 0.25f; 
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
