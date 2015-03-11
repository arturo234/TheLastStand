using UnityEngine;
using System.Collections;

public class GenericEnemy : GenericCharacter {
	
	GameObject arrow;
	// Use this for initialization
	void Start () 
	{
		theta = new Vector3(0, 0, 0);//z value controls rotation, 0 is facing to the right
		arrowDir = new Vector3 (Mathf.Cos(theta.z * Mathf.PI / 180), Mathf.Sin(theta.z * Mathf.PI / 180));
		//transform.Rotate(theta);
	}
	
	// Update is called once per frame
	void Update () 
	{
		currentTime += Time.deltaTime;
		if (currentTime >= fireRate) 
		{
			arrow = ObjectPool.instance.GetObjectForType("BasicProjectile", false);
			arrow.transform.position = transform.position;
			arrow.transform.rotation = transform.rotation;
			arrow.rigidbody2D.velocity = arrowDir * arrowVelocity;
			arrow.tag = "EnemyArrow";
			currentTime = 0;
		}


		if (health <= 0) 
		{
			Destroy(this.gameObject);
		}
	}
	
	public void OnTriggerEnter2D(Collider2D col) 
	{
		if (col.tag == "EnemyArrow") return;
		if (col.gameObject.tag.Equals("PlayerArrow")) 
		{
			health--;
			ObjectPool.instance.PoolObject(this.gameObject);
		}
	}
}