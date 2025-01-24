using UnityEngine;
using UnityEngine.InputSystem;

public class PlayerController : MonoBehaviour
{
    InputAction moveAction;
    InputAction interactAction;
    public float speed;
    public string ingredient;

    private void Start()
    {
        moveAction = InputSystem.actions.FindAction("Move");
        interactAction = InputSystem.actions.FindAction("Interact");
    }

    void Update()
    {
        Vector2 moveValue = moveAction.ReadValue<Vector2>();
        moveValue *= speed;
        // your code would then use moveValue to apply movement
        // to your GameObject
        this.transform.position = new Vector3(this.transform.position.x + moveValue.x, this.transform.position.y, this.transform.position.z + moveValue.y);
    }

    

}
