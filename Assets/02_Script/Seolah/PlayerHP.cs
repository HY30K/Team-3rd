using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class PlayerHP : MonoBehaviour, IDamage
{
    [SerializeField] private float maxHp = 0f;
    private Image hpGauge = null;
    private float currentHp = 0f;

    private void Awake()
    {
        currentHp = maxHp;
        hpGauge = GameObject.Find("Canvas/Image").GetComponent<Image>();
    }
    private void Update()
    {
        PlayerHp();
    }
    private void PlayerHp()
    {
        hpGauge.fillAmount = (float)currentHp / maxHp;
    }
    public void OnDamage(float damage)
    {
        currentHp -= damage;

        if (currentHp <= 0)
        {
            
        }
    }
}
