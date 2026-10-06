using UnityEngine;
using UnityEngine.InputSystem;

public class DroneMagnet : MonoBehaviour
{
    [SerializeField] private Camera sceneCamera;
    [SerializeField] private float moveSpeed = 6f;
    [SerializeField] private InputActionReference moveToCursor;

    private void OnEnable()
    {
        moveToCursor.action.Enable();
    }

    private void OnDisable()
    {
        moveToCursor.action.Disable();       
    }

    private void Update()
    {
        if (!moveToCursor.action.IsPressed()) return;
        Vector2 mouse = Mouse.current.position.ReadValue();
        Ray ray = sceneCamera.ScreenPointToRay(mouse);
        Plane ground = new Plane(Vector3.up, Vector3.zero);

        if (ground.Raycast(ray, out float distance))
        {
            Vector3 target = ray.GetPoint(distance);
            transform.position = Vector3.MoveTowards(transform.position, target, moveSpeed * Time.deltaTime);
        }

    }
}
