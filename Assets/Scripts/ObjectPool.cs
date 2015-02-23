using UnityEngine;
using System.Collections;
using System.Collections.Generic;

public class ObjectPool 
{

    Stack<IPoolableObject> pool;

    public GameObject objReference;
    public bool canGrow;
    public int poolSize;
        
    public ObjectPool(GameObject reference, bool grow, int size)
    {
        objReference = reference;
        canGrow = grow;
        poolSize = size;

        pool = new Stack<IPoolableObject>();
        for(int i = 0; i < poolSize; i++)
        {
            pool.Push(CreateObject());
        }
    }

    IPoolableObject CreateObject()
    {
        GameObject obj = GameObject.Instantiate(objReference) as GameObject;
        obj.SetActive(false);

        IPoolableObject pooledObj = obj.GetComponent(typeof(IPoolableObject)) as IPoolableObject;
        if (pooledObj == null)
            Debug.LogError("No pooled object for " + obj.name, obj);

        return pooledObj;
    }

    public void PushObject(IPoolableObject obj)
    {
        pool.Push(obj);
    }

    public GameObject PullObject()
    {  
        if (pool.Count == 0)
        {
            Debug.LogWarning("Pool is empty!");
            if(canGrow)
            {
                poolSize++;
                GameObject createdObj = CreateObject().gameObject;
                return createdObj;
            }
            return null;
        }
        IPoolableObject obj = pool.Pop();
        return obj.gameObject;
    }
}
