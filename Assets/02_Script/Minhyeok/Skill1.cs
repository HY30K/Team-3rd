using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Skill1 : MonoBehaviour
{
    [SerializeField] GameObject skillPrefab;
    [SerializeField] GameObject particle;
    [SerializeField] ObjectPooler objectPooler;
    [SerializeField] int direction = 0;
    float skillSpeed = 5f;
    Rigidbody2D rigid;
    void Start()
    {
        Rigidbody2D rigid = skillPrefab.GetComponent<Rigidbody2D>(); 
        Skill();
    }
    void Update()
    {
        
    }

    private void Skill()
    {
        GameObject prefab = objectPooler.SpawnPrefab("Circle");
        prefab.transform.position = transform.position;
        if (Input.GetKeyDown(KeyCode.L))
        {
            if (direction == 0)
                rigid.AddForce(Vector2.right * 20, ForceMode2D.Impulse);
            else if (direction == 1)
                rigid.AddForce(Vector2.left * 20, ForceMode2D.Impulse);
            else if (direction == 2)
                rigid.AddForce(Vector2.up * 20, ForceMode2D.Impulse);
            else if (direction == 3)
                rigid.AddForce(Vector2.down * 20, ForceMode2D.Impulse);
            else if (direction == 4)
                rigid.AddForce(new Vector2(10, 10), ForceMode2D.Impulse);
            else if (direction == 5)
                rigid.AddForce(new Vector2(-10, 10), ForceMode2D.Impulse);
            else if (direction == 6)
                rigid.AddForce(new Vector2(10, -10), ForceMode2D.Impulse);
            else if (direction == 7)
                rigid.AddForce(new Vector2(-10, -10), ForceMode2D.Impulse);
        }
    }
}

