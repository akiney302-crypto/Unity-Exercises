using UnityEngine;
using System.Collections.Generic;
using Random = UnityEngine.Random;

public class ItemSpawner : MonoBehaviour
{
    #region Fields
    [SerializeField] private float _minSpawnTime = 1;
    [SerializeField] private float _maxSpawnTime = 5;
    [SerializeField] private float _nextSpawnTime;
    [SerializeField] private List<Item> _spawnList;
    private float _cronotime = 0;

    #endregion

    #region Unity Callbacks

    void Start()
    {
        ResetTime();
    }
    
    void Update()
    {
        _cronotime += Time.deltaTime;
        if (_cronotime > _nextSpawnTime)
        {
            SpawnItem();
            ResetTime();
        }
    }

    #endregion

    #region Private Methods
    private void ResetTime()
    {
        _cronotime = 0;
        _nextSpawnTime = Random.Range(_minSpawnTime, _maxSpawnTime);
    }

    private void SpawnItem()
    {
        int index = Random.Range(0, _spawnList.Count);
        float xPos = Random.Range(-7f, 7f);
        Vector2 itemPosition = new Vector2(xPos, transform.position.y);
        Item newItem = Instantiate(_spawnList[index], itemPosition, Quaternion.identity);

        if (newItem.Type == Item.ItemType.Water)
            return;

        if (newItem.Type == Item.ItemType.Pebble)
        {
            ItemPebble pebble = newItem.GetComponent<ItemPebble>();

            pebble.SetSize();
        }
        float torqueForce = Random.Range(-100f, 100f);
        newItem.GetComponent<Rigidbody2D>().AddTorque(torqueForce);
    }
    #endregion
}
