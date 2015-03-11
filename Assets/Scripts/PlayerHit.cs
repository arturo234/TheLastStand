using UnityEngine;
using System.Collections;

public class PlayerHit : MonoBehaviour {
	
	//get script from parent class (Player.cs)
	Player playerScript;
	GenericEnemy enemyScript;
	// Use this for initialization
	void Start () {
		renderer.enabled = false;//makes catch radius invisible
		playerScript = transform.parent.GetComponent<Player>();
		
	}
	
	// Update is called once per frame
	void Update () {

	}
	public void OnTriggerEnter2D(Collider2D col) {
		if (col.gameObject.tag.Equals("EnemyArrow")) 
		{
			Debug.Log("Damage Taken!");
			playerScript.health--;
			ObjectPool.instance.PoolObject(col.gameObject);
		}
	}
}