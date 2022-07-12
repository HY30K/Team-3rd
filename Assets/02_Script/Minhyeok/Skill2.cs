using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using DG.Tweening;

public class Skill2 : MonoBehaviour
{
    [SerializeField] float AddPower;
    [SerializeField] Player playerMove;
    [SerializeField] private float dashMaxCoolDown = 2;
    float dashCurrentCoolDown;
    Rigidbody2D rigid;

    public float DashmaxCoolDown => dashMaxCoolDown; 
    public float DashCurrentCoolDown => dashCurrentCoolDown;
    void Update()
    {
        //Skill();
        if (dashCurrentCoolDown <= 0)
        {
            dashCurrentCoolDown = 0;
        }
    }
    /*private void Skill()
    {
        if (dashCurrentCoolDown <= 0)
        {
            if (Input.GetKeyDown(KeyCode.LeftShift))
            {
                if (playerMove.X != 0 || playerMove.Y != 0)
                {
                    transform.DOLocalMove(new Vector3(playerMove.X * 2, playerMove.Y * 2), 0.3f).SetRelative();
                    dashCurrentCoolDown = dashMaxCoolDown;
                }
            }
        }
        dashCurrentCoolDown -= Time.deltaTime;
    }*/
}
