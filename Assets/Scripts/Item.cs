using UnityEngine;

[CreateAssetMenu(fileName = "NewItem", menuName = "Inventory/Item")]
public class Item : ScriptableObject
{
    public string itemName; // Name of the item
    public Sprite itemSprite; // Sprite representing the item
    public string disease;
}

