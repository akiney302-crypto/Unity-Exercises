using UnityEngine;

public class AxialRotation : MonoBehaviour
{
    [SerializeField] private float degreesPerSecond = 20f;

    private void Update()
    {
        transform.Rotate(Vector3.up, degreesPerSecond * Time.deltaTime, Space.Self);

        Debug.DrawRay(transform.position, transform.up * 2f, Color.red);
        Debug.DrawRay(transform.position, transform.forward * 3f, Color.blue);
    }
}
