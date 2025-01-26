using UnityEngine;
using UnityEngine.UI;
using System.Collections.Generic;
using System.Linq;

public class CookingPotBehaviour : MonoBehaviour
{
    [Header("Pot Configuration")]
    public int maxSlots = 3;
    public Item defaultJunkFoodData;
    public GameObject itemPrefab;
    public List<Recipe> recipes;

    [Header("Pot State")]
    [SerializeField] private List<ItemInstance> itemsInPot = new List<ItemInstance>();
    [SerializeField] private bool isCooking;
    [SerializeField] private bool isCooked;

    [Header("Cooking Parameters")]
    public float totalCookingTime = 10.0f;
    [SerializeField] private float cookingTimer;
    [SerializeField] public Item cookedFoodData;

    [Header("Rendering")]
    [SerializeField] private Renderer potRenderer;
    private Color defaultColor = Color.white;
    private Color highlightColor = Color.yellow;
    private Color fullColor = Color.red;
    private Color cookingColor = Color.cyan;
    private Color cookedColor = Color.blue;

    [Header("Ingredient Sprites")]
    [SerializeField] private SpriteGroup[] spriteGroups;
    [SerializeField] public GameObject cookingPotInventoryCanvas;
    [SerializeField] public GameObject[] ingredientImageSlots;
    [SerializeField] public GameObject cookingImageSlot;
    [SerializeField] public GameObject cookedImageSlot;


    void Start()
    {
        
        ResetPotColor();

        //Set ingredient slots UI to disabled. 
        cookingPotInventoryCanvas.SetActive(false);

        cookingImageSlot.SetActive(false);
        cookedImageSlot.SetActive(false);

    }

    void Update()
    {
        if (isCooking)
        {
            UpdateCookingProcess();
        }
    }

    private void UpdateCookingProcess()
    {
        cookingTimer -= Time.deltaTime;
        if (cookingTimer <= 0.0f)
        {
            FinishCooking();
            return;
        }

        UpdateCookingColor();
    }

    private void UpdateCookingColor()
    {
        if (potRenderer != null)
        {
            Color tempCol = Color.Lerp(cookingColor, fullColor, cookingTimer / totalCookingTime);
            potRenderer.material.color = tempCol;
        }
    }

    public bool AddItem(GameObject item)
    {
        if (itemsInPot.Count >= maxSlots || isCooked || isCooking) return false;

        ItemInstance itemInstance = item.GetComponent<ItemInstance>();
        if (itemInstance == null) return false;

        itemsInPot.Add(itemInstance);

        Debug.Log($"Item {itemInstance.itemData.itemName} added to the pot.");

        UpdatePotColor();
        AddIngredientToUI(itemInstance); //ADD THE INGREDIENT TO THE UI CAMPFIRE UI PANEL. 

        if (itemsInPot.Count == maxSlots)
        {
            StartCooking();
        }

        return true;
    }

    private void StartCooking()
    {
        isCooking = true;
        cookingTimer = totalCookingTime;
        DetermineRecipe();

        //Set Inventory Panel items to empty and invisible. Activate centre panel with cooking sprite.
        HandleCooking(true);
    }

    
    private void DetermineRecipe()
    {
        // Check if recipes list is null or empty
        if (recipes == null || recipes.Count == 0)
        {
            Debug.LogError("No recipes defined!");
            return;
        }

        // Check if itemsInPot is null or empty
        if (itemsInPot == null || itemsInPot.Count == 0)
        {
            Debug.LogError("No items in pot!");
            return;
        }

        foreach (Recipe recipe in recipes)
        {
            // Null check for recipe
            if (recipe == null || recipe.ingredients == null)
            {
                Debug.LogWarning("Skipping null recipe");
                continue;
            }

            if (MatchRecipe(recipe))
            {
                cookedFoodData = recipe.result;
                Debug.Log($"Recipe matched! Created {cookedFoodData?.itemName ?? "Unknown"}.");
                return;
            }
        }

        // No matching recipe
        cookedFoodData = defaultJunkFoodData;
        Debug.Log("No recipe matched. Created junk food.");
    }

