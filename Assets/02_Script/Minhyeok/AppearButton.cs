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
            text.DOText("���� ���ġ..�׸��� �����ϴ� 17�� û�ҳ��̴�..",6).SetEase(Ease.InSine);
        }
    }
    void Start()
    {
        
    }
    void Update()
    {
     
    }
}
