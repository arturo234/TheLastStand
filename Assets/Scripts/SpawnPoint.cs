using UnityEngine;
using System.Collections;


public class SpawnPoint : MonoBehaviour {
	public GameObject spawnEnemy; //We can make this is a list later if multiple types of ememies need to spawn from the same point.
	public float delay;
	private bool EnemyCheck = false;
	private float Timer;
	private GameObject gameObject;

	void  Start (){
		//makes spawnpoints invisible during gameplay
		renderer.enabled = false;
		Timer = Time.time + delay;
	}

	void  Update (){
		if (Timer < Time.time && !EnemyCheck) { // check if real time has caught up with timer
			gameObject = ObjectPool.instance.GetObjectForType(spawnEnemy.name, false);
			gameObject.transform.position = transform.position;
			gameObject.transform.rotation = transform.rotation;
			Timer = Time.time + delay;
			EnemyCheck = true;
		}
	}

}
