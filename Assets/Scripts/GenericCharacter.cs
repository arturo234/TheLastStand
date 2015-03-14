using UnityEngine;
using System.Collections;

public class GenericCharacter : MonoBehaviour
{
    public double health, standardHealth, fireRate;
    public float arrowVelocity;
    protected Vector3 theta, arrowDir;
	protected double currentTime;
	protected GameObject arrow;
    // Shared projectile object pool for all generic characters

	void Start() {
		health = standardHealth;
	}

	protected void RePool(GameObject obj) {
		//added to fix issues with projectiles being repooled
		if (obj.name.Equals("BasicProjectile")) 
		{
			obj.tag = "";
		}
		ObjectPool.instance.PoolObject(obj);
	}

	protected void fireArrow() {
		arrow = ObjectPool.instance.GetObjectForType("BasicProjectile", true);
		arrow.transform.position = transform.position;
		arrow.transform.rotation = transform.rotation;
		arrowDir = new Vector3(Mathf.Cos(transform.eulerAngles.z * Mathf.PI/180), Mathf.Sin(transform.eulerAngles.z * Mathf.PI/180));
		arrow.rigidbody2D.velocity = arrowDir * arrowVelocity;
	}

	void OnEnable() {
		health = standardHealth;
	}
}