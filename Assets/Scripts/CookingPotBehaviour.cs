//using UnityEngine;
//using System.Collections.Generic;
//using System.Linq;

//public class CookingPotBehaviour : MonoBehaviour
//{
//    public int maxSlots = 3; // Maximum number of items the pot can hold
//    [SerializeField] private List<ItemInstance> itemsInPot = new List<ItemInstance>(); // Stores items in the pot
//    private Renderer potRenderer;
//    private Color defaultColor = Color.gray; // Default color of the pot
//    private Color highlightColor = Color.green; // Color when a player is nearby
//    private Color fullColor = Color.red; // Color when the pot is full
//    private Color cookingColor = Color.cyan; // Color when the pot is cooking
//    private Color cookedColor = Color.blue; // Color when the pot has finished cooking

//    private float totalCookingTime = 10.0f;
//    [SerializeField] private float cookingTimer;
//    [SerializeField] private bool isCooking = false;
//    [SerializeField] private bool isCooked = false;

//    public Item cookedFoodData; // Resulting cooked food
//    public Item defaultJunkFoodData; // Default food if no recipe matches
//    public GameObject itemPrefab; // Prefab used to spawn the cooked food

//    [Header("Recipes")]
//    public List<Recipe> recipes; // List of recipes

//    void Start()
//    {
//        potRenderer = GetComponent<Renderer>();
//        if (potRenderer != null)
//        {
//            potRenderer.material.color = defaultColor;
//        }
//    }

//    private void Update()
//    {
//        if (isCooking)
//        {
//            Cooking();
//        }
//    }

//    private void OnTriggerEnter(Collider other)
//    {
//        if (other.CompareTag("Player One") || other.CompareTag("Player Two"))
//        {
//            UpdatePotColor();
//        }
//    }

//    private void OnTriggerExit(Collider other)
//    {

//        if (other.CompareTag("Player One") || other.CompareTag("Player Two"))
//        {
//            if (potRenderer != null && !isCooking && !isCooked)
//            {
//                potRenderer.material.color = defaultColor;
//            }
//            else if (isCooked)
//            {
//                potRenderer.material.color = cookedColor;
//            }
//        }
//    }

//    public bool AddItem(GameObject item)
//    {
//        ItemInstance itemInstance = item.GetComponent<ItemInstance>();
//        if (itemInstance != null && itemsInPot.Count < maxSlots)
//        {
//            itemsInPot.Add(itemInstance);
//            Debug.Log($"Item {itemInstance.itemData.itemName} added to the pot.");
//            UpdatePotColor();

//            if (itemsInPot.Count == maxSlots)
//            {
//                isCooking = true;
//                cookingTimer = totalCookingTime;
//                RecipeLogic();
//            }
//            return true;
//        }
//        else
//        {
//            Debug.Log($"The pot is full or the item is invalid. Item {item.name} has been destroyed.");
//            return false;
//        }
//    }

//    private void UpdatePotColor()
//    {
//        if (potRenderer != null)
//        {
//            potRenderer.material.color = itemsInPot.Count >= maxSlots ? fullColor : highlightColor;
//        }
//    }

//    private void Cooking()
//    {
//        if (cookingTimer <= 0.0f)
//        {
//            FinishCooking();
//            return;
//        }

//        cookingTimer -= Time.deltaTime;
//        Color tempCol = Color.Lerp(cookingColor, fullColor, cookingTimer / totalCookingTime);
//        potRenderer.material.color = tempCol;
//    }

//    private void FinishCooking()
//    {
//        isCooking = false;
//        isCooked = true;
//        potRenderer.material.color = cookedColor;

//        SpawnCookedItem();
//        Debug.Log($"Cooking complete. Result: {cookedFoodData.itemName}");
//    }

//    private void RecipeLogic()
//    {
//        // Get a list of the item names
//        List<string> ingredientNames = itemsInPot.Select(item => item.itemData.itemName).ToList();

//        //foreach (Recipe recipe in recipes)
//        //{
//        //    if (recipe.ingredients.All(requiredItem => itemsInPot.Any(item => item.itemData.itemName == requiredItem.itemName)))
//        //    {
//        //        cookedFoodData = recipe.result;
//        //        Debug.Log($"Recipe matched! Created {cookedFoodData.itemName}.");
//        //        itemsInPot.Clear();
//        //        return;
//        //    }
//        //}



//        //// If no recipe matches, produce junk food
//        //cookedFoodData = defaultJunkFoodData;
//        //Debug.Log("No matching recipe. Producing junk food.");
//        foreach (Recipe recipe in recipes)
//        {
//            bool allIngredientsMatch = true;

//            // Create a dictionary to count items in the pot
//            Dictionary<string, int> itemCountsInPot = new Dictionary<string, int>();

//            // Count the items in the pot
//            foreach (var item in itemsInPot)
//            {
//                string itemName = item.itemData.itemName;
//                if (itemCountsInPot.ContainsKey(itemName))
//                {
//                    itemCountsInPot[itemName]++;
//                }
//                else
//                {
//                    itemCountsInPot[itemName] = 1;
//                }
//            }

