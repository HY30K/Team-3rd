using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerAttack : MonoBehaviour
{
    [SerializeField] Transform pos; // 충돌감지 상자 위치
    [SerializeField] Vector2 boxSize; // 충돌감지 상자 크기
    [SerializeField] float damage;
    void Start()
    {

    }
    void Update()
    {
        Attack();
    }
    private void Attack()
    {
        if (Input.GetKeyDown(KeyCode.K))
        {
            Collider2D [] collider2D = Physics2D.OverlapBoxAll(pos.position, boxSize, 0); //Enemy 태그 닿으면 데미지 -1
            foreach (Collider2D collider in collider2D)
            {
                if(collider.tag == "Enemy")
                {
                    collider.GetComponent<IDamage>().OnDamage(damage);
                }
            }
        }
    }
}
