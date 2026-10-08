using UnityEngine;
using UnityEngine.InputSystem;

public class GhostDJ : MonoBehaviour
{
    [SerializeField] private BeatSpell selectedBeat;
    public float energy = 30f;
    private int combo;

    public void Update()
    {
        if (Keyboard.current != null && Keyboard.current.spaceKey.wasPressedThisFrame)
            PerformBeat();
    }

    public void PerformBeat()
    {
        if (selectedBeat == null)
        {
            Debug.LogWarning("GhostDJ: falta asignar Selected Beart.");
            return;
        }

        if (energy < 10f)
        {
            Debug.Log("GhostDJ: no hay energia suficiente");
            return;
        }

        if (selectedBeat.Cast())
        {
            energy -= 10f;
            combo++;
            Debug.Log($"Beat lanzado. Combo: {combo} | Energia: {energy}");
        }
    }

    private void OnGUI()
    {
        GUI.Label(new Rect(20, 20, 380, 25), $"ESPACIO = lanzar beat | Energia: {energy:0} | Combo: {combo}");
    }


}
