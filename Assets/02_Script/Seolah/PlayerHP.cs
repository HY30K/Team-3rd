using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class PlayerHP : MonoBehaviour
{
    [SerializeField] private float MaxHP = 0f;
    private float CurrentHP = 0f;
    private Image HPGage = null;

    private void Awake()
    {
        CurrentHP = MaxHP;
        HPGage = GameObject.Find("Canvas/Image").GetComponent<Image>();
    }
    private void Update()
    {
        PlayerHp();
    }
    private void PlayerHp()
    {
        HPGage.fillAmount = CurrentHP / MaxHP;
    }
    public void OnDamage(float damage)
    {
        if (CurrentHP <= 0)
            CurrentHP -= damage;
        else { CurrentHP = 0; }
    }
}
