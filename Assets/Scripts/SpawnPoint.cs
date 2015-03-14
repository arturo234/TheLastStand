using UnityEngine;
using System.Collections;


public class SpawnPoint : MonoBehaviour {
	public GameObject spawnEnemy; //We can make this is a list later if multiple types of ememies need to spawn from the same point.
	public float delay;
	private bool EnemyCheck = false;
	private float Timer;
	private GameObject spawnedObject;

	void  Start (){
		//makes spawnpoints invisible during gameplay
		renderer.enabled = false;
		Timer = Time.time + delay;
	}

	void  Update (){
		if (Timer < Time.time && !EnemyCheck) { // check if real time has caught up with timer
			spawnedObject = ObjectPool.instance.GetObjectForType(spawnEnemy.name, true);
			if(spawnedObject == null) {
				Timer += 2;
			} else {
				spawnedObject.transform.position = transform.position;
				spawnedObject.transform.rotation = transform.rotation;
				Debug.Log(spawnedObject.transform.position);
				EnemyCheck = true;
			}
		}
	}
}