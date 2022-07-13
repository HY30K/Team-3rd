using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ItemViewer : MonoBehaviour
{
    public enum ItemType
    {
        Protein,
        Chicken,
        Energybar,
        Water
    }
    public ItemType itemType;
    public SpriteRenderer spriteRenderer;
    [SerializeField] private List<Sprite> itemIamge = new List<Sprite>();

    // Start is called before the first frame update
    void Start()
    {
        spriteRenderer = GetComponent<SpriteRenderer>();
    }

    // Update is called once per frame
    void Update()
    {
        
    }
    public void ItemViewers()
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
}
