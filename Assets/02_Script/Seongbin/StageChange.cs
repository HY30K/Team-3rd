using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;

public class StageChange : MonoBehaviour
{
    [SerializeField] private Transform stage1Target; // 이동할 타겟 설정
    [SerializeField] private Transform stage2Target; // 이동할 타겟 설정
    [SerializeField] private Transform stage3Target; // 이동할 타겟 설정
    [SerializeField] private Transform shopTarget; // 이동할 타겟 설정
    [SerializeField] private Transform bossStageTarget; // 이동할 타겟 설정

    public enum ChangeStage
    {
        stage1,
        stage2,
        stage3,
        shop,
        boss
    }
    // Start is called before the first frame update
    public void Awake()
    {
    }

    // 박스 콜라이더에 닿는 순간 이벤트 발생
    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.gameObject.CompareTag("Player"))
        {
            Change();
        }
    }

    private void Change()
    {
        ChangeStage changeStage = (ChangeStage)(UnityEngine.Random.Range(0, Enum.GetNames(typeof(ChangeStage)).Length));

        switch (changeStage)
        {
            case ChangeStage.stage1:
                GameObject.Find("Player").transform.position = stage1Target.transform.position;
                break;
            case ChangeStage.stage2:
                GameObject.Find("Player").transform.position = stage2Target.transform.position;
                break;
            case ChangeStage.stage3:
                GameObject.Find("Player").transform.position = stage3Target.transform.position;
                break;
            case ChangeStage.shop:
                GameObject.Find("Player").transform.position = shopTarget.transform.position;
                break;
            case ChangeStage.boss:
                GameObject.Find("Player").transform.position = bossStageTarget.transform.position;
                break;
            default:
                break;
        }
    }
}