using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class ShopItem : MonoBehaviour
{
    public bool interacted;

    //[SerializeField] private TextMeshPro itemText;
    [SerializeField] private GameObject itemBG;
    [SerializeField] private GameObject item;
    public string itemName;

    [SerializeField] private int itemPrice = 100;
    private enum ItemType
    {
        Protein,
        Chicken,
        Energybar,
        Water
    }
    [SerializeField] private ItemType itemType;

    public GameObject Image;
    public GameObject itemPrefab;

/*    void Start()
    {
        ItemName();
    }*/
    private void Interact()
    {
        Debug.Log(gameObject.name);
        if (BuyItem(itemPrice))
        {
/*            Image.SetActive(false);
            this.enabled = false;*/
        }
    }
    public bool BuyItem(int price)
    {
        if (PlayerMoney.money >= price)
        {
            PlayerMoney.money -= price;
            Debug.Log("아이템 구입");
            GameObject item = Instantiate(itemPrefab, gameObject.transform.position, Quaternion.identity);
            Item itemScript = item.GetComponent<Item>();

            switch (itemType)
            {
                case ItemType.Protein:
                    itemScript.itemType = Item.ItemType.Protein;
                    break;
                case ItemType.Chicken:
                    itemScript.itemType = Item.ItemType.Chicken;
                    break;
                case ItemType.Energybar:
                    itemScript.itemType = Item.ItemType.Energybar;
                    break;
                case ItemType.Water:
                    itemScript.itemType = Item.ItemType.Water;
                    break;
                default:
                    break;
            }
            itemScript.ItemSet();
            itemScript.SpawnItem();
            return true;
        }
        return false;
    }
/*    private void ItemName()
    {
        itemText.text = itemName + "\n" + itemPrice + "G";
    }*/
    private void Update()
    {
        if (Input.GetKeyDown(KeyCode.K) && interacted)
        {
            Interact();
        }

        ItemViewer itemScript = Image.GetComponent<ItemViewer>();

        switch (itemType)
        {
            case ItemType.Protein:
                itemScript.itemType = ItemViewer.ItemType.Protein;
                break;
            case ItemType.Chicken:
                itemScript.itemType = ItemViewer.ItemType.Chicken;
                break;
            case ItemType.Energybar:
                itemScript.itemType = ItemViewer.ItemType.Energybar;
                break;
            case ItemType.Water:
                itemScript.itemType = ItemViewer.ItemType.Water;
                break;
            default:
                break;
        }
        itemScript.ItemViewers();
    }
    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.gameObject.CompareTag("Player"))
        {
            interacted = true;
        }
    }
    private void OnTriggerExit2D(Collider2D collision)
    {
        if (collision.gameObject.CompareTag("Player"))
        {
            interacted = false;
        }
    }
}
