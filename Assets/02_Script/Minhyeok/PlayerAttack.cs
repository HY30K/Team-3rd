using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerAttack : MonoBehaviour
{
    [SerializeField] Transform pos;
    [SerializeField] Vector2 boxSize;
    [SerializeField] float Damage;
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
            Collider2D [] collider2D = Physics2D.OverlapBoxAll(pos.position, boxSize, 0);
            foreach (Collider2D collider in collider2D)
            {
                if(collider.tag == "Enemy")
                {
                    collider.GetComponent<IDamage>().OnDamage(Damage);
                }
            }
        }
    }
}
