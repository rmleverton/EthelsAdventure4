using UnityEngine;
using TMPro;

public class ChestBehavior : MonoBehaviour
{
    [SerializeField] private GameObject uiMenu; // Reference to the UI menu GameObject
    [SerializeField] private GameObject uiPanel; // Reference to the UI menu GameObject
    [SerializeField] private GameObject Marge; // Set to serialised for collision checks between chests and chars.
    [SerializeField] private GameObject Ethel;
    [SerializeField] private GameObject Chest;

    //public string item; // The unique item contained in this chest
    private Renderer chestRenderer;
    private Color defaultColor = new Color(0.39f, 0.19f, 0.0f); // Brown color
    private Color highlightColor = Color.red; // Red color when a player is near

    public Item itemInChest; // The item stored in this chest
    public GameObject itemPrefab; // Prefab for the item GameObject
    public TMP_Text panelText;

    void Start()
    {
        chestRenderer = GetComponent<Renderer>();
        if (chestRenderer != null)
        {
            chestRenderer.material.color = defaultColor;
        }

        //UI Checks - ensure the UI menu is initially disabled
        if (uiMenu != null)
        {
            uiMenu.SetActive(true);
            uiPanel.SetActive(false);
        }
        else
        {
            Debug.LogWarning("UI menu is not assigned in the ChestPrompt script.");
        }

        panelText = uiPanel.transform.Find("Text").GetComponent<TMP_Text>();
    }

    private void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("Player One") || other.CompareTag("Player Two"))
        {
            SetChestColor(highlightColor);
        }

        // Check if this object and the other object are the two specific objects
        if ((other.gameObject == Marge && gameObject == Chest) ||
            (other.gameObject == Chest && gameObject == Marge))
        {
            // Activate the UI menu
            uiPanel.SetActive(true);
            // Update the text
            panelText.text = "'E'";

            Debug.Log("Specific objects collided. UI panel activated.");
        }
        else if ((other.gameObject == Ethel && gameObject == Chest) ||
                (other.gameObject == Chest && gameObject == Ethel))
        {
            // Activate the UI menu
            uiPanel.SetActive(true);
            // Update the text
            panelText.text = "'?'";

            Debug.Log("Specific objects collided. UI panel activated.");
        }
    }

    private void OnTriggerExit(Collider other)
    {
        if (other.CompareTag("Player One") || other.CompareTag("Player Two"))
        {
            SetChestColor(defaultColor);
        }

        // Check if this object and the other object are the two specific objects
        if ((other.gameObject == Marge && gameObject == Chest) ||
            (other.gameObject == Chest && gameObject == Marge) ||
            (other.gameObject == Ethel && gameObject == Chest) ||
            (other.gameObject == Chest && gameObject == Ethel))
        {
            // Activate the UI menu
            uiPanel.SetActive(false);
            // Update the text
            panelText.text = "";

            Debug.Log("UI panel deactivated.");
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
