using UnityEngine;

public class DanceTarget : MonoBehaviour
{
    public int requiredHits = 3;
    public float danceTime = 2f;
    private int hits;

    public void ReceiveBeat(int power)
    {
        hits += power;
        if (hits >= requiredHits)
        {
            hits = 0;
        }
    }
}
