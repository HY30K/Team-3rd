using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class CoolDownUI : MonoBehaviour
{
    [SerializeField] Image fill;
    [SerializeField] GameObject player;
    float currentCoolDown;
    bool useSkill = false;

    private void Update()
    {
        if (gameObject.name == "Skill1UI")
        {
            fill.fillAmount = player.GetComponent<Skill1>().CurrentCoolDown / player.GetComponent<Skill1>().MaxCoolDown;
        }
        if(gameObject.name == "Skill2UI")
        {
            fill.fillAmount = player.GetComponent<Skill2>().DashCurrentCoolDown / player.GetComponent<Skill2>().DashmaxCoolDown;
        }
    }
}
