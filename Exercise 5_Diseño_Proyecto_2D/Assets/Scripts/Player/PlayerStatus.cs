using System;
using UnityEngine;

public class PlayerStatus : MonoBehaviour
{
    public event Action<bool> StunChanged;
    public event Action<bool> WetChanged;

    public bool IsStunned { get; private set; }
    public bool IsWet { get; private set; }

    public float MovRalent => IsWet ? 0.5f : 1f;

    public void Stun(float duration)
    {
        IsStunned = true;
        StunChanged?.Invoke(true);

        CancelInvoke(nameof(RecoverFromStun));

        if (duration > 0f)
            Invoke(nameof(RecoverFromStun), duration);
    }

    public void RecoverFromStun()
    {
        CancelInvoke(nameof(RecoverFromStun));

        IsStunned = false;
        StunChanged?.Invoke(false);
    }

    public void Wet(float duration)
    {
        IsWet = true;
        WetChanged?.Invoke(true);

        CancelInvoke(nameof(RemoveWet));

        if (duration > 0f)
            Invoke(nameof(RemoveWet), duration);
    }

    public void RemoveWet()
    {
        CancelInvoke(nameof(RemoveWet));

        IsWet = false;
        WetChanged?.Invoke(false);
    }
}
