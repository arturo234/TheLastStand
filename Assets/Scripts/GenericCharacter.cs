

using UnityEngine;
using System.Collections;


public class GenericCharacter : MonoBehaviour
{
    public double health, fireRate;
    public float arrowVelocity;
    protected Vector3 theta, arrowDir;
	protected double currentTime;
    // Shared projectile object pool for all generic characters
}