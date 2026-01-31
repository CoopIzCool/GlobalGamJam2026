using System.Collections;
using System.Collections.Generic;
using UnityEngine;


public class ObjectPooler : Singleton<ObjectPooler>
{
    #region Classes
    [System.Serializable]
    public class Pool
    {
        public string tag;
        public GameObject prefab;
        public int size;
    }
    #endregion

    #region Fields
    public List<Pool> pools;
    public Dictionary<string, Queue<GameObject>> poolDictionary;

    #endregion
    // Start is called before the first frame update
    void Start()
    {

    }

    //Gets reference to the pool to pull from and set it active
    public GameObject SpawnFromPool(string tag, Vector3 pos, Quaternion quat)
    {
        if (!poolDictionary.ContainsKey(tag))
        {
            Debug.LogWarning("Tag " + tag + " does not exist");
            return null;
        }
        GameObject objectSpawned = poolDictionary[tag].Dequeue();
        GameObject firstObject = objectSpawned;
        //Goes through list to make sure an inactive, or last active object in the pool, is spawned. Not the most efficient but I'll get back to it. -Ryan
        while(objectSpawned.activeInHierarchy)
        {
            poolDictionary[tag].Enqueue(objectSpawned);
            objectSpawned = poolDictionary[tag].Dequeue();
            //Debug.Log("OOPS");
            if(objectSpawned == firstObject)
            {
                break;
            }
        }
        objectSpawned.transform.position = pos;
        objectSpawned.transform.rotation = quat;
        objectSpawned.SetActive(true);
        objectSpawned.GetComponent<Rigidbody2D>().bodyType = RigidbodyType2D.Dynamic;

        poolDictionary[tag].Enqueue(objectSpawned);


        //Logic for if the object spawned has seperate logic to start
        IPooledObject [] iPooled = objectSpawned.GetComponentsInChildren<IPooledObject>();
        foreach(IPooledObject ipo in iPooled) {
            ipo.OnObjectSpawn();
        }

        return objectSpawned;
    }

}