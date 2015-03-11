using UnityEngine;
using System.Collections;

public class GenericEnemy : GenericCharacter {
	// Use this for initialization
	void Start () 
	{
		theta = new Vector3(0, 0, 0);//z value controls rotation, 0 is facing to the right
		//transform.Rotate(theta);
	}
	
	// Update is called once per frame
	void Update () 
	{
		currentTime += Time.deltaTime;
		if (currentTime >= fireRate) 
		{
			fireArrow();
			arrow.tag = "EnemyArrow";
			currentTime = 0;
		}

		if (health <= 0) 
		{
			RePool(this.gameObject);
		}
	}
	
	public void OnTriggerEnter2D(Collider2D col) 
	{
		if (col.tag == "EnemyArrow") return;
		if (col.gameObject.tag.Equals("PlayerArrow")) 
		{
			health--;
			RePool(col.gameObject);
		}
	}
}