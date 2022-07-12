using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class CoolDownUI : MonoBehaviour
{
    [SerializeField] Image fill;
    [SerializeField] float maxCoolDown;
    [SerializeField] float currentCoolDown;
    bool UseSkill = false;

    private void Awake()
    {
        currentCoolDown = maxCoolDown;
    }

    public void SetMaxCoolDown(float value)
    {
        maxCoolDown = value;
        UpdateFillAmount();
        
    }
    public void SetCurrentCoolDown(float value)
    {
        currentCoolDown = value;
        UpdateFillAmount();

    }
    public void UpdateFillAmount()
    {
        fill.fillAmount = Mathf.Lerp(0,1,currentCoolDown/maxCoolDown);
    }
    private void Update()
    {
        if (Input.GetKeyDown(KeyCode.L))
            UseSkill = true;
        if(UseSkill == true)
        {
            currentCoolDown -= Time.deltaTime;
            UpdateFillAmount();
            if (currentCoolDown <= 0f)
            {
                currentCoolDown = maxCoolDown;
                UseSkill = false;
            }
        }
    }
}
