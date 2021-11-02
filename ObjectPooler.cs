/* A standard template and parent class of Object Pooling.
 * Implement of Object Pooling Function.
 * Replace Instantiate and Destroy with SetActive to optimaize performance.
 */
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ObjectPooler : MonoBehaviour
{
    [SerializeField] GameObject _objectToPool;
    [SerializeField] int _amountToPool;

    private List<GameObject> _pooledObjects = new List<GameObject>();
    private int _current = 0;

    private void Awake()
    {
        
    }
    private void Start()
    {
        // Generate Object Pool, and set them inactive.
        for (int i = 0; i < _amountToPool; i++)
        {
            _pooledObjects.Add(Instantiate(_objectToPool,transform));
            _pooledObjects[_pooledObjects.Count - 1].SetActive(false);
        }

    }
    public GameObject GetNextInactivePooledObject()
    {
        int _loopCount = 0; // To prevent Death Loop
        while (true)
        {
            if (!_pooledObjects[_current].activeInHierarchy) { return _pooledObjects[_current]; }
            _current++;
            _current %= _pooledObjects.Count;

            _loopCount++;
            if (_loopCount > 2 * _amountToPool) { return null; }
        }
    }
}
