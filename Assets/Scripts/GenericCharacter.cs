

using UnityEngine;
using System.Collections;


public class GenericCharacter : MonoBehaviour
{
    public double health, fireRate;
    public float arrowVelocity;
    protected Vector3 theta, arrowDir;
	protected double currentTime;
	protected GameObject arrow;
    // Shared projectile object pool for all generic characters

	protected void RePool(GameObject obj) {
		//added to fix issues with projectiles being repooled
		if (obj.name =="BasicProjectile") 
		{
			this.gameObject.tag = "";
		}
		ObjectPool.instance.PoolObject(obj);
	}

	protected void fireArrow() {
		arrow = ObjectPool.instance.GetObjectForType("BasicProjectile", false);
		arrow.transform.position = transform.position;
		arrow.transform.rotation = transform.rotation;
		arrowDir = new Vector3(Mathf.Cos(transform.eulerAngles.z * Mathf.PI/180), Mathf.Sin(transform.eulerAngles.z * Mathf.PI/180));
		arrow.rigidbody2D.velocity = arrowDir * arrowVelocity;
	}
}