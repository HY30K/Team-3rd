using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;

public class Item : MonoBehaviour
{
    private GameObject player;
    public enum ItemType
    {
        Protein
    }
    public ItemType itemType;
    Rigidbody2D rig;
    public SpriteRenderer spriteRenderer;
    public float followSpeed;
    private bool itemGet = false;
    public float power;

    [SerializeField] private List<Sprite> itemIamge = new List<Sprite>();

    private void Awake()
    {
        rig = GetComponent<Rigidbody2D>();
        spriteRenderer = GetComponent<SpriteRenderer>();
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.gameObject.CompareTag("Player"))
        {
                GetItem();
                Destroy(gameObject, 0.75f);
        }
    }
    private void GetItem()
    {
        switch (itemType)
        {
            case ItemType.Protein:
                //stat up
                break;
            default:
                break;
        }
    }
    private void canGetItem()
    {
        itemGet = true;
    }
    public void ItemSet()
    {
        switch (itemType)
        {
            case ItemType.Protein:
                spriteRenderer.sprite = itemIamge[0];
                break;
            default:
                break;
        }
    }
    public void SpawnItem()
    {
        gameObject.SetActive(true);
        rig.AddForce(new Vector2(0, 10) * power);
        Invoke("canGetItem", 1f);
    }
}
