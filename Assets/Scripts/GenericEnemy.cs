using UnityEngine;
using System.Collections;

public class GenericEnemy : GenericCharacter {
	
	GameObject arrow;
	// Use this for initialization
	void Start () 
	{
		//this.transform.position.z
		theta = new Vector3(0, 0, 0);//z value controls rotation, 0 is facing to the right
		arrowDir = new Vector3 (Mathf.Cos(theta.z * Mathf.PI / 180), Mathf.Sin(theta.z * Mathf.PI / 180));
		//transform.Rotate(theta);
	}
	
	// Update is called once per frame
	void Update () 
	{
		currentTime += Time.deltaTime;
		///the following code causes the enemy to only fire when the player is
		/// in line of sight (work in progress)

		//RaycastHit2D hit = Physics2D.Raycast (transform.position, -Vector2.up);
		//if(hit.collider.tag == "Player") 
		//{  
			//attack code goes here
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

		//}


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
			
			Destroy(col.gameObject);
			//ObjectPool.instance.PoolObject(col.gameObject);
			//causes arrow to stick, cleans up after enough arrows have been
			//shot by player.
			//col.rigidbody2D.velocity = Vector2.zero;
			
		}
	}
	
	
}