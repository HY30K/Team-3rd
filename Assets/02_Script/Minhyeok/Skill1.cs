using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Skill1 : MonoBehaviour
{
    [SerializeField] GameObject skillPrefab;
    //[SerializeField] GameObject particle;
    [SerializeField] ObjectPooler objectPooler;
    [SerializeField] PlayerMove playerMove;
    [SerializeField] int direction = 0;
    float skillSpeed = 5f;
    Rigidbody2D rigid;
    void Update()
    {
        Skill();
    }

    private void Skill()
    {
        if (Input.GetKeyDown(KeyCode.L))
        {
            if (playerMove.X != 0 || playerMove.Y != 0)
            {
                GameObject prefab = objectPooler.SpawnPrefab("Circle");
                prefab.transform.position = transform.position;
                rigid = prefab.GetComponent<Rigidbody2D>();
                rigid.AddForce(new Vector2(playerMove.X, playerMove.Y) * 20, ForceMode2D.Impulse);
            }
        }
    }
}

