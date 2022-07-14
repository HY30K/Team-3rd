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
    [SerializeField] TextMeshProUGUI text3;
    [SerializeField] TextMeshProUGUI text4;

    GameObject test;

    private void Start()
    {
        Invoke("SetText", 11);
        Invoke("SetText2", 17);
        Invoke("SetText3", 24);
        Invoke("SetText4", 32);
        Invoke("SetText5", 42);
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
    void SetText4()
    {
        test = GameObject.Find("Text (TMP) (3)");
        test.SetActive(false);
    }
    void SetText5()
    {
        test = GameObject.Find("Text (TMP) (4)");
        test.SetActive(false);
    }
    
}
