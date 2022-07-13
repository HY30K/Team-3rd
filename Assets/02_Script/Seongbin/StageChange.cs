using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;

public class StageChange : MonoBehaviour
{
    [SerializeField] private Transform stage1Target; // �̵��� Ÿ�� ����
    [SerializeField] private Transform stage2Target; // �̵��� Ÿ�� ����
    [SerializeField] private Transform stage3Target; // �̵��� Ÿ�� ����
    [SerializeField] private Transform shopTarget; // �̵��� Ÿ�� ����
    [SerializeField] private Transform bossStageTarget; // �̵��� Ÿ�� ����

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

    // �ڽ� �ݶ��̴��� ��� ���� �̺�Ʈ �߻�
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