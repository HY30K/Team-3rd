using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;

public class Item : MonoBehaviour
{
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

    private Player player;
    void Update()
    {
        if (Physics2D.OverlapCircle(transform.position, 3, 1 << 7) != null && itemGet)
        {
            transform.position = Vector2.MoveTowards(transform.position, GameManager.instance.player.transform.position, followSpeed * Time.deltaTime);
            if (Physics2D.OverlapCircle(transform.position, 0.2f, 1 << 7) != null)
            {
                GetItem();
                Destroy(gameObject);
            }
        }
    }
    public void SpawnItem()
    {
        gameObject.SetActive(true);
        rig.AddForce(new Vector2(0, 10) * power);
        Invoke("canGetCoin", 1f);
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
    private void canGetCoin()
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
}
