using UnityEngine;
using UnityEngine.InputSystem;

public class ArcadeRunner : MonoBehaviour
{
    [SerializeField] private InputActionReference move;
    [SerializeField] private InputActionReference sprint;
    [SerializeField] private float speed = 4f;
    [SerializeField] private float sprintMultiplier = 1.0f;

    private void OnEnable()
    {
        move.action.Enable();
        sprint.action.Enable();        
    }

    private void OnDisable()
    {
        move.action.Disable();
        sprint.action.Disable();        
    }

    private void Update()
    {
        Vector2 input = move.action.ReadValue<Vector2>();
        if (input.sqrMagnitude > 1f)
            input.Normalize();
        
        float currentSpeed = sprint.action.IsPressed()
            ? speed * sprintMultiplier
            : speed;
        
        Vector3 direction = new Vector3(input.x, 0f, input.y);
        transform.position += direction * currentSpeed * Time.deltaTime;
    }
}
