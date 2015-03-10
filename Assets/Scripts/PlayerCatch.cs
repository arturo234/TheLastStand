using UnityEngine;
using System.Collections;

public class PlayerCatch : MonoBehaviour {

	//get script from parent class (Player.cs)
	Player playerScript;
	GenericEnemy enemyScript;
	// Use this for initialization
	void Start () {
		playerScript = transform.parent.GetComponent<Player>();

	}
	
	// Update is called once per frame
	void Update () {

	}
	public void OnTriggerStay2D(Collider2D col){
		if (col.gameObject.tag.Equals("EnemyArrow")&&Input.GetKeyDown(KeyCode.C)) 
		{
			if(playerScript.ammo <= playerScript.ammoLimit )
			{
				Destroy(col.gameObject);
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
