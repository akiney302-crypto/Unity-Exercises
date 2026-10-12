
using UnityEngine;

public class PlanetOrbit : MonoBehaviour
{
    // Escala visual: 1 AU equivale a 12 unidades de Unity.
    private const float UnityUnitsPerAU = 12f;

    [Header("Referencias")]
    [SerializeField] private Transform sun;

    [Header("Datos orbitales")]
    [Min(0f)]
    [SerializeField] private float semiMajorAxisAU = 1f;

    [Range(0f, 0.99f)]
    [SerializeField] private float eccentricity = 0.0167f;

    [Min(0f)]
    [SerializeField] private float orbitalPeriodDays = 365.26f;

    [SerializeField] private float inclinationDegrees = 0f;
    [SerializeField] private float startingMeanAnomalyDegrees = 0f;

    [Header("Velocidad de simulacion")]
    [SerializeField] private float simulatedDaysPerSecond = 1f;

    private double elapsedDays;

    private void Update()
    {
        if (sun == null ||
            orbitalPeriodDays <= 0f ||
            semiMajorAxisAU <= 0f)
        {
            return;
        }

        elapsedDays += Time.deltaTime * simulatedDaysPerSecond;

        // Convertir AU a unidades de Unity.
        float a = semiMajorAxisAU * UnityUnitsPerAU;

        // Anomalia media.
        double meanMotion = 2.0 * System.Math.PI / orbitalPeriodDays;

        float M = Mathf.Repeat(
            startingMeanAnomalyDegrees * Mathf.Deg2Rad
            + (float)(meanMotion * elapsedDays),
            2f * Mathf.PI
        );

        // Resolver la ecuacion de Kepler.
        float E = M;

        for (int i = 0; i < 8; i++)
        {
            float f = E - eccentricity * Mathf.Sin(E) - M;
            float fp = 1f - eccentricity * Mathf.Cos(E);

            E -= f / fp;
        }

        // Posicion en el plano orbital.
        float x = a * (Mathf.Cos(E) - eccentricity);

        float z = a * Mathf.Sqrt(
            1f - eccentricity * eccentricity
        ) * Mathf.Sin(E);

        Vector3 position = new Vector3(x, 0f, z);

        // Aplicar inclinacion orbital.
        position = Quaternion.Euler(
            inclinationDegrees, 0f, 0f
        ) * position;

        // Cada planeta se posiciona respecto al Sol.
        transform.position = sun.position + position;
    }
}
