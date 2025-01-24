using UnityEngine;
using UnityEngine.InputSystem;

public class PlayerController : MonoBehaviour
{
    InputAction moveAction;
    public float speed;

    private void Start()
    {
        moveAction = InputSystem.actions.FindAction("Move");
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
