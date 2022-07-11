using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerStatus : MonoBehaviour
{
    [SerializeField] PlayerAttack playerAttack;
    [SerializeField] PlayerMove playerMove;

    public void AttackPowerChange(bool increase)
    {
        if (increase)
        {
            playerAttack.AttackPower += 0.2f;
        }
    }

    public void AgilityChange(bool increase)
    {
        if (increase)
        {
            playerMove.Agility += 0.2f;
        }
    }
}
