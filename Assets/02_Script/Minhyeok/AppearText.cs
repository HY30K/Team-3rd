using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using DG.Tweening;
using TMPro;
public class AppearText : MonoBehaviour
{
    [SerializeField] string Text;
   [SerializeField] float duration;
    public TextMeshProUGUI text;
    void Start()
    {

    }
    void Update()
    {
        
    }
    private void OnGUI()
    {
        if (GUILayout.Button("Start"))
        {
            text.DOKill();
            text.text = " ";
            text.DOText(Text,duration);
        }
    }
}