    private bool MatchRecipe(Recipe recipe)
    {
        // Additional null checks
        if (recipe.ingredients == null || recipe.ingredients.Count == 0)
            return false;

        // Create a dictionary of ingredient counts in the pot
        var potIngredientCounts = itemsInPot
            .Where(item => item?.itemData != null)
            .GroupBy(item => item.itemData.itemName)
            .ToDictionary(g => g.Key, g => g.Count());

        // Create a dictionary of required ingredient counts for the recipe
        var recipeIngredientCounts = recipe.ingredients
            .GroupBy(item => item.itemName)
            .ToDictionary(g => g.Key, g => g.Count());

        // Check if the ingredient counts exactly match
        return potIngredientCounts.Count == recipeIngredientCounts.Count &&
               recipeIngredientCounts.All(recipeItem =>
                   potIngredientCounts.ContainsKey(recipeItem.Key) &&
                   potIngredientCounts[recipeItem.Key] == recipeItem.Value);
    }

    private void FinishCooking()
    {
        isCooking = false;
        isCooked = true;
        potRenderer.material.color = cookedColor;
        itemsInPot.Clear();

        //Set centre panel with cooked sprite.
        HandleCooking(false);
    }

    
    public GameObject TakeCooked()
    {
        if (!isCooked || cookedFoodData == null) return null;

        GameObject cookedItem = Instantiate(itemPrefab, transform.position + Vector3.up, Quaternion.identity);
        cookedItem.GetComponent<ItemInstance>().itemData = cookedFoodData;

        ResetPotState(); // Reset pot state and UI
        return cookedItem;
    }


    
    private void ResetPotState()
    {
        isCooked = false;
        isCooking = false;
        cookedFoodData = null;
        itemsInPot.Clear();
        ResetPotColor();

        HandleCooking(false); // Reset UI
    }


    private void ResetPotColor()
    {
        if (potRenderer != null)
        {
            potRenderer.material.color = defaultColor;
        }
    }

    private void UpdatePotColor()
    {
        if (potRenderer != null)
        {
            potRenderer.material.color = itemsInPot.Count >= maxSlots ? fullColor : highlightColor;
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
        if ((other.CompareTag("Player One") || other.CompareTag("Player Two")) && !isCooking && !isCooked)
        {
            ResetPotColor();
        }
    }



    private void AddIngredientToUI(ItemInstance item)
    {
        cookingPotInventoryCanvas.SetActive(true);
        cookingImageSlot.SetActive(false);
        cookedImageSlot.SetActive(false);

        for (int i = 0; i < ingredientImageSlots.Length; i++)
        {
            ingredientImageSlots[i].SetActive(true);
        }

        var slotIndex = itemsInPot.Count - 1;
        ingredientImageSlots[slotIndex].GetComponent<UnityEngine.UI.Image>().sprite = item.itemData.itemSprite;
        Debug.Log($"Added {item.itemData.itemName} to ingredient list UI.");
    }

    
    private void HandleCooking(bool cooking)
    {
        // Clear ingredient slots
        foreach (var slot in ingredientImageSlots)
        {
            var image = slot.GetComponent<Image>();
            image.sprite = null;
            slot.SetActive(false);
        }

        // Toggle cooking and cooked slots
        cookingImageSlot.SetActive(cooking);
        cookedImageSlot.SetActive(!cooking);

        cookedImageSlot.GetComponent<Image>().sprite = cookedFoodData.itemSprite;

        // Hide UI if not cooking or cooked
        cookingPotInventoryCanvas.SetActive(cooking || isCooked);
    }


}

[System.Serializable]
public class Recipe
{
    public List<Item> ingredients;
    public Item result;
}


