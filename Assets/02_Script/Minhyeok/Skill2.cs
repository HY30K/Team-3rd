using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using DG.Tweening;

public class Skill2 : MonoBehaviour
{
    [SerializeField] float AddPower;
    [SerializeField] PlayerMove playerMove;
    [SerializeField] private float dashLate = 2;
    public float coolTime;
    Rigidbody2D rigid;
    void Update()
    {
        Skill();
        if (coolTime <= 0)
        {
            coolTime = 0;
        }
    }
    void Skill()
    {
        if (coolTime <= 0)
        {
            if (Input.GetKeyDown(KeyCode.LeftShift))
            {
                if (playerMove.X != 0 || playerMove.Y != 0)
                    transform.DOLocalMove(new Vector3(playerMove.X * 2, playerMove.Y * 2), 0.3f).SetRelative();
                coolTime = dashLate;
            }
        }
        coolTime -= Time.deltaTime;
    }
}
