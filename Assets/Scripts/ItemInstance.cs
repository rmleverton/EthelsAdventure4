using UnityEngine;

public class ItemInstance : MonoBehaviour
{
    public Item itemData; // Reference to the Item ScriptableObject
    public SpriteRenderer spriteRenderer; // SpriteRenderer to display the item's sprite

    void Start()
    {
        if (itemData != null && spriteRenderer != null)
        {
            spriteRenderer.sprite = itemData.itemSprite; // Set the sprite
        }
    }

    public string GetItemName()
    {
        return itemData != null ? itemData.itemName : "Unknown";
    }

    public string GetItemCure()
    {
        return itemData != null ? itemData.disease : "Not A Cure";
    }
}
