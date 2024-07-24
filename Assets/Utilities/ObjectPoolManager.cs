using System.Collections.Generic;
using System.Linq;
using UnityEngine;

public enum PoolType
{
    BattleUnit, Particles, None
}

public class ObjectPoolManager : MonoBehaviour
{
    public static List<PooledIbjectInfo> ObjectPools = new List<PooledIbjectInfo>();

    public static PoolType PoolingType;

    private GameObject objectPoolEmptyHolder;
    private static GameObject battleUnitEmpty;
    private static GameObject particlesEmpty;


    private void Awake() => SetUpEmpties();

    public static GameObject SpawnObject(GameObject objectToSpwan, Vector3 spawnPosition, Quaternion spawnRotation, PoolType poolType = PoolType.None) 
    {
        PooledIbjectInfo pool = ObjectPools.Find(p => p.LookUpString == objectToSpwan.name);

        // If pool doesn't exist
        if(pool == null) 
        {
            pool = new PooledIbjectInfo() { LookUpString = objectToSpwan.name };
            ObjectPools.Add(pool);
        }

        //if there any inactive objects in the pool
        GameObject spawnableObj = pool.InactiveIbjects.FirstOrDefault();

        if(spawnableObj == null) 
        {
            // Find the parent of the empty object
            GameObject parentObj = SetParentObject(poolType);
            
            spawnableObj = Instantiate(objectToSpwan, spawnPosition, spawnRotation);

            if (parentObj != null) spawnableObj.transform.SetParent(parentObj.transform);
        }
        else 
        {
            spawnableObj.transform.position = spawnPosition;
            spawnableObj.transform.rotation = spawnRotation;
            pool.InactiveIbjects.Remove(spawnableObj);
            spawnableObj.SetActive(true);
        }

        return spawnableObj;
    }

    public static void ReturnObjectToPool(GameObject obj, PoolType poolType) 
    {
        string goName = obj.name.Contains("(Clone)") ? obj.name.Substring(0,obj.name.Length - 7) : obj.name;

        PooledIbjectInfo pool = ObjectPools.Find(p => p.LookUpString == goName);

        if(pool == null)
        {
            Debug.LogWarning("Trying to release an object that is not pooled" + obj.name);
        }
        else 
        {
            obj.SetActive(false);
            pool.InactiveIbjects.Add(obj);
        }
    }



    private void SetUpEmpties()
    {
        objectPoolEmptyHolder = new GameObject("Pooled Objects");

        battleUnitEmpty = new GameObject("Battle Units");
        battleUnitEmpty.transform.SetParent(objectPoolEmptyHolder.transform);

        particlesEmpty = new GameObject("Particles");
        particlesEmpty.transform.SetParent(objectPoolEmptyHolder.transform);
    }

    private void SetUpPool(string poolName) 
    {
        var pool = new GameObject(poolName);
        pool.transform.SetParent(objectPoolEmptyHolder.transform);
    }
    private static GameObject SetParentObject(PoolType poolType) 
    {
        switch (poolType) 
        {
            case PoolType.BattleUnit:
                return battleUnitEmpty;
            case PoolType.Particles:
                return particlesEmpty;
            case PoolType.None: 
                return null;
            default: return null;
        }
    }
}

public class PooledIbjectInfo 
{
    public string LookUpString;
    public List<GameObject> InactiveIbjects = new List<GameObject>(); 
}
