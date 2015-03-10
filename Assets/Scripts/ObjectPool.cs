using UnityEngine;
using System.Collections;
using System.Collections.Generic;

public class ObjectPool : MonoBehaviour
{
	public static ObjectPool instance;

	public GameObject[] objectPrefabs;

	public List<GameObject>[] pooledObjects;

	public int[] amountToBuffer;
	
	public int defaultBufferAmount = 3;

	protected GameObject containerObject;
	
	void Awake ()
	{
		instance = this;
	}
	
	// Use this for initialization
	void Start ()
	{
		containerObject = new GameObject("ObjectPool");
		
		//Loop through the object prefabs and make a new list for each one.
		//We do this because the pool can only support prefabs set to it in the editor,
		//so we can assume the lists of pooled objects are in the same order as object prefabs in the array
		pooledObjects = new List<GameObject>[objectPrefabs.Length];
		
		int i = 0;
		foreach ( GameObject objectPrefab in objectPrefabs )
		{
			pooledObjects[i] = new List<GameObject>(); 
			
			int bufferAmount;
			
			if(i < amountToBuffer.Length) bufferAmount = amountToBuffer[i];
			else
				bufferAmount = defaultBufferAmount;
			
			for ( int n=0; n<bufferAmount; n++)
			{
				GameObject newObj = Instantiate(objectPrefab) as GameObject;
				newObj.name = objectPrefab.name;
				PoolObject(newObj);
			}
			
			i++;
		}
	}

	public GameObject GetObjectForType ( string objectType , bool onlyPooled )
	{
		for(int i=0; i<objectPrefabs.Length; i++)
		{
			GameObject prefab = objectPrefabs[i];
			if(prefab.name == objectType)
			{
				
				if(pooledObjects[i].Count > 0)
				{
					GameObject pooledObject = pooledObjects[i][0];
					pooledObjects[i].RemoveAt(0);
					pooledObject.transform.parent = null;
					pooledObject.SetActiveRecursively(true);
					
					return pooledObject;
					
				} else if(!onlyPooled) {
					return Instantiate(objectPrefabs[i]) as GameObject;
				}
				
				break;
				
			}
		}
		
		//If we have gotten here either there was no object of the specified type or non were left in the pool with onlyPooled set to true
		return null;
	}

	public void PoolObject ( GameObject obj )
	{
		for ( int i=0; i<objectPrefabs.Length; i++)
		{
			if(objectPrefabs[i].name == obj.name)
			{
				obj.SetActiveRecursively(false);
				obj.transform.parent = containerObject.transform;
				pooledObjects[i].Add(obj);
				return;
			}
		}
	}
	
}