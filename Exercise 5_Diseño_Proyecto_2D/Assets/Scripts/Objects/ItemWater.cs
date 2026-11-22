using UnityEngine;

public class ItemWater : Item
{
    #region Constants    
    const float WATER_WET = -10;
    const float WATER_WET_DURATION = 4;
    #endregion

    #region Unity Callbacks
    private void OnCollisionEnter2D(Collision2D collision)
    {
        if (collision.gameObject.tag == "Ground" || collision.gameObject.tag == "Platform")
            Recolected();

        if (collision.gameObject.tag == "Player")
        {
            Wings wings = collision.gameObject.GetComponent<Wings>();
            PlayerStatus wetStatus = collision.gameObject.GetComponent<PlayerStatus>();
            if (wetStatus != null)
                wetStatus.Wet(WATER_WET_DURATION);
            else if (wings != null)
                wings.Wet(WATER_WET_DURATION);
            if (wings != null)
                wings.Regenerate(WATER_WET);
            Recolected();
        }


    }

    #endregion
}
