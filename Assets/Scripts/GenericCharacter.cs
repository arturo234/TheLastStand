

using UnityEngine;
using System.Collections;


public class GenericCharacter : MonoBehaviour
{
    public double health, fireRate;
    public float arrowVelocity;
    public GameObject arrowPrefab;
    public double currentTime;
    public Vector3 theta, arrowDir;
    // Shared projectile object pool for all generic characters

    public virtual void ProjectileUpDate()
    {

    }

    public virtual void DestroyProjectile(GameObject objToDestroy)
    {
    }
}


