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
        if (Input.GetKeyDown(KeyCode.E))
        {
            money+= 100;
        }
    }
}
