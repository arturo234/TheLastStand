using UnityEngine;
using System.Collections;


public class SpawnPoint : MonoBehaviour {
	//We can make this is a list later if multiple types of ememies need to spawn from the same point.
	public GameObject spawnEnemy;
	public float delay;
	private bool EnemyCheck = false;
	private float Timer;
	private GameObject spawnedObject;
	//Spawn Position
	private Vector2 sPosition;
	void  Start (){
		//makes spawnpoints invisible during gameplay
		renderer.enabled = false;
		Timer = Time.time + delay;
		sPosition = new Vector2(transform.position.x, transform.position.y);
		//Runs SpawnEnemy 1 second and repeats every Delay seconds
		InvokeRepeating ("SpawnEnemy", 1, delay);
	}
	
	void  SpawnEnemy() {
		Collider2D[] hitCollidersEnemy = Physics2D.OverlapCircleAll(sPosition, 1);
		if (Timer < Time.time && !EnemyCheck) { // check if real time has caught up with timer
			spawnedObject = ObjectPool.instance.GetObjectForType(spawnEnemy.name, true);
			if(spawnedObject == null) {
				Timer += 2;
			} else {
				spawnedObject.transform.position = transform.position;
				spawnedObject.transform.rotation = transform.rotation;
				Debug.Log(spawnedObject.transform.position);
				EnemyCheck = true;
				sPosition = new Vector2(spawnedObject.transform.position.x, spawnedObject.
				                        transform.position.y);
			}
		}
		else if (hitCollidersEnemy.Length == 0) {
			EnemyCheck = false;
		}
	}
}