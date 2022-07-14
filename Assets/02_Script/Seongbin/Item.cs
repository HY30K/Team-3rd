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
    Player player;

    [SerializeField] private List<Sprite> itemIamge = new List<Sprite>();

    private void Awake()
    {
        rig = GetComponent<Rigidbody2D>();
        spriteRenderer = GetComponent<SpriteRenderer>();
        player = GameObject.Find("Player").GetComponent<Player>();
    }
    private void Start()
    {
        GetItem();
        Destroy(gameObject, 1.5f);
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.gameObject.CompareTag("Player"))
        {
            Destroy(gameObject);
        }
    }
    private void GetItem()//statup
    {
        /*switch (itemType)
        {

            case ItemType.Chicken:
                player.HPCurrent += 50;
                break;
            case ItemType.Energybar:
                player.HPCurrent += 10;
                break;
            case ItemType.Water:
                player.HPCurrent += 2;
                break;
            case ItemType.Protein:
                player.HPLevel++;
                player.HPCurrent += 10;
                break;
            //stat up
            default:
                break;
        }*/
        if (itemType == ItemType.Chicken)
        {
            player.HPCurrent += 50;
        }
        else if (itemType == ItemType.Energybar)
        {
            player.HPCurrent += 10;
        }
        else if (itemType == ItemType.Water)
        {
            player.HPCurrent += 2;
        }
        else if (itemType == ItemType.Protein)
        {
            player.HPLevel++;
            player.HPCurrent += 10;
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
    }
}
