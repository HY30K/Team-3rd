using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using DG.Tweening;

public class Skill2 : MonoBehaviour
{
    [SerializeField] Player player;
    [SerializeField] private float dashMaxCoolDown = 2;
    float dashCurrentCoolDown;

    public float DashmaxCoolDown => dashMaxCoolDown; 
    public float DashCurrentCoolDown => dashCurrentCoolDown;
    void Update()
    {
        Skill();
        if (dashCurrentCoolDown <= 0)
        {
            dashCurrentCoolDown = 0;
        }
    }
    private void Skill()
    {
        if (dashCurrentCoolDown <= 0)
        {
            if (Input.GetKeyDown(KeyCode.LeftShift))
            {
                if (player.MoveDirection.x != 0 || player.MoveDirection.y != 0)
                {
                    transform.DOLocalMove(new Vector3(player.MoveDirection.x * 2, player.MoveDirection.y * 2), 0.3f).SetRelative();
                    dashCurrentCoolDown = dashMaxCoolDown;
                }
            }
        }
        dashCurrentCoolDown -= Time.deltaTime;
    }
}
