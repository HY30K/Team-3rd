using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Fireball : MonoBehaviour
{
    [SerializeField] private LayerMask enemyLayer;
    [SerializeField] private Vector2 hitPosition;
    [SerializeField] private Player player;
    [SerializeField] private float hitSize;

    private void Update()
    {
        foreach (Collider2D enemy in Physics2D.OverlapCircleAll(hitPosition, hitSize, enemyLayer))
        {
            enemy.GetComponent<Enemy>().OnDamage(player.ATKSkillCurrent);

            player.ATKSkillCurrent += 2.0f;
        }
    }
}
