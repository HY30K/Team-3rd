using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Cameras : MonoBehaviour
{
    [SerializeField] private float maxX;
    [SerializeField] private float minX;

    public GameObject target; // ī�޶� ���� ���
    public float moveSpeed; // ī�޶� ���� �ӵ�
    private Vector3 targetPosition; // ����� ���� ��ġ

    void Update()
    {
        CameraMove();
    }
    void CameraMove()
    {
        if (target.gameObject != null)
        {
            // this�� ī�޶� �ǹ� (z���� ī�޶��� �״�� ����)
            targetPosition.Set(target.transform.position.x, target.transform.position.y , this.transform.position.z);

            // vectorA -> B���� T�� �ӵ��� �̵�
            this.transform.position = Vector3.Lerp(this.transform.position, targetPosition, moveSpeed * Time.deltaTime);
        }
    }
}
