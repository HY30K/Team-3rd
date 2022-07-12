using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class Coin : MonoBehaviour
{
    TextMeshProUGUI textCoin;
    // Start is called before the first frame update
    void Start()
    {
        textCoin = GetComponent<TextMeshProUGUI>();
    }

    // Update is called once per frame
    void Update()
    {
        textCoin.text = "" + PlayerMoney.money;
    }
}
