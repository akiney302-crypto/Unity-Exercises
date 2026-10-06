using UnityEngine;

public class GhostDJ : MonoBehaviour
{
    [SerializeField] private BeatSpell selectedBeat;
    public float energy = 100f;
    private int combo;

    public void PerformBeat()
    {
        if (selectedBeat != null && energy >= 10f)
        {
            selectedBeat.Cast();
            energy -= 10f;
            combo++;
        }
    }


}
