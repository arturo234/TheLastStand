using UnityEngine;
using System.Collections;

public class BasicProjectile : MonoBehaviour, IPoolableObject 
{
    public GameObject gameObject
    {
        get
        {
            return this.transform.gameObject;
        }
    }

    private double timeSpawned;
    private double selfDestructTime;
    private double currentTime;
    private bool active;

    private ObjectPool pool;

    private GenericCharacter firingEntity;

	// Use this for initialization
	void Start () 
    {
        selfDestructTime = 3.0f;
	
	}
	
	// Update is called once per frame
	void Update () 
    {
        currentTime += Time.deltaTime;
        if (currentTime + timeSpawned >= timeSpawned + selfDestructTime && active)
        {
            firingEntity.DestroyProjectile(this.gameObject);
            active = false;
            
        }
		if (this.rigidbody2D.velocity.Equals(0) && active)
		{
			firingEntity.DestroyProjectile(this.gameObject);
			active = false;
			
		}
	}


    public void SpawnArrow(double time, GenericCharacter entity, ObjectPool pool)
    {
        
        timeSpawned = time;
        currentTime = 0f;
        active = true;
        firingEntity = entity;
    }

    public void RemoveArrow()
    {
        firingEntity.DestroyProjectile(this.gameObject);

    }
}
