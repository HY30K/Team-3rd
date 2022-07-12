using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Skill1 : MonoBehaviour
{
    [SerializeField] ObjectPooler objectPooler;
    [SerializeField] Player playerMove;
    [SerializeField] private float maxCoolDown = 2;
    float currentCoolDown;
    Rigidbody2D rigid;

    public float MaxCoolDown => maxCoolDown;
    public float CurrentCoolDown => currentCoolDown;
    void Update()
    {
        //Skill();
        if(currentCoolDown <= 0)
        {
            currentCoolDown = 0;
        }
    }

    /*private void Skill()
    {
        if (currentCoolDown <= 0)
        {
            if (Input.GetKeyDown(KeyCode.L))
            {
                if (playerMove.X != 0 || playerMove.Y != 0)
                {
                    GameObject prefab = objectPooler.SpawnPrefab("Circle");
                    prefab.transform.position = transform.position;
                    rigid = prefab.GetComponent<Rigidbody2D>();
                    rigid.AddForce(new Vector2(playerMove.X, playerMove.Y) * 20, ForceMode2D.Impulse);
                    currentCoolDown = maxCoolDown;
                }
            }
        }
        currentCoolDown -= Time.deltaTime;
    }*/
}

