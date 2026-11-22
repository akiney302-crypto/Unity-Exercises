using UnityEngine;

public class Item : MonoBehaviour, IRecolectable
{   
    #region Enums
    public enum ItemType
    {
        None,
        Pebble,
        Sunray,
        Water
    }
    #endregion

    #region Properties
    [field: SerializeField] public ItemType Type { get; private set; }
    #endregion

    #region Fields
    [SerializeField] private GameObject _particles;
    #endregion

    #region Unity Callbacks    
    #endregion

    #region Public Methods    
    public void Recolected()
    {
        Destroy(gameObject);
        CreateParticles();        
    }
    #endregion

    #region Private Methods
    private void CreateParticles()
    {
        Instantiate(_particles, transform.position, Quaternion.identity);
    }
    #endregion

}
