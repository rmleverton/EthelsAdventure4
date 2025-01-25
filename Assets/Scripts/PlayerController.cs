using UnityEngine;
using UnityEngine.InputSystem;

public class PlayerController : MonoBehaviour
{
    [Header("Movement")]
    public float speed;

    [Header("Inventory")]
    public GameObject inventoryItem;

    [Header("Interaction")]
    private CookingPotBehaviour nearbyPot;
    private ChestBehavior nearbyChest;

    private InputAction moveAction;
    private InputAction interactAction;
    private InputAction dropAction;

    private Animator anim;

    private Cat nearbyCat;

    private void Start()
    {
        ConfigureInputActions();
        anim = GetComponent<Animator>();
    }

    private void ConfigureInputActions()
    {
        string moveActionName = transform.CompareTag("Player One") ? "MovePlayerOne" : "MovePlayerTwo";
        string interactActionName = transform.CompareTag("Player One") ? "InteractPlayerOne" : "InteractPlayerTwo";
        string dropActionName = transform.CompareTag("Player One") ? "DropPlayerOne" : "DropPlayerTwo";

        moveAction = InputSystem.actions.FindAction(moveActionName);
        interactAction = InputSystem.actions.FindAction(interactActionName);
        dropAction = InputSystem.actions.FindAction(dropActionName);
    }

    void Update()
    {
        HandleMovement();
        HandleInteractions();
    }

    private void HandleMovement()
    {
        Vector2 moveValue = moveAction?.ReadValue<Vector2>() ?? Vector2.zero;
        transform.position += new Vector3(moveValue.x, 0, moveValue.y) * speed * Time.deltaTime;


        //anim.SetFloat("speedX", moveValue.x);
        //anim.SetFloat("speedY", moveValue.y);
    }

    private void HandleInteractions()
    {
        if (interactAction?.WasPerformedThisFrame() == true)
        {
            InteractWithNearbyObjects();
        }

        if (dropAction?.WasPerformedThisFrame() == true)
        {
            HandleDropAction();
        }
    }

    private void InteractWithNearbyObjects()
    {
        if (nearbyPot != null)
        {
            HandlePotInteraction();
        }
        else if (nearbyChest != null)
        {
            InteractWithChest();
        }
        if (nearbyCat != null && nearbyCat.CanBeFed)
        {
            HandleCatFeeding();
        }
    }

    private void HandlePotInteraction()
    {
        // If holding an item and pot has space
        if (inventoryItem != null )
        {
            ItemInstance itemInstance = inventoryItem.GetComponent<ItemInstance>();
            string itemName = itemInstance?.itemData?.itemName ?? inventoryItem.name;

            if (nearbyPot.AddItem(inventoryItem))
            {
                Debug.Log($"{gameObject.tag} deposited {itemName} into the cooking pot.");
                inventoryItem = null;
            }
            else
            {
                Debug.Log($"{gameObject.tag} cannot deposit {itemName}. Pot is full or cannot accept the item.");
            }
        }
        // If no item and pot has cooked food
        else if (inventoryItem == null)
        {
            GameObject cookedFood = nearbyPot.TakeCooked();
            if (cookedFood != null)
            {
                ItemInstance foodInstance = cookedFood.GetComponent<ItemInstance>();
                string foodName = foodInstance?.itemData?.itemName ?? cookedFood.name;

                inventoryItem = cookedFood;
                Debug.Log($"{gameObject.tag} picked up {foodName} from the cooking pot.");
            }
            else
            {
                Debug.Log($"{gameObject.tag} tried to pick up cooked food, but no food is available.");
            }
        }
    }

    private void HandleCatFeeding()
    {
        // Check if player has cooked food
        if (inventoryItem != null)
        {
            ItemInstance itemInstance = inventoryItem.GetComponent<ItemInstance>();
            if (itemInstance?.itemData?.itemName.Contains("Cooked") == true)
            {
                // Feed the cat
                nearbyCat.Feed();

                // Destroy the food
                Destroy(inventoryItem);
                inventoryItem = null;

                Debug.Log($"{gameObject.tag} fed the cat with cooked food.");
            }
            else
            {
                Debug.Log($"{gameObject.tag} can only feed cats with cooked food.");
            }
        }
        else
        {
            Debug.Log($"{gameObject.tag} has no food to feed the cat.");
        }
    }

    private void InteractWithChest()
    {
        if (inventoryItem == null)
        {
            inventoryItem = nearbyChest.CreateItem();

            if (inventoryItem != null)
            {
                ItemInstance itemInstance = inventoryItem.GetComponent<ItemInstance>();
                string itemName = itemInstance?.itemData?.itemName ?? inventoryItem.name;
                Debug.Log($"{gameObject.tag} picked up {itemName} from the chest.");
            }
            else
            {
                Debug.Log($"{gameObject.tag} tried to pick up an item from the chest, but no item was created.");
            }
        }
        else
        {
            ItemInstance currentItemInstance = inventoryItem.GetComponent<ItemInstance>();
            string currentItemName = currentItemInstance?.itemData?.itemName ?? inventoryItem.name;
            Debug.Log($"{gameObject.tag} is already carrying {currentItemName}. Cannot pick up another item.");
        }
    }

    
    private void HandleDropAction()
    {
        if (nearbyPot != null)
        {
            // If player has an item and is near the pot, drop player's item into fire
            if (inventoryItem != null)
            {
                ItemInstance itemInstance = inventoryItem.GetComponent<ItemInstance>();
                string itemName = itemInstance?.itemData?.itemName ?? inventoryItem.name;

                Debug.Log($"{gameObject.tag} dropped {itemName} into the fire and destroyed it.");
                Destroy(inventoryItem);
                inventoryItem = null;
            }
            // If player's inventory is empty and pot has cooked food, destroy cooked food
            else
            {
                GameObject cookedFood = nearbyPot.TakeCooked();
                if (cookedFood != null)
                {
                    ItemInstance foodInstance = cookedFood.GetComponent<ItemInstance>();
                    string foodName = foodInstance?.itemData?.itemName ?? cookedFood.name;

                    Debug.Log($"{gameObject.tag} destroyed {foodName} from the cooking pot.");
                    Destroy(cookedFood);
                }
                else
                {
                    Debug.Log($"{gameObject.tag} has no item to drop.");
                }
            }
        }
        else
        {
            Debug.Log("Can't Drop Here Mate");
        }
    }

    private void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("CookingPot"))
        {
            nearbyPot = other.GetComponent<CookingPotBehaviour>();
        }
        else if (other.CompareTag("Chest"))
        {
            nearbyChest = other.GetComponent<ChestBehavior>();
        }
        if (other.CompareTag("Cat"))
        {
            nearbyCat = other.GetComponent<Cat>();
        }
    }

    private void OnTriggerExit(Collider other)
    {
        if (other.CompareTag("CookingPot"))
        {
            nearbyPot = null;
        }
        else if (other.CompareTag("Chest"))
        {
            nearbyChest = null;
        }
        if (other.CompareTag("Cat"))
        {
            nearbyCat = null;
        }
    }
}