//            // Now compare the recipe's ingredients with the counts in the pot
//            foreach (var requiredItem in recipe.ingredients)
//            {
//                string requiredItemName = requiredItem.itemName;

//                if (!itemCountsInPot.ContainsKey(requiredItemName) || itemCountsInPot[requiredItemName] <= 0)
//                {
//                    allIngredientsMatch = false;
//                    break;
//                }

//                // Decrease the count for the matched ingredient
//                itemCountsInPot[requiredItemName]--;
//            }

//            // If all ingredients match, process the recipe, otherwise, it's junk
//            if (allIngredientsMatch)
//            {
//                cookedFoodData = recipe.result;
//                Debug.Log($"Recipe matched! Created {cookedFoodData.itemName}.");
//                itemsInPot.Clear();
//                return;
//            }
//        }

//        // If no recipe matched, create junk food
//        cookedFoodData = defaultJunkFoodData;  // Set the result to junk food if no match
//        Debug.Log("No recipe matched. Created junk food.");

//    }

//    private void SpawnCookedItem()
//    {
//        if (cookedFoodData != null && itemPrefab != null)
//        {
//            GameObject cookedItem = Instantiate(itemPrefab, transform.position + Vector3.up, Quaternion.identity);
//            ItemInstance itemInstance = cookedItem.GetComponent<ItemInstance>();

//            if (itemInstance != null)
//            {
//                itemInstance.itemData = cookedFoodData;
//            }
//        }
//        else
//        {
//            Debug.LogWarning("Cooked food data or item prefab is missing.");
//        }
//    }


//}



//[System.Serializable]
//public class Recipe
//{
//    public List<Item> ingredients; // Items required for the recipe
//    public Item result; // Resulting item
//}

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
    [SerializeField] public Item CookedFoodData;

    [Header("Rendering")]
    private Renderer potRenderer;
    private Color defaultColor = Color.gray;
    private Color highlightColor = Color.green;
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
        potRenderer = GetComponent<Renderer>();
        ResetPotColor();

        //Set ingredient slots UI to disabled. 
        cookingPotInventoryCanvas.SetActive(false);
        //cookingImageSlot.enabled = false;
        //cookedImageSlot.enabled = false;
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
        foreach (Recipe recipe in recipes)
        {
            if (MatchRecipe(recipe))
            {
                CookedFoodData = recipe.result;
                Debug.Log($"Recipe matched! Created {CookedFoodData.itemName}.");
                return;
            }
        }

        // No matching recipe
        CookedFoodData = defaultJunkFoodData;
        Debug.Log("No recipe matched. Created junk food.");
    }

    private bool MatchRecipe(Recipe recipe)
    {
        // Create a dictionary of ingredient counts in the pot
        var potIngredientCounts = itemsInPot
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
        if (!isCooked || CookedFoodData == null) return null;

        GameObject cookedItem = Instantiate(itemPrefab, transform.position + Vector3.up, Quaternion.identity);
        ItemInstance itemInstance = cookedItem.GetComponent<ItemInstance>();

        if (itemInstance != null)
        {
            itemInstance.itemData = CookedFoodData;
        }

        //Set centre panel with cooking sprite.
        HandleCooking(true);

        ResetPotState();
        return cookedItem;
    }

    private void ResetPotState()
    {
        isCooked = false;
        CookedFoodData = null;
        ResetPotColor();
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

    //private void AddIngredientToUI(ItemInstance item)
    //{
    //    //Set ingredient slots UI to enabled. 
    //    cookingPotInventoryCanvas.SetActive(true);
    //    cookingImageSlot.enabled = cookedImageSlot.enabled = false;
    //    for (int i = 0; i < ingredientImageSlots.Length; i++)
    //    {
    //        ingredientImageSlots[i].enabled = true;
    //    }

    //    //Get the amount of ingredients and subtract 1 to get the ingredientImageSlot index. 
    //    var slotIndex = itemsInPot.Count - 1;
    //    //Set the sprite in the slot to the current ingredient sprite - visual marker of the cooking pot inventory. 
    //    ingredientImageSlots[slotIndex].sprite = item.itemData.itemSprite;

    //    Debug.Log($"Added {item.itemData.itemName} to ingredient list UI.");
    //}

    //private void HandleCooking(bool cooking)
    //{
    //    for (int i = 0; i < ingredientImageSlots.Length; i++)
    //    {
    //        ingredientImageSlots[i].enabled = false;
    //    }
    //    //Switch between cooking and cooked.
    //    cookingImageSlot.enabled = cooking;
    //    cookedImageSlot.enabled = !cooking;
    //}

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
        for (int i = 0; i < ingredientImageSlots.Length; i++)
        {
            ingredientImageSlots[i].SetActive(false);
        }

        cookingImageSlot.SetActive(cooking);
        cookedImageSlot.SetActive(!cooking);
    }

}

[System.Serializable]
public class Recipe
{
    public List<Item> ingredients;
    public Item result;
}