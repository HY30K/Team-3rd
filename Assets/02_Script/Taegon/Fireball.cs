using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Fireball : MonoBehaviour
{
    [SerializeField] private LayerMask enemyLayer;
    [SerializeField] private Transform hitPosition;
    [SerializeField] private Player player;
    [SerializeField] private float hitSize;

    private void Start()
    {
        player = GameObject.Find("Player").GetComponent<Player>();
    }
    private void Update()
    {
        foreach (Collider2D enemy in Physics2D.OverlapCircleAll(hitPosition.position, hitSize, enemyLayer))
        {
            enemy.GetComponent<Enemy>().OnDamage(player.ATKLevel + player.ATKSkillLevel);

            player.ATKSkillLevel++;
        }
    }
}
