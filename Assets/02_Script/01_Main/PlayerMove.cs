using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerMove : MonoBehaviour
{
    [SerializeField] private PlayerStatus playerStatus;
    private Rigidbody2D rb2d = null;
    private float x;
    private float y;
    private float agility;
    private float agilityIncreaseDelay;

    public float Agility { get; set; }

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
        rb2d.velocity = dir.normalized * (0.5f + agility);

        if (Mathf.Abs(x) > 0 || Mathf.Abs(y) > 0)
        {
            agilityIncreaseDelay += Time.deltaTime;

            if (agilityIncreaseDelay >= 10.0f)
            {
                playerStatus.AgilityChange(true);
                agilityIncreaseDelay = 0;
            }
        }
    }
}
