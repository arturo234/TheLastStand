using UnityEngine;
using System.Collections;

public class GenericEnemy : MonoBehaviour {
	
	public double health, fireRate;
	public float arrowVelocity;
	public GameObject arrowPrefab;
	double currentTime;
	
	Vector3 theta, arrowDir;

    // Shared projectile object pool for all generic enemies
    public static ObjectPool projectilePool;
	
	// Use this for initialization
	void Start () 
    {
		theta = new Vector3(0, 0, Random.value*360);
		arrowDir = new Vector3 (Mathf.Cos(theta.z * Mathf.PI / 180), Mathf.Sin(theta.z * Mathf.PI / 180));
		transform.Rotate(theta);

        if (projectilePool == null)
            projectilePool = new ObjectPool(arrowPrefab, false, 16);
	}
	
	// Update is called once per frame
	void Update () 
    {
		currentTime += Time.deltaTime;
		if (currentTime >= fireRate) 
        {
            GameObject arrow = projectilePool.PullObject();
            arrow.transform.position = transform.position;
            arrow.transform.rotation = transform.rotation;
            arrow.GetComponent<BasicProjectile>().SpawnArrow(Time.time, this);
            arrow.SetActive(true);
			arrow.rigidbody2D.velocity = arrowDir * arrowVelocity;
			currentTime = 0;
			//All the arrow prefab needs is a rigidBody2D with everything 0'd out.
			//The arrow will only have to time out and kill itself.
            
		}
	}
	
	void OnColliderEnter2D(Collider col) 
    {
        if (!col.tag.Equals("Enemy")) //Incase of non hero hits. 
            // Messy, there will be an internal Action to handle objects going back to the pool OnDisable
            DestroyProjectile(col.gameObject);
	}

    public void DestroyProjectile(GameObject objToDestroy)
    {
        projectilePool.PushObject(objToDestroy.gameObject.GetComponent(typeof(IPoolableObject)) as IPoolableObject);
    }
}
