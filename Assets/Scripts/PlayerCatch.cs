using UnityEngine;
using System.Collections;

public class PlayerCatch : MonoBehaviour {

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
	public void OnTriggerStay2D(Collider2D col){
		//catches on click of right mouse button or spacebar
		if (col.gameObject.tag.Equals("EnemyArrow")&&(Input.GetMouseButtonDown(1)||Input.GetKeyDown(KeyCode.LeftShift)))
		{
			if(playerScript.ammo <= playerScript.ammoLimit )
			{
				ObjectPool.instance.PoolObject(col.gameObject);
				playerScript.ammo++;
				Debug.Log("Arrow Caught. New Ammo is :"+ playerScript.ammo);
			}
			else
			{
				Debug.Log("Ammo is full");
			}
		}
	}
}
