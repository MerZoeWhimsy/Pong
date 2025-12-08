using System.Collections.Generic;
using UnityEngine;

public class ObjectPool : MonoBehaviour
{
    [SerializeField] private GameObject prefab;
    [SerializeField] private int initialSize = 10;

    private List<GameObject> pool;

    private void Awake()
    {
        pool = new List<GameObject>();

        for (int i = 0; i < initialSize; i++)
        {
            GameObject obj = Instantiate(prefab, Vector3.zero, Quaternion.identity, transform); //create
            obj.SetActive(false);
            pool.Add(obj); //store
        }
    }

    public GameObject GetFromPool(Vector3 position, Quaternion rotation) //pool obj
    {
        for (int i = 0; i < pool.Count; i++)
        {
            if (!pool[i].activeSelf) //inactive
            {
                GameObject obj = pool[i];
                obj.transform.SetPositionAndRotation(position, rotation);
                obj.SetActive(true);
                return obj;
            }
        }

        GameObject newObj = Instantiate(prefab, position, rotation, transform);
        pool.Add(newObj);
        return newObj;
    }

    public void ReturnToPool(GameObject obj)
    {
        obj.SetActive(false);
    }
}