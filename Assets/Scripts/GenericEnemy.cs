using UnityEngine;
using System.Collections;

public class GenericEnemy : GenericCharacter {

    public ObjectPool projectilePool;


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
			ProjectileUpDate();
			currentTime = 0;
			//All the arrow prefab needs is a rigidBody2D with everything 0'd out.
			//The arrow will only have to time out and kill itself.
            
		}
	}

    public override void ProjectileUpDate()
    {
        GameObject arrow = projectilePool.PullObject();
        arrow.transform.position = transform.position;
        arrow.transform.rotation = transform.rotation;
        arrow.GetComponent<BasicProjectile>().SpawnArrow(Time.time, this, projectilePool);
        arrow.SetActive(true);
        arrow.rigidbody2D.velocity = arrowDir * arrowVelocity;
	
    }

    public override void DestroyProjectile(GameObject objToDestroy)
    {
        objToDestroy.rigidbody2D.velocity = Vector2.zero;
        projectilePool.PushObject(objToDestroy.gameObject.GetComponent(typeof(IPoolableObject)) as IPoolableObject);
    }


	
	public void OnTriggerEnter2D(Collider2D col) 
    {
        if (col.tag == "EnemyArrow") return;
		if (col.gameObject.tag.Equals("PlayerArrow")) 
		{
			health--;
			//causes arrow to stick, cleans up after enough arrows have been
			//shot by player.
            col.rigidbody2D.velocity = Vector2.zero;
		}
	}

   
}
