using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;

public class Item : MonoBehaviour
{
    public enum ItemType
    {
        Protein,
        Chicken,
        Energybar,
        Water
    }
    public ItemType itemType;
    Rigidbody2D rig;
    public SpriteRenderer spriteRenderer;
    public float followSpeed;
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
    private void GetItem()//statup
    {
        switch (itemType)
        {
            case ItemType.Protein:
                break;
            case ItemType.Chicken:
                break;
            case ItemType.Energybar:
                break;
            case ItemType.Water:
                break;
            //stat up
            default:
                break;
        }
    }
    public void ItemSet()
    {
        switch (itemType)
        {
            case ItemType.Protein:
                spriteRenderer.sprite = itemIamge[0];
                break;
            case ItemType.Chicken:
                spriteRenderer.sprite = itemIamge[1];
                break;
            case ItemType.Energybar:
                spriteRenderer.sprite = itemIamge[2];
                break;
            case ItemType.Water:
                spriteRenderer.sprite = itemIamge[3];
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
