using System;
using System.Collections.Generic;
using UnityEngine;

public class ObjectPooler : Singleton<ObjectPooler>
{
    private Dictionary<Type, Pool> pools = new Dictionary<Type, Pool>();

    public void AddToPool(MonoBehaviour obj)
    {
        if (pools.ContainsKey(obj.GetType()))
        {
            pools[obj.GetType()].PushObject(obj);
            return;
        }

        Pool newPool = CreateNewPool(obj);
        newPool.PushObject(obj);
    }

    public GameObject CreateObject(MonoBehaviour sample, Vector3 position, Quaternion rotation, Transform parent = null)
    {
        if(pools.ContainsKey(sample.GetType()) == false)
            CreateNewPool(sample);

        GameObject newGO = pools[sample.GetType()].GetObject();
        newGO.transform.position = position;
        newGO.transform.SetParent(parent);
        newGO.transform.rotation = rotation;

        if (newGO.TryGetComponent<IRecovered>(out var obj))
            obj.RecoverState();

        return newGO;
    }

    private Pool CreateNewPool(MonoBehaviour sample)
    {
        Pool newPool = new Pool(transform, sample);
        pools.Add(sample.GetType(), newPool);
        return newPool;
    }
}

[SerializeField]
public class Pool
{
    private MonoBehaviour sample;
    private Queue<MonoBehaviour> objects;
    private Transform collector;

    public Pool(Transform pooler, MonoBehaviour sample)
    {
        GameObject collector = new GameObject(sample.GetType().Name);
        collector.transform.SetParent(pooler);

        this.collector = collector.transform;
        this.sample = sample;

        objects = new Queue<MonoBehaviour>();
    }

    public GameObject GetObject()
    {
        if (objects.Count == 0)
            return GameObject.Instantiate(sample.gameObject);

        MonoBehaviour obj = objects.Dequeue();

        obj.gameObject.SetActive(true);
        obj.gameObject.transform.SetParent(null);

        return obj.gameObject;
    }

    public void PushObject(MonoBehaviour obj)
    {
        obj.gameObject.SetActive(false);
        obj.transform.SetParent(collector);
        objects.Enqueue(obj);
    }
}
