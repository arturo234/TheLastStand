using UnityEngine;
using System.Collections;

public class TrackingEnemy : GenericCharacter {
	Vector3 playerPosition, diff;
	GameObject arrow;
	float rotation;
	
	
	// Update is called once per frame
	void Update () 
	{
		RotateToPlayer ();
		theta = new Vector3(0, 0, rotation);//z value controls rotation, 0 is facing to the right
		arrowDir = new Vector3 (Mathf.Cos(theta.z * Mathf.PI / 180), Mathf.Sin(theta.z * Mathf.PI / 180));
		currentTime += Time.deltaTime;
		
		if (currentTime >= fireRate) 
		{
			arrow = ObjectPool.instance.GetObjectForType("BasicProjectile", false);
			arrow.transform.position = transform.position;
			arrow.transform.rotation = transform.rotation;
			arrow.rigidbody2D.velocity = arrowDir * arrowVelocity;
			arrow.tag = "EnemyArrow";
			currentTime = 0;
			//All the arrow prefab needs is a rigidBody2D with everything 0'd out.
			//The arrow will only have to time out and kill itself.
			
		}
		if (health <= 0) 
		{
			Destroy(this.gameObject);
		}
	}
	
	//Rotate to face a player
	private void RotateToPlayer()
	{
		GameObject player = GameObject.Find("Player");
		Transform playerTransform = player.transform;
		// get player position
		playerPosition = playerTransform.position;
		playerPosition = new Vector3(playerPosition.x, playerPosition.y, 0);
		diff = playerPosition - transform.position;
		diff.Normalize();
		rotation = Mathf.Atan2(diff.y, diff.x) * Mathf.Rad2Deg;
		transform.rotation = Quaternion.Euler(0f, 0f, rotation);
		
	}
	
	
	public void OnTriggerEnter2D(Collider2D col) 
	{
		if (col.tag == "EnemyArrow") return;
		if (col.gameObject.tag.Equals("PlayerArrow")) 
		{
			health--;
			
			Destroy(col.gameObject);
			//ObjectPool.instance.PoolObject(col.gameObject);
			//causes arrow to stick, cleans up after enough arrows have been
			//shot by player.
			//col.rigidbody2D.velocity = Vector2.zero;
			
		}
	}
	
	
}


