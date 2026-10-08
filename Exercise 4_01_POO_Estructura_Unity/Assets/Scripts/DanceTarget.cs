using System.Collections;
using UnityEngine;

public class DanceTarget : MonoBehaviour
{
    public int requiredHits = 3;
    public float danceTime = 2f;
    private int hits;

    public void ReceiveBeat(int power)
    {
        hits += power;
        Debug.Log($"DanceTarget: {hits}/{requiredHits} golpes.");

        if (hits >= requiredHits)
        {
            hits = 0;
            StartCoroutine(Dance());
        }
    }

    private IEnumerator Dance()
    {
        Renderer r = GetComponent<Renderer>();
        Color original = r != null ? r.material.color : Color.white;

        if (r != null)
            r.material.color = Color.cyan;
        
        float elapsed = 0f;
        while (elapsed < danceTime)
        {
            transform.Rotate(0f, 240f * Time.deltaTime, 0f);
            elapsed += Time.deltaTime;
            yield return null;
        }

        if (r != null)
            r.material.color = original;
    }

}
