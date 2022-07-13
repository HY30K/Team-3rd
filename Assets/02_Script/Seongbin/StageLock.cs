using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class StageLock : MonoBehaviour
{
    [SerializeField] private Transform stagePos;
    [SerializeField] private Vector2 size;
    public enum StageLockType
    {
        stage1,
        stage2,
        stage3,
        shop,
        boss
    }
    public StageLockType lockType;
    // Start is called before the first frame update
    void Start()
    {
        RaycastHit2D hit = Physics2D.BoxCast(stagePos.position, size, 0, new Vector2(1, 0), 5f);
    }
    void OnDrawGizmos()
    {
        Gizmos.color = Color.red;
        Gizmos.DrawWireCube(transform.position, size);
    }
    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.gameObject.CompareTag("Player"))
        {
            Debug.Log(lockType);
        }
    }
}
