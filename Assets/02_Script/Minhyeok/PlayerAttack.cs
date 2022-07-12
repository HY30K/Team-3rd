using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerAttack : MonoBehaviour
{
    [SerializeField] private Transform pos; // �浹���� ���� ��ġ
    [SerializeField] private Vector2 boxSize; // �浹���� ���� ũ��
    [SerializeField] private float attackPower;

    public float AttackPower { get; set; }

    void Update()
    {
        Attack();
    }
    private void Attack()
    {
        if (Input.GetKeyDown(KeyCode.K))
        {
            Collider2D [] collider2D = Physics2D.OverlapBoxAll(pos.position, boxSize, 0); //Enemy �±� ������ ������ -1
            foreach (Collider2D collider in collider2D)
            {
                if(collider.tag == "Enemy")
                {
                    collider.GetComponent<IDamage>().OnDamage(0.5f + attackPower);
                }
            }
        }
    }
}
