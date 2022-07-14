using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerMoney : MonoBehaviour
{
    public static int money;
    private void Awake()
    {
        money = 0;
    }

    void Update()
    {
        if (Input.GetKey(KeyCode.E) && Input.GetKeyDown(KeyCode.O))
        {
            money+= 200;
        }
    }
}
