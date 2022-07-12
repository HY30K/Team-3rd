using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;
using DG.Tweening;
public class AppearButton : MonoBehaviour
{
    public TextMeshProUGUI text;
    private void OnGUI()
    {
        if (GUILayout.Button("Start"))
        {
            text.DOKill();
            text.text = " ";
            text.DOText("나는 김멸치..겜마고에 존재하는 17세 청소년이다..",6).SetEase(Ease.InSine);
        }
    }
    void Start()
    {
        
    }
    void Update()
    {
     
    }
}
