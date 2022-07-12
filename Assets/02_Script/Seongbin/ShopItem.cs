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

    public int money;
    private enum ItemType
    {
        Protein
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
        if (money >= price)
        {
            money -= price;
            //UIManager.instance.coinUi();
            Debug.Log("아이템 구입");
            GameObject item = Instantiate(itemPrefab, gameObject.transform.position, Quaternion.identity);
            Item itemScript = item.GetComponent<Item>();

            switch (itemType)
            {
                case ItemType.Protein:
                    itemScript.itemType = Item.ItemType.Protein;
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
}
