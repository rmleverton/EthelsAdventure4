using UnityEngine;

public class ChestBehavior : MonoBehaviour
{
    //public string item; // The unique item contained in this chest
    private Renderer chestRenderer;
    private Color defaultColor = new Color(0.39f, 0.19f, 0.0f); // Brown color
    private Color highlightColor = Color.red; // Red color when a player is near

    public Item itemInChest; // The item stored in this chest
    public GameObject itemPrefab; // Prefab for the item GameObject

    void Start()
    {
        chestRenderer = GetComponent<Renderer>();
        if (chestRenderer != null)
        {
            chestRenderer.material.color = defaultColor;
        }
    }

    private void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("Player One") || other.CompareTag("Player Two"))
        {
            SetChestColor(highlightColor);
        }
    }

    private void OnTriggerExit(Collider other)
    {
        if (other.CompareTag("Player One") || other.CompareTag("Player Two"))
        {
            SetChestColor(defaultColor);
        }
    }

    

    private void SetChestColor(Color color)
    {
        if (chestRenderer != null)
        {
            chestRenderer.material.color = color;
        }
    }

    
    public GameObject CreateItem()
    {
        if (itemPrefab == null)
        {
            Debug.LogWarning("Item prefab is missing in Chest_Behavior.");
            return null;
        }

        if (itemInChest == null)
        {
            Debug.LogWarning("Item data is missing in Chest_Behavior.");
            return null;
        }

        // Instantiate the item
        GameObject newItem = Instantiate(itemPrefab, transform.position, Quaternion.identity);

        // Get the ItemInstance component
        ItemInstance itemInstance = newItem.GetComponent<ItemInstance>();

        if (itemInstance != null)
        {
            // Assign the item data and set the GameObject's name
            itemInstance.itemData = itemInChest;
            newItem.name = itemInChest.itemName; // Set the name of the GameObject
            Debug.Log($"Created item: {itemInstance.GetItemName()}");
        }
        else
        {
            Debug.LogWarning("ItemInstance script is missing on the itemPrefab.");
        }

        return newItem;
    }

}
