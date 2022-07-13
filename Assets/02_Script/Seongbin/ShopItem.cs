using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class ShopItem : InteractObj
{
    [SerializeField] private TextMeshPro itemText;
    [SerializeField] private GameObject itemBG;
    [SerializeField] private GameObject item;
    public string itemName;

    [SerializeField] private int itemPrice = 0;
    [SerializeField] private ItemType itemType;

    public GameObject Image;
    public GameObject itemPrefab;

    private enum ItemType
    {
        Protein,
        Chicken,
        Energybar,
        Water
    }
    void Start()
    {
        ItemName();
    }
    private void ItemName()
    {
        itemText.text = itemName + "\n" + itemPrice + "G";
        /*float x = itemText.preferredWidth;
        float y = itemText.preferredHeight / 0.5f;
        x = (x > 2.5f) ? 2.5f : x + 0.5f;
        Debug.Log(itemText.preferredHeight + "\n" + y);
        itemBG.transform.localScale = new Vector3(x, itemText.preferredHeight + y * 0.5f);
        item.transform.position += new Vector3(0, y * 0.5f);*/
    }
    protected override void Interact()
    {
        base.Interact();
        Debug.Log(gameObject.name);
        if (BuyItem(itemPrice))
        {
            Image.SetActive(false);
            this.enabled = false;
        }
    }
    public bool BuyItem(int price)
    {
        if (PlayerMoney.money >= price)
        {
            PlayerMoney.money -= price;
            //UIManager.instance.coinUi();
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
    private void Update()
    {
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
}
