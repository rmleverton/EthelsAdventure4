//using UnityEngine;

//public class Chest_Behavior : MonoBehaviour
//{
//    public string item = "Gold Coin"; // The item contained in the chest
//    private bool hasItem = true; // Whether the chest has an item

//    private Renderer chestRenderer;
//    private Color defaultColor = new Color(0.59f, 0.29f, 0.0f); // Brown color
//    private Color highlightColor = Color.red; // Red color when a player is near

//    void Start()
//    {
//        chestRenderer = GetComponent<Renderer>();
//        if (chestRenderer != null)
//        {
//            chestRenderer.material.color = defaultColor;
//        }
//    }

//    private void OnTriggerEnter(Collider other)
//    {
//        if (other.CompareTag("Player"))
//        {
//            SetChestColor(highlightColor);
//        }
//    }

//    private void OnTriggerExit(Collider other)
//    {
//        if (other.CompareTag("Player"))
//        {
//            SetChestColor(defaultColor);
//        }
//    }

//    public string TakeItem()
//    {
//        if (hasItem)
//        {
//            hasItem = false;
//            Debug.Log($"Item {item} taken from chest.");
//            return item;
//        }
//        else
//        {
//            Debug.Log("Chest is empty!");
//            return "";
//        }
//    }

//    private void SetChestColor(Color color)
//    {
//        if (chestRenderer != null)
//        {
//            chestRenderer.material.color = color;
//        }
//    }
//}

using UnityEngine;

public class Chest_Behavior : MonoBehaviour
{
    public string item; // The unique item contained in this chest
    private Renderer chestRenderer;
    private Color defaultColor = new Color(0.39f, 0.19f, 0.0f); // Brown color
    private Color highlightColor = Color.red; // Red color when a player is near

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

    public string GetItem()
    {
        Debug.Log($"Item {item} taken from chest.");
        return item; // The item is always available (unlimited)
    }

    private void SetChestColor(Color color)
    {
        if (chestRenderer != null)
        {
            chestRenderer.material.color = color;
        }
    }
}
