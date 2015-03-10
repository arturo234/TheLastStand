using UnityEngine;
using System.Collections;

public class BasicProjectile : MonoBehaviour, IPoolableObject 
{
    private double timeSpawned;
    private double selfDestructTime;
    private double currentTime;

	// Use this for initialization
	void Start () 
    {
        selfDestructTime = 3.0f;
	}
	
	// Update is called once per frame
	void Update () 
    {
        currentTime += Time.deltaTime;
        if (currentTime + timeSpawned >= timeSpawned + selfDestructTime)
        {
			RemoveArrow();
        }
	}

    public void RemoveArrow()
    {
		Destroy(this.gameObject);
		//ObjectPool.instance.PoolObject(gameObject);
    }
}
