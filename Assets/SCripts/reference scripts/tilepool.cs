using JetBrains.Annotations;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class tilepool : MonoBehaviour
{
    public static tilepool SharedInstance;
    public List<GameObject> pooledObjects;
    public List<GameObject> objectsToPool;
    public int amountToPool;


    public GameObject GetPooledObject()
    {
        //figures out how many objects need to be pooled
        for (int i = 0; i < amountToPool; i++)
        {
            if (!pooledObjects[i].activeInHierarchy)
            {
                return pooledObjects[i];
            }
        }
        return null;
    }
    //for other scripts to call
    private void Awake()
    {
        SharedInstance = this;
    }

    void Start()
    {
        //instantiating the correct about of objects
        pooledObjects = new List<GameObject>();
        for(int i = 0;i < amountToPool; i++)
        {
          //  tmp = Instantiate(objectsToPool);
          //  tmp.SetOnlyActive(false);
          //  pooledObjects.Add(tmp);
        }
    }//this can be comented back in

}
