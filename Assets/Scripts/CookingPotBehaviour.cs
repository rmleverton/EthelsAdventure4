using UnityEngine;
using System.Collections.Generic;

public class CookingPotBehaviour : MonoBehaviour
{
    public int maxSlots = 3; // Maximum number of items the pot can hold
    [SerializeField] private List<string> itemsInPot = new List<string>(); // Stores items in the pot
    private Renderer potRenderer;
    private Color defaultColor = Color.gray; // Default color of the pot
    private Color highlightColor = Color.green; // Color when a player is nearby
    private Color fullColor = Color.red; // Color when the pot is full

    void Start()
    {
        potRenderer = GetComponent<Renderer>();
        if (potRenderer != null)
        {
            potRenderer.material.color = defaultColor;
        }
    }

    private void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("Player One") || other.CompareTag("Player Two"))
        {
            UpdatePotColor();
        }
    }

    private void OnTriggerExit(Collider other)
    {
        if (other.CompareTag("Player One") || other.CompareTag("Player Two"))
        {
            if (potRenderer != null)
            {
                potRenderer.material.color = defaultColor;
            }
        }
    }

    public bool AddItem(string item)
    {
        if (itemsInPot.Count < maxSlots)
        {
            itemsInPot.Add(item);
            Debug.Log($"Item {item} added to the pot. Current items: {string.Join(", ", itemsInPot)}");
            UpdatePotColor();
            return true;
        }
        else
        {
            Debug.Log($"The pot is full. Item {item} has been destroyed.");
            return false;
        }
    }

    public void UpdatePotColor()
    {
        if (potRenderer != null)
        {
            potRenderer.material.color = itemsInPot.Count >= maxSlots ? fullColor : highlightColor;
        }
    }
}
