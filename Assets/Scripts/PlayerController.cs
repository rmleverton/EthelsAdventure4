//using UnityEngine;
//using UnityEngine.InputSystem;


//public class PlayerController : MonoBehaviour
//{
//    InputAction moveAction;
//    InputAction interactAction;
//    InputAction dropAction; // Action to drop item into the fire
//    public float speed;
//    public GameObject inventoryItem; // Inventory can hold one item at a time
//    private CookingPotBehaviour nearbyPot;
//    private ChestBehavior nearbyChest; // Tracks the chest the player is near

//    private void Start()
//    {
//        if (transform.CompareTag("Player One"))
//        {
//            moveAction = InputSystem.actions.FindAction("MovePlayerOne");
//            interactAction = InputSystem.actions.FindAction("InteractPlayerOne");
//            dropAction = InputSystem.actions.FindAction("DropPlayerOne");
//        }
//        if (transform.CompareTag("Player Two"))
//        {
//            moveAction = InputSystem.actions.FindAction("MovePlayerTwo");
//            interactAction = InputSystem.actions.FindAction("InteractPlayerTwo");
//            dropAction = InputSystem.actions.FindAction("DropPlayerTwo");
//        }
//    }

//    void Update()
//    {
//        // Handle movement
//        Vector2 moveValue = moveAction != null ? moveAction.ReadValue<Vector2>() : Vector2.zero;
//        moveValue *= speed;

//        transform.position += new Vector3(moveValue.x, 0, moveValue.y) * Time.deltaTime;

//        // Handle interactions
//        if (interactAction != null && interactAction.WasPerformedThisFrame())
//        {
//            if (nearbyPot != null)
//            {
//                //DepositItemToPot();
//                HandlePotInteraction();
//            }
//            if (nearbyChest != null)
//            {
//                InteractWithChest();
//            }
//        }

//        // Handle dropping items into the fire
//        if (dropAction != null && dropAction.WasPerformedThisFrame())
//        {
//            DropItemIntoFire();
//        }
//    }


//    private void OnTriggerEnter(Collider other)
//    {
//        if (other.CompareTag("CookingPot"))
//        {
//            nearbyPot = other.GetComponent<CookingPotBehaviour>();
//        }
//        else if (other.CompareTag("Chest"))
//        {
//            nearbyChest = other.GetComponent<ChestBehavior>();
//        }
//    }

//    private void OnTriggerExit(Collider other)
//    {
//        if (other.CompareTag("CookingPot"))
//        {
//            nearbyPot = null;
//        }
//        else if (other.CompareTag("Chest"))
//        {
//            nearbyChest = null;
//        }
//    }


//    private void InteractWithChest()
//    {
//        if (nearbyChest != null)
//        {
//            // If player already has an item, they can't pick another
//            if (inventoryItem == null)
//            {
//                inventoryItem = nearbyChest.CreateItem();
//                Debug.Log($"{gameObject.tag} picked up {inventoryItem.name}");
//            }
//            else
//            {
//                Debug.Log($"{gameObject.tag} is already carrying an item: {inventoryItem}");
//            }
//        }
//    }

//    private void HandlePotInteraction()
//    {
//        if (inventoryItem != null && !nearbyPot.IsPotFull() && nearbyPot.GetCookedFood() == null)
//        {
//            // If the pot isn't full and there's no cooked food, deposit the item
//            DepositItemToPot();
//        }
//        else if (inventoryItem == null && nearbyPot.GetCookedFood() != null)
//        {
//            // If the player has an empty inventory and there's cooked food in the pot, pick it up
//            PickUpCookedFood();
//        }
//    }


//    private void DepositItemToPot()
//    {
//        if (inventoryItem != null)
//        {
//            ItemInstance itemInstance = inventoryItem.GetComponent<ItemInstance>();
//            string itemName = itemInstance != null ? itemInstance.GetItemName() : inventoryItem.name;

//            if (nearbyPot.AddItem(inventoryItem))
//            {
//                Debug.Log($"{gameObject.tag} deposited {itemName} into the pot.");
//                inventoryItem = null; // Clear player's inventory
//            }
//            else
//            {
//                Debug.Log($"The pot is full. {itemName} is destroyed in the fire.");
//                inventoryItem = null; // Destroy the item
//            }
//        }
//        else
//        {
//            Debug.Log($"{gameObject.tag} has no item to deposit.");
//        }
//    }

//    private void PickUpCookedFood()
//    {
//        if (nearbyPot.GetCookedFood() != null)
//        {
//            // If there's cooked food in the pot and the player has no inventory item
//            inventoryItem = nearbyPot.GetCookedFood();
//            Debug.Log($"{gameObject.tag} picked up {inventoryItem.name} from the pot.");
//            nearbyPot.SetCookedFood(null); // Remove cooked food from the pot
//        }
//        else
//        {
//            Debug.Log($"{gameObject.tag} tried to pick up cooked food, but there's none in the pot.");
//        }
//    }

