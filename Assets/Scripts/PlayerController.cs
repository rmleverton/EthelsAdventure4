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
    private InputAction catAction;
    private InputAction openBookAction;
    private InputAction pageUpAction;
    private InputAction pageDownAction;

    public Animator anim;

    private Cat nearbyCat;

    [SerializeField] SpriteRenderer inventoryItemUI;

    private void Start()
    {
        ConfigureInputActions();
        anim = GetComponentInChildren<Animator>();
    }

    private void ConfigureInputActions()
    {
        string moveActionName = transform.CompareTag("Player One") ? "MovePlayerOne" : "MovePlayerTwo";
        string interactActionName = transform.CompareTag("Player One") ? "InteractPlayerOne" : "InteractPlayerTwo";
        string dropActionName = transform.CompareTag("Player One") ? "DropPlayerOne" : "DropPlayerTwo";
        string catActionName = transform.CompareTag("Player One") ? "CatPlayerOne" : "CatPlayerTwo";


        moveAction = InputSystem.actions.FindAction(moveActionName);
        interactAction = InputSystem.actions.FindAction(interactActionName);
        dropAction = InputSystem.actions.FindAction(dropActionName);
        catAction = InputSystem.actions.FindAction(catActionName);

        //Handle Medicine Book. 
        openBookAction = InputSystem.actions.FindAction("OpenBook");
        pageUpAction = InputSystem.actions.FindAction("PageUp");
        pageDownAction = InputSystem.actions.FindAction("PageDown");

    }

    void Update()
    {
        HandleMovement();
        HandleInteractions();
    }

    private void HandleMovement()
    {
        Vector2 moveValue = moveAction?.ReadValue<Vector2>() ?? Vector2.zero;
        transform.position += new Vector3((float)moveValue.x, 0, (float)moveValue.y) * speed * Time.deltaTime;


        anim.SetFloat("speedX", moveValue.x);
        anim.SetFloat("speedY", moveValue.y);
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
        if (catAction?.WasPerformedThisFrame() == true)
        {
            InteractWithCat();
        }
        if (openBookAction?.WasPerformedThisFrame() == true)
        {
            OpenBook();
        }
        if (pageUpAction?.WasPerformedThisFrame() == true)
        {
            PageUp();
        }
        if (pageDownAction?.WasPerformedThisFrame() == true)
        {
            PageDown();
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

    private void InteractWithCat()
    {
        if (nearbyCat != null && nearbyCat.CanBeFed)
        {
            HandleCatFeeding();
        }
        else
        {
            Debug.Log("Cannot Feed Cat");
        }
    }

    

    private void HandlePotInteraction()
    {
        if (inventoryItem == null) // Pick up cooked food
        {
            GameObject cookedFood = nearbyPot.TakeCooked();
            if (cookedFood != null)
            {
                ItemInstance itemInstance = cookedFood.GetComponent<ItemInstance>();
                inventoryItem = cookedFood;
                inventoryItemUI.sprite = itemInstance.itemData.itemSprite;

                Debug.Log($"{gameObject.tag} picked up {itemInstance.itemData.itemName} from the cooking pot.");
            }
            else
            {
                Debug.Log("No cooked food available in the pot.");
            }
        }
        else // Deposit item
        {
            if (nearbyPot.AddItem(inventoryItem))
            {
                Debug.Log($"{gameObject.tag} deposited {inventoryItem.name} into the cooking pot.");
                inventoryItem = null;
                inventoryItemUI.sprite = null;
            }
            else
            {
                Debug.Log("Cannot deposit item into the pot.");
            }
        }
    }


    private void HandleCatFeeding()
    {
        // Check if player has cooked food
        if (inventoryItem != null)
        {
            ItemInstance itemInstance = inventoryItem.GetComponent<ItemInstance>();
            if (itemInstance?.itemData?.disease != null)
            {
                // Feed the cat
                nearbyCat.Feed(itemInstance);

                // Destroy the food
                Destroy(inventoryItem);
                inventoryItem = null;
                inventoryItemUI.sprite = null;

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

                inventoryItemUI.sprite = itemInstance.itemData.itemSprite;
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
        // If near a cooking pot
        if (nearbyPot != null)
        {
            // Drop the player's inventory item into the pot fire
            if (inventoryItem != null)
            {
                string itemName = inventoryItem.GetComponent<ItemInstance>()?.itemData?.itemName ?? "unknown item";
                Debug.Log($"{gameObject.tag} dropped {itemName} into the fire.");
                Destroy(inventoryItem);
                inventoryItem = null;
                inventoryItemUI.sprite = null;
            }
            // Destroy cooked food from the pot if inventory is empty
            else
            {
                GameObject cookedFood = nearbyPot.TakeCooked();
                if (cookedFood != null)
                {
                    string foodName = cookedFood.GetComponent<ItemInstance>()?.itemData?.itemName ?? "unknown cooked food";
                    Debug.Log($"{gameObject.tag} destroyed {foodName} from the cooking pot.");
                    Destroy(cookedFood);
                }
                else
                {
                    Debug.Log($"{gameObject.tag} has no item to drop, and no cooked food to destroy.");
                }
            }
        }
        else
        {
            Debug.Log($"{gameObject.tag} can't drop an item here.");
        }
    }

    private void OpenBook()
    {

    }

    private void CloseBook()
    {

    }

    private void PageUp()
    {

    }

    private void PageDown()
    {

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
            nearbyCat.highlighted = true;
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
            nearbyCat.highlighted = false;
            nearbyCat = null;
        }
    }
}