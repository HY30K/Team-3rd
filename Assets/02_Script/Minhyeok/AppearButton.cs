using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;
using DG.Tweening;
public class AppearButton : MonoBehaviour
{
    [SerializeField] TextMeshProUGUI text;
    [SerializeField] TextMeshProUGUI text1;
    [SerializeField] TextMeshProUGUI text2;
    GameObject test;

    private void Start()
    {
        Invoke("SetText", 8);
        Invoke("SetText2", 15);
        Invoke("SetText3", 22);
    }
    private void Update()
    {
    }
    void SetText()
    {
        test = GameObject.Find("Text (TMP)");
        test.SetActive(false);
    }
    void SetText2()
    {
        test = GameObject.Find("Text (TMP) (1)");
        test.SetActive(false);
    }
    void SetText3()
    {
        test = GameObject.Find("Text (TMP) (2)");
        test.SetActive(false);
    }
    
}