//    private void DropItemIntoFire()
//    {
//        if (inventoryItem != null)
//        {
//            ItemInstance itemInstance = inventoryItem.GetComponent<ItemInstance>();
//            string itemName = itemInstance != null ? itemInstance.GetItemName() : inventoryItem.name;

//            Debug.Log($"{gameObject.tag} dropped {itemName} into the fire. It is destroyed.");
//            inventoryItem = null; // Destroy the item
//        }
//        else
//        {
//            Debug.Log($"{gameObject.tag} has no item to drop.");
//        }
//    }

//}
//using UnityEngine;
//using UnityEngine.InputSystem;

//public class PlayerController : MonoBehaviour
//{
//    [Header("Movement")]
//    public float speed;

//    [Header("Inventory")]
//    public GameObject inventoryItem;

//    [Header("Interaction")]
//    private CookingPotBehaviour nearbyPot;
//    private ChestBehavior nearbyChest;

//    private InputAction moveAction;
//    private InputAction interactAction;
//    private InputAction dropAction;

//    private void Start()
//    {
//        ConfigureInputActions();
//    }

//    private void ConfigureInputActions()
//    {
//        string moveActionName = transform.CompareTag("Player One") ? "MovePlayerOne" : "MovePlayerTwo";
//        string interactActionName = transform.CompareTag("Player One") ? "InteractPlayerOne" : "InteractPlayerTwo";
//        string dropActionName = transform.CompareTag("Player One") ? "DropPlayerOne" : "DropPlayerTwo";

//        moveAction = InputSystem.actions.FindAction(moveActionName);
//        interactAction = InputSystem.actions.FindAction(interactActionName);
//        dropAction = InputSystem.actions.FindAction(dropActionName);
//    }

//    void Update()
//    {
//        HandleMovement();
//        HandleInteractions();
//    }

//    private void HandleMovement()
//    {
//        Vector2 moveValue = moveAction?.ReadValue<Vector2>() ?? Vector2.zero;
//        transform.position += new Vector3(moveValue.x, 0, moveValue.y) * speed * Time.deltaTime;
//    }

//    private void HandleInteractions()
//    {
//        if (interactAction?.WasPerformedThisFrame() == true)
//        {
//            InteractWithNearbyObjects();
//        }

//        if (dropAction?.WasPerformedThisFrame() == true)
//        {
//            HandleDropAction();
//        }
//    }

//    private void InteractWithNearbyObjects()
//    {
//        if (nearbyPot != null)
//        {
//            HandlePotInteraction();
//        }
//        else if (nearbyChest != null)
//        {
//            InteractWithChest();
//        }
//    }

//    private void HandlePotInteraction()
//    {
//        // If holding an item and pot has space
//        if (inventoryItem != null && nearbyPot.AddItem(inventoryItem))
//        {
//            Debug.Log($"{gameObject.tag} deposited item into the pot.");
//            inventoryItem = null;
//        }
//        // If no item and pot has cooked food
//        else if (inventoryItem == null)
//        {
//            GameObject cookedFood = nearbyPot.TakeCooked();
//            if (cookedFood != null)
//            {
//                inventoryItem = cookedFood;
//                Debug.Log($"{gameObject.tag} picked up cooked food from the pot.");
//            }
//        }
//    }

//    private void InteractWithChest()
//    {
//        if (inventoryItem == null)
//        {
//            inventoryItem = nearbyChest.CreateItem();
//            Debug.Log($"{gameObject.tag} picked up item from chest.");
//        }
//        else
//        {
//            Debug.Log($"{gameObject.tag} is already carrying an item.");
//        }
//    }

//    private void HandleDropAction()
//    {
//        if (nearbyPot != null && nearbyPot.TakeCooked() != null)
//        {
//            // Destroy cooked food in the pot
//            Debug.Log($"{gameObject.tag} destroyed cooked food in the pot.");
//        }
//        else if (inventoryItem != null)
//        {
//            // Drop item into fire (destroy)
//            Debug.Log($"{gameObject.tag} dropped item into fire.");
//            inventoryItem = null;
//        }
//    }

//    private void OnTriggerEnter(Collider other)
//    {
//        if (other.CompareTag("CookingPot"))
//        {
//            nearbyPot = other.GetComponent<CookingPotBehaviour>();
//        }
//        else if (other.CompareTag("Chest"))
//        {
//            nearbyChest = other.GetComponent<ChestBehavior>();
//        }
//    }

//    private void OnTriggerExit(Collider other)
//    {
//        if (other.CompareTag("CookingPot"))
//        {
//            nearbyPot = null;
//        }
//        else if (other.CompareTag("Chest"))
//        {
//            nearbyChest = null;
//        }
//    }
//}

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

    private void Start()
    {
        ConfigureInputActions();
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
    }
}