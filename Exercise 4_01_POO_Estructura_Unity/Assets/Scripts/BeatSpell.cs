using UnityEngine;

public class BeatSpell : MonoBehaviour
{
    [SerializeField] private DanceTarget target;
    [SerializeField] private float cooldown = 1f;
    public int power = 1;
    private float cooldownTimer = 0f;

    private void Update()
    {
        cooldownTimer = Mathf.Max(0f, cooldownTimer - Time.deltaTime);
    }

    public bool Cast()
    {
        if (target == null)
        {
            Debug.LogWarning("BeatSpell: falta asignar Target.");
            return false;
        }

        if (cooldownTimer > 0f)
        {
            Debug.Log("BeatSpell: en cooldown.");
            return false;
        }

        target.ReceiveBeat(power);
        cooldownTimer = cooldown;
        return true;        
    }
}
