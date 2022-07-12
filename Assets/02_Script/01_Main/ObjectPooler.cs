using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ObjectPooler : MonoBehaviour
{
    [SerializeField] private GameObject[] prefabs;
    [SerializeField] private int poolSize;
    private Dictionary<string, GameObject> prefabQueue = new Dictionary<string, GameObject>();
    private Dictionary<string, Stack<GameObject>> poolers = new Dictionary<string, Stack<GameObject>>();

    public Dictionary<string, Stack<GameObject>> Poolers => poolers;

    private void Awake()
    {
        for (int i = 0; i < prefabs.Length; i++)
        {
            for (int j = 0; j < poolSize; j++)
            {
                if (!prefabQueue.ContainsKey(prefabs[i].name))
                {
                    prefabQueue.Add(prefabs[i].name, prefabs[i]);
                }

                if (!poolers.ContainsKey(prefabs[i].name))
                {
                    poolers.Add(prefabs[i].name, new Stack<GameObject>());
                }

                GameObject prefab = Instantiate(prefabs[i]);
                prefab.name = prefab.name.Replace("(Clone)", null);

                prefab.SetActive(false);
                poolers[prefabs[i].name].Push(prefab);
            }
        }
    }

    public GameObject SpawnPrefab(string key)
    {
        GameObject prefab;

        if (poolers[key].Count > 0)
        {
            prefab = poolers[key].Pop();
            prefab.SetActive(true);
        }
        else
        {
            prefab = Instantiate(prefabQueue[key]);
            prefab.name = prefab.name.Replace("(Clone)", null);
        }

        return prefab;
    }

    public void DespawnPrefab(GameObject prefab)
    {
        prefab.SetActive(false);
        poolers[prefab.name].Push(prefab);
    }
}
