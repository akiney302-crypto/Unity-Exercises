using System;
using UnityEngine;

public class PlanetRotation : MonoBehaviour
{
    [SerializeField] private float rotationPeriodHours = 24f;
    [SerializeField] private float simulatedHoursPerSecond = 24f;

    private void Update()
    {
        if (Mathf.Approximately(rotationPeriodHours, 0f)) return;
        float degreesPerHour = 360f/Mathf.Abs(rotationPeriodHours);

        float degreesPerSecond = 360f / Mathf.Abs(rotationPeriodHours);
        float degrees = degreesPerHour * simulatedHoursPerSecond * Time.deltaTime;
        transform.Rotate(Vector3.up, degrees, Space.Self);
    }
}
