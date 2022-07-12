using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class CoolDownUI : MonoBehaviour
{
    [SerializeField] Image imageCoolDown;
    [SerializeField] Player player;

    private void Update()
    {
        if (gameObject.name == "ATKSkillDelay")
        {
            imageCoolDown.fillAmount = player.ATKSkillDelay / player.ATKSkillDelayMax;
        }
        if(gameObject.name == "AGISkillDelay")
        {
            imageCoolDown.fillAmount = player.AGISkillDelay / player.AGISkillDelayMax;
        }
    }
}
