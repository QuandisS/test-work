using System.Collections.Generic;
using UnityEngine;

public class BulletPooler : MonoBehaviour
{
    [SerializeField] private int bulletPoolSize;
    [SerializeField] private GameObject bulletPrefab;
    private readonly Queue<GameObject> _bulletPool = new Queue<GameObject>();

    public static BulletPooler Instance;

    private void Awake()
    {
        Instance = this;
    }

    private void Start()
    {
        for (var i = 0; i < bulletPoolSize; i++)
        {
            var obj = Instantiate(bulletPrefab);
            obj.SetActive(false);
            _bulletPool.Enqueue(obj);
        }
    }

    public GameObject SpawnFromPool(Vector3 position, Quaternion rotation)
    {
        var objectToSpawn = _bulletPool.Dequeue();
        objectToSpawn.SetActive(true);
        objectToSpawn.transform.position = position;
        objectToSpawn.transform.rotation = rotation;
        _bulletPool.Enqueue(objectToSpawn);
        return objectToSpawn;
    }

}
