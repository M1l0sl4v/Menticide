using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Rendering;
using static ObjectPoolManager;

public class ObjectPoolManager : MonoBehaviour
{

    //this is a list of the object pools
  public static List<PooledObjectInfo> ObjectPools = new List<PooledObjectInfo>();

    private static GameObject Enemytype1empty;
    private static GameObject Enemytype2empty;
    private static GameObject objectPoolEmptyHolder;
    private static GameObject pathEmpty;
    private static GameObject treeEmpty;
    public GameObject parent;

    //setting up the empty objects in the hierarchy
    public enum PoolType
    {
        //list of object to be sorted into emptys
        Enemytype1,
        Enemytype2,
        Path,
        Tree,
        None
    }
    public static PoolType PoolingType;
    //calls the empty object method before the start
    private void Awake()
    {
        SetupEmpties();
    }
    //creates a single parent, then subcatagories under that
    private void SetupEmpties()
    {
        objectPoolEmptyHolder = new GameObject("pooled objects"); 
        objectPoolEmptyHolder.transform.SetParent(parent.transform);
        //setting up the original empties
        Enemytype1empty = new GameObject("commonenemies");
        Enemytype1empty.transform.SetParent(objectPoolEmptyHolder.transform);

        Enemytype2empty = new GameObject("rareenemies");
        Enemytype2empty.transform.SetParent(objectPoolEmptyHolder.transform);

        pathEmpty = new GameObject(("pathTiles "));
        pathEmpty.transform.SetParent((objectPoolEmptyHolder.transform));

        treeEmpty = new GameObject(("treetiles"));
        treeEmpty.transform.SetParent((objectPoolEmptyHolder.transform));
    }

    private static GameObject SetParentObject(PoolType poolType)
    {
        //assigning the catagories of prefab to their gameobjects
        switch (poolType)
        {
            case PoolType.Enemytype1:
                return Enemytype1empty;
            case PoolType.Enemytype2:
                return Enemytype2empty;
            case PoolType.Path:
                return pathEmpty;
            case PoolType.Tree:
                return treeEmpty;
            case PoolType.None:
                return null;
            default:
                return null;
        }
    }


    //when this is called it looks for the corrisponding pool
    public static GameObject SpawnObject(GameObject objectToSpawn, Vector3 spawnPosition, PoolType poolType = PoolType.None)//this creates another perameter for spawning,
                                                                                                                            //it makes it search through the empties to pull from
    {
        //tells us how many objects are in each pool
        /*Debug.Log("amount in common pool " + Enemytype1empty.transform.childCount);
        Debug.Log( "amount in rare pool " + Enemytype2empty.transform.childCount);
        Debug.Log("amount in path pool " + pathEmpty.transform.childCount);
        Debug.Log("amount in tree pool " + treeEmpty.transform.childCount);*/
        
        PooledObjectInfo pool = null;
        
        //finds the object in the pool
        foreach (PooledObjectInfo info in ObjectPools)
        {
            if(info.LookupString == objectToSpawn.name)
            {
                pool = info;
                break;
            }
        }

        //if it cant find the pool or object, it finds the prefab and adds it to the list of pooled object, does not spawn it, yet i dont think
        if (pool == null)
        {
            pool = new PooledObjectInfo() { LookupString = objectToSpawn.name };
            ObjectPools.Add(pool);
        }

        //check for inactive objects in the pool
        GameObject spawnableObj = null;
        foreach (GameObject Obj in pool.InactiveObjects)
        {
            if (Obj != null)
            {
                spawnableObj = Obj;
                break;
            }
        }
        
        //this is called when it finds an inactive object in the pool. it then 

        if (spawnableObj == null && objectToSpawn != null)
        {

            //starts the parent method, sorting the prefab
            GameObject parentObject = SetParentObject(poolType);

            //this instantiates the object to spawn. this is the actual and only sspawning of objects
            spawnableObj = Instantiate(objectToSpawn, spawnPosition,Quaternion.identity);

            //if the parent object is not null, it will parent the object to it
            if(parentObject != null)
            {
                spawnableObj.transform.SetParent(parentObject.transform);
            }
        }

        else
        {
            //finally, if/when it finds the object in the pool, it actually activates the inactive object at the set possition
            spawnableObj.transform.position = spawnPosition;
            pool.InactiveObjects.Remove(spawnableObj);  
            spawnableObj.SetActive(true);
        }
        

        return spawnableObj;
    }

    //to deactivate the object
    public static void ReturnObjectToPool(GameObject obj) 
    {
        PooledObjectInfo pool = null;

        string clonename = obj.name.Substring(0, obj.name.Length - 7); //allows to find objects even if they have the (clone) ending. shamelessly stole this from a guide
        
        foreach (PooledObjectInfo info in ObjectPools)
        {
            if (info.LookupString == clonename)
            {
                pool = info;
                break;
            }
        }
        if (pool == null)
        {
            Debug.LogWarning("there is no pooled object to destroy");
        }
        else
        {
            obj.SetActive(false);
            pool.InactiveObjects.Add(obj);
        }

    }
}

//this helps find the information needed to work with each prefab, the main method uses this to check if the object is pooled in the inactive loops.
public class PooledObjectInfo
{
    public string LookupString;
    public List<GameObject> InactiveObjects = new List<GameObject> ();
}
