//using UnityEngine;
//using UnityEngine.InputSystem;

//public class PlayerController : MonoBehaviour
//{
//    InputAction moveAction;
//    InputAction interactAction;
//    public float speed;
//    public string inventoryItem = ""; // Inventory can hold one item at a time
//    private Chest_Behavior nearbyChest; // Tracks the chest the player is near

//    private void Start()
//    {
//        if (transform.CompareTag("Player One"))
//        {
//            moveAction = InputSystem.actions.FindAction("MovePlayerOne");
//            interactAction = InputSystem.actions.FindAction("InteractPlayerOne");
//        }
//        if (transform.CompareTag("Player Two"))
//        {
//            moveAction = InputSystem.actions.FindAction("MovePlayerTwo");
//            interactAction = InputSystem.actions.FindAction("InteractPlayerTwo");
//        }
//    }

//    void Update()
//    {
//        // Handle movement
//        Vector2 moveValue = moveAction != null ? moveAction.ReadValue<Vector2>() : Vector2.zero;
//        moveValue *= speed;

//        transform.position += new Vector3(moveValue.x, 0, moveValue.y) * Time.deltaTime;

//        // Handle interaction
//        if (interactAction != null && interactAction.WasPerformedThisFrame())
//        {
//            if (nearbyChest != null)
//            {
//                InteractWithChest();
//            }
//        }
//    }

//    private void OnTriggerEnter(Collider other)
//    {
//        if (other.CompareTag("Chest"))
//        {
//            nearbyChest = other.GetComponent<Chest_Behavior>();
//        }
//    }

//    private void OnTriggerExit(Collider other)
//    {
//        if (other.CompareTag("Chest"))
//        {
//            nearbyChest = null;
//        }
//    }

//    private void InteractWithChest()
//    {
//        if (nearbyChest != null)
//        {
//            // If player already has an item, they can't pick another
//            if (string.IsNullOrEmpty(inventoryItem))
//            {
//                inventoryItem = nearbyChest.GetItem();
//                Debug.Log($"{gameObject.tag} picked up {inventoryItem}");
//            }
//            else
//            {
//                Debug.Log($"{gameObject.tag} is already carrying an item: {inventoryItem}");
//            }
//        }
//    }
//}

using UnityEngine;
using UnityEngine.InputSystem;


public class PlayerController : MonoBehaviour
{
    InputAction moveAction;
    InputAction interactAction;
    InputAction dropAction; // Action to drop item into the fire
    public float speed;
    public string inventoryItem = ""; // Inventory can hold one item at a time
    private CookingPotBehaviour nearbyPot;
    private Chest_Behavior nearbyChest; // Tracks the chest the player is near

    private void Start()
    {
        if (transform.CompareTag("Player One"))
        {
            moveAction = InputSystem.actions.FindAction("MovePlayerOne");
            interactAction = InputSystem.actions.FindAction("InteractPlayerOne");
            dropAction = InputSystem.actions.FindAction("DropPlayerOne");
        }
        if (transform.CompareTag("Player Two"))
        {
            moveAction = InputSystem.actions.FindAction("MovePlayerTwo");
            interactAction = InputSystem.actions.FindAction("InteractPlayerTwo");
            dropAction = InputSystem.actions.FindAction("DropPlayerTwo");
        }
    }

    void Update()
    {
        // Handle movement
        Vector2 moveValue = moveAction != null ? moveAction.ReadValue<Vector2>() : Vector2.zero;
        moveValue *= speed;

        transform.position += new Vector3(moveValue.x, 0, moveValue.y) * Time.deltaTime;

        // Handle interactions
        if (interactAction != null && interactAction.WasPerformedThisFrame())
        {
            if (nearbyPot != null)
            {
                DepositItemToPot();
            }
            if (nearbyChest != null)
            {
                InteractWithChest();
            }
        }

        // Handle dropping items into the fire
        if (dropAction != null && dropAction.WasPerformedThisFrame())
        {
            DropItemIntoFire();
        }
    }

    //private void OnTriggerEnter(Collider other)
    //{
    //    if (other.CompareTag("CookingPot"))
    //    {
    //        nearbyPot = other.GetComponent<CookingPotBehaviour>();
    //    }
    //}

    //private void OnTriggerExit(Collider other)
    //{
    //    if (other.CompareTag("CookingPot"))
    //    {
    //        nearbyPot = null;
    //    }
    //}
    private void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("CookingPot"))
        {
            nearbyPot = other.GetComponent<CookingPotBehaviour>();
        }
        else if (other.CompareTag("Chest"))
        {
            nearbyChest = other.GetComponent<Chest_Behavior>();
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


    private void InteractWithChest()
    {
        if (nearbyChest != null)
        {
            // If player already has an item, they can't pick another
            if (string.IsNullOrEmpty(inventoryItem))
            {
                inventoryItem = nearbyChest.GetItem();
                Debug.Log($"{gameObject.tag} picked up {inventoryItem}");
            }
            else
            {
                Debug.Log($"{gameObject.tag} is already carrying an item: {inventoryItem}");
            }
        }
    }

    private void DepositItemToPot()
    {
        if (!string.IsNullOrEmpty(inventoryItem))
        {
            if (nearbyPot.AddItem(inventoryItem))
            {
                Debug.Log($"{gameObject.tag} deposited {inventoryItem} into the pot.");
                inventoryItem = ""; // Clear player's inventory
            }
            else
            {
                Debug.Log($"The pot is full. {inventoryItem} is destroyed in the fire.");
                inventoryItem = ""; // Destroy the item
            }
        }
        else
        {
            Debug.Log($"{gameObject.tag} has no item to deposit.");
        }
    }

    private void DropItemIntoFire()
    {
        if (!string.IsNullOrEmpty(inventoryItem))
        {
            Debug.Log($"{gameObject.tag} dropped {inventoryItem} into the fire. It is destroyed.");
            inventoryItem = ""; // Destroy the item
        }
        else
        {
            Debug.Log($"{gameObject.tag} has no item to drop.");
        }
    }
}
