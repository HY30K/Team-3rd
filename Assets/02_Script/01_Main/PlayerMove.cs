using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerMove : MonoBehaviour
{
    private Rigidbody2D rb2d = null;
    private float x;
    private float y;
    private float agility;
    private float agilityIncreaseDelay;

    private void Awake()
    {
        rb2d = GetComponent<Rigidbody2D>();
    }
    private void Update()
    {
        Move();
    }
    private void Move()
    {
        x = Input.GetAxisRaw("Horizontal");
        y = Input.GetAxisRaw("Vertical");
        Vector2 dir = new Vector2(x, y);
        rb2d.velocity = dir.normalized * (1 + agility);

        if (Mathf.Abs(x) > 0 || Mathf.Abs(y) > 0)
        {
            agilityIncreaseDelay += Time.deltaTime;

            if (agilityIncreaseDelay >= 5.0f)
            {
                agility += 0.2f;
                agilityIncreaseDelay = 0;
            }
        }
    }
}
