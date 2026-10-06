using UnityEngine;

public class BeatSpell : MonoBehaviour
{
    [SerializeField] private DanceTarget target;
    public int power = 1;
    private float cooldown;

    public void Cast()
    {
        if (cooldown > 0f || target == null) return;
        target.ReceiveBeat(power);
        cooldown = 0.5f;
    }

    private void Update()
    {
        cooldown = Mathf.Max(0f, cooldown - Time.deltaTime);
    }
}
