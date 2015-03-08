using UnityEngine;
using System.Collections;

public class GenericEnemy : GenericCharacter {

	GameObject arrow;

	// Use this for initialization
	void Start () 
    {
		theta = new Vector3(0, 0, Random.value*360);
		arrowDir = new Vector3 (Mathf.Cos(theta.z * Mathf.PI / 180), Mathf.Sin(theta.z * Mathf.PI / 180));
		transform.Rotate(theta);
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
			//All the arrow prefab needs is a rigidBody2D with everything 0'd out.
			//The arrow will only have to time out and kill itself.
            
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
